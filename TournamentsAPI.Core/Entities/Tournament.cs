namespace TournamentsAPI.Core.Entities;

public class Tournament
{
    // Core properties
    public int Id { get; set; }
    public required string Title { get; set; }
    public DateTime StartDate { get; set; }

    // Navigation property
    public ICollection<Game> Games { get; set; } = [];
}
