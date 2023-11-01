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
    /// UDPクライアント基底クラス
    /// </summary>
    public abstract class UdpClientBase : UdpBase
    {
        /// <summary>
        /// UDP接続情報
        /// </summary>
        private UdpConnectInfo _udpConnectInfo = new();

        /// <summary>
        /// 接続開始
        /// </summary>
        /// <param name="connectInfo"></param>
        protected override void ConnectStart(UdpConnectInfo connectInfo)
        {
            _udpConnectInfo = connectInfo;
            this.Connection();
        }

        /// <summary>
        /// コネクション確立
        /// </summary>
        protected void Connection()
        {
            var udpClient = new UdpClient();

            while (true)
            {
                // 送信データを生成
                byte[] data = Encoding.UTF8.GetBytes("Hello");
                // サーバーへUDP送信
                udpClient.Send(data, data.Length, _udpConnectInfo.IpAddress, _udpConnectInfo.Port);

                // サーバーからの受信を待機
                var serverEndPoint = new IPEndPoint(IPAddress.Parse(_udpConnectInfo.IpAddress), _udpConnectInfo.Port);
                byte[] message = udpClient.Receive(ref serverEndPoint);

                // 内部電文送信処理
                if (message is not null)
                {
                    this.Send(new MsgBase(message));
                }
            }
        }

        /// <summary>
        /// UDP送信処理
        /// </summary>
        protected void UdpSend(object msgObj)
        {

        }

        /// <summary>
        /// 内部電文送信処理
        /// </summary>
        /// <param name="msg"></param>
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
