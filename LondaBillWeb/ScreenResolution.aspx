<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScreenResolution.aspx.cs" Inherits="LondaBillWeb.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <script type="text/javascript" language="javascript">
        res = "&res=" + screen.width + "x" + screen.height
        top.location.href = "ScreenResolution.aspx?action=set" + res
</script>
</body>
</html>
