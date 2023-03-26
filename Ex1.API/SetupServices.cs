using Ex1.API.Services.Users;
using Ex1.API.Swagger;
using Ex1.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Ex1.API
{
    public static class SetupServices
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
            services.AddScoped<IUserValidationService, UserValidationService>();

            services.AddSwaggerGen(SetSwaggerGenOptions);
        }

        static void SetSwaggerGenOptions(SwaggerGenOptions options)
        {
            DocumentModelAssembly();
            DocumentApi();
            return;

            void DocumentModelAssembly()
            {
                var modelXmlFile = $"{Assembly.GetAssembly(typeof(Ex1Context)).GetName().Name}.xml";
                var modelXmlPath = Path.Combine(AppContext.BaseDirectory, modelXmlFile);

                var doc = XDocument.Load(modelXmlPath);
                options.IncludeXmlComments(() => new XPathDocument(doc.CreateReader()), true);
                options.SchemaFilter<DescribeEnumMembers>(doc);
            }

            void DocumentApi()
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            }
        }
    }
}