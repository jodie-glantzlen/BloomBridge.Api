namespace BloomBridge.Api.Models;

public class User
{
	public int Id { get; set; }
	required public string Name { get; set; }
	public List<UserPredefinedNeed> UserPredefinedNeeds { get; set; } = new();
	public List<UserCustomNeed> UserCustomNeeds { get; set; } = new();
}