using System;
using System.DirectoryServices;
using System.Web.UI;
//using Promotion.Commons;
using log4net;

namespace Promotion.Account
{
    public partial class Login : Page
    {
        ILog log = LogManager.GetLogger(typeof(Login));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[Commons.Constant.USERNAME] != null && !string.IsNullOrEmpty(Session[Commons.Constant.USERNAME].ToString()))
                {
                    Page.Response.Redirect(Commons.Common.GetRootRequest() + "Default.aspx");
                }

                if (!IsPostBack)
                {
                    txtUsername.Focus();
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblInfor.Text = string.Empty;

            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                lblInfor.Text = "Nhập tên đăng nhập.";
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                lblInfor.Text = "Nhập mật khẩu.";
                txtPassword.Focus();
                return;
            }
            //if (ValidateByAD(txtUsername.Text.Trim(), txtPassword.Text, "g-bank.com.vn"))
            if (ValidateUser(txtUsername.Text.Trim(), txtPassword.Text))
            {
                using (DataModel.STAFF_USER_BO objSTAFF_USER_BO = new DataModel.STAFF_USER_BO())
                {
                    DataModel.STAFF_USER objSTAFF_USER = objSTAFF_USER_BO.GetByUSERNAME(txtUsername.Text);
                    if (objSTAFF_USER != null)
                    {
                        Session[Commons.Constant.USERNAME] = txtUsername.Text.Trim().ToLower();
                        Session[Commons.Constant.FULLNAME] = objSTAFF_USER.HO_TEN;
                        Session[Commons.Constant.DAO_CODE] = objSTAFF_USER.DAO_CODE;
                        Session[Commons.Constant.DEPT_CODE] = objSTAFF_USER.DEPT_CODE;
                        log.Info("Login success: " + txtUsername.Text.Trim());
                        //Page.Response.Redirect(Common.GetRootRequest() + "Default.aspx");
                        using (DataModel.User_BO objUser_BO = new DataModel.User_BO())
                        {
                            DataModel.User objUser = objUser_BO.GetByUserName(Session[Commons.Constant.USERNAME].ToString());
                            if (objUser != null && objUser.Permisions != null)
                            {
                                bool hasPer = false;
                                foreach (Promotion.DataModel.Permision item in objUser.Permisions)
                                {
                                    if (item != null && item.Permision1 == Commons.Constant.PERMISION_ADMIN)
                                    {
                                        hasPer = true;
                                        break;
                                    }
                                }
                                if (hasPer)  //Admin
                                {
                                    Page.Response.Redirect(Commons.Common.GetRootRequest() + "Manager.aspx");
                                }
                                else  //Tellers
                                {
                                    Page.Response.Redirect(Commons.Common.GetRootRequest() + "Default.aspx");
                                }
                            }
                        }

                    }
                    else
                    {
                        lblInfor.Text = "Người dùng không được truy cập vào hệ thống.";
                        txtUsername.Focus();
                        log.Info("Người dùng không được truy cập vào hệ thống");
                    }
                }
            }
            else
            {
                lblInfor.Text = "Tên đăng nhập và mật khẩu không phù hợp.";
                txtUsername.Focus();
                log.Info("Tên đăng nhập và mật khẩu không phù hợp. User: " + txtUsername.Text + ", password: " + txtPassword.Text);
            }
        }

        private bool ValidateUser(string strUsername, string strPassword)
        {
            bool validateUser = false;
            try
            {
                using (DataModel.User_BO objUser_BO = new DataModel.User_BO())
                {
                    DataModel.User objUser = objUser_BO.GetByUserName(strUsername, strPassword);
                    if (objUser != null)
                    {
                        validateUser = true;
                    }
                }
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
            return validateUser;
        }

        private bool ValidateByAD(string strUsername, string strPassword, string strDomain)
        {
            strDomain = "10.1.1.10";
            bool authentic = false;
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + strDomain, strUsername, strPassword);
                object nativeObject = entry.NativeObject;
                authentic = true;
            }
            catch (Exception ex)
            {
                authentic = false;
            }
            return authentic;
        }
    }
}