using Microsoft.EntityFrameworkCore;
using Project_00.Models;

namespace Project_00.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> CartItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<PaymentProduct> PaymentProducts { get; set; }
        public DbSet<PaymentCart> PaymentCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductPurchase> ProductPurachases { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
