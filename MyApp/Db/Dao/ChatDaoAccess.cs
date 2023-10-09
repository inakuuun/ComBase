using MyApp.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Db.Dao
{
    public class ChatDaoAccess
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(ChatDaoAccess).Name ?? string.Empty; }

        /// <summary>
        /// DBロジック
        /// </summary>
        DbLogic _dbLogic;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="obj"></param>
        public ChatDaoAccess(DbLogic obj)
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
                    var insert = new SqlBuilder(control);

                    // トランザクション開始
                    control.TransactionStart();

                    insert.Add("INSERT INTO users (");
                    insert.Add("  id");
                    insert.Add(", name");
                    insert.Add(", age");
                    insert.Add(") VALUES (");
                    insert.Add(" :id", 3);
                    insert.Add(",:name", "KATSUO");
                    insert.Add(",:age);", 42);

                    // SQL実行
                    control.ExecuteNonQuery(insert);

                    // トランザクションコミット
                    control.TransactionCommit();

                    var br = new SqlBuilder(control);
                    br.Add("SELECT * FROM users");
                    br.Add("WHERE id = :id OR id = :id2 OR id = :id3;", 1, 2, 3);

                    // SQL実行
                    var result = control.ExecuteReader(br);

                    // 実行結果を取得
                    using var rd = new SqlReader(result);
                    while (rd.Reader.Read())
                    {
                        short id = rd.ToShort("id");
                        string name = rd.ToStr("name");
                        int age = rd.ToInt("age");

                        Console.WriteLine($"ID:{id} 名前:{name}　年齢:{age}");
                    }

                }
                catch (Exception ex)
                {
                    Log.Trace(_logFileName, $"SQL実行時異常 => {ex}");
                    control.TransactionRollback();
                }
            });
        }
    }
}
