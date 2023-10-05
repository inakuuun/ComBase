﻿using MyApp.Logs;
using MyApp.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    public class TestThread2 : ThreadManager
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => base.ThreadName ?? string.Empty; }

        protected override bool RunInit()
        {
            Log.Trace(_logFileName, "呼び出し元が合っているかテスト2");
            return true;
        }
    }
}
