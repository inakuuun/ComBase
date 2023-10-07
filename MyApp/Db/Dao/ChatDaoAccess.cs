using MyApp.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Db.Dao
{
    public class DbController
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(DbController).Name ?? string.Empty; }

        /// <summary>
        /// DBロジック
        /// </summary>
        DbLogic _dbLogic;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="obj"></param>
        public DbController(DbLogic obj)
        {
            _dbLogic = obj;
        }

        /// <summary>
        /// チャットテーブル INSERT
        /// </summary>
        public void ChatInsert()
        {
            _dbLogic.SQLCommand((control) =>
            {
                try
                {
                    //// トランザクション開始
                    //control.TransactionStart();

                    ////// 接続先やSQL実行に必要なインスタンスの作成
                    ////using (var connection = new SQLiteConnection(ConnectionString))
                    ////using (var command = connection.CreateCommand())
                    ////{
                    ////    // 接続開始
                    ////    connection.Open();

                    ////    // usersテーブルの作成
                    ////    command.CommandText =
                    ////    @"CREATE TABLE users("
                    ////     + "id int,"
                    ////     + "name varchar(10),"
                    ////     + "age int)";
                    ////    command.ExecuteNonQuery();

                    ////    // データの挿入
                    ////    command.CommandText = @"
                    ////    INSERT INTO users(id, name, age) VALUES(1, 'Mike', 30);
                    ////    INSERT INTO users(id, name, age) VALUES(2, 'Lisa', 24);
                    ////    INSERT INTO users(id, name, age) VALUES(3, 'Taro', 35);";
                    ////    command.ExecuteNonQuery();

                    ////    // データの抽出
                    ////    command.CommandText = "SELECT * FROM users";
                    ////    using (var reader = command.ExecuteReader())
                    ////    {
                    ////        while (reader.Read())
                    ////        {
                    ////            Console.WriteLine($"ID:{reader["id"]} 名前:{reader["name"]}　年齢:{reader["age"]}");
                    ////        }
                    ////    }
                    ////}

                    //string sql = @"INSERT INTO users(id, name, age) VALUES(1, 'Mike', 30);";

                    //// SQL実行
                    //control.ExecuteNonQuery(sql);

                    //// トランザクションコミット
                    //control.TransactionCommit();
                }
                catch(Exception ex)
                {
                    try
                    {
                        Log.Trace(_logFileName, $"SQL実行時異常 => {ex}");
                        control.TransactionRollback();
                    }
                    catch(Exception ex2)
                    {
                        Log.Trace(_logFileName, $"ロールバック実行時異常 => {ex}");
                    }
                }
            });
        }
    }
}
