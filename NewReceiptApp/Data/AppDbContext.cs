using NewReceiptApp.Models;
using Microsoft.EntityFrameworkCore;

namespace NewReceiptApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Additional configuration can go here
        }

        public DbSet<Customer> Customers { get; set; }
 
        public DbSet<Item> Items { get; set; }
        public DbSet<Receipt> Receipt { get; set; }
        public DbSet<Transaction> Transaction { get; set; }


    }
}
