using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Threads
{
    public abstract class ThreadManager : ThreadBase
    {
        protected sealed override void ThreadRun()
        {
            try
            {
                if (RunInit())
                {
                    Console.WriteLine("RunInit() => 正常終了");
                }
                else
                {
                    Console.WriteLine("RunInit() => 異常終了");
                }
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        protected abstract bool RunInit();
    }
}
