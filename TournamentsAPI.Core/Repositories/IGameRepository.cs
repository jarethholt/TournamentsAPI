using TournamentsAPI.Core.Entities;

namespace TournamentsAPI.Core.Repositories;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetAllAsync();
    Task<Game?> GetAsync(int id);
    Task<bool> AnyAsync(int id);
    Task Add(Game game);
    Task Update(Game game);
    Task Remove(Game game);
}
