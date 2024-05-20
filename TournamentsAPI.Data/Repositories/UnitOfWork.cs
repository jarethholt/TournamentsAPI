using TournamentsAPI.API.Data;
using TournamentsAPI.Core.Repositories;
using TournamentsAPI.Data.Data;

namespace TournamentsAPI.Data.Repositories;

public class UnitOfWork(
    ITournamentRepository tournamentRepository,
    IGameRepository gameRepository,
    TournamentsContext context
) : IUnitOfWork
{
    public ITournamentRepository TournamentRepository { get; } = tournamentRepository;
    public IGameRepository GameRepository { get; } = gameRepository;
    private readonly TournamentsContext _context = context;

    public async Task CompleteAsync() =>
        await _context.SaveChangesAsync();
}
