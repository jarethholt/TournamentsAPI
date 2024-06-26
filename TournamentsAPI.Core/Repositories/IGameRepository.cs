﻿using TournamentsAPI.Core.Entities;

namespace TournamentsAPI.Core.Repositories;

public interface IGameRepository
{
    Task<(IEnumerable<Game>, PaginationMetadata)> GetAllAsync(
        bool sort, int currentPage, int pageSize, string? filterTitle);
    Task<(IEnumerable<Game>, PaginationMetadata)> GetAllFromTournament(
        int tournamentId, bool sort, int currentPage, int pageSize, string? filterTitle);
    Task<Game?> GetByIdAsync(int id);
    Task<Game?> GetByTitleAsync(string title);
    Task<bool> AnyAsync(int id);
    void Add(Game game);
    void Update(Game game);
    void Remove(Game game);
}
