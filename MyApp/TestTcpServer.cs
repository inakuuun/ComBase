using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using MyApp.Tcp;
using MyApp.Logs;
using static MyApp.Common.StractDef;
using MyApp.Events;
using MyApp.Msg.Deffine;
using MyApp.Msg.Messages;
using MyApp.Msg;

namespace MyApp
{
    public class TestTcpServer : TcpServerBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => base.ThreadName ?? string.Empty; }

        /// <summary>
        /// TCP接続情報
        /// </summary>
        private TcpConnectInfo _connectInfo { get; set; } = new();

        /// <summary>
        /// 初期処理
        /// </summary>
        /// <returns></returns>
        protected override bool RunInit()
        {
            try
            {
                _connectInfo = new TcpConnectInfo()
                {
                    // 接続先IPアドレス
                    IpAddress = IPAddress.Parse("127.0.0.1"),
                    // 待受けポート番号
                    Port = 50000,
                    // ヘルスチェック無し
                    IsHelthCheck = false
                };
                this.ConnectStart(_connectInfo);
            }
            catch(Exception ex)
            {
                // サーバーのTCP接続異常時はシステムを終了させる
                // ※初回起動時にリスナーポートが被るとTCPリスナーがnull値で検出され、無限ループに陥るため
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"初期処理異常終了 => {_logFileName} {ex}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// TCP接続開始
        /// </summary>
        public new void ConnectStart(TcpConnectInfo connectInfo)
        {
            try
            {
                base.ConnectStart(connectInfo);
            }
            catch (Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"異常終了 => {ex}");
            }
        }

        /// <summary>
        /// ヘルスチェック内部電文処理
        /// </summary>
        sealed protected override void OnHelthCheck(MsgBase msg)
        {
            Log.Trace(_logFileName, LOGLEVEL.INFO, $"ヘルスチェック要求受信");
        }

        /// <summary>
        /// TCP内部電文受信処理
        /// </summary>
        /// <param name="sender">内部電文メッセージクラス</param>
        /// <param name="e">メッセージイベント生成クラス</param>
        sealed protected override void OnTcpReceive(object? sender, MessageEventArgs e)
        {
            try
            {
                // 初期起動通知要求
                if (e.MessageId == MsgDef.MSG_BOOTSTART_REQ)
                {
                    var req = new BootStartReq(e.Message);
                    Log.Trace(_logFileName, LOGLEVEL.INFO, $"初期起動通知要求 => {req.UserId},{req.UserName},{req.UserIp}");
                }
                else
                {
                    // 確認用出力
                    string data = Encoding.UTF8.GetString(e.Message);
                    Log.Trace(_logFileName, LOGLEVEL.DEBUG, $"確認用出力 => {data}");
                }
            }
            catch (Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"TCP内部電文受信処理異常終了 => {ex}");
            }
        }

        /// <summary>
        /// 内部電文受信処理
        /// </summary>
        /// <param name="sender">内部電文メッセージクラス</param>
        /// <param name="e">メッセージイベント生成クラス</param>
        sealed protected override void OnReceive(object? sender, MessageEventArgs e)
        {
            try
            {
                // システム起動完了通知
                if (e.MessageId == MsgDef.MSG_SYSTEMBOOT_NOTICE)
                {
                }
            }
            catch(Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"内部電文受信処理異常終了 => {ex}");
            }
        }

        /// <summary>
        /// 接続解除
        /// </summary>
        public void TcpClose()
        {
            base.Close();
        }
    }
}
