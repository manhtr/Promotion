using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DevExpress.Web;
//using Promotion.Commons;
//using Promotion.DataModel;
using log4net;

namespace Promotion.Serial
{
    public partial class BaoCaoCapMa : System.Web.UI.Page
    {
        DateTime dtStartDate = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["StartDate"]);
        ILog log = LogManager.GetLogger(typeof(BaoCaoCapMa));

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFrom.Text = dtStartDate.ToString("dd/MM/yyyy");
                txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                if (Session[Commons.Constant.USERNAME] != null)
                {
                    BindBranch();
                }
            }

            BindData();
        }

        protected void dxgvSerial_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                Label lbl = dxgvSerial.FindRowCellTemplateControl(e.VisibleIndex, null, "lblBranchName") as Label;
                string strCoCode = e.GetValue("CO_CODE").ToString();
                if (lbl != null && strCoCode != string.Empty)
                {
                    using (DataModel.COMPANY_BO objT24_COMPANY_BO = new DataModel.COMPANY_BO())
                    {
                        DataModel.COMPANY objT24_COMPANY = objT24_COMPANY_BO.GetByCOMPANY_CODE(strCoCode);
                        if (objT24_COMPANY != null)
                        {
                            lbl.Text = objT24_COMPANY.COMPANY_NAME;
                        }
                    }
                }

                Label lbl2 = dxgvSerial.FindRowCellTemplateControl(e.VisibleIndex, null, "lblStatus") as Label;
                if (lbl2 != null)
                {
                    if (e.GetValue("IsUsed") != null)
                    {
                        bool strStatus = Convert.ToBoolean(e.GetValue("IsUsed"));

                        if (strStatus)
                        {
                            lbl2.Text = "Đã cấp mã";
                        }
                        else
                        {
                            lbl2.Text = "Đã xóa";
                        }
                    }
                    else
                    {
                        lbl2.Text = "Đã cấp mã";
                    }
                }

                Label lbl3 = dxgvSerial.FindRowCellTemplateControl(e.VisibleIndex, null, "lblMaDuThuong") as Label;
                if (lbl3 != null)
                {
                    if (e.GetValue("Serials") != null)
                    {
                        string strSerials = e.GetValue("Serials").ToString();
                        if (!string.IsNullOrEmpty(strSerials))
                        {
                            strSerials = Commons.Common.FormatSerial(strSerials.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList());
                        }

                        lbl3.Text = strSerials;
                    }
                    else
                    {
                        lbl3.Text = string.Empty;
                    }
                }
            }
        }

        protected void dxgvSerial_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "STT")
            {
                e.DisplayText = (e.VisibleRowIndex + 1).ToString();
            }
        }

        protected void grvExport_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
        {
            GridViewDataColumn dataColumn = e.Column as GridViewDataColumn;
            if (e.RowType == GridViewRowType.Data && dataColumn != null && dataColumn.VisibleIndex == 7)
            {
                if (e.GetValue("Serials") != null)
                {
                    string strSerials = e.GetValue("Serials").ToString();
                    if (!string.IsNullOrEmpty(strSerials))
                    {
                        strSerials = Commons.Common.FormatSerial(strSerials.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList());
                    }

                    e.TextValue = strSerials;
                }
                else
                {
                    e.TextValue = string.Empty;
                }
            }

            dataColumn = e.Column as GridViewDataColumn;
            if (e.RowType == GridViewRowType.Data && dataColumn != null && dataColumn.VisibleIndex == 9)
            {
                string strCoCode = e.GetValue("CO_CODE").ToString();
                if (strCoCode != string.Empty)
                {
                    using (DataModel.COMPANY_BO objT24_COMPANY_BO = new DataModel.COMPANY_BO())
                    {
                        DataModel.COMPANY objT24_COMPANY = objT24_COMPANY_BO.GetByCOMPANY_CODE(strCoCode);
                        if (objT24_COMPANY != null)
                        {
                            e.TextValue = objT24_COMPANY.COMPANY_NAME;
                        }
                    }
                }
            }

            dataColumn = e.Column as GridViewDataColumn;
            if (e.RowType == GridViewRowType.Data && dataColumn != null && dataColumn.VisibleIndex == 10)
            {
                if (e.GetValue("IsUsed") != null)
                {
                    bool strStatus = Convert.ToBoolean(e.GetValue("IsUsed"));

                    if (strStatus)
                    {
                        e.TextValue = "Đã cấp mã";
                    }
                    else
                    {
                        e.TextValue = "Đã xóa";
                    }
                }
                else
                {
                    e.TextValue = "Đã cấp mã";
                }
            }
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {

        }

        protected void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void cmdExport_Click(object sender, EventArgs e)
        {
            grvExport.WriteXlsToResponse("DanhSachCapMaDuThuong");
        }

        #endregion

        #region Methods
        private void BindData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtFrom.Text))
                {
                    lblMessage.Text = "Nhập ngày bắt đầu báo cáo";
                    txtFrom.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtTo.Text))
                {
                    lblMessage.Text = "Nhập ngày kết thúc báo cáo";
                    txtTo.Focus();
                    return;
                }

                //if (string.IsNullOrEmpty(cboBranch.SelectedItem.Value))
                //{
                //    lblMessage.Text = "Chọn chi nhánh báo cáo";
                //    cboBranch.Focus();
                //    return;
                //}


                DateTime dtFrom = new DateTime();
                DateTime dtTo = new DateTime();
                string[] arr = null;
                int day = -1;
                int month = -1;
                int year = -1;
                if (!string.IsNullOrEmpty(txtFrom.Text))
                {
                    arr = txtFrom.Text.Trim().Split('/');
                    if (arr != null && arr.Length > 2)
                    {
                        int.TryParse(arr[0], out day);
                        int.TryParse(arr[1], out month);
                        int.TryParse(arr[2], out year);
                        if (day > 0 && month > 0 && year > 0)
                        {
                            dtFrom = new DateTime(year, month, day, 0, 0, 1);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(txtTo.Text))
                {
                    arr = txtTo.Text.Trim().Split('/');
                    if (arr != null && arr.Length > 2)
                    {
                        int.TryParse(arr[0], out day);
                        int.TryParse(arr[1], out month);
                        int.TryParse(arr[2], out year);
                        if (day > 0 && month > 0 && year > 0)
                        {
                            dtTo = new DateTime(year, month, day, 23, 59, 59);
                        }
                    }
                }

                using (DataModel.SerialResult_BO objSerialResult_BO = new DataModel.SerialResult_BO())
                {
                    List<DataModel.SerialResult> lstData = objSerialResult_BO.GetByCO_CODEAndInput_time(cboBranch.SelectedItem.Value, dtFrom, dtTo).ToList();
                    dxgvSerial.DataSource = lstData;
                    dxgvSerial.DataBind();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
            }
        }

        private void BindBranch()
        {
            using (DataModel.COMPANY_BO objT24_COMPANY_BO = new DataModel.COMPANY_BO())
            {
                List<DataModel.COMPANY> lstData = objT24_COMPANY_BO.GetAll().ToList();
                foreach (DataModel.COMPANY item in lstData)
                {
                    cboBranch.Items.Add(new ListItem(item.COMPANY_CODE + "-" + item.COMPANY_NAME, item.COMPANY_CODE));
                }
                cboBranch.Items.Insert(0, new ListItem("--Chọn chi nhánh--", string.Empty));
                if (Session[Commons.Constant.DAO_CODE] != null)
                {
                    cboBranch.ClearSelection();
                    if (cboBranch.Items.FindByValue(Session[Commons.Constant.DAO_CODE].ToString()) != null)
                    {
                        cboBranch.Items.FindByValue(Session[Commons.Constant.DAO_CODE].ToString()).Selected = true;
                    }
                }
                using (DataModel.User_BO objUser_BO = new DataModel.User_BO())
                {
                    DataModel.User objUser = objUser_BO.GetByUserName(Session[Commons.Constant.USERNAME].ToString());
                    if (objUser != null && objUser.Permisions != null)
                    {
                        bool hasPer = false;
                        foreach (DataModel.Permision item in objUser.Permisions)
                        {
                            if (item != null && item.Permision1 == Commons.Constant.PERMISION_ADMIN)
                            {
                                hasPer = true;
                                break;
                            }
                        }
                        if (hasPer)
                        {
                            cboBranch.Enabled = true;
                        }
                    }
                }
            }
        }

        #endregion
    }
}