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
    }
}
