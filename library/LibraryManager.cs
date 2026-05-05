using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryApp
{
    /// <summary>
    /// Клас LibraryManager реалізує логіку керування колекцією книг.
    /// Забезпечує функції пошуку, видалення та збереження даних.
    /// </summary>
    public class LibraryManager
    {
        // Приватна колекція для забезпечення інкапсуляції (вимога ООП)
        private List<Book> _books = new List<Book>();

        /// <summary>
        /// Додає новий об'єкт книги до списку.
        /// </summary>
        public void AddBook(Book book)
        {
            if (book != null)
            {
                _books.Add(book);
            }
        }

        /// <summary>
        /// Повертає повний список книг для відображення в інтерфейсі.
        /// </summary>
        public List<Book> GetAllBooks()
        {
            return _books;
        }

        /// <summary>
        /// Шукає книги за фрагментом назви або автора (незалежно від регістру).
        /// </summary>
        public List<Book> SearchBooks(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return _books;

            string lowerQuery = query.ToLower();
            
            return _books.Where(b => 
                b.Title.ToLower().Contains(lowerQuery) || 
                b.Author.ToLower().Contains(lowerQuery)
            ).ToList();
        }

        /// <summary>
        /// Видаляє книгу зі списку за точною назвою.
        /// </summary>
        public bool RemoveBook(string title)
        {
            var bookToRemove = _books.FirstOrDefault(b => 
                b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            
            if (bookToRemove != null)
            {
                _books.Remove(bookToRemove);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Зберігає дані у текстовий файл для забезпечення цілісності.
        /// </summary>
        public void SaveToFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var book in _books)
                    {
                        // Формат запису: Назва;Автор;Рік
                        writer.WriteLine($"{book.Title};{book.Author};{book.Year}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Обробка помилок для стійкості програми
                Console.WriteLine($"Помилка збереження: {ex.Message}");
            }
        }

        /// <summary>
        /// Завантажує дані з файлу при запуску програми.
        /// </summary>
        public void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return;

            try
            {
                _books.Clear();
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length == 3)
                    {
                        _books.Add(new Book(parts[0], parts[1], int.Parse(parts[2])));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка завантаження: {ex.Message}");
            }
        }
    }
}