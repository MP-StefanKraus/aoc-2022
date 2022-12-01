// See https://aka.ms/new-console-template for more information

using System;
class Program1
{
    static void PrefMain(string[] args)
    {
        Int64 maxValue = 0;
        Int64 curValue = 0;
        String? currentLine = "";
        while ((currentLine = Console.ReadLine()) != null)
        {
            if (currentLine == "")
            {
                maxValue = Math.Max(maxValue, curValue);
                curValue = 0;
            }
            else
            {
                curValue += Convert.ToInt64(currentLine);
            }
        }
        Console.WriteLine(maxValue);
    }

    static void Main(string[] args)
    {
        List<long> sumValues = new List<long>();
        long curValue = 0;
        String? currentLine = "";
        while ((currentLine = Console.ReadLine()) != null)
        {
            if (currentLine == "")
            {
                sumValues.Add(curValue);
                curValue = 0;
            }
            else
            {
                curValue += Convert.ToInt64(currentLine);
            }
        }
        sumValues.Sort((a, b) => (int)(b - a));
        var sum = sumValues[0] + sumValues[1] + sumValues[2];
        Console.WriteLine(sum);
    }
}
