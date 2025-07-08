using BloomBridge.Api.Models;

public class TherapistMatcherService : ITherapistMatcherService
{
	public MatchResult MatchUserToTherapist(User user, List<Therapist> therapists)
	{
		// declare scorekeepers
		int bestMatchScore = 0;
		Therapist? bestMatch = null;

		// iterate over therapists
		foreach (var therapist in therapists)
		{
			// if therapist doesn't have capacity, skip it
			if (therapist.CurrentClients == therapist.Capacity)
			{
				continue;
			}

			int currentMatchScore = 0;

			// iterate over the user's needs
			// count how many are in the current therapist's fields of expertise
			foreach (var need in user.UserPredefinedNeeds)
			{
				if (therapist.Fields.Any(field => field.PredefinedNeedId == need.PredefinedNeedId))
				{
					// if the therapist has expertise in this need, increment score
					currentMatchScore++;
				}
			}

			// if current score is better than best score, update scorekeeper vars
			if (currentMatchScore > bestMatchScore)
			{
				bestMatchScore = currentMatchScore;
				bestMatch = therapist;
			}
		}

		if (bestMatch is null)
		{
			return new MatchResult
			{
				Therapist = null,
				Score = 0,
				Message = "No therapist available"
			};
		}

		return new MatchResult
		{
			Therapist = new TherapistResponseDto
			{
				Id = bestMatch.Id,
				Name = bestMatch.Name,
				Expertise = bestMatch.Fields.Select(f => f.PredefinedNeed.Label).ToList()
			},
			Score = bestMatchScore,
			Message = "Match found"
		};
	}
}

// TODO:
	// tie logic - if multiple therapists have the same score, return all of them
