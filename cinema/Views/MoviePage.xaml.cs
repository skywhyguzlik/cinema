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
    public partial class MoviePage : Page
    {
        private Movie movie;
        private DatabaseService dbService = new DatabaseService();
        private List<Session> sessions;
        private Session selectedSession;

        public MoviePage(Movie movie)
        {
            InitializeComponent();
            this.movie = movie;
            LoadMovieData();
            LoadSessions();
        }

        private void LoadMovieData()
        {
            TitleText.Text = movie.Title;
            RatingText.Text = $"Рейтинг: {movie.Rating:F1}";
            AgeRatingText.Text = $"Возрастное ограничение: {movie.AgeRating}";
            DurationText.Text = $"Длительность: {movie.DurationMinutes} мин";
            ReleaseDateText.Text = $"Дата выхода: {movie.ReleaseDate:dd.MM.yyyy}";
            DescriptionText.Text = movie.Description;

            try
            {
                string imageName = movie.PosterPath ?? "placeholder.jpg";
                string imagePath = System.IO.Path.Combine("Images", imageName);
                if (System.IO.File.Exists(imagePath))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    PosterImage.Source = bitmap;
                }
            }
            catch { }

            string genres = dbService.GetMovieGenres(movie.Id);
            GenreText.Text = $"Жанры: {genres}";
        }

        private void LoadSessions()
        {
            sessions = dbService.GetSessionsByMovie(movie.Id);
            SessionsListBox.ItemsSource = sessions.Select(s => new
            {
                DisplayText = $"{s.DateTime:dd.MM.yyyy HH:mm} - {s.HallName} ({s.HallQuality}) - {s.Price} руб",
                Session = s
            }).ToList();
        }

        private void SessionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SessionsListBox.SelectedItem != null)
            {
                dynamic selected = SessionsListBox.SelectedItem;
                selectedSession = selected.Session;
                SelectSessionButton.IsEnabled = true;
            }
            else
                SelectSessionButton.IsEnabled = false;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
        private void SelectSessionButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSession == null) return;

            if (!SessionManager.IsLoggedIn)
            {
                MessageBox.Show("Для выбора места необходимо войти в аккаунт", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                var loginWindow = new LoginWindow();
                loginWindow.Owner = Window.GetWindow(this);
                if (loginWindow.ShowDialog() == true)
                {
                    NavigationService.Navigate(new SessionPage(selectedSession));
                }
            }
            else
            {
                NavigationService.Navigate(new SessionPage(selectedSession));
            }
        }
    }
}
