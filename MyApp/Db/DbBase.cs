using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace MyApp.Db
{
    public abstract class DbBase
    {
        protected virtual string ConnectionString { get; set; } = "";

        public virtual void Open()
        {
            try
            {
                // 接続先やSQL実行に必要なインスタンスの作成
                using (var connection = new SQLiteConnection(ConnectionString))
                using (var command = connection.CreateCommand())
                {
                    // 接続開始
                    connection.Open();

                    //// usersテーブルの作成
                    //command.CommandText =
                    //@"CREATE TABLE users("
                    // + "id int,"
                    // + "name varchar(10),"
                    // + "age int)";
                    //command.ExecuteNonQuery();

                    //// データの挿入
                    //command.CommandText = @"
                    //INSERT INTO users(id, name, age) VALUES(1, 'Mike', 30);
                    //INSERT INTO users(id, name, age) VALUES(2, 'Lisa', 24);
                    //INSERT INTO users(id, name, age) VALUES(3, 'Taro', 35);";
                    //command.ExecuteNonQuery();

                    // データの抽出
                    command.CommandText = "SELECT * FROM users";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID:{reader["id"]} 名前:{reader["name"]}　年齢:{reader["age"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
