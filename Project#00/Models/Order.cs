namespace Project_00.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string? PaymentMode { get; set; }
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public Guid UserId { get; set; }
        public List<string>? Products { get; set; }

    }
}
