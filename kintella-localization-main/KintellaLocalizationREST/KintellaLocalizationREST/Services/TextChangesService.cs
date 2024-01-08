using KintellaLocalizationREST.Interfaces;
using KintellaLocalizationREST.Managers;
using KintellaLocalizationREST.Model;

namespace KintellaLocalizationREST.Services
{
    public class TextChangesService : ITextChangesService
    {
        private TextChangesDataManager _manager;

        public TextChangesService(TextChangesDataManager manager)
        {
            _manager = manager;
        }

        public Task<List<TextChanges>> GetAllTextChanges()
        {
            return _manager.GetAllTextChangesAsync();
        }
        public Task<TextChanges> GetTextChangeAsync(int textID)
        {
            return _manager.GetTextChangeAsync(textID);
        }
        public bool AddTextChanges(TextChanges textContent)
        {
            return _manager.AddTextChanges(textContent);
        }
        public bool UpdateTextChanges(Text textContent)
        {
            return _manager.UpdateTextChanges(textContent);
        }
        public bool DeleteTextChanges(int textID)
        {
            return _manager.DeleteTextChanges(textID);
        }
        public bool PublishText(int textChangeID)
        {
            return _manager.PublishText(textChangeID);
        }
        public bool CancelTextPublishing(int textChangeID)
        {
            return _manager.CancelTextPublishing(textChangeID);
        }

    }
}
