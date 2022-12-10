class Program
{

    static int count = 0;
    static int X = 1;

    static List<char> res = new List<char>();

    static void drawAndIncrease()
    {
        res.Add(Math.Abs(count % 40 - X) <= 1 ? '#' : '.');
        count++;
    }

    static void Main(String[] args)
    {
        String line;

        while ((line = Console.ReadLine()) != null)
        {
            if (line == "noop")
            {
                drawAndIncrease();
            }
            else
            {
                drawAndIncrease();
                drawAndIncrease();
                int num = Convert.ToInt32(line.Split()[1]);
                X += num;
            }

        }
        for (int i = 0; i < res.Count(); i++)
        {
            if (i % 40 == 0)
            {
                Console.Write('\n');
            }
            Console.Write(res[i]);
        }
    }
}
