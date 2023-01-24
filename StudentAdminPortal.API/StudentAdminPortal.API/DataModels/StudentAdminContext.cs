using Microsoft.EntityFrameworkCore;

namespace StudentAdminPortal.API.DataModels;

public class StudentAdminContext : DbContext
{
    public StudentAdminContext(DbContextOptions<StudentAdminContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public DbSet<Student?> Students { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Address> Addresses { get; set; }
}