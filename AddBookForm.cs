using System;
using System.Drawing;
using System.Windows.Forms;

namespace LibraryApp
{
    public class AddBookForm : Form
    {
        public Book NewBook { get; private set; } = null!;
        private TextBox txtTitle = null!, txtAuthor = null!, txtYear = null!, txtPublisher = null!, txtSection = null!, txtOrigin = null!;
        private CheckBox chkAvailable = null!;
        private NumericUpDown numRating = null!;

        public AddBookForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Додати нову книгу";
            this.Size = new Size(400, 520);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            int y = 20;
            txtTitle = CreateField("Назва:", ref y);
            txtAuthor = CreateField("Автор:", ref y);
            txtYear = CreateField("Рік видання:", ref y);
            txtPublisher = CreateField("Видавництво:", ref y);
            txtSection = CreateField("Розділ:", ref y);
            txtOrigin = CreateField("Походження:", ref y);

            Label lblAvail = new Label { Text = "В наявності:", Location = new Point(20, y), AutoSize = true };
            chkAvailable = new CheckBox { Checked = true, Location = new Point(150, y) };
            y += 30;

            Label lblRate = new Label { Text = "Оцінка (1-5):", Location = new Point(20, y), AutoSize = true };
            numRating = new NumericUpDown { Minimum = 1, Maximum = 5, Value = 5, Location = new Point(150, y), Width = 60 };
            y += 50;

            Button btnSave = new Button { Text = "Зберегти", Location = new Point(130, y), Width = 120, Height = 40 };
            btnSave.Click += (s, e) => Save();

            this.Controls.AddRange(new Control[] { lblAvail, chkAvailable, lblRate, numRating, btnSave });
        }

        private TextBox CreateField(string label, ref int y)
        {
            Label lbl = new Label { Text = label, Location = new Point(20, y), AutoSize = true };
            TextBox txt = new TextBox { Location = new Point(150, y), Width = 200 };
            this.Controls.Add(lbl); this.Controls.Add(txt);
            y += 40;
            return txt;
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || !int.TryParse(txtYear.Text, out int year))
            {
                MessageBox.Show("Помилка валідації! Перевірте назву та рік.", "Увага");
                return;
            }

            NewBook = new Book(txtTitle.Text, txtAuthor.Text, year, txtPublisher.Text, 
                               txtSection.Text, txtOrigin.Text, chkAvailable.Checked, (int)numRating.Value);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}