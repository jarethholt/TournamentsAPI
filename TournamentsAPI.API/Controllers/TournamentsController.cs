using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentsAPI.API.Data;
using TournamentsAPI.Core.Entities;
using TournamentsAPI.Core.Repositories;

namespace TournamentsAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TournamentsController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    // GET: api/Tournaments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tournament>>> GetAllTournaments() =>
        Ok(await _unitOfWork.TournamentRepository.GetAllAsync());

    // GET: api/Tournaments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Tournament>> GetTournamentById(int id)
    {
        var tournament = await _unitOfWork.TournamentRepository.GetAsync(id);

        if (tournament is null)
            return NotFound();
        return Ok(tournament);
    }

    // PUT: api/Tournaments/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTournament(int id, Tournament tournament)
    {
        if (id != tournament.Id)
            return BadRequest();

        _unitOfWork.TournamentRepository.Update(tournament);

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
    public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
    {
        _unitOfWork.TournamentRepository.Add(tournament);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetTournamentById), new { id = tournament.Id }, tournament);
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
