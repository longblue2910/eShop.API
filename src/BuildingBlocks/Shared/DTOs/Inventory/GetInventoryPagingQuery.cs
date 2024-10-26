using Shared.SeedWork;

namespace Shared.DTOs.Inventory
{
    public class GetInventoryPagingQuery : PagingRequestParameter
    {
        public string ItemNo() => _itemNo;
        private string _itemNo { get; set; }

        public void SetItemNo(string itemNo) => _itemNo = itemNo;
        public string SearchTerm { get; set; }
    }
}
