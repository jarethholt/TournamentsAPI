using AutoMapper;
using TournamentsAPI.Core.DTOs;
using TournamentsAPI.Core.Entities;

namespace TournamentsAPI.Data.Data;

public class TournamentMappings : Profile
{
    public TournamentMappings()
    {
        CreateMap<Tournament, TournamentPostDTO>()
            .ReverseMap();
        CreateMap<Tournament, TournamentWithIdDTO>()
            .ReverseMap();
        CreateMap<Game, GameDTO>()
            .ReverseMap();
    }
}
