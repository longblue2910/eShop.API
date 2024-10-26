namespace Shared.DTOs.Inventory
{
    public class SalesOrderDto
    {
        public string OrderNo { get; set; }
        public List<SalesItemDto> SalesItems { get; set; }
    }
}
