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

    let private generatePositions path =
        let lines = File.ReadAllLines(path)
        (50, lines)
            ||> Seq.scan (fun pos line ->
                pos + parseLine line)

    let part1 path =
        generatePositions path
            |> Seq.where (fun pos -> pos % 100 = 0)
            |> Seq.length

    let part2 path =

        let f pos =
            if pos >= 0 then pos / 100
            else (pos - 100) / 100

        generatePositions path
            |> Seq.pairwise
            |> Seq.map (fun (pos, pos') ->
                abs ((f pos') - (f pos)))
            |> Seq.sum
