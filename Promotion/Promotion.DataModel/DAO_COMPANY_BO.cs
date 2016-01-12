using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promotion.DataModel
{
    public class DAO_COMPANY_BO : IDisposable
    {
        ILog log = LogManager.GetLogger(typeof(DAO_COMPANY_BO));
        PromotionEntities db = null;

        public DAO_COMPANY_BO()
        {
            db = new PromotionEntities();
        }

        public DAO_COMPANY GetByCOMPANY_CODE(string strCOMPANY_CODE)
        {
            try
            {
                if (!string.IsNullOrEmpty(strCOMPANY_CODE))
                {
                    strCOMPANY_CODE = strCOMPANY_CODE.Trim().ToUpper();
                }
                return db.DAO_COMPANY.FirstOrDefault(o => o.COMPANY_CODE == strCOMPANY_CODE);
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return null;
            }
        }

        public IQueryable<DAO_COMPANY> GetByDAO_CODE(string strDAO_CODE)
        {
            try
            {
                if (!string.IsNullOrEmpty(strDAO_CODE))
                {
                    strDAO_CODE = strDAO_CODE.Trim().ToUpper();
                }
                return db.DAO_COMPANY.Where(o => o.DAO_CODE == strDAO_CODE);
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
