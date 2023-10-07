using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Db
{
    public interface IDbControl : IDisposable
    {
        /// <summary>
        /// コネクション確立
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
