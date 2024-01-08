using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Model;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace KintellaLocalizationREST.Managers
{
    public class TextDataManager
    {
        private readonly ApplicationDbContext _context;

        public TextDataManager(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create
        public bool AddText(Text text) // deprecated
        {
            bool success = true;
            try
            {
                object[] parameters = {text.SubModuleID, text.LanguageID, text.TextContent, text.DateCreated, text.DateModified, text.IsProduction, text.ShouldDisplay};
                string sql = "INSERT INTO \"Texts\" (\"SubModuleID\", \"LanguageID\", \"TextContent\", \"DateCreated\", \"DateModified\", \"IsProduction\", \"ShouldDisplay\") VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6)";
                _context.Database.ExecuteSqlRaw(sql, parameters);
            }
            catch (Exception e)
            {
                success = false;

                return success;
            }
            return success;
        }

        // Read
        public async Task<List<Text>> GetAllTextsAsync()
        {
            try
            {
                List<Text> textsInDb = _context.Texts.Where(display => display.ShouldDisplay == true).ToList();
                return textsInDb;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Text>();
            }
        }

        // Read
        public async Task<Text> GetTextAsync(int textID)
        {
            Text text = new();
            try
            {
                text = await _context.Texts.SingleOrDefaultAsync(x => x.TextID == textID);
                return text;
            }
            catch (Exception ex)
            {

            }
            return text;
        }

        // Update
        public bool UpdateText(Text text) // deprecated
        {
            try
            {
                Text textInfoDb = _context.Texts.Find(text.TextID);
                if (textInfoDb != null)
                {
                    textInfoDb.TextContent = text.TextContent;
                    _context.Texts.Update(textInfoDb);

                    //AddOrUpdateTextChanges(text, textInfoDb); // textchanges
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Delete
        public bool DeleteText(int textID) // deprecated
        {
            bool success = true;
            try
            {
                Text textToDelete = _context.Texts.Find(textID);
                _context.Texts.Remove(textToDelete);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                success = false;

                return success;
            }
            return success;
        }

    }
}
