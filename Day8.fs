namespace Advent

open System
open System.IO

module Day8 =

    let dist (xa, ya, za) (xb, yb, zb) =
        (pown (xa - xb) 2 + pown (ya - yb) 2 + pown (za - zb) 2)
            |> float
            |> sqrt

    let parseFile path =
        let locs =
            File.ReadAllLines(path)
                |> Seq.mapi (fun id line ->
                    let chunks =
                        line.Split(',') |> Array.map Int64.Parse
                    id, (chunks[0], chunks[1], chunks[2]))
                |> Map
        let adj =
            locs.Keys
                |> Seq.map (fun id -> id, Set.empty)
                |> Map
        let dists =
            List.sortBy snd [
                for i = 0 to locs.Count - 2 do
                    for j = i + 1 to locs.Count - 1 do
                        (i, j), dist locs[i] locs[j] ]
        locs, dists, adj

    let connect idA idB (adj : Map<_, Set<_>>) =
        adj
            |> Map.add idA (adj[idA].Add(idB))
            |> Map.add idB (adj[idB].Add(idA))

    let update (((idA, idB), _) :: rest) adj =
        rest, connect idA idB adj

    let getCircuits (adj : Map<_, _>) =

        let rec walk (seen : Set<_>) id =
            if seen.Contains(id) then seen
            else Seq.fold walk (seen.Add(id)) adj[id]

        set adj.Keys
            |> Seq.unfold (fun unseen ->
                if unseen.IsEmpty then None
                else
                    let seen = walk Set.empty (Seq.head unseen)
                    Some (seen, unseen - seen))

    let part1 path =
        let _, dists, adj = parseFile path
        let _, adj =
            ((dists, adj), [1..1000])
                ||> Seq.fold (fun (dists, adj) _ -> update dists adj)
        getCircuits adj
            |> Seq.map _.Count
            |> Seq.sortDescending
            |> Seq.take 3
            |> Seq.reduce (*)

    let rec updateLoop ((((idA, idB), _) :: _) as dists) adj =
        let dists, adj = update dists adj
        if getCircuits adj |> Seq.length = 1 then idA, idB
        else updateLoop dists adj

    let part2 path =
        let locs, dists, adj = parseFile path
        let idA, idB = updateLoop dists adj
        let xa, _, _ = locs[idA]
        let xb, _, _ = locs[idB]
        xa * xb
