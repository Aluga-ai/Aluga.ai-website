
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

        // DOCKER APLICAR MIGRATIONS
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
        ///////

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
            new IdentityRole { Id = "3", Name = "Owner", NormalizedName = "OWNER" },
            new IdentityRole { Id = "2", Name = "Student", NormalizedName = "STUDENT" }
        );

        // Seed college
        modelBuilder.Entity<College>().HasData(
            new College
            {
                Id = 1,
                Address = "Rodovia Senador José Ermírio de Moraes",
                District = "Sorocaba",
                Name = "FACENS",
                State = "SP",
                HomeComplement = "",
                Neighborhood = "Iporanga",
                Number = "",
                Lat = "-23.4440154",
                Long = "-47.3860489"
            }
        );

        // Seed users
        var adminId = Guid.NewGuid().ToString();
        var ownerId = Guid.NewGuid().ToString();
        var studentId = Guid.NewGuid().ToString();

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminId,
                Email = "admin@gmail.com",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "Senha#123"),
                EmailConfirmed = true,
                PhoneNumber = "999999999",
                PhoneNumberConfirmed = true,
                NormalizedEmail = "ADMIN@GMAIL.COM"
            }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = ownerId,
                Email = "owner@gmail.com",
                UserName = "Owner",
                NormalizedUserName = "OWNER",
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "Senha#1233"), 
                EmailConfirmed = true,
                PhoneNumber = "999999999",
                PhoneNumberConfirmed = true,
                NormalizedEmail = "OWNER@GMAIL.COM"
            }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = studentId,
                Email = "student@gmail.com",
                UserName = "Student",
                NormalizedUserName = "STUDENT",
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "Senha#123"), 
                EmailConfirmed = true,
                PhoneNumber = "999999999",
                PhoneNumberConfirmed = true,
                NormalizedEmail = "STUDENT@GMAIL.COM"
            }
        );

        // Seed user roles
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = adminId, RoleId = "1" }, // Admin role
            new IdentityUserRole<string> { UserId = ownerId, RoleId = "3" }, // Owner role
            new IdentityUserRole<string> { UserId = studentId, RoleId = "2" } // Student role
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
            .OnDelete(DeleteBehavior.Restrict);

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

