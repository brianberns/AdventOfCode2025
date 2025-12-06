namespace Advent

open System
open System.IO

module Day6 =

    let split (line : string) =
        line.Split(' ', StringSplitOptions.RemoveEmptyEntries)

    let parseOps lines =
        split (Array.last lines)
            |> Array.map (function "+" -> (+) | "*" -> (*))

    let apply ops inputs =
        Seq.map2 Seq.reduce ops inputs
            |> Seq.sum

    let part1 path =
        let lines = File.ReadAllLines(path)
        lines[.. lines.Length - 2]
            |> Array.map (split >> Array.map Int64.Parse)
            |> Array.transpose
            |> apply (parseOps lines)

    let part2 path =
        let lines = File.ReadAllLines(path)
        let strs =
            lines[0 .. lines.Length - 2]
                |> Array.map _.ToCharArray()
                |> Array.transpose
                |> Array.map (String >> _.Trim())
        (strs, [[]])
            ||> Array.foldBack (fun str (head :: tail) ->
                if str = "" then [] :: (head :: tail)     // start a new chunk
                else (Int64.Parse str :: head) :: tail)   // append to current chunk
            |> apply (parseOps lines)
