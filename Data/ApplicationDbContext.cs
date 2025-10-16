using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data
{
    public class ApplicationDbContext :DbContext
    {
       public DbSet<Activity> tasks {  get; set; }
        public DbSet<User> users {  get; set; }
        public DbSet<Project> projects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Project)
                .WithMany(p => p.Activity)
                .HasForeignKey(a => a.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(p=>p.Project)
                .HasForeignKey(a=>a.userId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

    }
}
