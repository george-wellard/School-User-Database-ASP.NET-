<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TutorDetails.aspx.cs" Inherits="Elearning.TutorDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="z-index: 1; left: -3px; top: 1px; position: absolute; height: 123px; width: 1384px">
   <form id="form1" runat="server">
    
    <asp:Button ID="btnUserAccount" runat="server" PostBackUrl="~/UserAccount.aspx" 
        style="z-index: 1; left: 104px; top: 15px; position: absolute" 
        Text="User Account" />
    
    <div> 
        <asp:Label ID="lblTutorDetails" runat="server" Font-Bold="True" 
            Font-Size="Larger" 
            style="z-index: 1; left: 100px; top: 48px; position: absolute" 
            Text="Tutor Details" Font-Underline="True"></asp:Label>
    </div>
    <asp:Label ID="lblCourse" runat="server" 
        style="z-index: 1; left: 102px; top: 90px; position: absolute" Text="Course:"></asp:Label>
    <asp:TextBox ID="txtCourse" runat="server" ReadOnly="True" 
        style="z-index: 1; left: 165px; top: 87px; position: absolute"></asp:TextBox>
        
    <asp:Label ID="lblError" runat="server" ForeColor="Red" 
        style="z-index: 1; left: 321px; top: 89px; position: absolute"></asp:Label>
        
    <asp:Label ID="lblStudents" runat="server" 
        style="z-index: 1; left: 102px; top: 125px; position: absolute" 
        Text="Students(s) on your course:"></asp:Label>

        <asp:ScriptManager ID="ScriptManager1" runat="server">          
    </asp:ScriptManager>
    <asp:ListBox ID="lstStudents" runat="server"   
        style="z-index: 1; left: 102px; top: 159px; position: absolute; height: 72px; width: 180px"
        OnSelectedIndexChanged="lstStudents_SelectedIndexChanged">
    </asp:ListBox>


    <asp:Button ID="btnRemoveStudent" runat="server" 
        style="z-index: 1; left: 102px; top: 250px; position: absolute" 
        Text="Remove Student" onclick="btnRemoveStudent_Click" />
        
    <asp:Label ID="lblSuccess" runat="server" ForeColor="#009900" 
        style="z-index: 1; left: 104px; top: 293px; position: absolute"></asp:Label>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" 	
                ChildrenAsTriggers="False" UpdateMode="Conditional">    
                <ContentTemplate>
                      <asp:Label ID="lblMatch" runat="server" Text=""></asp:Label>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 	
                        DynamicLayout="true"> 
                        <ProgressTemplate>        
                            <p>Loading...</p>     
                            </ProgressTemplate>
                    </asp:UpdateProgress>
                    </ContentTemplate> 
                <Triggers>        
                    <asp:AsyncPostBackTrigger ControlID="lstStudents" 				            
                        EventName="SelectedIndexChanged"/>
                </Triggers>
            </asp:UpdatePanel>
    
    </form>
</body>
</html>
