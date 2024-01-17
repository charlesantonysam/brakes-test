using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SESWeb.Transactions
{
    public partial class Notifications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static bool UpdateNotificationViewStatus(string TimeStamp)
        {
            try
            {
                Authorization authorization = new Authorization();
                authorization.UserID = HttpContext.Current.Session["UserId"].ToString();
                authorization.NotificationTimeStamp = DateTime.Parse(TimeStamp);
                authorization.UpdateNotificationView();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}