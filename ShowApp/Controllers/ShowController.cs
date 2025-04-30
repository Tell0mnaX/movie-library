using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShowApp.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualBasic;

[ApiController]
[Route("api/[controller]")]
public class ShowController : ControllerBase
{
    private readonly ShowContext _context;

    public ShowController(ShowContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShowDto>>> GetShows()
    {
        var shows = await _context.Shows
        .Include(s => s.User)
        .ToListAsync();

        var showsDto = shows.Select(s => new ShowDto
        {
            Id = s.Id,
            Title = s.Title,
            Creator = s.Creator,
            Genre = s.Genre,
            Seasons = s.Seasons,
            StartYear = s.StartYear,
            Rating = s.Rating,
            ImageUrl = s.ImageUrl,
            UserId = s.UserId,
            Username = s.User != null ? s.User.Username : "Inconnu"
        }).ToList();

        return Ok(showsDto);
    }

    [HttpGet("userShows/{userId}")]
    public async Task<ActionResult<IEnumerable<ShowDto>>> GetShowsByUserId(int userId)
    {
        return await _context.Shows
        .Include(s => s.User)
        .Select(s => new ShowDto
        {
            Id = s.Id,
            Title = s.Title,
            Creator = s.Creator,
            Seasons = s.Seasons,
            Genre = s.Genre,
            StartYear = s.StartYear,
            Rating = s.Rating,
            ImageUrl = s.ImageUrl,
            UserId = s.UserId,
            Username = s.User.Username
        })
        .Where(s => s.UserId == userId)
        .ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<ShowDto>>> GetShow(int id)
    {
        var show = await _context.Shows
        .Include(s => s.User)
        .Where(s => s.Id == id)
        .Select(s => new ShowDto
        {
            Id = s.Id,
            Title = s.Title,
            Creator = s.Creator,
            Seasons = s.Seasons,
            Genre = s.Genre,
            StartYear = s.StartYear,
            Rating = s.Rating,
            ImageUrl = s.ImageUrl,
            UserId = s.UserId,
            Username = s.User.Username
        })
        .FirstOrDefaultAsync();
        return show == null ? NotFound() : Ok(show);
    }

    [HttpGet("sort/year")]
    public async Task<ActionResult<IEnumerable<ShowDto>>> GetShowsByYear()
    {
        var shows = await _context.Shows
        .Include(s => s.User)
        .Select(s => new ShowDto
        {
            Id = s.Id,
            Title = s.Title,
            Creator = s.Creator,
            Seasons = s.Seasons,
            Genre = s.Genre,
            StartYear = s.StartYear,
            Rating = s.Rating,
            ImageUrl = s.ImageUrl,
            UserId = s.UserId,
            Username = s.User.Username
        })
        .OrderByDescending(s => s.StartYear)
        .ToListAsync();

        return shows.Count == 0 ? NotFound() : Ok(shows);
    }

    [HttpGet("search/{keyword}")]
    public async Task<ActionResult<IEnumerable<ShowDto>>> SearchShowsWithKeyword(string keyword)
    {

        var shows = await _context.Shows
        .Include(s => s.User)
        .Select(s => new ShowDto
        {
            Id = s.Id,
            Title = s.Title,
            Creator = s.Creator,
            Seasons = s.Seasons,
            Genre = s.Genre,
            StartYear = s.StartYear,
            Rating = s.Rating,
            ImageUrl = s.ImageUrl,
            UserId = s.UserId,
            Username = s.User.Username
        })
        .Where(s => s.Title.ToLower().Contains(keyword.ToLower()))
        .ToListAsync();

        return shows.Count == 0 ? NotFound() : Ok(shows);
    }

    [HttpGet("genre/{genre}")]
    public async Task<ActionResult<IEnumerable<ShowDto>>> SearchShowsWithGenre(string genre)
    {

        var shows = await _context.Shows
        .Include(s => s.User)
        .Select(s => new ShowDto
        {
            Id = s.Id,
            Title = s.Title,
            Creator = s.Creator,
            Seasons = s.Seasons,
            Genre = s.Genre,
            StartYear = s.StartYear,
            Rating = s.Rating,
            ImageUrl = s.ImageUrl,
            UserId = s.UserId,
            Username = s.User.Username
        })
        .Where(s => s.Genre == genre)
        .ToListAsync();

        return shows.Count == 0 ? shows : Ok(shows);
    }

    [HttpGet("top5")]
    public async Task<ActionResult<IEnumerable<ShowDto>>> GetTop5Shows()
    {

        var shows = await _context.Shows
        .Include(s => s.User)
        .Select(s => new ShowDto
        {
            Id = s.Id,
            Title = s.Title,
            Creator = s.Creator,
            Seasons = s.Seasons,
            Genre = s.Genre,
            StartYear = s.StartYear,
            Rating = s.Rating,
            ImageUrl = s.ImageUrl,
            UserId = s.UserId,
            Username = s.User.Username
        })
        .OrderByDescending(s => s.Rating)
        .Take(5)
        .ToListAsync();

        return shows.Count == 0 ? shows : Ok(shows);
    }

    [HttpPost]
    public async Task<ActionResult<ShowDto>> AddShow([FromBody] CreateShowDto newShow)
    {
        var show = new Show
        {
            Title = newShow.Title,
            Creator = newShow.Creator,
            Genre = newShow.Genre,
            Seasons = newShow.Seasons,
            StartYear = newShow.StartYear,
            Rating = newShow.Rating,
            ImageUrl = newShow.ImageUrl,
            UserId = newShow.UserId,
        };

        _context.Shows.Add(show);
        await _context.SaveChangesAsync();

        var showDto = new ShowDto
        {
            Id = show.Id,
            Title = show.Title,
            Creator = show.Creator,
            Genre = show.Genre,
            Seasons = show.Seasons,
            StartYear = show.StartYear,
            Rating = show.Rating,
            ImageUrl = show.ImageUrl,
            UserId = show.UserId,
            Username = (await _context.Users.FindAsync(show.UserId))?.Username
        };

        return CreatedAtAction(nameof(GetShow), new { id = show.Id }, showDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<IEnumerable<string>>> RemoveShow(int id)
    {
        var show = await _context.Shows.FindAsync(id);
        if (show == null) 
            return NotFound($"La série d'ID {id} n'existe pas...");

        _context.Shows.Remove(show);
        await _context.SaveChangesAsync();
        return Ok(new { message = $"La série {show.Title} a bien été supprimée"});
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShow(int id, [FromBody] UpdateShowDto updatedShow)
    {
        var show = await _context.Shows.FindAsync(id);
        if (show == null)
            return NotFound($"La série d'ID {id} est introuvable.");

        // Mise à jour des champs
        show.Title = updatedShow.Title;
        show.Creator = updatedShow.Creator;
        show.Genre = updatedShow.Genre;
        show.Seasons = updatedShow.Seasons;
        show.StartYear = updatedShow.StartYear;
        show.Rating = updatedShow.Rating;
        show.ImageUrl = updatedShow.ImageUrl;
        show.UserId = updatedShow.UserId;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}