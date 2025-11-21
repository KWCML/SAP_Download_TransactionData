using SAP_Download_TransactionData.Action;
using System;
using System.Linq;
using static SAP_Download_TransactionData.Entity.EnumMaster;
using log4net;

namespace SAP_Download_TransactionData
{
    class Program
    {
        private static ILog log = LogManager.GetLogger("Main");
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            var consoleRunner = new ConsoleRunner();

            if (args != null)
                consoleRunner.Run(args.First());
            else
                Console.WriteLine("No Request");
        }
    }
}
