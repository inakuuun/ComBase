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
            var udpListener = new UdpClient(_connectInfo.Port);
            var clientEndPoint = new IPEndPoint(IPAddress.Any, _connectInfo.Port);
            try
            {
                while (true)
                {
                    byte[] message = udpListener.Receive(ref clientEndPoint);
                    string receivedMessage = Encoding.UTF8.GetString(message);
                    // 内部電文送信処理
                    if (message is not null)
                    {
                        this.UdpReceivedSend(new MsgBase(message));
                    }

                    System.Threading.Thread.Sleep(10000);
                    string responseMessage = "サーバーからの応答：";
                    byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
                    udpListener.Send(responseData, responseData.Length, clientEndPoint);
                }
            }
            catch (Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.WARNING, $"コネクション確立時異常 => {ex}");
            }
        }

        /// <summary>
        /// 内部電文送信処理
        /// </summary>
        private new void Send(MsgBase msg)
        {
            // 基底クラスの内部電文イベントを実行させる
            base.Send(msg);
        }

        /// <summary>
        /// UDP内部電文受信処理
        /// </summary>
        /// <param name="sender">内部電文メッセージクラス</param>
        /// <param name="e">メッセージイベント生成クラス</param>
        protected abstract override void OnUdpReceive(object? sender, MessageEventArgs e);
    }
}
