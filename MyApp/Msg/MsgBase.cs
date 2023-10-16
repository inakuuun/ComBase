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
        /// <returns>プロパティ値をbyte配列に変換した値</returns>
        public abstract byte[] BytesRead();

        /// <summary>
        /// 変数ごとに確保するサイズを取得
        /// </summary>
        /// <param name="obj">サイズ取得対象インスタンス</param>
        /// <param name="size">加算時のサイズ</param>
        /// <returns>型サイズを加算した値</returns>
        protected int GetSize(object obj, int size = 0)
        {
            int calc = size;
            if (obj is string) return calc += 1024;
            if (obj is int) return calc += sizeof(int);
            if (obj is short) return calc += sizeof(short);
            if (obj is bool) return calc += sizeof(bool);
            return calc;
        }
    }
}
