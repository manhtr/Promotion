using System;
using System.Web.UI;
using Promotion.Commons;

namespace LogSystem.SupportCustomerSystem
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constant.USERNAME] == null && !Request.Url.ToString().Contains("Login.aspx"))
            {
                Response.Redirect(Common.GetRootRequest() + "Account/Login.aspx?re=" + Request.Url.ToString());
            }
            if (!IsPostBack)
            {
               
            }
        }
    }
}