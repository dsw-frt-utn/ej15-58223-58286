
using Dsw2026Ej15.Api.Middleware;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=Dsw2026Ej16; Integrated Security=True; Connect Timeout=30; Encrypt=True; Trust Server Certificate=True";

            builder.Services.AddDbContext<Dsw2026Ej15DbContext>(
                options=>
                {
                    options.UseSqlServer(connectionString);
                });
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IPersistence, PersistenceEf>();
            builder.Services.AddHealthChecks();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.MapControllers();
            app.MapHealthChecks("health-check");

            app.Run();
        }
    }
}
