using BloomBridge.Api.Models;

public interface ITherapistMatcherService
{
  MatchResult MatchUserToTherapist(User user, List<Therapist> therapists);
}