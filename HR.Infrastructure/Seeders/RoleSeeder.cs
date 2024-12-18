using HR.Domain.Classes.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Seeders
{
    public static class RoleSeeder
    {
        public static async Task Seed(RoleManager<Role> rolemanager)
        {
            var roleCount = await rolemanager.Roles.CountAsync();

            await rolemanager.CreateAsync(new Role()
            {
                Name = "Manager",
                NormalizedName = "MANAGER"
            });
            await rolemanager.CreateAsync(new Role()
            {
                Name = "User",
                NormalizedName = "USER"
            });
            await rolemanager.CreateAsync(new Role()
            {
                Name = "Hr",
                NormalizedName = "HR"
            });

        }
    }
}
