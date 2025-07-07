using System.Data.Common;
using System.Reflection.Emit;
using BloomBridge.Api.Models;

public class TherapistMatcherServiceTests
{
	[Fact]
	public void MatchUserToTherapist_ShouldReturnBestMatchIfFound()
	{
		var service = new TherapistMatcherService();

		// ACT
		var result = service.MatchUserToTherapist(TestValues.User, TestValues.Therapists);

		// ASSERT
		Assert.NotNull(result.Therapist);
		Assert.Equal("Rubeus", result.Therapist!.Name);
		Assert.Equal(2, result.Score);
	}

	[Fact]
	public void MatchUserToTherapist_ShouldReturnNullIfNoMatchFound()
	{
		// ARRANGE
		var therapists = new List<Therapist>
		{
			TestValues.Therapists[0], // no match
			TestValues.Therapists[1], // perfect match but full capacity
		};

		var service = new TherapistMatcherService();

		// ACT
		var result = service.MatchUserToTherapist(TestValues.User, therapists);

		// ASSERT
		Assert.Null(result.Therapist);
		Assert.Equal(0, result.Score);
		Assert.Equal("No therapist available", result.Message);
	}

	private static Therapist CreateTherapist(int id, string name, int capacity, int currentClients, params PredefinedNeed[] needs)
	{
		return new Therapist
		{
			Id = id,
			Name = name,
			Capacity = capacity,
			CurrentClients = currentClients,
			Fields = needs.Select((need, index) => new TherapistField
			{
				Id = 100 + id * 10 + index,
				TherapistId = id,
				PredefinedNeedId = need.Id,
				PredefinedNeed = need
			}).ToList()
		};
	}

	public static class TestValues
	{
		public static readonly PredefinedNeed Need1 = new PredefinedNeed { Id = 1, Label = "Anxiety" };
		public static readonly PredefinedNeed Need2 = new PredefinedNeed { Id = 2, Label = "Grief" };
		public static readonly PredefinedNeed Need3 = new PredefinedNeed { Id = 3, Label = "Nutrition" };
		public static readonly PredefinedNeed Need4 = new PredefinedNeed { Id = 4, Label = "Anger" };
		public static readonly User User;
		public static readonly List<Therapist> Therapists;

		static TestValues()
		{
			User = new User
			{
				Id = 1,
				Name = "Harry",
				UserPredefinedNeeds = new List<UserPredefinedNeed>()
			};

			// Add UserPredefinedNeeds and set User navigation property correctly
			User.UserPredefinedNeeds.AddRange(new[]
			{
						new UserPredefinedNeed {
								Id = 1,
								PredefinedNeedId = Need1.Id,
								PredefinedNeed = Need1,
								User = User
						},
						new UserPredefinedNeed {
								Id = 2,
								PredefinedNeedId = Need2.Id,
								PredefinedNeed = Need2,
								User = User
						}
				});

			Therapists = new List<Therapist>
			{
				CreateTherapist(1, "Albus", 3, 1, Need3), // no match
				CreateTherapist(2, "Severus", 2, 2, Need1, Need2), // full match but full capacity
				CreateTherapist(3, "Rubeus", 2, 0, Need1, Need2), // full match with capacity
				CreateTherapist(4, "Minerva", 3, 1, Need2, Need4) // partial match with capacity
			};
		}
	}
}