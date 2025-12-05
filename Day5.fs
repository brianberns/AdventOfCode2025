namespace Advent

open System
open System.IO

open FRange

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
                    let low = Int64.Parse(parts[0])
                    let high = Int64.Parse(parts[1])
                    low +-+ high)

        let ids =        
            lines[iSep + 1 .. ]
                |> Array.map Int64.Parse

        ranges, ids

    let part1 path =
        let ranges, ids = parseFile path
        ids
            |> Seq.where (fun id ->
                Seq.exists (Range.contains id) ranges)
            |> Seq.length

    let getValue = function
        | Inclusive value -> value
        | _ -> failwith "Unexpected"

    let part2 path =
        let ranges, _ = parseFile path
        Range.merge ranges
            |> Seq.sumBy (fun range ->
                getValue range.Upper
                    - getValue range.Lower
                    + 1L)
