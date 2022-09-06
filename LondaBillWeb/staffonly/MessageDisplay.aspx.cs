using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LondaBillWeb
{
    public partial class MessageDisplay : wwMessageDisplay
    {
        // *** Note: In ASP.NET 2.0 you don't need these control defs
        //protected System.Web.UI.WebControls.Label lblHeader;
        //protected System.Web.UI.WebControls.Label lblMessage;
        //protected System.Web.UI.WebControls.Label lblRedirectHyperLink;

        private void Page_Load(object sender, System.EventArgs e)
        {
            this.DisplayPage();
        }

        protected string BasePath = "";
        protected string RedirectMetaTag = null;

        protected string Header
        {
            get { return (string)Context.Items["ErrorMessage_Header"]; }
        }
        protected string Message
        {
            get { return (string)Context.Items["ErrorMessage_Message"]; }
        }
        protected string RedirectUrl
        {
            get { return (string)Context.Items["ErrorMessage_RedirectUrl"]; }
        }

        public void DisplayPage(/*Label Header, Label Message,
                                Label RedirectHyperLink*/)
        {
            lblHeader.Text = this.Header;
            lblMessage.Text = this.Message;

            // *** Get the base path
            this.BasePath = Request.Url.GetLeftPart(UriPartial.Authority) +
                   Request.ApplicationPath;

            if (this.RedirectUrl != null)
            {
                string NewUrl = this.RedirectUrl;

                /// *** Must fix up the path in case of sub-dir
                /// *** because the page is using <base>
                /// *** we must include the full relative path
                if (NewUrl.StartsWith("~") || NewUrl.StartsWith("/"))
                    NewUrl = this.ResolveUrl(NewUrl);
                else if (!NewUrl.ToLower().StartsWith("http:") &&
                                !NewUrl.ToLower().StartsWith("https:"))
                {
                    // *** It's a relative Path.
                    NewUrl = Request.FilePath.Substring(0,
                                   Request.FilePath.LastIndexOf("/") + 1) +
                                   NewUrl;
                }

                this.RedirectMetaTag =
                   string.Format("<META HTTP-EQUIV='Refresh' " +
                                  "CONTENT='{0}; URL={1}'>\r\n",
                          this.Context.Items["ErrorMessage_Timeout"],
                                  NewUrl);

                lblRedirectHyperLink.Text = "<a href='" + NewUrl +
                             "'>Click here</a> if your browser is not " +
                             "automatically continuing.";
            }
        }
    }
}
