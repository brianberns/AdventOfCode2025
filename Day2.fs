namespace Advent

open System
open System.IO

module Day2 =

    let parseFile path =
        File.ReadAllText(path)
            .Split(',')
            |> Array.map (fun str ->
                let split = str.Split('-')
                assert(split.Length = 2)
                Int64.Parse split[0],
                Int64.Parse split[1])

    let getInputs path =
        parseFile path
            |> Array.collect (fun (a, b) ->
                assert(b >= a)
                [| a .. b |])

    let isValid nChunks (str : string) =
        if nChunks > 1 && str.Length % nChunks = 0 then
            let size = str.Length / nChunks
            str.ToCharArray()
                |> Array.chunkBySize size
                |> Array.map String
                |> Array.distinct
                |> Array.length > 1
        else true

    let getDivisors n =
        [|
            for m = 1 to int (sqrt (float n)) do
                if n % m = 0 then
                    yield m
                    if n / m <> m then
                        yield n / m
        |]

    let isValidAll (str : string) =
        getDivisors str.Length
            |> Array.forall (fun n ->
                isValid n str)

    let part1 path =
        getInputs path
            |> Array.Parallel.filter (
                string >> isValid 2 >> not)
            |> Array.sum

    let part2 path =
        getInputs path
            |> Array.Parallel.filter (
                string >> isValidAll >> not)
            |> Array.sum
