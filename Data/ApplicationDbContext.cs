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
       public DbSet<Tasks> tasks {  get; set; }
        public DbSet<User> users {  get; set; }  
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
