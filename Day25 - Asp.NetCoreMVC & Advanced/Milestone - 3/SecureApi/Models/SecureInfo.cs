namespace SecureApi.Models;

public class SecureInfo
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public string SecureInfoText { get; set; } = null!;
}
