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
    public partial class SessionPage : Page
    {
        private Session session;
        private DatabaseService dbService = new DatabaseService();
        private List<Seat> seats;
        private List<int> takenSeatIds;
        private List<Seat> selectedSeats = new List<Seat>();
        private Dictionary<Button, Seat> seatButtons = new Dictionary<Button, Seat>();
        private bool hideTakenSeats = false;

        public SessionPage(Session session)
        {
            InitializeComponent();
            this.session = session;
            LoadData();
        }

        private void LoadData()
        {
            InfoText.Text = $"Сеанс: {session.DateTime:dd.MM.yyyy HH:mm}";
            HallInfoText.Text = $"Зал: {session.HallName} ({session.HallQuality})";

            seats = dbService.GetSeatsByHall(session.HallId);
            takenSeatIds = dbService.GetTakenSeats(session.Id);

            DisplaySeats();
        }

        private void DisplaySeats()
        {
            SeatsPanel.Children.Clear();
            seatButtons.Clear();

            foreach (var seat in seats)
            {
                var btn = new Button
                {
                    Content = $"{seat.RowNumber}-{seat.SeatNumber}",
                    Width = 40,
                    Height = 40,
                    Margin = new Thickness(2)
                };

                if (takenSeatIds.Contains(seat.Id))
                    btn.Background = Brushes.Red;
                else if (selectedSeats.Contains(seat))
                    btn.Background = Brushes.Yellow;
                else
                    btn.Background = Brushes.LightGreen;

                if (takenSeatIds.Contains(seat.Id) && hideTakenSeats)
                    btn.Visibility = Visibility.Collapsed;
                else
                    btn.Visibility = Visibility.Visible;

                btn.Click += SeatButton_Click;
                SeatsPanel.Children.Add(btn);
                seatButtons[btn] = seat;
            }

            ClearButton.Content = hideTakenSeats ? "Показать занятые места" : "Скрыть занятые места";
            BookButton.IsEnabled = selectedSeats.Count > 0;
        }

        private void SeatButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;
            var seat = seatButtons[btn];

            if (takenSeatIds.Contains(seat.Id))
                return;

            if (selectedSeats.Contains(seat))
            {
                selectedSeats.Remove(seat);
                btn.Background = Brushes.LightGreen;
            }
            else
            {
                selectedSeats.Add(seat);
                btn.Background = Brushes.Yellow;
            }

            BookButton.IsEnabled = selectedSeats.Count > 0;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            hideTakenSeats = !hideTakenSeats;
            DisplaySeats();
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSeats.Count == 0) return;

            var seat = selectedSeats[0];
            NavigationService.Navigate(new TicketPage(session, seat));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}
