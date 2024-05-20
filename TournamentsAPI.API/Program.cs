using Microsoft.EntityFrameworkCore;
using TournamentsAPI.API.Data;
using TournamentsAPI.API.Extensions;

namespace TournamentsAPI.API;

public class Program
{
    public static async void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<TournamentsContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("TournamentsContext") ?? throw new InvalidOperationException("Connection string 'TournamentsContext' not found.")));

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
