using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
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
        CreateMap<Game, GamePostDTO>()
            .ReverseMap();
        CreateMap<Game, GameWithIdDTO>()
            .ReverseMap();
        CreateMap<JsonPatchDocument<TournamentPostDTO>, JsonPatchDocument<Tournament>>();
        CreateMap<Operation<TournamentPostDTO>, Operation<Tournament>>();
        CreateMap<JsonPatchDocument<GamePostDTO>, JsonPatchDocument<Game>>();
        CreateMap<Operation<GamePostDTO>, Operation<Game>>();
    }
}
