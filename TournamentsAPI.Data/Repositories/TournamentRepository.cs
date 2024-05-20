using Microsoft.EntityFrameworkCore;
using TournamentsAPI.API.Data;
using TournamentsAPI.Core.Entities;
using TournamentsAPI.Core.Repositories;

namespace TournamentsAPI.Data.Repositories;

public class TournamentRepository(TournamentsContext context) : ITournamentRepository
{
    private readonly TournamentsContext _context = context;

    public void Add(Tournament tournament) =>
        _context.Add(tournament);

    public async Task<bool> AnyAsync(int id) =>
        await _context.Tournament.AnyAsync(t => t.Id == id);

    public async Task<IEnumerable<Tournament>> GetAllAsync() =>
        await _context.Tournament.ToListAsync();

    public async Task<Tournament?> GetAsync(int id) =>
        await _context.Tournament.FirstOrDefaultAsync(t => t.Id == id);

    public void Remove(Tournament tournament) =>
        _context.Remove(tournament);

    public void Update(Tournament tournament) =>
        _context.Update(tournament);
}
