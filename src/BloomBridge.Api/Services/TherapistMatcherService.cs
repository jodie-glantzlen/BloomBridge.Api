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
			// count how many are in the current therapist's expertise 
			foreach (var need in user.UserPredefinedNeeds)
			{
				if (therapist.Expertise.Any(e => e.Id == need.PredefinedNeed.Id))
				{
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
			Therapist = bestMatch,
			Score = bestMatchScore,
			Message = "Match found"
		};

	}
}