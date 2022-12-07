
class Program
{

    class Dir
    {
        public Dictionary<String, Dir> dirs = new Dictionary<string, Dir>();
        public Dictionary<String, UInt64> filesize = new Dictionary<string, UInt64>();

        public void parse()
        {
            String line;
            while ((line = Console.ReadLine()) != null)
            {
                String[] parts = line.Split();
                if (parts[0] == "$")
                {
                    String command = parts[1];
                    if (command == "cd")
                    {
                        String name = parts[2];
                        if (name == "..") return;
                        dirs[name] = new Dir();
                        dirs[name].parse();
                    }
                    else
                    {
                        // ignore, next loops contain stuff
                    }
                }
                else
                {
                    if (parts[0] != "dir")
                    {
                        UInt64 sz = Convert.ToUInt64(parts[0]);
                        filesize[parts[1]] = sz;
                    }
                    else
                    {
                        //silently ignore
                    }
                }
            }
        }

    }

    class Traverser
    {

        public Dictionary<Dir, UInt64> sizes = new Dictionary<Dir, ulong>();

        public UInt64 calc(Dir node)
        {
            UInt64 sum = 0;
            foreach (var a in node.filesize)
            {
                sum += a.Value;
            }
            foreach (var a in node.dirs)
            {
                sum += this.calc(a.Value);
            }
            sizes[node] = sum;
            return sum;
        }

    }


    static void Main(String[] args)
    {
        Dir root = new Dir();
        root.parse();

        Traverser t = new Traverser();
        t.calc(root);

        var ordered_sizes = t.sizes.Values.OrderBy(((a) => a));
        foreach (var s in ordered_sizes)
        {
            if (s >= 8381165)
            {
                Console.WriteLine(s);
                return;
            }
        }
        /*
        UInt64 s = 0;
        foreach (UInt64 x in t.sizes.Values)
        {
            s += x <= 100000 ? x : 0;
        }
        Console.WriteLine(s);
        */
    }
}
