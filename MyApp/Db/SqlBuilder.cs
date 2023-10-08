using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Db
{
    /// <summary>
    /// SQL実行用文字列生成クラス
    /// </summary>
    public class SqlBuilder
    {
        /// <summary>
        /// 文字列管理用変数
        /// </summary>
        private StringBuilder _builder = new();

        /// <summary>
        /// SQL文追加
        /// </summary>
        /// <param name="sql"></param>
        public void Add(string sql)
        {
            _builder.AppendLine(sql);
        }
        
        /// <summary>
        /// パラメータを含むSQL文追加
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="objects"></param>
        public void Add(string sql, params object[] objects)
        {
            _builder.AppendLine(sql);
        }

        /// <summary>
        /// コマンド実行用SQL文を取得
        /// </summary>
        /// <returns></returns>
        public string GetCommandText()
        {
            return _builder.ToString();
        }
    }
}
