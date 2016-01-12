using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Promotion.Commons
{
    public class Common
    {
        public static string GetRootRequest()
        {
            string r = "";
            HttpRequest cr = HttpContext.Current.Request;
            if (cr.Url.ToString().IndexOf("local") >= 0)
            {
                if (cr.Url.Port != 80)
                {
                    r = cr.Url.Scheme + "://" + cr.Url.Host + ":" + cr.Url.Port.ToString();
                }
                else { r = cr.Url.Scheme + "://" + cr.Url.Host; }
                r = r + HttpContext.Current.Request.ApplicationPath;
            }
            else
            {
                if (cr.Url.Port != 80)
                {
                    r = cr.Url.Scheme + "://" + cr.Url.Host + ":" + cr.Url.Port.ToString();
                }
                else { r = cr.Url.Scheme + "://" + cr.Url.Host; }
                r = r + HttpContext.Current.Request.ApplicationPath;
            }
            return r.Trim('/') + "/";
        }

        public static string FormatSerial(List<string> arrString)
        {
            StringBuilder strResult = new StringBuilder();
            if (arrString != null && arrString.Count > 0 && !string.IsNullOrEmpty(arrString[0]))
            {
                int i1 = Convert.ToInt32(arrString[0]), i2 = i1, i3;
                for (int i = 1; i < arrString.Count; i++)
                {
                    if (!string.IsNullOrEmpty(arrString[i]))
                    {
                        i3 = Convert.ToInt32(arrString[i]);
                        if (i3 != i2 + 1)
                        {
                            if (i2 != i1)
                                strResult.AppendFormat("Từ {0:000000} đến {0}{1:000000}, ", i1, i2);
                            else
                                strResult.AppendFormat("{0:000000}, ", i1);
                            i1 = i3;
                            i2 = i1;
                        }
                        else
                            i2 = i3;
                    }
                }
                if (i2 != i1)
                    strResult.AppendFormat("Từ {0:000000} đến {1:000000}", i1, i2);
                else
                    strResult.AppendFormat("{0:000000}", i1);
            }

            return strResult.ToString();
        }
        public static string ReFormatSerial(string[] arrString)
        {
            StringBuilder strResult = new StringBuilder();
            if (arrString != null && arrString.Length > 0 && !string.IsNullOrEmpty(arrString[0]))
            {
                int i1 = Convert.ToInt32(arrString[0]), i2 = i1, i3;
                for (int i = 1; i < arrString.Length; i++)
                {
                    if (!string.IsNullOrEmpty(arrString[i]))
                    {
                        i3 = Convert.ToInt32(arrString[i]);
                        if (i3 != i2 + 1)
                        {
                            if (i2 != i1)
                                strResult.AppendFormat("Từ {0:000000} đến {0}{1:000000}, ", i1, i2);
                            else
                                strResult.AppendFormat("{0:000000}, ", i1);
                            i1 = i3;
                            i2 = i1;
                        }
                        else
                            i2 = i3;
                    }
                }
                if (i2 != i1)
                    strResult.AppendFormat("Từ {0:000000} đến {1:000000}", i1, i2);
                else
                    strResult.AppendFormat("{0:000000}", i1);
            }

            return strResult.ToString();
        }
    }
}
