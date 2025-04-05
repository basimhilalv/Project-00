using Microsoft.EntityFrameworkCore;
using Project_00.Models;

namespace Project_00.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> CartItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<PaymentProduct> PaymentProducts { get; set; }
        public DbSet<PaymentCart> PaymentCarts { get; set; }
    }
}
