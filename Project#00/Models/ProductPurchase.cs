namespace Project_00.Models
{
    public class ProductPurchase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
