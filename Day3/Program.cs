var rows = File.ReadAllText("../../../input.txt").Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToList();

//Part 1
//Make columns out of the rows
var columns = new List<string>();
for (int i = 0; i < rows[i].Length; i++)
{
    columns.Add("");
    for (int j = 0; j < rows.Count(); j++)
    {
        columns[i] += rows[j][i];
    }
}

//Consolidate the columns into bits
string gammaBits = "";
string epsilonBits = "";
for (int i = 0; i < columns.Count(); i++)
{
    int count = 0;
    for (int j = 0; j < columns[i].Count(); j++)
    {
        if (columns[i][j] == '1')
        {
            count++;
        }
    }

    if (count >= (columns[i].Count() - count))
    {
        gammaBits += '1';
        epsilonBits += '0';
    }
    else
    {
        gammaBits += '0';
        epsilonBits += '1';
    }
}

//Bits to int
int gamma = Convert.ToInt32(gammaBits, 2);
int epsilon = Convert.ToInt32(epsilonBits, 2);

Console.WriteLine("Part 1: " + epsilon * gamma);

//Part 2

//Get the oxygen generator rating
var filteredRows = rows.ToList();
int oxyRating = 0;
for (int i = 0; i < rows.First().Length; i++)
{
    filteredRows = filteredRows.Where(x => x[i] == (filteredRows.Average(x => int.Parse(x[i].ToString())) >= .5 ? '1' : '0')).ToList();
    if (filteredRows.Count == 1)
    {
        oxyRating = Convert.ToInt32(filteredRows.First(), 2);
        break;
    }
}

//Get the co2 scrubber rating
filteredRows = rows.ToList();
int co2Rating = 0;
for (int i = 0; i < rows.First().Length; i++)
{
    filteredRows = filteredRows.Where(x => x[i] == (filteredRows.Average(x => int.Parse(x[i].ToString())) >= .5 ? '0' : '1')).ToList();
    if (filteredRows.Count == 1)
    {
        co2Rating = Convert.ToInt32(filteredRows.First(), 2);
        break;
    }
}

Console.WriteLine("Part 2: " + oxyRating * co2Rating);