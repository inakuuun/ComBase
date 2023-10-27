using MyApp.Db;
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
        /// <returns></returns>
        public static DbLogic DbLogic { get; set; } = new DbLogic();
    }
}
