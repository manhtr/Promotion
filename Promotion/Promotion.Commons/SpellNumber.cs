using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Promotion.Commons
{
    public class SpellNumber
    {
        StringDictionary dicNumberSpell = new StringDictionary();
        void Init()
        {
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
        public SpellNumber()
        {
            Init();
        }
        public String Spell(String strNumber, String strDonViTruoc)
        {
            String strDonVi = "";
            String strNumberLeft = "";
            String strNumberRight = "";
            String strResult;
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
                String spellLeft = Spell(strNumberLeft, strDonVi);
                String spellRight = Spell(strNumberRight, strDonViTruoc);
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
                    //strResult = String.Format("lẻ {0} {1}", dicNumberSpell[strNumber[2].ToString()], strDonViTruoc);
                    strResult = String.Format("không trăm linh {0} {1}", dicNumberSpell[strNumber[2].ToString()], strDonViTruoc);
                    return strResult;
                }
                String strBackup = strNumber;
                strResult = "";
                if ((strNumber.Length == 3))
                {
                    strResult = String.Format("{0} trăm", dicNumberSpell[strNumber[0].ToString()]);
                    strNumber = strNumber.Remove(0, 1);
                }
                if (strNumber != "00")
                {
                    if (strNumber[0] == '0')
                        //strResult = String.Format("{0} lẻ {1} {2}", strResult, dicNumberSpell[strNumber[1].ToString()], strDonViTruoc);
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
