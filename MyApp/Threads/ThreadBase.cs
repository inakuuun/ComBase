using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Threads
{
    public abstract class ThreadBase
    {
        private Thread? _thread;

        public void ThreadStart()
        {
            _thread = new Thread(new ThreadStart(ThreadRun));
            _thread.Start();
        }

        protected abstract void ThreadRun();
    }
}
