public class TherapistResponseDto
{
  public int Id { get; set; }
  public string Name { get; set; }
  public List<string> Expertise { get; set; } = new();
}