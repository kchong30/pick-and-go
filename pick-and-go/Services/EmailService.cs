using Microsoft.Extensions.Configuration;
using PickAndGo.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace PickAndGo.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<Response> SendRegistrationEmail(RegistrationEmailModel payload)
        {
            //var apiKey = _configuration.GetSection("SendGrid")["ApiKey"];
            var apiKey = _configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("pickandgoinc@gmail.com", "Pick And Go");
            var subject = payload.Subject;
            var to = new EmailAddress(payload.Email
                                     , $"{payload.FirstName} {payload.LastName}");
            var textContent = payload.Body;
            var htmlContent = $"<strong>{payload.Body}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject
                                                  , textContent, htmlContent);
            var request = client.SendEmailAsync(msg);
            request.Wait();
            var result = request.Result;
            return request;
        }

        public Task<Response> SendConfirmationEmail(ConfirmationEmailModel payload)
        {
            var apiKey = _configuration.GetSection("SendGrid")["ApiKey"];
/*            var apiKey = _configuration["SendGrid:ApiKey"];
*/            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("pickandgoinc@gmail.com", "Pick And Go");
            var subject = "Your Order Has Been Placed!";
            var to = new EmailAddress(payload.Email
                                     , $"{payload.FirstName} {payload.LastName}");
            var textContent = "ORDER PLACED FOR PICK UP AT: " + payload.PickUpTime + ".";
            var htmlContent = $"<strong>{textContent}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject
                                                  , textContent, htmlContent);
            var request = client.SendEmailAsync(msg);
            request.Wait();
            var result = request.Result;
            return request;
        }
    }
}
