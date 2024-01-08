using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Model;
using Microsoft.EntityFrameworkCore;

namespace KintellaLocalizationREST.Managers
{
    public class LanguageDataManager
    {
        private readonly ApplicationDbContext _context;

        public LanguageDataManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Language> GetAllLanguages()
        {
            try
            {
                List<Language> languagesInDb = _context.Languages.ToList();
                return languagesInDb;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"An error occurred: {ex.Message}");
                // You can also throw a custom exception or return a specific result
                // depending on your application's requirements.
                return new List<Language>(); // or throw ex; 
            }
        }
        public bool AddNewLanguage(Language lan)
        {
            bool success = true;
            try
            {
                string[] parameters = { lan.LanguageName, lan.LanguageLocale };
                string sql = "INSERT INTO \"Languages\" (\"Name\", \"LanguageLocale\") VALUES (@p0, @p1)";
                _context.Database.ExecuteSqlRaw(sql, parameters); // sqlraw becourse regular EF didn't work in the start
            }
            catch (DbUpdateException e)
            {
                success = false;
                // make some kind of loggin the error msg
                return success;
            }   
            return success;
        }

        public bool DeleteLanguageAsync(int lanID)
        {
            bool success = true;
            try
            {
                Language languageToDelete = _context.Languages.Find(lanID);
                _context.Languages.Remove(languageToDelete);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                success = false;

                return success;
            }
            return success;
        }

    }
}
