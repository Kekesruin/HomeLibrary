using Microsoft.EntityFrameworkCore;
using HomeLibrary.Models;

namespace HomeLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Book> Books { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("books");
        }
    }
}
