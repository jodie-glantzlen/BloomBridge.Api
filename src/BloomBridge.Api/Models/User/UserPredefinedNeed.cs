namespace BloomBridge.Api.Models;

public class UserPredefinedNeed
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public int PredefinedNeedId { get; set; }

  // navigation properties
  public required User User { get; set; }
  public required PredefinedNeed PredefinedNeed { get; set; }
}