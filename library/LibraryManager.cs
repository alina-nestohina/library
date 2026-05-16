using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryApp
{
    /// <summary>
    /// Керує колекцією книг: додавання, видалення, редагування, пошук та збереження.
    /// </summary>
    public class LibraryManager
    {
        private readonly List<Book> _books = new();

        /// <summary>
        /// Повертає повний список усіх книг у бібліотеці.
        /// </summary>
        public List<Book> GetAllBooks() => _books.ToList();

        /// <summary>
        /// Повертає загальну кількість книг у базі даних.
        /// </summary>
        public int GetTotalCount() => _books.Count;

        /// <summary>
        /// Додає нову книгу до колекції.
        /// </summary>
        public void AddBook(Book book) => _books.Add(book);

        /// <summary>
        /// Оновлює дані існуючої книги за її позицією у списку.
        /// </summary>
        public void UpdateBookAt(int index, Book updatedBook)
        {
            if (index >= 0 && index < _books.Count)
            {
                _books[index] = updatedBook;
            }
        }

        /// <summary>
        /// Вилучає книгу з колекції за її позицією.
        /// </summary>
        public void RemoveBookAt(int index)
        {
            if (index >= 0 && index < _books.Count) _books.RemoveAt(index);
        }

        /// <summary>
        /// Виконує пошук книг за частковим збігом у назві або імені автора.
        /// </summary>
        public List<Book> SearchBooks(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return GetAllBooks();
            return _books.Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) || 
                                     b.Author.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Зберігає поточну колекцію книг у текстовий файл.
        /// </summary>
        public void SaveToFile(string path)
        {
            var lines = _books.Select(b => $"{b.Title};{b.Author};{b.Year};{b.Publisher};{b.Section};{b.Origin};{b.IsAvailable};{b.Rating}");
            File.WriteAllLines(path, lines);
        }

        /// <summary>
        /// Завантажує колекцію книг із текстового файлу.
        /// </summary>
        public void LoadFromFile(string path)
        {
            if (!File.Exists(path)) return;
            var lines = File.ReadAllLines(path);
            _books.Clear();
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 8)
                {
                    _books.Add(new Book(parts[0], parts[1], int.Parse(parts[2]), parts[3], parts[4], parts[5], bool.Parse(parts[6]), int.Parse(parts[7])));
                }
            }
        }
    }
}