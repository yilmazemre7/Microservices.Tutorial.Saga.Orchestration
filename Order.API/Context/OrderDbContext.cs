using Microsoft.EntityFrameworkCore;
using Order.API.Models;

namespace Order.API.Context
{
    public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
    {
        public DbSet<Models.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
