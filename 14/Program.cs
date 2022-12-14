using System.Text.RegularExpressions;

class Program
{

    static void Main(String[] args)
    {
        String? line;

        List<List<Tuple<int, int>>> ways = new List<List<Tuple<int, int>>>();

        while ((line = Console.ReadLine()) != null)
        {
            var matches = Regex.Matches(line, @"(\d+),(\d+)");

            List<Tuple<int, int>> way = matches.ToList<Match>().ConvertAll(
                match => new Tuple<int, int>(Int32.Parse(match.Groups[2].Value), Int32.Parse(match.Groups[1].Value)));
            ways.Add(way);
        }

        char[,] area = new char[2000, 2000];
        for (int i = 0; i < area.GetLength(1); i++)
        {
            for (int j = 0; j < area.GetLength(0); j++)
            {
                area[j, i] = '.';
            }
        }

        var ym = 0;

        foreach (var w in ways)
        {
            for (int i = 0; i < w.Count - 1; i++)
            {
                Tuple<int, int> a = w[i];
                Tuple<int, int> b = w[i + 1];

                (var f, var t) = a.Item1 <= b.Item1 && a.Item2 <= b.Item2 ? (a, b) : (b, a);

                for (int j = f.Item1; j <= t.Item1; j++)
                {
                    area[j, f.Item2] = '#';
                }
                for (int j = f.Item2; j <= t.Item2; j++)
                {
                    area[f.Item1, j] = '#';
                }
                ym = Math.Max(ym, t.Item1);
            }
        }
        ym += 2;

        for (int i = 0; i < area.GetLength(1); i++)
        {
            area[ym, i] = '#';
        }

        int c = 0;
        while (true)
        {
            int x = 0;
            int y = 500;
            while (true)
            {
                /*
                if (x + 1 >= area.GetLength(0))
                {
                    Console.WriteLine(c);
                    return;
                }
                */
                if (area[x + 1, y] == '.')
                {
                    x++;
                    continue;
                }
                if (area[x + 1, y - 1] == '.')
                {
                    x++;
                    y--;
                    continue;
                }
                if (area[x + 1, y + 1] == '.')
                {
                    x++;
                    y++;
                    continue;
                }
                area[x, y] = 'o';
                c++;
                break;
            }

            for (int i = 0; i < 12; i++)
            {
                for (int j = 490; j < 510; j++)
                {
                    Console.Write(area[i, j]);
                }
                Console.Write('\n');
            }
            Console.Write('\n');

            if (x == 0 && y == 500)
            {
                Console.WriteLine(c);
                break;
            }
        }
    }
}
