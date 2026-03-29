using cinema.Models;
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
    public partial class TicketPage : Page
    {
        private Session session;
        private Seat seat;
        private DatabaseService dbService = new DatabaseService();

        public TicketPage(Session session, Seat seat)
        {
            InitializeComponent();
            this.session = session;
            this.seat = seat;

            MovieText.Text = $"Фильм: {GetMovieTitle(session.Id)}";
            HallText.Text = $"Зал: {session.HallName} ({session.HallQuality})";
            DateTimeText.Text = $"Дата и время: {session.DateTime:dd.MM.yyyy HH:mm}";
            SeatText.Text = $"Место: ряд {seat.RowNumber}, место {seat.SeatNumber}";
            PriceText.Text = $"Цена: {session.Price} руб";
        }

        private string GetMovieTitle(int sessionId)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(dbService.ConnectionString))
            {
                connection.Open();
                string query = @"SELECT m.Title FROM Sessions s
                                 JOIN Movies m ON s.MovieId = m.Id
                                 WHERE s.Id = @id";
                using (var cmd = new System.Data.SqlClient.SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", sessionId);
                    return cmd.ExecuteScalar()?.ToString() ?? "Неизвестно";
                }
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (SessionManager.CurrentUser == null)
            {
                MessageBox.Show("Необходимо войти в систему");
                return;
            }

            bool success = dbService.BuyTicket(session.Id, SessionManager.CurrentUser.Id, seat.Id, session.Price);
            if (success)
            {
                MessageBox.Show("Билет успешно куплен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new MoviesPage());
            }
            else
            {
                MessageBox.Show("Ошибка при покупке билета", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
