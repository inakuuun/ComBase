using MyApp.Events;
using MyApp.Logs;
using MyApp.Msg;
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
        /// TCPコネクション確立
        /// </summary>
        protected abstract void Connection();
        
        /// <summary>
        /// ヘルスチェック処理
        /// </summary>
        protected abstract void HelthCheck();

        /// <summary>
        /// ヘルスチェック内部電文処理
        /// </summary>
        protected abstract void OnHelthCheck(MsgBase msg);

        /// <summary>
        /// 接続解除
        /// </summary>
        protected abstract void Close();
    }
}
