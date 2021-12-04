var input = File.ReadAllText("../../../input.txt").Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToList();
var numbers = input.First().Split(',').Select(x => int.Parse(x));
var squares = input
    .TakeLast(input.Count - 1)
    .Select((square, index) => new { square, index })
    .GroupBy(g => g.index / 5, i => i.square.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)))
    .Select(y =>
    {
        return new Square(y.ToList());
    });

int score = -1;
List<int> calledNumbers = new List<int>();
foreach (var number in numbers)
{
    calledNumbers.Add(number);
    foreach (var square in squares)
    {
        score = square.CalcualteWinner(calledNumbers);
        if (score >= 0)
        {
            break;
        }
    }

    if (score >= 0)
    {
        break;
    }
}

Console.WriteLine("Part 1: " + score);

class Square
{
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
    public int CalcualteWinner(List<int> calledNumbers)
    {
        if (IsWinner(calledNumbers))
        {
            return CalculateScore(calledNumbers);
        }

        return -1;
    }

    //Determines whether this square is a winner, based on the called numbers
    private bool IsWinner(List<int> calledNumbers)
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
    private int CalculateScore(List<int> calledNumbers)
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