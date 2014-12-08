namespace nmct.ssa.dropbox.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using nmct.ssa.dropbox.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<nmct.ssa.dropbox.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(nmct.ssa.dropbox.Models.ApplicationDbContext context)
        {
            string roleAdmin = "Administrator";
            string roleNormalUser = "User";
            IdentityResult roleResult;

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!RoleManager.RoleExists(roleNormalUser))
                roleResult = RoleManager.Create(new IdentityRole(roleNormalUser));

            if (!RoleManager.RoleExists(roleAdmin))
                roleResult = RoleManager.Create(new IdentityRole(roleAdmin));

            if (!context.Users.Any(u => u.Email.Equals("test@dev.null")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Doe",
                    FirstName = "John",
                    Email = "test@dev.null",
                    UserName = "test@dev.null",
                    Address = "Straatlaan 55",
                    City = "Brussel",
                    Zipcode = "5000",
                    TwitterName = "@example"
                };

                manager.Create(user, "Password");
                manager.AddToRole(user.Id, roleAdmin);
            }
        }
    }
}
