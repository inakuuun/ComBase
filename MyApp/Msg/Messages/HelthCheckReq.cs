using MyApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public HelthCheckReq() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="message"></param>
        public HelthCheckReq(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetMaxLength()
        {
            int size = 0;
            size = GetSize(Message, size);
            return size;
        }

        /// <summary>
        /// 変数ごとに確保するサイズを取得
        /// </summary>
        /// <param name="variable">変数</param>
        /// <returns></returns>
        private int GetSize(object variable, int size)
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
