using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintellaLocalizationREST.Model
{
    public class SubModule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubModuleID { get; set; }
        [Required]
        public string SubModuleName { get; set;}
        public List<Text>? ListOfSubModuleTexts { get; set; }
        [Required]
        [ForeignKey("ModuleID")]
        public int ModuleID { get; set; }
        [Required]
        [ForeignKey("LanguageID")]
        public int LanguageID { get; set; }

        public SubModule()
        {
            
        }

        public SubModule(int subModuleID, string subModuleName, List<Text> listOfSubModuleTexts)
        {
            SubModuleID = subModuleID;
            SubModuleName = subModuleName;
            ListOfSubModuleTexts = listOfSubModuleTexts;
        }
    }
}
