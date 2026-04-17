namespace Models.Entity
{
    public class Portfolio
    {
        public Guid PortfolioId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
