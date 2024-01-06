#### プロジェクト概要
- 開発期間
  - 2023年9月17日～2023年11月26日
- 説明
  - アプリケーションで使う機能を汎用的に使えるように独自ライブラリ化

#### 機能
- Db（DB操作系）
  - https://github.com/inakuuun/MyApp/tree/main/MyApp/Db  
- Events（イベント系）
  - https://github.com/inakuuun/MyApp/tree/main/MyApp/Events  
- FileUtil（ファイル操作系）
  - https://github.com/inakuuun/MyApp/tree/main/MyApp/FileUtil  
- Init（初期処理系）
  - https://github.com/inakuuun/MyApp/tree/main/MyApp/Init  
- Logs（ログ機能系）
  - https://github.com/inakuuun/MyApp/tree/main/MyApp/Logs  
- Tcp（TCP通信系）
  - https://github.com/inakuuun/MyApp/tree/main/MyApp/Tcp  
- Threads（スレッド系）
  - https://github.com/inakuuun/MyApp/tree/main/MyApp/Threads  
- Udp（UDP通信系）
  - https://github.com/inakuuun/MyApp/tree/main/MyApp/Udp

#### ディレクトリ構成
```
MyApp
│   ├── App.cs
│   ├── Common
│   │   ├── CommonDef.cs
│   │   ├── FunctionDef.cs
│   │   ├── PropertyDef.cs
│   │   └── StractDef.cs
│   ├── Config
│   │   ├── DbProperties.xml
│   │   └── ProgramInfo.xml
│   ├── Db
│   │   ├── Dao
│   │   │   └── ChatDaoAccess.cs
│   │   ├── DbController.cs
│   │   ├── DbControllerFactory.cs
│   │   ├── DbControllerInfo.cs
│   │   ├── DbLogic.cs
│   │   ├── DbLogicBase.cs
│   │   ├── IDbControl.cs
│   │   ├── README.md
│   │   ├── SqlBuilder.cs
│   │   └── SqlReader.cs
│   ├── Events
│   │   └── MessageEventArgs.cs
│   ├── FileUtil
│   │   ├── FileUtil.cs
│   │   ├── PropertyReader.cs
│   │   └── README.md
│   ├── Init
│   │   ├── BootManager.cs
│   │   ├── README.md
│   │   └── SystemInit.cs
│   ├── Logs
│   │   ├── Log.cs
│   │   └── README.md
│   ├── Msg
│   │   ├── Deffine
│   │   │   └── MsgDef.cs
│   │   ├── Messages
│   │   │   ├── BootStartReq.cs
│   │   │   ├── HelthCheckReq.cs
│   │   │   └── HelthCheckRes.cs
│   │   ├── MsgBase.cs
│   │   ├── MsgReader.cs
│   │   └── MsgWriter.cs
│   ├── MyApp.csproj
│   ├── Tcp
│   │   ├── README.md
│   │   ├── TcpBase.cs
│   │   ├── TcpClientBase.cs
│   │   ├── TcpConnectInfo.cs
│   │   ├── TcpController.cs
│   │   ├── TcpControllerInfo.cs
│   │   └── TcpServerBase.cs
│   ├── TestTcpClient.cs
│   ├── TestTcpServer.cs
│   ├── TestThread.cs
│   ├── TestThread2.cs
│   ├── TestUdpClient.cs
│   ├── TestUdpServer.cs
│   ├── Threads
│   │   ├── README.md
│   │   ├── ThreadBase.cs
│   │   └── ThreadManager.cs
│   └── Udp
│       ├── UdpBase.cs
│       ├── UdpClientBase.cs
│       ├── UdpConnectInfo.cs
│       ├── UdpController.cs
│       ├── UdpControllerInfo.cs
│       └── UdpServerBase.cs
├── MyApp.sln
└── README.md
```
