using BloomBridge.Api.Models;

public class MatchResult
{
    public Therapist? Therapist { get; set; }
    public int Score { get; set; }
    required public string Message { get; set; }
}