using MyApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Msg.Messages
{
    /// <summary>
    /// ヘルスチェック要求メッセージクラス
    /// </summary>
    public class HelthCheckReq : MsgBase
    {
        /// <summary>
        /// 電文ID
        /// </summary>
        public short MessageId = MsgDef.MSG_HELTHCHECK_REQ;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HelthCheckReq()
        {
            byte[] result = new byte[GetLength()];
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HelthCheckReq(HelthCheckReq req)
        {

        }

        /// <summary>
        /// 電文データ取得
        /// </summary>
        /// <returns>プロパティ値をbyte配列に変換した値</returns>
        public override byte[] Read()
        {
            var builder = new StringBuilder();
            builder.Append(this.MessageId);
            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        /// <summary>
        /// 電文長取得
        /// </summary>
        /// <returns></returns>
        private int GetLength()
        {
            int size = 0;
            size = GetSize(MessageId);
            return size;
        }

        /// <summary>
        /// 変数ごとに確保するサイズを取得
        /// </summary>
        /// <param name="variable">変数</param>
        /// <returns></returns>
        private int GetSize(object variable, int size = 0)
        {
            int result = size;
            if (variable is string) return result += 1024;
            if (variable is int) return result += sizeof(int);
            if (variable is short) return result += sizeof(short);
            if (variable is bool) return result += sizeof(bool);
            return result;
        }
    }
}
