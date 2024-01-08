<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" action="login.aspx" method="post">
        <div>
            <input type="text" id="userName" name="userName" placeholder="User name" />
            <input type="password" id="password" name="password" placeholder="Password" />
            <input type="submit" value="Login" />
        </div>
    </form>
</body>
</html>
