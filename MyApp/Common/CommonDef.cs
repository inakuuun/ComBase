using MyApp.Db;
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
        /// DBコントローラー取得用デリゲート
        /// </summary>
        /// <returns></returns>
        public delegate IDbControl DbControlDelegate();
    }
}
