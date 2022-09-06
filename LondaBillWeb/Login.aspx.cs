using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Threading;

namespace LondaBillWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ScreenResolution"] == null)
            {
                Response.Redirect("ScreenResolution.aspx");
            }
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            int cnt = 0;
            START:
            try
            { 
                conn.Open();
            }
            catch (SqlException sqx)
            {
                
                LogFile.WriteToLogFile(Server.MapPath("~/App_Data/debug.log"), "Код ошибки: " + sqx.ErrorCode);
                LogFile.WriteToLogFile(Server.MapPath("~/App_Data/debug.log"), "Ошибка: " + sqx.Message);
                if (cnt < 5)
                {
                    Thread.Sleep(1000);
                    cnt = cnt + 1;
                    goto START;
                }
            }
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }

            /*SqlCommand cmd = new SqlCommand("", conn);
            
            try
            { 
                cmd.ExecuteReader();
            }
            catch (SqlException sqx)
            {
                
            }*/
        }
    }
}
