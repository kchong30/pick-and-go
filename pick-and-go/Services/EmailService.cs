using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PickAndGo.Models;
using PickAndGo.ViewModels;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Globalization;
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
            decimal totalCost = 0;

            DateTime dateObj = DateTime.ParseExact(payload.PickUpTime, "yyyy-MM-dd'T'HH:mm:ss", null);
            string formattedPickUp = dateObj.ToString("yyyy-MM-dd - hh:mm:ss tt");

            foreach (var product in productsArray )
            {
                if (decimal.TryParse(product.subtotal, out var subtotal))
                {
                    totalCost = totalCost + subtotal;
                    product.subtotal = subtotal.ToString("C");
                }
                else
                {
                    Console.WriteLine($"Unable to parse subtotal for product: {product.subtotal}");
                }
            }

            var totalString = totalCost.ToString("C");


            var dynamicTemplateData = new
            {
                name = payload.FirstName + " " + payload.LastName,
                subject = "Your Order Has Been Placed!",
                items = productsArray,
                pickuptime = formattedPickUp,
                total = totalString
            };
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);
            var request = client.SendEmailAsync(msg);
            request.Wait();
            var result = request.Result;
            return request;
        }
    }
}
