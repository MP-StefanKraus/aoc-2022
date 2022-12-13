

class Program
{

    static List<String> deconstruct(String s)
    {
        List<int> commas = new List<int>();
        commas.Add(-1);
        int indent = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '[') indent++;
            if (s[i] == ']') indent--;
            if (s[i] == ',' && indent == 0) commas.Add(i);
        }
        commas.Add(s.Length);

        List<String> res = new List<String>();
        for (int i = 0; i < commas.Count - 1; i++)
        {
            int from = commas[i] + 1;
            int to = commas[i + 1] - 1;
            Console.WriteLine(from);
            Console.WriteLine(to);
            res.Add(s.Substring(from, to - from + 1));
        }

        return res;
    }


    static int checkOrder(String a, String b)
    {
        Console.WriteLine($"{a} vs {b}");
        if (a.Length == 0 && b.Length == 0) return 0;
        if (a.Length == 0) return -1;
        if (b.Length == 0) return 1;

        // Both lists or integers
        if (a.StartsWith('[') == b.StartsWith('['))
        {
            // both lists
            if (a.StartsWith('['))
            {
                List<String> As = deconstruct(a.Substring(1, a.Length - 2));
                List<String> Bs = deconstruct(b.Substring(1, b.Length - 2));

                for (int i = 0; i < Math.Min(As.Count, Bs.Count); i++)
                {
                    int res = checkOrder(As[i], Bs[i]);
                    if (res != 0) return res;
                }
                return As.Count - Bs.Count;
            }
            else
            {
                // both numbers
                return Math.Sign((Int32.Parse(a) - Int32.Parse(b)));
            }

        }
        else
        {
            // Normalize to lists, then compare lists :)
            if (!a.StartsWith('[')) a = "[" + a + "]";
            if (!b.StartsWith('[')) b = "[" + b + "]";
            return checkOrder(a, b);
        }
    }

    static void Main1(String[] args)
    {

        List<int> results = new List<int>();

        for (int i = 1; ; i++)
        {
            String a = Console.ReadLine()!;
            String b = Console.ReadLine()!;
            if (Console.ReadLine() == null) break;

            int r = checkOrder(a, b);
            Console.WriteLine(r);
            if (r < 0) results.Add(i);
        }

        Console.WriteLine(results.Sum());
    }
    static void Main(String[] args)
    {

        List<String> packets = new List<String>();

        String a;
        while ((a = Console.ReadLine()) != null)
        {
            if (a.Trim() != "")
            {
                packets.Add(a);
            }
        }
        List<String> diverder = (new String[] { "[[2]]", "[[6]]" }).ToList<String>();
        packets.AddRange(diverder); ;

        packets.Sort((a, b) => checkOrder(a, b));
        List<int> results = new List<int>();

        for (int i = 0; i < packets.Count; i++)
        {
            if (diverder.Contains(packets[i]))
            {
                results.Add(i + 1);
            }
        }
        Console.WriteLine(results[0] * results[1]);
    }

}
