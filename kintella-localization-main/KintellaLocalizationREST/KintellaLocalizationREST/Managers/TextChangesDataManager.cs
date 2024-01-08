using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Model;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Threading.Channels;

namespace KintellaLocalizationREST.Managers
{
    public class TextChangesDataManager
    {
        private readonly ApplicationDbContext _context;

        public TextChangesDataManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TextChanges>> GetAllTextChangesAsync()
        {
            try
            {
                List<TextChanges> textChangesInDB = _context.TextChanges.ToList();
                return textChangesInDB;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<TextChanges>();
            }
        }
        public async Task<TextChanges> GetTextChangeAsync(int textChangesID)
        {
            TextChanges text = new();
            try
            {
                text = await _context.TextChanges.SingleOrDefaultAsync(x => x.TextChangeID == textChangesID);
                return text;
            }
            catch (Exception ex)
            {

            }
            return text;
        }
        public bool AddTextChanges(TextChanges text)
        {
            text.TextObject.ShouldDisplay = false;
            bool success = true;
            try
            {
                _context.TextChanges.Add(text);
                _context.SaveChanges();

            }
            catch (DbUpdateException e)
            {
                success = false;

                return success;
            }
            return success;
        }
        public bool UpdateTextChanges(Text text)
        {
            try
            {
                Text textInfo = _context.Texts.Find(text.TextID);
                var textChanges = new TextChanges
                {
                    DateModified = text.DateModified,
                    TextChangeFrom = text.TextContent,
                    TextChangeTo = textInfo.TextContent,
                };

                textChanges.TextObject = textInfo;
                textChanges.TextObject.TextID = textInfo.TextID;

                bool doesExist = _context.TextChanges.Local.Any(t => t.TextObject.TextID == textInfo.TextID);

                if (!doesExist)
                {
                    doesExist = _context.TextChanges.Any(t => t.TextObject.TextID == textInfo.TextID);
                }

                if (doesExist)
                {
                    _context.TextChanges.Update(textChanges);
                }
                else
                {
                    _context.TextChanges.Add(textChanges);
                }

                _context.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteTextChanges(int textChangesID)
        {
            bool success = true;
            try
            {
                TextChanges textToDelete = _context.TextChanges.Find(textChangesID);
                _context.TextChanges.Remove(textToDelete);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                success = false;

                return success;
            }
            return success;
        }
        public bool PublishText(int textChangeID)
        {
            try
            {
                TextChanges newTextChange = _context.TextChanges.Find(textChangeID);
                var textId = _context.Database.SqlQuery<int>($"SELECT \"TextID\" FROM \"TextChanges\" WHERE \"TextChangeID\" = {textChangeID}").ToList();

                newTextChange.TextObject = _context.Texts.Find(textId[0]);
                newTextChange.TextObject.TextContent = newTextChange.TextChangeFrom;
                newTextChange.TextObject.DateModified = newTextChange.DateModified;
                newTextChange.TextObject.ShouldDisplay = true;
                newTextChange.TextObject.IsProduction = true;
                _context.Texts.Update(newTextChange.TextObject);

                _context.TextChanges.Remove(newTextChange); // should we have this?
                _context.SaveChanges();
                
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exception
                // Log or handle the exception appropriately
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
        public bool CancelTextPublishing(int textChangeID) // unsure if we need the uncommented stuff
        {
            try
            {
                TextChanges newTextChange = _context.TextChanges.Find(textChangeID);
                //int textId = _context.TextChanges
                //    .Where(k => k.TextChangeID == newTextChange.TextChangeID)
                //    .Select(k => k.TextObject.TextID)
                //    .FirstOrDefault();
                //Text text = _context.Texts.Find(textId);

                _context.TextChanges.Remove(newTextChange);
                //_context.Texts.Remove(text);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exception
                // Log or handle the exception appropriately
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
