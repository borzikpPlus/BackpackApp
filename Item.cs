namespace BackpackApp;

public abstract class Item
{
    public string Name { get; }
    public decimal Volume { get; }
    public decimal Weight { get; }

    protected Item(string name, decimal volume, decimal weight)
    {
        Name = name;
        Volume = volume;
        Weight = weight;
    }

    public override string ToString()
    {
        return $"{Name}: {Volume:0.##} л, {Weight:0.##} кг";
    }
}
