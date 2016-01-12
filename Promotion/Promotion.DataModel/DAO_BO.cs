using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promotion.DataModel
{
    public class DAO_BO : IDisposable
    {
        ILog log = LogManager.GetLogger(typeof(DAO_BO));
        PromotionEntities db = null;

        public DAO_BO()
        {
            db = new PromotionEntities();
        }

        public DAO GetByDAO_CODE(string strDAO_CODE)
        {
            try
            {
                if (!string.IsNullOrEmpty(strDAO_CODE))
                {
                    strDAO_CODE = strDAO_CODE.Trim();
                }
                return db.DAOs.FirstOrDefault(o => o.DAO_CODE == strDAO_CODE);
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
