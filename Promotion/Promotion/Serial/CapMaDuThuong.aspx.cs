using System;
using System.Linq;
using Promotion.Commons;
using log4net;

namespace Promotion.Serial
{
    public partial class CapMaDuThuong : System.Web.UI.Page
    {
        ILog log = LogManager.GetLogger(typeof(CapMaDuThuong));
        const string EMPTY_TEXT = "........";
        const string cm_strQuaysSoSession = "QUAYSO_SESSION";

        DateTime dtStartDate = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["StartDate"]);
        DateTime dtEndDate = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["EndDate"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constant.USERNAME] == null)
            {
                Response.Redirect(Commons.Common.GetRootRequest() + "Account/Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Session[Constant.USERNAME] != null)
                {
                    log.Info(Session[Constant.USERNAME].ToString() + " truy cập trang " + Page.Title);
                }
            }
        }

        protected void cmdCapPhieu_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            if (Session[Constant.USERNAME] != null && !string.IsNullOrEmpty(Session[Constant.USERNAME].ToString()))
            {
                ClearForm();

                log.Info(Session[Constant.USERNAME].ToString() + ": Chọn cấp phiếu");

                if ((DateTime.Now < dtStartDate) || (DateTime.Now > dtEndDate))
                {
                    lblMessage.Text = "Chương trình khuyến mại đã kết thúc";
                    return;
                }

                try
                {
                    if (Session[Constant.SO_SO].ToString().ToUpper().Contains("GIB"))
                    {
                        lblMessage.Text = string.Format("Số sổ {0} không hợp lệ", Session[Constant.SO_SO].ToString());
                        log.Info(lblMessage.Text);
                        return;
                    }

                    Serial.SerialBook objSerialBook = (Serial.SerialBook)Session[Constant.SERIAL_BOOK];
                    if (objSerialBook != null)
                    {
                        Serial.Function objFunction = new Function();

                        if (!objSerialBook.ThamGiaKM.ToUpper().Contains("TET VE LOC DEN"))
                        {
                            lblMessage.Text = "Số sổ tiết kiệm không tham gia khuyến mại!";
                            lblMessage.CssClass = "errorMessage";
                            cmdCapPhieu.Visible = false;
                            return;
                        }

                        if (objSerialBook.Period > 6)
                        {
                            lblMessage.Text = string.Format("Số sổ {0} có kỳ hạn {1} tháng không thuộc điều kiện tham gia chương trình", objSerialBook.Book_Number, objSerialBook.Period);
                            log.Info(objSerialBook.Period.ToString());
                            return;
                        }
                        if (objFunction.IsSerialBookResultExisted(objSerialBook.Customer_Id, objSerialBook.Book_Number, dtStartDate, dtEndDate))
                        {
                            lblMessage.Text = string.Format("Khách hàng {0} có số sổ {1} đã được cấp mã", objSerialBook.Customer_Id, objSerialBook.Book_Number);
                            log.Info(lblMessage.Text);
                            return;
                        }

                        if (objSerialBook.MaturityDate < dtStartDate)
                        {
                            lblMessage.Text = string.Format("So {0} da dao han tu ngay {1} truoc ngay {2} chuong trinh quay so bat dau", objSerialBook.Book_Number, objSerialBook.MaturityDate, dtStartDate);
                            log.Info(lblMessage.Text);
                            return;
                        }
                        if (objSerialBook.ValueDate > dtEndDate)
                        {
                            lblMessage.Text = string.Format("So {0} co ngay hieu luc {1} va ngay dao han {2} khong thuoc khoang thoi gian cua chuong trinh quay", objSerialBook.Book_Number, objSerialBook.ValueDate, objSerialBook.MaturityDate);
                            log.Info(lblMessage.Text);
                            return;
                        }

                        if (objSerialBook.ValueDate < dtStartDate)
                        {
                            lblMessage.Text = string.Format("So {0} co ngay hieu luc {1} khong thuoc khoang thoi gian cua chuong trinh quay", objSerialBook.Book_Number, objSerialBook.ValueDate);
                            log.Info(lblMessage.Text);
                            return;
                        }

                        TimeSpan ts = objSerialBook.MaturityDate - objSerialBook.ValueDate;
                        if (ts.TotalDays < 28)
                        {
                            lblMessage.Text = string.Format("So {0} co tien gui {1} {2}, ngay hieu luc {3} va ngay dao han {4} khong hop le", objSerialBook.Book_Number, objSerialBook.TotalAmount.ToString(), objSerialBook.Currency, objSerialBook.ValueDate, objSerialBook.MaturityDate);
                            log.Info(lblMessage.Text);
                            return;
                        }
                        //check month
                        if ((objSerialBook.Currency == "USD") && (objSerialBook.TotalAmount < Constant.MIN_AMOUNT_USD))
                        {
                            lblMessage.Text = string.Format("So {0} co tien gui {1} USD, ngay hieu luc {2} va ngay dao han {3} khong du so tien de quay so", objSerialBook.Book_Number, objSerialBook.TotalAmount.ToString(), objSerialBook.ValueDate, objSerialBook.MaturityDate);
                            log.Info(lblMessage.Text);
                            return;
                        }
                        if ((objSerialBook.Currency == "VND") && (objSerialBook.TotalAmount < Constant.MIN_AMOUNT_VND))
                        {
                            lblMessage.Text = string.Format("So {0} co tien gui {1} VND, ngay hieu luc {2} va ngay dao han {3} khong du so tien de quay so", objSerialBook.Book_Number, objSerialBook.TotalAmount.ToString(), objSerialBook.ValueDate, objSerialBook.MaturityDate);
                            log.Info(lblMessage.Text);
                            return;
                        }
                        string strResponse = string.Empty;
                        bool result = objFunction.GetSerialBookByBookNumber(objSerialBook, dtStartDate, dtEndDate, ref strResponse);
                        if (!result)
                        {
                            lblMessage.Text = strResponse;
                            log.Info(lblMessage.Text);
                        }
                        else
                        {
                            if (objSerialBook != null)
                            {
                                objSerialBook.DAO_CODE = Session[Constant.DAO_CODE].ToString();
                                objSerialBook.DEPT_CODE = Session[Constant.DEPT_CODE].ToString();
                                objFunction.SaveSerialBook(Session[Constant.USERNAME].ToString(), objSerialBook, dtStartDate, dtEndDate, ref strResponse);
                                lblMaKhachHang.Text = objSerialBook.Customer_Id;
                                lblTenKhachHang.Text = objSerialBook.Customer_Name;
                                lblSoTienGui.Text = String.Format("{0:C}", objSerialBook.TotalAmount).Replace("$", "") + " " + objSerialBook.Currency;
                                lblKyHanGui.Text = objSerialBook.Period + " tháng";

                                if ((objSerialBook.ValueDate != null) && (objSerialBook.MaturityDate != null))
                                {
                                    lblvaluedate.Text = objSerialBook.ValueDate.ToString("dd/MM/yyyy");
                                    lblmaturitydate.Text = objSerialBook.MaturityDate.ToString("dd/MM/yyyy");
                                    ts = objSerialBook.MaturityDate - objSerialBook.ValueDate;
                                    objSerialBook.Period = (int)ts.TotalDays / 30;
                                    if ((ts.TotalDays % 30) >= 28)
                                        objSerialBook.Period++;
                                    lblKyHanGui.Text = objSerialBook.Period + " tháng";
                                }

                                if (objSerialBook.Masoduthuongs != null)
                                {
                                    cmdCapPhieu.Visible = false;
                                    cmdInphieu.Visible = true;
                                }
                                else
                                {
                                    cmdCapPhieu.Visible = true;
                                    cmdInphieu.Visible = false;
                                }
                                lblMaDuThuongCo.Text = Common.FormatSerial(objSerialBook.Masoduthuongs.ToList());
                                lblMessage.Text = "Cấp mã dự thưởng thành công!";
                                lblMessage.CssClass = "successMessage";
                                Session.Remove(Constant.SERIAL_BOOK);
                                cmdCapPhieu.Visible = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Lỗi cấp mã dự thưởng!<br />" + ex.Message;
                    lblMessage.CssClass = "errorMessage";
                    log.Fatal(ex);
                }
            }
        }

        protected void cmdKiemTra_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;

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


                BindingForm();
            }
            catch (Exception oExc)
            {
                lblMessage.Text = "Lỗi cấp mã dự thưởng!<br />" + oExc.Message;
                lblMessage.CssClass = "errorMessage";
            }
        }

        #region Method

        private void BindingForm()
        {
            ClearForm();
            Session[Constant.PHAN_HE] = lstradPhanHe.SelectedValue;
            hdfSoSo.Value = txtSoSo.Text.Trim();
            Function objFunction = new Function();
            SerialBook objSerialBook = objFunction.GetSerialBookResultByBookNumber(hdfSoSo.Value, dtStartDate, dtEndDate);
            if (objSerialBook != null)
            {
                //book is played.
                if ((objSerialBook.Masoduthuongs != null) && (objSerialBook.Masoduthuongs.Length > 0))
                {
                    lblMaDuThuongCo.Text = Common.FormatSerial(objSerialBook.Masoduthuongs.ToList());
                }
                lblMaKhachHang.Text = objSerialBook.Customer_Id;
                lblTenKhachHang.Text = objSerialBook.Customer_Name;
                lblSoTienGui.Text = String.Format("{0:C}", objSerialBook.TotalAmount).Replace("$", "") + " " + objSerialBook.Currency;
                lblKyHanGui.Text = objSerialBook.Period + " tháng";
                lblvaluedate.Text = objSerialBook.ValueDate.ToString("dd/MM/yyyy");
                lblmaturitydate.Text = objSerialBook.MaturityDate.ToString("dd/MM/yyyy");

                cmdCapPhieu.Visible = false;
                cmdInphieu.Visible = true;
                //write quayso result to session.
                string strQuaysoSession = cm_strQuaysSoSession + "_" + Session[Constant.USERNAME].ToString();
                QuaySoBook oBook = ConvertSerialToQuaysoBook(objSerialBook);
                Session[strQuaysoSession] = oBook;
                Session[Constant.SERIAL_BOOK] = objSerialBook;
            }
            else
            {
                string strBookCategory = string.Empty;
                objSerialBook = objFunction.GetSerialBookFromT24(hdfSoSo.Value, Session[Constant.PHAN_HE].ToString(), txtCIF.Text.Trim(), ref strBookCategory);

                if (objSerialBook == null)
                {
                    lblMessage.Text = "Không tìm thấy số sổ " + hdfSoSo.Value;
                    lblMessage.CssClass = "errorMessage";
                    cmdCapPhieu.Visible = false;
                }
                else
                {
                    if (objSerialBook.Book_Number.ToUpper().Contains("GIB"))
                    {
                        if (strBookCategory != "6658")//GIB book number take part in promotion to receive a gift.
                        {
                            lblMessage.Text = "Số sổ tiết kiệm điện tử không tham gia khuyến mại!";
                            lblMessage.CssClass = "errorMessage";
                            cmdCapPhieu.Visible = false;
                            return;
                        }
                    }


                    if (!objSerialBook.ThamGiaKM.ToUpper().Contains("TET VE LOC DEN"))
                    {
                        lblMessage.Text = "Số sổ tiết kiệm không tham gia khuyến mại!";
                        lblMessage.CssClass = "errorMessage";
                        cmdCapPhieu.Visible = false;
                        return;
                    }

                    if (strBookCategory == "6602" || strBookCategory == "6612" || strBookCategory == "6611" || strBookCategory == "6601")
                    {
                        Session[Constant.SERIAL_BOOK] = objSerialBook;
                        if ((objSerialBook.ValueDate != null) && (objSerialBook.MaturityDate != null))
                        {
                            lblvaluedate.Text = objSerialBook.ValueDate.ToString("dd/MM/yyyy");
                            lblmaturitydate.Text = objSerialBook.MaturityDate.ToString("dd/MM/yyyy");
                            TimeSpan ts = objSerialBook.MaturityDate - objSerialBook.ValueDate;
                            objSerialBook.Period = (int)ts.TotalDays / 30;
                            if ((ts.TotalDays % 30) >= 28)
                                objSerialBook.Period++;
                            lblKyHanGui.Text = objSerialBook.Period + " tháng";
                        }

                        lblMaKhachHang.Text = objSerialBook.Customer_Id;
                        lblTenKhachHang.Text = objSerialBook.Customer_Name;
                        lblSoTienGui.Text = String.Format("{0:C}", objSerialBook.TotalAmount).Replace("$", "") + " " + objSerialBook.Currency;
                        lblKyHanGui.Text = objSerialBook.Period + " tháng";
                        if (objSerialBook.Currency == "USD" && objSerialBook.TotalAmount < Constant.MIN_AMOUNT_USD)
                        {
                            cmdCapPhieu.Visible = false;
                            lblMessage.Text = "Sổ không được cấp mã. Số tiền gửi nhỏ hơn " + Constant.MIN_AMOUNT_USD.ToString() + " " + objSerialBook.Currency;
                            lblMessage.CssClass = "errorMessage";
                            cmdInphieu.Visible = false;
                            cmdCapPhieu.Visible = false;

                            return;
                        }
                        if (objSerialBook.Currency == "VND" && objSerialBook.TotalAmount < Constant.MIN_AMOUNT_VND)
                        {
                            lblMessage.Text = "Sổ không được cấp mã. Số tiền gửi nhỏ hơn " + Constant.MIN_AMOUNT_VND.ToString() + " " + objSerialBook.Currency;
                            lblMessage.CssClass = "errorMessage";
                            cmdInphieu.Visible = false;
                            cmdCapPhieu.Visible = false;
                            return;
                        }
                        Session[Constant.SO_SO] = objSerialBook.Book_Number;
                        cmdInphieu.Visible = false;
                        cmdCapPhieu.Visible = true;
                    }
                    else
                    {
                        lblMessage.Text = "Số sổ tiết kiệm không thuộc thể lệ tham gia khuyến mại!";
                        lblMessage.CssClass = "errorMessage";
                        cmdCapPhieu.Visible = false;
                    }
                }
            }
        }
        private Serial.QuaySoBook ConvertSerialToQuaysoBook(Serial.SerialBook oSerialBook)
        {
            if (oSerialBook != null)
            {
                Serial.QuaySoBook oBook = new Promotion.Serial.QuaySoBook();
                oBook.ACCOUNT_NUMBER = oSerialBook.ACCOUNT_NUMBER;
                oBook.Book_Number = oSerialBook.Book_Number;
                oBook.BookType = oSerialBook.BookType;
                oBook.CMTND = oSerialBook.CMTND;
                oBook.CO_CODE = oSerialBook.CO_CODE;
                oBook.Currency = oSerialBook.Currency;
                oBook.Customer_Id = oSerialBook.Customer_Id;
                oBook.Customer_Name = oSerialBook.Customer_Name;
                oBook.MaturityDate = oSerialBook.MaturityDate;
                oBook.ValueDate = oSerialBook.ValueDate;
                oBook.Period = oSerialBook.Period;
                oBook.Sector = oSerialBook.Sector;
                oBook.Telephone = oSerialBook.Telephone;
                oBook.TERM = oSerialBook.TERM;
                oBook.TotalAmount = oSerialBook.TotalAmount;
                return oBook;
            }

            return null;
        }

        private void ClearForm()
        {
            lblMaKhachHang.Text = EMPTY_TEXT;
            lblTenKhachHang.Text = EMPTY_TEXT;
            lblSoTienGui.Text = EMPTY_TEXT;
            lblKyHanGui.Text = EMPTY_TEXT;
            lblMaDuThuongCo.Text = EMPTY_TEXT;
            lblvaluedate.Text = EMPTY_TEXT;
            lblmaturitydate.Text = EMPTY_TEXT;
            lblMessage.Text = string.Empty;
            lblMessage.CssClass = "";
        }
        #endregion

        protected void txtCIF_TextChanged(object sender, EventArgs e)
        {

        }
    }
}