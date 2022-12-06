// See https://aka.ms/new-console-template for more information

class Program
{

    const int MARKER_LEN = 14;

    static void Main(String[] args)
    {
        String line = Console.ReadLine()!;
        for (int i = 0; i + MARKER_LEN < line.Length; i++)
        {
            HashSet<char> x = new HashSet<char>();
            for (int j = 0; j < MARKER_LEN; j++)
            {
                x.Add(line[i + j]);
            }
            if (x.Count() == MARKER_LEN)
            {
                Console.WriteLine(i + MARKER_LEN);
                return;
            }
        }
    }
}
