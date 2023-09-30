using MyApp.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    public class TestThread2 : ThreadManager
    {
        protected override bool RunInit()
        {
            return true;
        }
    }
}
