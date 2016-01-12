<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InPhieuMaDuThuong.aspx.cs" Inherits="Promotion.Serial.InPhieuMaDuThuong" %>

<%@ Register Assembly="DevExpress.XtraReports.v14.2.Web, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="x-ua-compatible" content="IE=9" />
    <title>In phiếu mã dự thưởng</title>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="width: 5%"></td>
                <td>
                    <dx:ReportToolbar ID="ReportToolbar1" runat='server' ShowDefaultButtons='False'
                        ReportViewer="<%# ReportViewer1 %>" Width="100%">
                        <Items>
                            <dx:ReportToolbarButton ItemKind='Search' />
                            <dx:ReportToolbarSeparator />
                            <dx:ReportToolbarButton ItemKind='PrintReport' />
                            <dx:ReportToolbarSeparator />
                            <dx:ReportToolbarButton Enabled='False' ItemKind='FirstPage' />
                            <dx:ReportToolbarButton Enabled='False' ItemKind='PreviousPage' />
                            <dx:ReportToolbarLabel ItemKind='PageLabel' />
                            <dx:ReportToolbarComboBox ItemKind='PageNumber' Width='65px'>
                            </dx:ReportToolbarComboBox>
                            <dx:ReportToolbarLabel ItemKind='OfLabel' />
                            <dx:ReportToolbarTextBox IsReadOnly='True' ItemKind='PageCount' />
                            <dx:ReportToolbarButton ItemKind='NextPage' />
                            <dx:ReportToolbarButton ItemKind='LastPage' />
                            <dx:ReportToolbarSeparator />
                            <dx:ReportToolbarButton ItemKind='SaveToDisk' />
                            <dx:ReportToolbarComboBox ItemKind='SaveFormat' Width='70px'>
                                <Elements>
                                    <dx:ListElement Value='pdf' />
                                    <dx:ListElement Value='xls' />
                                    <dx:ListElement Value='xlsx' />
                                    <dx:ListElement Value='rtf' />
                                    <dx:ListElement Value='mht' />
                                    <dx:ListElement Value='txt' />
                                    <dx:ListElement Value='csv' />
                                    <dx:ListElement Value='png' />
                                </Elements>
                            </dx:ReportToolbarComboBox>
                        </Items>
                        <Styles>
                            <LabelStyle>
                                <Margins MarginLeft='3px' MarginRight='3px' />
                            </LabelStyle>
                        </Styles>
                    </dx:ReportToolbar>
                </td>
                <td style="width: 5%"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <dx:ReportViewer ID="ReportViewer1" runat="server" Width="100%"
                        Height="600px">
                    </dx:ReportViewer>
                </td>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
