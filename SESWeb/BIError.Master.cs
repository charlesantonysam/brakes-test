using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SESWeb.Pages
{
    public partial class BIError : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Authorization objAuthorization = new Authorization();
                string LandingPageURL = objAuthorization.SelectLandingPageURL();
                if (!LandingPageURL.Equals(string.Empty))
                {
                    lknHome.NavigateUrl = LandingPageURL;
                    lknHome.Visible = true;
                }
                else
                {
                    lknHome.NavigateUrl = "#";
                    lknHome.Visible = false;
                }
            }
            catch (Exception ex)
            {
                //LogError(ex.Message, ex.ToString());
                throw;
            }
        }
    }
}