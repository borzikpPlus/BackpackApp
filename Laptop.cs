namespace BackpackApp;

public class Laptop : Item
{
    public string Model { get; }

    public Laptop(string model)
        : base($"Ноутбук {model}", 3.5m, 1.8m)
    {
        Model = model;
    }
}
