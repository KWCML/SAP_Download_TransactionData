using SAP_Download_TransactionData.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAP_Download_TransactionData.Entity.EnumMaster;

namespace SAP_Download_TransactionData
{
    public class DB
    {
        public IEnumerable<T_DN> T_DN { get; set; }

        public DB(ActionName action)
        {
            switch (action)
            {
                case ActionName.DocketData:
                    LinkSap();
                    break;
            }
        }

        private void MAM()
        {
        }

        private void LinkSap()
        {
            using (var context = new LINKSAP())
            {
                T_DN = context.T_DN.ToList();
            }
        }
    }
}
