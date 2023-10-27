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
    /// 関数定義クラス
    /// </summary>
    public static class FunctionDef
    {
        /// <summary>
        /// DBコントローラー取得用デリゲート
        /// </summary>
        /// <returns></returns>
        public delegate IDbControl DbControlDelegate();

        /// <summary>
        /// TCPコントローラー取得用デリゲート
        /// </summary>
        /// <returns></returns>
        public delegate void TcpControllerDelegate(TcpConnectInfo connectInfo);
    }
}
