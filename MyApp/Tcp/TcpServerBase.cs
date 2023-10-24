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
using MyApp.Msg;
using System.Net.Http;

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
        private TcpController? _tcpServer;

        /// <summary>
        /// TCP接続情報
        /// </summary>
        private TcpConnectInfo _connectInfo = new();

        /// <summary>
        /// 接続開始
        /// </summary>
        /// <param name="connectInfo">TCP接続情報インスタンス</param>
        protected override void ConnectStart(TcpConnectInfo connectInfo)
        {
            // 接続情報インスタンスを設定
            _connectInfo = connectInfo;
            // ヘルスチェック処理
            this.HelthCheck();
        }

        /// <summary>
        /// ヘルスチェック処理
        /// </summary>
        protected override sealed void HelthCheck()
        {
            // -------------------------------------------------
            // クライアントとTCP接続確立
            // クライアントから接続があるまで待機
            // -------------------------------------------------
            // サーバーコントローラーを生成
            _tcpServer = new TcpController(TCP.SERVER);
            while (true)
            {
                // TCPコネクション初期処理
                _tcpServer?.Connect(_connectInfo);
                while (true)
                {
                    try
                    {
                        // サーバーへ送信するデータ
                        HelthCheckReq req = new();
                        // TCP受信電文取得処理
                        byte[]? receivedData = _tcpServer?.TcpRead();
                        // TCP電文送信処理
                        _tcpServer?.TcpSend(req);
                        // ヘルスチェック内部電文処理
                        this.OnHelthCheck();
                    }
                    catch (Exception ex)
                    {
                        _tcpServer?.Close();
                        Log.Trace(_logFileName, LOGLEVEL.WARNING, $"{ex.Message}");
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// TCP電文送信処理
        /// </summary>
        protected void TcpSend(object msgObj)
        {
            // 型判定とキャスト
            if (msgObj is MsgBase req)
            {
                _tcpServer?.TcpSend(req);
            }
        }

        /// <summary>
        /// 接続解除
        /// </summary>
        protected override void Close()
        {

        }

        /// <summary>
        /// ヘルスチェック内部電文処理
        /// </summary>
        protected abstract override void OnHelthCheck();
    }
}
