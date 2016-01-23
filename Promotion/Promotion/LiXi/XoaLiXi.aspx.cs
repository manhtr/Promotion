using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Promotion.Commons;
using Promotion.DataModel;
using log4net;

namespace Promotion.LiXi
{
    public partial class XoaLiXi : System.Web.UI.Page
    {
        ILog log = LogManager.GetLogger(typeof(XoaLiXi));

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCIF.Focus();
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            if (string.IsNullOrEmpty(txtCIF.Text))
            {
                lblMessage.Text = "Nhập mã khách hàng";
                txtCIF.Focus();
                return;
            }
            if (string.IsNullOrEmpty(hdfCIF.Value))
            {
                lblMessage.Text = "Mã khách hàng không hợp lệ";
                txtCIF.Focus();
                return;
            }

            using (Promotion.DataModel.LiXi_BO objLiXi_BO = new Promotion.DataModel.LiXi_BO())
            {
                Promotion.DataModel.LiXi objLiXi = objLiXi_BO.GetByCIF(hdfCIF.Value);
                if (objLiXi != null)
                {
                    objLiXi.IsUsed = false;
                    objLiXi.Deleted = DateTime.Now;
                    objLiXi.DeletedBy = Session[Promotion.Commons.Constant.USERNAME].ToString();
                    bool result = objLiXi_BO.UpdateLiXi(objLiXi);
                    if (result)
                    {
                        cmdDelete.Visible = false;
                        lblMessage.Text = "Xóa cấp lì xì thành công.";
                    }
                    else
                    {
                        lblMessage.Text = "Có lỗi khi xóa cấp lì xì.";
                    }
                }
                else
                {
                    lblMessage.Text = "Dữ liệu không hợp lệ";
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
            lblStatus.Text = string.Empty;

            if (string.IsNullOrEmpty(txtCIF.Text))
            {
                lblMessage.Text = "Nhập mã khách hàng";
                txtCIF.Focus();
                return;
            }

            if (Session[Constant.USERNAME] != null)
            {
                using (PromotionEntities db = new PromotionEntities())
                {
                    var cust = db.CUSTOMERs.FirstOrDefault(t => t.CUSTOMER_CODE.Equals(txtCIF.Text));
                    if (cust != null)
                    {
                        lblCIF.Text = cust.CUSTOMER_CODE;
                        lblCustomerName.Text = cust.CUSTOMER_NAME;
                        lblCMND.Text = cust.LEGAL_ID;
                        if (!string.IsNullOrEmpty(cust.TELEPHONE))
                        {
                            lblTelephone.Text = cust.TELEPHONE;
                        }
                        if (!string.IsNullOrEmpty(cust.ADDRESS))
                        {
                            lblAddress.Text = cust.ADDRESS;
                        }
                        string strAccount = string.Empty;

                        var listAcc = db.AZ_ACCOUNT.Where(t => t.CUSTOMER_ID.Equals(txtCIF.Text.Trim())).ToList();
                        if (listAcc != null)
                        {
                            foreach (var azAccount in listAcc)
                            {
                                //strAccount += azAccount.ACCOUNT_NUMBER + "; ";
                            }
                        }

                        lblAccountNumber.Text = strAccount;
                        hdfCIF.Value = txtCIF.Text.Trim();
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
                                lblStatus.Text = "Đã được cấp Lì xì vào " + objLiXi.Created.Value.ToString("dd/MM/yyyy HH:mm") + " tại " + strPGD + ". Cấp bởi " + objLiXi.CreatedBy;
                                cmdDelete.Visible = true;
                            }
                            else
                            {
                                lblStatus.Text = "Khách hàng chưa được cấp Lì xì";
                            }
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Thông tin khách hàng không phù hợp. ";
                    }
                }
            }
        }

        #endregion

        #region Methods

        #endregion
    }
}