using MyApp.Logs;
using MyApp.Msg;
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
        private event EventHandler<MsgBase>? _msgEvent;

        /// <summary>
        /// メッセージ送信
        /// </summary>
        /// <param name="msg">送信メッセージ情報</param>
        public void Send(object msg)
        {
            // イベントを発生させる
            _msgEvent?.Invoke(this, (MsgBase)msg);
        }

        /// <summary>
        /// スレッドの実行
        /// </summary>
        /// <remarks>下位クラスのメソッド呼び出し</remarks>
        /// <exception cref="NotImplementedException"></exception>
        protected sealed override void ThreadRun()
        {
            try
            {
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
        /// 初期処理
        /// </summary>
        protected abstract bool RunInit();
    }
}
