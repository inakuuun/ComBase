using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Tcp
{
    public abstract class TcpServerBase : TcpBase
    {
        /// <summary>
        /// TCPリスナー
        /// </summary>
        /// <remarks>クライアントからの接続要求を待機時に利用</remarks>
        private TcpListener? _listener;

        /// <summary>
        /// TCPクライアント
        /// </summary>
        /// <remarks>応答電文を送信するために利用</remarks>
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
                // クライアントからの接続要求待ち
                _client = _listener?.AcceptTcpClient();
                if (_client != null)
                {
                    Console.WriteLine("A client connected.");

                    // データを読み書きするインスタンスを取得
                    NetworkStream netStream = _client.GetStream();
                    // 受信するデータのバッファサイズを指定して初期化
                    byte[] receiveBytes = new byte[_client.ReceiveBufferSize];

                    // クライアントからデータの送信があるまで処理を待機
                    int bytesRead = netStream.Read(receiveBytes, 0, _client.ReceiveBufferSize);
                    // 取得したデータを文字列に変換
                    string receivedData = Encoding.UTF8.GetString(receiveBytes, 0, bytesRead);
                    Console.WriteLine($"Received Data: {receivedData}");

                    // クライアントへ送信するデータ
                    string sendData = "Hello, Client!";
                    byte[] sendBytes = Encoding.UTF8.GetBytes(sendData);
                    // このタイミングでクライアントへデータを送信
                    netStream.Write(sendBytes, 0, sendBytes.Length);
                    Console.WriteLine($"Sent Data: {sendData}");
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
                // クライアントとTCP接続確立
                // 指定のポート番号で接続待機
                // -------------------------------------------------
                if(ipAddress != null)
                {
                    _listener = new TcpListener(ipAddress, portNum);
                    _listener.Start();
                    Console.WriteLine($"Server is listening on {ipAddress}:{portNum}");
                    result = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Server is not listening on {ipAddress}:{portNum}");
                Console.WriteLine($"{e}");
            }
            return result;
        }

        /// <summary>
        /// 接続解除
        /// </summary>
        protected override void Close()
        {
            _client?.Close();
            _listener?.Stop();
        }
    }
}
