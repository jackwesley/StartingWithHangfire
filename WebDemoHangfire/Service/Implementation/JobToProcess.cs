using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemoHangfire.Service.Intefaces;

namespace WebDemoHangfire.Service.Implementation
{
    public class JobToProcess : IJobToProcess
    {
        public int CallMethod1(int a, int b)
        {
            return a + b;
        }

        public string CallMethod2(string message)
        {
            return ($"this is your message: {message} from CallMethod1");
        }

        public string CallMethod3(string message)
        {
            return ($"this is your message: {message} from CallMethod2");
        }
    }
}
