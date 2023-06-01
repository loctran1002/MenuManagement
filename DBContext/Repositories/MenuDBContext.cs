using System;
using System.Collections.Generic;
using System.Text;
using ManageMenu.Configuration;
using ManageMenu.Entities;
using ManageMenu.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ManageMenu.DBContext.Repositories
{
    public class MenuDBContext : DbContext
    {
        public MenuDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Menu> Menu { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MenuConfiguration());
        }
    }
}
