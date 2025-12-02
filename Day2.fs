namespace Advent

open System
open System.IO

module Day2 =

    let private parseFile path =
        File.ReadAllText(path)
            .Split(',')
            |> Array.map (fun str ->
                let split = str.Split('-')
                assert(split.Length = 2)
                Int64.Parse split[0],
                Int64.Parse split[1])

    let isValid (str : string) =
        if str.Length % 2 = 0 then
            let halfLen = str.Length / 2
            str[.. halfLen - 1] <> str[halfLen ..]
        else true

    let part1 path =
        parseFile path
            |> Seq.collect (fun (a, b) ->
                assert(b >= a)
                seq { a .. b })
            |> Seq.where (string >> isValid >> not)
            |> Seq.sum
