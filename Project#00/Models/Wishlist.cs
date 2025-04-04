namespace Project_00.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
        public User? User { get; set; }
        public Product? Product { get; set; }
    }
}
