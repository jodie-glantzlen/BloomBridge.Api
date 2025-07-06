using BloomBridge.Api.Models;
namespace BloomBridge.Api.Extensions;

public static class UserExtensions
{
	public static UserResponseDto ToUserResponseDto(this User user)
	{
		if (user == null) return null;

		return new UserResponseDto
		{
			Id = user.Id,
			Name = user.Name,
			PredefinedNeeds = user.UserPredefinedNeeds?
				.Select(upn => new PredefinedNeedResponseDto
				{
					Id = upn.PredefinedNeed.Id,
					Label = upn.PredefinedNeed.Label
				}).ToList() ?? new List<PredefinedNeedResponseDto>()
			// UserCustomNeeds will be empty for now (not implemented yet)
		};
	}

	public static List<UserResponseDto> ToUserResponseDtos(this IEnumerable<User> users)
	{
		return users?.Select(u => u.ToUserResponseDto()).ToList() ?? new List<UserResponseDto>();
	}
}