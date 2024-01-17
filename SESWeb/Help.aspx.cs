using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SESWeb
{
    public partial class Help : System.Web.UI.Page
    {
        public string currentPage = "";
        public String UserID { get { return Session["UserId"] != null ? Session["UserId"].ToString() : string.Empty; } }
        public String ApplicationID { get { return Session["Application"] != null ? Session["Application"].ToString() : string.Empty; } }
        public Authorization objAuthorization = new Authorization();
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateMenu();
            try
            {
                string helpFolder = null;
                string MenuPath = ""; string helpFile = "";
                if (ConfigurationManager.AppSettings["HelpFilesRelativeUrl"] != null)
                {
                    helpFolder = ConfigurationManager.AppSettings["HelpFilesRelativeUrl"].ToString();
                }
                if (Request.QueryString["q"] != null)
                {
                    BISecurity bs = new BISecurity();
                    bs.setQueryStringToDecrypt(Request.QueryString["q"].ToString());
                    if (bs.getDecryptedQueryString("Menu") != null)
                    {
                       
                        if (bs.getDecryptedQueryString("MenuPath") != null)
                        {
                            MenuPath = bs.getDecryptedQueryString("MenuPath").ToString();
                            currentPage = MenuPath.Replace(">>", "/");
                        }
                        if (helpFolder != null)
                        {
                            helpFolder = ConfigurationManager.AppSettings["HelpFilesRelativeUrl"].ToString();                        
                        
                       helpFile = bs.getDecryptedQueryString("Menu").ToString();

                       pnlFileName.InnerHtml = MenuPath;
                      
                       helpFile = helpFolder + "/" + helpFile + ".html";
                            
                       if (File.Exists(Server.MapPath(helpFile)))
                       { 

                       MainPane.Scrolling = SplitterPaneScrolling.Both;
                       pnlContent.InnerHtml = File.ReadAllText(Server.MapPath(helpFile)).Replace("@@HelpFilesPath", helpFolder);
                       objAuthorization.PageName = helpFile;
                       objAuthorization.LogUserDetailsInDB();
                       }
                       else
                       {
                           pnlContent.InnerHtml = "No content found ! Kindly contact support team";
                       }
                        }
                        else {
                            pnlContent.InnerHtml = "Help folders configuration need to be done ! Contact administrator !";
                        }
                       pnlFileContent.Visible = true;
                    }
                }
                else
                {
                    MenuPath = "Home Page (Default Page)";
                    currentPage = "/Home";
                    pnlFileName.InnerHtml = MenuPath;
                      
                       helpFile = helpFolder + "/Home.html";
                       if (File.Exists(Server.MapPath(helpFile)))
                       { 

                       MainPane.Scrolling = SplitterPaneScrolling.Both;
                       pnlContent.InnerHtml = File.ReadAllText(Server.MapPath(helpFile)).Replace("@@HelpFilesPath", helpFolder);
                       pnlFileContent.Visible = true;
                       objAuthorization.PageName = helpFile;
                       objAuthorization.LogUserDetailsInDB();
                       }else{
                           pnlFileName.InnerHtml = helpFile + " Home Page Content not available";
                           pnlInfo.Visible = false; pnlFileContent.Visible = true;
                       }
                }
                fcp.InnerHtml = pnlContent.InnerHtml;
                fnp.InnerHtml = pnlFileName.InnerHtml;
            }
            catch (Exception exx)
            {

            }
        }        
        // Generates The Page Menu
        private void GenerateMenu()
        {
            try
            {
                objAuthorization.UserID = Session["UserID"].ToString();
                objAuthorization.ApplicationID = "BIEP";
                DataTable dtFunctionalities = objAuthorization.GenerateMenuByUserAndApplication().Tables[0];
                if (dtFunctionalities.Rows.Count > 0)
                {
                    String _ParentMenu = string.Empty;
                    RadTreeNode rmiParent = new RadTreeNode();
                    foreach (DataRow dr in dtFunctionalities.Rows)
                    {
                        BISecurity bs = new BISecurity();
                        bs.QueryStringsToEncrypt.Clear();
                        
                        if (_ParentMenu == dr[0].ToString())
                        {
                            bs.QueryStringsToEncrypt.Add("Menu", getFileName(dr[2].ToString()));
                            bs.QueryStringsToEncrypt.Add("MenuPath", rmiParent.Text  +" >> " + dr[1].ToString());                            
                            rmiParent.Nodes.Add(CreateMenuItem(dr[1].ToString(),"Help.aspx?q=" + bs.QueryStringsToEncrypt.getEncryptedString()));
                        }
                        else
                        {
                            
                            _ParentMenu = dr[0].ToString();
                            rmiParent = CreateMenuItem(_ParentMenu);
                            rtHome.Nodes.Add(rmiParent);
                             bs.QueryStringsToEncrypt.Add("Menu", getFileName(dr[2].ToString()));
                            bs.QueryStringsToEncrypt.Add("MenuPath", rmiParent.Text + " >> " + dr[1].ToString());  
                            rmiParent.Nodes.Add(CreateMenuItem(dr[1].ToString(),"Help.aspx?q=" + bs.QueryStringsToEncrypt.getEncryptedString()));
                        }
                    }
                }
                else
                {
                    LogErrorAndRedirect("Unauthorized", objAuthorization.UserID + " Doesn't Have a Single Role");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Create Menu
        private RadTreeNode CreateMenuItem(string _Text, string _NavigateURL = "")
        {
            try
            {
                RadTreeNode rmi = new RadTreeNode();
                rmi.Text = _Text;
                rmi.ToolTip = _Text;
                rmi.Expanded = true;
                if (_NavigateURL.Length > 0) {
                   
                    rmi.NavigateUrl =_NavigateURL;
                }
                return rmi;
            }
            catch (Exception)
            {

                throw;
            }

        }
        //gets FileName from the Passed FullPath
        private string getFileName(string path)
        {
            path = path.Replace("\\\\", "/");
            string[] spath = path.Split('/');
            string[] fn = spath[spath.Length-1].Split('.');
            return spath[spath.Length - 1].Replace("." + fn[fn.Length - 1],"").ToString();
        }
        // Log the Error and Redirected to the Source Page
        private void LogErrorAndRedirect(string _Message, string _Descriptoion)
        {
            objAuthorization.ErrorName = _Message;
            objAuthorization.ErrorDescription = _Descriptoion;
            objAuthorization.LogErrorDetailsInDB();
           // Response.Redirect("~/LogOut.aspx", false);
        }
    }
}