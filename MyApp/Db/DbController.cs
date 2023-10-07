using MyApp.Logs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Db
{
    public class DbController<TDbConnection, TDbCommand> : IDbControl
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(IDbControl).Name ?? string.Empty; }

        /// <summary>
        /// DB接続文字列
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// DB接続変数
        /// </summary>
        private DbConnection _dbConnection { get; set; }

        /// <summary>
        /// DB実行コマンド
        /// </summary>
        private DbCommand _dbCommand;

        /// <summary>
        /// DBパラメーター
        /// </summary>
        private DbParameter? _dbParameter;

        /// <summary>
        /// DBトランザクション
        /// </summary>
        private DbTransaction? _dbTransaction;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="command"></param>
        public DbController(DbConnection connection, DbCommand command)
        {
            _dbConnection = connection;
            _dbCommand = command;
            _dbParameter = null;
            _dbTransaction = null;
        }

        /// <summary>
        /// コネクション確立
        /// </summary>
        public void Open()
        {
            try
            {
                _dbConnection?.Open();
                if (_dbConnection?.State == ConnectionState.Open)
                {
                    Log.Trace(_logFileName, "DB接続に成功しました。");
                }
            }
            catch(Exception e)
            {
                Log.Trace(_logFileName, "DB接続に失敗しました。");
            }
        }
        
        /// <summary>
        /// トランザクション開始
        /// </summary>
        public void TransactionStart()
        {
            _dbTransaction = _dbConnection.BeginTransaction();
        }

        /// <summary>
        /// SQLコマンド実行
        /// </summary>
        public void ExecuteNonQuery(string str)
        {
            _dbCommand.CommandText = str;
            _dbCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// トランザクションコミット
        /// </summary>
        public void TransactionCommit()
        {
            _dbTransaction?.Commit();
        }

        /// <summary>
        /// トランザクションロールバック
        /// </summary>
        public void TransactionRollback()
        {
            _dbTransaction?.Rollback();
        }

        public void Dispose()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
        }
    }
}
