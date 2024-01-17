using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;
using System.Drawing;
using Telerik.Web.UI.Gantt;
using System.Data.SqlClient;
using System.Configuration;
using MyDetailsWeb;

namespace MyDetailsWeb.Transactions.SAPDBALERT
{
    public partial class ManageSAPDBAlert : System.Web.UI.Page
    {
        public bool isExporting = false;
        clsSAPDBLogAlert ObjSAP = new clsSAPDBLogAlert();
        BICommon objBiCommon = new BICommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //DivGrid.Visible = false;
            }
        }

        public Boolean validation()
        {

            if(rblAlert.SelectedIndex == -1)
            {
                Master.ShowMessage("Select Alert On/Off", MenuMaster.MessageType.Error);
                return false;
            }
            if (rtbReason.Text == string.Empty)
            {
                Master.ShowMessage("Enter Reason", MenuMaster.MessageType.Error);
                return false;
            }
            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(validation())
                {
                    ObjSAP.System = rcbSystem.SelectedValue.ToString();
                    ObjSAP.Alert = rblAlert.SelectedValue.ToString();
                    ObjSAP.Remarks = rtbReason.Text.ToString();
                    ObjSAP.CreatedBy = Master.UserID;
                    ObjSAP.Optn = 0;
                    DataSet dsSAP;
                    dsSAP = new DataSet();
                    dsSAP = ObjSAP.Manage_Get_SAPDB_Alert();
                    if (dsSAP.Tables[0].Rows.Count > 0)
                    {
                        Master.ShowMessage("SAP Replication Alert " + ((rblAlert.SelectedValue.ToString() == "0") ? "ON" : "OFF") + " Successfully", MenuMaster.MessageType.Error);
                        clear();
                    }
                    RGRDSAPDBAlert.Rebind();
                }
            }
            catch (Exception ex)
            {
                Master.LogError(ex.Message, ex.ToString());
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void RGRDSAPDBAlert_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGRDSAPDBAlert.DataSource = LoadReport();
        }

        protected void RGRDSAPDBAlert_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void clear()
        {
            rcbSystem.SelectedIndex = 0;
            rtbReason.Text = string.Empty;
            rblAlert.SelectedIndex = -1;
        }

        protected DataTable LoadReport()
        {
            ObjSAP.Optn = 1;
            DataSet DsSAPDBAlertManage;
            DsSAPDBAlertManage = new DataSet();
            DsSAPDBAlertManage = ObjSAP.Manage_Get_SAPDB_Alert();
            if (DsSAPDBAlertManage.Tables[0].Rows.Count > 0)
            {
                //DivGrid.Visible = true;
                return DsSAPDBAlertManage.Tables[0];
            }

            return null;
        }

        protected void RGRDSAPDBAlert_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (item["Alert"].Text == "ON")
                {
                    item["Alert"].BackColor = Color.Green;
                    item["Alert"].ForeColor = Color.White;
                    item["Alert"].Font.Bold = true;
                }
                else
                {
                    item["Alert"].BackColor = Color.Red;
                    item["Alert"].ForeColor = Color.White;
                    item["Alert"].Font.Bold = true;
                }
            }
        }

        protected void RGRDSAPDBAlert_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem)
            {
                GridFilteringItem item = e.Item as GridFilteringItem;
                item["System"].HorizontalAlign = HorizontalAlign.Center;
                item["Alert"].HorizontalAlign = HorizontalAlign.Center;
                item["CreatedBy"].HorizontalAlign = HorizontalAlign.Center;
                item["CreatedOn"].HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }
}