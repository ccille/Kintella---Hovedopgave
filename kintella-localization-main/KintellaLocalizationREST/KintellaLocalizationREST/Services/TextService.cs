using KintellaLocalizationREST.Interfaces;
using KintellaLocalizationREST.Managers;
using KintellaLocalizationREST.Model;

namespace KintellaLocalizationREST.Services
{
    public class TextService : ITextService
    {
        private TextDataManager _manager;

        public TextService(TextDataManager manager)
        {
            _manager = manager;
        }

        // Create
        public bool AddText(Text textContent)
        {
            return _manager.AddText(textContent);
        }

        // Read
        public Task<List<Text>> GetAllTextsAsync()
        {
            return _manager.GetAllTextsAsync();
        }

        public Task<Text> GetTextAsync(int textID)
        {
            return _manager.GetTextAsync(textID);
        }

        // Update
        public bool UpdateText(Text textContent)
        {
            return _manager.UpdateText(textContent);
        }

        // Delete
        public bool DeleteText(int textID)
        {
            return _manager.DeleteText(textID);
        }
    }
}
