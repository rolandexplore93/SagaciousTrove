using Microsoft.EntityFrameworkCore;
using SagaciousTrove.Models;

namespace SagaciousTrove.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}
