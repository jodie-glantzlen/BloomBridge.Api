using Microsoft.EntityFrameworkCore;
using BloomBridge.Api.Models;

namespace BloomBridge.Api.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
				: base(options) { }

		public DbSet<User> Users => Set<User>();
		public DbSet<PredefinedNeed> PredefinedNeeds => Set<PredefinedNeed>();
		public DbSet<UserPredefinedNeed> UserPredefinedNeeds => Set<UserPredefinedNeed>();
		public DbSet<UserCustomNeed> UserCustomNeeds => Set<UserCustomNeed>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PredefinedNeed>().HasData(
					// Mental Health Core
					new PredefinedNeed { Id = 1, Label = "Managing anxiety" },
					new PredefinedNeed { Id = 2, Label = "Dealing with depression" },
					new PredefinedNeed { Id = 3, Label = "Processing grief and loss" },
					new PredefinedNeed { Id = 4, Label = "Coping with stress" },
					new PredefinedNeed { Id = 5, Label = "Building self-esteem" },
					new PredefinedNeed { Id = 6, Label = "Managing anger" },

					// Relationships & Social
					new PredefinedNeed { Id = 7, Label = "Improving relationships" },
					new PredefinedNeed { Id = 8, Label = "Setting boundaries" },
					new PredefinedNeed { Id = 9, Label = "Dealing with loneliness" },
					new PredefinedNeed { Id = 10, Label = "Family conflict resolution" },
					new PredefinedNeed { Id = 11, Label = "Social anxiety support" },

					// Lifestyle & Habits
					new PredefinedNeed { Id = 12, Label = "Better sleep habits" },
					new PredefinedNeed { Id = 13, Label = "Nutrition and eating habits" },
					new PredefinedNeed { Id = 14, Label = "Exercise motivation" },
					new PredefinedNeed { Id = 15, Label = "Substance use support" },
					new PredefinedNeed { Id = 16, Label = "Time management" },

					// Work & Life Balance
					new PredefinedNeed { Id = 17, Label = "Work-life balance" },
					new PredefinedNeed { Id = 18, Label = "Career guidance" },
					new PredefinedNeed { Id = 19, Label = "Burnout prevention" },
					new PredefinedNeed { Id = 20, Label = "Financial stress" },

					// Personal Growth
					new PredefinedNeed { Id = 21, Label = "Finding purpose and meaning" },
					new PredefinedNeed { Id = 22, Label = "Building confidence" },
					new PredefinedNeed { Id = 23, Label = "Mindfulness and meditation" },
					new PredefinedNeed { Id = 24, Label = "Emotional regulation" },
					new PredefinedNeed { Id = 25, Label = "Personal goal setting" }
			);
		}
	}
}