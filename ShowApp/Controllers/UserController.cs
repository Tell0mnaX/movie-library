using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShowApp.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualBasic;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ShowContext _context;

    public UserController(ShowContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = _context.Users.ToListAsync();
        return Ok(users.Result);
    }
}