namespace SAP_Download_TransactionData.Context
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LINKSAP : DbContext
    {
        public LINKSAP()
            : base("name=LINKSAP")
        {
        }

        public virtual DbSet<T_DN> T_DN { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_DN>()
                .Property(e => e.DELIV_NUMB)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.DELIV_ITEM)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.CUSTOMER)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.CUST_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.SITECODE)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.SITE_ADDRC)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.PLANT)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.MATERIAL)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.SHORT_TEXT)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.MIX_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.DELV_DATE);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.EXT_DOCKET)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.TRUCK_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.DELV_QTY)
                .HasPrecision(18, 3);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.SALES_UNIT)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.SO_DOC)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.SO_ITM)
                .IsUnicode(false);

            modelBuilder.Entity<T_DN>()
                .Property(e => e.CONTRACT)
                .IsUnicode(false);

        }
    }
}
