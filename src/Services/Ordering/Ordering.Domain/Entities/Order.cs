using Contracts.Common.Events;
using Ordering.Domain.OrderAggregate.Events;
using Shared.Enums.Order;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Domain.Entities;

public class Order : AuditableEventEntity<long>
{
    public string DocumentNo { get; set; }
    [Required]
    public string UserName { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalPrice { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(250)")]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    [Column(TypeName = "nvarchar(250)")]
    public string EmailAddress { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string ShippingAddress { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string InvoiceAddress { get; set; }
    public OrderStatus Status { get; set; }

    public Order AddedOrder()
    {
        AddDomainEvent(new OrderCreatedEvent(Id, DocumentNo, UserName, TotalPrice, FirstName, LastName, EmailAddress, ShippingAddress, InvoiceAddress));
        return this;
    }

    public Order DeletedOrder()
    {
        RemoveDomainEvent(new OrderDeletedEvent(Id));
        return this;
    }
}
