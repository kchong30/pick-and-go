using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PickAndGo.ViewModels
{
    public class RoleVM
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

    }
}
