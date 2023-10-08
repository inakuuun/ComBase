using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
        /// パラメーターインスタンス取得
        /// </summary>
        DbParameter GetDbParameter();

        /// <summary>
        /// パラメーターコレクションインスタンス取得
        /// </summary>
        /// <returns></returns>
        DbParameterCollection GetDbParameterCollection();

        /// <summary>
        /// SQL実行結果読み取り
        /// </summary>
        DbDataReader ExecuteReader(SqlBuilder sql);
        
        /// <summary>
        /// SQLコマンド実行
        /// </summary>
        void ExecuteNonQuery(SqlBuilder str);

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
