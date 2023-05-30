using Hospital.Domain.Events;
using Hospital.Domain.Infastructure;
using Hospital.Infrastructure;
using System.Reflection;

namespace Hospital.API
{
    public static class WebApp
    {
        public static WebApplication Start(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHospitalDb("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital;Integrated Security=true;");
            builder.Services.AddEventDb("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalEvents;Integrated Security=true;");
            builder.Services.AddMediatR((cfg) => 
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.RegisterServicesFromAssemblyContaining<PetTransferedEvent>();
            });
            builder.Services.AddScoped<PatientStore>(); // This should be a singl

            var app = builder.Build();
            app.EnsureHospitalDbIsCreated();
            app.EnsureEventDbIsCreated();

            return app;
        }
    }
}
