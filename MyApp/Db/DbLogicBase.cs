using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace MyApp.Db
{
    /// <summary>
    /// DBロジック基底クラス
    /// </summary>
    public abstract class DbLogicBase
    {
        protected void SQLCommand(Action<IDbControl> action)
        {
            var ctrl = DbControllerFactory.GetControl();
            action(ctrl);
            ctrl.Dispose();
        }
    }
}
