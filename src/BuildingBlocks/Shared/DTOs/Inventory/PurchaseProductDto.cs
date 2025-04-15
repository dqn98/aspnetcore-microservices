using Shared.Enums.Inventory;

namespace Shared.DTOs.Inventory;

public class PurchaseProductDto
{
    public EDocumentType DocumentType { get; set; }
    public string? ItemNo { get; set; }
    public string? DocumentNo  { get; set; }
    public int Quantity { get; set; }
    public string? ExternalDocumentNo { get; set; }
}