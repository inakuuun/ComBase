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
        protected sealed override bool RunInit()
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
                var bootStartReq = new BootStartReq()
                {
                    UserId = "W0001",
                    UserName = "user",
                    UserIp = "127.0.0.1",
                };
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
        /// TCP内部電文受信処理
        /// </summary>
        /// <param name="sender">内部電文メッセージクラス</param>
        /// <param name="e">メッセージイベント生成クラス</param>
        protected override void OnTcpReceive(object? sender, MessageEventArgs e)
        {
            try
            {
                // 初期起動通知応答
                if (e.MessageId == MsgDef.MSG_BOOTSTART_RES)
                {

                }
                else
                {
                    // 確認用出力
                    string data = Encoding.UTF8.GetString(e.Message);
                    Log.Trace(_logFileName, LOGLEVEL.DEBUG, $"確認用出力 => {data}");
                }
            }
            catch (Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"TCP内部電文受信処理異常終了 => {ex}");
            }
        }

        /// <summary>
        /// 内部電文受信処理
        /// </summary>
        /// <param name="sender">内部電文メッセージクラス</param>
        /// <param name="e">メッセージイベント生成クラス</param>
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
