using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StudentManagement.API.Models;

namespace StudentManagement.API.Data
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILogger? logger = null)
        {
            var adminEmail = configuration["Admin:Email"];
            var adminPassword = configuration["Admin:Password"];

            var hasEmail = !string.IsNullOrWhiteSpace(adminEmail);
            var hasPassword = !string.IsNullOrWhiteSpace(adminPassword);

            if (!hasEmail || !hasPassword)
            {
                logger?.LogWarning(
                    "Admin credentials check failed: Email present = {HasEmail}, Password present = {HasPassword}.",
                    hasEmail, hasPassword);

                throw new InvalidOperationException(
                    "Admin email or password is missing from User Secrets.");
            }

            logger?.LogInformation(
                "Admin credentials loaded successfully (Email is present, Password is present).");

            // Ensure Admin role exists
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                logger?.LogInformation("Admin role does not exist. Creating Admin role...");
                var roleResult = await roleManager.CreateAsync(
                    new IdentityRole("Admin"));

                if (!roleResult.Succeeded)
                {
                    var roleErrors = string.Join(
                        ", ",
                        roleResult.Errors.Select(error => error.Description));

                    logger?.LogError("Failed to create Admin role: {Errors}", roleErrors);

                    throw new InvalidOperationException(
                        $"Failed to create Admin role: {roleErrors}");
                }

                logger?.LogInformation("Admin role created successfully.");
            }
            else
            {
                logger?.LogInformation("Admin role exists.");
            }

            // Check whether Admin account already exists
            var admin = await userManager.FindByEmailAsync(adminEmail!);

            if (admin == null)
            {
                logger?.LogInformation(
                    "Admin account does not exist. Creating Admin account for {Email}...",
                    adminEmail);

                admin = new ApplicationUser
                {
                    Name = "System Admin",
                    UserName = adminEmail!,
                    Email = adminEmail!,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(
                    admin,
                    adminPassword!);

                if (!result.Succeeded)
                {
                    var errors = string.Join(
                        ", ",
                        result.Errors.Select(error => error.Description));

                    logger?.LogError(
                        "Admin account creation failed with Identity validation errors: {Errors}",
                        errors);

                    throw new InvalidOperationException(
                        $"Admin creation failed: {errors}");
                }

                logger?.LogInformation("Admin account creation succeeded.");
            }
            else
            {
                logger?.LogInformation(
                    "Admin account already exists for {Email}.",
                    adminEmail);
            }

            // Ensure Admin role is assigned
            if (!await userManager.IsInRoleAsync(admin, "Admin"))
            {
                logger?.LogInformation("Assigning Admin role to Admin account...");
                var roleAssignment = await userManager.AddToRoleAsync(
                    admin, "Admin");

                if (!roleAssignment.Succeeded)
                {
                    var roleErrors = string.Join(
                        ", ",
                        roleAssignment.Errors.Select(error => error.Description));

                    logger?.LogError(
                        "Failed to assign Admin role: {Errors}",
                        roleErrors);

                    throw new InvalidOperationException(
                        $"Failed to assign Admin role: {roleErrors}");
                }

                logger?.LogInformation("Admin role assignment succeeded.");
            }
            else
            {
                logger?.LogInformation("Admin account is already assigned to Admin role.");
            }
        }
    }
}
