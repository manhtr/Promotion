using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promotion.DataModel
{
    public class User_BO : IDisposable
    {
        ILog log = LogManager.GetLogger(typeof(User_BO));
        PromotionEntities db = null;

        public User_BO()
        {
            db = new PromotionEntities();
        }

        public User GetByID(int id)
        {
            try
            {
                return db.Users.FirstOrDefault(o => o.ID == id);
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return null;
            }
        }

        public User GetByUserName(string strUserName)
        {
            try
            {
                strUserName = strUserName.Trim().ToLower();
                //return db.Users.FirstOrDefault(o => o.UserName == strUserName);
                User obj = null;
                obj = db.Users.FirstOrDefault(o => o.UserName == strUserName);
                if(obj != null)
                {
                    var tmp = db.Permisions.Where(t => t.UserID == obj.ID);
                    if (tmp != null)
                    {
                        obj.Permisions = db.Permisions.Where(t => t.UserID == obj.ID).ToList();
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return null;
            }
        }

        public User GetByUserName(string strUserName, string strPassword)
        {
            try
            {
                strUserName = strUserName.Trim().ToLower();
                return db.Users.FirstOrDefault(o => o.UserName.Equals(strUserName) && o.Password.Equals(strPassword));
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
