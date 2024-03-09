
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{

    public int? ImageId { get; set; }
    public Image? Image { get; set; }

    public IEnumerable<Notification>? Notifications { get; } = Enumerable.Empty<Notification>();

}
