namespace Advent

module Program =

    let problems =
        [
            Day1.part1, "Day1.input.txt", "Day 1, part 1"
            Day1.part2, "Day1.input.txt", "Day 1, part 2"
        ]
    for f, input, name in problems do
        let result = f input
        printfn $"{name}: {result}"
