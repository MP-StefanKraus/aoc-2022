using System.Text.RegularExpressions;

class Program
{

    static void Main(String[] args)
    {
        List<Monkey> monkeys = new List<Monkey>();
        String line;
        Monkey curMonkey = new Monkey(monkeys);
        while ((line = Console.ReadLine()) != null)
        {
            if (line.Contains("Monkey"))
            {
                curMonkey = new Monkey(monkeys);
                monkeys.Add(curMonkey);
            }
            else if (line.Contains("Starting items:"))
            {
                var matches = Regex.Matches(line, @"\d+");
                foreach (Match m in matches)
                {
                    UInt64 item = Convert.ToUInt64(m.Value);
                    curMonkey.GiveItem(item);
                    //Console.WriteLine($"gave item {item}");
                }
            }
            else if (line.Contains("Operation"))
            {
                var match = Regex.Match(line, "new = (.+)");
                curMonkey.evalString = match.Groups[1].Value;
                //Console.WriteLine($"evalString was {curMonkey.evalString}");
            }
            else if (line.Contains("Test:"))
            {
                var match = Regex.Match(line, @"by (\d+)");
                curMonkey.divideBy = Convert.ToUInt64(match.Groups[1].Value);
                //Console.WriteLine($"testnum was {curMonkey.divideBy}");
            }
            else if (line.Contains("throw"))
            {
                var match = Regex.Match(line, @"monkey (\d)");
                int num = Convert.ToInt32(match.Groups[1].Value);
                if (line.Contains("true"))
                {
                    curMonkey.trueMonkey = num;
                }
                else
                {
                    curMonkey.falseMonkey = num;
                }
            }
        }
        UInt64 commonModulo = monkeys.ConvertAll(a => a.divideBy).Aggregate((a, b) => a * b);
        Console.WriteLine(commonModulo);
        foreach (Monkey m in monkeys) m.modulo = commonModulo;
        for (int i = 0; i < 10000; i++)
        {
            foreach (Monkey m in monkeys)
            {
                m.SimulateRound();
            }
        }
        var biggest = monkeys.ConvertAll((a => a.inspectedItems)).OrderBy(a => a).TakeLast(2);
        Console.WriteLine(biggest.Aggregate((a, b) => a * b));
    }
}
