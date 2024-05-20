namespace TournamentsAPI.Core.DTOs;

public class GamePostDTO
{
    public string Title { get; set; } = string.Empty;
    public DateTime Time { get; set; }
    public int TournamentId { get; set; }
}

public class GameWithIdDTO : GamePostDTO
{
    public int Id { get; set; }
}
