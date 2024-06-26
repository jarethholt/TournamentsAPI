﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TournamentsAPI.Core.DTOs;
using TournamentsAPI.Core.Entities;
using TournamentsAPI.Core.Repositories;

namespace TournamentsAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TournamentsController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ITournamentRepository _repository = unitOfWork.TournamentRepository;
    const int MaxPageSize = 5;
    const int DefaultPageSize = 2;

    // GET: api/Tournaments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TournamentWithIdDTO>>> GetAllTournaments(
        [FromQuery] bool sort = true,
        [FromQuery] int currentPage = 1,
        [FromQuery] int pageSize = DefaultPageSize,
        [FromQuery] string? filterTitle = null)
    {
        currentPage = Math.Max(currentPage, 1);
        pageSize = Math.Clamp(pageSize, 1, MaxPageSize);

        var (tournaments, paginationMetadata) = await _repository.GetAllAsync(
            sort, currentPage, pageSize, filterTitle);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        return Ok(_mapper.Map<IEnumerable<TournamentWithIdDTO>>(tournaments));
    }

    // GET: api/Tournaments/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTournamentById(
        [FromRoute] int id,
        [FromQuery] bool includeGames = false)
    {
        var tournament = await _repository.GetAsync(id, includeGames);
        if (tournament is null)
            return NotFound();
        var tournamentOut
            = includeGames
            ? _mapper.Map<TournamentWithGamesDTO>(tournament)
            : _mapper.Map<TournamentWithIdDTO>(tournament);
        return Ok(tournamentOut);
    }

    // PUT: api/Tournaments/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTournament(
        [FromRoute] int id,
        [FromBody] TournamentPostDTO tournamentDTO)
    {
        if (!(await TournamentExists(id)))
            return NotFound();

        var tournament = _mapper.Map<Tournament>(tournamentDTO);
        tournament.Id = id;
        _repository.Update(tournament);

        try
        {
            await _unitOfWork.CompleteAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(await TournamentExists(id)))
                return NotFound();
            else
                return StatusCode(500);
        }

        return NoContent();
    }

    // POST: api/Tournaments
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TournamentPostDTO>> PostTournament([FromBody] TournamentPostDTO tournamentDTO)
    {
        var tournament = _mapper.Map<Tournament>(tournamentDTO);
        _repository.Add(tournament);
        await _unitOfWork.CompleteAsync();
        var tournamentOut = _mapper.Map<TournamentWithIdDTO>(tournament);
        return CreatedAtAction(nameof(GetTournamentById), new { id = tournamentOut.Id }, tournamentOut);
    }

    // DELETE: api/Tournaments/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTournament([FromRoute] int id)
    {
        var tournament = await _repository.GetAsync(id, includeGames: false);
        if (tournament is null)
            return NotFound();

        _repository.Remove(tournament);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<TournamentWithIdDTO>> PatchTournament(
        [FromRoute] int id,
        [FromBody] JsonPatchDocument<TournamentPostDTO> patchForDTO)
    {
        var tournament = await _repository.GetAsync(id, includeGames: false);
        if (tournament is null)
            return NotFound();


        var patch = _mapper.Map<JsonPatchDocument<Tournament>>(patchForDTO);
        patch.ApplyTo(tournament, ModelState);
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _repository.Update(tournament);
        await _unitOfWork.CompleteAsync();
        return new ObjectResult(_mapper.Map<TournamentWithIdDTO>(tournament));
    }

    private async Task<bool> TournamentExists(int id) =>
        await _repository.AnyAsync(id);
}
