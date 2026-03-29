using cinema.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cinema.Services
{
    public class DatabaseService
    {

        private readonly string connectionString;
        public DatabaseService()
        {
            var cs = ConfigurationManager.ConnectionStrings["cinema"];
            if (cs == null)
                throw new InvalidOperationException("Строка подключения 'cinema' не найдена в App.config");
            connectionString = cs.ConnectionString;
        }
        public string ConnectionString => connectionString;
        public List<Movie> GetMovies()
        {
            var movies = new List<Movie>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Title, PosterPath, Rating, ReleaseDate, AgeRating, DurationMinutes, Description FROM Movies";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new Movie
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            PosterPath = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Rating = reader.GetDecimal(3),
                            ReleaseDate = reader.GetDateTime(4),
                            AgeRating = reader.GetString(5),
                            DurationMinutes = reader.GetInt32(6),
                            Description = reader.IsDBNull(7) ? null : reader.GetString(7)
                        });
                    }
                }
            }

            return movies;
        }

        public List<Movie> SearchMovies(string searchText)
        {
            var movies = new List<Movie>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT Id, Title, PosterPath, Rating, ReleaseDate, AgeRating, DurationMinutes, Description 
                                FROM Movies 
                                WHERE Title LIKE @search OR Description LIKE @search";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@search", "%" + searchText + "%");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            movies.Add(new Movie
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                PosterPath = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Rating = reader.GetDecimal(3),
                                ReleaseDate = reader.GetDateTime(4),
                                AgeRating = reader.GetString(5),
                                DurationMinutes = reader.GetInt32(6),
                                Description = reader.IsDBNull(7) ? null : reader.GetString(7)
                            });
                        }
                    }
                }
            }

            return movies;
        }
        public List<User> GetUsers()
        {
            var users = new List<User>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Email, Password, FullName, BirthDate, CreatedAt FROM Users";
                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            Password = reader.GetString(2),
                            FullName = reader.GetString(3),
                            BirthDate = reader.IsDBNull(4) ? null : (DateTime?)reader.GetDateTime(4),
                            CreatedAt = reader.GetDateTime(5)
                        });
                    }
                }
            }
            return users;
        }
        public void AddUser(User user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Email, Password, FullName, BirthDate, CreatedAt) VALUES (@Email, @Password, @FullName, @BirthDate, @CreatedAt)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@FullName", user.FullName);
                    command.Parameters.AddWithValue("@BirthDate", (object)user.BirthDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedAt", user.CreatedAt);
                    command.ExecuteNonQuery();
                }
            }
        }
        public string GetMovieGenres(int movieId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT STRING_AGG(g.Name, ', ') 
            FROM MovieGenres mg
            JOIN Genres g ON mg.GenreId = g.Id
            WHERE mg.MovieId = @movieId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@movieId", movieId);
                    var result = command.ExecuteScalar();
                    return result?.ToString() ?? "нет жанров";
                }
            }
        }
        public List<Session> GetSessionsByMovie(int movieId)
        {
            var sessions = new List<Session>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                SELECT s.Id, s.DateTime, s.Price, h.Name, h.RatingQuality, h.Capacity, h.Id
                FROM Sessions s
                JOIN Halls h ON s.HallId = h.Id
                WHERE s.MovieId = @movieId 
                ORDER BY s.DateTime";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@movieId", movieId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sessions.Add(new Session
                            {
                                Id = reader.GetInt32(0),
                                DateTime = reader.GetDateTime(1),
                                Price = reader.GetDecimal(2),
                                HallName = reader.GetString(3),
                                HallQuality = reader.GetString(4),
                                Capacity = reader.GetInt32(5),
                                HallId = reader.GetInt32(6)
                            });
                        }
                    }
                }
            }
            return sessions;
        }
        public List<Seat> GetSeatsByHall(int hallId)
        {
            var seats = new List<Seat>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, RowNumber, SeatNumber FROM Seats WHERE HallId = @hallId ORDER BY RowNumber, SeatNumber";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@hallId", hallId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            seats.Add(new Seat
                            {
                                Id = reader.GetInt32(0),
                                RowNumber = reader.GetInt32(1),
                                SeatNumber = reader.GetInt32(2)
                            });
                        }
                    }
                }
            }
            return seats;
        }
        public List<int> GetTakenSeats(int sessionId)
        {
            var taken = new List<int>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT SeatId FROM Tickets WHERE SessionId = @sessionId AND Status = 'Active'";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sessionId", sessionId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taken.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            return taken;
        }
        public bool BuyTicket(int sessionId, int userId, int seatId, decimal price)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            INSERT INTO Tickets (SessionId, UserId, SeatId, PricePaid, Status)
            VALUES (@sessionId, @userId, @seatId, @price, 'Active')";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sessionId", sessionId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@seatId", seatId);
                    command.Parameters.AddWithValue("@price", price);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        public List<Ticket> GetUserTickets(int userId)
        {
            var tickets = new List<Ticket>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT t.Id, m.Title, h.Name, s.DateTime, st.RowNumber, st.SeatNumber, t.PricePaid, t.Status
            FROM Tickets t
            JOIN Sessions s ON t.SessionId = s.Id
            JOIN Movies m ON s.MovieId = m.Id
            JOIN Halls h ON s.HallId = h.Id
            JOIN Seats st ON t.SeatId = st.Id
            WHERE t.UserId = @userId
            ORDER BY s.DateTime DESC";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tickets.Add(new Ticket
                            {
                                Id = reader.GetInt32(0),
                                MovieTitle = reader.GetString(1),
                                HallName = reader.GetString(2),
                                DateTime = reader.GetDateTime(3),
                                RowNumber = reader.GetInt32(4),
                                SeatNumber = reader.GetInt32(5),
                                PricePaid = reader.GetDecimal(6),
                                Status = reader.GetString(7)
                            });
                        }
                    }
                }
            }
            return tickets;
        }
        public void DeleteUser(int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Users WHERE Id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", userId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}