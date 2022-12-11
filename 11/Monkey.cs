using Microsoft.CodeAnalysis.CSharp.Scripting;

class Monkey
{

    List<Monkey> allMonkeys;

    public Monkey(List<Monkey> allMonkeys)
    {
        this.allMonkeys = allMonkeys;
    }


    Queue<UInt64> items = new Queue<UInt64>();

    public String evalString { get; set; } = "";

    public UInt64 divideBy { get; set; }
    public UInt64 modulo { get; set; }

    public int trueMonkey { get; set; }
    public int falseMonkey { get; set; }

    public UInt64 inspectedItems = 0;

    public void GiveItem(UInt64 newItem)
    {
        items.Enqueue(newItem);
    }

    public UInt64 Bore(UInt64 val)
    {
        return val / 3;
    }

    public UInt64 DoOperation(UInt64 item)
    {
        String[] components = evalString.Split();
        UInt64 left = components[0] == "old" ? item : Convert.ToUInt64(components[0]);
        UInt64 right = components[2] == "old" ? item : Convert.ToUInt64(components[2]);
        UInt64 now = components[1] == "*" ? left * right : left + right;

        return now % modulo;
    }

    public int GetToWhichMonkey(UInt64 item)
    {
        return item % divideBy == 0 ? trueMonkey : falseMonkey;
    }

    public void throwToMonkey(UInt64 value, int monkey)
    {
        allMonkeys[monkey].GiveItem(value);
    }

    public void SimulateItem(UInt64 item)
    {
        UInt64 newValue = DoOperation(item);
        //newValue = Bore(newValue);
        int newMonkey = GetToWhichMonkey(newValue);
        throwToMonkey(newValue, newMonkey);
    }

    public void SimulateRound()
    {

        while (items.Count > 0)
        {
            UInt64 old = items.Dequeue();
            inspectedItems++;
            //Console.WriteLine("SIMULATE");
            SimulateItem(old);
        }
    }

}
