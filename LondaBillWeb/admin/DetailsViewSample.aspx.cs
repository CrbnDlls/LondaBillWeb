using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class DetailsViewSample : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        if (e.Exception != null)
        {
            LabelInsertMessage.Text = e.Exception.InnerException.Message + " Insert Failed";
            LabelInsertMessage.ForeColor = System.Drawing.Color.Red;
            e.ExceptionHandled = true;
        }
        else
        {
            LabelInsertMessage.Text = "";
        }
       
    }

    protected void DetailsView1_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {
        LabelInsertMessage.Text = "";
    }
}
