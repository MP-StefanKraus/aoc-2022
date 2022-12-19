using System.Numerics;
using System.Text.RegularExpressions;

using System.Collections.Concurrent;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using System.Text.Json;

class Program
{

    class Blueprint
    {

        public Vector3[] robot_costs;

        public Blueprint(Vector3[] robot_costs)
        {
            this.robot_costs = robot_costs;
        }
    }

    class TryState
    {

        public Vector4 resources;

        public Vector4 robots;

        public TryState(Vector4 resources, Vector4 robots)
        {
            this.resources = resources;
            this.robots = robots;
        }

        public TryState() : this(
            new Vector4(0),
            new Vector4(1, 0, 0, 0))
        { }

        public override bool Equals(object? obj)
        {
            return obj is TryState state &&
                   resources.Equals(state.resources) &&
                   robots.Equals(state.robots);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(resources, robots);
        }
    }

    static bool oneNegative(Vector3 v)
    {
        return v.X < 0 || v.Y < 0 || v.Z < 0;
    }

    static Vector4 getRVec(int r)
    {
        switch (r)
        {
            case 0: return Vector4.UnitX;
            case 1: return Vector4.UnitY;
            case 2: return Vector4.UnitZ;
            case 3: return Vector4.UnitW;
            default: return Vector4.Zero;
        }
    }

    static List<Vector4> getRobotCombinations(Vector3 resources, Vector3[] robotCosts)
    {
        List<Vector4> s = new List<Vector4>();
        s.Add(new Vector4(0));
        for (int r = 0; r < 4; r++)
        {
            Vector3 restVec = resources - robotCosts[r];
            if (!oneNegative(restVec))
            {
                s.Add(getRVec(r));
            }
        }
        return s;
    }

    static Boolean isBetter(TryState shouldBetter, TryState againstThat)
    {
        return
            (Vector4.Max(shouldBetter.resources, againstThat.resources) == shouldBetter.resources) &&
            (Vector4.Max(shouldBetter.robots, againstThat.robots) == shouldBetter.resources);

    }

    static HashSet<TryState> singleStep(HashSet<TryState> states, Blueprint print)
    {
        HashSet<TryState> nextStates = new HashSet<TryState>();

        foreach (TryState s in states)
        {
            Vector3 relResources = new Vector3(s.resources.X, s.resources.Y, s.resources.Z);
            var options = getRobotCombinations(
                            relResources,
                            print.robot_costs);

            foreach (Vector4 r in options)
            {
                Vector3 costs = print.robot_costs[0] * r.X
                                + print.robot_costs[1] * r.Y
                                + print.robot_costs[2] * r.Z
                                + print.robot_costs[3] * r.W;

                Vector4 newResources = (s.resources - new Vector4(costs, 0)) + s.robots;

                nextStates.Add(new TryState(
                        newResources
                        , (s.robots + r)
                    ));
            }
        }

        float maxGeo = nextStates.MaxBy(x => x.resources.W)!.resources.W;
        nextStates.RemoveWhere(s => (maxGeo - s.resources.W > 3));

        return nextStates;
        /*
                HashSet<TryState> toRemove = new HashSet<TryState>();

                foreach (TryState s in nextStates)
                {
                    foreach (TryState t in nextStates)
                    {
                        if (s != t && isBetter(s, t))
                        {
                            toRemove.Add(t);
                        }
                    }
                }
                */

        //Console.WriteLine(toRemove.Count);

        //nextStates.RemoveWhere((p => toRemove.Contains(p)));
    }

    static int doSimulation(Blueprint print)
    {
        HashSet<TryState> state = new HashSet<TryState>();

        var dp = new Dictionary<Vector3, HashSet<Vector4>>();

        state.Add(new TryState());
        for (int i = 0; i < 32; i++)
        {
            Console.WriteLine($"simulte step {i}");
            state = singleStep(state, print);
            //Console.WriteLine($"num states: {state.Count}");
            //Console.WriteLine("I have...");
            /*
            foreach (var s in state)
            {
                Console.WriteLine($"I have... {s.resources} + {s.robots}");
            }
            */
        }
        return extractBest(state.ToList<TryState>());
    }

    static int extractBest(List<TryState> states)
    {
        return (int)(states.ConvertAll<float>(x => x.resources.W).Aggregate((x, y) => Math.Max(x, y)));
    }

    static void Main(String[] args)
    {
        String line;

        List<Blueprint> prints = new List<Blueprint>();

        while ((line = Console.ReadLine()!) != null)
        {
            var match = Regex.Match(line, @"Blueprint (\d+): Each ore robot costs (\d+) ore. Each clay robot costs (\d+) ore. Each obsidian robot costs (\d+) ore and (\d+) clay. Each geode robot costs (\d+) ore and (\d+) obsidian.");

            prints.Add(new Blueprint(
                new Vector3[]{
                    new Vector3(Int32.Parse(match.Groups[2].Value), 0, 0),
                    new Vector3(Int32.Parse(match.Groups[3].Value), 0, 0),
                    new Vector3(Int32.Parse(match.Groups[4].Value), Int32.Parse(match.Groups[5].Value), 0),
                    new Vector3(Int32.Parse(match.Groups[6].Value), 0, Int32.Parse(match.Groups[7].Value))
                }
            ));

        }

        int[] s = new int[prints.Count];
        Parallel.For(0, prints.Count, index =>
        {
            s[index] = doSimulation(prints[index]);
            Console.WriteLine($"Finished {index}");
        }
        );

        int mul = 1;

        for (int i = 0; i < prints.Count; i++)
        {
            Console.WriteLine(i);
            int x = s[i];
            Console.WriteLine(x);
            mul *= x;
        }

        Console.WriteLine(mul);
    }

}
