using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SESWeb.Transactions
{
    public partial class UserImpersonation : System.Web.UI.Page
    {
        ClsSES objSES = new ClsSES();
        Authorization objAuthorization = new Authorization();
        protected void Page_Load(object sender, EventArgs e)
        {
            grdUserLoginImpersonate.Rebind();
        }
        protected void grdUserLoginImpersonate_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataSet dsUserRoles = SelectUserRolesByApplicationName("SES");
            grdUserLoginImpersonate.DataSource = dsUserRoles.Tables[1];
        }
        public DataSet SelectUserRolesByApplicationName(string _ApplicationID)
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[1];
                _params[0] = new SqlParameter("@ApplicationName", _ApplicationID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectUserRolesByApplicationName", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void grdUserLoginImpersonate_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "SetUser")
            {
                GridDataItem item = (GridDataItem)e.Item;

                string userID = item.GetDataKeyValue("UserID").ToString();

                if (Session["OriginalUserID"] == null) Session["OriginalUserID"] = Session["UserID"];
                Session["UserID"] = userID;

                Response.Redirect("~/Transactions/SESProcess.aspx");
            }            
        }
    }
}