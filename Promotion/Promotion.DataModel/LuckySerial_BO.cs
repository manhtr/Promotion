using log4net;
using System;
using System.Linq;

namespace Promotion.DataModel
{
    public class LuckySerial_BO : IDisposable
    {
        ILog log = LogManager.GetLogger(typeof(LuckySerial_BO));
        PromotionEntities db = null;

        public LuckySerial_BO()
        {
            db = new PromotionEntities();
        }

        public IQueryable<LuckySerial> GetByInputTime(DateTime dtFrom , DateTime dtTo)
        {
            try
            {
                return db.LuckySerials.Where(o => o.input_time >= dtFrom && o.input_time <= dtTo).OrderByDescending(o=>o.input_time);
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
