using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemoHangfire.Data.Implementations;
using WebDemoHangfire.Models;

namespace WebDemoHangfire.Data.Interfaces
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
            :base(context)
        {

        }
    }
}
