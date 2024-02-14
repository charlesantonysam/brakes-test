using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SESWeb
{
    public partial class BI : System.Web.UI.MasterPage
    {
        public string UserID { get { return Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty; } }
        public string UserName { get { return Session["LoginName"] != null ? Session["LoginName"].ToString() : string.Empty; } }
        public string ProfileURL { get { return Session["ProfileURL"] != null ? Session["ProfileURL"].ToString() : "#"; } set { Session["ProfileURL"] = value; } }
        public string LandingPageURL { get { return ViewState["LandingPageURL"] != null ? ViewState["LandingPageURL"].ToString() : ""; } set { ViewState["LandingPageURL"] = value; } }
        public String ApplicationName { get { return ConfigurationManager.AppSettings["ApplicationName"].ToString(); } }

        Authorization objAuthorization = new Authorization();
        public SharePointContext spContext;
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                var path = Path.GetFileName(Request.PhysicalPath);

                // Validating Login Details
                bool IsAuthenticated = true;

                if (Session["UserID"] == null || Session["UserName"] == null || Session["LoginName"] == null) // Validating Session Values
                {
                    Session["UserID"] = "12286";
                    Session["UserName"] = "Bharani Kumar";
                    Session["LoginName"] = "Bharani Kumar";
                    IsAuthenticated = true;
                }

                //bool IsAuthenticated = true;
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    IsAuthenticated = false;
                else if (Session["UserID"] == null || Session["UserName"] == null || Session["LoginName"] == null) // Validating Session Values
                    IsAuthenticated = false;

                if (!IsAuthenticated)
                {
                    var spContext = SharePointContextProvider.Current.GetSharePointContext(Context);
                    Session["hostWeb"] = Page.Request["SPHostUrl"];
                    Session["spContext"] = spContext;
                    if (spContext != null)
                    {
                        using (var clientContext = spContext.CreateUserClientContextForSPHost())
                        {
                            if (clientContext != null)
                            {
                                clientContext.Load(clientContext.Web);
                                clientContext.Load(clientContext.Web.CurrentUser);
                                clientContext.ExecuteQuery();
                                int number;
                                string userid = clientContext.Web.CurrentUser.LoginName.Split('|')[2].Split('@')[0];
                                bool success = Int32.TryParse(userid, out number);
                                string LoggedInUser = string.Empty;
                                if (success)
                                {
                                    LoggedInUser = userid;
                                }
                                else
                                {
                                    BICommon objBICommon = new BICommon();
                                    DataSet _dsGetSpecialEmployeeByUserid = objBICommon.SelectSpecialEmployeeNoByUserId(userid);
                                    if (_dsGetSpecialEmployeeByUserid.Tables[0].Rows.Count > 0)
                                    {
                                        LoggedInUser = _dsGetSpecialEmployeeByUserid.Tables[0].Rows[0]["EmpNo"].ToString();
                                    }
                                    else
                                    {
                                        string email = clientContext.Web.CurrentUser.Email;
                                        DataSet dsSpecialEmployeeNo = objBICommon.SelectSpecialEmployeeNo(email);
                                        if (email != "" && email != null)
                                        {
                                            if (dsSpecialEmployeeNo.Tables.Count > 0)
                                            {
                                                if (dsSpecialEmployeeNo.Tables[0].Rows.Count > 0)
                                                {
                                                    LoggedInUser = dsSpecialEmployeeNo.Tables[0].Rows[0]["pernr"].ToString();
                                                }
                                            }
                                        }
                                    }
                                }

                                if (!LoggedInUser.Equals(string.Empty))
                                {
                                    Session["UserID"] = LoggedInUser;
                                    Session["UserName"] = clientContext.Web.CurrentUser.Title;
                                    Session["LoginName"] = clientContext.Web.CurrentUser.Title;
                                    // Creating User Details For Cookie Store
                                    string UserDetails = string.Format("{0}|{1}|{2}", objAuthorization.UserID, clientContext.Web.CurrentUser.Title, clientContext.Web.CurrentUser.Email);
                                    // Create the cookie that contains the forms authentication ticket
                                    HttpCookie authCookie = FormsAuthentication.GetAuthCookie(objAuthorization.UserID, false);
                                    // Get the FormsAuthenticationTicket out of the encrypted cookie
                                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                                    // Create a new FormsAuthenticationTicket that includes our custom User Data
                                    FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, UserDetails);
                                    // Update the authCookie's Value to use the encrypted version of newTicket
                                    authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                                    // Manually add the authCookie to the Cookies collection
                                    Response.Cookies.Add(authCookie);

                                    objAuthorization.SessionID = Session.SessionID;
                                    // Logged In User ID
                                    objAuthorization.UserID = Session["UserID"].ToString();
                                    // Current Page Name
                                    objAuthorization.PageName = Path.GetFileName(Request.PhysicalPath);
                                    // Application ID
                                    objAuthorization.ApplicationID = ApplicationName;
                                    // Remote Host ID
                                    objAuthorization.RemoteHost = Request.UserHostAddress;
                                    // Client Browser Version
                                    objAuthorization.Browser = Request.Browser.Id + " - " + Request.Browser.Version;
                                    // Client Operating System
                                    objAuthorization.PlatForm = Request.Browser.Platform;
                                    if (!path.Equals("Profile.aspx") && !path.Equals("Notifications.aspx") && !objAuthorization.ValidateFunctionalityAuthorizationForUser())
                                        Response.Redirect("~/Error.aspx", false);
                                    else
                                        objAuthorization.LogUserDetailsInDB();
                                }
                                else
                                {
                                    Response.Redirect("~/Error.aspx", false);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Development debug mode commented this while go to live it will be uncomment

                        //Response.Redirect("~/Error.aspx", false);
                    }
                }
                else
                {
                    objAuthorization.SessionID = Session.SessionID;
                    // Logged In User ID
                    objAuthorization.UserID = Session["UserID"].ToString();
                    // Current Page Name
                    objAuthorization.PageName = Path.GetFileName(Request.PhysicalPath);
                    // Application ID
                    objAuthorization.ApplicationID = ApplicationName;
                    // Remote Host ID
                    objAuthorization.RemoteHost = Request.UserHostAddress;
                    // Client Browser Version
                    objAuthorization.Browser = Request.Browser.Id + " - " + Request.Browser.Version;
                    // Client Operating System
                    objAuthorization.PlatForm = Request.Browser.Platform;
                    if (!path.Equals("Profile.aspx") && !path.Equals("Notifications.aspx") && !objAuthorization.ValidateFunctionalityAuthorizationForUser())
                        Response.Redirect("~/Error.aspx", false);
                    else
                        objAuthorization.LogUserDetailsInDB();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                rnMessage.VisibleOnPageLoad = false;

                if (Session["OriginalUserID"] != null)
                {
                    divImp.Visible = true;
                    ImpText.InnerHtml = "You are impersonating user " + Session["UserID"].ToString() + ".";
                }
                else
                {
                    divImp.Visible = false;
                }

                if (!IsPostBack)
                {
                    lblUserName.Text = Session["LoginName"] == null ? string.Empty : Session["LoginName"].ToString();
                    imgUser.Src = GetUserAvatarURL(UserID);
                    //LoadEmployees();

                }
                GenerateApps();
                GetLandingPageURL();
                lnkMobLandingPageURL.HRef = LandingPageURL;
                lnkLandingPageURL.HRef = LandingPageURL;
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex.ToString());
                //throw;
            }
        }
        public void GetLandingPageURL()
        {
            try
            {
                if (LandingPageURL.Equals(string.Empty))
                {
                    string _LandingPageURL = objAuthorization.SelectLandingPageURL();
                    if (_LandingPageURL.Equals(string.Empty))
                    {
                        LandingPageURL = "#";
                    }
                    else
                    {
                        LandingPageURL = _LandingPageURL;
                    }
                }
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
                //liWidgetMenu.Visible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GetUserAvatarURL(string FileName)
        {
            try
            {
                string fullpath = string.Format("Photos/{0}.jpg", FileName.TrimStart('0').Trim());
                SecuredFileDownload sfd = new SecuredFileDownload(fullpath, FileDownloadPathType.AzureBlob, false, "", false, RequestedFileNotFoundAction.DownloadAlternateFile, "~/Resources/PIXEL/Images/icons/user.png");
                sfd.SetHeader("Content-Type", "Application/octet-stream");
                sfd.SetHeader("Content-Disposition", "attachment; filename=" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg");
                return sfd.GenerateUrl("../download.ashx?q=", true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GenerateApps()
        {
            try
            {
                Authorization authorization = new Authorization();
                authorization.UserID = Session["UserID"].ToString();
                DataTable dtApplications = authorization.SelectGroupwiseApplicationsById().Tables[0];
                string ApplicationGroups = string.Empty;
                string Applications = "<div class='tab-content'>";
                //string[] ApplicationGoupClass = { "ico-green", "ico-lblue", "ico-violet", "ico-dblue", "ico-red", "ico-sblue", "ico-orange" };
                if (dtApplications.Rows.Count > 0)
                {
                    DataTable dtApplicationGroup = dtApplications.AsEnumerable()
                      .GroupBy(r => r.Field<string>("ApplicationGroup"))
                      .Select(g =>
                      {
                          var row = dtApplications.NewRow();

                          row["ApplicationGroup"] = g.Key;
                          row["ApplicationColor"] = g.First()["ApplicationColor"].ToString();
                          row["PendingTasks"] = g.Sum(r => r.Field<int>("PendingTasks"));
                          return row;
                      }).CopyToDataTable();

                    if (dtApplicationGroup.Rows.Count > 0)
                    {
                        ApplicationGroups = "<ul class='nav nav-tabs sub-menu' role='tablist'><div class='owl_1 owl-carousel owl-theme'>";
                        foreach (DataRow drApplications in dtApplicationGroup.Rows)
                        {
                            ApplicationGroups += string.Format("<div class='item'><li role = 'presentation' class='applicationgrouplevel' ><a href = '#{1}' submenuid='{1}' aria-controls='{1}' role='tab' data-toggle='tab' style='background-color:{0};border-radius:5px;'>{2}</a></LI></DIV>", drApplications["ApplicationColor"].ToString(), drApplications["ApplicationGroup"].ToString().Replace(" ", "_").Replace("/", "_").Replace("&", "_"), drApplications["ApplicationGroup"].ToString());
                            //if (drApplications["PendingTasks"].ToString().Equals("0"))
                            //    ApplicationGroups += string.Format("<a href = '#{0}' class='count' aria-controls='{1}' role='tab' data-toggle='tab'><div class='ico-num' style='position:relative;top:-8px;'></div><div class='ic - item'></div></a></li></div>", drApplications["ApplicationGroup"].ToString(), drApplications["ApplicationGroup"].ToString());
                            //else
                            //    ApplicationGroups += string.Format("<a href = '#{0}' class='count' aria-controls='{1}' role='tab' data-toggle='tab'><div class='ico-num' style='position:relative;top:-8px;'>{2}</div><div class='ic - item'></div></a></li></div>", drApplications["ApplicationGroup"].ToString(), drApplications["ApplicationGroup"].ToString(), drApplications["PendingTasks"].ToString());
                            //ApplicationGroups += "<div class='ic-item'>&nbsp;</div>";


                            List<DataRow> dtApplicatoinsByGroup = (from applications in dtApplications.AsEnumerable()
                                                                   where applications.Field<string>("ApplicationGroup") == drApplications["ApplicationGroup"].ToString()
                                                                   select applications).ToList<DataRow>();

                            Applications += string.Format("<div role='tabpanel' class='tab-pane applicationlevel' id='{0}'><ul class='nav nav-tabs child-sub-menu' role='tablist'>", drApplications["ApplicationGroup"].ToString().Replace(" ", "_").Replace("/", "_").Replace("&", "_"));
                            for (int j = 0; j < dtApplicatoinsByGroup.Count; j++)
                            {
                                if (dtApplicatoinsByGroup[j]["PendingTasks"].ToString().Equals("0"))
                                    Applications += string.Format("<li role='presentation' ><a style='Background-Color:{0};' href='{1}' onclick='ShowCustomLoading();' title='{2}'>{3}</a></li>", drApplications["ApplicationColor"].ToString(), dtApplicatoinsByGroup[j]["SharepointApplicationUrl"].ToString(), dtApplicatoinsByGroup[j]["ApplicationFullDescription"].ToString(), dtApplicatoinsByGroup[j]["ApplicationFullDescription"].ToString());
                                else
                                    Applications += string.Format("<li role='presentation' ><a style='Background-Color:{0};' href='{1}' onclick='ShowCustomLoading();' title='{2}'>{3}<span class='scn-circle'>{4}</span></a></li>", drApplications["ApplicationColor"].ToString(), dtApplicatoinsByGroup[j]["SharepointApplicationUrl"].ToString(), dtApplicatoinsByGroup[j]["ApplicationFullDescription"].ToString(), dtApplicatoinsByGroup[j]["ApplicationFullDescription"].ToString(), dtApplicatoinsByGroup[j]["PendingTasks"].ToString());

                                if (dtApplicatoinsByGroup[j]["ApplicationName"].ToString().Equals(ApplicationName))
                                    ProfileURL = dtApplicatoinsByGroup[j]["SharepointApplicationUrl"].ToString();
                            }
                            Applications += "</ul></div>";
                        }
                        ApplicationGroups += "</div></ul>";
                        Applications += "</div>";
                    }
                }
                lblApplicationList.Text = string.Format("{0}{1}", ApplicationGroups, Applications);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void LogError(string _Message, string _Description)
        {
            LogErrorDetailsInDB(_Description, _Description);
            ShowMessage(_Description, MessageType.Error);
            //ShowMessage("Error Occured, Try After Sometime or Contact Administrator", MessageType.Error);
        }
        public void LogErrorDetailsInDB(string _Message, string _Description)
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[8];
                _params[0] = new SqlParameter("@UserID", UserID);
                _params[1] = new SqlParameter("@ApplicationID", ApplicationName);
                _params[2] = new SqlParameter("@PageName", Path.GetFileName(Request.PhysicalPath));
                _params[3] = new SqlParameter("@SessionID", "Azure");
                _params[4] = new SqlParameter("@RemoteHost", Request.UserHostAddress);
                _params[5] = new SqlParameter("@ErrorName", _Message);
                _params[6] = new SqlParameter("@ErrorDescription", _Description);
                _params[7] = new SqlParameter("@Return", DBNull.Value);
                _params[7].SqlDbType = SqlDbType.Int;
                _params[7].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_LogErrorDetailsInDB", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Notification Message Types
        public enum MessageType
        {
            Success = 0,
            Information = 1,
            Warning = 2,
            Error = 3
        }

        protected void LoadBirthdays()
        {
            //try
            //{
            //    DataSet _bds = new DataSet();
            //    DataTable _bdt = new DataTable();
            //    //if (ConfigurationManager.AppSettings["EmployeeBirthdaysXmlUrl"] != null)
            //    //{
            //    //    _bdUrl = ConfigurationManager.AppSettings["EmployeeBirthdaysXmlUrl"].ToString();
            //    //    _bds.ReadXml(_bdUrl);
            //    ApplicationManagement um = new ApplicationManagement();
            //    _bds = um.SelectBirthDayUsers();


            //    if (_bds.Tables.Count > 0)
            //    {
            //        _bdt = _bds.Tables[0];
            //    }
            //    if (_bdt.Rows.Count > 0)
            //    {
            //        _bdt.Columns.Add(new DataColumn("EPHOTO", typeof(string)));
            //        BISecurity bis = new BISecurity();
            //        int i = 0;
            //        foreach (DataRow dr in _bdt.Rows)
            //        {
            //            //fullpath = imgUrl.Replace("%EMPNO%", dr["PERNR"].ToString().TrimStart('0').Trim());
            //            fullpath = ConfigurationManager.AppSettings["BlobPhotos"].ToString() + dr["PERNR"].ToString().TrimStart('0').Trim() + ".jpg";
            //            SecuredFileDownload sfd = new SecuredFileDownload(fullpath, FileDownloadPathType.AzureBlob, false, "", false, RequestedFileNotFoundAction.DownloadAlternateFile, "/MyApps/Resources/Images/no-image.jpg");
            //            sfd.SetHeader("Content-Type", "Application/octet-stream");
            //            sfd.SetHeader("Content-Disposition", "attachment; filename=" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg");

            //            _bdt.Rows[i]["EPHOTO"] = sfd.GenerateUrl("../download.ashx?q=");
            //            i++;
            //        }
            //        bdList.DataSource = _bdt;
            //        bdList.DataBind();
            //    }
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }
        protected void LoadLeaving()
        {
            //try
            //{
            //    DataSet _lls = new DataSet();
            //    DataTable _llt = new DataTable();
            //    //if (ConfigurationManager.AppSettings["EmployeeBirthdaysXmlUrl"] != null)
            //    //{
            //    //    _bdUrl = ConfigurationManager.AppSettings["EmployeeBirthdaysXmlUrl"].ToString();
            //    //    _bds.ReadXml(_bdUrl);
            //    ApplicationManagement um = new ApplicationManagement();
            //    _lls = um.SelectLeavingUsers();


            //    if (_lls.Tables.Count > 0)
            //    {
            //        _llt = _lls.Tables[0];
            //    }
            //    if (_llt.Rows.Count > 0)
            //    {
            //        _llt.Columns.Add(new DataColumn("EPHOTO", typeof(string)));
            //        BISecurity bis = new BISecurity();
            //        int i = 0;
            //        foreach (DataRow dr in _llt.Rows)
            //        {
            //            //fullpath = imgUrl.Replace("%EMPNO%", dr["PERNR"].ToString().TrimStart('0').Trim());
            //            fullpath = ConfigurationManager.AppSettings["BlobPhotos"].ToString() + dr["PERNR"].ToString().TrimStart('0').Trim() + ".jpg";
            //            SecuredFileDownload sfd = new SecuredFileDownload(fullpath, FileDownloadPathType.AzureBlob, false, "", false, RequestedFileNotFoundAction.DownloadAlternateFile, "/MyApps/Resources/Images/no-image.jpg");
            //            sfd.SetHeader("Content-Type", "Application/octet-stream");
            //            sfd.SetHeader("Content-Disposition", "attachment; filename=" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg");

            //            _llt.Rows[i]["EPHOTO"] = sfd.GenerateUrl("../download.ashx?q=");
            //            i++;
            //        }
            //        leavingList.DataSource = _llt;
            //        leavingList.DataBind();
            //        lblLeavingStatus.Visible = false;
            //    }
            //    else
            //    {
            //        lblLeavingStatus.Visible = true;
            //        leavingList.Visible = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }
        protected void LoadRecentlyJoinedEmployees()
        {
            //try
            //{
            //    DataSet _bds = new DataSet();
            //    DataTable _bdt = new DataTable();
            //    ApplicationManagement um = new ApplicationManagement();
            //    _bds = um.SelectRecentlyJoinedUsers();


            //    if (_bds.Tables.Count > 0)
            //    {
            //        _bdt = _bds.Tables[0];
            //    }
            //    if (_bdt.Rows.Count > 0)
            //    {
            //        _bdt.Columns.Add(new DataColumn("EPHOTO", typeof(string)));
            //        BISecurity bis = new BISecurity();
            //        int i = 0;
            //        foreach (DataRow dr in _bdt.Rows)
            //        {
            //            //fullpath = imgUrl.Replace("%EMPNO%", dr["PERNR"].ToString().TrimStart('0').Trim());
            //            fullpath = ConfigurationManager.AppSettings["BlobPhotos"].ToString() + dr["PERNR"].ToString().TrimStart('0').Trim() + ".jpg";
            //            SecuredFileDownload sfd = new SecuredFileDownload(fullpath, FileDownloadPathType.AzureBlob, false, "", false, RequestedFileNotFoundAction.DownloadAlternateFile, "/MyApps/Resources/Images/no-image.jpg");
            //            sfd.SetHeader("Content-Type", "Application/octet-stream");
            //            sfd.SetHeader("Content-Disposition", "attachment; filename=" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg");

            //            _bdt.Rows[i]["EPHOTO"] = sfd.GenerateUrl("../download.ashx?q=");
            //            i++;
            //        }
            //        recentlist.DataSource = _bdt;
            //        recentlist.DataBind();
            //    }
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }
        public void ShowMessage(string _Message, MessageType _MessageType)//,bool Autoclose=true)
        {
            if (_Message.Length < 1)
                return;
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            _Message = textInfo.ToTitleCase(_Message);
            string _MessageClass = "danger";
            switch (_MessageType)
            {
                case MessageType.Success:
                    _MessageClass = "success";
                    break;
                case MessageType.Information:
                    _MessageClass = "info";
                    break;
                case MessageType.Warning:
                    _MessageClass = "warning";
                    break;
                case MessageType.Error:
                    _MessageClass = "danger";
                    break;
            }
            lblMessageBody.Text = string.Format("<div class='new-message-box no-width'><div class='new-message-box-{0}'><div class='info-tab tip-icon-{0}' title='error'><i></i></div><div class='tip-box-{0}'><p>{1}</p></div></div></div>", _MessageClass, _Message);
            //rnMessage.Visible = true;
            rnMessage.VisibleOnPageLoad = true;
            //if (Autoclose)
            //    rnMessage.AutoCloseDelay = 9000;
            //else
            //    rnMessage.AutoCloseDelay = 0;
        }

        //protected void ddlUser_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    try
        //    {
        //        Session["UserID"] = ddlUsers.SelectedValue.ToString();
        //        Session["LoginName"] = ddlUsers.SelectedItem.Text.ToString();
        //        Session["UserName"] = ddlUsers.SelectedItem.Text.ToString();
        //        Server.Transfer("./Profile.aspx");
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //private void LoadEmployees()
        //{
        //    try
        //    {
        //        Employee employee = new Employee();
        //        DataSet dsUsers = employee.SelectUserRolesByApplicationName("ESS");
        //        BICommon common = new BICommon();
        //        ddlUsers.Items.Clear();
        //        ddlUsers.Items.Add(new Telerik.Web.UI.RadComboBoxItem(""));
        //        foreach (DataRow drUsers in dsUsers.Tables[0].Rows)
        //        {
        //            ddlUsers.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Format("{0} - {1}", drUsers[0].ToString(), drUsers[1].ToString()), drUsers[0].ToString()));
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public void LogErrorAndRedirect(string _Message, string _Descriptoion)
        {
            objAuthorization.ErrorName = _Message;
            objAuthorization.ErrorDescription = _Descriptoion;
            objAuthorization.LogErrorDetailsInDB();
            Response.Redirect("~/SessionExpired.aspx", false);
        }

        protected void butCancelImporsonation_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["OriginalUserID"];
            Session["OriginalUserID"] = null;
            Response.Redirect("~/Transactions/UserImpersonation.aspx");
        }
    }
}