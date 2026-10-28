namespace BackpackApp;

public class WaterBottle : Item
{
    public decimal Liters { get; }

    public WaterBottle(decimal liters)
        : base($"Пляшка води {liters:0.##} л", liters + 0.1m, liters + 0.15m)
    {
        Liters = liters;
    }
}
