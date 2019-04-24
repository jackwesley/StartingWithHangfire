using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemoHangfire.Service.Intefaces
{
    public interface IJobToProcess
    {
        void InsertUser(string message);

    }
}
