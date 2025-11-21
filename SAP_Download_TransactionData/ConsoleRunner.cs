using SAP_Download_TransactionData.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAP_Download_TransactionData.Entity.EnumMaster;
using log4net;

namespace SAP_Download_TransactionData
{
    public class ConsoleRunner
    {
        private static ILog log = LogManager.GetLogger("Main");
        public void Run(string action)
        {
            IActionBase iAction = null;

            //Routing
            iAction = SAPConsoleRounting(action);

            //Start
            Start(iAction);
        }

        public IActionBase SAPConsoleRounting(string action)
        {
            try
            {
                //+ TODO Convert to Queue
                IActionBase iAction = null;
                var actionName = (ActionName)Enum.Parse(typeof(ActionName), action, true);
                //- TODO Convert to Queue
                switch (actionName)
                {
                    case ActionName.DocketData:
                        iAction = new DocketData();
                        break;
                }

                return iAction;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                log.Warn(ex.Message);
                return null;
            }
        }

        public void Start(IActionBase action)
        {
            if (action != null)
            {
                action.Start();
                action.Alert();
            }
        }
    }
}
