namespace TournamentsAPI.Core.Entities;

public class Game
{
    // Core properties
    public int Id { get; set; }
    public required string Title { get; set; }
    public DateTime Time { get; set; }

    // Navigation property
    public int TournamentId { get; set; }
}
