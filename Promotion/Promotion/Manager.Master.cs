using System;
using System.Web.UI;
using Promotion.Commons;
using Promotion.DataModel;
using log4net;

namespace Promotion
{
    public partial class Manager : System.Web.UI.MasterPage
    {
        ILog log = LogManager.GetLogger(typeof(Manager));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constant.USERNAME] != null && !string.IsNullOrEmpty(Session[Constant.USERNAME].ToString()))
            {
                using (Promotion.DataModel.User_BO objUser_BO = new User_BO())
                {
                    Promotion.DataModel.User objUser = objUser_BO.GetByUserName(Session[Constant.USERNAME].ToString());
                    if (objUser != null)
                    {
                        if (objUser.Permisions != null)
                        {
                            bool hasPer = false;
                            foreach (Promotion.DataModel.Permision item in objUser.Permisions)
                            {
                                if (item != null && item.Permision1 == Constant.PERMISION_ADMIN)
                                {
                                    hasPer = true;
                                    break;
                                }
                            }
                            if (!hasPer)
                            {
                                log.Info(Session[Constant.USERNAME].ToString() + " không có quyền truy cập trang " + Page.Title);
                                Page.Response.Redirect(Common.GetRootRequest() + "Account/Login.aspx");
                            }
                        }
                    }
                }
            }
            else
            {
                Page.Response.Redirect(Common.GetRootRequest() + "Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                if (Session[Constant.FULLNAME] != null)
                {
                    lblWelcome.Text = Session[Constant.FULLNAME].ToString();
                }
            }
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove(Constant.USERNAME);
            Session.Remove(Constant.FULLNAME);
            Session.Remove(Constant.DAO_CODE);
            Session.Remove(Constant.DEPT_CODE);
            Session.Clear();
            Page.Response.Redirect(Common.GetRootRequest() + "Account/Login.aspx");
        }
    }
}