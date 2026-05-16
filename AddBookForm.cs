using System;
using System.Drawing;
using System.Windows.Forms;

namespace LibraryApp
{
    /// <summary>
    /// Вікно введення та редагування даних про книгу.
    /// </summary>
    public class AddBookForm : Form
    {
        /// <summary> Об'єкт книги, що створюється або редагується. </summary>
        public Book NewBook { get; private set; } = null!;

        private TextBox txtTitle = null!, txtAuthor = null!, txtYear = null!;
        private TextBox txtPublisher = null!, txtSection = null!, txtOrigin = null!;
        private CheckBox chkAvailable = null!;
        private NumericUpDown numRating = null!;
        private Button btnSave = null!, btnCancel = null!;

        /// <summary> Конструктор для створення нової книги. </summary>
        public AddBookForm()
        {
            InitializeComponent();
            this.Text = "Додавання нової книги";
        }

        /// <summary> Конструктор для редагування існуючої книги. </summary>
        public AddBookForm(Book existingBook)
        {
            InitializeComponent();
            this.Text = "Редагування даних книги";
            
            txtTitle.Text = existingBook.Title;
            txtAuthor.Text = existingBook.Author;
            txtYear.Text = existingBook.Year.ToString();
            txtPublisher.Text = existingBook.Publisher;
            txtSection.Text = existingBook.Section;
            txtOrigin.Text = existingBook.Origin;
            chkAvailable.Checked = existingBook.IsAvailable;
            numRating.Value = existingBook.Rating;
        }

        private void InitializeComponent()
        {
            this.Size = new Size(420, 520);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int y = 20;
            txtTitle = CreateField("Назва книги:", ref y);
            txtAuthor = CreateField("Автор твору:", ref y);
            txtYear = CreateField("Рік видання:", ref y);
            txtPublisher = CreateField("Видавництво:", ref y);
            txtSection = CreateField("Тематичний розділ:", ref y);
            txtOrigin = CreateField("Походження книги:", ref y);

            Label lblAvail = new Label { Text = "Статус наявності:", Location = new Point(20, y), Width = 120 };
            chkAvailable = new CheckBox { Text = "В наявності вдома", Location = new Point(150, y), Checked = true, Width = 200 };
            y += 35;

            Label lblRate = new Label { Text = "Особистий рейтинг:", Location = new Point(20, y), Width = 120 };
            numRating = new NumericUpDown { Location = new Point(150, y), Minimum = 1, Maximum = 5, Value = 5, Width = 60 };
            y += 50;

            btnSave = new Button { Text = "Зберегти", Location = new Point(100, y), Width = 100, Height = 35 };
            btnCancel = new Button { Text = "Скасувати", Location = new Point(220, y), Width = 100, Height = 35 };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            // Клавіатурні прив'язки за методичкою
            this.AcceptButton = btnSave;   // Enter
            this.CancelButton = btnCancel; // Esc

            this.Controls.AddRange(new Control[] { 
                lblAvail, chkAvailable, lblRate, numRating, btnSave, btnCancel 
            });
        }

        private TextBox CreateField(string labelText, ref int y)
        {
            Label lbl = new Label { Text = labelText, Location = new Point(20, y), Width = 120 };
            TextBox tb = new TextBox { Location = new Point(150, y), Width = 220 };
            this.Controls.AddRange(new Control[] { lbl, tb });
            y += 35;
            return tb;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("Будь ласка, заповніть обов'язкові поля (Назва та Автор).", "Помилка введення");
                return;
            }

            if (!int.TryParse(txtYear.Text, out int year) || year < 0 || year > DateTime.Now.Year)
            {
                MessageBox.Show("Вкажіть коректний рік видання книги.", "Помилка введення");
                return;
            }

            NewBook = new Book(txtTitle.Text, txtAuthor.Text, year, txtPublisher.Text, txtSection.Text, txtOrigin.Text, chkAvailable.Checked, (int)numRating.Value);
            this.DialogResult = DialogResult.OK;
        }
    }
}