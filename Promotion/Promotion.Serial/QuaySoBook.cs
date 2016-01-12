using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promotion.Serial
{
    public class QuaySoBook : Book
    {
        long _lId = 0;
        public long ID
        {
            get { return _lId; }
            set { _lId = value; }
        }
        double _dblWin_Amount = 0;
        public double Win_Amount
        {
            get { return _dblWin_Amount; }
            set { _dblWin_Amount = value; }
        }
        string _strWinAmountInWord = string.Empty;
        public string WinAmountInWord
        {
            set { _strWinAmountInWord = value; }
            get { return _strWinAmountInWord; }
        }
        int _nSoluongThe = 0;
        public int SoluongThe
        {
            get { return _nSoluongThe; }
            set { _nSoluongThe = value; }
        }
        //TheCao[] _arrTheCaos;
        //public TheCao[] TheCaos
        //{
        //    get { return _arrTheCaos; }
        //    set { _arrTheCaos = value; }
        //}
        int _nSoluongMBH = 0;
        public int MuBaoHiem
        {
            get { return _nSoluongMBH; }
            set { _nSoluongMBH = value; }
        }
        int _nSoluongAoMua = 0;
        public int Aomua
        {
            get { return _nSoluongAoMua; }
            set { _nSoluongAoMua = value; }
        }
        int _nSoluongTheTrung = 0;
        public int SoluongTheTrung
        {
            get { return _nSoluongTheTrung; }
            set { _nSoluongTheTrung = value; }
        }
        int _nSoluongTheTruot = 0;
        public int SoluongTheTruot
        {
            get { return _nSoluongTheTruot; }
            set { _nSoluongTheTruot = value; }
        }
        public int GetSoluongTheTrung()
        {
            int nResult = 0;
            //foreach (TheCao oItem in _arrTheCaos)
            //{
            //    if (oItem.SOLUONG > 0)
            //        nResult += oItem.SOLUONG;
            //}
            return nResult;
        }
        public int GetSoluongTheTruot()
        {
            int nResult = 0;
            int nSothetrung = GetSoluongTheTrung();
            nResult = SoluongThe - nSothetrung;
            return nResult;
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
        int _nPoints = 0;
        public int Points
        {
            get { return _nPoints; }
            set { _nPoints = value; }
        }
        int _ncMoney = 0;
        public int cMoney
        {
            get { return _ncMoney; }
            set { _ncMoney = value; }
        }
    }
}
