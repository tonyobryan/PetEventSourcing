using Microsoft.EntityFrameworkCore;

namespace Pets.API.Infastructure
{
    public class PetDbContext : DbContext
    {
        public DbSet<Models.Pet> Pets { get; set; }

        public PetDbContext(DbContextOptions<PetDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Pet>().HasKey(p => p.Id);
        }
    }

    public static class DbContextExtensions
    {
        public static void AddDb(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PetDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
        public static void EnsureDbIsCreated(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<PetDbContext>())
            {
                context?.Database.EnsureCreated();
            }
        }
    }
}
