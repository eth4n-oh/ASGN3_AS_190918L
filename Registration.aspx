<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="ASGN3_AS_190918L.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
    <script>
        function validate() {
            var str = document.getElementById("tb_password").value;
            console.log("This shit ran");
            if (str.length < 12) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password Length must be at least 12 Characters";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("too_short")
            }

            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 number";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_number");
            }
            else if (str.search(/[#?!@$%^&*-]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 special character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_special");
            }

            document.getElementById("lbl_pwdchecker").innerHTML = "Excellent!!";
            document.getElementById("lbl_pwdchecker").style.color = "Blue";

        }

        function checkLuhn() {
            let cardNo = document.getElementById("tb_creditcard").value;
            let nDigits = cardNo.length;

            let nSum = 0;
            let isSecond = false;
            for (let i = nDigits - 1; i >= 0; i--) {

                let d = cardNo[i].charCodeAt() - '0'.charCodeAt();

                if (isSecond == true)
                    d = d * 2;

                // We add two digits to handle
                // cases that make two digits
                // after doubling
                nSum += parseInt(d / 10, 10);
                nSum += d % 10;

                isSecond = !isSecond;
            }
            //return (nSum % 10 == 0);


            if (nSum % 10 == 0 && cardNo.length == 16) {
                document.getElementById("lbl_creditchecker").innerHTML = "Valid";
                document.getElementById("lbl_creditchecker").style.color = "Blue";
            }
            else {
                document.getElementById("lbl_creditchecker").innerHTML = "Invalid";
                document.getElementById("lbl_creditchecker").style.color = "Red";
            }

            return (nSum % 10 == 0);

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <h2>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Registration"></asp:Label>
        <br />
        <br />
   </h2>
        <table class="style1">
            <tr>
                <td class="style3">
        <asp:Label style="font-size:20px" ID="label_firstname" runat="server" Text="First Name"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="tb_firstname" runat="server" Height="16px" Width="280px" required="required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
        <asp:Label style="font-size:20px" ID="label_lastname" runat="server" Text="Last Name"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="tb_lastname" runat="server" Height="16px" Width="280px" required="required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
        <asp:Label style="font-size:20px" ID="label_creditcard" runat="server" Text="Credit Card"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="tb_creditcard" runat="server" Height="16px" Width="280px" type="number" onkeyup="javascript:checkLuhn()" required="required"></asp:TextBox>
                    <asp:Label style="font-size:20px" ID="lbl_creditchecker" runat="server" Text="creditchecker"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
        <asp:Label style="font-size:20px" ID="label_email" runat="server" Text="Email"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="tb_email" runat="server" Height="16px" Width="280px" type="email" required="required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
        <asp:Label style="font-size:20px" ID="label_bday" runat="server" Text="Date of Birth"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="tb_bday" runat="server" Height="16px" Width="280px" type="date" required="required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
        <asp:Label style="font-size:20px" ID="label_password" runat="server" Text="Password"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="tb_password" runat="server" Height="16px" Width="281px" onkeyup="javascript:validate()" required="required"></asp:TextBox>
                    <asp:Label style="font-size:20px" ID="lbl_pwdchecker" runat="server" Text="pwdchecker"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
        <asp:Label style="font-size:20px" ID="label_image" runat="server" Text="Image"></asp:Label>
                </td>
                <td class="style2">
                    <asp:FileUpload ID="fu_Image" runat="server" accept=".png,.jpg,.jpeg,.gif"/>
                </td>
            </tr>
                        <tr>
                <td class="style3">
       
                </td>
                <td class="style2">
    <asp:Button ID="btn_Submit" runat="server" Height="48px" Text="Submit" Width="288px" OnClick="btn_Submit_Click" />
                </td>
            </tr>
    </table>
    </form>
</body>
</html>
