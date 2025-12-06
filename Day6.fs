namespace Advent

open System
open System.IO

module Day6 =

    let split (line : string) =
        line.Split(' ', StringSplitOptions.RemoveEmptyEntries)

    let parseOps (lines : string[]) : (int64 -> int64 -> int64)[] =
        Array.last lines
            |> split
            |> Array.map (function "+" -> (+) | "*" -> (*))

    let parseFile1 path =
        let lines = File.ReadAllLines(path)
        let inputRows =
            lines[.. lines.Length - 2]
                |> Array.map (split >> Array.map Int64.Parse)
        inputRows, parseOps lines

    let part1 path =
        let inputRows, ops = parseFile1 path
        (ops, Array.transpose inputRows)
            ||> Array.map2 Array.reduce
            |> Array.sum

    let chunkByEmptyString strs =
        (strs, [[]])
            ||> Array.foldBack (fun str (head :: tail) ->
                if str = "" then
                    [] :: (head :: tail)
                else
                    (Int64.Parse str :: head) :: tail)

    let parseFile2 path =
        let lines = File.ReadAllLines(path)
        let inputs =
            lines[0 .. lines.Length - 2]
                |> Array.map _.ToCharArray()
                |> Array.transpose
                |> Array.map (String >> _.Trim())
                |> chunkByEmptyString
        inputs, List.ofArray (parseOps lines)

    let part2 path =
        let inputs, ops = parseFile2 path
        (ops, inputs)
            ||> List.map2 List.reduce
            |> List.sum
