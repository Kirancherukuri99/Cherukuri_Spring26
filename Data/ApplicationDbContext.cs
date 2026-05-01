using Cherukuri_Spring26.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cherukuri_Spring26.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Person> People { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyOwner> PropertyOwners { get; set; }
        public DbSet<LeaseAgreement> LeaseAgreements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TPT Inheritance - Owner and Tenant get their own tables
            modelBuilder.Entity<Owner>().ToTable("Owners");
            modelBuilder.Entity<Tenant>().ToTable("Tenants");

            // Each Person links to one Identity user account via UserID
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.UserID)
                .IsUnique();

            // Email must be unique across all people
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.Email)
                .IsUnique();

            // PropertyOwner bridge table
            modelBuilder.Entity<PropertyOwner>()
                .HasOne(po => po.Property)
                .WithMany(p => p.PropertyOwners)
                .HasForeignKey(po => po.PropertyID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PropertyOwner>()
                .HasOne(po => po.Owner)
                .WithMany(o => o.PropertyOwners)
                .HasForeignKey(po => po.OwnerID)
                .OnDelete(DeleteBehavior.Restrict);

            // LeaseAgreement -> Property
            modelBuilder.Entity<LeaseAgreement>()
                .HasOne(la => la.Property)
                .WithMany(p => p.LeaseAgreements)
                .HasForeignKey(la => la.PropertyID)
                .OnDelete(DeleteBehavior.Restrict);

            // Tenant -> LeaseAgreement
            modelBuilder.Entity<Tenant>()
                .HasOne(t => t.LeaseAgreement)
                .WithMany(la => la.Tenants)
                .HasForeignKey(t => t.LeaseID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}