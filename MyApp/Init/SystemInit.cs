using MyApp.Common;
using MyApp.FileUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyApp.Init
{
    /// <summary>
    /// 起動準備クラス
    /// </summary>
    /// <remarks>プロパティの読み込みなど、システム起動時に必要な情報を事前にメモリに積んでおく</remarks>
    public class SystemInit
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SystemInit() 
        {
            try
            {
                // ログ出力先ディレクトリを作成
                // ※システム起動後一番最初にログ出力先ディレクトリを作成しておく
                Logs.Log.CreateLogDirectory();

                // DB定義ファイル設定
                PropertyReader.SetPropertyAll("./Config/DbProperties.xml");

                // データベース実行クラスを生成
                // ※DB定義ファイル情報取得後に実施する必要あり
                new MyApp.Db.DbControllerFactory(StractDef.DB.SQLite);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
