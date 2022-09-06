using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LondaBillWeb
{
    public class wwMessageDisplay : System.Web.UI.Page
    {

        public static string Pagename = "MessageDisplay.aspx";

        public static void DisplayMessage(string TemplatePageName,
                        string Header, string Message,
                        string RedirectUrl, int Timeout)
        {
            HttpContext Context = HttpContext.Current;
            Context.Items.Add("ErrorMessage_Header", Header);
            Context.Items.Add("ErrorMessage_Message", Message);
            Context.Items.Add("ErrorMessage_Timeout", Timeout);
            Context.Items.Add("ErrorMessage_RedirectUrl", RedirectUrl);

            Context.Server.Transfer(Context.Request.ApplicationPath + "/staffonly/" +
                                       TemplatePageName);
        }

        public static void DisplayMessage(string Header, string Message,
                                          string RedirectUrl, int Timeout)
        {
            DisplayMessage(Pagename, Header, Message, RedirectUrl, Timeout);
        }
        public static void DisplayMessage(string Header, string Message)
        {
            DisplayMessage(Header, Message, null, 0);
        }

       
    }
}
