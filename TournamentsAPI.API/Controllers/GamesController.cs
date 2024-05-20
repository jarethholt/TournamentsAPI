using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentsAPI.API.Data;
using TournamentsAPI.Core.Entities;
using TournamentsAPI.Core.Repositories;

namespace TournamentsAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    // GET: api/Games
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Game>>> GetAllGames() =>
        Ok(await _unitOfWork.GameRepository.GetAllAsync());

    // GET: api/Games/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Game>> GetGameById(int id)
    {
        var game = await _unitOfWork.GameRepository.GetAsync(id);
        if (game is null)
            return NotFound();
        return Ok(game);
    }

    // PUT: api/Games/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGame(int id, Game game)
    {
        if (id != game.Id)
            return BadRequest();

        _unitOfWork.GameRepository.Update(game);

        try
        {
            await _unitOfWork.CompleteAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(await GameExists(id)))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // POST: api/Games
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Game>> PostGame(Game game)
    {
        _unitOfWork.GameRepository.Add(game);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
    }

    // DELETE: api/Games/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(int id)
    {
        var game = await _unitOfWork.GameRepository.GetAsync(id);
        if (game is null)
            return NotFound();

        _unitOfWork.GameRepository.Remove(game);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }

    private async Task<bool> GameExists(int id) =>
        await _unitOfWork.GameRepository.AnyAsync(id);
}
