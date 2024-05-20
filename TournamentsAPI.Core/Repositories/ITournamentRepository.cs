using TournamentsAPI.Core.Entities;

namespace TournamentsAPI.Core.Repositories;

public interface ITournamentRepository
{
    Task<IEnumerable<Tournament>> GetAllAsync();
    Task<Tournament?> GetAsync(int id);
    Task<bool> AnyAsync(int id);
    Task Add(Tournament tournament);
    Task Update(Tournament tournament);
    Task Remove(Tournament tournament);
}
