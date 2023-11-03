using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Common;
using MyApp.Db;
using MyApp.Events;
using MyApp.Logs;
using MyApp.Msg;
using MyApp.Msg.Deffine;
using MyApp.Msg.Messages;
using MyApp.Threads;
using static MyApp.Common.StractDef;

namespace MyApp
{
    public class TestThread : ThreadManager
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => base.ThreadName ?? string.Empty; }

        /// <summary>
        /// DBロジック
        /// </summary>
        private DbLogic? _dbLogic;

        /// <summary>
        /// 初期処理
        /// </summary>
        /// <returns></returns>
        protected sealed override bool RunInit()
        {
            _dbLogic = CommonDef.DbLogic;
            var bootStartReq = new BootStartReq()
            {
                UserId = "W0001",
                UserName = "user",
                UserIp = "127.0.0.1",
            };
            base.Send(bootStartReq);
            //_dbLogic.ChatDaoAccess.ChatInsert();
            Log.Trace(_logFileName, LOGLEVEL.INFO, "呼び出し元が合っているかテスト");
            return true;
        }

        /// <summary>
        /// 内部電文送信処理
        /// </summary>
        public new void Send(MsgBase msg)
        {
            // 電文送信
            base.Send(msg);
        }

        /// <summary>
        /// 内部電文受信処理
        /// </summary>
        /// <param name="sender">内部電文メッセージクラス</param>
        /// <param name="e">メッセージイベント生成クラス</param>
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
