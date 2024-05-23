using AutoMapper;
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
public class GamesController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IGameRepository _repository = unitOfWork.GameRepository;
    const int MaxPageSize = 20;
    const int DefaultPageSize = 5;

    // GET: api/Games
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameWithIdDTO>>> GetAllGames(
        [FromQuery] bool sort = true,
        [FromQuery] int currentPage = 1,
        [FromQuery] int pageSize = DefaultPageSize)
    {
        currentPage = Math.Max(currentPage, 1);
        pageSize = Math.Clamp(pageSize, 1, MaxPageSize);

        var (games, paginationMetadata) = await _repository.GetAllAsync(sort, currentPage, pageSize);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        return Ok(_mapper.Map<IEnumerable<GameWithIdDTO>>(games));
    }

    [HttpGet("Tournament/{tournamentId:int}")]
    public async Task<ActionResult<IEnumerable<GameWithIdDTO>>> GetAllFromTournament(
        [FromRoute] int tournamentId,
        [FromQuery] bool sort = true,
        [FromQuery] int currentPage = 1,
        [FromQuery] int pageSize = DefaultPageSize)
    {
        currentPage = Math.Max(currentPage, 1);
        pageSize = Math.Clamp(pageSize, 1, MaxPageSize);

        var (games, paginationMetadata) = await _repository.GetAllFromTournament(tournamentId, sort, currentPage, pageSize);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        return Ok(_mapper.Map<IEnumerable<GameWithIdDTO>>(games));
    }

    // GET: api/Games/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GameWithIdDTO>> GetGameById([FromRoute] int id)
    {
        var game = await _repository.GetByIdAsync(id);
        if (game is null)
            return NotFound();
        return Ok(_mapper.Map<GameWithIdDTO>(game));
    }

    [HttpGet("{title}")]
    public async Task<ActionResult<GameWithIdDTO>> GetGameByTitle([FromRoute] string title)
    {
        var game = await _repository.GetByTitleAsync(title);
        if (game is null)
            return NotFound();
        return Ok(_mapper.Map<GameWithIdDTO>(game));
    }

    // PUT: api/Games/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGame([FromRoute] int id, [FromBody] GamePostDTO gameDTO)
    {
        if (!(await GameExists(id)))
            return NotFound();
        if (!(await TournamentExists(gameDTO.TournamentId)))
            return BadRequest($"Tournament with Id {gameDTO.TournamentId} not found");

        var game = _mapper.Map<Game>(gameDTO);
        game.Id = id;
        _repository.Update(game);

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
    public async Task<ActionResult<GameWithIdDTO>> PostGame([FromBody] GamePostDTO gameDTO)
    {
        if (!(await TournamentExists(gameDTO.TournamentId)))
            return BadRequest($"Tournament with Id {gameDTO.TournamentId} not found");

        var game = _mapper.Map<Game>(gameDTO);
        _repository.Add(game);
        await _unitOfWork.CompleteAsync();
        var gameOut = _mapper.Map<GameWithIdDTO>(game);
        return CreatedAtAction(nameof(GetGameById), new { id = gameOut.Id }, gameOut);
    }

    // DELETE: api/Games/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame([FromRoute] int id)
    {
        var game = await _repository.GetByIdAsync(id);
        if (game is null)
            return NotFound();

        _repository.Remove(game);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<GameWithIdDTO>> PatchGame([FromRoute] int id, [FromBody] JsonPatchDocument<GamePostDTO> patchForDTO)
    {
        var game = await _repository.GetByIdAsync(id);
        if (game is null)
            return NotFound();

        var patch = _mapper.Map<JsonPatchDocument<Game>>(patchForDTO);
        patch.ApplyTo(game, ModelState);
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (!(await TournamentExists(game.TournamentId)))
            return BadRequest($"Tournament with Id {game.TournamentId} not found");

        _repository.Update(game);
        await _unitOfWork.CompleteAsync();
        return new ObjectResult(_mapper.Map<GameWithIdDTO>(game));
    }

    private async Task<bool> GameExists(int id) =>
        await _repository.AnyAsync(id);

    private async Task<bool> TournamentExists(int id) =>
        await _unitOfWork.TournamentRepository.AnyAsync(id);
}
