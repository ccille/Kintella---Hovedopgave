using KintellaLocalizationREST.Model;

namespace KintellaLocalizationREST.Interfaces
{
    public interface ITextService
    {
        public Task<Text> GetTextAsync(int textID);
        public Task<List<Text>> GetAllTextsAsync();
        public bool AddText(Text textContent);
        public bool UpdateText(Text textContent);
        public bool DeleteText(int textID);
    }
}
