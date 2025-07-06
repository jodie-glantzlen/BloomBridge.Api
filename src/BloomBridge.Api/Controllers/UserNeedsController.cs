// POST /api/users/{userId}/needs
// payload: {"needIds": [1, 2, 3]}
// Fetch the valid PredefinedNeeds
// Create UserPredefinedNeed entries
// Optionally return a success message or the updated user info
// triggered by a user action like selecting needs from a list (FE)

namespace BloomBridge.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using BloomBridge.Api.Models;
using BloomBridge.Api.DTOs;
using BloomBridge.Api.Data;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/users/{userId}/predefinedneeds")]
public class UserNeedsController : ControllerBase
{
	private readonly AppDbContext _db;
	public UserNeedsController(AppDbContext db)
	{
		_db = db;
	}
	[HttpPost]
	public async Task<ActionResult> AddUserNeeds(int userId, [FromBody] AddUserPredefinedNeedsDto dto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var user = await _db.Users.FindAsync(userId);
		if (user == null)
		{
			return NotFound($"User with ID {userId} not found.");
		}

		var needs = await _db.PredefinedNeeds
			.Where(n => dto.PredefinedNeedIds.Contains(n.Id))
			.ToListAsync();

		if (needs.Count != dto.PredefinedNeedIds.Count)
		{
			return BadRequest("Some of the provided need IDs are invalid.");
		}

		foreach (var need in needs)
		{
			var userNeed = new UserPredefinedNeed
			{
				UserId = userId,
				PredefinedNeedId = need.Id,
				User = user,
				PredefinedNeed = need
			};
			await _db.UserPredefinedNeeds.AddAsync(userNeed);
		}

		await _db.SaveChangesAsync();

		return Ok(new { Message = "User needs added successfully." });
	}
}