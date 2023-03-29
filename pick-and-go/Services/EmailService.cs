using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PickAndGo.Models;
using PickAndGo.ViewModels;
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
            /*            var apiKey = _configuration.GetSection("SendGrid")["ApiKey"];*/
            var apiKey = _configuration["SendGrid:ApiKey"];

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("pickandgoinc@gmail.com", "Pick And Go");
            var to = new EmailAddress(payload.Email
                                     , $"{payload.FirstName} {payload.LastName}");
            var templateId = "d-aea4e36c45544c50b279e40406207b59";
            List<ShoppingCartVM> products = JsonConvert.DeserializeObject<List<ShoppingCartVM>>(payload.OrderDetails);
            var productsArray = products.ToArray();

            var dynamicTemplateData = new
            {
                name = payload.FirstName + " " + payload.LastName,
                subject = "Your Order Has Been Placed!",
                items = productsArray
            };
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);
            var request = client.SendEmailAsync(msg);
            request.Wait();
            var result = request.Result;
            return request;
        }
    }
}
