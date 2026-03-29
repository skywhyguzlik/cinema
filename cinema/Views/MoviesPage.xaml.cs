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
    public partial class MoviesPage : Page
    {
        private List<Movie> allMovies;
        private DatabaseService dbService = new DatabaseService();
        private string currentSortBy = "title";
        private bool sortAscending = true;

        public MoviesPage()
        {
            InitializeComponent();
            LoadMovies();
        }

        public void RefreshData()
        {
            LoadMovies();
        }

        private void LoadMovies()
        {
            allMovies = dbService.GetMovies();
            DisplayMovies();
        }

        private void DisplayMovies()
        {
            MoviesPanel.Children.Clear();

            List<Movie> currentMovies = GetCurrentMovies();
            List<Movie> sortedMovies;

            if (currentSortBy == "rating")
            {
                sortedMovies = sortAscending
                    ? currentMovies.OrderBy(m => m.Rating).ToList()
                    : currentMovies.OrderByDescending(m => m.Rating).ToList();
            }
            else if (currentSortBy == "date")
            {
                sortedMovies = sortAscending
                    ? currentMovies.OrderBy(m => m.ReleaseDate).ToList()
                    : currentMovies.OrderByDescending(m => m.ReleaseDate).ToList();
            }
            else
            {
                sortedMovies = sortAscending
                    ? currentMovies.OrderBy(m => m.Title).ToList()
                    : currentMovies.OrderByDescending(m => m.Title).ToList();
            }

            foreach (var movie in sortedMovies)
                MoviesPanel.Children.Add(CreateMovieCard(movie));
        }

        private List<Movie> GetCurrentMovies()
        {
            string searchText = SearchTextBox?.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(searchText))
                return allMovies;

            searchText = searchText.ToLower();
            return allMovies.Where(m => m.Title.ToLower().Contains(searchText) ||
                                       (m.Description != null && m.Description.ToLower().Contains(searchText))).ToList();
        }

        private Border CreateMovieCard(Movie movie)
        {
            var border = new Border
            {
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(8),
                Background = Brushes.White,
                Margin = new Thickness(10),
                Width = 220,
                Height = 380
            };

            border.MouseLeftButtonDown += (s, e) => MovieCard_Click(movie);
            border.Cursor = Cursors.Hand;

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(200) });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var imageBorder = new Border
            {
                Background = Brushes.LightGray,
                CornerRadius = new CornerRadius(8, 8, 0, 0),
                Height = 200
            };

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
                    imageBorder.Child = new Image { Source = bitmap, Stretch = Stretch.UniformToFill };
                }
                else
                {
                    imageBorder.Background = GetMovieColor(movie.Id);
                    imageBorder.Child = new TextBlock
                    {
                        Text = movie.Title,
                        Foreground = Brushes.White,
                        FontWeight = FontWeights.Bold,
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        TextWrapping = TextWrapping.Wrap,
                        Padding = new Thickness(10)
                    };
                }
            }
            catch
            {
                imageBorder.Background = Brushes.LightGray;
                imageBorder.Child = new TextBlock
                {
                    Text = "Нет изображения",
                    Foreground = Brushes.Gray,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
            }

            var infoPanel = new StackPanel { Margin = new Thickness(10) };
            infoPanel.Children.Add(new TextBlock
            {
                Text = movie.Title,
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                TextWrapping = TextWrapping.Wrap,
                MaxHeight = 40,
                Margin = new Thickness(0, 0, 0, 5)
            });

            var ratingPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 5) };
            ratingPanel.Children.Add(new TextBlock { Text = "⭐", FontSize = 12 });
            ratingPanel.Children.Add(new TextBlock { Text = $"{movie.Rating:F1}", Margin = new Thickness(5, 0, 0, 0) });
            infoPanel.Children.Add(ratingPanel);

            infoPanel.Children.Add(new TextBlock
            {
                Text = $"Вышел: {movie.ReleaseDate:dd.MM.yyyy}",
                Foreground = Brushes.Gray,
                FontSize = 12,
                Margin = new Thickness(0, 0, 0, 5)
            });

            var ageBorder = new Border
            {
                Background = Brushes.Red,
                CornerRadius = new CornerRadius(3),
                Padding = new Thickness(5, 2, 5, 2),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            ageBorder.Child = new TextBlock
            {
                Text = movie.AgeRating,
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                FontSize = 12
            };
            infoPanel.Children.Add(ageBorder);

            Grid.SetRow(imageBorder, 0);
            Grid.SetRow(infoPanel, 1);
            grid.Children.Add(imageBorder);
            grid.Children.Add(infoPanel);
            border.Child = grid;

            return border;
        }

        private Brush GetMovieColor(int id)
        {
            var colors = new[]
            {
                Brushes.DarkRed,
                Brushes.DarkBlue,
                Brushes.DarkGreen,
                Brushes.DarkOrange,
                Brushes.Purple,
                Brushes.Teal
            };
            return colors[(id - 1) % colors.Length];
        }

        private void MovieCard_Click(Movie movie)
        {
            NavigationService?.Navigate(new MoviePage(movie));
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) => DisplayMovies();
        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortComboBox.SelectedIndex == 0)
                currentSortBy = "title";
            else if (SortComboBox.SelectedIndex == 1)
                currentSortBy = "rating";
            else if (SortComboBox.SelectedIndex == 2)
                currentSortBy = "date";
            DisplayMovies();
        }
        private void SortDirectionButton_Click(object sender, RoutedEventArgs e)
        {
            sortAscending = !sortAscending;
            SortDirectionButton.Content = sortAscending ? "↑" : "↓";
            DisplayMovies();
        }
    }
}
