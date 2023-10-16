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
        public short MessageId { get => MsgDef.MSG_HELTHCHECK_REQ; }

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
        public override byte[] BytesRead()
        {
            var builder = new StringBuilder();
            builder.Append(this.MessageId);
            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        /// <summary>
        /// 電文長取得
        /// </summary>
        /// <returns>プロパティのサイズを全て加算した電文長</returns>
        private int GetLength()
        {
            int size = 0;
            size = base.GetSize(MessageId, size);
            return size;
        }
    }
}
