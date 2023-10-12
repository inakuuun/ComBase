using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyApp.Tcp;
using MyApp.Logs;
using static MyApp.Common.StractDef;

namespace MyApp
{
    public class TestTcpServer : TcpServerBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => base.ThreadName ?? string.Empty; }

        /// <summary>
        /// TCP接続情報
        /// </summary>
        private TcpConnectInfo _connectInfo { get; set; } = new();

        protected override bool RunInit()
        {
            try
            {
                _connectInfo = new TcpConnectInfo()
                {
                    // 接続先IPアドレス
                    IpAddress = IPAddress.Parse("127.0.0.1"),
                    // 待受けポート番号
                    Port = 50000
                };
                this.ConnectStart(_connectInfo);
            }
            catch(Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"異常終了 => {_logFileName} {ex}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// TCP接続開始
        /// </summary>
        public new void ConnectStart(TcpConnectInfo connectInfo)
        {
            try
            {
                base.ConnectStart(connectInfo);
            }
            catch (Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"異常終了 => {ex}");
            }
        }

        /// <summary>
        /// 接続解除
        /// </summary>
        public void TcpClose()
        {
            base.Close();
        }
    }
}
