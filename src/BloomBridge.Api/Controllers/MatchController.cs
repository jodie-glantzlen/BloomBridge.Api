using BloomBridge.Api.Data;
using BloomBridge.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class MatchController : ControllerBase
{
  private readonly ITherapistMatcherService _therapistMatcherService;
  private readonly AppDbContext _db;

  public MatchController(AppDbContext db, ITherapistMatcherService therapistMatcherService)
  {
    _therapistMatcherService = therapistMatcherService;
    _db = db;
  }

  [HttpPost]
  public async Task<ActionResult<MatchResult>> MatchUserToTherapist([FromBody] MatchRequestDto request)
  {
    // Get user with their needs
    var user = await _db.Users
        .Include(u => u.UserPredefinedNeeds)
            .ThenInclude(upn => upn.PredefinedNeed)
        .FirstOrDefaultAsync(u => u.Id == request.UserId);

    if (user == null)
    {
      return NotFound($"User with ID {request.UserId} not found.");
    }

    // Get all therapists with their fields
    var therapists = await _db.Therapists
        .Include(t => t.Fields)
            .ThenInclude(tf => tf.PredefinedNeed)
        .ToListAsync();

    if (therapists.Count == 0)
    {
      return NotFound("Error fetching therapists");
    }

    // Use the matcher service to find the best match
    var matchResult = _therapistMatcherService.MatchUserToTherapist(user, therapists);
    return Ok(matchResult);
  }
}
// triggered by a user action like clicking "Find Therapist" (FE)


// TODO
// GET /matches
// View all matches

// GET /matches/unmatched
//See users who couldn’t be matched
// implement waiting list system

