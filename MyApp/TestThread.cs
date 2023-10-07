﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Db;
using MyApp.Logs;
using MyApp.Threads;

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
            _dbLogic = new DbLogic();
            _dbLogic.ChatDaoAccess.ChatInsert();
            Log.Trace(_logFileName, "呼び出し元が合っているかテスト");
            return true;
        }
    }
}
