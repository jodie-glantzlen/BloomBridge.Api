using BloomBridge.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PredefinedNeedsController : ControllerBase
{
	private readonly AppDbContext _db;

	public PredefinedNeedsController(AppDbContext db)
	{
		_db = db;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<PredefinedNeedResponseDto>>> GetAllPredefinedNeeds()
	{
		var needs = await _db.PredefinedNeeds
		.Select(n => new PredefinedNeedResponseDto
		{
			Id = n.Id,
			Label = n.Label
		})
		.ToListAsync();
		return Ok(needs);
	}
}