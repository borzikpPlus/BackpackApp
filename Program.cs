using System.Text;

namespace BackpackApp;

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
