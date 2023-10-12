using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Tcp
{
    public class TcpConnectInfo
    {
        /// <summary>
        /// IPアドレス
        /// </summary>
        /// <returns></returns>
        public IPAddress IpAddress = IPAddress.Loopback;

        /// <summary>
        /// ポート番号
        /// </summary>
        /// <returns></returns>
        public int Port;

        /// <summary>
        /// 宛先ポート番号
        /// </summary>
        /// <returns></returns>
        public int DestPort;
    }
}
