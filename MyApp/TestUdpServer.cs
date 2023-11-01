using MyApp.Events;
using MyApp.Logs;
using MyApp.Udp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyApp.Common.StractDef;

namespace MyApp
{
    public class TestUdpServer : UdpServerBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => base.ThreadName ?? string.Empty; }

        /// <summary>
        /// UDP接続情報
        /// </summary>
        UdpConnectInfo _connectInfo = new();

        /// <summary>
        /// 初期処理
        /// </summary>
        /// <returns></returns>
        protected override sealed bool RunInit()
        {
            _connectInfo = new UdpConnectInfo()
            {
                IpAddress = "127.0.0.1",
                Port = 30000
            };
            ConnectStart(_connectInfo);
            return true;
        }

        /// <summary>
        /// UDP接続開始
        /// </summary>
        /// <param name="connectInfo"></param>
        protected sealed override void ConnectStart(UdpConnectInfo connectInfo)
        {
            try
            {
                base.ConnectStart(connectInfo);
            }
            catch(Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"異常終了 => {ex}");
            }
        }

        /// <summary>
        /// 内部電文受信処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected sealed override void OnReceive(object? sender, MessageEventArgs e)
        {

        }
    }
}
