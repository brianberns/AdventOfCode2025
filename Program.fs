namespace Advent

open System.Diagnostics

module Program =

    let problems =
        [
            (*
            Day1.part1 >> string, "Day1.input.txt", "Day 1, part 1"
            Day1.part2 >> string, "Day1.input.txt", "Day 1, part 2"

            Day2.part1 >> string, "Day2.input.txt", "Day 2, part 1"
            Day2.part2 >> string, "Day2.input.txt", "Day 2, part 2"

            Day3.part1 >> string, "Day3.input.txt", "Day 3, part 1"
            Day3.part2 >> string, "Day3.input.txt", "Day 3, part 2"

            Day4.part1 >> string, "Day4.input.txt", "Day 4, part 1"
            Day4.part2 >> string, "Day4.input.txt", "Day 4, part 2"

            Day5.part1 >> string, "Day5.input.txt", "Day 5, part 1"
            Day5.part2 >> string, "Day5.input.txt", "Day 5, part 2"

            Day6.part1 >> string, "Day6.input.txt", "Day 6, part 1"
            Day6.part2 >> string, "Day6.input.txt", "Day 6, part 2"
            *)

            Day7.part1 >> string, "Day7.input.txt", "Day 7, part 1"
            Day7.part2 >> string, "Day7.input.txt", "Day 7, part 2"
        ]
    let stopwatch = Stopwatch()
    for f, input, name in problems do
        stopwatch.Restart()
        let result = f input
        stopwatch.Stop()
        printfn $"{name}: {result} ({stopwatch.ElapsedMilliseconds} ms)"
