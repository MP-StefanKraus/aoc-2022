class Program
{

    static void PrevMain(String[] arg)
    {
        List<String> trees = new List<String>();

        String line;
        while ((line = Console.ReadLine()) != null)
        {
            trees.Add(line);
        }

        Boolean[,] visible = new Boolean[trees.Count(), trees[0].Length];

        for (int i = 0; i < trees.Count(); i++)
        {
            for (int j = 0; j < trees[i].Length; j++)
            {
                var cur_tree = trees[i][j];
                int mx1 = -1;
                for (int k = 0; k < j; k++)
                {
                    mx1 = Math.Max(mx1, trees[i][k]);
                }
                int mx2 = -1;
                for (int k = j + 1; k < trees[i].Length; k++)
                {
                    mx2 = Math.Max(mx2, trees[i][k]);
                }
                int mx3 = -1;
                for (int k = 0; k < i; k++)
                {
                    mx3 = Math.Max(mx3, trees[k][j]);
                }
                int mx4 = -1;
                for (int k = i + 1; k < trees.Count(); k++)
                {
                    mx4 = Math.Max(mx4, trees[k][j]);
                }
                if (Math.Min(Math.Min(mx1, mx2), Math.Min(mx3, mx4)) < cur_tree)
                {
                    visible[i, j] = true;
                }

            }
        }

        int res = 0;
        foreach (var r in visible)
        {
            res += r ? 1 : 0;
        }
        Console.WriteLine(res);
    }


    static void Main(String[] arg)
    {
        List<String> trees = new List<String>();

        String line;
        while ((line = Console.ReadLine()) != null)
        {
            trees.Add(line);
        }

        int mx = 0;

        for (int i = 0; i < trees.Count(); i++)
        {
            for (int j = 0; j < trees[i].Length; j++)
            {
                var cur_tree = trees[i][j];
                int k1 = j - 1, k2 = j + 1, k3 = i - 1, k4 = i + 1;
                while (k1 >= 0 && trees[i][k1] < cur_tree) k1--;
                while (k2 < trees[i].Length && trees[i][k2] < cur_tree) k2++;
                while (k3 >= 0 && trees[k3][j] < cur_tree) k3--;
                while (k4 < trees.Count && trees[k4][j] < cur_tree) k4++;
                k1 = Math.Max(k1, 0);
                k2 = Math.Min(k2, trees[i].Length - 1);
                k3 = Math.Max(k3, 0);
                k4 = Math.Min(k4, trees.Count() - 1);

                int res = (j - k1) * (k2 - j) * (i - k3) * (k4 - i);
                //Console.WriteLine($"{i}, {j}");
                mx = Math.Max(mx, res);
            }
        }
        Console.WriteLine(mx);

    }
}
