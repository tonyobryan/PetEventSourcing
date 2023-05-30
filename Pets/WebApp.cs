using Pets.API.Infastructure;
using System.Reflection;

namespace Pets.API
{
    public static class WebApp
    {
        public static WebApplication Start(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDb("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Pets;Integrated Security=true;");
            builder.Services.AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            var app = builder.Build();
            app.EnsureDbIsCreated();

            return app;
        }
    }
}
