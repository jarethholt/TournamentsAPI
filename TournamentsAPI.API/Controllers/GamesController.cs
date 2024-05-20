using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentsAPI.Core.DTOs;
using TournamentsAPI.Core.Entities;
using TournamentsAPI.Core.Repositories;

namespace TournamentsAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    // GET: api/Games
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameWithIdDTO>>> GetAllGames() =>
        Ok(_mapper.Map<IEnumerable<GameWithIdDTO>>(await _unitOfWork.GameRepository.GetAllAsync()));

    // GET: api/Games/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GameWithIdDTO>> GetGameById(int id)
    {
        var game = await _unitOfWork.GameRepository.GetAsync(id);
        if (game is null)
            return NotFound();
        return Ok(_mapper.Map<GameWithIdDTO>(game));
    }

    // PUT: api/Games/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGame(int id, GamePostDTO gameDTO)
    {
        if (!(await GameExists(id)))
            return NotFound();

        _unitOfWork.GameRepository.Update(_mapper.Map<Game>(gameDTO));

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
    public async Task<ActionResult<GameWithIdDTO>> PostGame(GamePostDTO gameDTO)
    {
        var game = _mapper.Map<Game>(gameDTO);
        _unitOfWork.GameRepository.Add(game);
        await _unitOfWork.CompleteAsync();
        var gameOut = _mapper.Map<GameWithIdDTO>(game);
        return CreatedAtAction(nameof(GetGameById), new { id = gameOut.Id }, gameOut);
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
