namespace Basket.API.Entities;

public class Cart
{
    public string Username { get; set; }
    public string EmailAddress { get; set; }
    public List<CartItem> Items { get; set; } = [];

    public Cart()
    {

    }

    public Cart(string username)
    {
        Username = username;
    }

    public decimal TotalPrice => Items.Sum(x => x.ItemPrice * x.Quantity);

    public DateTimeOffset LastModifiedDate { get; set; } = DateTimeOffset.UtcNow;
    public string JobId { get; set; }
}
