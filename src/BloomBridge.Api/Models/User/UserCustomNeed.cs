namespace BloomBridge.Api.Models;

public class UserCustomNeed
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public required string CustomText { get; set; }

  // navigation properties
  public required User User { get; set; }
}