
using System;
using System.Collections;
using System.Text.RegularExpressions;

class Program
{
    static void Main(String[] args)
    {
        String? line;
        Stack<char>[] cargos = new Stack<char>[20];
        for (int i = 0; i < cargos.Length; i++) cargos[i] = new Stack<char>();

        while ((line = Console.ReadLine()).TrimStart() != "")
        {
            for (int i = 1; i < line.Length; i += 4)
            {
                if (line[i] >= 'A')
                {
                    cargos[i / 4 + 1].Push(line[i]);
                }
            }
        }
        foreach (var cargo in cargos)
        {
            Queue<char> inverter = new Queue<char>();
            while (cargo.Count != 0)
            {
                inverter.Enqueue(cargo.Pop());
            }
            foreach (var x in inverter)
            {
                cargo.Push(x);
            }
        }

        while ((line = Console.ReadLine()) != null)
        {
            var instruction = Regex.Match(line!, @"move (\d+) from (\d+) to (\d+)");
            int number = Convert.ToInt32(instruction.Groups[1].Value);
            int from = Convert.ToInt32(instruction.Groups[2].Value);
            int to = Convert.ToInt32(instruction.Groups[3].Value);

            //Queue<char> helper = new Queue<char>();
            Stack<char> helper = new Stack<char>();

            for (int i = 0; i < number; i++)
            {
                char x = cargos[from].Pop();
                helper.Push(x);
            }
            foreach (char c in helper)
            {
                cargos[to].Push(c);
            }
        }
        foreach (var c in cargos)
        {
            if (c.Count != 0) Console.Write(c.Peek());
        }
    }

}
