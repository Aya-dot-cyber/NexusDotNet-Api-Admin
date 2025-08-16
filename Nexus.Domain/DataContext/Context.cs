using Microsoft.EntityFrameworkCore;
using Nexus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Domain.DataContext
{
    public class Context : DbContext
    {

        public Context()
        {
            this.ChangeTracker.LazyLoadingEnabled = false;

        }
        public Context(DbContextOptions<Context> options)
          : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsDeleted && p.IsActive);

            modelBuilder.Entity<UserOTP>().HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Country>().HasQueryFilter(p => !p.IsDeleted &&!p.IsActive);
            modelBuilder.Entity<AppSetting>().HasQueryFilter(p => !p.IsDeleted);

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserOTP> UserOTPs { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<AppSetting> AppSettings { get; set; }
    }
}
