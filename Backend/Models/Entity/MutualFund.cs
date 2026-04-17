namespace Models.Entity
{
    public class MutualFund
    {
        public Guid MutualFundId { get; set; }
        public string? Name { get; set; }
        public decimal NAV { get; set; }
        public decimal ReturnsPercent { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
