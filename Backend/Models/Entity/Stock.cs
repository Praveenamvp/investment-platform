namespace Models.Entity
{
    public class Stock
    {
        public Guid StockId { get; set; }
        public string? Symbol { get; set; }
        public string? Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal ChangePercent { get; set; }
    }
}
