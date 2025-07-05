namespace BloomBridge.Api.DTOs;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class AddUserNeedsDto
{
	[Required]
	public List<int> NeedIds { get; set; } = new List<int>();
}