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
using MyApp.Msg.Messages;

namespace MyApp.Tcp
{
    /// <summary>
    /// TCPサーバー基底クラス
    /// </summary>
    public abstract class TcpServerBase : TcpBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(TcpServerBase).Name ?? string.Empty; }

        /// <summary>
        /// サーバーコントローラー
        /// </summary>
        private TcpController? _tcpController;

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
        /// TCP接続情報
        /// </summary>
        private TcpConnectInfo _connectInfo { get; set; } = new();

        /// <summary>
        /// 接続開始
        /// </summary>
        /// <param name="connectInfo">TCP接続情報</param>
        protected override void ConnectStart(TcpConnectInfo connectInfo)
        {
            _connectInfo = connectInfo;
            this.HelthCheck();
        }

        /// <summary>
        /// コネクション確立
        /// </summary>
        protected override sealed void HelthCheck()
        {
            // -------------------------------------------------
            // クライアントとTCP接続確立
            // 接続を待機
            // -------------------------------------------------
            // サーバーコントローラーを生成
            _tcpController = new TcpController(TCP.SERVER);
            while (true)
            {
                // TCPコネクション初期処理
                _tcpController?.Connect(_connectInfo);
                while (true)
                {
                    try
                    {
                        // サーバーへ送信するデータ
                        HelthCheckReq req = new()
                        {
                            Message = "Hello, Client!"
                        };

                        // TCP受信電文取得処理
                        string? receivedData = _tcpController?.TcpRead();
                        Log.Trace(_logFileName, LOGLEVEL.INFO, $"Received Data: {receivedData}");

                        // TCP電文送信処理
                        byte[] sendBytes = Encoding.UTF8.GetBytes(req.Message);
                        _tcpController?.TcpSend(sendBytes);
                        Log.Trace(_logFileName, LOGLEVEL.INFO, $"Sent Data: {req.Message}");
                    }
                    catch (Exception ex)
                    {
                        Log.Trace(_logFileName, LOGLEVEL.WARNING, $"{ex.Message}");
                        break;
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
