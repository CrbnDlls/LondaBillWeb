<%@ Page language="c#" Codebehind="MessageDisplay.aspx.cs"
  Inherits="LondaBillWeb.MessageDisplay" %>
<HTML>
   <HEAD>
      <title>
        <%= this.Header %>
      </title>
      <%= this.RedirectMetaTag %>
      <link REL="stylesheet" TYPE="text/css"
            HREF='<%= this.BasePath %>/wwWebstore.css'>
      <base href='<%= this.BasePath %>/' >
   </HEAD>
   <body TOPMARGIN="0" LEFTMARGIN="0">
      
      <!-- West Wind Menu -->
      
      <table BORDER="0" CELLSPACING="0" CELLPADDING="0"
             WIDTH="100%" CLASS="body" HEIGHT="100%">
         <tr>
           <td class="categorylistbackground" valign="top"><br>
            </td>
      
            <!-- Custom Form Stuff -->
            <td VALIGN="top" BGCOLOR="#ffffff" CLASS="body" WIDTH="*">
               <form ID="form1" RUNAT="server">
                  <br>
                  <table border="0" width="97%">
                     <tr>
                        <td class="gridheader" align="center">
                           <asp:label ID="lblHeader" RUNAT="server">
                           </asp:label>
                        </td>
                     </tr>
                     <tr>
                        <td>
                           <br>
                           <blockquote>
                           <asp:label ID="lblMessage" RUNAT="server">
                           </asp:label>
                              <br>
                              <p></p>
                           </blockquote>
                           <center><small>
                   <asp:label ID="lblRedirectHyperLink" RUNAT="server">
                   </asp:label>
                          </small></center>
                        </td>
                     </tr>
                  </table>
               </form>
            </td>
         </tr>
         <!-- End Custom Form Stuff -->
      </table>
   </body>
</HTML>
