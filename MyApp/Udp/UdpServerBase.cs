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
                if(message is not null)
                {
                    this.Send(new MsgBase(message));
                }
                string responseMessage = "サーバーからの応答：" + message;
                byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
                udpListener.Send(responseData, responseData.Length, clientEndPoint);
            }
        }

        /// <summary>
        /// 内部電文送信処理
        /// </summary>
        private new void Send(object msgObj)
        {
            // 型判定とキャスト
            if (msgObj is MsgBase msg)
            {
                // 基底クラスの内部電文イベントを実行させる
                base.Send(msg);
            }
        }
    }
}
