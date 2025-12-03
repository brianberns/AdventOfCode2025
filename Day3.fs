namespace Advent

open System
open System.IO

module Day3 =

    let parseFile path =
        File.ReadLines(path)
            |> Seq.map (fun line ->
                line.ToCharArray()
                    |> Array.map (fun c ->
                        int c - int '0'))

    let getPairs (array : _[]) =
        seq {
            for i = 0 to array.Length - 2 do
                for j = i + 1 to array.Length - 1 do
                    array[i], array[j]
        }

    let toJolts (a, b) =
        10 * a + b

    let part1 path =
        parseFile path
            |> Seq.map (
                getPairs >> Seq.map toJolts >> Seq.max)
            |> Seq.sum
