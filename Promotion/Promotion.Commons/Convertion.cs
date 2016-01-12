using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promotion.Commons
{
    public class Convertion
    {
        #region ConvertAmountToWord
        private string[] strSo = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        /* đoạn này lấy từ chương trình của bạn đã đưa lên 
        private string[] strDonViNho = { "linh", "lăm", "mười", "mươi", "mốt", "trăm" };
        private string[] strDonViLon = { "", "ngàn", "triệu", "tỷ" };*/
        int a, b, c;
        public string ConvertAmountToString(string strInput)
        {
            string strResult = string.Empty;
            string[] strList = splitArray(strInput);
            strResult = converNumToString(strList);
            return strResult;
        }
        //Tạo một mảng chuỗi, mỗi chuỗi có độ dài 3 ký tự
        private string[] splitArray(string input)
        {
            int i = 0;
            string[] strArray;
            int length = input.Length;
            if (length % 3 == 0)//Nếu chỗi chia hết cho 3 thì lấy độ dài bằng phần nguyên
                strArray = new string[length / 3];
            else//Nếu chỗi không chia hết cho 3 thì lấy độ dài bằng phần nguyên+1
                strArray = new string[length / 3 + 1];
            if (length < 3)
                strArray[0] = input;
            else
            {
                while (length >= 3)
                {
                    strArray[i] = input.Remove(0, length - 3);
                    input = input.Remove(length - 3, 3);
                    i++;
                    length = length - 3;
                }
                if (length > 0)
                    strArray[i] = input;
            }
            return strArray;
        }
        private string converNumToString(string[] list)
        {

            int i;
            string results = "";
            int length = list.Length;
            if (length <= 4)
            {
                if (length == 1)
                    results = readThousand(list[0]);
                if (length == 2)
                    results = readThousand(list[1]) + " nghìn " + readThousand(list[0]);
                if (length == 3)
                    results = readThousand(list[2]) + " triệu " + readThousand(list[1]) + " nghìn " + readThousand(list[0]);
                if (length == 4)
                    results = readThousand(list[3]) + " tỷ " + readThousand(list[2]) + " triệu " + readThousand(list[1]) + " nghìn " + readThousand(list[0]);
            }
            if (length > 4)
            {
                string[] strArray1 = new string[3];
                string[] strArray2 = new string[length - 3];
                for (i = 0; i < 3; i++)
                {
                    strArray1[i] = list[i];
                }
                for (i = 0; i < length - 3; i++)
                {
                    strArray2[i] = list[3 + i];
                }
                //Gọi đệ quy
                results = converNumToString(strArray2) + " tỷ " + converNumToString(strArray1);
            }
            return results;
        }
        //hàm đọc một chuỗi có 3 chữ số ra chữ
        private string readThousand(string input)
        {
            string output = "";
            input = input.Trim();
            string numStr = input;
            int length = numStr.Length;
            if (length == 1)
                output = strSo[Convert.ToInt32(input)];
            if (length == 2)
            {
                a = Convert.ToInt32(Convert.ToString(numStr[0]));
                b = Convert.ToInt32(Convert.ToString(numStr[1]));
                if (a != 1)
                    if (b != 5)
                    {
                        string str = readThousand(Convert.ToString(numStr[1]));
                        if (str == strSo[0])
                            output = strSo[a] + " mươi ";
                        else
                            output = strSo[a] + " mươi " + readThousand(Convert.ToString(numStr[1]));
                    }
                    else
                        output = strSo[a] + " mươi lăm";
                if (a == 1)
                    if (b != 5)
                        output = "mười " + readThousand(Convert.ToString(numStr[1]));
                    else
                        output = "mười lăm";
            }
            if (length == 3)
            {
                a = Convert.ToInt32(Convert.ToString(numStr[0]));
                b = Convert.ToInt32(Convert.ToString(numStr[1]));
                c = Convert.ToInt32(Convert.ToString(numStr[2]));
                if (b == 0)
                    output = strSo[a] + " trăm linh " + readThousand(Convert.ToString(numStr[2]));
                else
                    output = strSo[a] + " trăm " + readThousand(Convert.ToString(numStr[1]) + Convert.ToString(numStr[2]));
            }
            return output;
        }

        public String ConvertNumberToVietnameseSpell(String strNumber, String strCurrency, ref bool result, ref String response)
        {
            SpellNumber spell = new SpellNumber();
            String strResult = "";
            String[] strArray;
            if ((strNumber.Contains(',')) && (strNumber.Contains('.')))
            {
                int l1 = strNumber.LastIndexOf(',');
                int l2 = strNumber.LastIndexOf('.');
                if (l1 > l2)
                {
                    //Dấu thập phân là ,
                    strNumber = strNumber.Replace(".", "");
                    strArray = strNumber.Split(new char[] { ',' });
                    String s2 = strArray[1];
                    if ((s2.Length > 0) && Convert.ToInt32(s2) > 0)
                        strResult = String.Format("{0} phẩy {1} {2}", spell.Spell(strArray[0], "").Trim(), spell.Spell(strArray[1], "").Trim(), strCurrency);
                    else
                        strResult = String.Format("{0} {1}", spell.Spell(strArray[0], "").Trim(), strCurrency);
                }
                else
                {
                    //Dấu thập phân là .
                    strNumber = strNumber.Replace(",", "");
                    strArray = strNumber.Split(new char[] { '.' });
                    String s2 = strArray[1];
                    if ((s2.Length > 0) && Convert.ToInt32(s2) > 0)
                        strResult = String.Format("{0} phẩy {1} {2}", spell.Spell(strArray[0], "").Trim(), spell.Spell(strArray[1], "").Trim(), strCurrency);
                    else
                        strResult = String.Format("{0} {1}", spell.Spell(strArray[0], "").Trim(), strCurrency);
                }
            }
            else
            {
                if ((strNumber.Contains(',')) || (strNumber.Contains('.')))
                {
                    strArray = strNumber.Split(new char[] { '.', ',' });
                    String s2 = strArray[1];
                    if ((s2.Length > 0) && Convert.ToInt32(s2) > 0)
                        strResult = String.Format("{0} phẩy {1} {2}", spell.Spell(strArray[0], "").Trim(), spell.Spell(strArray[1], "").Trim(), strCurrency);
                    else
                        strResult = String.Format("{0} {1}", spell.Spell(strArray[0], "").Trim(), strCurrency);
                }
                else
                {
                    strResult = spell.Spell(strNumber, strCurrency).Trim();
                }
            }

            if (strResult == "")
                strResult = "Không";
            char ch;
            if (strResult.Length > 1)
            {
                ch = Char.ToUpper(strResult[0]);
                strResult = String.Format("{0}{1}", ch, strResult.Substring(1));
            }
            if (!strResult.Contains(strCurrency))
                strResult = String.Format("{0} {1}", strResult, strCurrency);
            return strResult;

        }
        #endregion ConvertAmountToWord
        #region ConvertStringToDateTime
        public DateTime ConvertToMMddyyyy(string strDateTime)
        {
            if (strDateTime.Length != 8)
                throw new Exception("Invalid Format datetime yyyyMMdd");
            string str = strDateTime.Substring(4, 2) + "/" + strDateTime.Substring(6, 2) + "/" + strDateTime.Substring(0, 4);
            return DateTime.Parse(str);
        }
        #endregion ConvertStringToDateTime
    }
}
