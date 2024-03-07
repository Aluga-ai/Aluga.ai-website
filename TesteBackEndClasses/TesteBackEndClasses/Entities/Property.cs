using Microsoft.Identity.Client;

namespace TesteBackEndClasses.Entities
{
    public class Property : Building
    {
        
        public string OwnerId { get; set; }
        public Owner Owner { get; set; }

        public IEnumerable<User>? UsersLikes { get; } = new List<User>();

    }
}
