using Microsoft.EntityFrameworkCore;
using TournamentsAPI.API.Data;
using TournamentsAPI.Core.Entities;
using TournamentsAPI.Core.Repositories;

namespace TournamentsAPI.Data.Repositories;

public class GameRepository(TournamentsContext context) : IGameRepository
{
    private readonly TournamentsContext _context = context;
    private readonly DbSet<Game> _dbset = context.Game;

    public void Add(Game game) =>
        _context.Add(game);

    public async Task<bool> AnyAsync(int id) =>
        await _dbset.AnyAsync(g => g.Id == id);

    public async Task<IEnumerable<Game>> GetAllAsync(bool sort)
    {
        IQueryable<Game> query = _dbset;
        if (sort)
            query = query.OrderBy(g => g.Time);
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Game>> GetAllFromTournament(int tournamentId, bool sort)
    {
        IQueryable<Game> query = _dbset.Where(g => g.TournamentId == tournamentId);
        if (sort)
            query = query.OrderBy(g => g.Time);
        return await query.ToListAsync();
    }

    public async Task<Game?> GetByIdAsync(int id) =>
        await _dbset.FirstOrDefaultAsync(g => g.Id == id);

    public async Task<Game?> GetByTitleAsync(string title) =>
        await _dbset.FirstOrDefaultAsync(g => g.Title == title);

    public void Remove(Game game) =>
        _context.Remove(game);

    public void Update(Game game) =>
        _context.Update(game);
}
