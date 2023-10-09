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
        /// 接続開始
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="portNum"></param>
        protected override void ConnectStart(IPAddress? ipAddress, int portNum)
        {
            // コネクションの確立に成功した場合に以降の処理を実施
            if (Connection(ipAddress, portNum))
            {
                if (_client != null)
                {
                    // データを読み書きするインスタンスを取得
                    NetworkStream netStream = _client.GetStream();

                    // サーバーへ送信するデータ
                    string sendData = "Hello, Server!";
                    byte[] sendBytes = Encoding.UTF8.GetBytes(sendData);
                    // このタイミングでサーバへデータを送信処理
                    netStream.Write(sendBytes, 0, sendBytes.Length);
                    Log.Trace(_logFileName, LOGLEVEL.INFO, $"Sent Data: {sendData}");

                    // 受信するデータのバッファサイズを指定して初期化
                    byte[] receiveBytes = new byte[_client.ReceiveBufferSize];

                    // サーバからデータの送信があるまで処理を待機
                    int bytesRead = netStream.Read(receiveBytes, 0, _client.ReceiveBufferSize);
                    // 取得したデータを文字列に変換
                    string receivedData = Encoding.UTF8.GetString(receiveBytes, 0, bytesRead);
                    Log.Trace(_logFileName, LOGLEVEL.INFO, $"Received Data: {receivedData}");
                }
            }
        }

        /// <summary>
        /// コネクション確立
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="portNum"></param>
        protected override sealed bool Connection(IPAddress? ipAddress, int portNum)
        {
            bool result = false;
            try
            {
                // -------------------------------------------------
                // サーバーとTCP接続確立
                // サーバーへ接続開始
                // -------------------------------------------------
                if(ipAddress != null)
                {
                    _client = new TcpClient();
                    _client.Connect(ipAddress, portNum);
                    Log.Trace(_logFileName, LOGLEVEL.INFO, $"Server is listening on {ipAddress}:{portNum}");
                    result = true;
                }
            }
            catch (Exception e)
            {
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"コネクション確立時異常 => {ipAddress}:{portNum} {e}");
            }
            return result;
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
