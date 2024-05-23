using TournamentsAPI.Core.Entities;

namespace TournamentsAPI.Core.Repositories;

public interface ITournamentRepository
{
    Task<(IEnumerable<Tournament>, PaginationMetadata)> GetAllAsync(
        bool sort, int currentPage, int pageSize, string? filterTitle);
    Task<Tournament?> GetAsync(int id, bool includeGames);
    Task<bool> AnyAsync(int id);
    void Add(Tournament tournament);
    void Update(Tournament tournament);
    void Remove(Tournament tournament);
}
