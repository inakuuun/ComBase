using MyApp.Logs;
using MyApp.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Tcp
{
    /// <summary>
    /// TCP基底クラス
    /// </summary>
    public abstract class TcpBase : ThreadManager
    {
        /// <summary>
        /// 接続開始
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="portNum"></param>
        protected abstract void ConnectStart(IPAddress ipAddress, int portNum);

        /// <summary>
        /// コネクション確立
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="portNum"></param>
        protected abstract bool Connection(IPAddress ipAddress, int portNum);

        /// <summary>
        /// 接続解除
        /// </summary>
        protected abstract void Close();
    }
}
