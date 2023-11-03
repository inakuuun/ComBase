using MyApp.Tcp;
using MyApp.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using static MyApp.Common.StractDef;
using MyApp.Events;
using MyApp.Msg.Deffine;
using MyApp.Msg.Messages;

namespace MyApp
{
    public class TestTcpClient : TcpClientBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => base.ThreadName ?? string.Empty; }

        /// <summary>
        /// TCP接続情報
        /// </summary>
        private TcpConnectInfo _connectInfo = new();

        /// <summary>
        /// 初期処理
        /// </summary>
        /// <returns></returns>
        protected override bool RunInit()
        {
            _connectInfo = new TcpConnectInfo()
            {
                // 接続先IPアドレス
                IpAddress = IPAddress.Parse("127.0.0.1"),
                // 宛先ポート
                Port = 50000,
                // ヘルスチェック無し
                IsHelthCheck = false,
                // ヘルスチェック間隔(ミリ秒)
                HelthCheckInterval = 10000
            };
            Task.Run(() =>
            {
                // 10秒後に初期起動通知要求を送信
                System.Threading.Thread.Sleep(10000);
                var bootStartReq = new BootStartReq();
                base.TcpSend(bootStartReq);
            });
            // コネクションの維持を行うため、ConnectStartメソッドより下で処理を実行できない
            this.ConnectStart(_connectInfo);
            return true;
        }

        /// <summary>
        /// TCP接続開始
        /// </summary>
        protected sealed override void ConnectStart(TcpConnectInfo connectInfo)
        {
            try
            {
                base.ConnectStart(connectInfo);
            }
            catch (Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"異常終了 => { ex }");
            }
        }

        /// <summary>
        /// ヘルスチェック内部電文処理
        /// </summary>
        protected override void OnHelthCheck()
        {
            Log.Trace(_logFileName, LOGLEVEL.DEBUG, $"ヘルスチェック応答受信");
        }

        /// <summary>
        /// 内部電文受信処理
        /// </summary>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void OnReceive(object? sender, MessageEventArgs e)
        {
            try
            {
                // システム起動完了通知
                if (e.MessageId == MsgDef.MSG_SYSTEMBOOT_NOTICE)
                {
                }
            }
            catch (Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"内部電文受信処理異常終了 => {ex}");
            }
        }
    }
}
