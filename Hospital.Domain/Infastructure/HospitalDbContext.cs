using Hospital.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Domain.Infastructure
{
    // TODO: Remove / Replace with rehidrated content?

    public class HospitalDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Patient>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }
    }

    public static class HospitalDbContextExtensions
    {
        public static void AddHospitalDb(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<HospitalDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
        public static void EnsureHospitalDbIsCreated(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<HospitalDbContext>())
            {
                context?.Database.EnsureCreated();
            }
        }
    }
}
