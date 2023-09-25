using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Threads
{
    /// <summary>
    /// スレッドBaseクラス
    /// </summary>
    public abstract class ThreadBase
    {
        /// <summary>
        /// スレッド変数
        /// </summary>
        private Thread? _thread;

        /// <summary>
        /// スレッド名
        /// </summary>
        protected string? ThreadName { get; set; }

        /// <summary>
        /// スレッド実行
        /// </summary>
        public void ThreadStart()
        {
            _thread = new Thread(new ThreadStart(ThreadRun));
            _thread.Start();
        }

        protected abstract void ThreadRun();
    }
}
