using Ispit.Todo.Models.Dbo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ispit.Todo.Data
{
    public class ApplicationDbContext : IdentityDbContext<AspNetUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<AspNetUser> AspNetUser { get; set; }
        public DbSet<TodoTask> TodoTask { get; set; }
        public DbSet<TodoList> TodoList { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var role = "user";
            var normalizedRole = role.ToUpper();
            var userName = "user@user.com";
            var normalizedUserName = userName.ToUpper();
            var roleId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = roleId,
                Name = role,
                NormalizedName = normalizedRole
            });

            var hasher = new PasswordHasher<AspNetUser>();

            builder.Entity<AspNetUser>().HasData(new AspNetUser
            {
                Id = userId,
                UserName = userName,
                NormalizedUserName = normalizedUserName,
                Email = userName,
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null!, "Password123!"),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                FirstName = "John",
                LastName = "Doe",
                
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = userId,
            });
        }

    }
}