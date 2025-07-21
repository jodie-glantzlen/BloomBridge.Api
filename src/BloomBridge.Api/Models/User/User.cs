namespace BloomBridge.Api.Models;

public class User
{
  public int Id { get; set; }
  required public string Name { get; set; }

  // navigation properties
  public List<UserPredefinedNeed> UserPredefinedNeeds { get; set; } = new();
  public List<UserCustomNeed> UserCustomNeeds { get; set; } = new();
}

// TODO
// add email, password, and other user properties to make them unique
// in a galaxy far far away: implement user roles (user or therapist)