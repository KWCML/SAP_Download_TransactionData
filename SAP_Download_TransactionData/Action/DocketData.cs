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
    public class DocketData : ActionMaster
    {
        private static ILog log = LogManager.GetLogger("Main");
        Thread th_docket = null;

        public DocketData()
        {
            log.Info("Start Process : Docket Data");
            sap = new SAPHelper();
            db = new DB(ActionName.DocketData);
        }

        public override void Start()
        {
            try
            {
                Console.WriteLine(string.Concat("Starting : ", DateTime.Now));
                log.Info(string.Concat("Starting : ", DateTime.Now));

                sap.GetSAPDocketData(environment);

                th_docket = new Thread(() => Docket());
                th_docket.Start();
                th_docket.Join();
                Console.WriteLine(string.Concat("End : ", DateTime.Now));
                log.Info(string.Concat("End : ", DateTime.Now));
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        static void Docket()
        {
            using (var _context = new LINKSAP())
            {
                var new_docket = (from s in sap.T_OUT_DN
                                     join localDB in db.T_DN on new
                                     {
                                         DELIV_NUMB = s.DELIV_NUMB,
                                         DELIV_ITEM = s.DELIV_ITEM
                                     } equals new
                                     {
                                         DELIV_NUMB = localDB.DELIV_NUMB,
                                         DELIV_ITEM = localDB.DELIV_ITEM
                                     } into SAPOnly
                                     where SAPOnly.Count() == 0
                                     from localDB in SAPOnly.DefaultIfEmpty()
                                     group s by new
                                     {
                                         s.DELIV_NUMB,
                                         s.DELIV_ITEM
                                     }
                                     into g
                                     select g.First()).ToArray();
                foreach (var item in new_docket)
                {
                    Console.WriteLine("Inserting New Docket:" + item.DELIV_NUMB);
                    log.Info("Inserting New Docket:" + item.DELIV_NUMB);

                    var n = new T_DN()
                    {
                        DELIV_NUMB = item.DELIV_NUMB,
                        DELIV_ITEM = item.DELIV_ITEM,
                        CUSTOMER = item.CUSTOMER,
                        CUST_NAME = item.CUST_NAME,
                        SITECODE = item.SITECODE,
                        SITE_ADDRC = item.SITE_ADDRC,
                        PLANT = item.PLANT,
                        MATERIAL = item.MATERIAL,
                        SHORT_TEXT = item.SHORT_TEXT,
                        MIX_CODE = item.MIX_CODE,
                        DELV_DATE = ConvertToDate(item.DELV_DATE),
                        EXT_DOCKET = item.EXT_DOCKET,
                        TRUCK_CODE = item.TRUCK_CODE,
                        DELV_QTY = ConvertToNumber(item.DELV_QTY),
                        SALES_UNIT = item.SALES_UNIT,
                        SO_DOC = item.SO_DOC,
                        SO_ITM = item.SO_ITM,
                        CONTRACT = item.CONTRACT,
                    };
                    _context.T_DN.Add(n);
                    _context.SaveChanges();
                }

                var update_docket = (from p in sap.T_OUT_DN
                                        join q in db.T_DN on new
                                        {
                                            DELIV_NUMB = p.DELIV_NUMB,
                                            DELIV_ITEM = p.DELIV_ITEM
                                        } equals new
                                        {
                                            DELIV_NUMB = q.DELIV_NUMB,
                                            DELIV_ITEM = q.DELIV_ITEM
                                        }
                                        where p.CUSTOMER != q.CUSTOMER
                                        || p.CUST_NAME != q.CUST_NAME
                                        || p.SITECODE != q.SITECODE
                                        || p.SITE_ADDRC != q.SITE_ADDRC
                                        || p.PLANT != q.PLANT
                                        || p.MATERIAL != q.MATERIAL
                                        || p.SHORT_TEXT != q.SHORT_TEXT
                                        || p.MIX_CODE != q.MIX_CODE
                                        || ConvertToDate(p.DELV_DATE) != q.DELV_DATE
                                        || p.EXT_DOCKET != q.EXT_DOCKET
                                        || p.TRUCK_CODE != q.TRUCK_CODE
                                        || ConvertToNumber(p.DELV_QTY) != q.DELV_QTY
                                        || p.SALES_UNIT != q.SALES_UNIT
                                        || p.SO_DOC != q.SO_DOC
                                        || p.SO_ITM != q.SO_ITM
                                        || p.CONTRACT != q.CONTRACT
                                        group p by new
                                        {
                                            p.DELIV_NUMB,
                                            p.DELIV_ITEM
                                        }
                                        into g
                                        select g.First()).ToArray();

                foreach (var item in update_docket)
                {
                    Console.WriteLine("Updating docket:" + item.DELIV_NUMB);
                    log.Info("Updating docket:" + item.DELIV_NUMB);

                    var n = _context.T_DN.Where(x => x.DELIV_NUMB == item.DELIV_NUMB && x.DELIV_ITEM == item.DELIV_ITEM).FirstOrDefault();
                    if (n == null) continue;
                        n.CUSTOMER = item.CUSTOMER;
                        n.CUST_NAME = item.CUST_NAME;
                        n.SITECODE = item.SITECODE;
                        n.SITE_ADDRC = item.SITE_ADDRC;
                        n.PLANT = item.PLANT;
                        n.MATERIAL = item.MATERIAL;
                        n.SHORT_TEXT = item.SHORT_TEXT;
                        n.MIX_CODE = item.MIX_CODE;
                        n.DELV_DATE = ConvertToDate(item.DELV_DATE);
                        n.EXT_DOCKET = item.EXT_DOCKET;
                        n.TRUCK_CODE = item.TRUCK_CODE;
                        n.DELV_QTY = ConvertToNumber(item.DELV_QTY);
                        n.SALES_UNIT = item.SALES_UNIT;
                        n.SO_DOC = item.SO_DOC;
                        n.SO_ITM = item.SO_ITM;
                        n.CONTRACT = item.CONTRACT;
                        _context.SaveChanges();
                }
            }


        }


        private static DateTime? ConvertToDate(string date)
        {
            if (!date.Contains("-") && !string.IsNullOrEmpty(date))
            {
                date = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
            }

            DateTime datetime;
            if (DateTime.TryParse(date, out datetime))
            {
                return datetime;
            }

            return null;
        }

        private static Decimal ConvertToNumber(string number)
        {
            Decimal num;

            if (Decimal.TryParse(number, out num))
            {
                return num;
            }

            return Convert.ToDecimal(0.0);
        }

        private static int ConvertToInt(string number)
        {
            int num;

            if (int.TryParse(number, out num))
            {
                return num;
            }

            return Convert.ToInt32(0);
        }
    }
}
