namespace Advent

open System.IO

type Item = Beam of int64 | Splitter

module Day7 =

    let update iter (world : Map<_, _>[]) =
        let splitterCols =
            set [
                for (KeyValue(col : int, (item : Item))) in world[iter + 1] do
                    if item.IsSplitter then col
                    else ()
            ]
        let nextRow =
            let pairs =
                [|
                    for (KeyValue(col, item)) in world[iter] do
                        match item with
                            | Beam n ->
                                if splitterCols.Contains(col) then
                                    yield (col - 1), Beam n
                                    yield (col + 1), Beam n
                                    yield col, Splitter
                                else
                                    yield col, Beam n
                            | _ -> ()
                |]
            pairs
                |> Array.groupBy fst
                |> Array.map (fun (col, group) ->
                    let items = Array.map snd group
                    let item =
                        match Array.tryExactlyOne items with
                            | Some item -> item
                            | None ->
                                let n = Seq.sumBy (fun (Beam n) -> n) items
                                Beam n
                    col, item)
                |> Map
        Array.updateAt (iter + 1) nextRow world

    let parseFile path =
        let lines = File.ReadAllLines(path)
        [|
            for line in lines do
                Map [
                    for col, c in Array.indexed (line.ToCharArray()) do
                        match c with
                            | 'S' -> col, Beam 1
                            | '^' -> col, Splitter
                            | '.' -> ()
                ]
        |]

    let run (world : _[]) =
        (world, [0 .. world.Length - 2])
            ||> Seq.fold (fun world iter ->
                update iter world)

    let part1 path =
        let rows = parseFile path
        run rows
            |> Seq.sumBy (fun itemMap ->
                itemMap.Values
                    |> Seq.where _.IsSplitter
                    |> Seq.length)

    let part2 path =
        let rows = parseFile path
        let last = run rows |> Array.last
        Seq.sumBy (fun (Beam n) -> n) last.Values
