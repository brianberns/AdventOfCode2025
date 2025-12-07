namespace Advent

open System.IO

type Item = Beam | Splitter

type World = Map<int (*col*), Item>[]

module Day7 =

    let update iter (world : World) : World =
        let beamCols =
            world[iter]
                |> Seq.choose (fun (KeyValue(col, item)) ->
                    if item.IsBeam then Some col
                    else None)
                |> Seq.toArray
        let splitterCols =
            world[iter + 1]
                |> Seq.choose (fun (KeyValue(col, item)) ->
                    if item.IsSplitter then Some col
                    else None)
                |> set
        let nextRow =
            Map [
                for col in beamCols do
                    if splitterCols.Contains(col) then
                        assert(not (splitterCols.Contains(col - 1)))
                        assert(not (splitterCols.Contains(col + 1)))
                        yield (col - 1), Beam
                        yield (col + 1), Beam
                        yield col, Splitter
                    else
                        yield col, Beam
            ]
        Array.updateAt (iter + 1) nextRow world

    let parseFile path =
        let lines = File.ReadAllLines(path)
        [|
            for line in lines do
                Map [
                    for col, c in Array.indexed (line.ToCharArray()) do
                        match c with
                            | 'S' -> col, Beam
                            | '^' -> col, Splitter
                            | '.' -> ()
                ]
        |]

    let part1 path =
        let rows = parseFile path
        (rows, [0 .. rows.Length - 2])
            ||> Seq.fold (fun (world : World) iter ->
                update iter world)
            |> Seq.sumBy (fun itemMap ->
                itemMap.Values
                    |> Seq.where _.IsSplitter
                    |> Seq.length)

    let part2 path =
        parseFile path
