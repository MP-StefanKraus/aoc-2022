class Program01
{

    private static int count = 0;
    private static int X = 1;

    private static int result = 0;

    private static int[] relevant = new int[] { 20, 60, 100, 140, 180, 220 };

    private static void increaseCountAndCheck()
    {
        count++;
        if (relevant.Contains(count))
        {
            result += count * X;

        }

    }

    static void PrevMain(String[] args)
    {
        String line;

        while ((line = Console.ReadLine()) != null)
        {
            if (line == "noop")
            {
                increaseCountAndCheck();
            }
            else
            {
                increaseCountAndCheck();
                increaseCountAndCheck();
                int num = Convert.ToInt32(line.Split()[1]);
                X += num;
            }

        }
        Console.WriteLine(result);
    }
}
