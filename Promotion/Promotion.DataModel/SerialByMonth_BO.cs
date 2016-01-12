using log4net;
using System;
using System.Linq;

namespace Promotion.DataModel
{
    public class SerialByMonth_BO : IDisposable
    {
        ILog log = LogManager.GetLogger(typeof(SerialByMonth_BO));
        PromotionEntities db = null;

        public SerialByMonth_BO()
        {
            db = new PromotionEntities();
        }

        public IQueryable<SerialByMonth> GetTop(int top)
        {
            try
            {
                if (top > 0)
                {
                    return db.SerialByMonths.OrderByDescending(o => o.effect_date).Take(top);
                }
                else
                {
                    return db.SerialByMonths.OrderByDescending(o => o.effect_date);
                }
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

