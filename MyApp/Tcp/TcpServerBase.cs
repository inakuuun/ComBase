using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyApp.Logs;
using static MyApp.Common.StractDef;
using System.IO;
using System.Net.NetworkInformation;

namespace MyApp.Tcp
{
    public abstract class TcpServerBase : TcpBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(TcpServerBase).Name ?? string.Empty; }

        /// <summary>
        /// TCPリスナー
        /// </summary>
        private TcpListener? _listener;

        /// <summary>
        /// TCP接続情報
        /// </summary>
        private TcpConnectInfo _connectInfo { get; set; } = new();

        /// <summary>
        /// 接続開始
        /// </summary>
        /// <remarks>
        /// </remarks>
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
            // クライアントとTCP接続確立
            // 指定のポート番号で接続待機
            // -------------------------------------------------
            while (true)
            {
                // ====================================================
                // 使用されているポートの判定処理
                // C#でリスニング状態のポートを取得する方法
                // https://usefuledge.com/csharp-check-port-open.html
                // 使用済みポート判定フラグ
                bool isUsePort = false;
                // ネットワーク関連のグローバルなプロパティと設定情報を取得
                IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                // アクティブなTCPリスナーポートの情報を取得
                IPEndPoint[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();
                // 指定したポート番号がTCP/IPリスナーポートとして使用されているかチェック
                foreach (IPEndPoint endpoint in tcpConnInfoArray)
                {
                    // 使用しているポートがある場合ONを設定
                    if (endpoint.Port == _connectInfo.Port)
                    {
                        isUsePort = true;
                    }
                }
                // TCP/IPリスナーポートを使用していない場合にリッスンする
                if (!isUsePort)
                {
                    _listener = new TcpListener(_connectInfo.IpAddress, _connectInfo.Port);
                    _listener.Start();
                }

                if(_listener != null)
                {
                    Log.Trace(_logFileName, LOGLEVEL.INFO, "Waiting for connection...");
                    // クライアントからの接続要求待ち
                    using TcpClient client = _listener.AcceptTcpClient();
                    // データを読み書きするインスタンスを取得
                    using NetworkStream stream = client.GetStream();
                    // 受信するデータのバッファサイズを指定して初期化
                    byte[] buffer = new byte[client.ReceiveBufferSize];
                    while (true)
                    {
                        try
                        {
                            // サーバからデータの送信があるまで処理を待機
                            int bytesRead = stream.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0)
                            {
                                throw new Exception("クライアントが切断しました。");
                            }

                            // ここで受信したデータを処理
                            // 取得したデータを文字列に変換
                            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            Log.Trace(_logFileName, LOGLEVEL.INFO, $"Received Data: {receivedData}");

                            // クライアントへ送信するデータ
                            string sendData = "Hello, Client!";
                            byte[] sendBytes = Encoding.UTF8.GetBytes(sendData);
                            // このタイミングでクライアントへデータを送信処理
                            stream.Write(sendBytes, 0, sendBytes.Length);
                            Log.Trace(_logFileName, LOGLEVEL.INFO, $"Sent Data: {sendData}");
                        }
                        catch (Exception ex)
                        {
                            Log.Trace(_logFileName, LOGLEVEL.WARNING, $"{ex.Message}");
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 接続解除
        /// </summary>
        protected override void Close()
        {
        }
    }
}
