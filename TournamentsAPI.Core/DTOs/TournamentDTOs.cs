﻿namespace TournamentsAPI.Core.DTOs;

public class TournamentPostDTO
{
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate => StartDate.AddMonths(3);
}

public class TournamentWithIdDTO : TournamentPostDTO
{
    public int Id { get; set; }
}