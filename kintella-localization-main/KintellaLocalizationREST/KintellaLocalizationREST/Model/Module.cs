using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintellaLocalizationREST.Model
{
    public class Module
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModuleID { get; set; }
        [Required]
        public string ModuleName { get; set; }
        public string? ModuleIndexText { get; set; }
        public List<SubModule> SubModules { get; set; }
        [Required]
        [ForeignKey("LanguageID")]
        public int LanguageID { get; set; }

        public Module()
        {
            
        }

        public Module(int moduleID, string moduleName, List<SubModule> subModules, string? moduleIndexText = null)
        {
            ModuleID = moduleID;
            ModuleName = moduleName;
            SubModules = subModules;
            ModuleIndexText = moduleIndexText;
        }
    }
}
