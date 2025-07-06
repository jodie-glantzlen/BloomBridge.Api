// INPUT DTO (Client -> API)

namespace BloomBridge.Api.DTOs;

using System.ComponentModel.DataAnnotations;

public class CreateUserDto
{
	[Required(ErrorMessage = "Name is required.")]
	[MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
	public string Name { get; set; }
}