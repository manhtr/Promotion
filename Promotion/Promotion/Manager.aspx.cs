using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

namespace Promotion
{
    public partial class Manager1 : System.Web.UI.Page
    {
        ILog log = LogManager.GetLogger(typeof(Manager1));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Commons.Constant.USERNAME] == null && !Request.Url.ToString().Contains("Login.aspx"))
            {
                Response.Redirect(Commons.Common.GetRootRequest() + "Account/Login.aspx?re=" + Request.Url.ToString());
            }
            if (!IsPostBack)
            {

            }
        }
    }
}