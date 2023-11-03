﻿using MyApp.Events;
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
    /// UDPクライアント基底クラス
    /// </summary>
    public abstract class UdpClientBase : UdpBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(TcpClientBase).Name ?? string.Empty; }

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
            var serverEndPoint = new IPEndPoint(IPAddress.Parse(_udpConnectInfo.IpAddress), _udpConnectInfo.Port);
            try
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(5000);
                    // 送信データを生成
                    byte[] data = Encoding.UTF8.GetBytes("Hello");
                    // サーバーへUDP送信
                    udpClient.Send(data, data.Length, serverEndPoint);

                    // サーバーからの受信を待機
                    byte[] message = udpClient.Receive(ref serverEndPoint);
                    string receivedMessage = Encoding.UTF8.GetString(message);
                    Console.WriteLine(receivedMessage);
                    // 内部電文送信処理
                    if (message is not null)
                    {
                        this.UdpReceivedSend(new MsgBase(message));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.WARNING, $"コネクション確立時異常 => {ex}");
            }
        }

        /// <summary>
        /// UDP送信処理
        /// </summary>
        protected void UdpSend(MsgBase msg)
        {

        }

        /// <summary>
        /// 内部電文送信処理
        /// </summary>
        /// <param name="msg"></param>
        private new void UdpReceivedSend(MsgBase msg)
        {
            // 基底クラスの内部電文イベントを実行させる
            base.UdpReceivedSend(msg);
        }

        /// <summary>
        /// UDP内部電文受信処理
        /// </summary>
        /// <param name="sender">内部電文メッセージクラス</param>
        /// <param name="e">メッセージイベント生成クラス</param>
        protected abstract override void OnUdpReceive(object? sender, MessageEventArgs e);
    }
}
