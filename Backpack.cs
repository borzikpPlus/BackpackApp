namespace BackpackApp;

public class Backpack
{
    private readonly List<Item> items = new List<Item>();

    public string Color { get; private set; } = "";
    public string Brand { get; private set; } = "";
    public string Manufacturer { get; private set; } = "";
    public string Fabric { get; private set; } = "";
    public decimal Weight { get; private set; }
    public decimal Volume { get; private set; }

    public IReadOnlyList<Item> Items => items;
    public decimal UsedVolume => items.Sum(item => item.Volume);
    public decimal FreeVolume => Volume - UsedVolume;
    public decimal TotalWeight => Weight + items.Sum(item => item.Weight);

    public event ItemAddedHandler? ItemAdded;

    public void SetColor(string color)
    {
        Color = color;
    }

    public void SetBrand(string brand, string manufacturer)
    {
        Brand = brand;
        Manufacturer = manufacturer;
    }

    public void SetFabric(string fabric)
    {
        Fabric = fabric;
    }

    public void SetWeight(decimal weight)
    {
        if (weight <= 0)
        {
            throw new ArgumentException("Вага рюкзака має бути більшою за 0");
        }
        Weight = weight;
    }

    public void SetVolume(decimal volume)
    {
        if (volume <= 0)
        {
            throw new ArgumentException("Об'єм рюкзака має бути більшим за 0");
        }
        Volume = volume;
    }

    public void AddItem(Item item)
    {
        if (UsedVolume + item.Volume > Volume)
        {
            throw new BackpackOverflowException(
                $"Не вдалося покласти «{item.Name}»: потрібно {item.Volume:0.##} л, а вільно лише {FreeVolume:0.##} л");
        }

        items.Add(item);
        ItemAdded?.Invoke(this, item);
    }

    public void PrintInfo()
    {
        Console.WriteLine("=== Рюкзак ===");
        Console.WriteLine($"Колір: {Color}");
        Console.WriteLine($"Марка: {Brand}, виробник: {Manufacturer}");
        Console.WriteLine($"Тканина: {Fabric}");
        Console.WriteLine($"Вага рюкзака: {Weight:0.##} кг");
        Console.WriteLine($"Об'єм: {Volume:0.##} л (зайнято {UsedVolume:0.##} л, вільно {FreeVolume:0.##} л)");
        Console.WriteLine("Вміст:");
        if (items.Count == 0)
        {
            Console.WriteLine("  порожньо");
        }
        foreach (Item item in items)
        {
            Console.WriteLine($"  - {item}");
        }
        Console.WriteLine($"Загальна вага з вмістом: {TotalWeight:0.##} кг");
        Console.WriteLine();
    }
}
