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

		// Convert to response DTO
		var userResponse = new UserResponseDto
		{
			Id = user.Id,
			Name = user.Name
			// UserPredefinedNeeds and UserCustomNeeds will be empty lists (because new user)
		};

		return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
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
		var userResponse = MapToUserResponseDto(user);
		return Ok(userResponse);
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
	{
		var users = await _db.Users
			.Include(u => u.UserPredefinedNeeds)
				.ThenInclude(upn => upn.PredefinedNeed)
			.ToListAsync();

		// Convert each user to response DTO
		var userResponses = users.Select(MapToUserResponseDto).ToList();
		return Ok(userResponses);
	}

	private UserResponseDto MapToUserResponseDto(User user)
	{
		return new UserResponseDto
		{
			Id = user.Id,
			Name = user.Name,
			PredefinedNeeds = user.UserPredefinedNeeds.Select(upn => new PredefinedNeedResponseDto
			{
				Id = upn.Id,
				Label = upn.PredefinedNeed.Label
			}).ToList(),

			// UserCustomNeeds will be empty for now, as we haven't implemented custom needs yet
		};
	}
}
