using System;
using System.Linq;
using System.Web.UI;
using Promotion.Commons;
using log4net;

namespace Promotion.Serial
{
    public partial class XoaMaDuThuong : System.Web.UI.Page
    {
        ILog log = LogManager.GetLogger(typeof(XoaMaDuThuong));
        const string EMPTY_TEXT = "........";
        const string cm_strQuaysSoSession = "QUAYSO_SESSION";

        DateTime dtStartDate = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["StartDate"]);
        DateTime dtEndDate = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["EndDate"]);

        //string strT24Host = System.Configuration.ConfigurationManager.AppSettings["gpb.t24.host"];
        //string strT24Port = System.Configuration.ConfigurationManager.AppSettings["gpb.t24.port"];
        //string strT24Service = System.Configuration.ConfigurationManager.AppSettings["gpb.t24.servicename"];
        //string strT24Uid = System.Configuration.ConfigurationManager.AppSettings["gpb.t24.userid"];
        //string strT24Pwd = System.Configuration.ConfigurationManager.AppSettings["gpb.t24.pwd"];
        //string strT24Schema = System.Configuration.ConfigurationManager.AppSettings["gbp.t24.schema"];

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[Constant.USERNAME] != null)
                {
                    log.Info(Session[Constant.USERNAME].ToString() + ": truy cập trang " + Page.Title);
                }
            }
        }

        protected void cmdKiemTra_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCIF.Text))
            {
                lblMessage.Text = "Nhập mã khách hàng";
                txtCIF.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtSoSo.Text))
            {
                lblMessage.Text = "Nhập số sổ";
                txtSoSo.Focus();
                return;
            }

            BindForm();
        }

        protected void cmdXoaPhieu_Click(object sender, EventArgs e)
        {
            if (Session[Constant.USERNAME] != null)
            {
                Serial.Function objFunction = new Function();
                Serial.SerialBook objSerialBook = objFunction.GetSerialBookResultByBookNumber(Session[Constant.SO_SO].ToString(), dtStartDate, dtEndDate);
                if (objSerialBook != null)
                {
                    string strResponse = string.Empty;
                    bool delSuccess = objFunction.DeleteSerialBookByBookNumber(Session[Constant.SO_SO].ToString(), Session[Constant.USERNAME].ToString(), dtStartDate, dtEndDate, ref strResponse);
                    if (delSuccess)
                    {
                        lblMessage.Text = "Xóa mã dự thưởng thành công!";
                        lblMessage.CssClass = "successMessage";
                        BindForm();
                    }
                    else
                    {
                        lblMessage.Text = "Lỗi xóa mã dự thưởng. " + strResponse;
                        lblMessage.CssClass = "errorMessage";
                    }
                }
            }
        }

        #endregion

        #region Methods

        private void BindForm()
        {
            ClearForm();
            Session[Constant.SO_SO] = txtSoSo.Text.Trim();
            Session[Constant.PHAN_HE] = lstradPhanHe.SelectedValue;
            string strBookCategory = string.Empty;
            Serial.Function objFunction = new Function();
            Serial.SerialBook objSerialBook = objFunction.GetSerialBookFromT24(txtSoSo.Text, lstradPhanHe.SelectedValue, txtCIF.Text.Trim(), ref strBookCategory);
            if (objSerialBook != null && Session[Constant.USERNAME] != null)
            {
                lblMaKhachHang.Text = objSerialBook.Customer_Id;
                lblTenKhachHang.Text = objSerialBook.Customer_Name;
                lblSoTienGui.Text = String.Format("{0:C}", objSerialBook.TotalAmount).Replace("$", "") + " " + objSerialBook.Currency;
                lblKyHanGui.Text = objSerialBook.PeriodInWord;
                Serial.SerialBook objSB = objFunction.GetSerialBookResultByBookNumber(txtSoSo.Text, dtStartDate, dtEndDate);
                if (objSB != null)
                {
                    if (objSB.Masoduthuongs != null)
                    {
                        cmdXoaPhieu.Visible = true;
                    }
                    else
                    {
                        cmdXoaPhieu.Visible = false;
                        lblMessage.Text = "Số này chưa được cấp mã dự thưởng!";
                        lblMessage.CssClass = "errorMessage";
                    }

                    lblMaDuThuongCo.Text = Common.FormatSerial(objSB.Masoduthuongs.ToList());
                }
                else
                {
                    lblMessage.Text = "Số này chưa được cấp mã dự thưởng!";
                    lblMessage.CssClass = "errorMessage";
                    cmdXoaPhieu.Visible = false;
                }
            }
            else
            {
                lblMessage.Text = "Số sổ không thấy!";
                lblMessage.CssClass = "errorMessage";
                cmdXoaPhieu.Visible = false;
            }
        }

        private void ClearForm()
        {
            lblMaKhachHang.Text = EMPTY_TEXT;
            lblTenKhachHang.Text = EMPTY_TEXT;
            lblSoTienGui.Text = EMPTY_TEXT;
            lblKyHanGui.Text = EMPTY_TEXT;
            lblMaDuThuongCo.Text = EMPTY_TEXT;
            lblMessage.Text = string.Empty;
            lblMessage.CssClass = "";
            Session[Constant.SO_SO] = null;
            Session[Constant.PHAN_HE] = null;
        }

        #endregion
    }
}