namespace Advent

open System
open System.IO

module Day6 =

    let parseOps (lines : string[]) : (int64 -> int64 -> int64)[] =
        (Array.last lines)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            |> Array.map (function
                | "+" -> (+)
                | "*" -> (*))

    let parseFile1 path =
        let lines = File.ReadAllLines(path)
        let inputRows =
            lines[.. lines.Length - 2]
                |> Array.map (fun line ->
                    line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        |> Array.map Int64.Parse)
        inputRows, parseOps lines

    let part1 path =
        let inputRows, ops = parseFile1 path
        Array.sum [|
            for col = 0 to inputRows[0].Length - 1 do
                let inputs =
                    inputRows
                        |> Array.map (fun inputRow ->
                            inputRow[col])
                let op = ops[col]
                Array.reduce op inputs
        |]

    let toNum chars =
        let digits =
            chars
                |> Array.choose (function
                    | ' ' -> None
                    | c -> Some (int64 c - int64 '0'))
        (0L, digits)
            ||> Array.fold (fun acc n -> 10L * acc + n)

    let parseFile2 path =
        let lines = File.ReadAllLines(path)
        let inputCols =
            [|
                for col = lines[0].Length - 1 downto 0 do
                    toNum [|
                        for row = 0 to lines.Length - 2 do
                            lines[row][col]
                    |]
            |]
        let inputCols =
            (inputCols, [])
                ||> Array.foldBack (fun col acc ->
                    if col = 0L then [] :: acc
                    else
                        match acc with
                            | head :: tail ->
                                (col :: head) :: tail
                            | [] -> [[col]])
                |> List.toArray
        let ops =
            parseOps lines
                |> Array.rev
        inputCols, ops

    let part2 path =
        let inputCols, ops = parseFile2 path
        Array.sum [|
            for col = 0 to inputCols.Length - 1 do
                let inputs = inputCols[col]
                let op = ops[col]
                List.reduce op inputs
        |]
