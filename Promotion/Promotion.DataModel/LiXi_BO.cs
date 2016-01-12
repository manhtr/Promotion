using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promotion.DataModel
{
    public class LiXi_BO : IDisposable
    {
        ILog log = LogManager.GetLogger(typeof(LiXi_BO));
        PromotionEntities db = null;

        public LiXi_BO()
        {
            db = new PromotionEntities();
        }

        public IQueryable<LiXi> GetByCoCode(string strCoCode, DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                if (!string.IsNullOrEmpty(strCoCode))
                {
                    strCoCode = strCoCode.Trim().ToUpper();
                }
                if (!string.IsNullOrEmpty(strCoCode))
                {
                    return db.LiXis.Where(o => o.CoCode == strCoCode && o.Created >= dtFrom && o.Created <= dtTo).OrderByDescending(o => o.Created);
                }
                else
                {
                    return db.LiXis.Where(o => o.Created >= dtFrom && o.Created <= dtTo).OrderByDescending(o => o.Created);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return null;
            }
        }

        public IQueryable<LiXi> GetByDAO_CODE(string strDAO_CODE, DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                if (!string.IsNullOrEmpty(strDAO_CODE))
                {
                    strDAO_CODE = strDAO_CODE.Trim().ToUpper();
                }
                return db.LiXis.Where(o => o.DAO_CODE == strDAO_CODE && o.Created >= dtFrom && o.Created <= dtTo).OrderByDescending(o => o.Created);
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return null;
            }
        }

        public LiXi GetByCIF(string strCIF)
        {
            try
            {
                if (!string.IsNullOrEmpty(strCIF))
                {
                    strCIF = strCIF.Trim();
                }
                return db.LiXis.FirstOrDefault(o => o.CIF == strCIF && (!o.IsUsed.HasValue || (o.IsUsed.HasValue && o.IsUsed.Value)));
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return null;
            }
        }

        public bool CreateLiXi(LiXi objLiXi)
        {
            bool result = false;

            if (objLiXi != null)
            {
                try
                {
                    db.LiXis.Add(objLiXi);
                    db.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                    log.Fatal(ex);
                }
            }

            return result;
        }

        public bool UpdateLiXi(LiXi objLiXi)
        {
            bool result = false;

            if (objLiXi != null)
            {
                try
                {
                    LiXi objTemp = db.LiXis.FirstOrDefault(o => o.ID == objLiXi.ID);
                    if (objTemp != null)
                    {
                        objTemp.AccountNumber = objLiXi.AccountNumber;
                        objTemp.Address = objLiXi.Address;
                        objTemp.CIF = objLiXi.CIF;
                        objTemp.CMND = objLiXi.CMND;
                        objTemp.CoCode = objLiXi.CoCode;
                        objTemp.Created = objLiXi.Created;
                        objTemp.CreatedBy = objLiXi.CreatedBy;
                        objTemp.CustomerName = objLiXi.CustomerName;
                        objTemp.DAO_CODE = objLiXi.DAO_CODE;
                        objTemp.Deleted = objLiXi.Deleted;
                        objTemp.DeletedBy = objLiXi.DeletedBy;
                        objTemp.DEPT_CODE = objLiXi.DEPT_CODE;
                        objTemp.IsUsed = objLiXi.IsUsed;
                        objTemp.Telephone = objLiXi.Telephone;

                        db.SaveChanges();
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    log.Fatal(ex);
                }
            }

            return result;
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
