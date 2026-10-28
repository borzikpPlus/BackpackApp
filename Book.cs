namespace BackpackApp;

public class Book : Item
{
    public string Author { get; }

    public Book(string title, string author)
        : base($"Книга «{title}»", 1.5m, 0.6m)
    {
        Author = author;
    }
}
