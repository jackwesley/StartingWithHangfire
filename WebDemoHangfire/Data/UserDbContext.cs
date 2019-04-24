using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemoHangfire.Models;

namespace WebDemoHangfire.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }


        public UserDbContext(DbContextOptions<UserDbContext> options)
            :base(options)
        {

        }
    }
}
