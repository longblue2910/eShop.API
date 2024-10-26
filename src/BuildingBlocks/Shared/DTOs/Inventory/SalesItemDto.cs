using Shared.Enums.Inventory;

namespace Shared.DTOs.Inventory
{
    public class SalesItemDto
    {
        public string ItemNo { get; set; }
        public int Quantity { get; set; }
        public EDocumentType DocumentType => EDocumentType.Sale;
    }
}
