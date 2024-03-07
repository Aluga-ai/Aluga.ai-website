using Microsoft.AspNetCore.Identity;

namespace TesteBackEndClasses.Entities
{
    public class User : IdentityUser
    {

        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        public IEnumerable<Notification>? Notifications { get;} = Enumerable.Empty<Notification>();

    }
}
