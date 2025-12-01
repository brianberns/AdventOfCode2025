namespace Advent

open System
open System.IO

module Day1 =

    let private parseLine (line : string) =
        let rot = Int32.Parse line[1..]
        match line[0] with
            | 'R' -> rot
            | 'L' -> -rot
            | _ -> failwith "Unexpected"

    let private parseFile path =
        File.ReadAllLines(path)
            |> Seq.map parseLine

    let part1 path =
        (50, parseFile path)
            ||> Seq.scan (+)
            |> Seq.where (fun pos -> pos % 100 = 0)
            |> Seq.length

    let part2 path =
        (50, parseFile path)
            ||> Seq.mapFold (fun pos rot ->
                let seq =
                    let sign = sign rot
                    seq { pos + sign .. sign .. pos + rot }   // elegant but slow
                seq, pos + rot)
            |> fst
            |> Seq.concat
            |> Seq.where (fun pos -> pos % 100 = 0)
            |> Seq.length
