
using BackEndASP.Entities;
using Microsoft.AspNetCore.Identity;
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

        public DbSet<Building> Buildings { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PropertyStudent> StudentProperties { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }


    [Obsolete]
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "Student", NormalizedName = "STUDENT" },
            new IdentityRole { Id = "3", Name = "Owner", NormalizedName = "OWNER" }
        );

        
        //modelBuilder.Entity<College>().ToTable("Colleges");
        //modelBuilder.Entity<Property>().ToTable("Properties");
        //modelBuilder.Entity<Owner>().ToTable("Owners");
        //modelBuilder.Entity<Student>().ToTable("Students");


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

        modelBuilder.Entity<Property>()
            .HasOne(p => p.Owner)
            .WithMany(o => o.Properties)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<Owner>()
            .HasMany(o => o.Properties)
            .WithOne(p => p.Owner)
            .OnDelete(DeleteBehavior.ClientCascade);


        modelBuilder.Entity<PropertyStudent>()
            .HasKey(sp => new { sp.StudentId, sp.PropertyId });

        modelBuilder.Entity<PropertyStudent>()
            .HasOne(sp => sp.Student)
            .WithMany(s => s.StudentProperties)
            .HasForeignKey(sp => sp.StudentId);

        modelBuilder.Entity<PropertyStudent>()
            .HasOne(sp => sp.Property)
            .WithMany(p => p.StudentProperties)
            .HasForeignKey(sp => sp.PropertyId);


        modelBuilder.Entity<UserConnection>()
            .HasKey(uc => new { uc.StudentId, uc.OtherStudentId });

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Connections)
            .WithOne(c => c.Student)
            .HasForeignKey(c => c.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        //
        modelBuilder.Entity<UserConnection>()
            .HasOne(uc => uc.OtherStudent)
            .WithMany()
            .HasForeignKey(uc => uc.OtherStudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}

