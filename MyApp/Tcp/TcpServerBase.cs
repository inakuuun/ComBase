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
using MyApp.Events;

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
        /// ヘルスチェック要求メッセージクラス
        /// </summary>
        private HelthCheckReq _helthCheckReq = new();

        /// <summary>
        /// 接続開始
        /// </summary>
        /// <param name="connectInfo">TCP接続情報インスタンス</param>
        protected override void ConnectStart(TcpConnectInfo connectInfo)
        {
            // 接続情報インスタンスを設定
            _connectInfo = connectInfo;
            // ヘルスチェックが必要な場合
            if (_connectInfo.IsHelthCheck)
            {
                // ヘルスチェック処理
                this.HelthCheck();
            }
            else
            {
                // TCPコネクション確立
                this.Connection();
            }
        }

        /// <summary>
        /// TCPコネクション確立
        /// </summary>
        protected void Connection()
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
                        // コネクションを維持
                        byte[]? receivedData = _tcpServer?.TcpRead();
                        if(receivedData is not null)
                        {
                            var msg = new MsgReader(receivedData);
                            // TODO：
                            // マップに電文IDをキーにしたメッセージクラスを用意しておき、取得した電文から識別子を取得して、
                            // 紐づくメッセージクラスを取得し後インスタンス化。内部電文として分配
                            var aaa = msg.RdShort();
                        }
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
        /// ヘルスチェック処理
        /// </summary>
        protected override sealed void HelthCheck()
        {
            // -------------------------------------------------
            // クライアントとTCP接続確立＆ヘルスチェック
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
                        // TCP受信電文取得処理
                        byte[]? receivedData = _tcpServer?.TcpRead();
                        // ヘルスチェック内部電文処理
                        this.OnHelthCheck();
                        // クライアントへ送信するデータ
                        _helthCheckReq = new HelthCheckReq();
                        // TCP電文送信処理
                        _tcpServer?.TcpSend(_helthCheckReq);
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

        /// <summary>
        /// 内部電文受信処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract override void OnReceive(object? sender, MessageEventArgs e);
    }
}
