using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAP_Download_TransactionData.Entity.EnumMaster;

namespace SAP_Download_TransactionData.Action
{
    public interface IActionBase
    {
        void Start();
        void Alert();
    }
}
