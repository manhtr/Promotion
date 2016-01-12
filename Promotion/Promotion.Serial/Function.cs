using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Promotion.Commons;
using Promotion.DataModel;
using log4net;


namespace Promotion.Serial
{
    public class Function
    {
        ILog log = LogManager.GetLogger(typeof(Function));
        const string m_cstrStandardSelectionName = "LD.LOANS.AND.DEPOSITS";
        public SerialBook GetSerialBookFromT24(string strBookNumber, string strHost, string strPort, string strService, string strUserId, string strPwd, string strSchema, string strBOOK_TYPE, string strCIF, ref string strCategory)
        {
            SerialBook oBook = null;
            //T24DataMining.DataMiningClient oClient = new T24DataMining.DataMiningClient();
            //bool bResult = false;
            //string strResponse = string.Empty;
            //bResult = oClient.Connect(strHost, strPort, strService, strUserId, strPwd, ref strResponse);
            //if (!bResult)
            //{
            //    log.Fatal(strResponse);
            //}
            //else
            //{
            //    try
            //    {
            //        string strAZCommandText = "SELECT ID,PASSBOOK.NUMBER,CUSTOMER,CURRENCY,PRINCIPAL,VALUE.DATE,MATURITY.DATE,CO.CODE,TERM FROM " + strSchema + ".";
            //        strAZCommandText += "FBNK_AZ_ACCOUNT WHERE PASSBOOK.NUMBER='" + strBookNumber + "'";

            //        string strLDCommandText = "SELECT ID,CONTRACT.NO,CUSTOMER.ID,CURRENCY,AMOUNT,VALUE.DATE,FIN.MAT.DATE,CO.CODE,TERM FROM " + strSchema + ".";
            //        strLDCommandText += "FBNK_LD_L002 WHERE CONTRACT.NO='" + strBookNumber + "'";

            //        if (strBOOK_TYPE.ToUpper() == "AZ")
            //        {
            //            strResponse = string.Empty;
            //            //DataSet ods = oClient.ExecSQL(strHost, strPort, strService, strUserId, strPwd, strAZCommandText, "", false, ref bResult, ref strResponse);
            //            DataSet ods = oClient.GetAZByCustomerID(strHost, strPort, strService, strSchema, strUserId, strPwd, strCIF, false, ref bResult, ref strResponse);
            //            if ((ods == null) || ods.Tables.Count <= 0)
            //            {

            //            }
            //            else
            //            {
            //                Commons.Convertion _oConvertion = new Commons.Convertion();
            //                DataTable oAZTable = ods.Tables[0];
            //                foreach (DataRow row in oAZTable.Rows)
            //                {
            //                    if (row["PASSBOOK.NUMBER"] != null && row["PASSBOOK.NUMBER"].ToString() == strBookNumber)
            //                    {
            //                        oBook = new SerialBook();
            //                        oBook.Book_Number = row["PASSBOOK.NUMBER"].ToString();
            //                        oBook.Customer_Id = row["CUSTOMER"].ToString();
            //                        oBook.Currency = row["CURRENCY"].ToString().ToUpper();
            //                        oBook.TotalAmount = double.Parse(row["PRINCIPAL"].ToString());
            //                        oBook.ValueDate = _oConvertion.ConvertToMMddyyyy(row["VALUE.DATE"].ToString());
            //                        oBook.MaturityDate = _oConvertion.ConvertToMMddyyyy(row["MATURITY.DATE"].ToString());
            //                        oBook.ACCOUNT_NUMBER = row["ID"].ToString();
            //                        oBook.CO_CODE = row["CO.CODE"].ToString();
            //                        if (row["THAM.GIA.KM"] != null)
            //                        {
            //                            oBook.ThamGiaKM = row["THAM.GIA.KM"].ToString();
            //                        }
            //                        if (oAZTable.Rows[0]["TERM"] != null && !string.IsNullOrEmpty(row["TERM"].ToString()))
            //                        {
            //                            oBook.TERM = row["TERM"].ToString();
            //                        }
            //                        else
            //                        {
            //                            oBook.TERM = row["ORG.TERM"].ToString();
            //                        }
            //                        strCategory = row["CATEGORY"].ToString();

            //                        //reset response
            //                        strResponse = string.Empty;
            //                        string strCustomerCommandText = "SELECT ID,NAME.1,LEGAL.ID,SECTOR,PHONE.1 FROM " + strSchema + ".FBNK_CUSTOMER WHERE ID='" + oBook.Customer_Id + "'";
            //                        DataSet oCustomerDs = oClient.ExecSQL(strHost, strPort, strService, strUserId, strPwd, strCustomerCommandText, "", false, ref bResult, ref strResponse);
            //                        if ((oCustomerDs == null) || (oCustomerDs.Tables.Count <= 0))
            //                        {

            //                        }
            //                        else
            //                        {
            //                            DataTable oCustomerTable = oCustomerDs.Tables[0];
            //                            oBook.Customer_Name = oCustomerTable.Rows[0]["NAME.1"].ToString();
            //                            oBook.CMTND = oCustomerTable.Rows[0]["LEGAL.ID"].ToString();
            //                            oBook.Sector = oCustomerTable.Rows[0]["SECTOR"].ToString();
            //                            oBook.Telephone = oCustomerTable.Rows[0]["PHONE.1"].ToString();
            //                        }
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //        else //LD
            //        {
            //            strResponse = string.Empty;
            //            DataSet ods = oClient.ExecSQL(strHost, strPort, strService, strUserId, strPwd, strLDCommandText, m_cstrStandardSelectionName, false, ref bResult, ref strResponse);
            //            if ((ods == null) || ods.Tables.Count <= 0)
            //            {
            //                log.Fatal(strResponse);
            //            }
            //            else
            //            {
            //                Commons.Convertion _oConvertion = new Commons.Convertion();
            //                DataTable oAZTable = ods.Tables[0];
            //                oBook = new SerialBook();
            //                oBook.Book_Number = oAZTable.Rows[0]["CONTRACT.NO"].ToString();
            //                oBook.Customer_Id = oAZTable.Rows[0]["CUSTOMER.ID"].ToString();
            //                oBook.Currency = oAZTable.Rows[0]["CURRENCY"].ToString().ToUpper();
            //                oBook.TotalAmount = double.Parse(oAZTable.Rows[0]["AMOUNT"].ToString());
            //                oBook.ValueDate = _oConvertion.ConvertToMMddyyyy(oAZTable.Rows[0]["VALUE.DATE"].ToString());
            //                oBook.MaturityDate = _oConvertion.ConvertToMMddyyyy(oAZTable.Rows[0]["FIN.MAT.DATE"].ToString());
            //                oBook.ACCOUNT_NUMBER = oAZTable.Rows[0]["ID"].ToString();
            //                oBook.CO_CODE = oAZTable.Rows[0]["CO.CODE"].ToString();
            //                if (oAZTable.Rows[0]["TERM"] != null && !string.IsNullOrEmpty(oAZTable.Rows[0]["TERM"].ToString()))
            //                {
            //                    oBook.TERM = oAZTable.Rows[0]["TERM"].ToString();
            //                }
            //                else
            //                {
            //                    oBook.TERM = oAZTable.Rows[0]["ORG.TERM"].ToString();
            //                }
            //                strCategory = oAZTable.Rows[0]["CATEGORY"].ToString();

            //                //reset response
            //                strResponse = string.Empty;
            //                string strCustomerCommandText = "SELECT ID,NAME.1,LEGAL.ID,SECTOR,PHONE.1 FROM " + strSchema + ".FBNK_CUSTOMER WHERE ID='" + oBook.Customer_Id + "'";
            //                DataSet oCustomerDs = oClient.ExecSQL(strHost, strPort, strService, strUserId, strPwd, strCustomerCommandText, "", false, ref bResult, ref strResponse);

            //                if ((oCustomerDs == null) || (oCustomerDs.Tables.Count <= 0))
            //                {
            //                    log.Fatal(strResponse);
            //                }
            //                else
            //                {
            //                    DataTable oCustomerTable = oCustomerDs.Tables[0];
            //                    oBook.Customer_Name = oCustomerTable.Rows[0]["NAME.1"].ToString();
            //                    oBook.CMTND = oCustomerTable.Rows[0]["LEGAL.ID"].ToString();
            //                    oBook.Sector = oCustomerTable.Rows[0]["SECTOR"].ToString();
            //                    oBook.Telephone = oCustomerTable.Rows[0]["PHONE.1"].ToString();
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception oExc)
            //    {
            //        log.Fatal(oExc);
            //    }
            //    finally
            //    {
            //        oClient.Close();
            //        oClient = null;
            //    }
            //    if (oBook != null)
            //    {
            //        oBook.Customer_Name = SplitT24CustomerName(oBook.Customer_Name);
            //        TimeSpan ts = oBook.MaturityDate - oBook.ValueDate;
            //        int nPeriod = (int)(ts.TotalDays / 30);
            //        int nRemain = (int)(ts.TotalDays % 30);
            //        if (nRemain >= 28) nPeriod++;
            //        if (nPeriod > 0)
            //        {
            //            oBook.PeriodInWord = string.Format("{0} tháng.", nPeriod.ToString());
            //        }
            //        else
            //        {
            //            int nWeeks = nRemain / 7;
            //            oBook.PeriodInWord = string.Format("{0} tuần.", nWeeks.ToString());
            //        }
            //    }
            //}

            return oBook;
        }

        public SerialBook GetSerialBookResultByBookNumber(string strBookNumber, DateTime dtFrom, DateTime dtTo)
        {
            SerialBook arr = new SerialBook();
            using (DataModel.SerialResult_BO objSerialResult_BO = new SerialResult_BO())
            {
                DataModel.SerialResult objSerialResult = objSerialResult_BO.GetByBook_numberAndInput_time(strBookNumber, dtFrom, dtTo);
                if (objSerialResult != null)
                {
                    arr = ConvertTo(objSerialResult);
                }
                else
                {
                    arr = null;
                }
            }
            return arr;
        }

        private SerialBook[] ConvertTo(DataTable oTable)
        {
            ArrayList arr = new ArrayList();
            if ((oTable == null) || (oTable.Rows.Count <= 0))
                return (SerialBook[])arr.ToArray(typeof(SerialBook));
            foreach (DataRow oRow in oTable.Rows)
            {
                SerialBook oItem = new SerialBook();
                oItem.ID = long.Parse(oRow["id"].ToString());
                oItem.Customer_Id = oRow["customer_id"].ToString();
                oItem.Customer_Name = oRow["customer_name"].ToString();
                oItem.CMTND = oRow["CMND"].ToString();
                oItem.Telephone = oRow["tel"].ToString();
                oItem.Book_Number = oRow["book_number"].ToString();
                oItem.TotalAmount = double.Parse(oRow["amount"].ToString());
                oItem.Period = int.Parse(oRow["kyhan"].ToString());
                oItem.Currency = oRow["Currency"].ToString();
                string strValueDate = oRow["EFFECT_DATE"].ToString();
                oItem.ValueDate = DateTime.Parse(strValueDate);
                oItem.MaturityDate = DateTime.Parse(oRow["MATURITY_DATE"].ToString());
                oItem.Sector = oRow["Sector"].ToString();
                oItem.TERM = oRow["TERM"].ToString();
                oItem.CO_CODE = oRow["CO_CODE"].ToString();
                oItem.ACCOUNT_NUMBER = oRow["ACCT_NUM"].ToString();
                try
                {
                    oItem.Times = int.Parse(oRow["Times"].ToString());//no longer using.
                }
                catch { oItem.Times = 1; }
                try
                {
                    oItem.InputTime = DateTime.Parse(oRow["Input_time"].ToString());
                }
                catch { }
                try
                {
                    oItem.SavedBy = oRow["SavedBy"].ToString();
                }
                catch
                {

                }
                //process serials
                string strSerials = oRow["Serials"].ToString();
                if (strSerials.Length > 0)
                {
                    strSerials = strSerials.Substring(0, strSerials.Length - 1);
                    string[] Serials = strSerials.Split("|".ToCharArray());
                    oItem.Masoduthuongs = Serials;
                }
                else
                {
                    ArrayList arrTmp = new ArrayList();
                    oItem.Masoduthuongs = (string[])arrTmp.ToArray(typeof(string));
                }
                try
                {
                    oItem.cMoney = int.Parse(oRow["cMoney"].ToString());
                }
                catch { oItem.cMoney = 0; }
                try
                {
                    oItem.Seq = long.Parse(oRow["seq"].ToString());
                }
                catch { oItem.Seq = 0; }
                arr.Add(oItem);
            }

            return (SerialBook[])arr.ToArray(typeof(SerialBook));
        }

        private SerialBook ConvertTo(DataModel.SerialResult objSerialResult)
        {

            SerialBook oItem = new SerialBook();
            oItem.ID = objSerialResult.id;
            oItem.Customer_Id = objSerialResult.customer_id;
            oItem.Customer_Name = objSerialResult.customer_name;
            oItem.CMTND = objSerialResult.CMND;
            oItem.Telephone = objSerialResult.tel;
            oItem.Book_Number = objSerialResult.book_number;
            oItem.TotalAmount = objSerialResult.amount.Value;
            oItem.Period = objSerialResult.kyhan;
            oItem.Currency = objSerialResult.currency;
            oItem.ValueDate = objSerialResult.EFFECT_DATE.Value;
            oItem.PeriodInWord = objSerialResult.TERM;
            oItem.MaturityDate = objSerialResult.MATURITY_DATE.Value;
            oItem.Sector = objSerialResult.Sector;
            oItem.TERM = objSerialResult.TERM;
            oItem.CO_CODE = objSerialResult.CO_CODE;
            oItem.ACCOUNT_NUMBER = objSerialResult.ACCT_NUM;
            oItem.DAO_CODE = objSerialResult.DAO_CODE;
            oItem.ACCOUNT_NUMBER = objSerialResult.DEPT_CODE;
            try
            {
                oItem.Times = objSerialResult.Times.Value;
            }
            catch { oItem.Times = 1; }
            try
            {
                oItem.InputTime = objSerialResult.Input_time.Value;
            }
            catch { }
            try
            {
                oItem.SavedBy = objSerialResult.SavedBy;
            }
            catch
            {

            }
            //process serials
            string strSerials = objSerialResult.Serials;
            if (strSerials.Length > 0)
            {
                strSerials = strSerials.Substring(0, strSerials.Length - 1);
                string[] Serials = strSerials.Split("|".ToCharArray());
                oItem.Masoduthuongs = Serials;
            }
            else
            {
                ArrayList arrTmp = new ArrayList();
                oItem.Masoduthuongs = (string[])arrTmp.ToArray(typeof(string));
            }
            try
            {
                oItem.cMoney = objSerialResult.cMoney.Value;
            }
            catch { oItem.cMoney = 0; }
            try
            {
                oItem.Seq = objSerialResult.Seq.Value;
            }
            catch { oItem.Seq = 0; }

            return oItem;
        }

        public bool IsSerialBookResultExisted(string strCustomer_Id, string strBook_Number, DateTime dtFrom, DateTime dtTo)
        {
            bool result = false;
            using (Promotion.DataModel.SerialResult_BO objSerialResult_BO = new SerialResult_BO())
            {
                List<Promotion.DataModel.SerialResult> lstData = objSerialResult_BO.GetByCustomer_idAndBook_numberAndInput_time(strCustomer_Id, strBook_Number, dtFrom, dtTo).ToList();
                if (lstData != null && lstData.Count > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool DeleteSerialBookByBookNumber(string strBook_Number, string strSavedBy, DateTime dtFrom, DateTime dtTo, ref string strResponse)
        {
            bool result = false;
            strResponse = string.Empty;
            using (Promotion.DataModel.SerialResult_BO objSerialResult_BO = new SerialResult_BO())
            {
                DataModel.SerialResult objSerialResult = objSerialResult_BO.GetByBook_numberAndInput_time(strBook_Number, dtFrom, dtTo);
                if (objSerialResult != null)
                {
                    string strSerials = objSerialResult.Serials;
                    string[] Serials = null;
                    if (strSerials.Length > 0)
                    {
                        strSerials = strSerials.Substring(0, strSerials.Length - 1);
                        Serials = strSerials.Split("|".ToCharArray());
                    }
                    else
                    {
                        ArrayList arrTmp = new ArrayList();
                        Serials = (string[])arrTmp.ToArray(typeof(string));
                    }
                    log.Info(strSerials);
                    using (Promotion.DataModel.DelSerial_BO objDelSerial_BO = new DelSerial_BO())
                    {
                        for (int nIndex = 0; nIndex < Serials.Length; nIndex++)
                        {
                            DelSerial objDelSerial = new DelSerial();
                            objDelSerial.book_number = objSerialResult.book_number;
                            objDelSerial.Serial = Serials[nIndex];
                            objDelSerial.input_time = DateTime.Now;
                            objDelSerial.SavedBy = strSavedBy;
                            objDelSerial_BO.CreateDelSerial(objDelSerial);
                            log.Info(strBook_Number + ": Xóa " + Serials[nIndex]);
                        }
                    }
                    objSerialResult.DeletedBy = strSavedBy;
                    objSerialResult.DeletedDate = DateTime.Now;
                    objSerialResult_BO.DeleteSerialResult(objSerialResult);
                    result = true;
                }
                else
                {
                    strResponse = string.Format("Số sổ {0} không tồn tại.", strBook_Number);
                    log.Info(strResponse);
                }
            }
            return result;
        }

        public string SplitT24CustomerName(string strCustomerName)
        {
            Char ch = (Char)253;
            string strResult = strCustomerName.Split(ch.ToString().ToCharArray())[0];
            return strResult;
        }

        public bool GetSerialBookByBookNumber(SerialBook objSerialBook, DateTime dtFrom, DateTime dtTo, ref string strResponse)
        {
            bool result = false;
            strResponse = string.Empty;
            if (objSerialBook == null)
            {

            }
            else
            {
                TimeSpan ts = objSerialBook.MaturityDate - objSerialBook.ValueDate;
                int nPeriod = (int)(ts.TotalDays / 30);
                int nRemain = (int)(ts.TotalDays % 30);
                if (nRemain >= 28) nPeriod++;
                objSerialBook.Period = nPeriod;

                if (objSerialBook.Period > 0)
                {
                    //month
                    CalculateSerialByMonth(objSerialBook);
                }
                else
                {
                    //week
                    CalculateSerialByWeek(objSerialBook, nRemain);
                }
                //initiate serial number 
                string[] strSerials = GenerateSerialNumber();
                ArrayList arrMasoduthuongs = new ArrayList();
                SerialMax objSerialMax = new SerialMax();
                using (Promotion.DataModel.SerialMax_BO objSerialMax_BO = new SerialMax_BO())
                {
                    objSerialMax = objSerialMax_BO.GetTop(1, dtFrom, dtTo).FirstOrDefault();
                }
                long lIndex = 0;
                if (objSerialMax != null)
                {
                    lIndex += objSerialMax.MaxSerial;
                }

                if ((lIndex + objSerialBook.Soluong) < Constant.MAX_SERIAL_COUNT)
                {
                    long nIdx = lIndex + objSerialBook.Soluong;
                    using (Promotion.DataModel.SerialMax_BO objSerialMax_BO = new SerialMax_BO())
                    {
                        objSerialMax = objSerialMax_BO.GetTop(1, dtFrom, dtTo).FirstOrDefault();
                        long lSerial = long.Parse(strSerials[nIdx]);
                        if (objSerialMax != null)
                        {
                            objSerialMax.MaxSerial = lSerial;
                            objSerialMax.input_time = DateTime.Now;
                            objSerialMax_BO.UpdateSerialMax(objSerialMax);
                        }
                        else
                        {
                            objSerialMax = new SerialMax();
                            objSerialMax.MaxSerial = lSerial;
                            objSerialMax.input_time = DateTime.Now;
                            objSerialMax_BO.CreateSerialMax(objSerialMax);
                        }
                    }
                    for (long nIndex = (lIndex + 1); nIndex <= (lIndex + objSerialBook.Soluong); nIndex++)
                    {
                        arrMasoduthuongs.Add(strSerials[nIndex]);
                    }
                }
                else if ((lIndex + objSerialBook.Soluong) >= Constant.MAX_SERIAL_COUNT)
                {
                    List<DelSerial> arrDel = new List<DelSerial>();
                    using (Promotion.DataModel.DelSerial_BO objDelSerial_BO = new DelSerial_BO())
                    {
                        arrDel = objDelSerial_BO.GetByDatTime(dtFrom, dtTo).ToList();
                    }

                    if ((lIndex + objSerialBook.Soluong) > (Constant.MAX_SERIAL_COUNT + arrDel.Count))
                    {
                        long nRemainSerial = (Constant.MAX_SERIAL_COUNT - lIndex) + arrDel.Count;
                        strResponse = string.Format("Không đủ mã dự thưởng để cấp.Số lượng mã dự thưởng còn lại {0}, số lượng phải cấp {1}.", nRemainSerial, (objSerialBook.Soluong));
                        log.Info(strResponse);
                    }
                    using (Promotion.DataModel.SerialMax_BO objSerialMax_BO = new SerialMax_BO())
                    {

                        objSerialMax = objSerialMax_BO.GetTop(1, dtFrom, dtTo).FirstOrDefault();
                        long lSerial = long.Parse(strSerials[Constant.MAX_SERIAL_COUNT]);

                        if (objSerialMax != null)
                        {
                            objSerialMax.MaxSerial = lSerial;
                            objSerialMax.input_time = DateTime.Now;
                            objSerialMax_BO.UpdateSerialMax(objSerialMax);
                        }
                        else
                        {
                            objSerialMax = new SerialMax();
                            objSerialMax.MaxSerial = lSerial;
                            objSerialMax.input_time = DateTime.Now;
                            objSerialMax_BO.CreateSerialMax(objSerialMax);
                        }
                    }
                    int nSerialCount = 0;
                    for (long i = lIndex + 1; i <= Constant.MAX_SERIAL_COUNT; i++)
                    {
                        nSerialCount++;
                        arrMasoduthuongs.Add(strSerials[i]);
                    }
                    if (nSerialCount < objSerialBook.Soluong)
                    {
                        using (Promotion.DataModel.DelSerial_BO objDelSerial_BO = new DelSerial_BO())
                        {
                            foreach (DelSerial oSerial in arrDel)
                            {
                                if (nSerialCount >= objSerialBook.Soluong)
                                {
                                    break;
                                }
                                arrMasoduthuongs.Add(oSerial.Serial);
                                objDelSerial_BO.DeleteDelSerial(oSerial.Id);
                                nSerialCount++;
                            }
                        }
                    }
                }
                objSerialBook.Masoduthuongs = (string[])arrMasoduthuongs.ToArray(typeof(string));
                result = true;
            }
            return result;
        }

        public bool SaveSerialBook(string strUserName, SerialBook oBook, DateTime dtStart, DateTime dtEnd, ref string strResponse)
        {
            bool result = false;
            if (oBook == null)
            {
                strResponse = "Dữ liệu không phù hợp";
            }
            else
            {
                if (IsSerialBookResultExisted(oBook.Customer_Id, oBook.Book_Number, dtStart, dtEnd))
                {
                    strResponse = string.Format("Số sổ book_number {0} của khách hàng {1} đã tồn tại!", oBook.Customer_Id, oBook.Book_Number);
                    log.Info(strResponse);
                }
                else
                {
                    try
                    {
                        string strSerials = string.Empty;
                        foreach (string str in oBook.Masoduthuongs)
                        {
                            strSerials += str + "|";
                        }
                        int nSeq = 0;
                        using (Promotion.DataModel.LuckySerial_BO objLuckySerial_BO = new LuckySerial_BO())
                        {
                            List<Promotion.DataModel.LuckySerial> lstData = objLuckySerial_BO.GetByInputTime(dtStart, dtEnd).ToList();
                            if (lstData != null && lstData.Count > 0)
                            {
                                nSeq = lstData.Count;
                            }

                        }
                        nSeq++;

                        using (Promotion.DataModel.SerialResult_BO objSerialResult_BO = new SerialResult_BO())
                        {
                            Promotion.DataModel.SerialResult objSerialResult = new SerialResult();
                            objSerialResult.book_number = oBook.Book_Number;
                            objSerialResult.customer_id = oBook.Customer_Id;
                            objSerialResult.customer_name = oBook.Customer_Name;
                            objSerialResult.CMND = oBook.CMTND;
                            objSerialResult.tel = oBook.Telephone;
                            objSerialResult.amount = oBook.TotalAmount;
                            objSerialResult.kyhan = oBook.Period;
                            objSerialResult.currency = oBook.Currency;
                            objSerialResult.Serials = strSerials;
                            objSerialResult.Input_time = DateTime.Now;
                            objSerialResult.SavedBy = strUserName;
                            objSerialResult.EFFECT_DATE = oBook.ValueDate;
                            objSerialResult.MATURITY_DATE = oBook.MaturityDate;
                            objSerialResult.Sector = oBook.Sector;
                            objSerialResult.ACCT_NUM = oBook.ACCOUNT_NUMBER;
                            objSerialResult.CO_CODE = oBook.CO_CODE;
                            objSerialResult.TERM = oBook.TERM;
                            objSerialResult.Seq = nSeq;
                            objSerialResult.DAO_CODE = oBook.DAO_CODE;
                            objSerialResult.DEPT_CODE = oBook.DEPT_CODE;
                            //objSerialResult.cMoney = oBook.TERM;
                            objSerialResult_BO.CreateSerialResult(objSerialResult);

                        }
                        log.Info(strSerials);
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        log.Fatal(ex);
                    }
                }
            }

            return result;
        }

        private void CalculateSerialByMonth(SerialBook objSerialBook)
        {
            List<SerialByMonth> arr = new List<SerialByMonth>();
            using (Promotion.DataModel.SerialByMonth_BO objSerialByMonth_BO = new SerialByMonth_BO())
            {
                arr = objSerialByMonth_BO.GetTop(36).ToList();
            }

            if (objSerialBook != null && arr != null && arr.Count > 0)
            {
                if (objSerialBook.Currency == "VND")
                {
                    int nPart = (int)(objSerialBook.TotalAmount / arr[0].VND);
                    SerialByMonth oItem = FindSerialMonthByPeriod(objSerialBook.Period, arr);
                    if (oItem != null)
                    {
                        objSerialBook.Soluong = oItem.SoPhieu * nPart;
                    }
                }
                else
                {
                    int nPart = (int)(objSerialBook.TotalAmount / arr[0].USD);
                    SerialByMonth oItem = FindSerialMonthByPeriod(objSerialBook.Period, arr);
                    if (oItem != null)
                    {
                        objSerialBook.Soluong = oItem.SoPhieu * nPart;
                    }
                }
            }
        }

        private void CalculateSerialByWeek(SerialBook objSerialBook, int nRemaindays)
        {
            List<SerialByWeek> arr = new List<SerialByWeek>();
            using (Promotion.DataModel.SerialByWeek_BO objSerialByWeek_BO = new SerialByWeek_BO())
            {
                arr = objSerialByWeek_BO.GetTop(36).ToList();
            }
            if (objSerialBook != null && arr != null && arr.Count > 0)
            {
                int nWeeks = nRemaindays / 7;
                if (objSerialBook.Currency == "VND")
                {
                    int nPart = (int)(objSerialBook.TotalAmount / arr[0].VND);
                    SerialByWeek oItem = FindSerialWeekByPeriod(nWeeks, arr);
                    if (oItem != null)
                    {
                        objSerialBook.Soluong = oItem.SoPhieu * nPart;
                    }
                }
                else
                {
                    int nPart = (int)(objSerialBook.TotalAmount / arr[0].USD);
                    SerialByWeek oItem = FindSerialWeekByPeriod(nWeeks, arr);
                    if (oItem != null)
                    {
                        objSerialBook.Soluong = oItem.SoPhieu * nPart;
                    }
                }
            }

        }

        private SerialByMonth FindSerialMonthByPeriod(int nPeriod, List<SerialByMonth> arr)
        {
            foreach (SerialByMonth oItem in arr)
            {
                if (oItem.Period == nPeriod)
                    return oItem;
            }
            return null;
        }

        private SerialByWeek FindSerialWeekByPeriod(int nPeriod, List<SerialByWeek> arr)
        {
            foreach (SerialByWeek oItem in arr)
            {
                if (oItem.Period == nPeriod)
                    return oItem;
            }
            return null;
        }

        private string[] GenerateSerialNumber()
        {
            try
            {
                ArrayList arr = new ArrayList();
                int[] arrNumbers = { 0, 0, 0, 0, 0, 0 };
                int i = arrNumbers.Length - 1;
                while (i >= 0)
                {
                    i = arrNumbers.Length - 1;
                    Generatestring(arrNumbers, arr);
                    while ((i >= 0) && (arrNumbers[i] >= 9)) i--;
                    if (i >= 0)
                    {
                        arrNumbers[i]++;
                        for (int nIndex = i + 1; nIndex <= (arrNumbers.Length - 1); nIndex++)
                        {
                            arrNumbers[nIndex] = 0;
                        }
                    }
                }
                return (string[])arr.ToArray(typeof(string));
            }
            catch (Exception oExc)
            {
                log.Fatal(oExc);
                return null;
            }
        }

        private void Generatestring(int[] arrNumbers, ArrayList arrMemory)
        {
            string strNumber = string.Empty;
            foreach (int n in arrNumbers)
            {
                strNumber += n.ToString();
            }
            arrMemory.Add(strNumber);
        }
    }
}
