/*
Copyright ? 2005, Peter Kellner
All rights reserved.
http://peterkellner.net

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:

- Redistributions of source code must retain the above copyright
notice, this list of conditions and the following disclaimer.

- Neither Peter Kellner, nor the names of its
contributors may be used to endorse or promote products
derived from this software without specific prior written 
permission. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE 
COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES INCLUDING,
BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER 
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
POSSIBILITY OF SUCH DAMAGE.
*/

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

public partial class _Default : System.Web.UI.Page
{
    private System.Diagnostics.Process process1;

	protected void Page_Load(object sender, EventArgs e)
	{

		// Grab first username and load roles below
		if (!IsPostBack)
		{
			FindFirstUserName();
		}
	}

	/// <summary>
	/// Used to retrieve the first user that would normally be processed
	/// by the Membership List
	/// </summary>
	private void FindFirstUserName()
	{
		MembershipUserCollection muc = Membership.GetAllUsers();
		foreach (MembershipUser mu in muc)
		{
			// Just grab the first name then break out of loop
			string userName = mu.UserName;
			ObjectDataSourceRoleObject.SelectParameters["UserName"].DefaultValue = userName;
			break;
		}
	}


	protected void GridViewMembershipUser_SelectedIndexChanged(object sender, EventArgs e)
	{

        LabelInsertMessage.Text = "";

		GridView gv = (GridView)sender;

        // cover case where there is no current user
        if (Membership.GetUser() != null)
        {
            ObjectDataSourceRoleObject.SelectParameters["UserName"].DefaultValue = Membership.GetUser().UserName;
            ObjectDataSourceRoleObject.SelectParameters["ShowOnlyAssignedRolls"].DefaultValue = "true";
        }

        GridViewRole.DataBind();
	}
	protected void ButtonCreateNewRole_Click(object sender, EventArgs e)
	{
		if (TextBoxCreateNewRole.Text.Length > 0)
		{
			ObjectDataSourceRoleObject.InsertParameters["RoleName"].DefaultValue = TextBoxCreateNewRole.Text; ;
			ObjectDataSourceRoleObject.Insert();
			GridViewRole.DataBind();
			TextBoxCreateNewRole.Text = "";
		}
	}

	protected void ToggleInRole_Click(object sender, EventArgs e)
	{
		// Grab text from button and parse, not so elegant, but gets the job done
		Button bt = (Button)sender;
		string buttonText = bt.Text;

		char[] seps = new char[1];
		seps[0] = ' ';
		string[] buttonTextArray = buttonText.Split(seps);
		string roleName = buttonTextArray[4];
		string userName = buttonTextArray[1];
		string whatToDo = buttonTextArray[0];
		string[] userNameArray = new string[1];
		userNameArray[0] = userName;  // Need to do this because RemoveUserFromRole requires string array.

		if (whatToDo.StartsWith("Un"))
		{
			// need to remove assignment of this role to this user
			Roles.RemoveUsersFromRole(userNameArray, roleName);
		}
		else
		{
			Roles.AddUserToRole(userName, roleName);
		}
        GridViewRole.DataBind();
	}

	protected void ButtonNewUser_Click(object sender, EventArgs e)
	{
        //if (TextBoxUserName.Text.Length > 0 && TextBoxPassword.Text.Length > 0)
        //{
            ObjectDataSourceMembershipUser.InsertParameters["UserName"].DefaultValue = TextBoxUserName.Text; ;
            ObjectDataSourceMembershipUser.InsertParameters["password"].DefaultValue = TextBoxPassword.Text;
            ObjectDataSourceMembershipUser.InsertParameters["passwordQuestion"].DefaultValue = TextBoxPasswordQuestion.Text;
            ObjectDataSourceMembershipUser.InsertParameters["passwordAnswer"].DefaultValue = TextBoxPasswordAnswer.Text;
            ObjectDataSourceMembershipUser.InsertParameters["email"].DefaultValue = TextBoxEmail.Text;
            ObjectDataSourceMembershipUser.InsertParameters["isApproved"].DefaultValue = CheckboxApproval.Checked == true ? "true" : "false";
          
            ObjectDataSourceMembershipUser.Insert();
            GridViewMemberUser.DataBind();
            TextBoxUserName.Text = "";
            TextBoxPassword.Text = "";
            TextBoxEmail.Text = "";
            TextBoxPasswordAnswer.Text = "";
            TextBoxPasswordQuestion.Text = "";
            CheckboxApproval.Checked = false;
        //}
	}

	protected void GridViewMembership_RowDeleted(object sender, GridViewDeletedEventArgs e)
	{
		FindFirstUserName();  // Current user is deleted so need to select a new user as current
		GridViewRole.DataBind(); // update roll lists to reflect new counts
	}


	protected string ShowNumberUsersInRole(int numUsersInRole)
	{
		string result;
		result = "Number of Users In Role: " + numUsersInRole.ToString();
		return result;
	}

	protected string ShowInRoleStatus(string userName, string roleName)
	{
		string result;

        if (userName == null | roleName == null)
        {
            return "No UserName Specified";
        }

		if (Roles.IsUserInRole(userName, roleName) == true)
		{
			result = "Unassign " + userName + " From Role " + roleName;
		}
		else
		{
			result = "Assign " + userName + " To Role " + roleName;
		}

		return result;
	}


    protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        GridViewMemberUser.DataBind();
    }
    protected void DetailsView1_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {

    }
    protected void ObjectDataSourceMembershipUser_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            LabelInsertMessage.Text = e.Exception.InnerException.Message + " Insert Failed";
            LabelInsertMessage.ForeColor = System.Drawing.Color.Red;

            e.ExceptionHandled = true;
        }
        else
        {
            LabelInsertMessage.Text = "Member " + TextBoxUserName.Text + " Inserted Successfully.";
            LabelInsertMessage.ForeColor = System.Drawing.Color.Green;
        }
    }

    private void InitializeComponent()
    {
        this.process1 = new System.Diagnostics.Process();
        // 
        // process1
        // 
        this.process1.StartInfo.Domain = "";
        this.process1.StartInfo.LoadUserProfile = false;
        this.process1.StartInfo.Password = null;
        this.process1.StartInfo.StandardErrorEncoding = null;
        this.process1.StartInfo.StandardOutputEncoding = null;
        this.process1.StartInfo.UserName = "";

    }
    
}
