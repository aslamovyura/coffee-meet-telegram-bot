﻿using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    /// <summary>
    /// Application context.
    /// </summary>
    public class ApplicationContext : DbContext, IApplicationContext
    {
        /// <summary>
        /// Users table.
        /// </summary>
        public DbSet<AppUser> AppUsers { get; set; }

        /// <summary>
        /// Define application context with parameters.
        /// </summary>
        /// <param name="options">Context options.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        /// <inheritdoc/>
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>(u =>
            {
                u.HasKey(a => a.Id);
            });
        }
    }
}