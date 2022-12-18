using System.Text.RegularExpressions;

class Program
{

    class Point
    {
        public int x;
        public int y;
        public int z;

        public Point(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override bool Equals(object? obj)
        {
            return obj is Point point &&
                   x == point.x &&
                   y == point.y &&
                   z == point.z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }

        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }
    }

    static void Main(String[] args)
    {
        String line;

        List<Point> all_points = new List<Point>();

        while ((line = Console.ReadLine()!) != null)
        {
            var match = Regex.Match(line, @"(\d+),(\d+),(\d+)");
            int x = Int32.Parse(match.Groups[1].Value);
            int y = Int32.Parse(match.Groups[2].Value);
            int z = Int32.Parse(match.Groups[3].Value);

            Point p = new Point(x, y, z);

            all_points.Add(p);
        }

        int[,] reference = new int[,] {
            {-1, 0, 0},
            {1, 0, 0},
            {0, -1, 0},
            {0, 1, 0},
            {0, 0, -1},
            {0, 0, 1}
        };

        Queue<Point> q = new Queue<Point>();
        HashSet<Point> visited = new HashSet<Point>();

        q.Enqueue(new Point(-1, -1, -1));

        int c = 0;

        while (q.Count > 0)
        {
            Point p = q.Dequeue();

            if (visited.Contains(p)) continue;
            visited.Add(p);

            for (int i = 0; i < reference.GetLength(0); i++)
            {

                Point refPoint = new Point(
                    Math.Clamp(p.x + reference[i, 0], -1, 25),
                    Math.Clamp(p.y + reference[i, 1], -1, 25),
                    Math.Clamp(p.z + reference[i, 2], -1, 25)
                );

                // flow continous!
                if (all_points.Contains(refPoint))
                {
                    c++;
                }
                else
                {
                    q.Enqueue(refPoint);
                }
            }

        }

        Console.WriteLine(c);

    }

}
