using PickAndGo.Models;
using SendGrid;
using System.Threading.Tasks;

namespace PickAndGo.Services
{
    public interface IEmailService
    {
        Task<Response> SendRegistrationEmail(RegistrationEmailModel payload);
        Task<Response> SendConfirmationEmail(ConfirmationEmailModel payload);

    }
}
