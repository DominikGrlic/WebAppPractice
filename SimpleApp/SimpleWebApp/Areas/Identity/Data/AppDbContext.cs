using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SimpleWebApp.Areas.Identity.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        

        string adminRole = "Admin";
        string adminRoleId = "ed70f95f-f713-43d3-9af8-e0947be13d79";

        string userRole = "User";
        string userRoleId = "092ccefc-1e85-40df-888d-15bbcc180fce";

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole()
            {
                Id = adminRoleId,
                Name = adminRole,
                NormalizedName = adminRole.ToUpper()
                
            },
            new IdentityRole()
            {
                Id = userRoleId,
                Name = userRole,
                NormalizedName = userRole.ToUpper()
            });
        
        string admin = "admin@admin.com";
        string adminId = "d6b6e5a5-8fd9-465d-b043-ace7d30878f7";
        string adminPassword = "AdminPass123!";
        var hasher = new PasswordHasher<AppUser>();

        builder.Entity<AppUser>().HasData(
            new AppUser()
            {
                Id = adminId,
                UserName = admin,
                NormalizedUserName = admin.ToUpper(),
                Email = admin,
                NormalizedEmail = admin.ToUpper(),
                PasswordHash = hasher.HashPassword(null, adminPassword),
                EmailConfirmed = true
            });

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>()
            {
                UserId = adminId,
                RoleId = adminRoleId
            });
        
        
    }
}
