using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Msg
{
    /// <summary>
    /// メッセージ基底クラス
    /// </summary>
    public abstract class MsgBase : System.EventArgs
    {
        /// <summary>
        /// 電文メッセージ
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MsgBase() { }

        /// <summary>
        /// 電文データ取得
        /// </summary>
        /// <returns></returns>
        public abstract byte[] Read();
    }
}
