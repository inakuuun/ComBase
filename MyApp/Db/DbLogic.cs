using MyApp.Db.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Db
{
    public class DbLogic : DbLogicBase
    {
        /// <summary>
        /// チャットDaoアクセスクラス
        /// </summary>
        public DbController ChatDaoAccess { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DbLogic()
        {
            ChatDaoAccess = new DbController(this);
        }

        /// <summary>
        /// SQL実行処理
        /// </summary>
        /// <param name="action"></param>
        public new void SQLCommand(Action<IDbControl> action)
        {
            base.SQLCommand(action);
        }
    }
}
