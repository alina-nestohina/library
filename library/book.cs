namespace LibraryApp
{
    /// <summary>
    /// Модель книги.
    /// </summary>
    public class Book
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Genre { get; set; } = string.Empty;

        // Конструктор без параметрів (потрібен для ініціалізації)
        public Book() { }

        // Конструктор з 3 параметрами (який вимагає LibraryManager)
        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }
    }
}