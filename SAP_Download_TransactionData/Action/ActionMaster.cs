using SAP_Download_TransactionData.Context;
using SAP_Download_TransactionData.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SAP_Download_TransactionData.Entity.EnumMaster;
using log4net;

namespace SAP_Download_TransactionData.Action
{
    public abstract class ActionMaster : IActionBase
    {
        private static ILog log = LogManager.GetLogger("Main");

        public string environment = ConfigurationManager.AppSettings["Environment"];
        public static SAPHelper sap;
        public static DB db;

        public abstract void Start();

        public void Alert()
        {
            log.Info(string.Concat("End Console : ", DateTime.Now));
            log.Info("-------------- Process Completed --------------");
            //TODO Send Alert for any status 
        }
    }
}
