using Shared.SeedWork;

namespace Shared.DTOs.Inventory;

public abstract class GetInventoryPagingQuery : PagingRequestParameters
{
    public string? ItemNo  { get; set; }
    private string? _itemNo;
    
    public void SetItemNo(string itemNo) => _itemNo = itemNo;
    public string? SearchTerm { get; set; }
    
}