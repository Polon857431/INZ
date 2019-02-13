using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using pz.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace pz.DAL
{
    public class PzInitializer : DropCreateDatabaseIfModelChanges<PzContext>
    {
        protected override void Seed(PzContext context)
        {
            /*var roleMenager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new PzContext()));
            var userMenager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new PzContext()));

            roleMenager.Create(new IdentityRole("Admin"));
            roleMenager.Create(new IdentityRole("User"));

            var user = new ApplicationUser { UserName = "admin@admin.pl" };
            string passwor = "Qazxsw.12";
            userMenager.Create(user, passwor);
            userMenager.AddToRole(user.Id, "Admin");
            context.SaveChanges();
            */
        }
    }
}