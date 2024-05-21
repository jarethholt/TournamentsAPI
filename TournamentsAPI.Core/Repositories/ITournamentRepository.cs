using TournamentsAPI.Core.Entities;

namespace TournamentsAPI.Core.Repositories;

public interface ITournamentRepository
{
    Task<IEnumerable<Tournament>> GetAllAsync(bool sort);
    Task<Tournament?> GetAsync(int id, bool includeGames);
    Task<bool> AnyAsync(int id);
    void Add(Tournament tournament);
    void Update(Tournament tournament);
    void Remove(Tournament tournament);
}
