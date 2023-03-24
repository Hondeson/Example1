using Ex1.API.Services.Users;
using Ex1.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ex1.API
{
    public class SetupServices
    {
        public static void Setup(IServiceCollection services, ConfigurationManager configuration)
        {
            string connString = "";

            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var name = Environment.GetEnvironmentVariable("DB_NAME");
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            if (!string.IsNullOrEmpty(host))
                connString = $"Host={host};Database={name};Username={user};Password={password}";
            else
                connString = configuration.GetConnectionString("DB_Data");

            if (string.IsNullOrEmpty(connString))
                throw new Exception("Invalid connection string!");

            services.AddDbContext<Ex1Context>(options => options.UseNpgsql(connString));
            services.AddScoped<IUsersService, UsersDbContextService>();
        }
    }
}
