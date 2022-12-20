class Program
{

    static int Lmod(Int64 v, int e)
    {
        return (int)((v % e) + e) % e;
    }

    static Int64 DECRYPTION_KEY = 811589153;

    static void Main(String[] args)
    {
        String line;

        List<Tuple<Int64, Int32>> playList = new List<Tuple<Int64, int>>();

        int c = 0;
        while ((line = Console.ReadLine()) != null)
        {
            Int64 num = Int64.Parse(line);
            num *= DECRYPTION_KEY;
            playList.Add(new Tuple<Int64, Int32>(num, c));
            c++;
        }

        for (int d = 0; d < 10; d++)
        {
            for (int i = 0; i < playList.Count; i++)
            {
                int idx = playList.FindIndex((x => x.Item2 == i));
                var elem = playList[idx];
                int dir = Math.Sign(elem.Item1);
                for (int j = 0; j < Math.Abs(elem.Item1 % (playList.Count - 1)); j++)
                {
                    int f = Lmod(idx + (dir * j), playList.Count);
                    int t = Lmod(idx + (dir * (j + 1)), playList.Count);
                    var tmp = playList[f];
                    playList[f] = playList[t];
                    playList[t] = tmp;
                }
                /*
                for (int j = 0; j < playList.Count; j++)
                {
                    Console.Write(playList[j].Item1);
                    Console.Write(" ");
                }
                Console.WriteLine();
                */
            }
            //Console.WriteLine();
        }
        int zero_idx = playList.FindIndex((x => x.Item1 == 0));
        Console.WriteLine(0
            + playList[Lmod(zero_idx + 1000, playList.Count)].Item1
            + playList[Lmod(zero_idx + 2000, playList.Count)].Item1
            + playList[Lmod(zero_idx + 3000, playList.Count)].Item1);
    }

}
