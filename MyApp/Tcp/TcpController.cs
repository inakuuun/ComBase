using MyApp.Common;
using MyApp.Logs;
using MyApp.Msg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static MyApp.Common.StractDef;

namespace MyApp.Tcp
{
    /// <summary>
    /// TCPコントローラークラス
    /// </summary>
    public class TcpController
    {
        /// <summary>
        /// TCPリスナー
        /// </summary>
        private TcpListener? _listener;

        /// <summary>
        /// TCPクライアント
        /// </summary>
        /// <remarks>電文の送信に利用</remarks>
        private TcpClient? _client;

        /// <summary>
        /// 電文送受信用変数
        /// </summary>
        private NetworkStream? _stream;

        /// <summary>
        /// TCPコントローラー取得用デリゲート
        /// </summary>
        /// <returns></returns>
        private CommonDef.TcpControllerDelegate _Connect;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="service">SERVER：TCPサーバー、CLIENT：TCPクライアント</param>
        public TcpController(TCP service)
        {
            // TCPサーバー
            if (service == TCP.SERVER)
            {
                _Connect = TcpServerConnect;
            }
            // TCPクライアント
            else
            {
                _Connect = TcpClientConnect;
            }
        }

        /// <summary>
        /// コネクション確立処理
        /// </summary>
        /// <param name="connectInfo"></param>
        public void Connect(TcpConnectInfo connectInfo)
        {
            try
            {
                _Connect(connectInfo);
            }
            catch
            {
                // エラーハンドリングは呼び出し元で実装
                throw;
            }
        }

        /// <summary>
        /// TCP電文送信処理
        /// </summary>
        /// <param name="reqObj">送信要求メッセージクラス</param>
        public void TcpSend(byte[] sendBytes)
        {
            try
            {
                // データを読み書きするインスタンスを取得
                _stream = _client?.GetStream();
                _stream?.Write(sendBytes, 0, sendBytes.Length);
            }
            catch
            {
                // エラーハンドリングは呼び出し元で実装
                throw;
            }
        }

        /// <summary>
        /// TCP電文取得処理
        /// </summary>
        public string TcpRead()
        {
            string resultData = string.Empty;
            try
            {
                if (_client != null && _stream != null)
                {
                    // 受信データのバッファサイズを指定して初期化
                    byte[] buffer = new byte[_client.ReceiveBufferSize];
                    // サーバからデータの送信があるまで処理を待機
                    int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                    // 取得したデータを文字列に変換
                    resultData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                }
                else
                {
                    _ = new Exception("TCP電文取得処理異常 => ハンドルのnull値を検出");
                }
            }
            catch
            {
                // エラーハンドリングは呼び出し元で実装
                throw;
            }
            return resultData;
        }

        /// <summary>
        /// コネクションの確立(サーバー)
        /// </summary>
        /// <param name="connectInfo"></param>
        private void TcpServerConnect(TcpConnectInfo connectInfo)
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
                if (endpoint.Port == connectInfo.Port)
                {
                    isUsePort = true;
                }
            }
            // TCP/IPリスナーポートを使用していない場合にリッスンする
            if (!isUsePort)
            {
                _listener = new TcpListener(connectInfo.IpAddress, connectInfo.Port);
                _listener.Start();
            }
        }

        /// <summary>
        /// コネクションの確立(クライアント)
        /// </summary>
        /// <param name="connectInfo"></param>
        private void TcpClientConnect(TcpConnectInfo connectInfo)
        {
            if (_client == null || !_client.Connected)
            {
                // サーバーへの接続要求
                _client = new TcpClient();
                _client.Connect(connectInfo.IpAddress, connectInfo.Port);
            }
        }

        /// <summary>
        /// メモリ開放処理
        /// </summary>
        /// <remarks>異常終了などによって電文送受信用変数のメモリを開放する必要がある場合に実行</remarks>
        public void Close()
        {
            _stream?.Close();
            _stream?.Dispose();
        }
    }
}
