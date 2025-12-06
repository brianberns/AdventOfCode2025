namespace Advent

open System
open System.IO

module Day6 =

    let parseFile path =
        let lines = File.ReadAllLines(path)
        let inputRows =
            lines[.. lines.Length - 2]
                |> Array.map (fun line ->
                    line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        |> Array.map Int64.Parse)
        let ops =
            (Array.last lines)
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                |> Array.map (function
                    | "+" -> (fun (x : int64) (y : int64) -> x + y)
                    | "*" -> (fun (x : int64) (y : int64) -> x * y)
                    | _ -> failwith "Unexpected")
        inputRows, ops

    let part1 path =
        let inputRows, ops = parseFile path
        Array.sum [|
            for col = 0 to inputRows[0].Length - 1 do
                let inputs =
                    inputRows
                        |> Array.map (fun inputRow ->
                            inputRow[col])
                let op = ops[col]
                Array.reduce op inputs
        |]


    let part2 path =
        parseFile path
            |> ignore
