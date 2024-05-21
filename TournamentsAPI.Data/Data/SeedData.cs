using TournamentsAPI.API.Data;
using TournamentsAPI.Core.Entities;

namespace TournamentsAPI.Data.Data;

public static class SeedData
{
    private static readonly List<Tournament> tournaments = [
        new() { Title = "2023 World Women's Handball Championship", StartDate = new(2023, 11, 29, 00, 00, 00), Games = [
            new() { Title = "Quarterfinal 2-1 (France) vs 4-2 (Czech Republic)", Time = new(2023, 12, 12, 17, 30, 00) },
            new() { Title = "Quarterfinal 4-1 (Netherlands) vs 2-2 (Norway)", Time = new(2023, 12, 12, 20, 30, 00) },
            new() { Title = "Quarterfinal 1-1 (Sweden) vs 3-2 (Germany)", Time = new(2023, 12, 13, 17, 30, 00) },
            new() { Title = "Quarterfinal 3-1 (Denmark) vs 1-2 (Montenegro)", Time = new(2023, 12, 13, 20, 30, 00) },
            new() { Title = "Semifinal 3-1/1-2 (Denmark) vs 4-1/2-2 (Norway)", Time = new(2023, 12, 15, 17, 30, 00) },
            new() { Title = "Semifinal 1-1/3-2 (Sweden) vs 2-1/4-2 (France)", Time = new(2023, 12, 15, 21, 00, 00) },
            new() { Title = "Final 1-1/2-1/3-2/4-2 (France) vs 1-2/2-2/3-1/4-1 (Norway)", Time = new(2023, 12, 17, 19, 00, 00) } ] },
        new() { Title = "2023 World Men's Handball Championship", StartDate = new(2023, 01, 11, 00, 00, 00), Games = [
            new() { Title = "Quarterfinal 4-1 (Denmark) vs 2-2 (Hungary)", Time = new(2023, 01, 25, 18, 00, 00) },
            new() { Title = "Quarterfinal 3-1 (Norway) vs 1-2 (Spain)", Time = new(2023, 01, 25, 18, 00, 00) },
            new() { Title = "Quarterfinal 2-1 (Sweden) vs 4-2 (Egypt)", Time = new(2023, 01, 25, 20, 30, 00) },
            new() { Title = "Quarterfinal 1-1 (France) vs 3-2 (Germany)", Time = new(2023, 01, 25, 20, 54, 00) },
            new() { Title = "Semifinal 3-1/1-2 (Spain) vs 4-1/2-2 (Denmark)", Time = new(2023, 01, 27, 18, 00, 00) },
            new() { Title = "Semifinal 1-1/3-2 (France) vs 2-1/4-2 (Sweden)", Time = new(2023, 01, 27, 21, 00, 00) },
            new() { Title = "Final 1-1/2-1/3-2/4-2 (France) vs 1-2/2-2/3-1/4-1 (Denmark)", Time = new(2023, 01, 29, 21, 00, 00) } ] }
    ];

    public static async Task SeedDataAsync(TournamentsContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.AddRange(tournaments);
        await context.SaveChangesAsync();
    }
}
