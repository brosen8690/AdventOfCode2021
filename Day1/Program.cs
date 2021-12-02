int[] depths = File.ReadAllText("../../../input.txt").Split().Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToArray();

//Part 1
int count = 0;
int previousDepth = int.MaxValue;
foreach (var depth in depths)
{
    if (depth > previousDepth)
    {
        count++;
    }

    previousDepth = depth;
}

Console.WriteLine("Part 1: " + count);

//Part 2
count = 0;
int previousCombinedDepth = int.MaxValue;

for (int i = 0; i < depths.Length; i++)
{
    if (i < 2)
    {
        continue;
    }

    var currentCombinedDepth = depths[i] + depths[i - 1] + depths[i - 2];

    if (currentCombinedDepth > previousCombinedDepth)
    {
        count++;
    }

    previousCombinedDepth = currentCombinedDepth;
}

Console.WriteLine("Part 1: " + count);