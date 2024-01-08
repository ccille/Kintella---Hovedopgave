using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintellaLocalizationREST.Model
{
    public class TextChanges
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TextChangeID { get; set; }
        [ForeignKey("TextID")]
        public Text TextObject { get; set; }
        public DateTime DateModified { get; set; }
        //public List<Text>? ListOfChangedTexts { get; set; }
        public string? TextChangeFrom { get; set; }
        public string? TextChangeTo { get; set; }

        public TextChanges()
        {
            TextObject = new Text();
        }
    }
}
