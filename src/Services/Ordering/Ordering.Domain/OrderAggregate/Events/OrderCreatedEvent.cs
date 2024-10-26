using Contracts.Common.Events;

namespace Ordering.Domain.OrderAggregate.Events;

public class OrderCreatedEvent(long id, string documentNo, string userName, decimal totalPrice, string firstName, string lastName, string emailAddress, string shippingAddress, string invoiceAddress) : BaseEvent
{
    public long Id { get; private set; } = id;
    public string DocumentNo { get; private set; } = documentNo;
    public string UserName { get; private set; } = userName;
    public decimal TotalPrice { get; private set; } = totalPrice;
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
    public string EmailAddress { get; private set; } = emailAddress;
    public string ShippingAddress { get; private set; } = shippingAddress;
    public string InvoiceAddress { get; private set; } = invoiceAddress;
    public string Url { get; set; }
}
