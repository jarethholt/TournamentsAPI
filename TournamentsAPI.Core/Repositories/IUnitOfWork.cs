namespace TournamentsAPI.Core.Repositories;

public interface IUnitOfWork
{
    ITournamentRepository TournamentRepository { get; }
    IGameRepository GameRepository { get; }

    Task CompleteAsync();
}
