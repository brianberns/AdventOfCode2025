namespace Advent

open System
open System.IO

module Day5 =

    let parseFile path =
        let lines = File.ReadAllLines(path)
        let iSep = Array.findIndex ((=) "") lines
        assert(iSep >= 0 && iSep < lines.Length)

        let ranges =
            lines[.. iSep - 1]
                |> Array.map (fun line ->
                    let parts = line.Split('-')
                    assert(parts.Length = 2)
                    Int64.Parse(parts[0]),
                    Int64.Parse(parts[1]))

        let ids =        
            lines[iSep + 1 .. ]
                |> Array.map Int64.Parse

        ranges, ids

    let isInRange id (low, high) =
        id >= low && id <= high

    let part1 path =
        let ranges, ids = parseFile path
        ids
            |> Seq.where (fun id ->
                Seq.exists (isInRange id) ranges)
            |> Seq.length

    let part2 path =
        parseFile path
