using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promotion.Serial
{
    public class Book
    {
        string _strBook_Number = string.Empty;
        public string Book_Number
        {
            get { return _strBook_Number; }
            set { _strBook_Number = value; }
        }
        int _nPeriod = 0;
        public int Period
        {
            get { return _nPeriod; }
            set { _nPeriod = value; }
        }
        string _strPeriodInWord = string.Empty;
        public string PeriodInWord
        {
            get { return _strPeriodInWord; }
            set { _strPeriodInWord = value; }
        }
        double _dblTotalAmount = 0;
        public double TotalAmount
        {
            get { return _dblTotalAmount; }
            set { _dblTotalAmount = value; }
        }
        string _strCustomer_Id = string.Empty;
        public string Customer_Id
        {
            get { return _strCustomer_Id; }
            set { _strCustomer_Id = value; }
        }
        string _strCustomer_Name = string.Empty;
        public string Customer_Name
        {
            get { return _strCustomer_Name; }
            set { _strCustomer_Name = value; }
        }
        string _strCMND = string.Empty;
        public string CMTND
        {
            get { return _strCMND; }
            set { _strCMND = value; }
        }
        string _strTelephone = string.Empty;
        public string Telephone
        {
            get { return _strTelephone; }
            set { _strTelephone = value; }
        }
        string _strSector = string.Empty;
        public string Sector
        {
            get { return _strSector; }
            set { _strSector = value; }
        }
        string _strCurrency = string.Empty;
        public string Currency
        {
            get { return _strCurrency; }
            set { _strCurrency = value; }
        }
        DateTime _dtValueDate;
        public DateTime ValueDate
        {
            get { return _dtValueDate; }
            set { _dtValueDate = value; }
        }
        DateTime _dtMaturityDate;
        public DateTime MaturityDate
        {
            get { return _dtMaturityDate; }
            set { _dtMaturityDate = value; }
        }
        string _strID = string.Empty;
        public string ACCOUNT_NUMBER
        {
            get { return _strID; }
            set { _strID = value; }
        }
        string _strCO_CODE = string.Empty;
        public string CO_CODE
        {
            get { return _strCO_CODE; }
            set { _strCO_CODE = value; }
        }
        string _strTERM = string.Empty;
        public string TERM
        {
            get { return _strTERM; }
            set { _strTERM = value; }
        }
        string _strBookType = "AZ";
        public string BookType
        {
            get { return _strBookType; }
            set { _strBookType = value; }
        }
    }
}
