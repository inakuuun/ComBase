using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Tcp
{
    /// <summary>
    /// TCP接続情報クラス
    /// </summary>
    public class TcpConnectInfo
    {
        /// <summary>
        /// IPアドレス
        /// </summary>
        public IPAddress IpAddress = IPAddress.Loopback;

        /// <summary>
        /// ポート番号
        /// </summary>
        public int Port;

        /// <summary>
        /// 宛先ポート番号
        /// </summary>
        public int DestPort;

        /// <summary>
        /// ヘルスチェック間隔(ミリ秒)
        /// </summary>
        public int HelthCheckInterval;
    }
}
