<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ASGN3_AS_190918L.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://www.google.com/recaptcha/api.js?render=6LdzQmMeAAAAAL28BlwMGfih2ZYGWDGh7I-ImAGp"></script>
</head>
<body style="height: 322px">
    <form id="form1" runat="server">
        <h2>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Login"></asp:Label>
        <br />
        <br />
   </h2>
        <table class="style1">
            <tr>
                <td class="style3">
        <asp:Label style="font-size:20px" ID="label_login_email" runat="server" Text="Email"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="tb_login_email" runat="server" Height="16px" Width="280px" type="email" required="required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
        <asp:Label style="font-size:20px" ID="label_login_password" runat="server" Text="Password"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="tb_login_password" runat="server" Height="16px" Width="280px" required="required"></asp:TextBox>
                </td>
            </tr>
                    <tr>
                <td class="style3">
       
                </td>
                <td class="style2">
    <asp:Button ID="btn_Submit" runat="server" Height="48px" Text="Login" Width="288px" OnClick="btn_Submit_Click" />
                </td>

        <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
        <asp:Label ID="lbl_gScore" runat="server" EnableViewState="False">Error message here(GCaptcha)</asp:Label>
    </form>

    <br />
        </table>
     <asp:Label ID="lbl_login_msg" runat="server" Text=""></asp:Label>
    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('6LdzQmMeAAAAAL28BlwMGfih2ZYGWDGh7I-ImAGp', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>
</body>
</html>
