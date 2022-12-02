
class Program
{
    static void PrevMain(String[] args)
    {
        int result = 0;
        String? line;
        while ((line = Console.ReadLine()) != null)
        {
            String[] guide = line.Split(' ');
            char enemy = guide[0][0];
            char you = guide[1][0];
            int enemy_number = enemy - 'A';
            int your_number = you - 'X';

            int local = ((your_number - enemy_number) + 3) % 3;

            result += (1 + your_number);
            if (local == 1)
            {
                result += 6;
            }
            else if (local == 0)
            {
                result += 3;
            }
            else
            {
                result += 0;
            }
            Console.WriteLine(result);
        }
    }
    static void Main(String[] args)
    {
        int result = 0;
        String? line;
        while ((line = Console.ReadLine()) != null)
        {
            String[] guide = line.Split(' ');
            char enemy = guide[0][0];
            char end = guide[1][0];
            int enemy_number = enemy - 'A';
            int diff_number = end - 'Y';

            int your_number = ((enemy_number + diff_number) + 3) % 3;

            result += (1 + your_number);
            result += (diff_number + 1) * 3;
            Console.WriteLine(result);
        }
    }

}
