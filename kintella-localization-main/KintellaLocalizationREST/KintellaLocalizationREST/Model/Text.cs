using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintellaLocalizationREST.Model
{
    public class Text
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TextID { get; set; }
        [Required]
        [ForeignKey("SubModuleID")]
        public int SubModuleID { get; set; }
        [Required]
        [ForeignKey("LanguageID")]
        public int LanguageID { get; set; }
        public string? TextContent { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [Required]
        public bool IsProduction { get; set; }
        public bool ShouldDisplay { get; set; } = true; // should prob talk about this fix

        public Text()
        {
            
        }

        public Text(int textID, int subModuleID, string textContent, int languageID, DateTime dateCreated, DateTime dateModified, bool shouldDisplay = true, bool isProduction = false)
        {
            TextID = textID;
            SubModuleID = subModuleID;
            TextContent = textContent;
            LanguageID = languageID;
            DateCreated = dateCreated;
            DateModified = dateModified;
            ShouldDisplay = shouldDisplay;
            IsProduction = isProduction;
        }
    }
}
