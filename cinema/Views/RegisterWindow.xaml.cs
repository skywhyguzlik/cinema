using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using cinema.Models;
using cinema.Services;

namespace cinema.Views
{
    public partial class RegisterWindow : Window
    {
        private DatabaseService dbService = new DatabaseService();

        public RegisterWindow()
        {
            InitializeComponent();
        }

        public bool Register(string email, string password, string fullName, DateTime? birthDate, out string errorMessage)
        {
            errorMessage = null;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fullName))
            {
                errorMessage = "Заполните все обязательные поля";
                return false;
            }

            try
            {
                if (dbService.GetUsers().Any(u => u.Email == email))
                {
                    errorMessage = "Пользователь с таким email уже существует";
                    return false;
                }

                var newUser = new User
                {
                    Email = email,
                    Password = password,
                    FullName = fullName,
                    BirthDate = birthDate,
                    CreatedAt = DateTime.Now
                };

                dbService.AddUser(newUser);
                SessionManager.CurrentUser = newUser;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Ошибка: {ex.Message}";
                return false;
            }
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;
            string fullName = FullNameTextBox.Text.Trim();
            DateTime? birthDate = BirthDatePicker.SelectedDate;

            if (Register(email, password, fullName, birthDate, out string error))
            {
                DialogResult = true;
                Close();
            }
            else
            {
                ErrorText.Text = error;
            }
        }
    }
}
