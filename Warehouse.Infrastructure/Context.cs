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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Structure> Structures { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ItemStructure> ItemStructure { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Item>()
            //    .HasOne(a => a.Category).WithMany(b => b.Items);

            //builder.Entity<CheckIn>()
            //    .HasMany(i => i.Items).WithOne();
            //builder.Entity<CheckOut>()
            //    .HasMany(i => i.Items).WithOne();

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
        //NEW SaveChanges:
        //TODO: Sprawdzić tę metodę step by step
        public int SaveChanges(string userId)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).ModifiedById = Users.FirstOrDefault(u => u.Id == userId).Id;
                ((BaseEntity)entityEntry.Entity).ModifiedDateTime = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).ModifiedById = Users.FirstOrDefault(u => u.Id == userId).Id;
                    ((BaseEntity)entityEntry.Entity).CreatedDateTime = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
}
