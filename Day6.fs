namespace Advent

open System
open System.IO

module Day6 =

    let split (line : string) =
        line.Split(' ', StringSplitOptions.RemoveEmptyEntries)

    let parseOps (lines : string[]) : (int64 -> int64 -> int64)[] =
        split (Array.last lines)
            |> Array.map (function "+" -> (+) | "*" -> (*))

    let part1 path =
        let lines = File.ReadAllLines(path)
        let inputRows =
            lines[.. lines.Length - 2]
                |> Array.map (split >> Array.map Int64.Parse)
        (parseOps lines, Array.transpose inputRows)
            ||> Array.map2 Array.reduce
            |> Array.sum

    let part2 path =
        let lines = File.ReadAllLines(path)
        let inputs =
            let strs =
                lines[0 .. lines.Length - 2]
                    |> Array.map _.ToCharArray()
                    |> Array.transpose
                    |> Array.map (String >> _.Trim())
            (strs, [[]])
                ||> Array.foldBack (fun str (head :: tail) ->
                    if str = "" then [] :: (head :: tail)     // start a new chunk
                    else (Int64.Parse str :: head) :: tail)   // append to current chunk
        (List.ofArray (parseOps lines), inputs)
            ||> List.map2 List.reduce
            |> List.sum
