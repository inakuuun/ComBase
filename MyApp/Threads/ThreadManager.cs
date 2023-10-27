using MyApp.Logs;
using MyApp.Msg;
using MyApp.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyApp.Common.StractDef;

namespace MyApp.Threads
{
    /// <summary>
    /// スレッド管理クラス
    /// </summary>
    public abstract class ThreadManager : ThreadBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get { return base.ThreadName ?? string.Empty; } }

        /// <summary>
        /// メッセージイベント
        /// </summary>
        /// <remarks>
        /// 内部電文生成用イベント
        /// ※クラス変数とすることで、別スレッドでイベントを発行した際に全スレッドでOnReceive処理が走る
        /// </remarks>
        private static event EventHandler<MessageEventArgs>? _msgEvent;

        /// <summary>
        /// スレッドの実行
        /// </summary>
        /// <remarks>下位クラスのメソッド呼び出し</remarks>
        /// <exception cref="NotImplementedException"></exception>
        protected sealed override void ThreadRun()
        {
            try
            {
                // 初期処理時にイベントの登録
                _msgEvent += OnReceive;

                if (RunInit())
                {
                    Log.Trace(_logFileName, LOGLEVEL.INFO, $"正常終了 => {base.ThreadName}");
                }
                else
                {
                    Log.Trace(_logFileName, LOGLEVEL.WARNING, $"異常終了 => {base.ThreadName}");
                }
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// メッセージ送信
        /// </summary>
        /// <param name="msgObj">送信メッセージ情報</param>
        protected void Send(object msgObj)
        {
            if (msgObj is MsgBase msg)
            {
                // イベントを発生させる
                _msgEvent?.Invoke(this, new MessageEventArgs(msg));
            }
        }

        /// <summary>
        /// 初期処理
        /// </summary>
        protected abstract bool RunInit();

        /// <summary>
        /// 内部電文受信処理
        /// </summary>
        protected abstract void OnReceive(object? sender, MessageEventArgs e);
    }
}
