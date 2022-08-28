using Microsoft.EntityFrameworkCore;
using ProductCrud.Models;

namespace ProductCrud.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Mobile> Mobiles { get; set; }
}