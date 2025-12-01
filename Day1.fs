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
