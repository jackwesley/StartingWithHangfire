using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemoHangfire.Models;

namespace WebDemoHangfire.Data
{
    public class UserRepository : IDatabase<User>
    {
        private readonly UserDbContext _userDbContext;

        public UserRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public void Add(User user)
        {
            _userDbContext.Add(user);
            _userDbContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _userDbContext.Users.ToList();
        }

        public User GetById(string id)
        {
            return _userDbContext.Users.Find(Guid.Parse(id));
        }

        public void Remove(string id)
        {

            var userToRemove = _userDbContext.Users.Find(Guid.Parse(id));
            if (userToRemove != null)
            {
                _userDbContext.Users.Remove(userToRemove);
                _userDbContext.SaveChanges();
            }
        }

        public void Update(string id, User user)
        {
            _userDbContext.Update(user);
            _userDbContext.SaveChanges();
        }
    }
}
