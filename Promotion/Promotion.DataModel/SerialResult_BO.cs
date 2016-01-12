using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Promotion.DataModel
{
    public class SerialResult_BO : IDisposable
    {
        ILog log = LogManager.GetLogger(typeof(SerialResult_BO));
        PromotionEntities db = null;

        public SerialResult_BO()
        {
            db = new PromotionEntities();
        }

        public SerialResult GetByBook_numberAndInput_time(string strbook_number, DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                return db.SerialResults.Where(o => o.book_number == strbook_number && o.Input_time >= dtFrom && o.Input_time <= dtTo && (!o.IsUsed.HasValue || (o.IsUsed.HasValue && o.IsUsed.Value))).OrderByDescending(o => o.Input_time).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return null;
            }
        }
        public IQueryable<SerialResult> GetByCustomer_idAndBook_numberAndInput_time(string strcustomer_id, string strbook_number, DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                strbook_number = strbook_number.Trim();
                return db.SerialResults.Where(o => o.customer_id == strcustomer_id && o.book_number == strbook_number && o.Input_time >= dtFrom && o.Input_time <= dtTo && (!o.IsUsed.HasValue || (o.IsUsed.HasValue && o.IsUsed.Value))).OrderByDescending(o => o.Input_time);
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return null;
            }
        }

        public IQueryable<SerialResult> GetByCO_CODEAndInput_time(string strCO_CODE, DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                if (!string.IsNullOrEmpty(strCO_CODE))
                {
                    strCO_CODE = strCO_CODE.Trim().ToUpper();
                }
                if (!string.IsNullOrEmpty(strCO_CODE))
                {
                    return db.SerialResults.Where(o => o.CO_CODE == strCO_CODE && o.Input_time >= dtFrom && o.Input_time <= dtTo).OrderByDescending(o => o.Input_time);
                }
                else
                {
                    return db.SerialResults.Where(o => o.Input_time >= dtFrom && o.Input_time <= dtTo).OrderByDescending(o => o.Input_time);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return null;
            }
        }

        public void DeleteSerialResult(SerialResult objSerialResult)
        {
            if (objSerialResult != null)
            {
                try
                {
                    SerialResult objTemp = db.SerialResults.FirstOrDefault(o => o.id == objSerialResult.id && o.book_number == objSerialResult.book_number);
                    if (objTemp != null)
                    {
                        objTemp.IsUsed = false;
                        objTemp.DeletedDate = DateTime.Now;
                        objTemp.DeletedBy = objSerialResult.DeletedBy;
                        //db.SerialResults.Remove(objTemp);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    log.Fatal(ex);
                }
            }
        }

        public void CreateSerialResult(SerialResult objSerialResult)
        {
            if (objSerialResult != null)
            {
                try
                {
                    objSerialResult.IsUsed = true;
                    db.SerialResults.Add(objSerialResult);
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
