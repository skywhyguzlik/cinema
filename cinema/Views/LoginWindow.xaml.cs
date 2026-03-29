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
    public partial class LoginWindow : Window
    {
        private DatabaseService dbService = new DatabaseService();

        public LoginWindow()
        {
            InitializeComponent();
        }

        public bool Auth(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            try
            {
                var user = dbService.GetUsers().FirstOrDefault(u => u.Email == email && u.Password == password);
                if (user != null)
                {
                    SessionManager.CurrentUser = user;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (Auth(email, password))
            {
                DialogResult = true;
                Close();
            }
            else
            {
                ErrorText.Text = "Неверный email или пароль";
            }
        }
    }
}