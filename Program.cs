using System.Text;

namespace BackpackApp;

public delegate void ItemAddedHandler(Backpack backpack, Item item);

public class BackpackOverflowException : Exception
{
    public BackpackOverflowException(string message)
        : base(message)
    {
    }
}

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

public class Book : Item
{
    public string Author { get; }

    public Book(string title, string author)
        : base($"Книга «{title}»", 1.5m, 0.6m)
    {
        Author = author;
    }
}

public class Laptop : Item
{
    public string Model { get; }

    public Laptop(string model)
        : base($"Ноутбук {model}", 3.5m, 1.8m)
    {
        Model = model;
    }
}

public class WaterBottle : Item
{
    public decimal Liters { get; }

    public WaterBottle(decimal liters)
        : base($"Пляшка води {liters:0.##} л", liters + 0.1m, liters + 0.15m)
    {
        Liters = liters;
    }
}

public class Jacket : Item
{
    public string Size { get; }

    public Jacket(string size)
        : base($"Куртка (розмір {size})", 6m, 1.2m)
    {
        Size = size;
    }
}

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

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Backpack backpack = new Backpack();
        backpack.SetColor("Чорний");
        backpack.SetBrand("Osprey", "Osprey Packs (США)");
        backpack.SetFabric("Нейлон 420D");
        backpack.SetWeight(0.9m);
        backpack.SetVolume(15m);

        backpack.ItemAdded += delegate (Backpack sender, Item item)
        {
            Console.WriteLine($"Додано: {item}. Вільно ще {sender.FreeVolume:0.##} л");
        };

        backpack.PrintInfo();

        Item[] things =
        {
            new Laptop("Lenovo IdeaPad 5"),
            new Book("Кобзар", "Тарас Шевченко"),
            new WaterBottle(0.5m),
            new Jacket("M"),
            new Jacket("L"),
            new Book("Тигролови", "Іван Багряний"),
        };

        foreach (Item thing in things)
        {
            try
            {
                backpack.AddItem(thing);
            }
            catch (BackpackOverflowException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        Console.WriteLine();
        backpack.PrintInfo();
    }
}
