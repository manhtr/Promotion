using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Promotion.Serial
{
    public partial class rptMaSoDuThuong : DevExpress.XtraReports.UI.XtraReport
    {
        public rptMaSoDuThuong()
        {
            InitializeComponent();
        }

        public void SetReportParameter(string strTenChuongTrinh, string strCamKet)
        {
            xrLabel4.Text = strTenChuongTrinh;
            xrLabel8.Text = strCamKet;
        }

    }
}
