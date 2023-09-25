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
            // 自身のクラス名をスレッド名にセット
            base.ThreadName = this.GetType().Name;
            return true;
        }
    }
}
