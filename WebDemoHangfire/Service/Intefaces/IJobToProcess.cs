using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemoHangfire.Service.Intefaces
{
    public interface IJobToProcess
    {
        int CallMethod1(int a, int b);
        string CallMethod2(string message);
        string CallMethod3(string message);

    }
}
