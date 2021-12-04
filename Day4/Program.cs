var input = File.ReadAllText("../../../input.txt").Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToList();
var numbers = input.First().Split(',').Select(x => int.Parse(x));
var squares = input
    .TakeLast(input.Count - 1)
    .Select((square, index) => new { square, index })
    .GroupBy(g => g.index / 5, i => i.square.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)))
    .Select(y =>
    {
        return new Square(y.ToList());
    }).ToList();

//Part 1
int score = -1;
List<int> calledNumbers = new List<int>();
foreach (var number in numbers)
{
    calledNumbers.Add(number);
    foreach (var square in squares)
    {
        score = square.CalculateWinner(calledNumbers);
        if (score > -1)
        {
            break;
        }
    }

    if (score > -1)
    {
        break;
    }
}

Console.WriteLine("Part 1: " + score);

//Part 2

//Reset HasWon properties from part 1
squares = squares.Select(x =>
{
    x.HasWon = false;
    return x;
}).ToList();

calledNumbers = new List<int>();
List<int> scores = new List<int>();
foreach (var number in numbers)
{
    calledNumbers.Add(number);
    for (int i = 0; i < squares.Count(); i++)
    {
        score = squares.ElementAt(i).CalculateWinner(calledNumbers);
        if (score > -1)
        {
            scores.Add(score);
        }
    }
}

Console.WriteLine("Part 2: " + scores.Last());

class Square
{
    public bool HasWon { get; set; }
    public IEnumerable<IEnumerable<int>> Rows { get; set; }
    public IEnumerable<IEnumerable<int>> Columns
    {
        get
        {
            List<List<int>> columns = new List<List<int>>();
            for (int i = 0; i < 5; i++)
            {
                List<int> column = new List<int>();
                foreach (var row in Rows)
                {
                    column.Add(row.ElementAt(i));
                }
                columns.Add(column);
            }

            return columns;
        }
    }

    public Square(IEnumerable<IEnumerable<int>> rows)
    {
        Rows = rows;
    }

    //Determines whether this square is a winner and if so, returns the score
    public int CalculateWinner(IEnumerable<int> calledNumbers)
    {
        if (IsWinner(calledNumbers) && !HasWon)
        {
            HasWon = true;
            return CalculateScore(calledNumbers);
        }

        return -1;
    }

    //Determines whether this square is a winner, based on the called numbers
    private bool IsWinner(IEnumerable<int> calledNumbers)
    {
        foreach (var row in Rows)
        {
            if (row.Except(calledNumbers).Count() == 0)
            {
                return true;
            }
        }

        foreach (var column in Columns)
        {
            if (column.Except(calledNumbers).Count() == 0)
            {
                return true;
            }
        }

        return false;
    }

    //Calculates the score of this board
    private int CalculateScore(IEnumerable<int> calledNumbers)
    {
        int sum = 0;
        foreach (var row in Rows)
        {
            foreach (var rowNumber in row)
            {
                if (!calledNumbers.Contains(rowNumber))
                {
                    sum += rowNumber;
                }
            }
        }

        return sum * calledNumbers.Last();
    }
}