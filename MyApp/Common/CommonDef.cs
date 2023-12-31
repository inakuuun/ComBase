﻿using MyApp.Db;
using MyApp.Tcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Common
{
    /// <summary>
    /// 共通定義クラス
    /// </summary>
    public static class CommonDef
    {
        /// <summary>
        /// DBロジッククラス
        /// </summary>
        public static DbLogic DbLogic { get; set; } = new DbLogic();

        /// <summary>
        /// TCPクライアント接続状態
        /// </summary>
        /// <remaeks>true:接続、false:未接続</remaeks>
        public static bool IsTcpClientConnected { get; set; }
    }
}
