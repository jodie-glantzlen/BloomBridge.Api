using Microsoft.AspNetCore.Mvc;
using BloomBridge.Api.Models;
using BloomBridge.Api.DTOs;
using BloomBridge.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace BloomBridge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
	private readonly AppDbContext _db;

	public UsersController(AppDbContext db)
	{
		_db = db;
	}

	[HttpPost]
	public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserDto dto)
	{

		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var user = new User
		{
			Name = dto.Name
		};

		_db.Users.Add(user);
		await _db.SaveChangesAsync();

		return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<User>> GetUser(int id)
	{
		var user = await _db.Users.FindAsync(id);
		if (user == null) return NotFound();

		return Ok(user);
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<User>>> GetUsers()
	{
		var users = await _db.Users.ToListAsync();
		return Ok(users);
	}
}
