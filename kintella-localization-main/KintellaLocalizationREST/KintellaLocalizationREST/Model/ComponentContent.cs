using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintellaLocalizationREST.Model
{
    public class ComponentContent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComponentContentID { get; set; }
        [Required]
        [ForeignKey("LanguageID")]
        public int LanguageID { get; set; }
        [Required]
        public string? CategoryName { get; set; }
        [Required]
        public string? ComponentClassName { get; set; }
        [Required]
        public string? Content { get; set; }

        public ComponentContent()
        {
            
        }

        public ComponentContent(int componentContentID, int languageID, string category, string componentClassName, string content)
        {
            ComponentContentID = componentContentID;
            LanguageID = languageID;
            CategoryName = category;
            ComponentClassName = componentClassName;
            Content = content;
        }
    }
}
