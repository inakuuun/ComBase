using MyApp.Tcp;
using MyApp.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static MyApp.Common.StractDef;

namespace MyApp
{
    public class TestTcpClient : TcpClientBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => base.ThreadName ?? string.Empty; }

        /// <summary>
        /// 接続先IPアドレス
        /// </summary>
        /// <returns></returns>
        private IPAddress? _ipAddress;

        /// <summary>
        /// 宛先ポート番号
        /// </summary>
        /// <returns></returns>
        private int _portNum;

        protected override bool RunInit()
        {
            //throw new NotImplementedException();
            // 接続先IPアドレス
            _ipAddress = IPAddress.Parse("127.0.0.1");
            // 宛先ポート番号
            _portNum = 50000;
            ConnectStart();
            // 宛先ポート番号
            return true;
        }

        public void ConnectStart()
        {
            try
            {
                base.ConnectStart(_ipAddress, _portNum);
            }
            catch (Exception e)
            {
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"異常終了 => { e }");
            }
        }

        /// <summary>
        /// 接続解除
        /// </summary>
        public void TcpClose()
        {
            base.Close();
        }
    }
}
