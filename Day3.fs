namespace Advent

open System.IO

module Day3 =

    let parseFile path =
        File.ReadLines(path)
            |> Seq.map (fun line ->
                line.ToCharArray()
                    |> Array.map (fun c ->
                        int c - int '0'))

    let rec getMaxSubseq len (items : _[]) =
        if len = 0 then Seq.empty
        else
            let i, item =
                Array.indexed items[.. items.Length - len]
                    |> Seq.maxBy snd
            seq {
                yield item
                yield! getMaxSubseq (len - 1) items[i + 1 ..]
            }

    let toJolts subseq =
        (0L, subseq)
            ||> Seq.fold (fun acc digit ->
                10L * acc + int64 digit)

    let part1 path =
        parseFile path
            |> Seq.map (
                getMaxSubseq 2
                    >> toJolts)
            |> Seq.sum

    let part2 path =
        parseFile path
            |> Seq.map (
                getMaxSubseq 12
                    >> toJolts)
            |> Seq.sum
