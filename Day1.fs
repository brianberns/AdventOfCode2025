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

    let part1 path =
        let lines = File.ReadAllLines(path)
        (50, lines)
            ||> Seq.scan (fun pos line ->
                pos + parseLine line)
            |> Seq.where (fun pos -> pos % 100 = 0)
            |> Seq.length

    let part2 path =
        let lines = File.ReadAllLines(path)
        (50, lines)
            ||> Seq.mapFold (fun pos line ->
                let rot = parseLine line
                let seq =
                    let sign = sign rot
                    seq { pos + sign .. sign .. pos + rot }   // elegant but slow
                seq, pos + rot)
            |> fst
            |> Seq.concat
            |> Seq.where (fun pos -> pos % 100 = 0)
            |> Seq.length
