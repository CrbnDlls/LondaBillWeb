<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LondaBillWeb._Default" %>

<%@ Register assembly="Microsoft.Web.GeneratedImage" namespace="Microsoft.Web" tagprefix="cc1" %>

<%@ Register src="../MessageBoxUserControl.ascx" tagname="MessageBoxUserControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="Default.css" rel="stylesheet" />
    <title></title>
<style type='text/css'>
        .button {
    font: 2em Arial;
            width: 100%;
    height: 100%;
}

        .buttonNumbers {
    font: 3em Arial;
            width: 100%;
    height: 100%;
}

#textBoxMonitor {
    font: 2.3em Arial;
    box-sizing: border-box;
    width: 100%;
    height: 100%;
}
    #labelHint {
        font: 2.1em Arial;
    box-sizing: border-box;
    width: 100%;
    height: 100%;
    }
#textBoxCode {
    font: 3em Arial;
    box-sizing: border-box;
    width: 100%;
    height: 100%;
    }

.monitors {
    font: 1.4em Arial;
    box-sizing: border-box;
    width: 100%;
    height: 100%;
}
    #TextBoxMonitorItems {
    background-color: lime;
    color: #660066;
    }
        </style>
    
</head>
<body>
        <form id="form1" runat="server">
        
            <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                <uc1:MessageBoxUserControl ID="MessageBoxUserControl1" runat="server" />
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <div id="MonitorWrap">
            <div class="Monitor" id="Monitor1">
                <asp:TextBox ID="TextBoxMonitorStaff" runat="server" ReadOnly="True" CssClass="monitors" Wrap="False" TextMode="MultiLine" Font-Names="Arial"></asp:TextBox>
            </div>
            <div class="Monitor" id="Monitor2">
                <asp:TextBox ID="TextBoxMonitorItems" runat="server" ReadOnly="True" CssClass="monitors" TextMode="MultiLine" Font-Names="Arial"></asp:TextBox>
            </div>
            <div class="Monitor" id="Monitor3">
                <asp:TextBox ID="TextBoxWholeSumm" runat="server" ReadOnly="True" CssClass="monitors" Wrap="False" TextMode="MultiLine" Font-Names="Arial"></asp:TextBox>
            </div>
        </div>
        <div id="PreviewWrap">
            <div id="MonitorPreview">
                <asp:TextBox ID="textBoxMonitor" runat="server" ReadOnly="True" Wrap="False" TextMode="MultiLine" 
                    Font-Names="Arial">Дані відсутні</asp:TextBox>
            </div>
            <div id="MonitorHint">
                <asp:Label ID="labelHint" runat="server" Text="Введіть код позиції прайс-листа" 
                    CssClass="textbox" style="text-align:right;" Font-Names="Arial" 
                    ForeColor="Red"></asp:Label>
            </div>
        </div>       
        <div ID="ButtonsWrap">
            <div  class="Buttons" id="Button1">
                <asp:Button ID="buttonBegin" runat="server" CssClass="button" Text="Скинути" onclick="buttonBegin_Click"/>
            </div>
            <div class="Buttons" id="Button2">
                <asp:Button ID="buttonDelString" runat="server" CssClass="button" Text="Видалити строку" 
                    onclick="buttonDelLast_Click"/>
            </div>
            <div class="Buttons" id="Input3">
                <asp:TextBox ID="textBoxCode" runat="server" ReadOnly="True" CssClass="textbox" Font-Names="Arial" ></asp:TextBox>
            </div>
            <div class="Buttons" id="Button4">
                <asp:Button ID="buttonPrint" runat="server" CssClass="button" Text="Обрати майстра" onclick="buttonPrint_Click"/>
            </div>
            <div class="Buttons" id="Button5">
                <asp:Button ID="buttonDelSign" runat="server" Text="Видалити знак" onclick="buttonDel_Click" CssClass="button" />
            </div>
            <div class="Buttons" id="Button6">
                <asp:Button ID="button0" runat="server" Text="0" onclick="button0_Click" CssClass="buttonNumbers" />
            </div>
            <div class="Buttons" id="Button7">
                <asp:Button ID="buttonSave" runat="server" Text="Зберегти" 
                     onclick="buttonSave_Click" CssClass="button" />
            </div>
            <div class="Buttons" id="Button8">
                <asp:Button ID="buttonBO" runat="server" Text="БО" 
                     onclick="buttonBO_Click" CssClass="button" />
            </div>
            <div class="Buttons" id="Button9">
                <asp:Button ID="button1" runat="server" Text="1" 
                     onclick="button1_Click" CssClass="buttonNumbers" />
            </div>
            <div class="Buttons" id="Button10">
                <asp:Button ID="button2" runat="server" Text="2" 
                     onclick="button2_Click" CssClass="buttonNumbers" />
            </div>
            <div class="Buttons" id="Button11">
                <asp:Button ID="button3" runat="server" Text="3" 
                     onclick="button3_Click" CssClass="buttonNumbers" />
            </div>
            <div class="Buttons" id="Button12">
                <asp:Button ID="button50procent" runat="server" Text="50%" 
                     onclick="button50procent_Click" CssClass="buttonNumbers" /></div>
            <div class="Buttons" id="Button13">
                <asp:Button ID="button4" runat="server" Text="4" 
                     onclick="button4_Click" CssClass="buttonNumbers" /></div>
            <div class="Buttons" id="Button14">
                <asp:Button ID="button5" runat="server" Text="5" 
                     onclick="button5_Click" CssClass="buttonNumbers" /></div>
            <div class="Buttons" id="Button15">
                <asp:Button ID="button6" runat="server" Text="6" 
                     onclick="button6_Click" CssClass="buttonNumbers" /></div>
            <div class="Buttons" id="Button16">
                <asp:Button ID="button10Procent" runat="server" Text="Постійний клієнт" 
                     onclick="button10Procent_Click" CssClass="button" /></div>
            <div class="Buttons" id="Button17">
                <asp:Button ID="button7" runat="server" Text="7" 
                     onclick="button7_Click" CssClass="buttonNumbers" /></div>
            <div class="Buttons" id="Button18">
                <asp:Button ID="button8" runat="server" Text="8" 
                     onclick="button8_Click" CssClass="buttonNumbers" /></div>
            <div class="Buttons" id="Button19">
                <asp:Button ID="button9" runat="server" Text="9" 
                     onclick="button9_Click" CssClass="buttonNumbers" /></div>
            <div class="Buttons" id="Button20">
                <asp:Button ID="buttonStaff" runat="server" Text="Співробітник" 
                     onclick="buttonStaff_Click" CssClass="button" /></div>
         </div>
                <asp:Label ID="LabelSalon" runat="server" Text="Label"></asp:Label>
                                                              </ContentTemplate></asp:UpdatePanel>
   
        
    
    <asp:LoginStatus ID="LoginStatus1" runat="server" />         
    </form>
</body>
</html>
