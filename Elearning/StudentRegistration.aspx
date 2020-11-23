<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentRegistration.aspx.cs" Inherits="Elearning.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblRegister" runat="server" 
            style="z-index: 1; left: 98px; top: 40px; position: absolute" 
            Text="Student Registration" Font-Bold="True" Font-Size="Larger" 
            Font-Underline="True"></asp:Label>
    
    <asp:Label ID="lblUsername" runat="server" 
        style="z-index: 1; left: 102px; top: 77px; position: absolute; height: 19px" 
        Text="Username:"></asp:Label>
    <asp:Label ID="lblPassword" runat="server" 
        style="z-index: 1; left: 103px; top: 111px; position: absolute" 
        Text="Password:"></asp:Label>
    <asp:Label ID="lblConfirmPassword" runat="server" 
        style="z-index: 1; left: 50px; top: 148px; position: absolute" 
        Text="Confirm Password:"></asp:Label>
    <asp:Label ID="lblCourse" runat="server" 
        style="z-index: 1; left: 119px; top: 266px; position: absolute" 
        Text="Course:"></asp:Label>
    <asp:TextBox ID="txtUsername" runat="server" 
        style="z-index: 1; left: 184px; top: 76px; position: absolute"></asp:TextBox>
    <asp:TextBox ID="txtPassword" runat="server" 
        style="z-index: 1; left: 184px; top: 111px; position: absolute" 
        TextMode="Password"></asp:TextBox>
    <asp:TextBox ID="txtConfirmPassword" runat="server" 
        style="z-index: 1; left: 184px; top: 149px; position: absolute" 
        TextMode="Password"></asp:TextBox>
    <asp:DropDownList ID="ddlCourses" runat="server" 
        style="z-index: 1; left: 184px; top: 267px; position: absolute;">
    </asp:DropDownList>
    <asp:Button ID="btnRegister" runat="server" 
        style="z-index: 1; left: 184px; top: 353px; position: absolute" 
        Text="Register" onclick="btnRegister_Click" />
    <asp:Label ID="lblError" runat="server" ForeColor="Red" 
        style="z-index: 1; left: 184px; top: 398px; position: absolute"></asp:Label>
     </div>
    <asp:Label ID="lblRealName" runat="server" 
        style="z-index: 1; left: 100px; top: 186px; position: absolute" 
        Text="Full Name:"></asp:Label>
    <asp:TextBox ID="txtRealName" runat="server" 
        style="z-index: 1; left: 184px; top: 184px; position: absolute"></asp:TextBox>
    <asp:TextBox ID="txtEmailAddress" runat="server" 
        style="z-index: 1; left: 184px; top: 225px; position: absolute"></asp:TextBox>
    <asp:Label ID="lblEmailAddress" runat="server" 
        style="z-index: 1; left: 60px; top: 226px; position: absolute" 
        Text="Email Address:"></asp:Label>        
    <asp:Label ID="LblRole" runat="server" 
        style="z-index: 1; left: 131px; top: 313px; position: absolute" 
        Text="Role:"></asp:Label>
    <asp:DropDownList ID="ddlRoles" runat="server" 
        style="z-index: 1; left: 188px; top: 312px; position: absolute;">
        <asp:ListItem Text="Student" Value="1"></asp:ListItem>   
        <asp:ListItem Text="Tutor" Value="2"></asp:ListItem>
    </asp:DropDownList>

    </form>
</body>
</html>
