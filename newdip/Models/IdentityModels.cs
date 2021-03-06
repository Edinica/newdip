﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace newdip.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PointM>()
                              .HasMany(m => m.EdgesIn)
                              .WithRequired(t => t.PointTo)
                              .HasForeignKey(m => m.PointToId)
                              .WillCascadeOnDelete(false);

            modelBuilder.Entity<PointM>()
                              .HasMany(m => m.EdgesOut)
                              .WithRequired(t => t.PointFrom)
                              .HasForeignKey(m => m.PointFromId)
                              .WillCascadeOnDelete(false);

        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public System.Data.Entity.DbSet<newdip.Models.Building> Buildings { get; set; }
        public System.Data.Entity.DbSet<newdip.Models.EdgeM> Edges { get; set; }
        public System.Data.Entity.DbSet<newdip.Models.Floor> Floors{ get; set; }
        public System.Data.Entity.DbSet<newdip.Models.Note> Notes { get; set; }
        public System.Data.Entity.DbSet<newdip.Models.PointM> Points { get; set; }
        public System.Data.Entity.DbSet<newdip.Models.Room> Rooms { get; set; }
        public System.Data.Entity.DbSet<newdip.Models.Worker> Workers { get; set; }
        public System.Data.Entity.DbSet<newdip.Models.Client> Clients { get; set; }

        public System.Data.Entity.DbSet<newdip.Models.FavoriteRoom> FavoriteRooms { get; set; }
    }
}