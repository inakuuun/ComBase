using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Common;
using MyApp.Db;
using MyApp.Logs;
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

        protected override bool RunInit()
        {
            _dbLogic = CommonDef.DbLogic;
            //_dbLogic.ChatDaoAccess.ChatInsert();
            Log.Trace(_logFileName, LOGLEVEL.INFO, "呼び出し元が合っているかテスト");
            return true;
        }
    }
}
