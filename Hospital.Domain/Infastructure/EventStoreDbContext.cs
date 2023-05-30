using Hospital.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Domain.Infastructure
{
    public class EventStoreDbContext : DbContext
    {
        public EventStoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EventModel> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventModel>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<EventModel>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            //modelBuilder.Entity<EventModel>() // This isn't working...
            //    .OwnsOne(e => e.Json, o =>
            //    {
            //        o.ToJson();
            //    });
        }
    }

    public static class EventStoreDbContextExtensions
    {
        public static void AddEventDb(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EventStoreDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
        public static void EnsureEventDbIsCreated(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<EventStoreDbContext>())
            {
                context?.Database.EnsureCreated();
            }
        }
    }
}
