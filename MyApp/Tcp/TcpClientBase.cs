using MyApp.Db.Dao;
using MyApp.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static MyApp.Common.StractDef;

namespace MyApp.Tcp
{
    /// <summary>
    /// TCPクライアント基底クラス
    /// </summary>
    public abstract class TcpClientBase : TcpBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(TcpClientBase).Name ?? string.Empty; }

        /// <summary>
        /// TCPクライアント
        /// </summary>
        /// <remarks>電文の送信に利用</remarks>
        private TcpClient? _client;

        /// <summary>
        /// TCP接続情報
        /// </summary>
        private TcpConnectInfo _connectInfo { get; set; } = new();

        /// <summary>
        /// 接続開始
        /// </summary>
        protected override void ConnectStart(TcpConnectInfo connectInfo)
        {
            _connectInfo = connectInfo;
            this.Connection();
        }

        /// <summary>
        /// コネクション確立
        /// </summary>
        protected override sealed void Connection()
        {
            // -------------------------------------------------
            // サーバーとTCP接続確立
            // サーバーへ接続開始
            // -------------------------------------------------
            // エラー復帰フラグ
            bool wasErr = false;
            while (true)
            {
                try
                {
                    // サーバーと接続出来ていない場合
                    if (_client == null || !_client.Connected)
                    {
                        // サーバーへの接続要求
                        _client = new TcpClient();
                        _client.Connect(_connectInfo.IpAddress, _connectInfo.Port);
                        Log.Trace(_logFileName, LOGLEVEL.INFO, $"Server is listening on {_connectInfo.IpAddress}:{_connectInfo.Port}");
                    }
                    // データを読み書きするインスタンスを取得
                    using NetworkStream netStream = _client.GetStream();
                    // 受信するデータのバッファサイズを指定して初期化
                    byte[] receiveBytes = new byte[_client.ReceiveBufferSize];
                    while (true)
                    {
                        if (_client != null)
                        {
                            // エラーからの復帰ではない場合
                            // ※エラー時に指定時間処理を遅らせるため、ここでは遅延処理を実施しない
                            if (!wasErr)
                            {
                                // サーバーへデータを送信する時間を指定時間遅らせる
                                System.Threading.Thread.Sleep(_connectInfo.HelthCheckInterval);
                            }
                            // サーバーへ送信するデータ
                            string sendData = "Hello, Server!";
                            byte[] sendBytes = Encoding.UTF8.GetBytes(sendData);
                            // このタイミングでサーバへデータを送信処理
                            netStream.Write(sendBytes, 0, sendBytes.Length);
                            Log.Trace(_logFileName, LOGLEVEL.INFO, $"Sent Data: {sendData}");

                            // サーバからデータの送信があるまで処理を待機
                            int bytesRead = netStream.Read(receiveBytes, 0, _client.ReceiveBufferSize);
                            // 取得したデータを文字列に変換
                            string receivedData = Encoding.UTF8.GetString(receiveBytes, 0, bytesRead);
                            Log.Trace(_logFileName, LOGLEVEL.INFO, $"Received Data: {receivedData}");

                            // エラーからの復帰の場合にフラグを更新する必要あり
                            // ※while文中の処理が正常の場合は常に「false」
                            wasErr = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Trace(_logFileName, LOGLEVEL.WARNING, $"コネクション確立時異常 => {_connectInfo.IpAddress}:{_connectInfo.Port} {ex}");
                    //_ = new Timer(new TimerCallback(ReConnect), null, 10000, Timeout.Infinite);
                    System.Threading.Thread.Sleep(_connectInfo.HelthCheckInterval);
                    wasErr = true;
                }
            }
        }

        /// <summary>
        /// 再接続処理
        /// </summary>
        private void ReConnect(object? state)
        {
            this.Connection();
        }

        /// <summary>
        /// 接続解除
        /// </summary>
        protected override void Close()
        {
            // TcpClient をクローズする
            _client?.Close();
            _client?.Dispose();
        }
    }
}
