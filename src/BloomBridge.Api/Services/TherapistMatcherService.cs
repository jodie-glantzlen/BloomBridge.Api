using BloomBridge.Api.Models;
using Microsoft.Extensions.ObjectPool;

public class TherapistMatcherService : ITherapistMatcherService
{
	public MatchResult MatchUserToTherapist(User user, List<Therapist> therapists)
	{
		int bestMatchScore = 0;
		Therapist? bestMatch = null;

		foreach (var therapist in therapists)
		{
			if (therapist.CurrentClients == therapist.Capacity)
			{
				// log therapist to keep track of who is full?
				continue;
			}

			int currentMatchScore = 0;

			foreach (var need in user.UserPredefinedNeeds)
			{
				if (therapist.Expertise.Any(e => e.Id == need.PredefinedNeed.Id))
				{
					currentMatchScore++;
				}
			}

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