namespace Advent

open System.IO

type Item = Beam of int64 | Splitter

type World = Map<int (*col*), Item>[]

module Day7 =

    let update iter (world : World) : World =
        let beamPairs =
            world[iter]
                |> Seq.choose (fun (KeyValue(col, item)) ->
                    match item with
                        | Beam n -> Some (col, n)
                        | _ -> None)
                |> Seq.toArray
        let splitterCols =
            world[iter + 1]
                |> Seq.choose (fun (KeyValue(col, item)) ->
                    if item.IsSplitter then Some col
                    else None)
                |> set
        let nextRow =
            let pairs =
                [|
                    for (col, n) in beamPairs do
                        if splitterCols.Contains(col) then
                            assert(not (splitterCols.Contains(col - 1)))
                            assert(not (splitterCols.Contains(col + 1)))
                            yield (col - 1), Beam n
                            yield (col + 1), Beam n
                            yield col, Splitter
                        else
                            yield col, Beam n
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

    let run (rows : _[]) =
        (rows, [0 .. rows.Length - 2])
            ||> Seq.fold (fun (world : World) iter ->
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
