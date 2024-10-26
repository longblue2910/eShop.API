namespace Shared.DTOs.Baskets
{
    public class CartDto
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public List<CartItemDto> Items { get; set; } = new();

        public CartDto()
        {

        }

        public CartDto(string username)
        {
            Username = username;
        }

        public decimal TotalPrice => Items.Sum(x => x.ItemPrice * x.Quantity);

        public string JobId { get; set; }
    }
}
