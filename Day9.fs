namespace Advent

open System
open System.IO

module Day9 =

    let parseFile path =
        File.ReadAllLines(path)
            |> Array.map (fun line ->
                let parts = line.Split(',') |> Array.map Int64.Parse
                parts[0], parts[1])

    let area (xa, xb) (ya, yb) =
        abs (xa - ya + 1L) * abs (xb - yb + 1L)

    let part1 path =
        let points = parseFile path
        Seq.max [
            for i = 0 to points.Length - 2 do
                for j = i + 1 to points.Length - 1 do
                    area points[i] points[j]
        ]

    let part2 path =
        parseFile path
