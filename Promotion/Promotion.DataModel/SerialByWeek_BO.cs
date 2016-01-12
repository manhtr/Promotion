using log4net;
using System;
using System.Linq;

namespace Promotion.DataModel
{
    public class SerialByWeek_BO : IDisposable
    {
        ILog log = LogManager.GetLogger(typeof(SerialByWeek_BO));
        PromotionEntities db = null;

        public SerialByWeek_BO()
        {
            db = new PromotionEntities();
        }

        public IQueryable<SerialByWeek> GetTop(int top)
        {
            try
            {
                if (top > 0)
                {
                    return db.SerialByWeeks.OrderByDescending(o => o.effect_date).Take(top);
                }
                else
                {
                    return db.SerialByWeeks.OrderByDescending(o => o.effect_date);
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
