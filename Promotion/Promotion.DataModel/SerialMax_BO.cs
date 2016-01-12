using log4net;
using System;
using System.Linq;

namespace Promotion.DataModel
{
    public class SerialMax_BO: IDisposable
    {
        ILog log = LogManager.GetLogger(typeof(SerialMax_BO));
        PromotionEntities db = null;

        public SerialMax_BO()
        {
            db = new PromotionEntities();
        }

        public IQueryable<SerialMax> GetTop(int top, DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                if (top > 0)
                {
                    return db.SerialMaxes.OrderByDescending(o => o.input_time).Take(top);
                }
                else
                {
                    return db.SerialMaxes.OrderByDescending(o => o.input_time);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return null;
            }
        }

        public void CreateSerialMax(SerialMax objSerialMax)
        {
            if (objSerialMax != null)
            {
                try
                {
                    db.SerialMaxes.Add(objSerialMax);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    log.Fatal(ex);
                }
            }
        }

        public void UpdateSerialMax(SerialMax objSerialMax)
        {
            if (objSerialMax != null)
            {
                try
                {
                    SerialMax objTemp = db.SerialMaxes.FirstOrDefault(o => o.Id == objSerialMax.Id);
                    if (objTemp != null)
                    {
                        objTemp.input_time = objSerialMax.input_time;
                        objTemp.MaxSerial = objSerialMax.MaxSerial;
                        db.SaveChanges();
                    }
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
