using Microsoft.EntityFrameworkCore;
using PruebaRedArborBryan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PruebaRedArborBryan.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Employee>().Property(e => e.CompanyId).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Email).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Password).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.PortalId).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.RoleId).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.StatusId).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Username).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Name).IsRequired();
        }
    }
}
