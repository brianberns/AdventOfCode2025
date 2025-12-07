namespace Advent

open System.IO

type Item = Beam of int64 | Splitter

module Day7 =

    let update (world : _[][]) iter =
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
                                yield col - 1, Beam n; yield col + 1, Beam n
                                yield col, Splitter
                            else
                                yield col, Beam n
                        | _ -> ()
            |]
                |> Array.groupBy fst
                |> Array.map (fun (col, group) ->
                    let items = Array.map snd group
                    let item =
                        if items.Length = 1 then items[0]
                        else Array.sumBy (fun (Beam n) -> n) items |> Beam
                    col, item)
        Array.updateAt (iter + 1) nextRow world

    let parseFile path =
        let lines = File.ReadAllLines(path)
        [|
            for line in lines do [|
                for col, c in Array.indexed (line.ToCharArray()) do
                    match c with
                        | 'S' -> col, Beam 1
                        | '^' -> col, Splitter
                        | '.' -> ()
            |]
        |]

    let run (world : _[][]) =
        Array.fold update world [| 0 .. world.Length - 2 |]

    let part1 path =
        parseFile path
            |> run
            |> Array.sumBy (fun posItems ->
                posItems
                    |> Array.where (snd >> _.IsSplitter)
                    |> Array.length)

    let part2 path =
        parseFile path
            |> run
            |> Array.last
            |> Array.sumBy (fun (_, Beam n) -> n)
