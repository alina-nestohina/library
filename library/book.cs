namespace LibraryApp
{
    /// <summary>
    /// Представляє модель книги в особистій бібліотеці.
    /// </summary>
    public class Book
    {
        /// <summary> Назва книги. </summary>
        public string Title { get; set; }

        /// <summary> Автор книги. </summary>
        public string Author { get; set; }

        /// <summary> Рік видання. </summary>
        public int Year { get; set; }

        /// <summary> Жанр або категорія. </summary>
        public string Genre { get; set; }
    }
}