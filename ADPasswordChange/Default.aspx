<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ADPasswordChange.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Your Password</title>
</head>
<body>
    <asp:Panel ID="form" runat="server" Width="500px">
    <form id="form2" runat="server" action="" method="post">
    <div>
        <table border="0">
            <tbody>
                <tr>
                    <td>Username:</td>
                    <td><input id="loginname" name="loginname" type="text" /></td>
                </tr>
                <tr>
                    <td>Old Password:</td>
                    <td><input id="oldpass" name="oldpass" type="password" /></td>
                </tr>
                <tr>
                    <td>New Password:</td>
                    <td><input id="newpass" name="newpass" type="password" /></td>
                </tr>
                <tr>
                    <td>Confirm New Password:</td>
                    <td><input id="confpass" name="confpass" type="password" /></td>
                </tr>
                <tr>
                    <td colspan="2"><input id="submit" type="submit" value="submit" /></td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
    </asp:Panel>
    <asp:Panel ID="status" runat="server" Width="500px">
        <asp:Label ID="msg" runat="server"></asp:Label>
    </asp:Panel>
</body>
</html>
