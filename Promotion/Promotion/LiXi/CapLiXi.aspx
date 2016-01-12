<%@ Page Title="Cấp Lì xì" Language="C#" MasterPageFile="~/Support.Master" AutoEventWireup="true" CodeBehind="CapLiXi.aspx.cs" Inherits="Promotion.LiXi.CapLiXi" %>

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
                <td style="width: 30%">Chi nhánh/PGD:
                </td>
                <td>
                    <asp:DropDownList ID="cboBranch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboBranch_SelectedIndexChanged"></asp:DropDownList>
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
            <tr>
                <td colspan="2"><h3>Thông tin Chi nhánh/PGD</h3>
                </td>
            </tr>
            <tr>
                <td>Mã Chi nhánh/PGD:
                </td>
                <td>
                    <asp:Label ID="lblCoCode" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Tên Chi nhánh/PGD:
                </td>
                <td>
                    <asp:Label ID="lblBranchName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Địa chỉ Chi nhánh/PGD:
                </td>
                <td>
                    <asp:Label ID="lblBranchAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Số lượng Lì xì đã cấp:
                </td>
                <td>
                    <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
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
                <td colspan="2">
                    <asp:Button ID="cmdAgree" runat="server" Visible="false" BackColor="#0E4E95" ForeColor="White" Text="Cấp Lì xì" OnClick="cmdAgree_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
