using Microsoft.EntityFrameworkCore;
using TournamentsAPI.API.Data;
using TournamentsAPI.Core.Entities;
using TournamentsAPI.Core.Repositories;

namespace TournamentsAPI.Data.Repositories;

public class TournamentRepository(TournamentsContext context) : ITournamentRepository
{
    private readonly TournamentsContext _context = context;
    private readonly DbSet<Tournament> _dbset = context.Tournament;

    public void Add(Tournament tournament) =>
        _context.Add(tournament);

    public async Task<bool> AnyAsync(int id) =>
        await _dbset.AnyAsync(t => t.Id == id);

    public async Task<IEnumerable<Tournament>> GetAllAsync(bool sort)
    {
        IQueryable<Tournament> query = _dbset;
        if (sort)
            query = query.OrderBy(t => t.StartDate);
        return await query.ToListAsync();
    }

    public async Task<Tournament?> GetAsync(int id, bool includeGames = false)
    {
        IQueryable<Tournament> query = _dbset;
        if (includeGames)
            query = query.Include(t => t.Games);
        return await query.FirstOrDefaultAsync(t => t.Id == id);
    }

    public void Remove(Tournament tournament) =>
        _context.Remove(tournament);

    public void Update(Tournament tournament) =>
        _context.Update(tournament);
}
