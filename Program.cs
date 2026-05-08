using System;
using System.Windows.Forms;

namespace LibraryApp
{
    static class Program
    {
        /// <summary>
        /// Головна точка входу для додатка.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Вмикаємо візуальні стилі Windows (щоб кнопки були гарними)
            Application.EnableVisualStyles();
            // Налаштовуємо сумісність тексту
            Application.SetCompatibleTextRenderingDefault(false);
            
            // ЗАПУСК ГОЛОВНОГО ВІКНА
            // (Ми створимо клас MainForm на наступному кроці)
            Application.Run(new MainForm()); 
        }
    }
}