using cinema.Services;
using cinema.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace SignInTests
{
    [TestClass]
    public class SignInTests
    {
            [TestMethod]
            public void SignInTest()
            {
            var page = new LoginWindow();

            var loginWindow = new LoginWindow();
            Assert.IsTrue(loginWindow.Auth("ilia@gmail.com", "123456"));

            Assert.IsFalse(loginWindow.Auth("user1", "12345"));
            Assert.IsFalse(loginWindow.Auth("", ""));
            Assert.IsFalse(loginWindow.Auth(" ", " "));
        }

        [TestMethod]
            public void SignInSuccessTest()
            {
            var db = new DatabaseService();
            var users = db.GetUsers();
            var loginWindow = new LoginWindow();

            foreach (var user in users)
            {
                bool result = loginWindow.Auth(user.Email, user.Password);
                Assert.IsTrue(result, $"Не удалось войти для {user.Email}");
            }
        }

        [TestMethod]
            public void SignInFailTest()
            {
                var signInWindow = new LoginWindow();
                var db = new DatabaseService();
            var users = db.GetUsers().ToList();

               // Пустые и null поля
                Assert.IsFalse(signInWindow.Auth("", ""));
                Assert.IsFalse(signInWindow.Auth(null, null));
                Assert.IsFalse(signInWindow.Auth("test@test.com", ""));
                Assert.IsFalse(signInWindow.Auth("", "secret"));

                // Несуществующий email
                Assert.IsFalse(signInWindow.Auth("fake@example.org", "password"));

                // Неверный пароль для всех существующих пользователей
                foreach (var user in users)
                {
                    Assert.IsFalse(signInWindow.Auth(user.Email, "incorrect"));
                }

                // Пробелы
                Assert.IsFalse(signInWindow.Auth("   ", "   "));
                Assert.IsFalse(signInWindow.Auth("admin@cinema.ru", "   "));
            }
        }
    }

