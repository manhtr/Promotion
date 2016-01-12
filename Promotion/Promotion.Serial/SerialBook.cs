using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promotion.Serial
{
    public class SerialBook : Book
    {
        long _lId = 0;
        public long ID
        {
            get { return _lId; }
            set { _lId = value; }
        }
        int _nsoluong = 0;
        public int Soluong
        {
            get { return _nsoluong; }
            set { _nsoluong = value; }
        }
        string[] _arrMasoduthuong;
        public string[] Masoduthuongs
        {
            get { return _arrMasoduthuong; }
            set { _arrMasoduthuong = value; }
        }
        DateTime _dtInputTime;
        public DateTime InputTime
        {
            get { return _dtInputTime; }
            set { _dtInputTime = value; }
        }
        string _strSavedBy = string.Empty;
        public string SavedBy
        {
            get { return _strSavedBy; }
            set { _strSavedBy = value; }
        }
        int _nTimes = 0;
        public int Times
        {
            get { return _nTimes; }
            set { _nTimes = value; }
        }
        int _ncMoney = 0;
        public int cMoney
        {
            get { return _ncMoney; }
            set { _ncMoney = value; }
        }
        long _lseq = 0;
        public long Seq
        {
            get { return _lseq; }
            set { _lseq = value; }
        }

        string strThamGiaKM = string.Empty;
        public  string ThamGiaKM
        {
            get { return strThamGiaKM; }
            set { strThamGiaKM = value; }
        }

        string strDAO_CODE = string.Empty;
        public string DAO_CODE
        {
            get { return strDAO_CODE; }
            set { strDAO_CODE = value; }
        }

        string strDEPT_CODE = string.Empty;
        public string DEPT_CODE
        {
            get { return strDEPT_CODE; }
            set { strDEPT_CODE = value; }
        }
    }
}
