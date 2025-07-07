namespace BloomBridge.Api.Models;

public class Therapist
{
	public int Id { get; set; }
	required public string Name { get; set; }
	required public int Capacity { get; set; }
	required public int CurrentClients { get; set; }

	// Navigation properties
	public List<TherapistField> Fields { get; set; } = new();
}