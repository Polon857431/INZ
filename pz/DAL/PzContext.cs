using Microsoft.AspNet.Identity.EntityFramework;
using pz.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace pz.DAL
{
    public class PzContext : IdentityDbContext<ApplicationUser>

    {
        public PzContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new PzInitializer());
        }

        public static PzContext Create()
        {
            return new PzContext();
        }
        public DbSet<PointModel> Points { get; set; }
        public DbSet<EventModel> Events { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Spot> Spots { get; set;}
        public DbSet<MemberEvent> MemberEvents { get; set; }
        public DbSet<MemberSpots> MemberSpots { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}