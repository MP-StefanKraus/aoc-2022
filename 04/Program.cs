
using System.Text.RegularExpressions;

class Program
{

    static void PrevMain(String[] args)
    {
        String? line;
        int count = 0;
        while ((line = Console.ReadLine()) != null)
        {
            var match = Regex.Match(line, @"^(\d+)-(\d+),(\d+)-(\d+)$");
            int s1 = Convert.ToInt32(match.Groups[1].Value);
            int e1 = Convert.ToInt32(match.Groups[2].Value);
            int s2 = Convert.ToInt32(match.Groups[3].Value);
            int e2 = Convert.ToInt32(match.Groups[4].Value);
            Console.WriteLine(line);
            Console.WriteLine(s1);
            Console.WriteLine(e1);
            Console.WriteLine(s2);
            Console.WriteLine(e2);
            if ((s1 <= s2 && e1 >= e2) || (s2 <= s1 && e2 >= e1))
            {
                Console.WriteLine("YES");
                count++;
            }
            else
            {
                Console.WriteLine("No");
            }
        }
        Console.WriteLine(count);
    }

    static void Main(String[] args)
    {
        String? line;
        int count = 0;
        while ((line = Console.ReadLine()) != null)
        {
            var match = Regex.Match(line, @"^(\d+)-(\d+),(\d+)-(\d+)$");

            int s1 = Convert.ToInt32(match.Groups[1].Value);
            int e1 = Convert.ToInt32(match.Groups[2].Value);
            int s2 = Convert.ToInt32(match.Groups[3].Value);
            int e2 = Convert.ToInt32(match.Groups[4].Value);

            int s = Math.Min(s1, s2);
            int e = Math.Max(e1, e2);

            int fullSize = e - s;
            int size1 = e1 - s1;
            int size2 = e2 - s2;

            if (size1 + size2 >= fullSize)
            {
                Console.WriteLine("YES");
                count++;
            }
            else
            {
                Console.WriteLine("No");
            }
        }
        Console.WriteLine(count);
    }
}
