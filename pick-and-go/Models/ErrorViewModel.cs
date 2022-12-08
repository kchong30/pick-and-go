namespace PickAndGo.Models
{
    public class ErrorViewModels
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}