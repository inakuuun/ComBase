using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Db
{
    /// <summary>
    /// DBコントローラーインタフェース
    /// </summary>
    public interface IDbControl : IDisposable
    {
        /// <summary>
        /// DB接続
        /// </summary>
        void Open();

        /// <summary>
        /// トランザクション開始
        /// </summary>
        void TransactionStart();

        /// <summary>
        /// SQLコマンド実行
        /// </summary>
        void ExecuteNonQuery(string str);

        /// <summary>
        /// トランザクションコミット
        /// </summary>
        void TransactionCommit();

        /// <summary>
        /// トランザクションロールバック
        /// </summary>
        void TransactionRollback();
    }
}
