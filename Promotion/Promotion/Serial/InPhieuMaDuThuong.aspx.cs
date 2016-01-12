using System;
using System.Collections.Specialized;
using System.Linq;
using Promotion.Commons;
using log4net;

namespace Promotion.Serial
{
    public partial class InPhieuMaDuThuong : System.Web.UI.Page
    {
        ILog log = LogManager.GetLogger(typeof(InPhieuMaDuThuong));
        DateTime dtStartDate = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["StartDate"]);
        DateTime dtEndDate = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["EndDate"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constant.USERNAME] == null)
            {
                Response.Redirect(Common.GetRootRequest() + "Account/Login.aspx");
            }

            if (Request["bn"] != null && !string.IsNullOrEmpty(Request["bn"]))
            {
                BindReport(Request["bn"]);
            }
            if (!IsPostBack)
            {
                if (Session[Constant.USERNAME] != null)
                {
                    log.Info(Session[Constant.USERNAME].ToString() + ": truy cập trang " + Page.Title);
                }
            }
        }

        private void BindReport(string strSoSo)
        {
            rptMaSoDuThuong tblReport = new rptMaSoDuThuong();
            Promotion.Serial.Function objFunction = new Function();
            tblReport.SetReportParameter(Constant.TEN_CHUONG_TRINH, "Khách hàng đã đọc, hiểu thể lệ " + Constant.TEN_CHUONG_TRINH + " và đồng ý tham gia Chương trình.");
            if (Session[Constant.USERNAME] != null)
            {
                Promotion.Serial.SerialBook objSerialBook = objFunction.GetSerialBookResultByBookNumber(strSoSo, dtStartDate, dtEndDate);
                if (objSerialBook != null)
                {
                    tblReport.Parameters["SOSO"].Value = objSerialBook.Book_Number;
                    if (objSerialBook.ValueDate != null)
                    {
                        tblReport.Parameters["NGAY_CAP_MA"].Value = "Ngày " + System.DateTime.Now.ToString("dd") + " tháng " + System.DateTime.Now.ToString("MM") + " năm " + System.DateTime.Now.ToString("yyyy");
                    }
                    string strCustomerName = string.Empty;
                    if (objSerialBook.Customer_Name.IndexOf("ý") > 0)
                    {
                        strCustomerName = objSerialBook.Customer_Name.Substring(0, objSerialBook.Customer_Name.IndexOf("ý"));
                    }
                    else
                    {
                        strCustomerName = objSerialBook.Customer_Name;
                    }
                    tblReport.Parameters["CUSNAME"].Value = strCustomerName;
                    tblReport.Parameters["CMND"].Value = objSerialBook.CMTND;
                    tblReport.Parameters["PHONE"].Value = objSerialBook.Telephone;
                    tblReport.Parameters["KY_HAN"].Value = objSerialBook.Period.ToString() + " tháng";
                    tblReport.Parameters["AMOUNT"].Value = String.Format("{0:C}", objSerialBook.TotalAmount).Replace("$", "") + " " + objSerialBook.Currency;
                    //preprocess lucky serial
                    int nLuckyLength = 6 - objSerialBook.Seq.ToString().Length;
                    string strLucky = objSerialBook.Seq.ToString();
                    int nLuckySerial = int.Parse(strLucky);
                    for (int nIndex = 0; nIndex < nLuckyLength; nIndex++)
                    {
                        strLucky = "0" + strLucky;
                    }

                    tblReport.Parameters["LuckySerial"].Value = (nLuckySerial <= 11000) ? strLucky : string.Empty;

                    string strSoTienBangChu = string.Empty;

                    if (objSerialBook.Currency == "VND")
                    {
                        string TEMP = string.Empty;
                        TEMP = objSerialBook.TotalAmount.ToString().Replace(",", "").Replace(".00 VND", "");
                        strSoTienBangChu = Spell(TEMP.ToString(), "đồng");

                    }
                    if (objSerialBook.Currency == "USD")
                    {

                        int dau = objSerialBook.TotalAmount.ToString().IndexOf(".");
                        if (dau > 0)
                        {
                            string phan_chan = objSerialBook.TotalAmount.ToString().Substring(0, dau);
                            string phan_le = objSerialBook.TotalAmount.ToString().Substring(dau, objSerialBook.TotalAmount.ToString().Length - dau).Replace(".", "");
                            string doc_le = Spell(phan_le.ToString(), "cent");
                            string doc_chan = Spell(phan_chan.ToString(), "đô la");
                            strSoTienBangChu = doc_chan + " " + doc_le;

                        }
                        else
                        {
                            strSoTienBangChu = Spell(objSerialBook.TotalAmount.ToString(), "đô la");

                        }
                    }
                    if (!string.IsNullOrEmpty(strSoTienBangChu))
                    {
                        string strFirstLetter = strSoTienBangChu.Substring(0, 1);
                        strFirstLetter = strFirstLetter.ToUpper();
                        strSoTienBangChu = strFirstLetter + strSoTienBangChu.Substring(1);
                    }
                    tblReport.Parameters["AMOUNT_CHU"].Value = strSoTienBangChu;
                    tblReport.Parameters["SERIAL"].Value = Common.FormatSerial(objSerialBook.Masoduthuongs.ToList());
                }
            }

            ReportViewer1.Report = tblReport;
        }

        StringDictionary dicNumberSpell = null;
        void DefineDic()
        {
            dicNumberSpell = new StringDictionary();
            dicNumberSpell["0"] = "không";
            dicNumberSpell["1"] = "một";
            dicNumberSpell["2"] = "hai";
            dicNumberSpell["3"] = "ba";
            dicNumberSpell["4"] = "bốn";
            dicNumberSpell["5"] = "năm";
            dicNumberSpell["6"] = "sáu";
            dicNumberSpell["7"] = "bảy";
            dicNumberSpell["8"] = "tám";
            dicNumberSpell["9"] = "chín";
            dicNumberSpell["10"] = "mười";
        }
        /// <summary>
        /// Sinh ra số tiền bằng chữ
        /// </summary>
        /// <param name="strNumber">Số tiền. VD: 90000</param>
        /// <param name="strDonViTruoc">Mã tiền tệ hoặc đơn vị. Ví dụ: đồng, triệu</param>
        /// <returns></returns>
        public string Spell(string strNumber, string strDonViTruoc)
        {
            if (dicNumberSpell == null)
                DefineDic();
            string strDonVi = "";
            string strNumberLeft = "";
            string strNumberRight = "";
            string strResult;
            int iSplit = 0;
            if (strNumber.Length > 3)
            {
                if (strNumber.Length > 9)
                {
                    strDonVi = "tỷ";
                    iSplit = 9;
                }
                else
                    if (strNumber.Length > 6)
                    {
                        strDonVi = "triệu";
                        iSplit = 6;
                    }
                    else
                    {
                        strDonVi = "nghìn";
                        iSplit = 3;
                    }
                strNumberLeft = strNumber.Substring(0, strNumber.Length - iSplit);
                strNumberRight = strNumber.Substring(strNumber.Length - iSplit, iSplit);
                string spellLeft = Spell(strNumberLeft, strDonVi);
                string spellRight = Spell(strNumberRight, strDonViTruoc);
                if ((spellLeft != "") && (spellRight != ""))
                    strResult = String.Format("{0} {1}", spellLeft, spellRight);
                else
                {
                    if (spellRight == "")
                    {
                        if (spellLeft != "")
                            strResult = String.Format("{0} {1}", spellLeft, strDonViTruoc);
                        else
                            strResult = "";
                    }
                    else
                    {
                        strResult = spellRight;
                    }
                }
                return strResult;
            }
            else
            {
                if ((strNumber == "000") || (strNumber == "00") || (strNumber == "0")) return "";
                if (strNumber.IndexOf("00") == 0)
                {
                    strResult = String.Format("không trăm linh {0} {1}", dicNumberSpell[strNumber[2].ToString()], strDonViTruoc);
                    return strResult;
                }
                string strBackup = strNumber;
                strResult = "";
                if ((strNumber.Length == 3))
                {
                    strResult = String.Format("{0} trăm", dicNumberSpell[strNumber[0].ToString()]);
                    strNumber = strNumber.Remove(0, 1);
                }
                if (strNumber != "00")
                {
                    if (strNumber[0] == '0')
                        strResult = String.Format("{0} linh {1} {2}", strResult, dicNumberSpell[strNumber[1].ToString()], strDonViTruoc);
                    else
                    {
                        if (strNumber == "10")
                        {
                            strResult = String.Format("{0} {1} {2}", strResult, dicNumberSpell[strNumber], strDonViTruoc);
                        }
                        else
                        {
                            if (strNumber.Length == 2)
                            {
                                if (strNumber[0] != '1')
                                    strResult = String.Format("{0} {1} mươi", strResult, dicNumberSpell[strNumber[0].ToString()]);
                                else
                                    strResult = String.Format("{0} mười", strResult);
                                if ((strNumber[1] != '5') && (strNumber[1] != '1') && (strNumber[1] != '0'))
                                    strResult = String.Format("{0} {1} {2}", strResult, dicNumberSpell[strNumber[1].ToString()], strDonViTruoc);
                                else
                                {
                                    if (strNumber[1] == '5')
                                    {
                                        strResult = String.Format("{0} lăm {1}", strResult, strDonViTruoc);
                                    }
                                    if (strNumber[1] == '1')
                                    {
                                        if (strNumber[0] != '1')
                                            strResult = String.Format("{0} mốt {1}", strResult, strDonViTruoc);
                                        else
                                            strResult = String.Format("{0} một {1}", strResult, strDonViTruoc);
                                    }
                                    if (strNumber[1] == '0')
                                        strResult = String.Format("{0} {1}", strResult, strDonViTruoc);
                                }
                            }
                            else
                            {
                                strResult = String.Format("{0} {1}", dicNumberSpell[strNumber], strDonViTruoc);
                            }
                        }
                    }
                    return strResult;
                }
                else
                {
                    strResult = String.Format("{0} {1}", strResult, strDonViTruoc);
                }
                strNumber = strBackup;
                return strResult;
            }

        }
    }
}