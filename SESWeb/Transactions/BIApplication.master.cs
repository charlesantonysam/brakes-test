using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Configuration;

namespace SESWeb
{
    public partial class BIApplication : System.Web.UI.MasterPage
    {
        public DateTime NotificationTimeStamp { get { return this.Session["NotificationTimeStamp" + this.LandingPageKey] != null ? DateTime.Parse(this.Session["NotificationTimeStamp" + this.LandingPageKey].ToString()) : DateTime.Now; } set { this.Session["NotificationTimeStamp" + this.LandingPageKey] = value; } }
        private string LandingPageKey { get { return this.ViewState["LandingPageKey"] != null ? this.ViewState["LandingPageKey"].ToString() : DateTime.Now.ToString("HHmmssfff"); } set { this.ViewState["LandingPageKey"] = value; } }
        public bool FirstItem = false;
        public String UserID { get { return Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty; } }
        public String ApplicationName { get { return ConfigurationManager.AppSettings["ApplicationName"].ToString(); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Path.GetFileName(Request.PhysicalPath).Equals("Profile.aspx"))
                {
                    lnkAppMenu.Attributes.Add("data-step", "-2");
                    lnkAppMenu.Attributes.Add("data-position", "bottom");
                    lnkAppMenu.Attributes.Add("data-intro", "Click to open application menu");

                    lnkPortalLandingPage.Attributes.Add("data-step", "-1");
                    lnkPortalLandingPage.Attributes.Add("data-position", "bottom");
                    lnkPortalLandingPage.Attributes.Add("data-intro", "Click to go to portal home");

                    lnkHome.Attributes.Add("data-step", "0");
                    lnkHome.Attributes.Add("data-position", "bottom");
                    lnkHome.Attributes.Add("data-intro", "Click to go to application home");
                }


                ApplicationManagement applicationManagement = new ApplicationManagement();
                Authorization authorization = new Authorization();
                authorization.UserID = this.Master.UserID;
                DataTable dtMenuList = GenerateMenus();

                DataSet dsNotification = authorization.SelectNotificationDetails();
                rlvNotification.DataSource = dsNotification.Tables[1];
                rlvNotification.DataBind();

                NotificationTimeStamp = authorization.NotificationTimeStamp;
                if (dsNotification.Tables[0].Rows[0][0].ToString().Equals("0"))
                {
                    lblNotificationCount.Visible = false;
                    lblNotificationBadge.Visible = false;
                    lnkNotification.Attributes.Add("onmouseover", "UpdateNotificationViewStatus()");
                }
                else
                {
                    lblNotificationCount.Text = string.Format("<h5>{0}</h5>", dsNotification.Tables[0].Rows[0][0].ToString());
                    lblNotificationBadge.Visible = true;
                    lblNotificationCount.Visible = true;
                    lnkNotification.Attributes.Add("onmouseover", "UpdateNotificationViewStatus()");
                }
  
                                           
                this.Master.GetLandingPageURL();
                lnkPortalLandingPage.HRef = this.Master.LandingPageURL;

                //if (Path.GetFileName(Request.PhysicalPath).Equals("VendorMasterApprovalProcess.aspx"))
                //{
                //    lnkHome.CssClass = "breadcrumbbutton active";
                //}
                //else
                //{
                //    lnkHome.CssClass = "breadcrumbbutton";
                //}

                if (UserID.Equals("18820") || UserID.Equals("14510") || UserID.Equals("23051"))
                    pnlAdmin.Visible = true;
                else
                    pnlAdmin.Visible = false;
            }
            catch (Exception ex)
            {
                this.Master.LogError(ex.Message, ex.ToString());
            }
        }

        protected void txtSearchBox_Search(object sender, Telerik.Web.UI.SearchBoxEventArgs e)
        {
            try
            {
                if (e.DataItem != null)
                {
                    // Commented By Geetha N on 27.05.2021
                    //Response.Redirect(((Dictionary<string, object>)e.DataItem)["SharepointApplicationUrl"].ToString());

                    if (((Dictionary<string, object>)e.DataItem)["Type"].ToString().Equals("Link"))
                    {
                        Response.Redirect(string.Format("~/{0}", ((Dictionary<string, object>)e.DataItem)["Value"].ToString()));
                        this.txtSearchBox.Text = string.Empty;
                    }
                    else
                    {
                        string SearchValue = ((Dictionary<string, object>)e.DataItem)["Value"].ToString();
                        Dictionary<string, string> Params = SearchValue.Split('~')
                          .Select(s => s.Split(':'))
                          .ToDictionary(key => key[0].Trim(), value => value[1].Trim());


                        string _GetRequestNo = Params["RequestNo"].ToString();
                        int _GetSequence = Convert.ToInt32(Params["Sequence"].ToString());
                        string _QueryString = _GetRequestNo + "&" + _GetSequence;
                        string URL = string.Format("./Report.aspx?q={0}", PrepareQueryString(_QueryString));
                        Response.Redirect(URL);                            
                        
                    }
                }
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SLP - {0}", ex.Message), ex.ToString());
            }
        }
        protected string PrepareQueryString(string QueryString)
        {
            BISecurity bs = new BISecurity();
            bs.QueryStringsToEncrypt.Clear();
            bs.QueryStringsToEncrypt.Add("QueryString", QueryString);
           
            return bs.QueryStringsToEncrypt.getEncryptedString();
        }
        protected void rlvNotification_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            try
            {
                RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
                Authorization authorization = new Authorization();
                authorization.UserID = this.Master.UserID;
               
                authorization.UpdateNotificationRead(item.GetDataKeyValue("ApplicationName").ToString(), item.GetDataKeyValue("NotificationId").ToString());
                if (e.CommandName.Equals("OpenTask"))
                {
                    if (item.GetDataKeyValue("ApplicationName").ToString().Equals(ApplicationName))
                    {
                        string _QueryString = item.GetDataKeyValue("Querystring").ToString() + "&MailLink";
                        string URL = string.Format("./{0}?q={1}", item.GetDataKeyValue("PageName").ToString(), PrepareQueryString(_QueryString));
                        Response.Redirect(URL);                        
                    }
                    else if (item.GetDataKeyValue("ApplicationClientId").ToString().Equals(string.Empty) || item.GetDataKeyValue("VirtualDirectory").ToString().Equals(string.Empty))
                    {
                        string _RedirectionPage = item.GetDataKeyValue("RedirectionPage").ToString();
                        string _QueryString = item.GetDataKeyValue("Querystring").ToString() + "&MailLink";
                        string URL = string.Format("{0}?q={1}", _RedirectionPage, PrepareQueryString(_QueryString));
                        Response.Redirect(URL);                        
                    }
                    else
                    {
                        string portalURL = item.GetDataKeyValue("PortalURL").ToString();
                        string ApplicationClientId = item.GetDataKeyValue("ApplicationClientId").ToString();
                        string VirtualDirectory = item.GetDataKeyValue("VirtualDirectory").ToString();
                        string PageName = item.GetDataKeyValue("PageName").ToString();
                        string _QueryString = item.GetDataKeyValue("Querystring").ToString() + "&MailLink";
                        string URL = string.Format("{0}?client_id={1}&redirect_uri=https%3A%2F%2Fportaldev.brakesindia.com%2F{2}%2FTransactions%2F{3}%3F%7BStandardTokens%7D", portalURL, ApplicationClientId, VirtualDirectory, PageName);                        
                        Response.Redirect(Server.UrlEncode(URL));  
                    }
                }
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SLP - {0}", ex.Message), ex.ToString());
            }
        }
        public void LogError(string Message, string ErrorTrace)
        {
            try
            {
                this.Master.LogError(Message, ErrorTrace);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ShowMessage(string Message, BI.MessageType messageType)
        {
            try
            {
                this.Master.ShowMessage(Message, messageType);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private DataTable GenerateMenus()
        {
            try
            {
                Authorization authorization = new Authorization();
                authorization.UserID = Session["UserID"].ToString();
                authorization.ApplicationID = ApplicationName;
                DataTable dtFunctionalities = authorization.GenerateMenuByUserAndApplication().Tables[0];
                rpMenus.Items.Clear();
                //string ParentMenus = string.Empty;
                //string Menus = "<div class='tab-content'>";
                ////string[] ApplicationGoupClass = { "ico-green", "ico-lblue", "ico-violet", "ico-dblue", "ico-red", "ico-sblue", "ico-orange" };
                if (dtFunctionalities.Rows.Count > 0)
                {
                    //    DataTable dtParentMenus = dtFunctionalities.AsEnumerable()
                    //      .GroupBy(r => r.Field<string>("FunctionalityParent"))
                    //      .Select(g =>
                    //      {
                    //          var row = dtFunctionalities.NewRow();

                    //          row["FunctionalityParent"] = g.Key;
                    //          row["FunctionalityColor"] = g.First()["FunctionalityColor"].ToString();
                    //          row["PendingTasks"] = g.Sum(r => r.Field<int>("PendingTasks"));
                    //          return row;
                    //      }).CopyToDataTable();

                    //    if (dtParentMenus.Rows.Count > 0)
                    //    {
                    //        ParentMenus = "<ul class='nav nav-tabs sub-menu' role='tablist'><div class='owl_1 owl-carousel owl-theme'>";
                    //        foreach (DataRow drApplications in dtParentMenus.Rows)
                    //        {
                    //            ParentMenus += string.Format("<div class='item'><li role = 'presentation' class='ParentMenuLevel' ><a href = '#{1}' submenuid='{1}' aria-controls='{1}' role='tab' data-toggle='tab' style='background-color:{0};border-radius:5px;'>{2}</a></LI></DIV>", drApplications["FunctionalityColor"].ToString(), drApplications["FunctionalityParent"].ToString().Replace(" ", "_").Replace("/", "_").Replace("&", "_"), drApplications["FunctionalityParent"].ToString());
                    //            //if (drApplications["PendingTasks"].ToString().Equals("0"))
                    //            //    ApplicationGroups += string.Format("<a href = '#{0}' class='count' aria-controls='{1}' role='tab' data-toggle='tab'><div class='ico-num' style='position:relative;top:-8px;'></div><div class='ic - item'></div></a></li></div>", drApplications["ApplicationGroup"].ToString(), drApplications["ApplicationGroup"].ToString());
                    //            //else
                    //            //    ApplicationGroups += string.Format("<a href = '#{0}' class='count' aria-controls='{1}' role='tab' data-toggle='tab'><div class='ico-num' style='position:relative;top:-8px;'>{2}</div><div class='ic - item'></div></a></li></div>", drApplications["ApplicationGroup"].ToString(), drApplications["ApplicationGroup"].ToString(), drApplications["PendingTasks"].ToString());
                    //            //ApplicationGroups += "<div class='ic-item'>&nbsp;</div>";


                    //            List<DataRow> dtApplicatoinsByGroup = (from applications in dtFunctionalities.AsEnumerable()
                    //                                                   where applications.Field<string>("FunctionalityParent") == drApplications["FunctionalityParent"].ToString()
                    //                                                   select applications).ToList<DataRow>();

                    //            Menus += string.Format("<div role='tabpanel' class='tab-pane MenuLevel' id='{0}'><ul class='nav nav-tabs child-sub-menu' role='tablist'>", drApplications["FunctionalityParent"].ToString().Replace(" ", "_").Replace("/", "_").Replace("&", "_"));
                    //            for (int j = 0; j < dtApplicatoinsByGroup.Count; j++)
                    //            {
                    //                if (dtApplicatoinsByGroup[j]["PendingTasks"].ToString().Equals("0"))
                    //                    Menus += string.Format("<li role='presentation' ><a style='Background-Color:{0};' onclick='ShowCustomLoading();' href='/ESSUIUX{1}' title='{2}'>{3}</a></li>", drApplications["FunctionalityColor"].ToString(), dtApplicatoinsByGroup[j]["FunctionalityPath"].ToString(), dtApplicatoinsByGroup[j]["FunctionalityDescription"].ToString(), dtApplicatoinsByGroup[j]["FunctionalityDescription"].ToString());
                    //                else
                    //                    Menus += string.Format("<li role='presentation' ><a style='Background-Color:{0};' onclick='ShowCustomLoading();' href='/ESSUIUX{1}' title='{2}'>{3}<span class='scn-circle'>{4}</span></a></li>", drApplications["FunctionalityColor"].ToString(), dtApplicatoinsByGroup[j]["FunctionalityPath"].ToString(), dtApplicatoinsByGroup[j]["FunctionalityDescription"].ToString(), dtApplicatoinsByGroup[j]["FunctionalityDescription"].ToString(), dtApplicatoinsByGroup[j]["PendingTasks"].ToString());
                    //            }
                    //            Menus += "</ul></div>";
                    //        }
                    //        ParentMenus += "</div></ul>";
                    //        Menus += "</div>";
                    //    }

                    String _ParentMenu = string.Empty;
                    RadPanelItem rmiParent = new RadPanelItem();

                    foreach (DataRow dr in dtFunctionalities.Rows)
                    {
                        if (_ParentMenu == dr[0].ToString())
                        {
                            rmiParent.Items.Add(CreateMenuItem(dr[1].ToString(), dr[2].ToString()));
                        }
                        else
                        {
                            _ParentMenu = dr[0].ToString();
                            rmiParent = CreateMenuItem(_ParentMenu);
                            // rmiParent.ImageUrl = "../Resources/Images/Document.png";
                            rpMenus.Items.Add(rmiParent);
                            rmiParent.Items.Add(CreateMenuItem(dr[1].ToString(), dr[2].ToString()));
                        }
                    }

                    RadPanelItem selectedItem = rpMenus.FindItemByUrl(Request.FilePath);
                    if (selectedItem != null)
                    {
                        if (selectedItem.Items.Count > 0)
                        {
                            selectedItem.Expanded = true;
                            selectedItem.Selected = true;
                        }
                        else
                        {
                            selectedItem.Selected = true;
                            while ((selectedItem != null) &&
                                   (selectedItem.Parent.GetType() == typeof(RadPanelItem)))
                            {
                                selectedItem = (RadPanelItem)selectedItem.Parent;
                                selectedItem.Expanded = true;
                            }
                        }
                    }

                }
                else
                {
                    Master.LogErrorAndRedirect("Unauthorized", Master.UserID + " Doesn't Have a Single Role");
                }
                //lblMenuList.Text = string.Format("{0}{1}", ParentMenus, Menus);
                return dtFunctionalities;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private RadPanelItem CreateMenuItem(string _Text, string _NavigateURL = "")
        {
            try
            {
                RadPanelItem rmi = new RadPanelItem();
                rmi.Text = _Text;
                if (_NavigateURL.Length < 0)
                    rmi.ImageUrl = "../Resources/Images/Document.png";
                rmi.ToolTip = _Text;
                if (_NavigateURL.Length > 0)
                {
                    rmi.NavigateUrl = "~/" + _NavigateURL;
                    rmi.Attributes.Add("onclick", "ShowCustomLoading();");
                }
                return rmi;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            Session["UserID"] = txtUserId.Text;
            Response.Redirect("./Report.aspx");
        }

        public string GetUserAvatarURL(string FileName)
        {
            try
            {
                return this.Master.GetUserAvatarURL(FileName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ShowWigetMenu()
        {
            try
            {
                this.Master.ShowWigetMenu();
            }
            catch (Exception)
            {

                throw;
            }
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