using System;
using LibraryApp;

class Program
{
    static void Main(string[] args)
    {
        LibraryManager manager = new LibraryManager();

        // Додаємо тестову книгу
        manager.AddBook(new Book("Kobzar", "T. Shevchenko", 1840));

        Console.WriteLine("--- Список книг у бібліотеці ---");
        foreach (var book in manager.GetAllBooks())
        {
            Console.WriteLine($"{book.Author} - {book.Title} ({book.Year})");
        }

        // Тестуємо збереження (вимога розділу 1.2)
        manager.SaveToFile("library.txt");
        Console.WriteLine("\nДані успішно збережені у файл library.txt");
    }
}