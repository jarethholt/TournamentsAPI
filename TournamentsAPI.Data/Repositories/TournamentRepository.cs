using Microsoft.EntityFrameworkCore;
using TournamentsAPI.API.Data;
using TournamentsAPI.Core.Entities;
using TournamentsAPI.Core.Repositories;

namespace TournamentsAPI.Data.Repositories;

public class TournamentRepository : ITournamentRepository
{
    private TournamentsContext _context;

    public TournamentRepository(TournamentsContext context) =>
        _context = context;

    public async Task Add(Tournament tournament)
    {
        _context.Add(tournament);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AnyAsync(int id)
    {
        return await _context.Tournament.AnyAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Tournament>> GetAllAsync()
    {
        return await _context.Tournament.ToListAsync();
    }

    public async Task<Tournament?> GetAsync(int id)
    {
        return await _context.Tournament.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task Remove(Tournament tournament)
    {
        _context.Remove(tournament);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Tournament tournament)
    {
        _context.Update(tournament);
        await _context.SaveChangesAsync();
    }
}
