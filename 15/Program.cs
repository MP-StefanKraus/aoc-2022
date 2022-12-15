using System.Text.RegularExpressions;

class Program
{

    static void Main(String[] args)
    {
        String? line;

        int offset = 2 * 1000 * 1000;

        List<Tuple<int, int>>[] slices = new List<Tuple<int, int>>[4 * offset];
        for (int i = 0; i < 4 * offset; i++)
        {
            slices[i] = new List<Tuple<int, int>>();
        }

        while ((line = Console.ReadLine()) != null)
        {

            Console.WriteLine(line);
            var match = Regex.Match(line, @"Sensor at x=(.+), y=(.+): closest beacon is at x=(.+), y=(.+)");
            int ys = Int32.Parse(match.Groups[1].Value);
            int xs = Int32.Parse(match.Groups[2].Value);
            int ye = Int32.Parse(match.Groups[3].Value);
            int xe = Int32.Parse(match.Groups[4].Value);

            int d = Math.Abs(xe - xs) + Math.Abs(ye - ys);

            for (int i = -d; i <= d; i++)
            {
                int dx = i;
                int dy = d - Math.Abs(i);
                int x = xs + dx;
                int l1 = ys - dy;
                int l2 = ys + dy;
                slices[x + offset].Add(new Tuple<int, int>(l1, l2));
            }

        }

        List<List<Tuple<int, int>>> sweeps = new List<List<Tuple<int, int>>>();

        foreach (var slice in slices)
        {
            List<Tuple<int, int>> s = new List<Tuple<int, int>>();
            List<Tuple<int, int>> sweep = new List<Tuple<int, int>>();
            foreach (var x in slice)
            {
                s.Add(new Tuple<int, int>(x.Item1, 1));
                s.Add(new Tuple<int, int>(x.Item2, -1));
            }
            s.Sort((x, y) => Math.Sign(x.Item1 - y.Item1) * 2 + Math.Sign(y.Item2 - x.Item2));
            (int c, int f) = (0, 0);
            foreach (var x in s)
            {
                if (c == 0 && x.Item2 == 1)
                {
                    f = x.Item1;
                }
                c += x.Item2;
                if (c == 0 && x.Item2 == -1)
                {
                    sweep.Add(new Tuple<int, int>(f, x.Item1));
                }
            }
            sweeps.Add(sweep);
        }

        for (int i = offset; i < 3 * offset + 1; i++)
        {
            {
                for (int j = 0; j < sweeps[i].Count - 1; j++)
                {

                    int o = sweeps[i][j + 1].Item1;
                    int u = sweeps[i][j].Item2;
                    if (u + 2 == o)
                    {
                        Int64 x = i - offset;
                        Int64 y = o - 1;
                        Console.WriteLine($"{x}, {y}");
                        Console.WriteLine($"{y * 4000000 + x}");
                    }
                }
            }
        }
    }
}

