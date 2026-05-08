namespace LibraryApp
{
    /// <summary>
    /// Представляє модель книги з розширеними характеристиками згідно з темою курсової роботи.
    /// </summary>
    public class Book
    {
        // Основні дані
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
        
        // Додаткові дані за методичкою
        public string Publisher { get; set; } = string.Empty; // Видавництво
        public string Section { get; set; } = string.Empty;   // Розділ (хобі, белетристика тощо)
        public string Origin { get; set; } = string.Empty;    // Походження (куплена, подарована)
        public bool IsAvailable { get; set; } = true;         // Наявність (вдома / позичена)
        public int Rating { get; set; } = 5;                  // Оцінка (1-5)

        // Конструктор без параметрів (для ініціалізації)
        public Book() { }

        // Конструктор для швидкого створення об'єкта
        public Book(string title, string author, int year, string publisher, string section, string origin, bool isAvailable, int rating)
        {
            Title = title;
            Author = author;
            Year = year;
            Publisher = publisher;
            Section = section;
            Origin = origin;
            IsAvailable = isAvailable;
            Rating = rating;
        }
    }
}