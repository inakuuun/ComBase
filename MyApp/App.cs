using MyApp;
using MyApp.Common;
using MyApp.Init;
using MyApp.Threads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

_ = new SystemInit();

var bootMgr = new BootManager();
bootMgr.SystemStart();

//var dbFactory = new MyApp.Db.DbFactory(StractDef.Db.SQlite);
//dbFactory.Open();
