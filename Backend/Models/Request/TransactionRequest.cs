namespace Models.Request
{
    public class TransactionRequest
    {
        public Guid UserId { get; set; }
        public Guid AssetId { get; set; }
        public string? AssetType { get; set; } // "Stock" or "MutualFund"
        public decimal Quantity { get; set; }
    }
}
