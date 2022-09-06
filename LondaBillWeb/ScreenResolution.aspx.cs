using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LondaBillWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
            {
                // store the screen resolution in Session["ScreenResolution"]
                // and redirect back to default.aspx
                Session["ScreenResolution"] = Request.QueryString["res"].ToString();
                
                Response.Redirect("Login.aspx");
            }
        }
    }
}