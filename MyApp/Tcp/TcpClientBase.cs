using MyApp.Db.Dao;
using MyApp.Logs;
using MyApp.Msg;
using MyApp.Msg.Messages;
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
        /// TCP接続情報
        /// </summary>
        private TcpConnectInfo _connectInfo = new();
        
        /// <summary>
        /// クライアントコントローラー
        /// </summary>
        private TcpController? _tcpClient;

        /// <summary>
        /// 接続開始
        /// </summary>
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
            // サーバーとTCP接続
            // サーバーへ接続要求を送信する
            // -------------------------------------------------
            // 遅延判定フラグ
            // ※遅延が必要かを判定するフラグ
            bool needDelay = true;
            // クライアントコントローラーを生成
            _tcpClient = new TcpController(TCP.CLIENT);
            // 接続を維持するためにwhile文が必要そう
            while (true)
            {
                try
                {
                    // TCPコネクション確立
                    _tcpClient?.Connect(_connectInfo);
                    // 通信異常がない間ループ処理を実施
                    while (true)
                    {
                        // 遅延が必要な場合
                        // ※エラー発生時に遅延処理を入れるため、エラー発生後1回目の処理は遅延処理を実施しない
                        if (needDelay)
                        {
                            // サーバーへデータを送信する時間を指定時間遅らせる
                            System.Threading.Thread.Sleep(_connectInfo.HelthCheckInterval);
                        }

                        // サーバーへ送信するデータ
                        HelthCheckReq req = new()
                        {
                            Message = "Hello, Server!"
                        };

                        // TCP電文送信処理
                        byte[] sendBytes = Encoding.UTF8.GetBytes(req.Message);
                        _tcpClient?.TcpSend(sendBytes);
                        Log.Trace(_logFileName, LOGLEVEL.INFO, $"Sent Data: {req.Message}");

                        // TCP受信電文取得処理
                        string? receivedData = _tcpClient?.TcpRead();
                        Log.Trace(_logFileName, LOGLEVEL.INFO, $"Received Data: {receivedData}");

                        // エラーからの復帰の場合にフラグを更新する必要あり
                        // ※while文中の処理が正常の場合は「true」を維持
                        if (!needDelay)
                        {
                            needDelay = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Trace(_logFileName, LOGLEVEL.WARNING, $"コネクション確立時異常 => {_connectInfo.IpAddress}:{_connectInfo.Port} {ex}");
                    // 正常処理の遅延処理を実施しないようにfalseを設定
                    needDelay = false;
                    // 電文送受信用インスタンスを開放
                    _tcpClient?.Close();
                    // サーバーへデータを送信する時間を指定時間遅らせる
                    // ※TCPコネクション確立処理で落ちる可能性もあるため、エラー時に指定秒数処理を遅延させる
                    //_ = new Timer(new TimerCallback(ReConnect), null, 10000, Timeout.Infinite);
                    System.Threading.Thread.Sleep(_connectInfo.HelthCheckInterval);
                }
            }
        }

        /// <summary>
        /// TCP電文送信処理
        /// </summary>
        protected void TcpSend(byte[] sendBytes)
        {
            _tcpClient?.TcpSend(sendBytes);
        }

        /// <summary>
        /// 接続解除
        /// </summary>
        protected override void Close()
        {
            // TODO：不要な処理であれば後で削除
            // 現状呼び出し元から接続解除する予定はないが、念のため残しておく
        }
    }
}
