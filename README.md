## MyApp
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
- PropertyDef.cs
  - 定義ファイルをプロパティとして格納するクラス  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Common/PropertyDef.cs
- StractDef.cs
  - 構造体系をまとめるクラス  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Common/StractDef.cs
- DbProperties.xml
  - DB接続時に使用する値を定義  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Config/DbProperties.xml
- ProgramInfo.xml
  - 定義が必要なクラスを定義  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Config/ProgramInfo.xml
- DbControllerInfo.cs
  - DB接続時に必要な情報を保持するクラス  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Db/DbControllerInfo.cs
- DbController.cs
  - DB操作(接続、処理の実行、トランザクション)で必要なリソースを用意  
    => ※TODO：これから作成
- DbFactory.cs
  - DBロジッククラスで利用する必要なリソースを用意  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Db/DbFactory.cs
- DbLogicBase.cs
  - DBロジッククラスの必要な処理を抽象化  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Db/DbLogicBase.cs
- IDbController.cs
  - DB操作に必要な処理を持つインターフェース  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Db/IDbController.cs
- FileUtil.cs
  - 汎用的なファイル操作系の処理  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/FileUtil/FileUtil.cs
- PropertyReader.cs
  - プロパティ定義ファイル情報を管理するクラス  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/FileUtil/PropertyReader.cs
- BootManager.cs
  - スレッドを実行するクラス  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Init/BootManager.cs
- SystemInit.cs
  - システムで使用するリソースを事前準備(格納など)する処理クラス  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Init/SystemInit.cs
- Log.cs
  - ログ生成クラス  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Logs/Log.cs
- TcpBase.cs
  - TCPに必要な処理を用意した抽象クラス  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Tcp/TcpBase.cs
- TcpClientBase.cs
  - クライアントのTCP接続で必要な処理を用意した抽象クラス  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Tcp/TcpClientBase.cs
- TcpServerBase.cs
  - サーバーのTCP接続で必要な処理を用意した抽象クラス  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Tcp/TcpServerBase.cs
- ThreadBase.cs
  - スレッド処理に必要な処理を用意する抽象クラス  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Threads/ThreadBase.cs
- ThreadManager.cs
  - 拡張可能なスレッド管理クラス  
    => https://github.com/inakuuun/MyApp/blob/main/MyApp/Threads/ThreadManager.cs


