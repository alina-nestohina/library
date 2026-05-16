using System;

namespace LibraryApp
{
    /// <summary>
    /// Представляє модель даних для однієї книги в бібліотеці.
    /// </summary>
    public class Book
    {
        /// <summary> Назва літературного твору. </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary> Прізвище та ініціали автора. </summary>
        public string Author { get; set; } = string.Empty;

        /// <summary> Рік видання книги. </summary>
        public int Year { get; set; }

        /// <summary> Назва видавництва. </summary>
        public string Publisher { get; set; } = string.Empty;

        /// <summary> Тематичний розділ або жанр. </summary>
        public string Section { get; set; } = string.Empty;

        /// <summary> Джерело походження (купівля, дарунок тощо). </summary>
        public string Origin { get; set; } = string.Empty;

        /// <summary> Статус наявності книги в домашній бібліотеці. </summary>
        public bool IsAvailable { get; set; }

        /// <summary> Особиста оцінка книги користувачем від 1 до 5. </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Book"/> з усіма параметрами.
        /// </summary>
        public Book(string title, string author, int year, string publisher,
                    string section, string origin, bool isAvailable, int rating)
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