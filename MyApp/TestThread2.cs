using MyApp.Events;
using MyApp.Logs;
using MyApp.Msg;
using MyApp.Msg.Deffine;
using MyApp.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MyApp.Common.StractDef;

namespace MyApp
{
    public class TestThread2 : ThreadManager
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => base.ThreadName ?? string.Empty; }

        /// <summary>
        /// 初期処理
        /// </summary>
        /// <returns></returns>
        protected sealed override bool RunInit()
        {
            Log.Trace(_logFileName, LOGLEVEL.INFO, "呼び出し元が合っているかテスト2");
            return true;
        }

        /// <summary>
        /// 内部電文送信処理
        /// </summary>
        public new void Send(MsgBase msg)
        {
            base.Send(msg);
        }

        /// <summary>
        /// 内部電文受信処理
        /// </summary>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void OnReceive(object? sender, MessageEventArgs e)
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
