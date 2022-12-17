/*
class Program
{

    static List<String>[] rocks = new List<String>[]{
        (new String[]{
            "..####."
        }).ToList<String>(),
        (new String[]{
            "...#...",
            "..###..",
            "...#...",
        }).ToList<String>(),
        (new String[]{
            "..###..",
            "....#..",
            "....#..",
        }).ToList<String>(),
        (new String[]{
            "..#....",
            "..#....",
            "..#....",
            "..#....",
        }).ToList<String>(),
        (new String[]{
            "..##...",
            "..##...",
        }).ToList<String>(),
    };


    static List<String> optionalMove(List<String> cave, List<String> rock, int height, char direction)
    {
        int offset = direction == '<' ? -1 : 1;
        List<String> moved = new List<String>();
        for (int i = 0; i < rock.Count; i++)
        {
            for (int j = 0; j < rock[i].Length; j++)
            {
                if (rock[i][j] != '#') { continue; }

                int after_j = j + offset;
                // check if it would be 'outside of cave'
                if (after_j < 0 || after_j >= rock[i].Length)
                {
                    return rock;
                }

                int height_of_row = height + i;

                // Check against previous rocks (if inside area)
                if (height_of_row < cave.Count)
                {
                    if (cave[height_of_row][after_j] == '#')
                    {
                        return rock;
                    }
                }
            }
            var m = offset == -1 ? (rock[i].Substring(1) + ".") : ("." + rock[i].Substring(0, 6));
            moved.Add(m);
        }
        return moved;
    }

    static void mergeTogether(List<String> cave, List<String> rock, int height)
    {
        for (int i = 0; i < rock.Count; i++)
        {
            int caveHeight = height + i;
            if (caveHeight >= cave.Count)
            {
                cave.Add(".......");
            }
            char[] row = new char[rock[i].Length];
            for (int j = 0; j < rock[i].Length; j++)
            {
                row[j] = (cave[caveHeight][j] == '.' && rock[i][j] == '.') ? '.' : '#';
            }
            cave[caveHeight] = new String(row);
        }
    }

    static bool canMoveDown(List<String> cave, List<String> currentRock, int curHeight)
    {
        for (int i = 0; i < currentRock.Count; i++)
        {
            for (int j = 0; j < currentRock[i].Length; j++)
            {
                if (currentRock[i][j] != '#') { continue; }
                int relPos = (curHeight + i) - 1;
                // If the pos below this rock part is also a rock, we can't go down, stop
                if (relPos < cave.Count && cave[relPos][j] == '#')
                {
                    return false;
                }
            }
        }

        return true;
    }

    static void Main(String[] args)
    {
        String pattern = Console.ReadLine()!;

        List<String> cave = new List<String>();
        cave.Add("#######");

        int patternPos = 0;

        for (int i = 0; i < 2022000; i++)
        {
            int curHeight = cave.Count() + 3;

            var currentRock = rocks[i % 5];

            while (true)
            {

                //Console.WriteLine(curHeight);
                char direction = pattern[patternPos];
                patternPos = (patternPos + 1) % pattern.Length;

                currentRock = optionalMove(cave, currentRock, curHeight, direction);

                if (canMoveDown(cave, currentRock, curHeight))
                {
                    curHeight--;
                }
                else
                {
                    mergeTogether(cave, currentRock, curHeight);
                    break;
                }
            }

            while (cave[cave.Count - 1] == ".......")
            {
                cave.RemoveAt(cave.Count - 1);
            }
        }
        Console.WriteLine(cave.Count - 1);
    }
}
*/
