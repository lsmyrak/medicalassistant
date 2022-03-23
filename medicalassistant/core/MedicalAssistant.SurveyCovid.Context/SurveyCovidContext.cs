using MedicalAssistant.SurveyCovid.Entitis;
using Microsoft.EntityFrameworkCore;

namespace MedicalAssistant.SurveyCovid.Context
{
    public class SurveyCovidContext : DbContext
    {
        public SurveyCovidContext()
        {
        }

        public SurveyCovidContext(DbContextOptions<SurveyCovidContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }

        public DbSet<Survey> Survey { get; set; }

        public DbSet<Department> Department { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<RefreshToken> RefreshToken { get; set; }

        public DbSet<Setting> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=MedicalAssistant; User ID=sa;Password=Immolation@138");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(
                entity =>
                {
                    entity.HasMany(s => s.Survey).WithOne(p => p.Product);
                });
        }
    }
}
