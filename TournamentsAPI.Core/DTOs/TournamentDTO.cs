namespace TournamentsAPI.Core.DTOs;

public class TournamentDTO
{
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate => StartDate.AddMonths(3);
}
