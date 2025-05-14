using AcunMedyaProject3.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcunMedyaProject3.Context;

public class ProjectContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //
        optionsBuilder.UseSqlServer("Server=ABDULLAH;initial catalog=AcunMedyaProject3;integrated security=true");
    }

    public DbSet<Category> Categories { get;set;}
    public DbSet<Test> Tests { get;set;}

    public DbSet<Users> Users { get;set;}

}
