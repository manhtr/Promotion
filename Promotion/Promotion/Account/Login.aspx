<%@ Page Title="Đăng nhập hệ thống" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Promotion.Account.Login" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="loginForm">
        <div class="headLoginForm">Đăng nhập hệ thống</div>
        <div class="fieldLogin">
            <label>
                Tên đăng nhập
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            runat="server" ErrorMessage="(*)" ForeColor="Red"
                            ControlToValidate="txtUsername" ValidationGroup="Login"
                            SetFocusOnError="true"></asp:RequiredFieldValidator>
            </label>
            <br />
            <asp:TextBox ID="txtUsername" runat="server" CssClass="login"></asp:TextBox><br />
            <label>
                Mật khẩu
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                            runat="server" ErrorMessage="(*)"
                            ForeColor="Red" SetFocusOnError="true"
                            ControlToValidate="txtPassword" ValidationGroup="Login"></asp:RequiredFieldValidator>
            </label>
            <br />
            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="login"></asp:TextBox><br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="button"
                OnClick="btnLogin_Click" ValidationGroup="Login" /><br />
            <label>
                <asp:Label ID="lblInfor" runat="server" Text="" ForeColor="Red"></asp:Label>
            </label>
        </div>
    </div>
</asp:Content>
