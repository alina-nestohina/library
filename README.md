## 📊 Діаграма класів (UML Class Diagram)

```mermaid
classDiagram
    class Book {
        +string Title
        +string Author
        +int Year
        +string Publisher
        +string Section
        +string Origin
        +bool IsAvailable
        +int Rating
    }

    class LibraryManager {
        -List _books
        +GetAllBooks() List
        +GetTotalCount() int
        +AddBook(Book book) void
        +UpdateBookAt(int index, Book updatedBook) void
        +RemoveBookAt(int index) void
        +SearchBooks(string query) List
        +SaveToFile(string path) void
        +LoadFromFile(string path) void
    }

    class MainForm {
        -LibraryManager _manager
        -DataGridView _dataGridView
        -TextBox _txtSearch
        -Label _lblStatus
        -InitializeComponent() void
        -RefreshGrid() void
        -Search() void
        -AddBook() void
        -EditBook() void
        -DeleteBook() void
    }

    class AddBookForm {
        +Book NewBook
        -TextBox txtTitle
        -TextBox txtAuthor
        -Button btnSave
        -Button btnCancel
        -InitializeComponent() void
        -CreateField(string labelText, int y) TextBox
        -BtnSave_Click(object sender, EventArgs e) void
    }

    LibraryManager *-- Book
    MainForm --> LibraryManager
    MainForm ..> AddBookForm