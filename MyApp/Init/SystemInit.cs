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
    public class SystemInit
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// プロパティの読み込みなど、システム起動時に必要な情報を事前にメモリに積んでおく
        /// </remarks>
        public SystemInit() 
        {
            try
            {
                // DB定義ファイル設定
                PropertyReader.SetPropertyAll("./Config/DbProperties.xml");
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
