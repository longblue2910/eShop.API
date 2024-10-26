namespace Shared.Services.Email;

public class MailModel
{
    public string DocumentNo { get; set; }
    public string UserName { get; set; }
    public decimal TotalPrice { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string ShippingAddress { get; set; }
    public string InvoiceAddress { get; set; }
    public string Url { get; set; }
}
