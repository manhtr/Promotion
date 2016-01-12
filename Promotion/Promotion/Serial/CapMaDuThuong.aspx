<%@ Page Title="Cấp mã dự thưởng" Language="C#" MasterPageFile="~/Support.Master" AutoEventWireup="true" CodeBehind="CapMaDuThuong.aspx.cs" Inherits="Promotion.Serial.CapMaDuThuong" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<link href="../css/smoothness/jquery-ui-1.9.1.custom.css" rel="stylesheet" type="text/css" />--%>
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
                    <asp:TextBox ID="txtCIF" runat="server" OnTextChanged="txtCIF_TextChanged"></asp:TextBox>
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
                <td>&nbsp;
                </td>
                <td align="left">
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
                <td>Ngày hiệu lực:
                </td>
                <td>
                    <asp:Label ID="lblvaluedate" runat="server" Text="........"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Ngày đến hạn:
                </td>
                <td>
                    <asp:Label ID="lblmaturitydate" runat="server" Text="........"></asp:Label>
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
                <td>Số mã dự thưởng đã có:
                </td>
                <td>
                    <asp:Label ID="lblMaDuThuongCo" runat="server" Text="........"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 10px"></td>
            </tr>
            <tr>
                <td align="right" style="width: auto;">
                    <asp:Button ID="cmdCapPhieu" runat="server" BackColor="#0E4E95" ForeColor="White" Visible="false" Text="Cấp phiếu" OnClick="cmdCapPhieu_Click" />
                </td>
                <td align="left" style="width: auto;">
                    <asp:Button ID="cmdInphieu" runat="server" BackColor="#0E4E95" ForeColor="White" Visible="false" Text="In phiếu" OnClientClick="return openPrintForm();" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="divPrintReport" style="display: none">
        <iframe id="ifrmPrintReport" src="" width="900px" height="650px" frameborder="0"
            scrolling="no"></iframe>
    </div>
    <asp:HiddenField ID="hdfSoSo" Value="" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtSoSo.ClientID %>").keydown(function (e) {
                if (e.which == '13') {
                    e.preventDefault();
                    $("#<%=cmdKiemTra.ClientID %>").click();
                }
            });
        });

        function checkForm() {
            if ($.trim($("#<%=txtSoSo.ClientID %>").val()) == "") {
                alert("Nhập số sổ tiết kiệm!");
                $("#<%=txtSoSo.ClientID %>").focus();
                return false;
            }
            return true;
        }
        function OpenWindow(strUrl, strName) {
            window.open(strUrl, strName, 'fullscreen=yes|1,location=yes,menubar=no,resizable=yes,scrollbars=yes,status=no,titlebar=no,toolbar=no');
        }
        function openPrintForm() {
            var soso = "";
            soso = $("#<%=hdfSoSo.ClientID %>").val();
            //window.open("InPhieuMaDuThuong.aspx?bn=" + soso + "&timespan=" + Number(new Date()), "_blank", "type=fullWindow,fullscreen,fullscreen=yes|1,location=no,menubar=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no", false);
            $("#ifrmPrintReport").attr("src", "InPhieuMaDuThuong.aspx?bn=" + soso + "&timespan=" + Number(new Date()));
            $("#divPrintReport").css("display", "block");
            $("#divPrintReport").dialog({
                autoOpen: true,
                height: 800,
                width: 1000,
                title: "In phiếu mã dự thưởng",
                modal: true,
                resizable: true,
                close: function (event, ui) {
                    $("#ifrmPrintReport").attr("src", "");
                    $("#divPrintReport").css("display", "none");
                }

            });
            return false;
        }

    </script>
</asp:Content>
