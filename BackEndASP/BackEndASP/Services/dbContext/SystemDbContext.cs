
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;



    public class SystemDbContext : IdentityDbContext<User>
    {
        
        public SystemDbContext(DbContextOptions<SystemDbContext> option) : base(option) 
        {

        try
        {
            var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (databaseCreator != null)
            {
                if (!databaseCreator.CanConnect()) databaseCreator.Create();
                if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

        DbSet<Building> Buildings { get; set; }
        DbSet<College> Colleges { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<Owner> Owners { get; set; }
        DbSet<Property> Properties { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<User> Users { get; set; }

    [Obsolete]
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Image>()
            .HasOne(i => i.User)
            .WithOne(u => u.Image)
            .HasForeignKey<User>(u => u.ImageId) 
            .IsRequired(false);


        modelBuilder.Entity<Student>()
            .HasOne(s => s.College)
            .WithMany(c => c.Students)
            .HasForeignKey(s => s.CollegeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<Student>()
            .HasMany(s => s.PropertiesLiked)
            .WithMany(p => p.StudentsLiked);

        modelBuilder.Entity<Property>()
            .HasOne(p => p.Owner)
            .WithMany(o => o.Properties)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.ClientSetNull);




    }

}

