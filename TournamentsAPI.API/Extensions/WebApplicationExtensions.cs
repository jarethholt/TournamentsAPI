using TournamentsAPI.API.Data;
using TournamentsAPI.Data.Data;

namespace TournamentsAPI.API.Extensions;

public static class WebApplicationExtensions
{
    public static Task<int> SeedDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TournamentsContext>();
        return SeedData.SeedDataAsync(context);
    }
}
