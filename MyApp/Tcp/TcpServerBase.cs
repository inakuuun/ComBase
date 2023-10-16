﻿using System;
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
        /// <param name="connectInfo">TCP接続情報</param>
        protected override void ConnectStart(TcpConnectInfo connectInfo)
        {
            _connectInfo = connectInfo;
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
                        string? receivedData = _tcpServer?.TcpRead();
                        //Log.Trace(_logFileName, LOGLEVEL.DEBUG, $"Received Data: {receivedData}");
                        Log.Trace(_logFileName, LOGLEVEL.DEBUG, $"ヘルスチェック要求受信");

                        // TCP電文送信処理
                        _tcpServer?.TcpSend(req);
                        //Log.Trace(_logFileName, LOGLEVEL.DEBUG, $"Sent Data: {req.MessageId}");
                        Log.Trace(_logFileName, LOGLEVEL.DEBUG, $"ヘルスチェック応答送信");
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
            var req = (MsgBase)msgObj;
            _tcpServer?.TcpSend(req);
        }

        /// <summary>
        /// 接続解除
        /// </summary>
        protected override void Close()
        {

        }
    }
}
