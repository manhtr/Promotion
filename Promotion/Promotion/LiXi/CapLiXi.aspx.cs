using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Promotion.Commons;
using log4net;

namespace Promotion.LiXi
{
    public partial class CapLiXi : System.Web.UI.Page
    {
        ILog log = LogManager.GetLogger(typeof(CapLiXi));
        DateTime dtStartLiXi = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["StartLiXi"]);
        DateTime dtEndLiXi = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["EndLiXi"]);
        const int MAX_LIXI = 5;
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[Constant.USERNAME] != null)
                {
                    BindBranch();
                }
            }
        }

        protected void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBranchInfo();
        }
        protected void cmdAgree_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            if (string.IsNullOrEmpty(txtCIF.Text))
            {
                lblMessage.Text = "Nhập mã khách hàng";
                txtCIF.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cboBranch.SelectedItem.Value))
            {
                lblMessage.Text = "Chọn chi nhánh";
                cboBranch.Focus();
                return;
            }

            if ((DateTime.Now < dtStartLiXi) || (DateTime.Now > dtEndLiXi))
            {
                lblMessage.Text = "Chương trình khuyến mại đã kết thúc";
                return;
            }

            using (DataModel.LiXi_BO objLiXi_BO = new DataModel.LiXi_BO())
            {
                DataModel.LiXi objLiXi = new DataModel.LiXi();
                objLiXi.AccountNumber = lblAccountNumber.Text;
                objLiXi.Address = lblAddress.Text;
                objLiXi.CIF = txtCIF.Text.Trim();
                objLiXi.CMND = lblCMND.Text;
                objLiXi.CoCode = cboBranch.SelectedItem.Value;
                objLiXi.Created = DateTime.Now;
                objLiXi.CreatedBy = Session[Constant.USERNAME].ToString();
                objLiXi.CustomerName = lblCustomerName.Text;
                objLiXi.DAO_CODE = Session[Constant.DAO_CODE].ToString();
                objLiXi.DEPT_CODE = Session[Constant.DEPT_CODE].ToString();
                objLiXi.IsUsed = true;
                objLiXi.Telephone = lblTelephone.Text;
                bool result = objLiXi_BO.CreateLiXi(objLiXi);
                if (result)
                {
                    lblMessage.Text = "Cấp Lì xì cho khách hàng thành công";
                    cmdAgree.Visible = false;
                }
                else
                {
                    lblMessage.Text = "Có lỗi khi Cấp Lì xì cho khách hàng";
                }
            }
        }
        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

            lblCIF.Text = string.Empty;
            lblCustomerName.Text = string.Empty;
            lblCMND.Text = string.Empty;
            lblTelephone.Text = string.Empty;
            lblAddress.Text = string.Empty;
            lblAccountNumber.Text = string.Empty;
            lblAmount.Text = string.Empty;

            if (string.IsNullOrEmpty(txtCIF.Text))
            {
                lblMessage.Text = "Nhập mã khách hàng";
                txtCIF.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cboBranch.SelectedItem.Value))
            {
                lblMessage.Text = "Chọn chi nhánh";
                cboBranch.Focus();
                return;
            }
            bool bValidate = false;
            //using (eCS.IeCSAdminClient client = new eCS.IeCSAdminClient())
            //{
            //    bool result = true;
            //    string response = string.Empty;
            //    eCS.EBankingCustomerInformation objEBankingCustomerInformation = client.GetCustomerInformation(txtCIF.Text, ref result, ref response);
            //    if (result && objEBankingCustomerInformation != null)
            //    {
            //        lblCIF.Text = objEBankingCustomerInformation.CustomerID;
            //        lblCustomerName.Text = objEBankingCustomerInformation.VNCustomerName;
            //        lblCMND.Text = objEBankingCustomerInformation.LegalID;
            //        if (!string.IsNullOrEmpty(objEBankingCustomerInformation.Telephone))
            //        {
            //            lblTelephone.Text = objEBankingCustomerInformation.Telephone;
            //        }
            //        else
            //        {
            //            lblTelephone.Text = objEBankingCustomerInformation.SMSNumber.Replace("#", " ");
            //        }
            //        lblAddress.Text = objEBankingCustomerInformation.Address.Replace("ý", " ");
            //        string strAccount = string.Empty;
            //        using (eCS.IeCSClient objIeCSClient = new eCS.IeCSClient())
            //        {
            //            DataSet ds = objIeCSClient.GetAccount(txtCIF.Text.Trim(), ref result, ref response);
            //            if (result && ds != null && ds.Tables.Count > 0)
            //            {
            //                foreach (DataRow item in ds.Tables[0].Rows)
            //                {
            //                    strAccount += item["ACCOUNT_NUMBER"].ToString() + "; ";
            //                }
            //            }
            //        }
            //        bValidate = true;
            //        lblAccountNumber.Text = strAccount;
            //    }
            //    else
            //    {
            //        lblMessage.Text = "Thông tin khách hàng không phù hợp. " + response;
            //    }
            //}

            if (bValidate)
            {
                using (DataModel.BLACKLIST_CUSTOMER_BO objBLACKLIST_CUSTOMER_BO = new DataModel.BLACKLIST_CUSTOMER_BO())
                {
                    DataModel.BLACKLIST_CUSTOMER objBLACKLIST_CUSTOMER = objBLACKLIST_CUSTOMER_BO.GetByCUSTOMER_ID(txtCIF.Text);
                    if (objBLACKLIST_CUSTOMER != null)
                    {
                        lblMessage.Text = "Yêu cầu cấp lì xì không hợp lệ. Khách hàng thuộc danh sách cán bộ, nhân viên GPBank. Đề nghị kiểm tra lại thông tin.";
                        bValidate = false;
                    }
                }
            }

            if (bValidate)
            {
                using (DataModel.LiXi_BO objLiXi_BO = new DataModel.LiXi_BO())
                {
                    DataModel.LiXi objLiXi = objLiXi_BO.GetByCIF(txtCIF.Text);
                    if (objLiXi != null)
                    {
                        string strPGD = string.Empty;
                        using (DataModel.COMPANY_BO objT24_COMPANY_BO = new DataModel.COMPANY_BO())
                        {
                            DataModel.COMPANY objT24_COMPANY = objT24_COMPANY_BO.GetByCOMPANY_CODE(objLiXi.CoCode);
                            if (objT24_COMPANY != null)
                            {
                                strPGD = objT24_COMPANY.COMPANY_NAME;
                            }
                        }
                        lblMessage.Text = "Khách hàng " + lblCustomerName.Text + " đã được cấp Lì xì vào " + objLiXi.Created.Value.ToString("dd/MM/yyyy HH:mm") + " tại " + strPGD + ". Đề nghị kiểm tra lại thông tin.";
                        bValidate = false;
                    }
                    else
                    {
                        DateTime dtNow = DateTime.Now;
                        DateTime dtFrom = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 1);
                        DateTime dtTo = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 23, 59, 59);
                        List<DataModel.LiXi> lstData = objLiXi_BO.GetByCoCode(cboBranch.SelectedItem.Value, dtFrom, dtTo).ToList();
                        if (lstData != null)
                        {
                            lstData = lstData.Where(o => !o.IsUsed.HasValue || (o.IsUsed.HasValue && o.IsUsed.Value)).ToList();
                            lblAmount.Text = lstData.Count.ToString();
                            if (lstData.Count < MAX_LIXI)
                            {
                                bValidate = true;
                            }
                            else
                            {
                                bValidate = false;
                                lblMessage.Text = "Yêu cầu cấp Lì xì không hợp lệ. Điểm giao dịch đã cấp đủ số lượng (" + MAX_LIXI.ToString() + ") Lì xì trong ngày. Đề nghị kiểm tra lại thông tin.";
                            }
                        }
                    }
                }
            }

            if (bValidate)
            {
                cmdAgree.Visible = true;
            }

        }

        #endregion


        #region Methods

        private void BindBranch()
        {
            using (DataModel.COMPANY_BO objT24_COMPANY_BO = new DataModel.COMPANY_BO())
            {
                List<DataModel.COMPANY> lstData = objT24_COMPANY_BO.GetAll().ToList();
                foreach (DataModel.COMPANY item in lstData)
                {
                    cboBranch.Items.Add(new ListItem(item.COMPANY_CODE + "-" + item.COMPANY_NAME, item.COMPANY_CODE));
                }
                cboBranch.Items.Insert(0, new ListItem("--Chọn CN/PGD--", string.Empty));
                if (Session[Commons.Constant.DAO_CODE] != null)
                {
                    cboBranch.ClearSelection();
                    using (DataModel.DAO_COMPANY_BO objDAO_COMPANY_BO = new DataModel.DAO_COMPANY_BO())
                    {
                        List<DataModel.DAO_COMPANY> lstDaoCompany = objDAO_COMPANY_BO.GetByDAO_CODE(Session[Commons.Constant.DAO_CODE].ToString()).ToList();
                        if (lstDaoCompany != null)
                        {
                            foreach (DataModel.DAO_COMPANY item in lstDaoCompany)
                            {
                                if (item.DAO_CODE == Session[Commons.Constant.DAO_CODE].ToString() && !string.IsNullOrEmpty(item.COMPANY_CODE.Trim()))
                                {
                                    if (cboBranch.Items.FindByValue(item.COMPANY_CODE) != null)
                                    {
                                        cboBranch.Items.FindByValue(item.COMPANY_CODE).Selected = true;
                                    }
                                    break;
                                }
                            }
                        }
                    }

                }
            }
        }

        private void BindBranchInfo()
        {
            lblCoCode.Text = string.Empty;
            lblBranchName.Text = string.Empty;
            lblBranchAddress.Text = string.Empty;
            lblAmount.Text = string.Empty;

            if (!string.IsNullOrEmpty(cboBranch.SelectedItem.Value))
            {
                using (DataModel.COMPANY_BO objT24_COMPANY_BO = new DataModel.COMPANY_BO())
                {
                    DataModel.COMPANY objT24_COMPANY = objT24_COMPANY_BO.GetByCOMPANY_CODE(cboBranch.SelectedItem.Value);
                    if (objT24_COMPANY != null)
                    {
                        lblCoCode.Text = objT24_COMPANY.COMPANY_CODE;
                        lblBranchName.Text = objT24_COMPANY.COMPANY_NAME;
                        lblBranchAddress.Text = objT24_COMPANY.NAME_ADDRESS.Replace("ý", string.Empty);
                    }
                }
            }
        }
        #endregion

    }
}