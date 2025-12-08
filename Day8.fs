namespace Advent

open System
open System.IO

module Day8 =

    let dist (xa, ya, za) (xb, yb, zb) =
        (pown (xa - xb) 2 + pown (ya - yb) 2 + pown (za - zb) 2)
            |> float
            |> sqrt

    let parseFile path =
        let pointMap =
            File.ReadAllLines(path)
                |> Array.mapi (fun id line ->
                    let chunks =
                        line.Split(',') |> Array.map Int64.Parse
                    id, (chunks[0], chunks[1], chunks[2]))
                |> Map
        let graph =
            pointMap.Keys
                |> Seq.map (fun id -> id, Set.empty)
                |> Map
        let pointDists =
            List.sortBy snd [
                for i = 0 to pointMap.Count - 2 do
                    for j = i + 1 to pointMap.Count - 1 do
                        (i, j), dist pointMap[i] pointMap[j]
            ]
        pointMap, pointDists, graph

    let connect idA idB (graph : Map<_, Set<_>>) =
        graph
            |> Map.add idA (graph[idA].Add(idB))
            |> Map.add idB (graph[idB].Add(idA))

    let update (((idA, idB), _) :: rest) graph =
        let graph = connect idA idB graph
        rest, graph

    let getCircuits (graph : Map<_, _>) =

        let rec walk (seen : Set<_>) id =
            if seen.Contains(id) then seen
            else Seq.fold walk (seen.Add(id)) graph[id]

        set graph.Keys
            |> Seq.unfold (fun (unseen: Set<_>) ->
                if unseen.IsEmpty then None
                else
                    let seen = walk Set.empty (Seq.head unseen)
                    Some (seen, unseen - seen))

    let part1 path =
        let _, pointDists, graph = parseFile path
        let _, graph =
            ((pointDists, graph), [1..1000])
                ||> Seq.fold (fun (pointDists, graph) _ ->
                    update pointDists graph)
        let circuits = getCircuits graph |> Seq.toArray
        circuits
            |> Seq.map _.Count
            |> Seq.sortDescending
            |> Seq.take 3
            |> Seq.reduce (*)

    let rec updateLoop ((((idA, idB), _) :: _) as pointDists) graph =
        let pointDists, graph = update pointDists graph
        if getCircuits graph |> Seq.length = 1 then idA, idB
        else updateLoop pointDists graph

    let part2 path =
        let pointMap, pointDists, graph = parseFile path
        let idA, idB = updateLoop pointDists graph
        let xa, _, _ = pointMap[idA]
        let xb, _, _ = pointMap[idB]
        xa * xb
