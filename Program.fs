namespace Advent

module Program =

    let day1_part1 = Day1.part1 "Day1.input.txt"
    assert(day1_part1 = 1084)
    printfn "Day 1, part 1: %A" day1_part1

    let day1_part2 = Day1.part2 "Day1.input.txt"
    assert(day1_part2 = 6475)
    printfn "Day 1, part 2: %A" day1_part2
