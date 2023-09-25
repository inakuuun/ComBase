using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Threads;

namespace MyApp
{
    public class TestThread : ThreadManager
    {
        protected override bool RunInit()
        {
            return true;
        }
    }
}
