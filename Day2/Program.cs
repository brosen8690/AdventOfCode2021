string[] moves = File.ReadAllText("../../../input.txt").Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToArray();

//Part 1
int depth = 0;
int horizontal = 0;

foreach (var move in moves)
{
    string[] splitMove = move.Split(' ');
    string direction = splitMove[0];
    int distance = int.Parse(splitMove[1]);

    switch (direction)
    {
        case "forward":
            horizontal += distance;
            break;
        case "down":
            depth += distance;
            break;
        case "up":
            depth -= distance;
            break;
        default:
            break;
    }
}

Console.WriteLine("Part 1: " + (depth * horizontal));

//Part 2
int aim = 0;
depth = 0;
horizontal = 0;

foreach (var move in moves)
{
    string[] splitMove = move.Split(' ');
    string direction = splitMove[0];
    int distance = int.Parse(splitMove[1]);

    switch (direction)
    {
        case "forward":
            horizontal += distance;
            depth += aim * distance;
            break;
        case "down":
            aim += distance;
            break;
        case "up":
            aim -= distance;
            break;
        default:
            break;
    }
}

Console.WriteLine("Part 2: " + (depth * horizontal));