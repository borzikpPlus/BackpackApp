namespace BackpackApp;

public class Jacket : Item
{
    public string Size { get; }

    public Jacket(string size)
        : base($"Куртка (розмір {size})", 6m, 1.2m)
    {
        Size = size;
    }
}
