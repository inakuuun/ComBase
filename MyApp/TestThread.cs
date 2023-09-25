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
            // 自身のクラス名をスレッド名にセット
            base.ThreadName = this.GetType().Name;
            return true;
        }
    }
}
