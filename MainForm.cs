using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace LibraryApp
{
    public class MainForm : Form
    {
        private LibraryManager _manager = new();
        private DataGridView _dataGridView = null!;
        private Button _btnAdd = null!;
        private Button _btnDelete = null!;
        private TextBox _txtSearch = null!;
        private Label _lblStatus = null!;

        public MainForm()
        {
            _manager.LoadFromFile("library.txt");
            InitializeComponent();
            RefreshGrid();
        }

        private void InitializeComponent()
        {
            this.Text = "Особиста бібліотека";
            this.Size = new Size(950, 550); // Трохи розширив для 8 колонок
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblSearch = new Label { Text = "Пошук:", Location = new Point(20, 10), AutoSize = true };
            _txtSearch = new TextBox { Location = new Point(20, 30), Width = 250 };
            _txtSearch.TextChanged += (s, e) => Search();

            _dataGridView = new DataGridView
            {
                Location = new Point(20, 70),
                Size = new Size(890, 320),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White
            };

            _btnAdd = new Button { Text = "➕ Додати книгу", Location = new Point(20, 410), Width = 150, Height = 40 };
            _btnAdd.Click += (s, e) => AddBook();

            _btnDelete = new Button { Text = "🗑 Видалити", Location = new Point(180, 410), Width = 150, Height = 40 };
            _btnDelete.Click += (s, e) => DeleteBook();

            _lblStatus = new Label { Location = new Point(20, 470), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };

            this.Controls.AddRange(new Control[] { lblSearch, _txtSearch, _dataGridView, _btnAdd, _btnDelete, _lblStatus });
        }

        private void RefreshGrid()
        {
            _dataGridView.DataSource = null;
            _dataGridView.DataSource = _manager.GetAllBooks();
            _lblStatus.Text = $"Книг у базі: {_manager.GetTotalCount()}";
        }

        private void Search()
        {
            _dataGridView.DataSource = _manager.SearchBooks(_txtSearch.Text);
        }

        private void AddBook()
        {
            // СТВОРЮЄМО ТЕСТОВУ КНИГУ З 8 ПОЛЯМИ, ЩОБ НЕ БУЛО ПОМИЛКИ
            // (Заміни цей блок, коли створиш AddBookForm.cs)
            var testBook = new Book(
                "Нова книга", 
                "Автор", 
                2026, 
                "Видавництво", 
                "Розділ", 
                "Куплена", 
                true, 
                5
            );
            
            _manager.AddBook(testBook);
            _manager.SaveToFile("library.txt");
            RefreshGrid();
        }

        private void DeleteBook()
        {
            if (_dataGridView.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Видалити цей запис?", "Підтвердження", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int index = _dataGridView.SelectedRows[0].Index;
                    _manager.RemoveBookAt(index);
                    _manager.SaveToFile("library.txt");
                    RefreshGrid();
                }
            }
        }
    }
}