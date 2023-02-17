using System.ComponentModel.DataAnnotations;

namespace PickAndGo.ViewModels
{
    public class UserVM
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }



    }
}
