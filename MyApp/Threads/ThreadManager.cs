using MyApp.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Log.Trace(_logFileName, $"正常終了{base.ThreadName}");
                }
                else
                {
                    Log.Trace(_logFileName, $"異常終了{base.ThreadName}");
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
