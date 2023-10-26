using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using MyApp.Tcp;
using MyApp.Logs;
using static MyApp.Common.StractDef;
using MyApp.Events;
using MyApp.Msg.Deffine;

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
        /// ヘルスチェック内部電文処理
        /// </summary>
        protected override void OnHelthCheck()
        {
            Log.Trace(_logFileName, LOGLEVEL.INFO, $"ヘルスチェック要求受信");
        }
        
        /// <summary>
        /// 内部電文受信処理
        /// </summary>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void OnReceive(object? sender, MessageEventArgs e)
        {
            // ヘルスチェックの場合
            if (e.MessageId == MsgDef.MSG_HELTHCHECK_REQ)
            {
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
