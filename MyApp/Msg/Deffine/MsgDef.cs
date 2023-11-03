using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Msg.Deffine
{
    /// <summary>
    /// 電文メッセージ定義クラス
    /// </summary>
    public static class MsgDef
    {
        /// <summary>ヘルスチェック要求</summary>
        public const short MSG_HELTHCHECK_REQ = 0;
        /// <summary>初期起動通知要求</summary>
        public const short MSG_BOOTSTART_REQ = 1;

        /// <summary>初期起動通知応答</summary>
        public const short MSG_BOOTSTART_RES = 21;

        /// <summary>システム起動完了通知</summary>
        public const short MSG_SYSTEMBOOT_NOTICE = 41;
    }
}
