namespace Advent

module Program =

    let problems =
        [
            Day1.part1 >> string, "Day1.input.txt", "Day 1, part 1"
            Day1.part2 >> string, "Day1.input.txt", "Day 1, part 2"
            Day2.part1 >> string, "Day2.input.txt", "Day 2, part 1"
        ]
    for f, input, name in problems do
        let result = f input
        printfn $"{name}: {result}"
