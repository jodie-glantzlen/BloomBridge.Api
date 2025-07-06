// OUTPUT DTO (API -> Client)
// returning user data from GET endpoints

public class UserResponseDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public List<PredefinedNeedResponseDto> PredefinedNeeds { get; set; } = new();

	// TODO
	//public List<UserCustomNeedResponseDto> UserCustomNeeds { get; set; } = new();
}