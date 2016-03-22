namespace OptionsWebsite.Migrations.IdentityMigrations
{
    using DiplomaDataModel.Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\IdentityMigrations";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var role_store = new RoleStore<IdentityRole>(context);
            var role_manager = new RoleManager<IdentityRole>(role_store);
            var user_store = new UserStore<ApplicationUser>(context);
            var user_manager = new UserManager<ApplicationUser>(user_store);

            // Seed database with custom roles
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                // Create Admin Role
                var admin = new IdentityRole { Name = "Admin" };
                role_manager.Create(admin);
            }
            if (!context.Roles.Any(r => r.Name == "Student"))
            {
                // Create Student Role
                var student = new IdentityRole { Name = "Student" };
                role_manager.Create(student);
            }

            // Seed database with custom users
            if (!context.Users.Any(u => u.UserName == "A00111111")
            && !context.Users.Any(u => u.Email == "a@a.a"))
            {
                // Create Admin Account
                var admin = new ApplicationUser
                {
                    UserName = "A00111111",
                    Email = "a@a.a"
                };
                user_manager.Create(admin, "P@$$w0rd");
                user_manager.AddToRole(admin.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "A00222222")
            && !context.Users.Any(u => u.Email == "s@s.s"))
            {
                // Create Student Account
                var student = new ApplicationUser
                {
                    UserName = "A00222222",
                    Email = "s@s.s"
                };
                user_manager.Create(student, "P@$$w0rd");
                user_manager.AddToRole(student.Id, "Student");
            }
        }
    }
}
