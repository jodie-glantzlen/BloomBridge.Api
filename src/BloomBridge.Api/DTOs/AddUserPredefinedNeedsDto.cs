// INPUT DTO (Client -> API)

namespace BloomBridge.Api.DTOs;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class AddUserPredefinedNeedsDto
{
  [Required]
  public List<int> PredefinedNeedIds { get; set; } = new List<int>();
}