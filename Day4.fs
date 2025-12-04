namespace Advent

open System.IO

module Day4 =

    let parseFile path =
        let jagged =
            File.ReadAllLines(path)
                |> Array.map (fun line ->
                    line.ToCharArray()
                        |> Array.map ((=) '@'))
        assert(
            jagged
                |> Seq.map Array.length
                |> Seq.distinct
                |> Seq.length = 1)
        Array2D.init
            jagged.Length
            jagged[0].Length
            (fun row col -> jagged[row][col])

    let getNeighbors row col (grid : _[,]) =
        seq {
            for rowIncr = -1 to 1 do
                for colIncr = -1 to 1 do
                    if rowIncr <> 0 || colIncr <> 0 then
                        let row = row + rowIncr
                        let col = col + colIncr
                        if row >= 0 && row < grid.GetLength(0)
                            && col >= 0 && col < grid.GetLength(1) then
                            row, col
        }

    let isAccessible row col (grid : _[,]) =
        if grid[row, col] then
            getNeighbors row col grid
                |> Seq.where (fun (row, col) -> grid[row, col])
                |> Seq.length < 4
        else false

    let getAccessibles (grid : _[,]) =
        [|
            for row = 0 to grid.GetLength(0) - 1 do
                for col = 0 to grid.GetLength(1) - 1 do
                    if isAccessible row col grid then
                        row, col
        |]

    let part1 path =
        parseFile path
            |> getAccessibles
            |> Array.length

    let removeAccessibles grid=
        let accessibles = getAccessibles grid
        let grid = Array2D.copy grid
        for row, col in accessibles do
            grid[row, col] <- false
        grid, accessibles.Length

    let removeLoop grid =

        let rec loop count grid =
            let grid, incr = removeAccessibles grid
            if incr = 0 then count
            else
                loop (count + incr) grid

        loop 0 grid

    let part2 path =
        parseFile path
            |> removeLoop
