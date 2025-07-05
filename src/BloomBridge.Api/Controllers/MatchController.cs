using BloomBridge.Api.Data;
using BloomBridge.Api.Models;
using Microsoft.AspNetCore.Mvc;

public class MatchController : ControllerBase
{
	private readonly ITherapistMatcherService _therapistMatcherService;
	private readonly AppDbContext _db;

	public MatchController(AppDbContext db, ITherapistMatcherService therapistMatcherService)
	{
		_therapistMatcherService = therapistMatcherService;
		_db = db;
	}

	// do something weeee

};

// POST /api/match
	// Match a user to a therapist based on their needs
	// payload: {"userId": 1}
	// Fetch the user and their needs
	// Fetch all therapists
	// Use the ITherapistMatcherService to find the best match
	// Return MatchResult
	// triggered by a user action like clicking "Find Therapist" (FE)

// GET /matches
	// View all matches.

// GET /matches/ unmatched
	//(Optional) See users who couldn’t be matched

