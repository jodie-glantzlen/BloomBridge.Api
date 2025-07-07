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

		return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user.ToUserResponseDto()); // HTTP 201 Created
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<UserResponseDto>> GetUser(int id)
	{
		var user = await _db.Users
			.Include(u => u.UserPredefinedNeeds)
				.ThenInclude(upn => upn.PredefinedNeed)
			.FirstOrDefaultAsync(u => u.Id == id);

		if (user == null) return NotFound();
		return Ok(user.ToUserResponseDto());
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
	{
		var users = await _db.Users
			.Include(u => u.UserPredefinedNeeds)
				.ThenInclude(upn => upn.PredefinedNeed)
			.ToListAsync();
		return Ok(users.ToUserResponseDtos());
	}
}

// TODO
// add validation for user creation (e.g., unique names, required fields)
// implement user update and delete endpoints 
// add try/catch error handling
