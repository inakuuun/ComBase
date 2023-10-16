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
        /// <param name="connectInfo">TCP接続情報インスタンス</param>
        protected abstract void ConnectStart(TcpConnectInfo connectInfo);

        /// <summary>
        /// ヘルスチェック処理
        /// </summary>
        protected abstract void HelthCheck();

        /// <summary>
        /// 接続解除
        /// </summary>
        protected abstract void Close();
    }
}
