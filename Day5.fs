namespace Advent

open System
open System.IO

open FRange

module Day5 =

    let parseFile path =
        let lines = File.ReadAllLines(path)
        let iSep = Array.findIndex ((=) "") lines

        let ranges =
            lines[.. iSep - 1]
                |> Array.map (fun line ->
                    let parts =
                        line.Split('-')
                            |> Array.map Int64.Parse
                    parts[0] +-+ parts[1])

        let ids =
            lines[iSep + 1 ..]
                |> Array.map Int64.Parse

        ranges, ids

    let part1 path =
        let ranges, ids = parseFile path
        ids
            |> Array.where (fun id ->
                Array.exists (Range.contains id) ranges)
            |> Array.length

    let (~~) (Inclusive value) = value

    let part2 path =
        parseFile path
            |> fst
            |> Range.merge
            |> List.sumBy (fun range ->
                ~~range.Upper - ~~range.Lower + 1L)
