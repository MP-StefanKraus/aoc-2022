class Program
{
    struct QElem
    {
        public QElem(int x, int y, int d) : this()
        {
            X = x;
            Y = y;
            Dist = d;
        }

        public int X { get; }
        public int Y { get; }
        public int Dist { get; }
    }

    static void Main(String[] args)
    {
        List<String> lab = new List<String>();

        String line;
        while ((line = Console.ReadLine()) != null)
        {
            lab.Add(line);
        }
        (int xS, int yS, int xE, int yE) = (0, 0, 0, 0);

        for (int i = 0; i < lab.Count; i++)
        {
            int a = 0;
            if ((a = lab[i].IndexOf('S')) != -1)
            {
                xS = i; yS = a;
            }
            if ((a = lab[i].IndexOf('E')) != -1)
            {
                xE = i; yE = a;
            }
        }

        Queue<QElem> q = new Queue<QElem>();
        for (int i = 0; i < lab.Count; i++)
        {
            lab[i] = lab[i].Replace('S', 'z').Replace('E', '{');
            for (int j = 0; j < lab[i].Length; j++)
            {
                if (lab[i][j] == 'a')
                {
                    q.Enqueue(new QElem(i, j, 0));
                }
            }
        }


        Boolean[,] visited = new Boolean[lab.Count, lab[0].Length];

        int[,] neighbours = new int[,]{
            {-1, 0}, {1, 0}, {0, -1}, {0, 1}
        };

        while (q.Count > 0)
        {
            QElem elem = q.Dequeue();
            (int X, int Y) = (elem.X, elem.Y);
            Console.WriteLine($"{X}, {Y}");
            Console.WriteLine(lab[X][Y]);
            if (visited[elem.X, elem.Y]) continue;
            if (elem.X == xE && elem.Y == yE)
            {
                Console.WriteLine(elem.Dist);
                return;
            }
            visited[elem.X, elem.Y] = true;

            for (int i = 0; i < neighbours.GetLength(0); i++)
            {
                {
                    (int nX, int nY) = (elem.X + neighbours[i, 0], elem.Y + neighbours[i, 1]);
                    if (Math.Clamp(nX, 0, lab.Count - 1) == nX &&
                        Math.Clamp(nY, 0, lab[i].Length - 1) == nY &&
                        lab[nX][nY] - lab[elem.X][elem.Y] <= 1)
                    {

                        q.Enqueue(new QElem(nX, nY, elem.Dist + 1));
                    }
                }
            }
        }
    }
}
