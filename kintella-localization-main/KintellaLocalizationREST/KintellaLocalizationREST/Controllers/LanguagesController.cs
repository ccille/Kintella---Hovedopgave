using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Managers;
using KintellaLocalizationREST.Mockdata;
using KintellaLocalizationREST.Model;
using KintellaLocalizationREST.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KintellaLocalizationREST.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly LanguageDataManager _languageDataManager = new(new ApplicationDbContext());
        private readonly LanguageService _languageService;

        public LanguagesController()
        {
            _languageService = new(_languageDataManager);
        }

        // GET: api/<LanguagesController>
        [HttpGet("GetAllCultureInfo")]
        public List<string> Get()
        {
            // support ISO 639-1, ISO 639-2
            CultureInfo[] rawCulture = CultureInfo.GetCultures(CultureTypes.AllCultures);
            List<string> cultures = rawCulture.Select(x => x.EnglishName).ToList();
            cultures.RemoveAt(0);
            return cultures;
        }

        // GET api/<LanguagesController>/5
        [AllowAnonymous]
        [HttpGet("GetAllFromDB")]
        public List<Language> GetAll()
        {
            List<Language> langs = _languageService.GetAllLanguages();
            return langs;
        }

        // POST api/<LanguagesController>
        [HttpPost("AddLanguage")]
        public bool Post(Language lan)
        {
            return _languageService.AddNewLanguage(lan);
        }

        // DELETE api/<LanguagesController>/5
        [HttpDelete("DeleteLanguage")]
        public bool Delete(int lan)
        {
            return _languageService.DeleteLanguage(lan);
        }
    }
}
