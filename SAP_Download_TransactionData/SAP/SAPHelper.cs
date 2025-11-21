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

                    if (ConfigurationManager.AppSettings["Filter"] == "X")
                    {
                        List<SapComplexStructureParameter> complex = new List<SapComplexStructureParameter>();
                        var p = new SapComplexStructureParameter();
                        p.Structure = "BLDAT_RAN";
                        p.Parameters.Add(new SapParameter("SIGN", "I"));
                        p.Parameters.Add(new SapParameter("OPTION", "BT"));
                        p.Parameters.Add(new SapParameter("LOW", Convert.ToDateTime(ConfigurationManager.AppSettings["From"]).Date));
                        p.Parameters.Add(new SapParameter("HIGH", Convert.ToDateTime(ConfigurationManager.AppSettings["To"]).Date));
                        complex.Add(p);
                        command.ComplexParameters.Add("DOC_DATE_RANGE", complex);
                    }


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
