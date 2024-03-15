
using BackEndASP.Entities;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{

    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public DateTimeOffset BirthDate { get; set; }

    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

    public int? ImageId { get; set; }
    public Image? Image { get; set; }

    public ICollection<UserNotifications>? UserNotifications { get; set; } = new List<UserNotifications>();

}
