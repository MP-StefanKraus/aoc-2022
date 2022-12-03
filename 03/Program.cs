// See https://aka.ms/new-console-template for more information

class Program
{

    static void PrevMain(String[] args)
    {
        String? line;

        int count = 0;
        while ((line = Console.ReadLine()) != null)
        {
            HashSet<char> firstWordLetters = new HashSet<char>();
            HashSet<char> secondWordLetters = new HashSet<char>();
            for (int i = 0; i < line.Length / 2; i++)
            {
                firstWordLetters.Add(line[i]);
            }
            for (int i = line.Length / 2; i < line.Length; i++)
            {
                secondWordLetters.Add(line[i]);
            }
            var singleLetters = firstWordLetters.Intersect(secondWordLetters);

            foreach (var item in singleLetters)
            {
                if (item <= 'Z')
                {
                    count += (item - 'A' + 27);
                }
                else
                {
                    count += (item - 'a' + 1);
                }

            }
        }
        Console.WriteLine(count);
    }


    static void Main(String[] args)
    {
        String? line1;

        int count = 0;
        while ((line1 = Console.ReadLine()) != null)
        {

            String line2 = Console.ReadLine()!;
            String line3 = Console.ReadLine()!;

            var singleLetters = line1.Intersect(line2).Intersect(line3);

            foreach (var item in singleLetters)
            {
                if (item <= 'Z')
                {
                    count += (item - 'A' + 27);
                }
                else
                {
                    count += (item - 'a' + 1);
                }

            }
        }
        Console.WriteLine(count);
    }
}
