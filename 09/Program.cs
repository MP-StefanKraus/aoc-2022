class Program
{

    public class Point
    {
        public int x { get; set; } = 0;
        public int y { get; set; } = 0;

    }
    public static void updateT(Point H, Point T)
    {
        int dx = H.x - T.x;
        int dy = H.y - T.y;
        if (Math.Abs(dy) > 1 || Math.Abs(dx) > 1)
        {
            T.x += Math.Sign(dx);
            T.y += Math.Sign(dy);
        }
    }

    static void PrevMain(String[] args)
    {

        Point H = new Point();
        Point T = new Point();

        HashSet<Tuple<int, int>> positions = new HashSet<Tuple<int, int>>();

        SortedDictionary<String, Tuple<int, int>> d = new SortedDictionary<string, Tuple<int, int>>();
        d["R"] = Tuple.Create(1, 0);
        d["L"] = Tuple.Create(-1, 0);
        d["U"] = Tuple.Create(0, 1);
        d["D"] = Tuple.Create(0, -1);

        String line;
        while ((line = Console.ReadLine()) != null)
        {
            String[] parts = line.Split();
            int c = Convert.ToInt32(parts[1]);
            for (int i = 0; i < c; i++)
            {
                var direction = d[parts[0]];
                H.x += direction.Item1;
                H.y += direction.Item2;

                updateT(H, T);

                positions.Add(Tuple.Create(T.x, T.y));
                Console.WriteLine($"{T.x} {T.y}");
            }
        }
        Console.WriteLine(positions.Count());
    }
    static void Main(String[] args)
    {

        Point[] points = new Point[10];
        for (int i = 0; i < points.Length; i++) points[i] = new Point();

        HashSet<Tuple<int, int>> positions = new HashSet<Tuple<int, int>>();

        SortedDictionary<String, Tuple<int, int>> d = new SortedDictionary<string, Tuple<int, int>>();
        d["R"] = Tuple.Create(1, 0);
        d["L"] = Tuple.Create(-1, 0);
        d["U"] = Tuple.Create(0, 1);
        d["D"] = Tuple.Create(0, -1);

        String line;
        while ((line = Console.ReadLine()) != null)
        {
            String[] parts = line.Split();
            int c = Convert.ToInt32(parts[1]);
            for (int j = 0; j < c; j++)
            {
                var direction = d[parts[0]];
                points[0].x += direction.Item1;
                points[0].y += direction.Item2;

                for (int i = 0; i < 9; i++)
                {
                    updateT(points[i], points[i + 1]);
                }

                positions.Add(Tuple.Create(points[9].x, points[9].y));
            }
        }
        Console.WriteLine(positions.Count());
    }

}
