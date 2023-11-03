using MyApp.Events;
using MyApp.Logs;
using MyApp.Msg;
using MyApp.Tcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static MyApp.Common.StractDef;

namespace MyApp.Udp
{
    /// <summary>
    /// UDPサーバー基底クラス
    /// </summary>
    public abstract class UdpServerBase : UdpBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(TcpClientBase).Name ?? string.Empty; }

        /// <summary>
        /// UDPサーバーコントローラー
        /// </summary>
        private UdpController? _ucpServer;

        /// <summary>
        /// UDP接続情報
        /// </summary>
        private UdpConnectInfo _connectInfo = new();

        /// <summary>
        /// 接続開始
        /// </summary>
        /// <param name="connectInfo"></param>
        protected override void ConnectStart(UdpConnectInfo connectInfo)
        {
            _connectInfo = connectInfo;
            Connection();
        }

        /// <summary>
        /// コネクション確立
        /// </summary>
        protected void Connection()
        {
            // -------------------------------------------------
            // クライアントとUDP接続確立
            // クライアントから接続があるまで待機
            // -------------------------------------------------
            _ucpServer = new UdpController(UDP.SERVER, _connectInfo);
            try
            {
                while (true)
                {
                    byte[] message = _ucpServer.Receive();
                    // 内部電文送信処理
                    if (message is not null)
                    {
                        this.UdpReceivedSend(new MsgBase(message));
                    }
                    byte[] response = Encoding.UTF8.GetBytes("サーバーからの応答");
                    UdpSend(new MsgBase(response));
                }
            }
            catch (Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.WARNING, $"コネクション確立時異常 => {ex}");
            }
        }

        /// <summary>
        /// UDP内部電文送信処理
        /// </summary>
        /// <param name="msg">UDP内部電文送信メッセージ</param>
        private new void UdpReceivedSend(MsgBase msg)
        {
            // 基底クラスの内部電文イベントを実行させる
            base.UdpReceivedSend(msg);
        }

        /// <summary>
        /// TCP電文送信処理
        /// </summary>
        /// <param name="msg">TCP電文送信メッセージ</param>
        protected void UdpSend(MsgBase msg)
        {
            _ucpServer?.UdpSend(msg);
        }

        /// <summary>
        /// UDP内部電文受信処理
        /// </summary>
        /// <param name="sender">内部電文メッセージクラス</param>
        /// <param name="e">メッセージイベント生成クラス</param>
        protected abstract override void OnUdpReceive(object? sender, MessageEventArgs e);
    }
}
