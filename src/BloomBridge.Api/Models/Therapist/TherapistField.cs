using BloomBridge.Api.Models;

public class TherapistField
{
		public int Id { get; set; }
		public int TherapistId { get; set; }
		public int PredefinedNeedId { get; set; }

		// navigation properties
		public Therapist Therapist { get; set; }
		public PredefinedNeed PredefinedNeed { get; set; }
}