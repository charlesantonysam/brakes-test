using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Configuration;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Globalization;
using SESWeb.wsSES;
using System.Net;
using System.Text;
using System.IO;
using System.Windows;
using Telerik.Windows.Documents.Core;
using Telerik.Windows.Documents.Fixed;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Editing.Tables;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;

namespace SESWeb.Transactions
{
    public partial class SESProcess : System.Web.UI.Page
    {
        ClsSES objSES = new ClsSES();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Master.UserID))
                {
                    Response.Redirect("~/error.aspx");
                }

                if (!IsPostBack)
                {
                    GetDashboardDetails();
                }
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SES - {0}", ex.Message), ex.ToString());
            }
        }

        protected void btnDashboardRefresh_Click(object sender, EventArgs e)
        {
            GetDashboardDetails();
        }
        private string GetUserRole()
        {
            try
            {
                if (string.IsNullOrEmpty(Master.UserID))
                {
                    Response.Redirect("~/error.aspx");
                }
                objSES.EmpNo = Master.UserID.ToString().Trim();
                objSES.ServiceEntryNo = lblServiceEntryNo.Text.ToString().Trim();
                DataSet _dsGetELUserRole = objSES.GetUserRole();
                return (_dsGetELUserRole.Tables[0].Rows[0]["Role"].ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void ClearData()
        {
            txtComments.Text = string.Empty;
            rdpInvoiceDate.Clear();
            rdpInvoiceDate.DateInput.Clear();
            rdpInvoiceReceiptDate.Clear();
            rdpInvoiceReceiptDate.DateInput.Clear();
            txtInvoiceValue.Text = string.Empty;
            rcbTeamMembers.Items.Clear();
            rcbTeamMembers.Text = "";
        }
        private void GetDashboardDetails()
        {
            ClearData();
            objSES.EmpNo = Master.UserID.ToString().Trim();
            objSES.Role = GetUserRole();

            DataSet _dsGetDashboardDetails = objSES.GetDashboardDetails();
            string _QAItems = string.Empty;

            bcSESActive.Visible = false;
            bcText.InnerText = string.Empty;
            lblAssignWorklowHeading.Text = string.Empty;

            libtnDashboardRefresh.Visible = true;

            pnlPending.Visible = false;
            pnlCompleted.Visible = false;
            pnlRejected.Visible = false;

            if (_dsGetDashboardDetails.Tables.Count > 0)
            {
                if (_dsGetDashboardDetails.Tables[0].Rows.Count > 0)
                {
                    _QAItems = GetActionableItemMarkup("Pending", "pending.png", _dsGetDashboardDetails.Tables[0].Rows.Count.ToString(), "0", "collapseOne", "pen", "Pending", "Completed", "Rejected");
                    pnlPending.Visible = true;

                    grdPending.DataSource = _dsGetDashboardDetails.Tables[0];
                    grdPending.DataBind();
                    if (objSES.Role == "Approver" || objSES.Role == "Final Approver")
                    {
                        libtnBulkApprove.Visible = true;
                        grdPending.Columns.FindByUniqueName("CheckCol").Visible = true;
                    }
                    else
                    {
                        libtnBulkApprove.Visible = false;
                        grdPending.Columns.FindByUniqueName("CheckCol").Visible = false;
                    }
                }
                if (_dsGetDashboardDetails.Tables[1].Rows.Count > 0)
                {
                    _QAItems += GetActionableItemMarkup("Completed", "completed.png", _dsGetDashboardDetails.Tables[1].Rows.Count.ToString(), "0", "collapse2", "com", "Completed", "Pending", "Rejected");
                    pnlCompleted.Visible = true;

                    grdCompleted.DataSource = _dsGetDashboardDetails.Tables[1];
                    grdCompleted.DataBind();
                }
                if (_dsGetDashboardDetails.Tables[2].Rows.Count > 0)
                {
                    _QAItems += GetActionableItemMarkup("Rejected", "no-commit.png", _dsGetDashboardDetails.Tables[2].Rows.Count.ToString(), "0", "collapse3", "pen", "Rejected", "Pending", "Completed");
                    pnlRejected.Visible = true;

                    grdRejected.DataSource = _dsGetDashboardDetails.Tables[2];
                    grdRejected.DataBind();
                }
                pnlVGP.InnerHtml = _QAItems;
                pnlDashboard.Visible = true;
                pnlSESProcess.Visible = false;
            }
            else
            {
                pnlPending.Visible = false;
                pnlCompleted.Visible = false;
                pnlRejected.Visible = false;
                pnlSESProcess.Visible = false;
                pnlDashboard.Visible = false;
            }
            pnlKPI.CssClass = "col-lg-12 col-md-12 col-sm-12 col-xs-12 nopad-right";
        }
        private string GetActionableItemMarkup(string ActionCaption,
                                               string ActionIcon,
                                               string ActionValue,
                                               string ActionMessageValue,
                                               string AccordionId,
                                               string MessageClass,
                                               string NavigationID,
                                               string ControlToHidden,
                                               string ControlToHidden1)
        {
            try
            {
                string actionableItemMarkup = "<div class='col-lg-4 col-md-4 col-sm-6 col-xs-12 pd-r'>";
                actionableItemMarkup += string.Format("<a style='cursor:pointer;' onclick=OpenDashboard('{0}','{1}','{2}')>", NavigationID, ControlToHidden, ControlToHidden1);
                actionableItemMarkup += string.Format(" <div class='project-box {0}'>", MessageClass);
                actionableItemMarkup += "<div class='pro-img'>";
                actionableItemMarkup += string.Format("<img src = '../Resources/Pixel/images/dashboard-icons/{0}' >", ActionIcon);
                actionableItemMarkup += "</div >";
                actionableItemMarkup += "<div style='cursor:pointer;' class='pro-cnt'>";
                actionableItemMarkup += string.Format("<h5>{0} <span>{1}</span> </h5>", ActionValue, ActionCaption);
                actionableItemMarkup += "</div>";
                if (!ActionMessageValue.Equals("0"))
                {
                    actionableItemMarkup += "<div class='notify-bx'>";
                    actionableItemMarkup += string.Format("<span>{0}</span>", ActionMessageValue);
                    actionableItemMarkup += "</div>";
                }

                actionableItemMarkup += "</div>";
                actionableItemMarkup += "</a>";
                actionableItemMarkup += "</div>";
                return actionableItemMarkup;
            }
            catch (Exception) { throw; }
        }
        private void DisableButtons()
        {
            libtnSave.Visible = false;
            libtnSubmit.Visible = false;
            libtnApprove.Visible = false;
            libtnReject.Visible = false;
            libtnFinalApprove.Visible = false;
            libtnUpdate.Visible = false;
            libtnTransferToSAP.Visible = false;
        }
        private void DisableInputs()
        {
            txtInvoiceValue.Visible = false;
            lblInvoiceValue.Visible = true;

            rdpInvoiceDate.Visible = false;
            lblInvoiceDate.Visible = true;

            rdpInvoiceReceiptDate.Visible = false;
            lblInvoiceReceiptDate.Visible = true;
        }
        private void FillDetails(string serviceentryno, string status)
        {
            try
            {
                objSES.ServiceEntryNo = serviceentryno;
                DataSet dsGetSESDetails = objSES.GetSESDetails();

                bcSESActive.Visible = true;
                bcText.InnerText = serviceentryno + " (" + status + ")";

                pnlDashboard.Visible = false;
                pnlSESProcess.Visible = true;

                if (status != "Pending")
                {
                    rtsSESProcess.SelectedIndex = 0;
                    rpvSESHeader.Selected = true;
                }

                grdSESDetails.DataSource = dsGetSESDetails.Tables[1];
                grdSESDetails.DataBind();

                if ((status == "Pending") && (GetUserRole() == "Preparer" || GetUserRole() == "Site Finance"))
                {
                    txtComments.Text = dsGetSESDetails.Tables[0].Rows[0]["Comments"].ToString();
                }
                LoadFiles(dsGetSESDetails.Tables[0].Rows[0]["LBLNI"].ToString());

                LoadgrdTeamMembers(dsGetSESDetails.Tables[0].Rows[0]["LBLNI"].ToString());

                LoadOtherFiles(dsGetSESDetails.Tables[0].Rows[0]["LBLNI"].ToString());

                if (dsGetSESDetails.Tables[2].Rows.Count > 0)
                {
                    grdWorkflowStatus.DataSource = dsGetSESDetails.Tables[2];
                    grdWorkflowStatus.DataBind();
                    rtsSESProcess.FindTabByText("Workflow Status").Visible = true;
                }
                else
                {
                    rtsSESProcess.FindTabByText("Workflow Status").Visible = false;
                    grdWorkflowStatus.DataSource = null;
                    grdWorkflowStatus.DataBind();
                }

                lblServiceEntryNo.Text = dsGetSESDetails.Tables[0].Rows[0]["LBLNI"].ToString();
                lblPostingDate.Text = dsGetSESDetails.Tables[0].Rows[0]["BUDAT"].ToString();
                lblVendorName.Text = dsGetSESDetails.Tables[0].Rows[0]["VendorName"].ToString();
                lblCreatedDate.Text = dsGetSESDetails.Tables[0].Rows[0]["ERDAT"].ToString();
                lblInvoiceNo.Text = dsGetSESDetails.Tables[0].Rows[0]["LBLNE"].ToString();

                lblServiceLocation.Text = dsGetSESDetails.Tables[0].Rows[0]["DLORT"].ToString();
                lblShortText.Text = dsGetSESDetails.Tables[0].Rows[0]["TXZ01"].ToString();
                lblDocumentHeaderText.Text = dsGetSESDetails.Tables[0].Rows[0]["BKTXT"].ToString();
                lblServicePeriod.Text = dsGetSESDetails.Tables[0].Rows[0]["ServicePeriod"].ToString();
                lblPONumber.Text = dsGetSESDetails.Tables[0].Rows[0]["EBELN"].ToString();
                lblItemNo.Text = dsGetSESDetails.Tables[0].Rows[0]["EBELP"].ToString();
                lblPOItemShortText.Text = dsGetSESDetails.Tables[0].Rows[0]["TXZ01P"].ToString();

                lblSGST.Text = dsGetSESDetails.Tables[0].Rows[0]["SGST"].ToString();
                lblCGST.Text = dsGetSESDetails.Tables[0].Rows[0]["CGST"].ToString();

                double BaseValue = 0.00;
                for (int i = 0; i < dsGetSESDetails.Tables[1].Rows.Count; i++)
                {
                    BaseValue = BaseValue + Convert.ToDouble(dsGetSESDetails.Tables[1].Rows[i]["NETWR"].ToString());
                }

                lblBaseValue.Text = string.Format("{0:n}", BaseValue).ToString();
                lblTotalTax.Text = dsGetSESDetails.Tables[0].Rows[0]["TOTALTAX_AMOUNT"].ToString();

                if (lblSGST.Text == "0.00" && lblCGST.Text == "0.00")
                {
                    divSGSTCGST.Visible = false;
                }
                else
                {
                    divSGSTCGST.Visible = true;
                }

                lblIGST.Text = dsGetSESDetails.Tables[0].Rows[0]["IGST"].ToString();

                if (lblIGST.Text == "0.00")
                {
                    divIGST.Visible = false;
                }
                else
                {
                    divIGST.Visible = true;
                }

                lblInvoiceValue.Text = dsGetSESDetails.Tables[0].Rows[0]["LWERT"].ToString();
                lblCurrency.Text = dsGetSESDetails.Tables[0].Rows[0]["WAERS"].ToString();

                txtInvoiceValue.Text = dsGetSESDetails.Tables[0].Rows[0]["LWERT"].ToString();

                if (dsGetSESDetails.Tables[0].Rows[0]["INVOICE_RECEIPT_DATE"].ToString() != "")
                {
                    DateTime invoicereceiptdate = DateTime.Parse(dsGetSESDetails.Tables[0].Rows[0]["INVOICE_RECEIPT_DATE"].ToString());
                    lblInvoiceReceiptDate.Text = invoicereceiptdate.ToString("dd/MM/yyyy");
                    rdpInvoiceReceiptDate.SelectedDate = invoicereceiptdate;
                }

                if (dsGetSESDetails.Tables[0].Rows[0]["BLDAT"].ToString() != "")
                {
                    DateTime invoicedate = DateTime.Parse(dsGetSESDetails.Tables[0].Rows[0]["BLDAT"].ToString());
                    lblInvoiceDate.Text = invoicedate.ToString("dd/MM/yyyy");
                    rdpInvoiceDate.SelectedDate = invoicedate;

                    rdpInvoiceReceiptDate.MinDate = invoicedate;
                }

                rdpInvoiceDate.MaxDate = DateTime.Now;

                rdpInvoiceReceiptDate.MaxDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SES - {0}", ex.Message), ex.ToString());
            }
        }
        protected void grdPending_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridDataItem _SelectedItem = (GridDataItem)this.grdPending.Items[this.grdPending.SelectedItems[0].ItemIndex];
                string ServiceEntryNo = _SelectedItem.OwnerTableView.DataKeyValues[_SelectedItem.ItemIndex]["LBLNI"].ToString();
                FillDetails(ServiceEntryNo, "Pending");

                RadTab tab = (RadTab)rtsSESProcess.FindTabByValue("2");
                tab.Text = "Approvers List";
                lblAssignWorklowHeading.Text = "Approvers List";

                RadTab tab5 = (RadTab)rtsSESProcess.FindTabByValue("5");
                tab5.Text = "Comments & Other Documents";
                tab5.Width = 220;
                lblCommentsHeading.Text = "Comments & Other Documents";

                DisableButtons();

                string Role = GetUserRole();
                if (Role == "Preparer")
                {
                    libtnSave.Visible = true;
                    libtnSubmit.Visible = true;

                    RadTab tabprep = (RadTab)rtsSESProcess.FindTabByValue("2");
                    tabprep.Text = "Assign Workflow";
                    lblAssignWorklowHeading.Text = "Assign Workflow";

                    grdTeamMembers.Columns.FindByUniqueName("DeleteColumn").Visible = true;
                    grdAttachments.Columns.FindByUniqueName("DeleteColumn").Visible = true;

                    pnlSelectApprover.Visible = true;

                    txtInvoiceValue.Visible = true;
                    lblInvoiceValue.Visible = false;

                    rdpInvoiceDate.Visible = false;
                    lblInvoiceDate.Visible = true;

                    rdpInvoiceReceiptDate.Visible = true;
                    lblInvoiceReceiptDate.Visible = false;

                    rtsSESProcess.FindTabByText("Workflow Status").Visible = false;
                }
                else if (Role == "Approver")
                {
                    libtnSave.Visible = true;
                    libtnApprove.Visible = true;
                    libtnReject.Visible = true;

                    DisableInputs();

                    rtsSESProcess.FindTabByText("Workflow Status").Visible = true;
                }
                else if (Role == "Final Approver")
                {
                    libtnSave.Visible = true;
                    libtnReject.Visible = true;
                    libtnFinalApprove.Visible = true;

                    DisableInputs();

                    rtsSESProcess.FindTabByText("Workflow Status").Visible = true;
                }
                else if (Role == "Site Finance")
                {
                    libtnReject.Visible = true;
                    libtnUpdate.Visible = true;
                    libtnTransferToSAP.Visible = true;

                    txtInvoiceValue.Visible = true;
                    lblInvoiceValue.Visible = false;

                    rdpInvoiceDate.Visible = true;
                    lblInvoiceDate.Visible = false;

                    rdpInvoiceReceiptDate.Visible = true;
                    lblInvoiceReceiptDate.Visible = false;

                    rtsSESProcess.FindTabByText("Workflow Status").Visible = true;
                }
                rtsSESProcess.FindTabByValue("5").Visible = true;
                pnlWorkflowComments.Visible = true;
                pnlFileUpload.Visible = true;
                grdOtherDocuments.Columns.FindByUniqueName("DeleteColumn").Visible = true;
            }
            catch (Exception ex)
            {
                Master.LogError(ex.Message, ex.ToString());
            }
        }

        protected void grdCompleted_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridDataItem _SelectedItem = (GridDataItem)this.grdCompleted.Items[this.grdCompleted.SelectedItems[0].ItemIndex];
                string ServiceEntryNo = _SelectedItem.OwnerTableView.DataKeyValues[_SelectedItem.ItemIndex]["LBLNI"].ToString();
                FillDetails(ServiceEntryNo, "Completed");

                RadTab tab = (RadTab)rtsSESProcess.FindTabByValue("2");
                tab.Text = "Approvers List";
                lblAssignWorklowHeading.Text = "Approvers List";

                DisableButtons();

                secContent.Style.Add("margin-top", "120px");

                pnlSelectApprover.Visible = false;

                grdTeamMembers.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                grdAttachments.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                grdOtherDocuments.Columns.FindByUniqueName("DeleteColumn").Visible = false;

                DisableInputs();

                if (grdOtherDocuments.Items.Count < 1)
                {
                    rtsSESProcess.FindTabByValue("5").Visible = false;
                    rpvComments.Visible = false;
                }
                else
                {
                    RadTab tab5 = (RadTab)rtsSESProcess.FindTabByValue("5");
                    tab5.Text = "Other Documents";
                    tab5.Width = 150;
                    lblCommentsHeading.Text = "Other Documents";

                    rtsSESProcess.FindTabByValue("5").Visible = true;
                    rpvComments.Visible = true;
                    pnlWorkflowComments.Visible = false;
                    pnlFileUpload.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Master.LogError(ex.Message, ex.ToString());
            }
        }

        protected void grdRejected_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridDataItem _SelectedItem = (GridDataItem)this.grdRejected.Items[this.grdRejected.SelectedItems[0].ItemIndex];
                string ServiceEntryNo = _SelectedItem.OwnerTableView.DataKeyValues[_SelectedItem.ItemIndex]["LBLNI"].ToString();
                FillDetails(ServiceEntryNo, "Rejected");

                DisableButtons();

                string Role = GetUserRole();
                if (Role == "Preparer")
                {
                    libtnSave.Visible = true;
                    libtnSubmit.Visible = true;

                    RadTab tabprep = (RadTab)rtsSESProcess.FindTabByValue("2");
                    tabprep.Text = "Assign Workflow";
                    lblAssignWorklowHeading.Text = "Assign Workflow";

                    RadTab tab5 = (RadTab)rtsSESProcess.FindTabByValue("5");
                    tab5.Text = "Comments & Other Documents";
                    tab5.Width = 220;
                    lblCommentsHeading.Text = "Comments & Other Documents";

                    rtsSESProcess.FindTabByValue("5").Visible = true;
                    pnlWorkflowComments.Visible = true;
                    pnlFileUpload.Visible = true;

                    pnlSelectApprover.Visible = true;

                    grdTeamMembers.Columns.FindByUniqueName("DeleteColumn").Visible = true;
                    grdAttachments.Columns.FindByUniqueName("DeleteColumn").Visible = true;
                    grdOtherDocuments.Columns.FindByUniqueName("DeleteColumn").Visible = true;

                    txtInvoiceValue.Visible = true;
                    lblInvoiceValue.Visible = false;

                    rdpInvoiceDate.Visible = false;
                    lblInvoiceDate.Visible = true;

                    rdpInvoiceReceiptDate.Visible = true;
                    lblInvoiceReceiptDate.Visible = false;
                }

                else
                {
                    RadTab tab = (RadTab)rtsSESProcess.FindTabByValue("2");
                    tab.Text = "Approvers List";
                    lblAssignWorklowHeading.Text = "Approvers List";

                    pnlSelectApprover.Visible = false;

                    grdTeamMembers.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                    grdAttachments.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                    grdOtherDocuments.Columns.FindByUniqueName("DeleteColumn").Visible = false;

                    secContent.Style.Add("margin-top", "120px");

                    DisableInputs();

                    if (grdOtherDocuments.Items.Count < 1)
                    {
                        rtsSESProcess.FindTabByValue("5").Visible = false;
                        rpvComments.Visible = false;
                    }
                    else
                    {
                        RadTab tab5 = (RadTab)rtsSESProcess.FindTabByValue("5");
                        tab5.Text = "Other Documents";
                        tab5.Width = 150;
                        lblCommentsHeading.Text = "Other Documents";

                        rtsSESProcess.FindTabByValue("5").Visible = true;
                        rpvComments.Visible = true;
                        pnlWorkflowComments.Visible = false;
                        pnlFileUpload.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Master.LogError(ex.Message, ex.ToString());
            }
        }

        protected bool validation()
        {
            if (txtInvoiceValue.Text == string.Empty)
            {
                Master.ShowMessage("Enter The Invoice Value", BI.MessageType.Error);
                rtsSESProcess.FindTabByText("SES Header").Selected = true;
                rpvSESHeader.Selected = true;
                txtInvoiceValue.Focus();
                return false;
            }
            if (rdpInvoiceReceiptDate.SelectedDate == null)
            {
                Master.ShowMessage("Enter The Invoice Receipt Date", BI.MessageType.Error);
                rtsSESProcess.FindTabByText("SES Header").Selected = true;
                rpvSESHeader.Selected = true;
                rdpInvoiceReceiptDate.Focus();
                return false;
            }
            if (txtComments.Text == string.Empty)
            {
                Master.ShowMessage("Enter The Comments", BI.MessageType.Error);
                rtsSESProcess.FindTabByValue("5").Selected = true;
                rpvComments.Selected = true;
                txtComments.Focus();
                return false;
            }
            return true;
        }
        protected bool FileDownloadValidation()
        {
            foreach (GridDataItem item in grdAttachments.MasterTableView.Items)
            {
                string IsSAPFile = item.GetDataKeyValue("IsSAPFile").ToString();
                string IsFinanceDownloaded = item.GetDataKeyValue("IsFinanceDownloaded").ToString();

                if (IsSAPFile == "True" && IsFinanceDownloaded == "False")
                {
                    Master.ShowMessage("View/Download The SES Top Sheet", BI.MessageType.Error);
                    rtsSESProcess.FindTabByText("SES Documents").Selected = true;
                    rpvAttachments.Selected = true;
                    return false;
                }
                if (IsSAPFile == "False" && IsFinanceDownloaded == "False")
                {
                    Master.ShowMessage("View/Download The Invoice Document", BI.MessageType.Error);
                    rtsSESProcess.FindTabByText("SES Documents").Selected = true;
                    rpvAttachments.Selected = true;
                    return false;
                }
            }
            return true;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Master.UserID))
                {
                    Response.Redirect("~/error.aspx");
                }
                objSES.ServiceEntryNo = lblServiceEntryNo.Text.ToString().Trim();
                objSES.EmpNo = Master.UserID.ToString();
                double invoicevalue = 0;
                double.TryParse(txtInvoiceValue.Text, out invoicevalue);
                objSES.InvoiceValue = invoicevalue;
                objSES.InvoiceReceiptDate = ((rdpInvoiceReceiptDate.SelectedDate != null && rdpInvoiceReceiptDate.SelectedDate.GetValueOrDefault() != DateTime.MinValue && rdpInvoiceReceiptDate.SelectedDate.GetValueOrDefault() != DateTime.MaxValue) ? Convert.ToDateTime(rdpInvoiceReceiptDate.SelectedDate).ToString("yyyyMMdd") : null);
                objSES.Comments = txtComments.Text.ToString().Trim();
                objSES.Mode = "Save";

                if (objSES.UpdateSESDetails())
                {
                    SaveAttachments(objSES.ServiceEntryNo);
                    SaveOtherDocuments(objSES.ServiceEntryNo);
                    this.Master.ShowMessage("SES Saved Sucessfully!! SES No : " + objSES.ServiceEntryNo, BI.MessageType.Success);
                    FillDetails(objSES.ServiceEntryNo, "Pending");
                }
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SES - {0}", ex.Message), ex.ToString());
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Master.UserID))
                {
                    Response.Redirect("~/error.aspx");
                }
                if (ruAttachment.UploadedFiles.Count == 0 && grdAttachments.Items.Count <= 1)
                {
                    Master.ShowMessage("Upload Invoice Document", BI.MessageType.Error);
                    rtsSESProcess.FindTabByText("SES Documents").Selected = true;
                    rpvAttachments.Selected = true;
                    ruAttachment.Focus();
                }
                else if (grdTeamMembers.Items.Count == 0)
                {
                    Master.ShowMessage("Add Approvers", BI.MessageType.Error);
                    rtsSESProcess.FindTabByText("Assign Workflow").Selected = true;
                    rpvAssignWorkflow.Selected = true;
                    rcbTeamMembers.Focus();
                }
                else
                {
                    if (validation())
                    {
                        objSES.ServiceEntryNo = lblServiceEntryNo.Text.ToString().Trim();
                        objSES.EmpNo = Master.UserID.ToString();
                        double invoicevalue = 0;
                        double.TryParse(txtInvoiceValue.Text, out invoicevalue);
                        objSES.InvoiceValue = invoicevalue;
                        objSES.InvoiceReceiptDate = ((rdpInvoiceReceiptDate.SelectedDate != null && rdpInvoiceReceiptDate.SelectedDate.GetValueOrDefault() != DateTime.MinValue) ? Convert.ToDateTime(rdpInvoiceReceiptDate.SelectedDate).ToString("yyyyMMdd") : null);
                        objSES.Comments = txtComments.Text.ToString().Trim();
                        objSES.Mode = "Submit";

                        if (objSES.UpdateSESDetails())
                        {
                            SaveAttachments(objSES.ServiceEntryNo);
                            SaveOtherDocuments(objSES.ServiceEntryNo);
                            objSES.SendMail(objSES.GetMailSendDetails());
                            this.Master.ShowMessage("SES Submitted Sucessfully!! SES No : " + objSES.ServiceEntryNo, BI.MessageType.Success);
                            GetDashboardDetails();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SES - {0}", ex.Message), ex.ToString());
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Master.UserID))
                {
                    Response.Redirect("~/error.aspx");
                }
                if (txtComments.Text == string.Empty)
                {
                    Master.ShowMessage("Enter The Comments", BI.MessageType.Error);
                    rtsSESProcess.FindTabByValue("5").Selected = true;
                    rpvComments.Selected = true;
                    txtComments.Focus();
                }
                else
                {
                    objSES.ServiceEntryNo = lblServiceEntryNo.Text.ToString().Trim();
                    objSES.EmpNo = Master.UserID.ToString();
                    objSES.Comments = txtComments.Text.ToString().Trim();
                    objSES.Mode = "Approve";

                    if (objSES.UpdateSESDetails())
                    {
                        SaveOtherDocuments(objSES.ServiceEntryNo);
                        objSES.SendMail(objSES.GetMailSendDetails());
                        this.Master.ShowMessage("SES Approved Sucessfully!! SES No : " + objSES.ServiceEntryNo, BI.MessageType.Success);
                        GetDashboardDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SES - {0}", ex.Message), ex.ToString());
            }
        }

        protected void btnFinalApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Master.UserID))
                {
                    Response.Redirect("~/error.aspx");
                }
                if (txtComments.Text == string.Empty)
                {
                    Master.ShowMessage("Enter The Comments", BI.MessageType.Error);
                    rtsSESProcess.FindTabByValue("5").Selected = true;
                    rpvComments.Selected = true;
                    txtComments.Focus();
                }
                else
                {
                    objSES.ServiceEntryNo = lblServiceEntryNo.Text.ToString().Trim();
                    objSES.EmpNo = Master.UserID.ToString();
                    objSES.Comments = txtComments.Text.ToString().Trim();
                    objSES.Mode = "Final Approve";

                    if (objSES.UpdateSESDetails())
                    {
                        SaveOtherDocuments(objSES.ServiceEntryNo);
                        objSES.SendMail(objSES.GetMailSendDetails());
                        this.Master.ShowMessage("SES Final Approved Sucessfully!! SES No : " + objSES.ServiceEntryNo, BI.MessageType.Success);
                        GetDashboardDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SES - {0}", ex.Message), ex.ToString());
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Master.UserID))
                {
                    Response.Redirect("~/error.aspx");
                }
                objSES.ServiceEntryNo = lblServiceEntryNo.Text.ToString().Trim();
                objSES.EmpNo = Master.UserID.ToString();
                double invoicevalue = 0;
                double.TryParse(txtInvoiceValue.Text, out invoicevalue);
                objSES.InvoiceValue = invoicevalue;
                objSES.InvoiceDate = ((rdpInvoiceDate.SelectedDate != null && rdpInvoiceDate.SelectedDate.GetValueOrDefault() != DateTime.MinValue) ? Convert.ToDateTime(rdpInvoiceDate.SelectedDate).ToString("yyyyMMdd") : null);
                objSES.InvoiceReceiptDate = ((rdpInvoiceReceiptDate.SelectedDate != null && rdpInvoiceReceiptDate.SelectedDate.GetValueOrDefault() != DateTime.MinValue) ? Convert.ToDateTime(rdpInvoiceReceiptDate.SelectedDate).ToString("yyyyMMdd") : null);
                objSES.Comments = txtComments.Text.ToString().Trim();
                objSES.Mode = "Update";

                if (objSES.UpdateSESDetails())
                {
                    SaveOtherDocuments(objSES.ServiceEntryNo);
                    this.Master.ShowMessage("SES Updated Sucessfully!! SES No : " + objSES.ServiceEntryNo, BI.MessageType.Success);
                    FillDetails(objSES.ServiceEntryNo, "Pending");
                }
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SES - {0}", ex.Message), ex.ToString());
            }
        }
        private MemoryStream GetPDFStream()
        {
            try
            {
                objSES.ServiceEntryNo = lblServiceEntryNo.Text;
                DataSet dsGetSESDetails = objSES.GetSESDetails();

                RadFixedDocument document = new RadFixedDocument();
                RadFixedDocumentEditor editor = new RadFixedDocumentEditor(document);
                Telerik.Windows.Documents.Fixed.Model.Editing.Tables.Table table;
                Telerik.Windows.Documents.Fixed.Model.Editing.Tables.TableRow row;
                Telerik.Windows.Documents.Fixed.Model.Editing.Tables.TableCell cell;
                Border border = new Border(0, new RgbColor(0, 0, 0));
                Block block;

                table = new Telerik.Windows.Documents.Fixed.Model.Editing.Tables.Table();
                table.LayoutType = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.TableLayoutType.FixedWidth;

                string strtitle = string.Empty;

                string heading = string.Empty;
                heading = "SES Workflow";
                block = new Block();
                block.InsertText(new System.Windows.Media.FontFamily("Arial"), FontStyles.Normal, FontWeights.UltraBlack, heading);
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center;
                editor.InsertBlock(block);

                block = new Block();
                block.SpacingAfter = 5;
                block.InsertText("___________________________________________________________________________________");
                editor.InsertBlock(block);


                row = table.Rows.AddTableRow();

                cell = row.Cells.AddTableCell();
                block = new Block();
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                block.InsertText(new System.Windows.Media.FontFamily("calibri"), FontStyles.Normal, FontWeights.SemiBold, "Plant");
                cell.Blocks.Add(block);
                cell.PreferredWidth = 150;
                cell.Padding = new Thickness(3);

                cell = row.Cells.AddTableCell();
                block = new Block();
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                block.InsertText(new System.Windows.Media.FontFamily("calibri"), ":  " + dsGetSESDetails.Tables[0].Rows[0]["PLANT"].ToString());
                cell.Blocks.Add(block);
                cell.PreferredWidth = 500;
                cell.ColumnSpan = 3;
                cell.Padding = new Thickness(3);

                row = table.Rows.AddTableRow();

                cell = row.Cells.AddTableCell();
                block = new Block();
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                block.InsertText(new System.Windows.Media.FontFamily("calibri"), FontStyles.Normal, FontWeights.SemiBold, "PO No");
                cell.Blocks.Add(block);
                cell.PreferredWidth = 150;
                cell.Padding = new Thickness(3);

                cell = row.Cells.AddTableCell();
                block = new Block();
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                block.InsertText(new System.Windows.Media.FontFamily("calibri"), ":  " + dsGetSESDetails.Tables[0].Rows[0]["EBELN"].ToString());
                cell.Blocks.Add(block);
                cell.PreferredWidth = 500;
                cell.ColumnSpan = 3;
                cell.Padding = new Thickness(3);

                row = table.Rows.AddTableRow();

                cell = row.Cells.AddTableCell();
                block = new Block();
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                block.InsertText(new System.Windows.Media.FontFamily("calibri"), FontStyles.Normal, FontWeights.SemiBold, "SES No");
                cell.Blocks.Add(block);
                cell.PreferredWidth = 150;
                cell.Padding = new Thickness(3);

                cell = row.Cells.AddTableCell();
                block = new Block();
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                block.InsertText(new System.Windows.Media.FontFamily("calibri"), ":  " + dsGetSESDetails.Tables[0].Rows[0]["LBLNI"].ToString());
                cell.Blocks.Add(block);
                cell.PreferredWidth = 500;
                cell.ColumnSpan = 3;
                cell.Padding = new Thickness(3);

                row = table.Rows.AddTableRow();

                cell = row.Cells.AddTableCell();
                block = new Block();
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                block.InsertText(new System.Windows.Media.FontFamily("calibri"), FontStyles.Normal, FontWeights.SemiBold, "Vendor Code & Name");
                cell.Blocks.Add(block);
                cell.PreferredWidth = 150;
                cell.Padding = new Thickness(3);

                cell = row.Cells.AddTableCell();
                block = new Block();
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                block.InsertText(new System.Windows.Media.FontFamily("calibri"), ":  " + dsGetSESDetails.Tables[0].Rows[0]["VendorName"].ToString());
                cell.Blocks.Add(block);
                cell.PreferredWidth = 500;
                cell.ColumnSpan = 3;
                cell.Padding = new Thickness(3);


                editor.InsertTable(table);


                // Workflow Details

                block = new Telerik.Windows.Documents.Fixed.Model.Editing.Block();
                block.SpacingAfter = 5;
                block.InsertText("___________________________________________________________________________________");
                editor.InsertBlock(block);

                block = new Telerik.Windows.Documents.Fixed.Model.Editing.Block();
                block.SpacingAfter = 5;
                block.InsertText("");
                editor.InsertBlock(block);

                string strActivityDetails = string.Empty;
                strActivityDetails = "Workflow Details";
                block = new Block();
                block.InsertText(new System.Windows.Media.FontFamily("Arial"), FontStyles.Normal, FontWeights.UltraBlack, strActivityDetails);
                editor.InsertBlock(block);

                block = new Telerik.Windows.Documents.Fixed.Model.Editing.Block();
                block.SpacingAfter = 5;
                block.InsertText("");
                editor.InsertBlock(block);

                table = new Telerik.Windows.Documents.Fixed.Model.Editing.Tables.Table();
                table.LayoutType = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.TableLayoutType.FixedWidth;
                table.DefaultCellProperties.Borders = new TableCellBorders(border, border, border, border);

                row = table.Rows.AddTableRow();

                cell = row.Cells.AddTableCell();
                block = new Block();
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                block.InsertText(new System.Windows.Media.FontFamily("calibri"), FontStyles.Normal, FontWeights.SemiBold, "S.No.");
                cell.Blocks.Add(block);
                cell.PreferredWidth = 50;
                cell.Padding = new Thickness(3);

                cell = row.Cells.AddTableCell();
                block = new Block();
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                block.InsertText(new System.Windows.Media.FontFamily("calibri"), FontStyles.Normal, FontWeights.SemiBold, "Employee");
                cell.Blocks.Add(block);
                cell.PreferredWidth = 200;
                cell.Padding = new Thickness(3);

                cell = row.Cells.AddTableCell();
                block = new Block();
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                block.InsertText(new System.Windows.Media.FontFamily("calibri"), FontStyles.Normal, FontWeights.SemiBold, "Designation");
                cell.Blocks.Add(block);
                cell.PreferredWidth = 170;
                cell.Padding = new Thickness(3);

                cell = row.Cells.AddTableCell();
                block = new Block();
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                block.InsertText(new System.Windows.Media.FontFamily("calibri"), FontStyles.Normal, FontWeights.SemiBold, "Date");
                cell.Blocks.Add(block);
                cell.PreferredWidth = 100;
                cell.Padding = new Thickness(3);

                cell = row.Cells.AddTableCell();
                block = new Block();
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                block.InsertText(new System.Windows.Media.FontFamily("calibri"), FontStyles.Normal, FontWeights.SemiBold, "Comments");
                cell.Blocks.Add(block);
                cell.PreferredWidth = 200;
                cell.Padding = new Thickness(3);

                cell = row.Cells.AddTableCell();
                block = new Block();
                block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                block.InsertText(new System.Windows.Media.FontFamily("calibri"), FontStyles.Normal, FontWeights.SemiBold, "Status");
                cell.Blocks.Add(block);
                cell.PreferredWidth = 130;
                cell.Padding = new Thickness(3);

                DataTable dt = dsGetSESDetails.Tables[3];

                foreach (DataRow item in dt.Rows)
                {
                    row = table.Rows.AddTableRow();

                    cell = row.Cells.AddTableCell();
                    block = new Block();
                    block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                    block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                    block.InsertText(new System.Windows.Media.FontFamily("calibri"), item["Sequence"].ToString());
                    cell.Blocks.Add(block);
                    cell.Padding = new Thickness(3);

                    cell = row.Cells.AddTableCell();
                    block = new Block();
                    block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                    block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                    block.InsertText(new System.Windows.Media.FontFamily("calibri"), item["UserID"].ToString());
                    cell.Blocks.Add(block);
                    cell.Padding = new Thickness(3);

                    cell = row.Cells.AddTableCell();
                    block = new Block();
                    block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                    block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                    block.InsertText(new System.Windows.Media.FontFamily("calibri"), item["Designation"].ToString());
                    cell.Blocks.Add(block);
                    cell.Padding = new Thickness(3);


                    string CreatedDate = "";
                    if (item["CreatedOn"].ToString() != null && item["CreatedOn"].ToString() != "")
                    {
                        CreatedDate = DateTime.Parse(item["CreatedOn"].ToString()).ToString("dd/MM/yyyy");
                    }

                    cell = row.Cells.AddTableCell();
                    block = new Block();
                    block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                    block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                    block.InsertText(new System.Windows.Media.FontFamily("calibri"), CreatedDate);
                    cell.Blocks.Add(block);
                    cell.Padding = new Thickness(3);

                    cell = row.Cells.AddTableCell();
                    block = new Block();
                    block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                    block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                    block.InsertText(new System.Windows.Media.FontFamily("calibri"), item["Comments"].ToString());
                    cell.Blocks.Add(block);
                    cell.Padding = new Thickness(3);

                    cell = row.Cells.AddTableCell();
                    block = new Block();
                    block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
                    block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
                    block.InsertText(new System.Windows.Media.FontFamily("calibri"), item["Flowstatus"].ToString());
                    cell.Blocks.Add(block);
                    cell.Padding = new Thickness(3);
                }
                editor.InsertTable(table);

                PdfFormatProvider formatProvider = new PdfFormatProvider();
                MemoryStream ms = new MemoryStream();
                formatProvider.Export(document, ms);
                return ms;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void SaveGeneratedFile(string ServiceEntryNo)
        {
            Boolean _ReturnMsgFileUploaded = false;

            MemoryStream mem = GetPDFStream();

            var containerName = ConfigurationManager.AppSettings.Get("AzureBlobContainer");
            string storageConnection = ConfigurationManager.AppSettings.Get("AzureBlobConnectionString");

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);


            string FileName = "WF-" + ServiceEntryNo + ".pdf";
            string AliasName = "WF-" + ServiceEntryNo + ".pdf";
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(ConfigurationManager.AppSettings.Get("BlobFolderName") + ServiceEntryNo + "/" + AliasName);

            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-10);
            sasConstraints.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(10);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Read;

            var sasBlobToken = cloudBlockBlob.GetSharedAccessSignature(sasConstraints);
            var BlobURL = cloudBlockBlob.Uri + sasBlobToken;
            CloudBlockBlob blob = new CloudBlockBlob(new Uri(BlobURL));

            mem.Position = 0;
            blob.UploadFromStream(mem);

            objSES.ServiceEntryNo = ServiceEntryNo;
            objSES.FileName = FileName;
            objSES.AliasName = AliasName;
            objSES.EmpNo = Master.UserID.ToString();

            _ReturnMsgFileUploaded = objSES.SaveFile();
        }
        protected void btnTransferToSAP_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Master.UserID))
                {
                    Response.Redirect("~/error.aspx");
                }
                if (rdpInvoiceDate.SelectedDate == null)
                {
                    Master.ShowMessage("Enter The Invoice Date", BI.MessageType.Error);
                    rtsSESProcess.FindTabByText("SES Header").Selected = true;
                    rpvSESHeader.Selected = true;
                    rdpInvoiceDate.Focus();
                }

                else
                {
                    if (validation() && FileDownloadValidation())
                    {
                        string ServiceEntryNo = lblServiceEntryNo.Text.ToString();

                        objSES.ServiceEntryNo = ServiceEntryNo;
                        objSES.EmpNo = Master.UserID.ToString();
                        objSES.Comments = txtComments.Text.ToString().Trim();
                        objSES.Mode = "Transfer To SAP";

                        if (objSES.UpdateSESDetails())
                        {
                            SaveOtherDocuments(ServiceEntryNo);

                            SaveGeneratedFile(ServiceEntryNo);
                        }

                        CommonMasters objCommonMasters = new CommonMasters();
                        string PIPIP = objCommonMasters.GetPIPIP();

                        string _UserName = System.Configuration.ConfigurationManager.AppSettings["SAPPILogin"];
                        string _Password = System.Configuration.ConfigurationManager.AppSettings["SAPPIPassword"];

                        int SAPPIConnectionTimeout = int.Parse(ConfigurationManager.AppSettings["SAPPIConnectionTimeout"]) * 60000;

                        // Proxy Object For SAP PI Connectivity                     
                        ServiceSheetFileDetailsFromWEBPORTAL_OutService objServiceProxy = new ServiceSheetFileDetailsFromWEBPORTAL_OutService();
                        objServiceProxy.Credentials = new NetworkCredential(_UserName, _Password);
                        objServiceProxy.PreAuthenticate = true;
                        objServiceProxy.Url = ConfigurationManager.AppSettings["urlSAPPISES"].Replace("{PISERVERIP}",PIPIP);

                        string InvoiceFile = "BI-" + ServiceEntryNo + ".pdf";
                        string WorkflowFile = "WF-" + ServiceEntryNo + ".pdf";

                        string[] Files = { InvoiceFile, WorkflowFile };

                        for (int i = 0; i < Files.Count(); i++)
                        {
                            ServiceSheetFileDetailsFromWEBPORTAL_Request objServiceRequest = new ServiceSheetFileDetailsFromWEBPORTAL_Request();
                            objServiceRequest.Request = new ServiceSheetFileDetailsFromWEBPORTAL_RequestRow[1];

                            ServiceSheetFileDetailsFromWEBPORTAL_RequestRow[] dra;
                            dra = new ServiceSheetFileDetailsFromWEBPORTAL_RequestRow[1];

                            ServiceSheetFileDetailsFromWEBPORTAL_RequestRow objServiceRequestRow = new ServiceSheetFileDetailsFromWEBPORTAL_RequestRow();
                            objServiceRequestRow.LBLNI = lblServiceEntryNo.Text.ToString().Trim();
                            objServiceRequestRow.LWERT = txtInvoiceValue.Text.ToString().Trim();
                            objServiceRequestRow.BLDAT = ((rdpInvoiceDate.SelectedDate != null && rdpInvoiceDate.SelectedDate.GetValueOrDefault() != DateTime.MinValue) ? Convert.ToDateTime(rdpInvoiceDate.SelectedDate).ToString("yyyyMMdd") : null);
                            objServiceRequestRow.INVOICE_RECEIPT_DATE = ((rdpInvoiceReceiptDate.SelectedDate != null && rdpInvoiceReceiptDate.SelectedDate.GetValueOrDefault() != DateTime.MinValue) ? Convert.ToDateTime(rdpInvoiceReceiptDate.SelectedDate).ToString("yyyyMMdd") : null);
                            objServiceRequestRow.FILENAME = Files[i];

                            byte[] bts = null;
                            string blobPath = ConfigurationManager.AppSettings.Get("BlobFolderName") + ServiceEntryNo + "/" + Files[i];
                            var containerName = ConfigurationManager.AppSettings.Get("AzureBlobContainer");
                            string storageConnection = ConfigurationManager.AppSettings.Get("AzureBlobConnectionString");
                            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
                            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
                            CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(containerName);
                            CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(blobPath);
                            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
                            sasConstraints.SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-10);
                            sasConstraints.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(10);
                            sasConstraints.Permissions = SharedAccessBlobPermissions.Read;

                            var sasBlobToken = blockBlob.GetSharedAccessSignature(sasConstraints);
                            var BlobURL = blockBlob.Uri + sasBlobToken;
                            CloudBlockBlob blob = new CloudBlockBlob(new Uri(BlobURL));

                            System.IO.MemoryStream memStream = new System.IO.MemoryStream();

                            if (blob.Exists())
                            {
                                blob.DownloadToStream(memStream);

                                if (memStream != null)
                                {
                                    bts = memStream.ToArray();
                                }
                            }

                            objServiceRequestRow.FILEDATA = bts;
                            dra[0] = objServiceRequestRow;
                            objServiceRequest.Request = dra;

                            ServiceSheetFileDetailsFromWEBPORTAL_Response objServiceResponse = new ServiceSheetFileDetailsFromWEBPORTAL_Response();
                            objServiceResponse = objServiceProxy.ServiceSheetFileDetailsFromWEBPORTAL_Out(objServiceRequest);

                            ServiceSheetFileDetailsFromWEBPORTAL_ResponseRow[] resprow = objServiceResponse.Response;

                            objSES.ServiceEntryNo = resprow[0].LBLNI.ToString();
                            objSES.InvoiceValue = double.Parse(resprow[0].LWERT.ToString());
                            objSES.InvoiceDate = resprow[0].BLDAT.ToString();
                            objSES.InvoiceReceiptDate = resprow[0].INVOICE_RECEIPT_DATE.ToString();
                            objSES.SAPIND = resprow[0].SAPIND.ToString();
                        }

                        objSES.Mode = "Response From SAP";

                        if (objSES.UpdateSESDetails())
                        {
                            objSES.SendMail(objSES.GetMailSendDetails());
                            this.Master.ShowMessage("SES Transferred To SAP Sucessfully!! SES No : " + ServiceEntryNo, BI.MessageType.Success);
                            GetDashboardDetails();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SES - {0}", ex.Message), ex.ToString());
            }
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Master.UserID))
                {
                    Response.Redirect("~/error.aspx");
                }
                if (txtComments.Text == string.Empty)
                {
                    Master.ShowMessage("Enter The Comments", BI.MessageType.Error);
                    rtsSESProcess.FindTabByValue("5").Selected = true;
                    rpvComments.Selected = true;
                    txtComments.Focus();
                }
                else
                {
                    objSES.ServiceEntryNo = lblServiceEntryNo.Text.ToString().Trim();
                    objSES.EmpNo = Master.UserID.ToString();
                    objSES.Comments = txtComments.Text.ToString().Trim();
                    objSES.Mode = "Reject";

                    if (objSES.UpdateSESDetails())
                    {
                        objSES.SendMail(objSES.GetMailSendDetails());
                        this.Master.ShowMessage("SES Rejected!! SES No : " + objSES.ServiceEntryNo, BI.MessageType.Success);
                        GetDashboardDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SES - {0}", ex.Message), ex.ToString());
            }
        }
        protected void SaveAttachments(string ServiceEntryNo)
        {
            Boolean _ReturnMsgFileUploaded = false;

            var containerName = ConfigurationManager.AppSettings.Get("AzureBlobContainer");
            string storageConnection = ConfigurationManager.AppSettings.Get("AzureBlobConnectionString");

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

            foreach (UploadedFile fileitem in ruAttachment.UploadedFiles)
            {
                string FileName = fileitem.FileName;
                string AliasName = "BI-" + ServiceEntryNo + ".pdf";
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(ConfigurationManager.AppSettings.Get("BlobFolderName") + ServiceEntryNo + "/" + AliasName);

                SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
                sasConstraints.SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-10);
                sasConstraints.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(10);
                sasConstraints.Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Read;

                var sasBlobToken = cloudBlockBlob.GetSharedAccessSignature(sasConstraints);
                var BlobURL = cloudBlockBlob.Uri + sasBlobToken;
                CloudBlockBlob blob = new CloudBlockBlob(new Uri(BlobURL));

                fileitem.InputStream.Position = 0;
                blob.UploadFromStream(fileitem.InputStream);

                objSES.ServiceEntryNo = ServiceEntryNo;
                objSES.FileName = FileName;
                objSES.AliasName = AliasName;
                objSES.EmpNo = Master.UserID.ToString();

                _ReturnMsgFileUploaded = objSES.SaveFile();
            }
        }
        protected void SaveOtherDocuments(string ServiceEntryNo)
        {
            Boolean _ReturnMsgFileUploaded = false;

            var containerName = ConfigurationManager.AppSettings.Get("AzureBlobContainer");
            string storageConnection = ConfigurationManager.AppSettings.Get("AzureBlobConnectionString");

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

            foreach (UploadedFile fileitem in rauOtherDocuments.UploadedFiles)
            {
                string FileName = fileitem.FileName;
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(ConfigurationManager.AppSettings.Get("BlobFolderName") + ServiceEntryNo + "/" + "Others" + "/" + FileName);

                SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
                sasConstraints.SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-10);
                sasConstraints.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(10);
                sasConstraints.Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Read;

                var sasBlobToken = cloudBlockBlob.GetSharedAccessSignature(sasConstraints);
                var BlobURL = cloudBlockBlob.Uri + sasBlobToken;
                CloudBlockBlob blob = new CloudBlockBlob(new Uri(BlobURL));

                fileitem.InputStream.Position = 0;
                blob.UploadFromStream(fileitem.InputStream);

                objSES.ServiceEntryNo = ServiceEntryNo;
                objSES.FileName = FileName;
                objSES.EmpNo = Master.UserID.ToString();

                _ReturnMsgFileUploaded = objSES.SaveOtherFile();
            }
        }
        protected void grdAttachments_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    ImageButton DelButton = (ImageButton)ditem["DeleteColumn"].Controls[0];
                    string IsSAPFile = ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["IsSAPFile"].ToString();
                    if (IsSAPFile == "True")
                    {
                        DelButton.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Master.LogError(ex.Message, ex.ToString());
            }
        }

        protected void grdAttachments_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    objSES.ServiceEntryNo = ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["LBLNI"].ToString();
                    objSES.AliasName = ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["AliasName"].ToString();
                    objSES.EmpNo = Master.UserID.ToString().Trim();

                    if (objSES.DeleteFile())
                    {
                        string fullpath = ConfigurationManager.AppSettings.Get("BlobFolderName") + objSES.ServiceEntryNo + "/" + objSES.AliasName;
                        var containerName = ConfigurationManager.AppSettings.Get("AzureBlobContainer");
                        string storageConnection = ConfigurationManager.AppSettings.Get("AzureBlobConnectionString");

                        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
                        CloudBlobClient _blobClient = cloudStorageAccount.CreateCloudBlobClient();
                        CloudBlobContainer _cloudBlobContainer = _blobClient.GetContainerReference(containerName);
                        CloudBlockBlob _blockBlob = _cloudBlobContainer.GetBlockBlobReference(fullpath);
                        //delete blob from container    
                        _blockBlob.Delete();

                        Master.ShowMessage("File Deleted", BI.MessageType.Success);

                        LoadFiles(ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["LBLNI"].ToString());
                    }

                    else
                    {
                        Master.ShowMessage("Error In File Deletion", BI.MessageType.Error);
                        return;
                    }
                }
                if (e.CommandName == "FileDownload")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    string _ServiceEntryNo = ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["LBLNI"].ToString();
                    string _GetAliasName = ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["AliasName"].ToString();

                    if (GetUserRole() == "Site Finance")
                    {
                        objSES.ServiceEntryNo = _ServiceEntryNo;
                        objSES.FileName = _GetAliasName;
                        objSES.EmpNo = Master.UserID.ToString().Trim();

                        objSES.UpdateFinanceFileDownloadStatus();

                        LoadFiles(_ServiceEntryNo);
                    }

                    byte[] bts = DownloadFileFromBlob(ConfigurationManager.AppSettings.Get("BlobFolderName") + _ServiceEntryNo + "/" + _GetAliasName);

                    if (bts != null)
                    {
                        Session["AliasName"] = _GetAliasName;
                        Session["InvoiceDocument"] = bts;

                        string _Script = "try{window.open('DownloadReport.aspx', 'popUpWindow', 'width=800,height=900,left=100,top=100,resizable=yes');$('#" + grdAttachments.ClientID + "').find('.grdAttachments').hide();}catch(e){}";
                        ClientScript.RegisterStartupScript(this.GetType(), "_PrintReport", _Script, true);
                    }

                    else
                    {
                        Master.ShowMessage("Error In File Download", BI.MessageType.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Master.LogError(ex.Message, ex.ToString());
            }
        }
        public byte[] DownloadFileFromBlob(string blobPath)
        {
            try
            {
                byte[] bts = null;
                var containerName = ConfigurationManager.AppSettings.Get("AzureBlobContainer");

                string storageConnection = ConfigurationManager.AppSettings.Get("AzureBlobConnectionString");
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
                CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(containerName);
                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(blobPath);

                SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
                sasConstraints.SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-10);
                sasConstraints.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(10);
                sasConstraints.Permissions = SharedAccessBlobPermissions.Read;

                var sasBlobToken = blockBlob.GetSharedAccessSignature(sasConstraints);
                var BlobURL = blockBlob.Uri + sasBlobToken;
                CloudBlockBlob blob = new CloudBlockBlob(new Uri(BlobURL));

                System.IO.MemoryStream memStream = new System.IO.MemoryStream();

                if (blob.Exists())
                {
                    blob.DownloadToStream(memStream);

                    if (memStream != null)
                    {
                        bts = memStream.ToArray();
                    }
                }

                return bts;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void LoadFiles(string ServiceEntryNo)
        {
            objSES.ServiceEntryNo = ServiceEntryNo;
            DataSet _dsGetFiles = objSES.GetFiles();

            if (_dsGetFiles.Tables[0].Rows.Count == 0)
            {
                grdAttachments.DataSource = string.Empty;
                grdAttachments.DataBind();
                pnlgrdAttachments.Visible = false;
                pnlInvoiceUpload.Visible = true;
            }
            else if (_dsGetFiles.Tables[0].Rows.Count == 1)
            {
                grdAttachments.DataSource = _dsGetFiles.Tables[0];
                grdAttachments.DataBind();
                pnlgrdAttachments.Visible = true;
                pnlInvoiceUpload.Visible = true;
            }
            else
            {
                grdAttachments.DataSource = _dsGetFiles.Tables[0];
                grdAttachments.DataBind();
                pnlgrdAttachments.Visible = true;
                pnlInvoiceUpload.Visible = false;
            }
        }
        private void LoadOtherFiles(string ServiceEntryNo)
        {
            objSES.ServiceEntryNo = ServiceEntryNo;
            DataSet dsGetOtherFiles = objSES.GetOtherFiles();

            if (dsGetOtherFiles.Tables[0].Rows.Count > 0)
            {
                grdOtherDocuments.DataSource = dsGetOtherFiles.Tables[0];
                grdOtherDocuments.DataBind();
                pnlOtherDocuments.Visible = true;
            }
            else
            {
                grdOtherDocuments.DataSource = string.Empty;
                grdOtherDocuments.DataBind();
                pnlOtherDocuments.Visible = false;
            }
        }
        protected void rcbTeamMembers_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            try
            {
                RadComboBox combo = (RadComboBox)sender;
                combo.Items.Clear();

                objSES.SearchString = e.Text;
                objSES.ServiceEntryNo = lblServiceEntryNo.Text.ToString();
                DataSet dsApprovalEmployee = objSES.GetEmployee();
                if (dsApprovalEmployee.Tables.Count > 0)
                {
                    if (dsApprovalEmployee.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dsApprovalEmployee.Tables[0].Rows)
                        {
                            RadComboBoxItem item = new RadComboBoxItem(row["Text"].ToString(), row["Value"].ToString());
                            combo.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            try
            {
                if (rcbTeamMembers.SelectedValue == "")
                {
                    Master.ShowMessage("Select A Member", BI.MessageType.Error);
                    return;
                }
                else
                {
                    objSES.ServiceEntryNo = lblServiceEntryNo.Text.ToString().Trim();
                    objSES.WorkflowMember = rcbTeamMembers.SelectedValue.ToString();
                    if (objSES.AddWorkflowMember())
                    {
                        this.Master.ShowMessage("Workflow Member Added Sucessfully!! Request No : " + lblServiceEntryNo.Text, BI.MessageType.Success);
                        LoadgrdTeamMembers(lblServiceEntryNo.Text.ToString().Trim());
                    }
                }
                rcbTeamMembers.Items.Clear();
                rcbTeamMembers.Text = "";
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SES - {0}", ex.Message), ex.ToString());
            }
        }

        protected void grdTeamMembers_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridItem _item = e.Item;
                    GridDataItem item = (GridDataItem)e.Item;
                    System.Web.UI.WebControls.Image imgCreatedBy = e.Item.FindControl("imgCreatedBy") as System.Web.UI.WebControls.Image;
                    string empno = item.GetDataKeyValue("EmployeeNo").ToString();
                    imgCreatedBy.ImageUrl = this.Master.GetUserAvatarURL(empno);
                }
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SES - {0}", ex.Message), ex.ToString());
            }
        }

        protected void grdTeamMembers_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    GridDataItem item = e.Item as GridDataItem;
                    objSES.ServiceEntryNo = lblServiceEntryNo.Text.ToString().Trim();
                    objSES.EmpNo = Master.UserID.ToString().Trim();
                    objSES.WorkflowMember = item.GetDataKeyValue("EmployeeNo").ToString().Trim();
                    objSES.Sequence = Convert.ToInt32(item.GetDataKeyValue("Sequence").ToString().Trim());

                    Boolean _ReturnDeleteStatus = objSES.DeleteWorkflowMember();
                    if (_ReturnDeleteStatus)
                    {
                        this.Master.ShowMessage("Workflow Member Deleted!! Request No : " + objSES.ServiceEntryNo.ToString(), BI.MessageType.Success);
                        LoadgrdTeamMembers(lblServiceEntryNo.Text.ToString().Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SES - {0}", ex.Message), ex.ToString());
            }
        }
        private void LoadgrdTeamMembers(string ServiceEntryNo)
        {
            objSES.ServiceEntryNo = ServiceEntryNo;
            DataSet _dsGetWorkflowMembers = objSES.GetWorkflowMembers();

            if (_dsGetWorkflowMembers.Tables[0].Rows.Count > 0)
            {
                grdTeamMembers.DataSource = _dsGetWorkflowMembers.Tables[0];
                grdTeamMembers.DataBind();
                pnlTeamMembers.Visible = true;
            }
            else
            {
                grdTeamMembers.DataSource = String.Empty;
                grdTeamMembers.DataBind();
                pnlTeamMembers.Visible = false;
            }
        }
        protected void grdWorkflowStatus_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = e.Item as GridDataItem;
                    System.Web.UI.WebControls.Image imgCreatedBy = e.Item.FindControl("imgCreatedBy") as System.Web.UI.WebControls.Image;
                    string empno = item.GetDataKeyValue("CreatedBy").ToString();
                    imgCreatedBy.ImageUrl = this.Master.GetUserAvatarURL(empno);

                    Label _lblStatus = item.FindControl("lblStatus") as Label;
                    if (item.GetDataKeyValue("Flowstatus").ToString() == "Pending")
                    {
                        _lblStatus.CssClass = "pnbs pen";
                        _lblStatus.Text = "<img src='../resources/pixel/images/icons/pen-1.png'>&nbsp; Pending";
                    }
                    else if (item.GetDataKeyValue("Flowstatus").ToString() == "Released")
                    {
                        _lblStatus.CssClass = "pnbs sub";
                        _lblStatus.Text = "<img src='../resources/pixel/images/icons/rel-1.png'>&nbsp; Released";
                    }
                    else if (item.GetDataKeyValue("Flowstatus").ToString() == "Approved")
                    {
                        _lblStatus.CssClass = "pnbs acc";
                        _lblStatus.Text = "<img src='../resources/pixel/images/icons/acc-1.png'>&nbsp; Approved";
                    }
                    else if (item.GetDataKeyValue("Flowstatus").ToString() == "Final Approved")
                    {
                        _lblStatus.CssClass = "pnbs acc changedocu";
                        _lblStatus.Text = "<img src='../resources/pixel/images/icons/acc-1.png'>&nbsp; Final Approved";
                    }
                    else if (item.GetDataKeyValue("Flowstatus").ToString() == "Transferred To SAP")
                    {
                        _lblStatus.CssClass = "pnbs acc changedocu";
                        _lblStatus.Text = "<img src='../resources/pixel/images/icons/rel-1.png'>&nbsp; Transferred To SAP";
                    }
                    else if (item.GetDataKeyValue("Flowstatus").ToString() == "Transferred From SAP")
                    {
                        _lblStatus.CssClass = "pnbs acc changedocu";
                        _lblStatus.Text = "<img src='../resources/pixel/images/icons/rel-1.png'>&nbsp; Transferred From SAP";
                    }
                    else if (item.GetDataKeyValue("Flowstatus").ToString() == "Rejected")
                    {
                        _lblStatus.CssClass = "pnbs acc deletedocu";
                        _lblStatus.Text = "<img src='../resources/pixel/images/icons/rej-1.png'>&nbsp; Rejected";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        protected bool CheckBoxValidation()
        {
            int checkedCount = 0;

            foreach (GridDataItem item in grdPending.MasterTableView.Items)
            {
                CheckBox chkRow = (CheckBox)item.FindControl("chkRow");

                if (chkRow.Checked == true)
                {
                    checkedCount = checkedCount++;
                    return true;
                }
            }

            if (checkedCount == 0)
            {
                Master.ShowMessage("Select The SES No.", BI.MessageType.Error);
                return false;
            }
            return true;
        }


        protected void btnBulkApprove_Click(object sender, EventArgs e)
        {
            try
            {
                bool RecordsAffected = false;

                if (string.IsNullOrEmpty(Master.UserID))
                {
                    Response.Redirect("~/error.aspx");
                }

                if (CheckBoxValidation())
                {
                    foreach (GridDataItem item in grdPending.MasterTableView.Items)
                    {
                        CheckBox chkRow = (CheckBox)item.FindControl("chkRow");

                        if (chkRow.Checked == true)
                        {
                            objSES.ServiceEntryNo = item.GetDataKeyValue("LBLNI").ToString();
                            objSES.EmpNo = Master.UserID.ToString();

                            DataSet dsGetBulkApprovalMode = objSES.GetBulkApprovalMode();
                            objSES.Mode = dsGetBulkApprovalMode.Tables[0].Rows[0]["Mode"].ToString();

                            objSES.Comments = "";

                            if (objSES.UpdateSESDetails())
                            {
                                objSES.SendMail(objSES.GetMailSendDetails());
                                RecordsAffected = true;
                            }
                        }
                    }
                }

                if (RecordsAffected)
                {
                    this.Master.ShowMessage("SES Bulk Approved Sucessfully!!", BI.MessageType.Success);
                    GetDashboardDetails();
                }
            }
            catch (Exception ex)
            {
                this.Master.LogError(string.Format("SES - {0}", ex.Message), ex.ToString());
            }
        }

        protected void grdPending_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem hitem = (GridHeaderItem)e.Item;
                CheckBox chk1 = (CheckBox)hitem.FindControl("chkAll");
                hdnCheckAll.Value = chk1.ClientID.ToString();
            }
        }

        protected void grdOtherDocuments_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    objSES.ServiceEntryNo = ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["LBLNI"].ToString();
                    objSES.SNo = Convert.ToInt32(ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["SNo"].ToString());
                    objSES.FileName = ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["FileName"].ToString();
                    objSES.EmpNo = Master.UserID.ToString().Trim();

                    if (objSES.DeleteOtherFile())
                    {
                        string fullpath = ConfigurationManager.AppSettings.Get("BlobFolderName") + objSES.ServiceEntryNo + "/" + "Others" + "/" + objSES.FileName;
                        var containerName = ConfigurationManager.AppSettings.Get("AzureBlobContainer");
                        string storageConnection = ConfigurationManager.AppSettings.Get("AzureBlobConnectionString");

                        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
                        CloudBlobClient _blobClient = cloudStorageAccount.CreateCloudBlobClient();
                        CloudBlobContainer _cloudBlobContainer = _blobClient.GetContainerReference(containerName);
                        CloudBlockBlob _blockBlob = _cloudBlobContainer.GetBlockBlobReference(fullpath);
                        //delete blob from container    
                        _blockBlob.Delete();

                        Master.ShowMessage("File Deleted", BI.MessageType.Success);

                        LoadOtherFiles(ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["LBLNI"].ToString());
                    }

                    else
                    {
                        Master.ShowMessage("Error In File Deletion", BI.MessageType.Error);
                        return;
                    }
                }
                if (e.CommandName == "FileDownload")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    string _ServiceEntryNo = ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["LBLNI"].ToString();
                    string _GetFileName = ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["FileName"].ToString();

                    byte[] bts = DownloadFileFromBlob(ConfigurationManager.AppSettings.Get("BlobFolderName") + _ServiceEntryNo + "/" + "Others" + "/" + _GetFileName);

                    if (bts != null)
                    {
                        Session["FileName"] = _GetFileName;
                        Session["InvoiceDocument"] = bts;

                        string _Script = "try{window.open('DownloadReport.aspx', 'popUpWindow', 'width=800,height=900,left=100,top=100,resizable=yes');$('#" + grdAttachments.ClientID + "').find('.grdAttachments').hide();}catch(e){}";
                        ClientScript.RegisterStartupScript(this.GetType(), "_PrintReport", _Script, true);
                    }

                    else
                    {
                        Master.ShowMessage("Error In File Download", BI.MessageType.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Master.LogError(ex.Message, ex.ToString());
            }

        }

        protected void grdOtherDocuments_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    ImageButton DeleteButton = (ImageButton)ditem["DeleteColumn"].Controls[0];
                    string UploadedBy = ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["UploadedBy"].ToString();
                    string UserID = Master.UserID.ToString();
                    if (UploadedBy == UserID)
                    {
                        DeleteButton.Visible = true;
                    }
                    else
                    {
                        DeleteButton.Visible = false;
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