using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promotion.DataModel
{
    public class BLACKLIST_CUSTOMER_BO : IDisposable
    {
        ILog log = LogManager.GetLogger(typeof(BLACKLIST_CUSTOMER_BO));
        PromotionEntities db = null;

        public BLACKLIST_CUSTOMER_BO()
        {
            db = new PromotionEntities();
        }

        public BLACKLIST_CUSTOMER GetByCUSTOMER_ID(string strCUSTOMER_ID)
        {
            try
            {
                if(!string.IsNullOrEmpty(strCUSTOMER_ID))
                {
                    strCUSTOMER_ID = strCUSTOMER_ID.Trim();
                }
                return db.BLACKLIST_CUSTOMER.FirstOrDefault(o => o.CUSTOMER_ID == strCUSTOMER_ID);
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return null;
            }
        }

        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }
    }
}
