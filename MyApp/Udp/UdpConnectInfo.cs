using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Udp
{
    /// <summary>
    /// UDP接続情報クラス
    /// </summary>
    public class UdpConnectInfo
    {
        /// <summary>
        /// IPアドレス
        /// </summary>
        public string IpAddress { get; set; } = string.Empty;

        /// <summary>
        /// サーバーポート
        /// </summary>
        public short ServerPort { get; set; }

        /// <summary>
        /// ポート
        /// </summary>
        public short Port { get; set; }
    }
}
