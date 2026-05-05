using System.Collections.Generic;

namespace LibraryApp
{
    /// <summary>
    /// Клас для керування колекцією книг.
    /// </summary>
    public class LibraryManager
    {
        // Список книг (наша колекція)
        private List<Book> _books = new List<Book>();

        /// <summary> Додає нову книгу до списку. </summary>
        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        /// <summary> Повертає весь список книг. </summary>
        public List<Book> GetAllBooks()
        {
            return _books;
        }
    }
}