using Azure.Identity;
using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Managers;
using KintellaLocalizationREST.Model;
using KintellaLocalizationREST.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KintellaRestTest
{
    [TestClass]
    public class TestService
    {
        UserService _userService;
        LanguageService _languageService;
        TextService _textService;
        [TestInitialize] public void Initialize()
        {
            UserDataManager _userMananger = new(new ApplicationDbContext());
            _userService = new(_userMananger, new PasswordHasher<User>());

            LanguageDataManager _langMananger = new(new ApplicationDbContext());
            _languageService = new(_langMananger);

            TextDataManager _textManager = new(new ApplicationDbContext());
            _textService = new(_textManager);
        }

        [TestMethod]
        public void GetAllTextAsync_ExcpectedSuccessReturnListWithTextObject_TextDataManager()
        {
            int listCount = _textService.GetAllTextsAsync().Result.Count; // .Result, since its async 
            Assert.IsTrue(listCount > 0);
        }
        [TestMethod] 
        public void AddText_ExcpectedSuccessReturnTrue_TextDataManager()
        {
            Text newText = new(99, 9, "This is for testing", 2, DateTime.Now.ToUniversalTime(), DateTime.Now.ToUniversalTime(), false);
            bool success = _textService.AddText(newText);
            Assert.IsTrue(success);
        }
        [TestMethod] 
        public void AddText_ExcpectedFailReturnFalse_TextDataManager()
        {
            Text newText = new(99, 9, "This is for testing", 2, DateTime.Now, DateTime.Now, false); // postgreSQL needs datetime in UTC format.
            bool success = _textService.AddText(newText);
            Assert.IsFalse(success);
        }
        [TestMethod]
        public void AddNewLanguage_ExcpectedSuccessReturnTrue_LanguageDataManager()
        {
            Language newLanguage = new(99, "Lantin", "la-LT"); // we ignore the id, sence the database manage it.
            bool success = _languageService.AddNewLanguage(newLanguage);
            Assert.IsTrue(success);
        }
        [TestMethod]
        public void DeleteLanguage_ExcpectedSuccessReturnTrue_LanguageDataManager()
        {
            bool success = _languageService.DeleteLanguage(3);
            Assert.IsTrue(success);
        }
        [TestMethod]
        public void DeleteLanguage_ExcpectedFailReturnFalse_LanguageDataManager()
        {
            bool success = _languageService.DeleteLanguage(33); // we dont have one with id of 33
            Assert.IsFalse(success);
        }



        [TestMethod]
        public void ValidateUserAsync_ExcpectedSuccessReturnUser_UserService()
        {
            User excpectedUser = _userService.GetAllUsers().FirstOrDefault(); // we only have one user
            User actualUser = _userService.ValidateUserAsync("testuser", "1234").Result; // async. We just want the result sync

            Assert.AreEqual(excpectedUser, actualUser);
        }
        [TestMethod]
        [DataRow("testuser", "123")]
        [DataRow("testuser", "1235")]
        [DataRow("welp", "1234")]
        public void ValidateUserAsync_ExcpectedFailReturnNull_UserService(string username, string password)
        {
            User excpectedUser = _userService.GetAllUsers().FirstOrDefault(); // we only have one user
            User actualUser = _userService.ValidateUserAsync(username, password).Result; // async. We just want the result sync

            Assert.AreNotEqual(excpectedUser, actualUser);
        }
    }
}