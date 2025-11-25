using SAP_Download_TransactionData.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSAPConnector;
using log4net;

namespace SAP_Download_TransactionData
{
    public class SAPHelper
    {
        //LINK SAP
        public IEnumerable<T_OUT_DN> T_OUT_DN { get; set; }
        private static ILog log = LogManager.GetLogger("Main");

        public void GetSAPDocketData(string environment)
        {
            try
            {
                using (var connection = new SapConnection(environment))
                {
                    connection.Open();
                    var command = new SapCommand("ZBAPI_GET_KWC_DN", connection);
                    //command.Parameters.Add("INIT_LOAD", ConfigurationManager.AppSettings["InitialLoad"]);

                    List<SapComplexStructureParameter> complex = new List<SapComplexStructureParameter>();
                    var p = new SapComplexStructureParameter();
                    p.Structure = "BLDAT_RAN";
                    p.Parameters.Add(new SapParameter("SIGN", "I"));
                    p.Parameters.Add(new SapParameter("OPTION", "BT"));

                    if (ConfigurationManager.AppSettings["Filter"] == "X")
                    {
                        p.Parameters.Add(new SapParameter("LOW", Convert.ToDateTime(ConfigurationManager.AppSettings["From"]).Date));
                        p.Parameters.Add(new SapParameter("HIGH", Convert.ToDateTime(ConfigurationManager.AppSettings["To"]).Date));

                        Console.WriteLine(string.Format("fromdate={0};todate={1}", ConfigurationManager.AppSettings["From"], ConfigurationManager.AppSettings["To"]));
                        log.Debug(string.Format("fromdate={0};todate={1}", ConfigurationManager.AppSettings["From"], ConfigurationManager.AppSettings["To"]));
                    }
                    else
                    {
                        DateTime FirstDayLastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 1); ;
                        DateTime LastDayLastMonth = new DateTime(FirstDayLastMonth.Year, FirstDayLastMonth.Month, DateTime.DaysInMonth(FirstDayLastMonth.Year, FirstDayLastMonth.Month));

                        DateTime DayBefore = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day);


                        if (DateTime.Now.Day == 15)
                        {
                            p.Parameters.Add(new SapParameter("LOW", FirstDayLastMonth.Date));
                            p.Parameters.Add(new SapParameter("HIGH", DayBefore.Date));

                            Console.WriteLine(string.Format("fromdate={0};todate={1}", FirstDayLastMonth, DayBefore));
                            log.Debug(string.Format("fromdate={0};todate={1}", FirstDayLastMonth, DayBefore));
                        }
                        else
                        {
                            p.Parameters.Add(new SapParameter("LOW", DayBefore.Date));
                            p.Parameters.Add(new SapParameter("HIGH", DayBefore.Date));

                            Console.WriteLine(string.Format("fromdate={0};todate={1}", DayBefore, DayBefore));
                            log.Debug(string.Format("fromdate={0};todate={1}", DayBefore, DayBefore));
                        }
                    }

                    complex.Add(p);
                    command.ComplexParameters.Add("DOC_DATE_RANGE", complex);

                    var sapDataReader = command.ExecuteDataSet();

                    T_OUT_DN = command.DataTableToEntities<T_OUT_DN>(sapDataReader.Tables["T_DN"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                log.Fatal(ex.Message);
            }

        }
    }
}
