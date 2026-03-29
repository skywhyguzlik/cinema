using cinema.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cinema.Views
{
    public partial class ProfilePage : Page
    {
        private DatabaseService dbService = new DatabaseService();

        public ProfilePage()
        {
            InitializeComponent();
            LoadUserInfo();
            LoadTickets();
        }

        private void LoadUserInfo()
        {
            if (SessionManager.CurrentUser == null) return;
            var user = SessionManager.CurrentUser;
            UserNameText.Text = $"Имя: {user.FullName}";
            UserEmailText.Text = $"Email: {user.Email}";
            if (user.BirthDate.HasValue)
                UserBirthText.Text = $"Дата рождения: {user.BirthDate.Value:dd.MM.yyyy}";
        }

        private void LoadTickets()
        {
            var tickets = dbService.GetUserTickets(SessionManager.CurrentUser.Id);
            TicketsListView.ItemsSource = tickets.Select(t => new
            {
                t.MovieTitle,
                t.HallName,
                DateTime = t.DateTime.ToString("dd.MM.yyyy HH:mm"),
                SeatDisplay = $"Ряд {t.RowNumber} Место {t.SeatNumber}",
                t.PricePaid,
                t.Status
            }).ToList();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
            else
                NavigationService.Navigate(new MoviesPage()); 
        }
    }
}
