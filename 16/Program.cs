
using System.Text.RegularExpressions;
class Program
{

    static Tuple<int, List<int>>[] connections = new Tuple<int, List<int>>[60];
    static Dictionary<State, int> dp = new Dictionary<State, int>();

    public class State
    {
        public int restTime { get; }
        public Tuple<int, int> places { get; }
        public Int64 opened { get; }

        public State(int restTime, Tuple<int, int> cur, Int64 opened)
        {
            this.restTime = restTime;
            this.places = cur;
            this.opened = opened;
        }

        public override bool Equals(object? obj)
        {
            var res = obj is State state &&
                   restTime == state.restTime &&
                   opened == state.opened &&
                   (
                       (places.Item1 == state.places.Item1 && places.Item2 == state.places.Item2) ||
                       (places.Item1 == state.places.Item2 && places.Item2 == state.places.Item1)
                    );
            return res;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(restTime, opened,
                    HashCode.Combine(places.Item1, places.Item2) ^ HashCode.Combine(places.Item2, places.Item1)
            );
        }

        public override string? ToString()
        {
            return $"{restTime}, {places.Item1}+{places.Item2} : " + opened;
        }
    }

    static int search(
        int restTime,
        Tuple<int, int> cur,
        Int64 opened
    )
    {
        var state = new State(restTime, cur, opened);
        if (restTime == 0)
        {
            return 0;
        }
        if (!dp.ContainsKey(state))
        {
            int d1 = cur.Item1;
            int d2 = cur.Item2;

            int curRes = 0;
            int flow1 = connections[d1].Item1;
            int flow2 = connections[d2].Item1;
            // d1 can open and does
            if ((opened & (1L << d1)) == 0 && flow1 > 0)
            {
                opened |= 1L << d1;
                // both open theirs
                if ((opened & (1L << d2)) == 0 && flow2 > 0)
                {
                    opened |= 1L << d2;

                    curRes = Math.Max(curRes, restTime * (flow1 + flow2) + search(restTime - 1, cur, opened));
                    opened &= ~(1L << d2);
                }

                // only d1 opens, d2 moves
                foreach (int neigh in connections[d2].Item2)
                {
                    curRes = Math.Max(curRes, restTime * flow1 + search(restTime - 1, new Tuple<int, int>(d1, neigh), opened));
                }
                opened &= ~(1L << d1);
            }

            // d1 moves
            foreach (int neigh1 in connections[d1].Item2)
            {
                // d2 opens
                if ((opened & (1L << d2)) == 0 && flow2 > 0)
                {
                    opened |= 1L << d2;

                    curRes = Math.Max(curRes, restTime * flow2 + search(restTime - 1, new Tuple<int, int>(neigh1, d2), opened));

                    opened &= ~(1L << d2);
                }

                // d2 moves also!
                foreach (int neigh2 in connections[d2].Item2)
                {
                    curRes = Math.Max(curRes, search(restTime - 1, new Tuple<int, int>(neigh1, neigh2), opened));
                }
            }
            dp[state] = curRes;
        }
        else
        {
            //Console.WriteLine("dp!");
        }
        return dp[state];
    }

    static void Main(String[] args)
    {

        Dictionary<String, int> maps = new Dictionary<string, int>();

        String? line;
        while ((line = Console.ReadLine()) != null)
        {
            var match = Regex.Match(line, @"Valve (.+) has flow rate=(\d+); tunnel.? lead.? to valve.? (.+)");
            String name = match.Groups[1].Value;
            if (!maps.ContainsKey(name))
            {
                maps[name] = maps.Count();
            }
            List<int> neigh = new List<int>();
            int flow = Int32.Parse(match.Groups[2].Value);
            foreach (var m in match.Groups[3].Value.Split(','))
            {
                var v = m.Trim();
                if (!maps.ContainsKey(v))
                {
                    maps[v] = maps.Count();
                }
                neigh.Add(maps[v]);
            }
            connections[maps[name]] = new Tuple<int, List<int>>(flow, neigh);
        }

        Dictionary<State, int> dp = new Dictionary<State, int>();
        int mx = search(25, new Tuple<int, int>(maps["AA"], maps["AA"]), 0L);
        Console.WriteLine(mx);
    }
}
