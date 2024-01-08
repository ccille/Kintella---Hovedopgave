using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KintellaLocalizationREST.Model
{
    public class Language
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LanguageID { get; set; }
        [Required]
        public string LanguageName { get; set; }
        [Required]
        public string LanguageLocale { get; set; }

        public Language()
        {
            
        }
        public Language(int languageID, string languageName, string languageLocale)
        {
            LanguageID = languageID;
            LanguageName = languageName;
            LanguageLocale = languageLocale;
        }
    }
}
