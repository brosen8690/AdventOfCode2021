string[] inputStrings = File.ReadAllText("../../../input.txt").Split();

int count = 0;
int previousDepth = int.MaxValue;
foreach (var depthString in inputStrings.Where(x => !string.IsNullOrEmpty(x)))
{
    int intDepth = int.Parse(depthString);

    if (intDepth > previousDepth)
    {
        count++;
    }

    previousDepth = intDepth;
}

Console.WriteLine(count);
