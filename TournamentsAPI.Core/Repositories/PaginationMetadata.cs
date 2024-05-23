namespace TournamentsAPI.Core.Repositories;

public readonly struct PaginationMetadata(int totalItemCount, int pageSize, int currentPage)
{
    public readonly int TotalItemCount { get; } = totalItemCount;
    public readonly int PageSize { get; } = pageSize;
    public readonly int CurrentPage { get; } = currentPage;

    public readonly int TotalPageCount =>
        (int)Math.Ceiling(TotalItemCount / (double)PageSize);
}
