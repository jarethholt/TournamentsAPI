﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    // GET: api/Tournaments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TournamentWithIdDTO>>> GetAllTournaments() =>
        Ok(_mapper.Map<IEnumerable<TournamentWithIdDTO>>(await _unitOfWork.TournamentRepository.GetAllAsync()));

    // GET: api/Tournaments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TournamentWithIdDTO>> GetTournamentById(int id)
    {
        var tournament = await _unitOfWork.TournamentRepository.GetAsync(id);

        if (tournament is null)
            return NotFound();
        return Ok(_mapper.Map<TournamentWithIdDTO>(tournament));
    }

    // PUT: api/Tournaments/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTournament(int id, TournamentPostDTO tournamentDTO)
    {
        if (!(await TournamentExists(id)))
            return NotFound();

        _unitOfWork.TournamentRepository.Update(_mapper.Map<Tournament>(tournamentDTO));

        try
        {
            await _unitOfWork.CompleteAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(await TournamentExists(id)))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // POST: api/Tournaments
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TournamentPostDTO>> PostTournament(TournamentPostDTO tournamentDTO)
    {
        var tournament = _mapper.Map<Tournament>(tournamentDTO);
        _unitOfWork.TournamentRepository.Add(tournament);
        await _unitOfWork.CompleteAsync();
        var tournamentOut = _mapper.Map<TournamentWithIdDTO>(tournament);
        return CreatedAtAction(nameof(GetTournamentById), new { id = tournamentOut.Id }, tournamentOut);
    }

    // DELETE: api/Tournaments/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTournament(int id)
    {
        var tournament = await _unitOfWork.TournamentRepository.GetAsync(id);
        if (tournament is null)
            return NotFound();

        _unitOfWork.TournamentRepository.Remove(tournament);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }

    private async Task<bool> TournamentExists(int id) =>
        await _unitOfWork.TournamentRepository.AnyAsync(id);
}
