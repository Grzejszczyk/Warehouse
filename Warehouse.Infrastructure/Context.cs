using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Models.Common;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public Context(DbContextOptions options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Structure> Structures { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ItemStructure> ItemStructure { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }
        public DbSet<MagazineZone> MagazineZones { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ItemStructure>()
                .HasKey(t => new { t.ItemId, t.StructureId });
            builder.Entity<ItemStructure>()
                .HasOne<Item>(itS => itS.Item)
                .WithMany(i => i.ItemStructures)
                .HasForeignKey(itS => itS.ItemId);
            builder.Entity<ItemStructure>()
                .HasOne<Structure>(itS => itS.Structure)
                .WithMany(s => s.ItemStructures)
                .HasForeignKey(itS => itS.StructureId);
        }

        public int SaveChanges(string userId)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is AuditableModelForEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((AuditableModelForEntity)entityEntry.Entity).ModifiedById = userId;
                ((AuditableModelForEntity)entityEntry.Entity).ModifiedDateTime = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditableModelForEntity)entityEntry.Entity).CreatedById = userId;
                    ((AuditableModelForEntity)entityEntry.Entity).CreatedDateTime = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
}
