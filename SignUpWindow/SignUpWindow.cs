using cinema.Services;
using cinema.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace SignUpWindow
{
    [TestClass]
    public class SignUpWindow
    {
        private DatabaseService dbService = new DatabaseService();

        [TestMethod]
        public void RegisterSuccessTest()
        {
            string uniqueEmail = $"friend_{Guid.NewGuid()}@friend.com";
            var signUpWindow = new RegisterWindow();

            bool result = signUpWindow.Register(uniqueEmail, "friendPass123", "Друг Пользователь", DateTime.Now.AddYears(-30), out string error);

            Assert.IsTrue(result, "Регистрация не удалась");
            Assert.IsNull(error, "Ошибка не ожидалась");

            var addedUser = dbService.GetUsers().FirstOrDefault(u => u.Email == uniqueEmail);
            Assert.IsNotNull(addedUser, "Пользователь не найден в БД");
            Assert.AreEqual("Друг Пользователь", addedUser.FullName);
            Assert.AreEqual("friendPass123", addedUser.Password);
                
            dbService.DeleteUser(addedUser.Id);
        }

        [TestMethod]
        public void RegisterFailTest()
        {
            var signUpWindow = new RegisterWindow();
            var db = new DatabaseService();
            var existingUser = db.GetUsers().FirstOrDefault();

            // пустые поля 
            Assert.IsFalse(signUpWindow.Register("", "", "", null, out string errorAll));
            Assert.AreEqual("Заполните все обязательные поля", errorAll);

            Assert.IsFalse(signUpWindow.Register("user@example.com", "", "Name", null, out string errorPass));
            Assert.AreEqual("Заполните все обязательные поля", errorPass);

            Assert.IsFalse(signUpWindow.Register("", "pass123", "Name", null, out string errorEmail));
            Assert.AreEqual("Заполните все обязательные поля", errorEmail);

            // существующий email
            if (existingUser != null)
            {
                Assert.IsFalse(signUpWindow.Register(existingUser.Email, "newpass", "New Name", null, out string errorDuplicate));
                Assert.AreEqual("Пользователь с таким email уже существует", errorDuplicate);
            }

            // null вместо email
            Assert.IsFalse(signUpWindow.Register(null, "pass", "Name", null, out string errorNull));
            Assert.AreEqual("Заполните все обязательные поля", errorNull);
        }
    }
}