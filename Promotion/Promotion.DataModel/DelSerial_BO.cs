using log4net;
using System;
using System.Linq;

namespace Promotion.DataModel
{
    public class DelSerial_BO : IDisposable
    {
        ILog log = LogManager.GetLogger(typeof(DelSerial_BO));
        PromotionEntities db = null;

        public DelSerial_BO()
        {
            db = new PromotionEntities();
        }


        public IQueryable<DelSerial> GetByDatTime(DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                return db.DelSerials.Where(o => o.input_time >= dtFrom && o.input_time <= dtTo).OrderBy(o => o.Serial);
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return null;
            }
        }

        public void DeleteDelSerial(long id)
        {
            try
            {
                DelSerial objDelSerial = db.DelSerials.FirstOrDefault(o => o.Id == id);
                if (objDelSerial != null)
                {
                    db.DelSerials.Remove(objDelSerial);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
            }
        }

        public void DeleteDelSerial2(DelSerial objDelSerial)
        {
            try
            {
                if (objDelSerial != null)
                {
                    DelSerial objTemp = db.DelSerials.FirstOrDefault(o => o.Id == objDelSerial.Id);
                    if (objTemp != null)
                    {
                        db.DelSerials.Remove(objTemp);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
            }
        }

        public void CreateDelSerial(DelSerial objDelSerial)
        {
            if(objDelSerial!=null)
            {
                try
                {
                    db.DelSerials.Add(objDelSerial);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    log.Fatal(ex);
                }
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
