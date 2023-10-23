using MyApp.Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Events
{
    /// <summary>
    /// メッセージイベント
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// 電文ID
        /// </summary>
        public short MessageId { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="msg"></param>
        public MessageEventArgs(MsgBase msg)
        {
            MessageId = msg.MessageId;
        }
    }
}
