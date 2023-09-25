using MyApp.Threads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MyApp.Init
{
    public class BootManager
    {
        /// <summary>
        /// ProgramInfo.xml情報格納用ディクショナリ
        /// </summary>
        private static Dictionary<string, ThreadManager> _programInfoDic = new();

        public BootManager()
        {
            SetProgramInfo();
        }

        /// <summary>
        /// ProgramInfo.xmlで定義されているスレッドをディクショナリに格納
        /// </summary>
        public void SetProgramInfo()
        {
            // XMLファイルからXDocumentをインスタンス化
            XDocument? xd = XDocument.Load("./Config/ProgramInfo.xml");
            // ルート要素を取得
            XElement? info = xd.Element("ProgramInfo");
            if (info != null)
            {
                IEnumerable items = info.XPathSelectElements("Threads/Item");
                foreach (XElement item in items)
                {
                    if (item != null)
                    {
                        string key = item.Attribute("className")?.Value ?? string.Empty;
                        string value = item.Attribute("classPath")?.Value ?? string.Empty;

                        // クラス名からTypeオブジェクトを取得
                        Type? testThread = Type.GetType(value);
                        if (testThread != null)
                        {
                            // Typeオブジェクトを使用してクラスのインスタンスを生成
                            object? instance = Activator.CreateInstance(testThread);
                            if (instance != null && instance is ThreadManager obj)
                            {
                                // 同じキーが存在しない場合
                                if (!_programInfoDic.ContainsKey(key))
                                {
                                    // classNameをキーにして、スレッド実行体をディクショナリに格納
                                    _programInfoDic.Add(key, obj);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 定義されているスレッドを実行
        /// </summary>
        public void SystemStart()
        {
            // スレッドを実行するインスタンス分ループ処理
            foreach (ThreadManager instance in _programInfoDic.Values)
            {
                instance.ThreadStart();
            }
        }
    }
}
