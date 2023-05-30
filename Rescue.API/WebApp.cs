using Rescue.API.Infastructure;
using System.Reflection;

namespace Rescue.API
{
    public static class WebApp
    {
        public static WebApplication Start(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDb("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Rescue;Integrated Security=true;");
            builder.Services.AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            var app = builder.Build();
            app.EnsureDbIsCreated();

            return app;
        }
    }
}
