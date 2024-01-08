using KintellaLocalizationREST.Managers;
using KintellaLocalizationREST.Model;

namespace KintellaLocalizationREST.Services
{
    public class LanguageService
    {
        private LanguageDataManager _manager;

        public LanguageService(LanguageDataManager manager)
        {
            _manager = manager;
        }

        public List<Language> GetAllLanguages()
        {
            return _manager.GetAllLanguages();
        }
        public bool AddNewLanguage(Language lan) 
        {
            return _manager.AddNewLanguage(lan);
        }
        public bool DeleteLanguage(int lang)
        {
            return _manager.DeleteLanguageAsync(lang);
        }
    }
}
