using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemoHangfire.Data;
using WebDemoHangfire.Models;
using WebDemoHangfire.Service.Intefaces;

namespace WebDemoHangfire.Service.Implementation
{
    public class JobToProcess : IJobToProcess
    {
        private readonly IDatabase<User> _userRepository;
        public JobToProcess(IDatabase<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public void InsertUser(string message)
        {
            var id = Guid.NewGuid();
            var user = new User
            {
                Id = id,
                Name = $"{id} + {message}",
                Description = $"Usuario salvo pelo método: {message} às {DateTime.Now}"
            };

            _userRepository.Add(user);
        }
    }
}
