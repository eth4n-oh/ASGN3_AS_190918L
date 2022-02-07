<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="ASGN3_AS_190918L.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Change Password for: "></asp:Label>
        <asp:Label ID="label_change_user" runat="server" Text=""></asp:Label>
        <br />
        <br />
   </h2>
        <table class="style1">
            <tr>
                <td class="style3">
        <asp:Label style="font-size:20px" ID="label_change_newpwd" runat="server" Text="New Password"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="tb_change_newpwd" runat="server" Height="16px" Width="280px" required="required"></asp:TextBox>
                </td>
            </tr>
            <td class="style3">
        <asp:Label style="font-size:20px" ID="label_change_cfmpwd" runat="server" Text="Re-Enter New Password"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="tb_change_cfmpwd" runat="server" Height="16px" Width="280px" required="required"></asp:TextBox>
                </td>
                    <tr>
                <td class="style3">
       
                </td>
                <td class="style2">
    <asp:Button ID="btn_Submit" runat="server" Height="48px" Text="Change Password" Width="288px" OnClick="btn_Submit_Click" />
                </td>
    </form>

    <br />
        </table>
    </form>
    <asp:Label style="font-size:20px" ID="lbl_pwdError" runat="server" Text=""></asp:Label>
</body>
</html>
