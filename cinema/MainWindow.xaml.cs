using cinema.Models;
using cinema.Services;
using cinema.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace cinema
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MoviesPage());
            UpdateLoginUI();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Owner = this;
            if (loginWindow.ShowDialog() == true)
            {
                UpdateLoginUI();
                if (MainFrame.Content is MoviesPage moviesPage)
                    moviesPage.RefreshData();
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Owner = this;
            if (registerWindow.ShowDialog() == true)
            {
                UpdateLoginUI();
                if (MainFrame.Content is MoviesPage moviesPage)
                    moviesPage.RefreshData();
            }
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProfilePage());
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.Logout();
            UpdateLoginUI();
            MainFrame.Navigate(new MoviesPage());
        }

        public void UpdateLoginUI()
        {
            bool isLoggedIn = SessionManager.IsLoggedIn;
            LoginButton.Visibility = isLoggedIn ? Visibility.Collapsed : Visibility.Visible;
            RegisterButton.Visibility = isLoggedIn ? Visibility.Collapsed : Visibility.Visible;
            ProfileButton.Visibility = isLoggedIn ? Visibility.Visible : Visibility.Collapsed;
            LogoutButton.Visibility = isLoggedIn ? Visibility.Visible : Visibility.Collapsed;
            if (isLoggedIn)
                ProfileButton.Content = SessionManager.CurrentUser.FullName;
        }
    }
}