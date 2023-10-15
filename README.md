## MyApp
#### プロジェクト概要
- 2023年9月17日～プロジェクト始動

#### 環境
- Windows 10 Pro
- Visual Studio 2022
- .NET6.0
#### ディレクトリ構成
```
App.cs
├── Common
│   ├── PropertyDef.cs
│   └── StractDef.cs
├── Config
│   ├── DbProperties.xml
│   └── ProgramInfo.xml
├── Db
│   ├── DbLogicBase.cs
│   ├── DbControllerInfo.cs
│   ├── DbFactory.cs
│   └── IDbController.cs
├── FileUtil
│   ├── FileUtil.cs
│   └── PropertyReader.cs
├── Init
│   ├── BootManager.cs
│   └── SystemInit.cs
├── Logs
│   └── Log.cs
├── MyApp.csproj
├── Tcp
│   ├── TcpBase.cs
│   ├── TcpClientBase.cs
│   └── TcpServerBase.cs
├── TestTcpClient.cs
├── TestTcpServer.cs
├── TestThread.cs
├── TestThread2.cs
└── Threads
    ├── ThreadBase.cs
    └── ThreadManager.cs
```

#### 機能の概要
- Db（DB操作系）
  - https://github.com/inakuuun/MyApp/tree/main/MyApp/Db  
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
