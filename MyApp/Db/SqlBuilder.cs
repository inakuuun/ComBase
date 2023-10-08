using System;
using System.Collections.Generic;
using System.Data.Common;
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
        /// コントローラー保持用変数
        /// </summary>
        private IDbControl _control;

        /// <summary>
        /// DBパラメーターコレクション
        /// </summary>
        private DbParameterCollection _parameters;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="control"></param>
        public SqlBuilder(IDbControl control)
        {
            _control = control;
            _parameters = _control.GetDbParameterCollection();
        }

        /// <summary>
        /// SQL文追加
        /// </summary>
        /// <param name="sql"></param>
        public void Add(string sql)
        {
            // 文字列が空の場合は処理を終了
            if (string.IsNullOrEmpty(sql))
            {
                return;
            }
            // 構文置換処理
            sql = SyntaxReplace(sql);
            // SQL実行用文字列として格納
            _builder.AppendLine(sql);
        }

        /// <summary>
        /// パラメータを含むSQL文追加
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="objects"></param>
        public void Add(string sql, params object[] objects)
        {
            // 文字列が空の場合は処理を終了
            if (string.IsNullOrEmpty(sql))
            {
                return;
            }
            // 構文置換処理
            sql = SyntaxReplace(sql);

            // TODO：SQL文から対象文字を抽出するために考える必要あり
            //// ループ処理を実施
            //foreach (object obj in objects)
            //{
            //    // コントローラーのパラメーターインスタンス取得
            //    // ※パラメーターを設定する度にインスタンス化する必要あり
            //    var param = _control.GetDbParameter();
            //    // 数値型の場合
            //    if (long.TryParse(obj.ToString(), out long result))
            //    {
            //        if (!_parameters.Contains(":id"))
            //        {
            //            // シングルクォーテーションを付けずに設定
            //            param.ParameterName = ":id";
            //            param.Value = result;
            //            _parameters.Add(param);
            //        }
            //    }
            //    // 数値型以外の場合
            //    else
            //    {
            //        if (!_parameters.Contains(":id"))
            //        {
            //            // シングルクォーテーションを付けて設定
            //            param.ParameterName = ":id";
            //            param.Value = $@"'{result}'";
            //            _parameters.Add(param);
            //        }
            //    }
            //}
            // SQL実行用文字列として格納
            _builder.AppendLine(sql);
        }

        /// <summary>
        /// 構文置換処理
        /// </summary>
        /// <remarks>SQL文に含みたくない文字列の置換を実施</remarks>
        /// <returns>置換後文字列</returns>
        public string SyntaxReplace(string sql)
        {
            // 前後空白スペース削除
            string result = sql.Trim();
            // 文字をエスケープ
            // ※「@」をつけることで、取得した値を全てただの文字列として扱うことができる
            result = $@"{result}";
            return result;
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
