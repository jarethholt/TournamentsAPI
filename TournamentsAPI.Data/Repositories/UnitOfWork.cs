using TournamentsAPI.API.Data;
using TournamentsAPI.Core.Repositories;
using TournamentsAPI.Data.Data;

namespace TournamentsAPI.Data.Repositories;

public class UnitOfWork(TournamentsContext context) : IUnitOfWork
{
    public ITournamentRepository TournamentRepository { get; } = new TournamentRepository(context);
    public IGameRepository GameRepository { get; } = new GameRepository(context);
    private readonly TournamentsContext _context = context;

    public async Task CompleteAsync() =>
        await _context.SaveChangesAsync();
}
