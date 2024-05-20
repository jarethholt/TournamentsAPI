using Microsoft.EntityFrameworkCore;
using TournamentsAPI.API.Data;
using TournamentsAPI.Core.Entities;
using TournamentsAPI.Core.Repositories;

namespace TournamentsAPI.Data.Repositories;

public class GameRepository(TournamentsContext context) : IGameRepository
{
    private readonly TournamentsContext _context = context;

    public void Add(Game game) =>
        _context.Add(game);

    public async Task<bool> AnyAsync(int id) =>
        await _context.Game.AnyAsync(g => g.Id == id);

    public async Task<IEnumerable<Game>> GetAllAsync() =>
        await _context.Game.ToListAsync();

    public async Task<Game?> GetAsync(int id) =>
        await _context.Game.FirstOrDefaultAsync(g => g.Id == id);

    public void Remove(Game game) =>
        _context.Remove(game);

    public void Update(Game game) =>
        _context.Update(game);
}
