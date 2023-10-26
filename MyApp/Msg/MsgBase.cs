using MyApp.Common;
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
        /// 電文ID
        /// </summary>
        public abstract short MessageId { get; }

        /// <summary>
        /// 電文メッセージ
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// メッセージ読み取りインスタンス
        /// </summary>
        protected MsgReader? MsgReader;

        /// <summary>
        /// メッセージ生成インスタンス
        /// </summary>
        protected MsgWriter? MsgWriter;

        /// <summary>
        /// 
        /// </summary>
        private byte[] _buffer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MsgBase() 
        {
            // 派生クラスで定義している全てのサイズで初期化
            _buffer = new byte[GetLength()];
            MsgWriter = new MsgWriter(_buffer);
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MsgBase(byte[] bytesMessage) 
        {
            _buffer = bytesMessage;
            MsgReader = new MsgReader(bytesMessage);
        }

        /// <summary>
        /// 電文データ取得
        /// </summary>
        /// <returns>プロパティ値をbyte配列に変換した値</returns>
        public byte[] BytesRead()
        {
            // メモリストリームからbyte配列を取得
            // https://yuzutan-hnk.hatenablog.com/entry/2017/05/29/020348
            if (MsgWriter != null)
            {
                // 0バイト目から派生クラスで定義している全てのサイズ分読み出す
                MsgWriter.Writer.BaseStream.Position = 0;
                _ = MsgWriter.Writer.BaseStream.Read(_buffer, 0, GetLength());
                MsgWriter.Dispose();
            }
            // 読み出したバイト配列を返却
            return _buffer;
        }

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
            else if (obj is bool) return calc += sizeof(bool);
            else if (obj is short) return calc += sizeof(short);
            else if (obj is int) return calc += sizeof(int);
            else if (obj is long) return calc += sizeof(long);
            else if (obj is byte) return calc += sizeof(byte);
            return calc;
        }

        /// <summary>
        /// 電文長取得
        /// </summary>
        /// <returns>プロパティのサイズを全て加算した電文長</returns>
        protected abstract int GetLength();
    }
}
