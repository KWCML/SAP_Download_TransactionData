namespace SAP_Download_TransactionData.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_DN
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string DELIV_NUMB { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string DELIV_ITEM { get; set; }

        [StringLength(20)]
        public string CUSTOMER { get; set; }

        [StringLength(200)]
        public string CUST_NAME { get; set; }

        [StringLength(20)]
        public string SITECODE { get; set; }

        [StringLength(200)]
        public string SITE_ADDRC { get; set; }

        [StringLength(10)]
        public string PLANT { get; set; }

        [StringLength(20)]
        public string MATERIAL { get; set; }

        [StringLength(100)]
        public string SHORT_TEXT { get; set; }

        [StringLength(20)]
        public string MIX_CODE { get; set; }

        public DateTime? DELV_DATE { get; set; }

        [StringLength(20)]
        public string EXT_DOCKET { get; set; }

        [StringLength(20)]
        public string TRUCK_CODE { get; set; }

        public decimal? DELV_QTY { get; set; }

        [StringLength(10)]
        public string SALES_UNIT { get; set; }


        [StringLength(20)]
        public string SO_DOC { get; set; }

        [StringLength(10)]
        public string SO_ITM { get; set; }

        [StringLength(20)]
        public string CONTRACT { get; set; }

    }
}
