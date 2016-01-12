using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promotion.DataModel
{
    public class STAFF_USER_BO : IDisposable
    {

        ILog log = LogManager.GetLogger(typeof(STAFF_USER_BO));
        PromotionEntities db = null;

        public STAFF_USER_BO()
        {
            db = new PromotionEntities();
        }

        public IQueryable<STAFF_USER> GetAll()
        {
            try
            {
                return db.STAFF_USER.OrderBy(o => o.HO_TEN);
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return null;
            }
        }

        public STAFF_USER GetByUSERNAME(string strUSERNAME)
        {
            try
            {
                if (!string.IsNullOrEmpty(strUSERNAME))
                {
                    strUSERNAME = strUSERNAME.Trim().ToLower();
                }
                return db.STAFF_USER.FirstOrDefault(o => o.USERNAME == strUSERNAME);
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
