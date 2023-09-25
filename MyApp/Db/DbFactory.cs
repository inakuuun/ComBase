using MyApp.Common;
using MyApp.FileUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Db
{
    public class DbFactory : DbBase
    {
        protected override string ConnectionString { set => base.ConnectionString = value; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DbFactory(StractDef.Db db)
        {
            if (db == StractDef.Db.SQlite)
            {
                // dbディレクトリが存在しない場合は作成
                // 実行環境に生成される
                if (!Directory.Exists("./db"))
                {
                    Directory.CreateDirectory("./db");
                }
                ConnectionString = PropertyReader.GetProperty(PropertyDef.Prop_ConnectionString);
            }
        }
    }
}
