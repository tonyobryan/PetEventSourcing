using Microsoft.EntityFrameworkCore;
using Rescue.API.Events;
using Rescue.API.Models;

namespace Rescue.API.Infastructure
{
    public class RescueDbContext : DbContext
    {
        //public DbSet<PetAdoptedEvent> AdoptionMetaData { get; set; }
        public DbSet<RescuedAnimal> RescuedAnimals { get; set; }

        public RescueDbContext(DbContextOptions<RescueDbContext> options) : base(options) { }

        // TODO Fix this
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PetAdoptedEvent>()
                .HasKey(p => p.Id);

            //modelBuilder.Entity<PetAdoptedEvent>()
            //    .HasOne(p => p.RescuedAnimal)
            //    .WithMany(r => r.PetAdoptedEvents)
            //    .HasForeignKey(x => x.);

            modelBuilder.Entity<RescuedAnimal>()
                .HasKey(r => r.Id);

            //modelBuilder.Entity<RescuedAnimal>()
            //    .HasMany(x => x.PetAdoptedEvents)
            //    .WithOne(x => x.RescuedAnimal)
            //    .HasForeignKey(x => x.PetId);
        }
    }

    // TODO Refactor
    public static class PetDbContextExtensions
    {
        public static void AddDb(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RescueDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
        public static void EnsureDbIsCreated(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<RescueDbContext>())
            {
                context?.Database.EnsureCreated();
            }
        }
    }
}
