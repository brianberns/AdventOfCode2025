namespace Advent

open System
open System.IO

module Day1 =

    let part1 path =
        let lines = File.ReadAllLines(path)
        (50, lines)
            ||> Seq.scan (fun pos line ->
                let op =
                    match line[0] with
                        | 'R' -> (+)
                        | 'L' -> (-)
                        | _ -> failwith "Unexpected"
                let incr = Int32.Parse line[1..]
                op pos incr)
            |> Seq.where (fun pos -> pos % 100 = 0)
            |> Seq.length
