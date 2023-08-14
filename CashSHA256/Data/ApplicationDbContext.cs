using CashSHA256.Model;
using Microsoft.EntityFrameworkCore;

namespace CashSHA256.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> users { get; set; }
    }
}
