using MyApp.Common;
using MyApp.Events;
using MyApp.Logs;
using MyApp.Msg;
using MyApp.Msg.Deffine;
using MyApp.Udp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyApp.Common.StractDef;

namespace MyApp
{
    public class TestUdpClient : UdpClientBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get =>  base.ThreadName ?? string.Empty; }

        /// <summary>
        /// TCPクライアント接続状態
        /// </summary>
        private bool _isTcpClientConnected { get => CommonDef.IsTcpClientConnected; }

        /// <summary>
        /// UDP接続情報
        /// </summary>
        private UdpConnectInfo _connectInfo = new();

        /// <summary>
        /// 初期処理
        /// </summary>
        /// <returns></returns>
        sealed protected override bool RunInit()
        {
            _connectInfo = new()
            {
                // IPアドレス
                IpAddress = "127.0.0.1",
                // 接続先ポート番号
                DestPort = 30000,
                // ポート番号
                Port = 30001,
            };
            this.ConnectStart(_connectInfo);
            // 5秒間隔でサーバー側に「Hello」をUDP送信
            Task.Run(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(5000);
                    // TCPクライアントのコネクションが確立されている場合にのみUDP電文を送信
                    if (_isTcpClientConnected)
                    {
                        // 送信データを生成
                        byte[] data = Encoding.UTF8.GetBytes("Hello");
                        this.UdpSend(new MsgBase(data));
                    }
                }
            });
            return true;
        }

        /// <summary>
        /// UDP接続開始
        /// </summary>
        /// <param name="connectInfo"></param>
        sealed protected override void ConnectStart(UdpConnectInfo connectInfo)
        {
            try
            {
                base.ConnectStart(connectInfo);
            }
            catch (Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"UDP接続開始異常終了 => {ex}");
            }
        }

        /// <summary>
        /// UDP内部電文受信処理
        /// </summary>
        /// <param name="sender">内部電文メッセージクラス</param>
        /// <param name="e">メッセージイベント生成クラス</param>
        sealed protected override void OnUdpReceive(object? sender, MessageEventArgs e)
        {
            try
            {
                // システム起動完了通知
                if (e.MessageId == MsgDef.MSG_SYSTEMBOOT_NOTICE)
                {
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
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"UDP内部電文受信処理異常終了 => {ex}");
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
            catch (Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"内部電文受信処理異常終了 => {ex}");
            }
        }
    }
}
