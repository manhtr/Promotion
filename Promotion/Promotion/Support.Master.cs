using System;
using System.Web.UI;
using Promotion.Commons;
using log4net;


namespace LogSystem.SupportCustomerSystem
{
    public partial class Support : System.Web.UI.MasterPage
    {
        ILog log = LogManager.GetLogger(typeof(Support));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constant.USERNAME] != null && !string.IsNullOrEmpty(Session[Constant.USERNAME].ToString()))
            {

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
            log.Info(Session[Constant.USERNAME].ToString() + " thoát khỏi hệ thống.");
            Session.Remove(Constant.USERNAME);
            Session.Remove(Constant.FULLNAME);
            Session.Remove(Constant.DAO_CODE);
            Session.Remove(Constant.DEPT_CODE);
            Session.Clear();
            Page.Response.Redirect(Common.GetRootRequest() + "Account/Login.aspx");
        }
    }
}