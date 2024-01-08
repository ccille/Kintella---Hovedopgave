using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintellaLocalizationREST.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }

        public User()
        {
            
        }

        public User(int userID, string username, string password)
        {
            UserID = userID;
            Username = username;
            Password = password;
        }


    }
}
