using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



    public class SystemDbContext : IdentityDbContext<User>
    {
        
        public SystemDbContext(DbContextOptions<SystemDbContext> option) : base(option) { }

        

    }

