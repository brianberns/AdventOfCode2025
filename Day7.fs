namespace Advent

open System.IO

type Item = Beam of int64 | Splitter

module Day7 =

    let update iter (world : _[][]) =
        let splitterCols =
            set [
                for col, (item : Item) in world[iter + 1] do
                    if item.IsSplitter then col : int
                    else ()
            ]
        let nextRow =
            [|
                for col, item in world[iter] do
                    match item with
                        | Beam n ->
                            if splitterCols.Contains(col) then
                                yield col - 1, Beam n
                                yield col + 1, Beam n
                                yield col, Splitter
                            else
                                yield col, Beam n
                        | _ -> ()
            |]
                |> Array.groupBy fst
                |> Array.map (fun (col, group) ->
                    let items = Array.map snd group
                    let item =
                        match Array.tryExactlyOne items with
                            | Some item -> item
                            | None ->
                                items
                                    |> Seq.sumBy (fun (Beam n) -> n)
                                    |> Beam
                    col, item)
        Array.updateAt (iter + 1) nextRow world

    let parseFile path =
        let lines = File.ReadAllLines(path)
        [|
            for line in lines do
                [|
                    for col, c in Array.indexed (line.ToCharArray()) do
                        match c with
                            | 'S' -> col, Beam 1
                            | '^' -> col, Splitter
                            | '.' -> ()
                |]
        |]

    let run (world : _[]) =
        (world, [0 .. world.Length - 2])
            ||> Seq.fold (fun world iter ->
                update iter world)

    let part1 path =
        let rows = parseFile path
        run rows
            |> Seq.sumBy (fun posItems ->
                posItems
                    |> Seq.where (snd >> _.IsSplitter)
                    |> Seq.length)

    let part2 path =
        let rows = parseFile path
        let last = run rows |> Array.last
        Seq.sumBy (fun (_, Beam n) -> n) last
