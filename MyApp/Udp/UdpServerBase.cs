using MyApp.Events;
using MyApp.Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Udp
{
    /// <summary>
    /// UDPサーバー基底クラス
    /// </summary>
    public abstract class UdpServerBase : UdpBase
    {
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
            while (true)
            {
                var clientEndPoint = new IPEndPoint(IPAddress.Any, _connectInfo.Port);
                byte[] message = udpListener.Receive(ref clientEndPoint);
                string receivedMessage = Encoding.UTF8.GetString(message);
                if (message is not null)
                {
                    this.Send(new MsgBase(message));
                }
                Console.WriteLine("クライアントからのメッセージ: " + receivedMessage);

                string responseMessage = "サーバーからの応答：" + receivedMessage;
                byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
                udpListener.Send(responseData, responseData.Length, clientEndPoint);
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
