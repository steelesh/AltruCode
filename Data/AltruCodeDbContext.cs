using AltruCode.Models;
using Microsoft.EntityFrameworkCore;

namespace AltruCode.Data;

public class AltruCodeDbContext : DbContext
{
    public AltruCodeDbContext(DbContextOptions<AltruCodeDbContext> options)
        : base(options)
    {
    }

    public DbSet<User>? Users { get; set; }
    public DbSet<Project>? Projects { get; set; }
}