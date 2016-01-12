<%@ Page Title="Xóa mã dự thưởng" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="XoaMaDuThuong.aspx.cs" Inherits="Promotion.Serial.XoaMaDuThuong" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
    <script type="text/javascript">
        function checkForm() {
            if ($.trim($("#<%=txtSoSo.ClientID %>").val()) == "") {
                alert("Nhập số sổ tiết kiệm!");
                $("#<%=txtSoSo.ClientID %>").focus();
                return false;
            }
            return true;
        }

        function confirmDelete() {
            if (confirm("Có chắc chắn xóa tất cả mã dự thưởng của sổ này không?"))
                return true;
            else
                return false;
        }
    </script>
    <h3>Xóa mã dự thưởng</h3>
    <div class="quoteOfDay">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">Mã khách hàng:
                </td>
                <td>
                    <asp:TextBox ID="txtCIF" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Sổ tiết kiệm:
                </td>
                <td>
                    <asp:TextBox ID="txtSoSo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Phân hệ:
                </td>
                <td>
                    <asp:RadioButtonList ID="lstradPhanHe" runat="server" RepeatDirection="Horizontal"
                        RepeatColumns="2">
                        <asp:ListItem Text="AZ" Value="AZ" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="LD" Value="LD"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="cmdKiemTra" runat="server" BackColor="#0E4E95" ForeColor="White" Text="Kiểm tra" OnClick="cmdKiemTra_Click"
                        OnClientClick="return checkForm();" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 10px"></td>
            </tr>
            <tr>
                <td>Mã khách hàng:
                </td>
                <td>
                    <asp:Label ID="lblMaKhachHang" runat="server" Text="........"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Tên khách hàng:
                </td>
                <td>
                    <asp:Label ID="lblTenKhachHang" runat="server" Text="........"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Số tiền gửi:
                </td>
                <td>
                    <asp:Label ID="lblSoTienGui" runat="server" Text="........"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Kỳ hạn gửi:
                </td>
                <td>
                    <asp:Label ID="lblKyHanGui" runat="server" Text="........"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Số mã dự thưởng đã cấp:
                </td>
                <td>
                    <asp:Label ID="lblMaDuThuongCo" runat="server" Text="........"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 20px"></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="cmdXoaPhieu" runat="server" BackColor="#0E4E95" ForeColor="White" Visible="false" Text="Xóa phiếu" OnClientClick="return confirmDelete();"
                        OnClick="cmdXoaPhieu_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
