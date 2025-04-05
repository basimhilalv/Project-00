namespace Project_00.Models
{
    public class PaymentProduct
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Product? Product { get; set; }

    }
}
