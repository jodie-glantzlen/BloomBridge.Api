using Microsoft.AspNetCore.Mvc;
using BloomBridge.Api.Models;
using BloomBridge.Api.DTOs;
using BloomBridge.Api.Data;
using BloomBridge.Api.Extensions;
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

		return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user.ToUserResponseDto());
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<UserResponseDto>> GetUser(int id)
	{
		var user = await _db.Users
			.Include(u => u.UserPredefinedNeeds)
				.ThenInclude(upn => upn.PredefinedNeed)
			.FirstOrDefaultAsync(u => u.Id == id);

		if (user == null) return NotFound();

		// Convert to response DTO
		return Ok(user.ToUserResponseDto());
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
	{
		var users = await _db.Users
			.Include(u => u.UserPredefinedNeeds)
				.ThenInclude(upn => upn.PredefinedNeed)
			.ToListAsync();

		// Convert each user to response DTO
		return Ok(users.ToUserResponseDtos());
	}

}
