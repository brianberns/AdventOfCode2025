namespace Advent

module Program =

    let check expected actual =
        if actual <> expected then
            printfn $"*** Failure: Expected: {expected}, but received {actual}"

    let day1_part1 = Day1.part1 "Day1.input.txt"
    printfn "Day 1, part 1: %A" day1_part1
    check 1084 day1_part1

    let day1_part2 = Day1.part2 "Day1.input.txt"
    printfn "Day 1, part 2: %A" day1_part2
    check 6475 day1_part2
