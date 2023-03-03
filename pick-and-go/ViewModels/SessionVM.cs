using DataAnnotationsExtensions;

namespace PickAndGo.ViewModels
{
    public class SessionVM
    {
        public string? PickupTime { get; set; }
        public string? CartJson { get; set; }
        
        [Email]
        public string? Email { get; set; }
    }
}
