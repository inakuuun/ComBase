using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyApp.Msg.Deffine;

namespace MyApp.Msg.Messages
{
    /// <summary>
    /// 初期起動通知要求メッセージクラス
    /// </summary>
    public class BootStartReq : MsgBase
    {
        /// <summary>
        /// 電文ID
        /// </summary>
        public override short MessageId { get => _messageId; }
        private short _messageId = MsgDef.MSG_BOOTSTART_REQ;

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// ユーザー名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// ユーザーIPアドレス
        /// </summary>
        public string UserIp { get; set; } = string.Empty;

        /// <summary>
        /// メッセージ読み取りインスタンス
        /// </summary>
        private MsgReader? _msgReader
        { 
            get =>  base.MsgReader;
            set => base.MsgReader = value;
        }

        /// <summary>
        /// メッセージ生成インスタンス
        /// </summary>
        private MsgWriter? _msgWriter 
        {
            get => base.MsgWriter; 
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BootStartReq() : base()
        {
            if(_msgWriter != null)
            {
                _msgWriter.WtShort(_messageId);
                _msgWriter.WtStr(UserId);
                _msgWriter.WtStr(UserName);
                _msgWriter.WtStr(UserIp);
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BootStartReq(byte[] bytesmessage) : base(bytesmessage)
        {
            if(_msgReader != null)
            {
                _messageId = _msgReader.RdShort();
                UserId = _msgReader.RdStr();
                UserName = _msgReader.RdStr();
                UserIp = _msgReader.RdStr();
            }
        }

        /// <summary>
        /// 電文長取得
        /// </summary>
        /// <returns>プロパティのサイズを全て加算した電文長</returns>
        protected override sealed int GetLength()
        {
            int size = 0;
            size = base.GetSize(_messageId, size);
            size = base.GetSize(UserId, size);
            size = base.GetSize(UserName, size);
            size = base.GetSize(UserIp, size);
            return size;
        }
    }
}
