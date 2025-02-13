namespace VaquinhaOnline.Infrastructure.Seeds;

public static class DbInitializer
{
    public static async Task SeedAsync(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager)
    {
        var rolesCreated = await SeedRolesAsync(roleManager);
        if (rolesCreated)
        {
            await SeedAdminUserAsync(userManager);
        }
    }

    private static async Task<bool> SeedRolesAsync(RoleManager<AppRole> roleManager)
    {
        var roles = new List<string> { "Admin", "Investidor", "Empreendedor" };
        var success = true;

        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new AppRole(roleName);
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    success = false;
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating role {roleName}: {error.Description}");
                    }
                }
            }
        }

        return success;
    }

    private static async Task SeedAdminUserAsync(UserManager<AppUser> userManager)
    {
        const string adminEmail = "vaquinhaonline.ao@gmail.com";
        const string adminPassword = "Admin@123";

        var existingUser = await userManager.FindByEmailAsync(adminEmail);
        if (existingUser == null)
        {
            var adminUser = new AppUser
            {
                Id = Guid.NewGuid(),
                Name = "Administrador",
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                PhoneNumber = "934818736"
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        Console.WriteLine($"Error adding user to role: {error.Description}");
                    }
                }
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error creating user: {error.Description}");
                }
            }
        }
    }
}
