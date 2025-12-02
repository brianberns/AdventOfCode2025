namespace Advent

open System
open System.IO

module Day1 =

    let parseLine (line : string) =
        let rot = Int32.Parse line[1..]
        match line[0] with
            | 'R' -> rot
            | 'L' -> -rot
            | _ -> failwith "Unexpected"

    let parseFile path =
        File.ReadLines(path)
            |> Seq.map parseLine

    let countZeros rots =
        (50, rots)
            ||> Seq.scan (+)
            |> Seq.where (fun pos -> pos % 100 = 0)
            |> Seq.length

    let part1 path =
        parseFile path
            |> countZeros

    let part2 path =
        parseFile path
            |> Seq.collect (fun rot ->
                Seq.replicate (abs rot) (sign rot))
            |> countZeros
