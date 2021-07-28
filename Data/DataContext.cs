using authenticationJWT.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationJWT.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<User>(entity => { entity.HasIndex(u => u.Email).IsUnique(); });
    }
  }
}