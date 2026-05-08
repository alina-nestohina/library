using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryApp
{
    public class LibraryManager
    {
        private List<Book> _books = new List<Book>();

        public void AddBook(Book book) => _books.Add(book);

        public List<Book> GetAllBooks() => _books;

        public List<Book> SearchBooks(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return GetAllBooks();

            return _books.Where(b =>
                b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                b.Author.Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        /// <summary>
        /// Редагування існуючої книги (пошук за індексом для надійності).
        /// </summary>
        public void UpdateBook(int index, Book updatedBook)
        {
            if (index >= 0 && index < _books.Count)
            {
                _books[index] = updatedBook;
            }
        }

        public void RemoveBookAt(int index)
        {
            if (index >= 0 && index < _books.Count) _books.RemoveAt(index);
        }

        /// <summary>
        /// Інвентаризація: підрахунок загальної кількості книг.
        /// </summary>
        public int GetTotalCount() => _books.Count;

        // Збереження всіх полів (тепер їх 8)
        public void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var b in _books)
                {
                    writer.WriteLine($"{b.Title};{b.Author};{b.Year};{b.Publisher};{b.Section};{b.Origin};{b.IsAvailable};{b.Rating}");
                }
            }
        }

        // Безпечне завантаження з валідацією TryParse
        public void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return;
            _books.Clear();
            foreach (var line in File.ReadAllLines(filePath))
            {
                var p = line.Split(';');
                if (p.Length == 8)
                {
                    int.TryParse(p[2], out int year);
                    bool.TryParse(p[6], out bool available);
                    int.TryParse(p[7], out int rating);
                    _books.Add(new Book(p[0], p[1], year, p[3], p[4], p[5], available, rating));
                }
            }
        }
    }
}