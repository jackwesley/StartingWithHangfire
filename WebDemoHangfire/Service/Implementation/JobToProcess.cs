using System;
using WebDemoHangfire.Data.Interfaces;
using WebDemoHangfire.Models;
using WebDemoHangfire.Service.Intefaces;

namespace WebDemoHangfire.Service.Implementation
{
    public class JobToProcess : IJobToProcess
    {
        private readonly IUserRepository _userRepository;
        public JobToProcess(IUserRepository userRepository)
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
