using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SESWeb.Transactions
{
    public partial class DownloadReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserID"] == null || Session["UserName"] == null) // Validating Session Values
                    Response.Redirect("~/LogOut.aspx?strType=1", false);
                else
                {
                    if (Session["InvoiceDocument"] != null)
                    {
                        {
                            Response.Clear();
                            Response.ContentType = "application/pdf";
                            Response.BinaryWrite((Byte[])Session["InvoiceDocument"]);
                            Session.Remove("InvoiceDocument");
                            Response.End();
                        }
                    }
                    else
                    {
                        Response.Write("");
                    }
                }
            }
            catch (Exception ex)
            {
                Master.LogError(ex.Message, ex.ToString());
            }
        }
    }
}