<%@ Page Title="Xóa cấp Lì xì" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="XoaLiXi.aspx.cs" Inherits="Promotion.LiXi.XoaLiXi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/smoothness/jquery-ui-1.9.1.custom.min.css" rel="stylesheet" />
    <style type="text/css">
        .errorMessage {
            color: Red;
            font-weight: bold;
        }

        .successMessage {
            color: Green;
            font-weight: bold;
        }
    </style>

    <h3><%:Page.Title %></h3>
    <div class="quoteOfDay" style="overflow: auto;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">Mã khách hàng:
                </td>
                <td>
                    <asp:TextBox ID="txtCIF" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="cmdSearch" runat="server" Text="Kiểm tra" BackColor="#0E4E95" ForeColor="White" OnClick="cmdSearch_Click" />
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="2">
                    <h3>Thông tin khách hàng</h3>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">Mã khách hàng:
                </td>
                <td>
                    <asp:Label ID="lblCIF" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Tên khách hàng:
                </td>
                <td>
                    <asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>CMND:
                </td>
                <td>
                    <asp:Label ID="lblCMND" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Số điện thoại:
                </td>
                <td>
                    <asp:Label ID="lblTelephone" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Địa chỉ:
                </td>
                <td>
                    <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Số tài khoản:
                </td>
                <td>
                    <asp:Label ID="lblAccountNumber" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Trạng thái cấp Lì xì:
                </td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="cmdDelete" runat="server" Visible="false" OnClientClick="return confirm('Bạn có chắc chắn xóa việc cấp mã Lì xì cho khách hàng này không?');" BackColor="#0E4E95" ForeColor="White" Text="Xóa cấp Lì xì" OnClick="cmdDelete_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdfCIF" Value="" runat="server" />
</asp:Content>
