namespace Advent

open System
open System.IO

type Id = int

type Graph = Map<Id, Set<Id>>

module Day8 =

    let parseFile path =
        let pointMap =
            File.ReadAllLines(path)
                |> Array.mapi (fun i line ->
                    let chunks =
                        line.Split(',') |> Array.map Int64.Parse
                    i + 1, (chunks[0], chunks[1], chunks[2]))
                |> Map
        let graph : Graph =
            pointMap.Keys
                |> Seq.map (fun id -> id, Set.empty)
                |> Map
        pointMap, graph

    let dist (xa, ya, za) (xb, yb, zb) =
        (pown (xa - xb) 2 + pown (ya - yb) 2 + pown (za - zb) 2)
            |> float
            |> sqrt

    let connect idA idB (graph : Graph) =
        graph
            |> Map.add idA (graph[idA].Add(idB))
            |> Map.add idB (graph[idB].Add(idA))

    let update (((idA, idB), _) :: rest) graph =
        let graph = connect idA idB graph
        rest, graph

    let getCircuits (graph : Graph) =

        let rec walk id (seen : Set<_>) =
            if seen.Contains(id) then seen
            else
                let seen = seen.Add(id)
                (seen, graph[id])
                    ||> Seq.fold (fun seen id ->
                        walk id seen)

        set graph.Keys
            |> Seq.unfold (fun unseen ->
                if Set.isEmpty unseen then None
                else
                    let seen = walk (Seq.head unseen) Set.empty
                    assert(seen - unseen |> Set.isEmpty)
                    let unseen = unseen - seen
                    Some (seen, unseen))

    let part1 path =
        let pointMap, graph = parseFile path
        let pointDists =
            List.sortBy snd [
                for i = 1 to pointMap.Count - 1 do
                    for j = i + 1 to pointMap.Count do
                        (i, j), dist pointMap[i] pointMap[j]
            ]
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

    let part2 path =
        parseFile path
