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
        /// TCPサーバーコントローラー
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
        sealed protected override void Connection()
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
                // クライアントからのメッセージを別スレッドで受信
                Task.Run(() => HandleCnnection());
            }
        }

        /// <summary>
        /// ハンドルコネクション
        /// </summary>
        private void HandleCnnection()
        {
            while (true)
            {
                try
                {
                    // コネクションを維持
                    byte[]? message = _tcpServer?.TcpRead();
                    // 内部電文処理
                    if (message is not null)
                    {
                        this.TcpReceivedSend(new MsgBase(message));
                    }
                }
                catch (Exception ex)
                {
                    // 電文送受信用インスタンスを開放
                    // ※NetStreamのみ開放し、コネクションは開放しない。
                    _tcpServer?.Close();
                    Log.Trace(_logFileName, LOGLEVEL.WARNING, $"{ex.Message}");
                    break;
                }
            }
        }

        /// <summary>
        /// ヘルスチェック処理
        /// </summary>
        sealed protected override void HelthCheck()
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
        /// <param name="msg">TCP電文送信メッセージ</param>
        protected void TcpSend(MsgBase msg)
        {
            _tcpServer?.TcpSend(msg);
        }

        /// <summary>
        /// TCP内部電文送信処理
        /// </summary>
        /// <param name="msg">内部電文送信メッセージ</param>
        private new void TcpReceivedSend(MsgBase msg)
        {
            // 基底クラスのTCP内部電文受信イベントを実行させる
            base.TcpReceivedSend(msg);
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
        /// TCP内部電文受信処理
        /// </summary>
        /// <param name="sender">内部電文メッセージクラス</param>
        /// <param name="e">メッセージイベント生成クラス</param>
        protected abstract override void OnTcpReceive(object? sender, MessageEventArgs e);
    }
}
