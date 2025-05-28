using AcunMedyaProject3.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace AcunMedyaProject3.Context;

public class ProjectContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("workstation id=project3.mssql.somee.com;packet size=4096;user id=abdullahkus_SQLLogin_1;pwd=ku15crkej7;data source=project3.mssql.somee.com;persist security info=False;initial catalog=project3;TrustServerCertificate=True");
    }




    public DbSet<Category> Categories { get;set;}
    public DbSet<Test> Tests { get;set;}

    public DbSet<Users> Users { get;set;}
    public DbSet<Reservation> Reservations { get;set;}
    public DbSet<Doctor> Doctors { get;set;}
    public DbSet<Department> Departments { get;set;}

}
