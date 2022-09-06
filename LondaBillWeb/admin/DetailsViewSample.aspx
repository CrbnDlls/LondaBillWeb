<%@ Page Language="C#" AutoEventWireup="true" Inherits="DetailsViewSample" Codebehind="DetailsViewSample.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" AllowPaging="True" AutoGenerateRows="False" DataKeyNames="UserName" DataSourceID="ObjectDataSource1" OnItemInserted="DetailsView1_ItemInserted" OnPageIndexChanging="DetailsView1_PageIndexChanging">
            <Fields>
                <asp:BoundField DataField="ProviderName" HeaderText="ProviderName" ReadOnly="True"
                    SortExpression="ProviderName" />
                <asp:CheckBoxField DataField="IsOnline" HeaderText="IsOnline" ReadOnly="True" SortExpression="IsOnline" />
                <asp:BoundField DataField="LastPasswordChangedDate" HeaderText="LastPasswordChangedDate"
                    ReadOnly="True" SortExpression="LastPasswordChangedDate" />
                <asp:BoundField DataField="PasswordQuestion" HeaderText="PasswordQuestion" ReadOnly="True"
                    SortExpression="PasswordQuestion" />
                <asp:CheckBoxField DataField="IsLockedOut" HeaderText="IsLockedOut" ReadOnly="True"
                    SortExpression="IsLockedOut" />
                <asp:BoundField DataField="Comment" HeaderText="Comment" SortExpression="Comment" />
                <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" SortExpression="UserName" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" ReadOnly="True"
                    SortExpression="CreationDate" />
                <asp:CheckBoxField DataField="IsApproved" HeaderText="IsApproved" SortExpression="IsApproved" />
                <asp:BoundField DataField="LastLockoutDate" HeaderText="LastLockoutDate" ReadOnly="True"
                    SortExpression="LastLockoutDate" />
                <asp:BoundField DataField="LastLoginDate" HeaderText="LastLoginDate" SortExpression="LastLoginDate" />
                <asp:BoundField DataField="LastActivityDate" HeaderText="LastActivityDate" SortExpression="LastActivityDate" />
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            </Fields>
        </asp:DetailsView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"
            InsertMethod="Insert"  SelectMethod="GetMembers"
            TypeName="MembershipUtilities.MembershipUserODS" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="UserName" Type="Object" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="email" Type="String" />
                <asp:Parameter Name="isApproved" Type="Boolean" />
                <asp:Parameter Name="comment" Type="String" />
                <asp:Parameter Name="lastActivityDate" Type="DateTime" />
                <asp:Parameter Name="lastLoginDate" Type="DateTime" />
            </UpdateParameters>
            <SelectParameters>
                <asp:Parameter Name="sortData" Type="String" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="userName" Type="String" />
                <asp:Parameter Name="isApproved" Type="Boolean" />
                <asp:Parameter Name="comment" Type="String" />
                <asp:Parameter Name="lastLockoutDate" Type="DateTime" />
                <asp:Parameter Name="creationDate" Type="DateTime" />
                <asp:Parameter Name="email" Type="String" />
                <asp:Parameter Name="lastActivityDate" Type="DateTime" />
                <asp:Parameter Name="providerName" Type="String" />
                <asp:Parameter Name="isLockedOut" Type="Boolean" />
                <asp:Parameter Name="lastLoginDate" Type="DateTime" />
                <asp:Parameter Name="isOnline" Type="Boolean" />
                <asp:Parameter Name="passwordQuestion" Type="String" />
                <asp:Parameter Name="lastPasswordChangedDate" Type="DateTime" />
                <asp:Parameter Name="password" Type="String" />
                <asp:Parameter Name="passwordAnswer" Type="String" />
            </InsertParameters>
        </asp:ObjectDataSource>
        <asp:Label ID="LabelInsertMessage" runat="server"></asp:Label></div>
    </form>
</body>
</html>
