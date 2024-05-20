using Microsoft.EntityFrameworkCore;
using TournamentsAPI.API.Data;
using TournamentsAPI.API.Extensions;
using TournamentsAPI.Core.Repositories;
using TournamentsAPI.Data.Repositories;

namespace TournamentsAPI.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<TournamentsContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("TournamentsContext")
                ?? throw new InvalidOperationException("Connection string 'TournamentsContext' not found.")));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Add services to the container.
        builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
            .AddNewtonsoftJson()
            .AddXmlDataContractSerializerFormatters();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            // Seed the database
            await app.SeedDataAsync();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
