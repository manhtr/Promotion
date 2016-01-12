using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promotion.DataModel
{
    public class COMPANY_BO : IDisposable
    {
        ILog log = LogManager.GetLogger(typeof(COMPANY_BO));
        PromotionEntities db = null;

        public COMPANY_BO()
        {
            db = new PromotionEntities();
        }

        public IQueryable<COMPANY> GetAll()
        {
            try
            {
                return db.COMPANies.OrderBy(o => o.COMPANY_NAME);
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return null;
            }
        }

        public COMPANY GetByCOMPANY_CODE(string strCOMPANY_CODE)
        {
            try
            {
                if (!string.IsNullOrEmpty(strCOMPANY_CODE))
                {
                    strCOMPANY_CODE = strCOMPANY_CODE.Trim().ToUpper(); 
                }
                return db.COMPANies.FirstOrDefault(o => o.COMPANY_CODE == strCOMPANY_CODE);
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
