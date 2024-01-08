using KintellaLocalizationREST.Model;

namespace KintellaLocalizationREST.Interfaces
{
    public interface ITextChangesService
    {
        public Task<List<TextChanges>> GetAllTextChanges();
        Task<TextChanges> GetTextChangeAsync(int textID);
        bool AddTextChanges(TextChanges text);
        bool DeleteTextChanges(int textChangesID);
        bool UpdateTextChanges(Text text);
        bool PublishText(int textChangesID);
        bool CancelTextPublishing(int textChangesID);

    }
}
