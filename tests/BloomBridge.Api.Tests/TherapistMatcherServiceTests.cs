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
						// no match
						new Therapist
						{
								Id = 1,
								Name = "Albus",
								Fields = new List<TherapistField>
								{
										new TherapistField
										{
												Id = 1,
												TherapistId = 1,
												PredefinedNeedId = Need3.Id,
												PredefinedNeed = Need3
										},
								},
								Capacity = 3,
								CurrentClients = 1
						},
						// full match but full capacity
						new Therapist
						{
								Id = 2,
								Name = "Severus",
								Fields = new List<TherapistField> {
									new TherapistField
										{
												Id = 2,
												TherapistId = 2,
												PredefinedNeedId = Need1.Id,
												PredefinedNeed = Need1
										},
										new TherapistField
										{
												Id = 3,
												TherapistId = 2,
												PredefinedNeedId = Need2.Id,
												PredefinedNeed = Need2
										}
								},
								Capacity = 2,
								CurrentClients = 2
						},
						// full match with capacity
						new Therapist
						{
								Id = 3,
								Name = "Rubeus",
								Fields = new List<TherapistField> {
										new TherapistField
										{
												Id = 4,
												TherapistId = 3,
												PredefinedNeedId = Need1.Id,
												PredefinedNeed = Need1
										},
										new TherapistField
										{
												Id = 5,
												TherapistId = 3,
												PredefinedNeedId = Need2.Id,
												PredefinedNeed = Need2
										}
								},
								Capacity = 2,
								CurrentClients = 0
						},
						// partial match with capacity
						new Therapist
						{
								Id = 4,
								Name = "Minerva",
								Fields = new List<TherapistField> {
										new TherapistField
										{
												Id = 6,
												TherapistId = 4,
												PredefinedNeedId = Need2.Id,
												PredefinedNeed = Need2
										},
										new TherapistField
										{
												Id = 7,
												TherapistId = 4,
												PredefinedNeedId = Need4.Id,
												PredefinedNeed = Need4
										}
								},
								Capacity = 3,
								CurrentClients = 1
						}
				};
		}
	}
}