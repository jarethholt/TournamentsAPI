using AutoMapper;
using TournamentsAPI.Core.DTOs;
using TournamentsAPI.Core.Entities;

namespace TournamentsAPI.Data.Data;

public class TournamentMappings : Profile
{
    public TournamentMappings()
    {
        CreateMap<Tournament, TournamentDTO>()
            .ReverseMap();
        CreateMap<Game, GameDTO>()
            .ReverseMap();
    }
}
