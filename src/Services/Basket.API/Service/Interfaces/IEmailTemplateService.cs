namespace Basket.API.Service.Interfaces;

public interface IEmailTemplateService
{
    string GenerateReminderCheckoutOrderEmail(string username, string checkoutUrl = "basket/checkout");   
}
