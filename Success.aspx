<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="ASGN3_AS_190918L.Success" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <div>
        <h2>User Profile</h2>
        <h2>Name : <asp:Label ID="lbl_userfirstName" runat="server"></asp:Label> <asp:Label ID="lbl_userlastName" runat="server"></asp:Label>
        </h2>
        <h2>Email : <asp:Label ID="lbl_userEmail" runat="server"></asp:Label>
        </h2>
        <asp:Image ID="image_User" runat="server" />
        <asp:Button ID="btn_logOut" runat="server" Text="Log Out" OnClick="btn_logOut_Click" />
           <asp:Button ID="btn_changePwd" runat="server" Text="Change Password" OnClick="btn_changePwd_Click" />
      </div>
    </form>
</body>
</html>
