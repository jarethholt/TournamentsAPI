using Microsoft.EntityFrameworkCore;
using TournamentsAPI.API.Data;
using TournamentsAPI.Core.Entities;
using TournamentsAPI.Core.Repositories;

namespace TournamentsAPI.Data.Repositories;

public class GameRepository : IGameRepository
{
    private TournamentsContext _context;

    public GameRepository(TournamentsContext context) =>
        _context = context;

    public async Task Add(Game game)
    {
        _context.Add(game);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AnyAsync(int id)
    {
        return await _context.Game.AnyAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await _context.Game.ToListAsync();
    }

    public async Task<Game?> GetAsync(int id)
    {
        return await _context.Game.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task Remove(Game game)
    {
        _context.Remove(game);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Game game)
    {
        _context.Update(game);
        await _context.SaveChangesAsync();
    }
}
