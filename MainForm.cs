using System;
using System.Drawing;
using System.Windows.Forms;

namespace LibraryApp
{
    /// <summary>
    /// Головне вікно системи особистої бібліотеки користувача.
    /// </summary>
    public class MainForm : Form
    {
        private readonly LibraryManager _manager = new();
        private DataGridView _dataGridView = null!;
        private TextBox _txtSearch = null!;
        private Label _lblStatus = null!;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="MainForm"/>.
        /// </summary>
        public MainForm()
        {
            _manager.LoadFromFile("library.txt");
            InitializeComponent();
            RefreshGrid();
        }

        private void InitializeComponent()
        {
            this.Text = "Особиста бібліотека (Група ПЗПІ-25-1)";
            this.Size = new Size(1000, 520);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblSearch = new Label { Text = "Швидкий пошук за назвою або автором:", Location = new Point(20, 15), AutoSize = true };
            _txtSearch = new TextBox { Location = new Point(20, 35), Width = 300 };
            _txtSearch.TextChanged += (s, e) => Search();

            _dataGridView = new DataGridView
            {
                Location = new Point(20, 70),
                Size = new Size(940, 310),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White
            };

            Button btnAdd = new Button { Text = "➕ Додати книгу", Location = new Point(20, 400), Width = 150, Height = 40 };
            btnAdd.Click += (s, e) => AddBook();

            Button btnEdit = new Button { Text = "✏️ Редагувати", Location = new Point(180, 400), Width = 150, Height = 40 };
            btnEdit.Click += (s, e) => EditBook();

            Button btnDelete = new Button { Text = "🗑 Видалити", Location = new Point(340, 400), Width = 150, Height = 40 };
            btnDelete.Click += (s, e) => DeleteBook();

            _lblStatus = new Label { Location = new Point(20, 455), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };

            this.Controls.AddRange(new Control[] { lblSearch, _txtSearch, _dataGridView, btnAdd, btnEdit, btnDelete, _lblStatus });
        }

        private void RefreshGrid()
        {
            _dataGridView.DataSource = null;
            _dataGridView.DataSource = _manager.GetAllBooks();
            _lblStatus.Text = $"Загальна кількість книг у сховищі: {_manager.GetTotalCount()}";
        }

        private void Search()
        {
            _dataGridView.DataSource = _manager.SearchBooks(_txtSearch.Text);
        }

        private void AddBook()
        {
            using (AddBookForm addForm = new AddBookForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    _manager.AddBook(addForm.NewBook);
                    _manager.SaveToFile("library.txt");
                    RefreshGrid();
                }
            }
        }

        private void EditBook()
        {
            if (_dataGridView.CurrentRow != null)
            {
                Book selectedBook = (Book)_dataGridView.CurrentRow.DataBoundItem;
                int position = _dataGridView.CurrentRow.Index;

                using (AddBookForm editForm = new AddBookForm(selectedBook))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        _manager.UpdateBookAt(position, editForm.NewBook);
                        _manager.SaveToFile("library.txt");
                        RefreshGrid();
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть рядок для редагування.", "Увага");
            }
        }

        private void DeleteBook()
        {
            if (_dataGridView.CurrentRow != null)
            {
                var result = MessageBox.Show("Ви дійсно бажаєте вилучити цей запис із системи?", "Підтвердження вилучення", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int position = _dataGridView.CurrentRow.Index;
                    _manager.RemoveBookAt(position);
                    _manager.SaveToFile("library.txt");
                    RefreshGrid();
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть рядок для вилучення.", "Увага");
            }
        }
    }
}