using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 

        }

        public ApplicationDbContext() { }

        public DbSet<AdditionalRole> AdditionalRoles { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>()
                .HasOne(rt => rt.ResourceType)
                .WithMany(r => r.Resources)
                .HasForeignKey(r => r.ResourceTypeId);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Resource)
                .WithMany(r => r.Schedules)
                .HasForeignKey(s => s.ResourceId);
            modelBuilder.Entity<Schedule>()
                .HasOne(req => req.User)
                .WithMany(u => u.Schedules)
                .HasForeignKey(req => req.UserId);

            modelBuilder.Entity<Request>()
                .HasOne(req => req.Resource)
                .WithMany(r => r.Requests)
                .HasForeignKey(req => req.ResourceId);
            modelBuilder.Entity<Request>()
                .HasOne(req => req.User)
                .WithMany(u => u.Requests)
                .HasForeignKey(req => req.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(ad => ad.Users)
                .HasForeignKey(u => u.RoleId);
        }
    }

}
