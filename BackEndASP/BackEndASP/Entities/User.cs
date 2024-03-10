
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{

    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public int? ImageId { get; set; }
    public Image? Image { get; set; }

    public IEnumerable<Notification>? Notifications { get; } = Enumerable.Empty<Notification>();

}
