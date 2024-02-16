<%@ Page Title="" Language="C#" MasterPageFile="~/Transactions/BIApplication.master" AutoEventWireup="true" CodeBehind="SESProcess.aspx.cs" Inherits="SESWeb.Transactions.SESProcess" %>

<%@ MasterType VirtualPath="~/Transactions/BIApplication.master" %>

<asp:Content ID="pgBreadCrumb" ContentPlaceHolderID="cphBreadCrumb" runat="server">
    <li class="breadcrumbbutton active" id="bcSESActive" runat="server" visible="false">
        <img src="../Resources/PIXEL/images/icons/fileview.png">
        <span>
            <h5 id="bcText" runat="server" />
        </span>
    </li>
</asp:Content>

<asp:Content ID="pgContent" ContentPlaceHolderID="cphPagecontent" runat="server">
    <asp:Panel runat="server" ID="pnlDashboard" Width="100%" Height="100%" CssClass="margin-0 padding-0" Visible="true">
        <section class="section main-btns">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <asp:Panel runat="server" Style="display: inline-block" ID="pnlDashboardIndicator">
                            <div class="">
                                <ul>
                                    <li>
                                        <telerik:RadButton runat="server" Skin="Bootstrap" ID="btnDashboardRefresh" Text="Refresh" AccessKey="R"
                                            ToolTip="Refresh (Alt+R)" OnClick="btnDashboardRefresh_Click" OnClientClicked="ShowCustomLoading">
                                            <Icon PrimaryIconUrl="~/Resources/Pixel/images/btn-icons/refresh.png"
                                                PrimaryIconLeft="15px" PrimaryIconTop="5px" PrimaryIconHeight="24px" PrimaryIconWidth="24px"></Icon>
                                        </telerik:RadButton>
                                    </li>
                                    <li>
                                        <telerik:RadButton runat="server" Skin="Bootstrap" ID="btnCreate" Text="Create" AccessKey="N" Visible="false"
                                            ToolTip="Create (Alt+N)" OnClick="btnCreate_Click" OnClientClicked="ShowCustomLoading">
                                            <Icon PrimaryIconUrl="~/Resources/Pixel/images/btn-icons/new-project.png"
                                                PrimaryIconLeft="15px" PrimaryIconTop="5px" PrimaryIconHeight="24px" PrimaryIconWidth="24px"></Icon>
                                        </telerik:RadButton>
                                    </li>
                                </ul>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </section>

        <section class="section tabs-projects clearfix">
            <div class="container-fluid">
                <div class="row">

                    <asp:Panel class="col-lg-6 col-md-12 col-sm-12 col-xs-12 nopad-right" ID="pnlKPI" runat="server">

                        <div runat="server" id="pnlVGP"></div>

                    </asp:Panel>
                </div>
            </div>
        </section>
        <section class="section projcts-functions hme">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">

                            <div id="Pending" style="display: none; width: 100% !important;">
                                <asp:Panel class="panel panel-default" runat="server" ClientIDMode="Static" ID="pnlPending">
                                    <div class="panel-heading" role="tab" id="headingOne">
                                        <h4 class="panel-title">
                                            <a class="" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">Pending Task</a>
                                        </h4>
                                    </div>
                                    <div id="collapseOne" class="panel-collapse collapse show" role="tabpanel" aria-labelledby="headingOne">
                                        <div class="panel-body table-responsive">
                                            <telerik:RadGrid runat="server" ID="grdPending" Skin="Metro" RenderMode="Auto" AutoGenerateColumns="false"
                                                AllowMultiRowSelection="false" GroupingSettings-CaseSensitive="false"
                                                GridLines="None"
                                                OnSelectedIndexChanged="grdPending_SelectedIndexChanged"
                                                AllowPaging="true" PageSize="100" AllowFilteringByColumn="false" AllowSorting="false">
                                                <ClientSettings
                                                    Scrolling-AllowScroll="true"
                                                    Scrolling-SaveScrollPosition="true"
                                                    Scrolling-UseStaticHeaders="true"
                                                    Scrolling-FrozenColumnsCount="4"
                                                    EnableRowHoverStyle="true"
                                                    Selecting-AllowRowSelect="true"
                                                    EnablePostBackOnRowClick="true">
                                                    <Resizing AllowResizeToFit="true" AllowColumnResize="true" />
                                                </ClientSettings>
                                                <MasterTableView DataKeyNames="LBLNI" NoDetailRecordsText="" NoMasterRecordsText="">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="LBLNI" HeaderText="SES No." HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VendorName" HeaderText="Vendor Name" HeaderStyle-Width="300px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EBELN" HeaderText="PO No." HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EBELP" HeaderText="Item No." HeaderStyle-Width="70px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ServicePeriod" HeaderText="Service Period" HeaderStyle-Width="180px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="LBLNE" HeaderText="Invoice No." HeaderStyle-Width="130px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="BLDAT" HeaderText="Invoice Date" HeaderStyle-Width="100px" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="LWERT" HeaderText="Invoice Value" HeaderStyle-Width="100px" DataFormatString="{0:n}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="WAERS" HeaderText="Currency" HeaderStyle-Width="70px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CreatedBy" HeaderText="Created By" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                    </Columns>
                                                    <ItemStyle Wrap="false" />
                                                    <AlternatingItemStyle Wrap="false" />
                                                    <HeaderStyle Wrap="false" Font-Bold="true" />
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div id="Completed" style="display: none; width: 100% !important;">
                                <asp:Panel class="panel panel-default" runat="server" ClientIDMode="Static" ID="pnlCompleted">
                                    <div class="panel-heading" role="tab" id="heading2">
                                        <h4 class="panel-title">
                                            <a class="" data-toggle="collapse" data-parent="#accordion" href="#collapse2" aria-expanded="false" aria-controls="collapse2">Completed Task</a>
                                        </h4>
                                    </div>
                                    <div id="collapse2" class="panel-collapse collapse show" role="tabpanel" aria-labelledby="heading2">
                                        <div class="panel-body table-responsive">
                                            <telerik:RadGrid runat="server" ID="grdCompleted" Skin="Metro" RenderMode="Auto" AutoGenerateColumns="false"
                                                AllowMultiRowSelection="false" GroupingSettings-CaseSensitive="false"
                                                GridLines="None"
                                                OnSelectedIndexChanged="grdCompleted_SelectedIndexChanged"
                                                AllowPaging="true" PageSize="100"
                                                AllowFilteringByColumn="false" AllowSorting="false">
                                                <ClientSettings
                                                    Scrolling-AllowScroll="true"
                                                    Scrolling-SaveScrollPosition="true"
                                                    Scrolling-UseStaticHeaders="true"
                                                    Scrolling-FrozenColumnsCount="4"
                                                    EnableRowHoverStyle="true"
                                                    Selecting-AllowRowSelect="true"
                                                    EnablePostBackOnRowClick="true">
                                                    <Resizing AllowResizeToFit="true" AllowColumnResize="true" />
                                                </ClientSettings>
                                                <MasterTableView DataKeyNames="LBLNI" NoDetailRecordsText="" NoMasterRecordsText="">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="LBLNI" HeaderText="SES No." HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VendorName" HeaderText="Vendor Name" HeaderStyle-Width="300px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EBELN" HeaderText="PO No." HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EBELP" HeaderText="Item No." HeaderStyle-Width="70px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ServicePeriod" HeaderText="Service Period" HeaderStyle-Width="180px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="LBLNE" HeaderText="Invoice No." HeaderStyle-Width="130px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="BLDAT" HeaderText="Invoice Date" HeaderStyle-Width="100px" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="LWERT" HeaderText="Invoice Value" HeaderStyle-Width="100px" DataFormatString="{0:n}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="WAERS" HeaderText="Currency" HeaderStyle-Width="70px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CreatedBy" HeaderText="Created By" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                    </Columns>
                                                    <ItemStyle Wrap="false" />
                                                    <AlternatingItemStyle Wrap="false" />
                                                    <HeaderStyle Wrap="false" Font-Bold="true" />
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div id="Rejected" style="display: none; width: 100% !important;">
                                <asp:Panel class="panel panel-default" runat="server" ClientIDMode="Static" ID="pnlRejected">
                                    <div class="panel-heading" role="tab" id="heading3">
                                        <h4 class="panel-title">
                                            <a class="" data-toggle="collapse" data-parent="#accordion" href="#collapse3" aria-expanded="false" aria-controls="collapse3">Rejected</a>
                                        </h4>
                                    </div>
                                    <div id="collapse3" class="panel-collapse collapse show" role="tabpanel" aria-labelledby="heading3">
                                        <div class="panel-body table-responsive">
                                            <telerik:RadGrid runat="server" ID="grdRejected" Skin="Metro" RenderMode="Auto" AutoGenerateColumns="false"
                                                AllowMultiRowSelection="false" GroupingSettings-CaseSensitive="false"
                                                GridLines="None"
                                                OnSelectedIndexChanged="grdRejected_SelectedIndexChanged"
                                                AllowPaging="true" PageSize="100"
                                                AllowFilteringByColumn="false"
                                                AllowSorting="false">
                                                <ClientSettings
                                                    Scrolling-AllowScroll="true"
                                                    Scrolling-SaveScrollPosition="true"
                                                    Scrolling-UseStaticHeaders="true"
                                                    Scrolling-FrozenColumnsCount="4"
                                                    EnableRowHoverStyle="true"
                                                    Selecting-AllowRowSelect="true"
                                                    EnablePostBackOnRowClick="true">
                                                    <Resizing AllowResizeToFit="true" AllowColumnResize="true" />
                                                </ClientSettings>
                                                <MasterTableView DataKeyNames="LBLNI" NoDetailRecordsText="" NoMasterRecordsText="">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="LBLNI" HeaderText="SES No." HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VendorName" HeaderText="Vendor Name" HeaderStyle-Width="300px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EBELN" HeaderText="PO No." HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EBELP" HeaderText="Item No." HeaderStyle-Width="70px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ServicePeriod" HeaderText="Service Period" HeaderStyle-Width="180px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="LBLNE" HeaderText="Invoice No." HeaderStyle-Width="130px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="BLDAT" HeaderText="Invoice Date" HeaderStyle-Width="100px" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="LWERT" HeaderText="Invoice Value" HeaderStyle-Width="100px" DataFormatString="{0:n}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="WAERS" HeaderText="Currency" HeaderStyle-Width="70px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CreatedBy" HeaderText="Created By" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                    </Columns>
                                                    <ItemStyle Wrap="false" />
                                                    <AlternatingItemStyle Wrap="false" />
                                                    <HeaderStyle Wrap="false" Font-Bold="true" />
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlSESProcess" Visible="true">
        <section class="section main-btns">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div>
                            <ul>
                                <li runat="server" id="libtnSave" visible="false">
                                    <telerik:RadButton runat="server" Skin="Bootstrap" ID="btnSave" Text="Save" AccessKey="s"
                                        ToolTip="Save" OnClick="btnSave_Click" OnClientClicked="ShowCustomLoading">
                                        <Icon PrimaryIconUrl="~/Resources/Pixel/images/icons/save.png" PrimaryIconTop="5px" PrimaryIconLeft="15px"
                                            PrimaryIconHeight="24px" PrimaryIconWidth="24px" />
                                    </telerik:RadButton>
                                </li>
                                <li runat="server" id="libtnSubmit" visible="false">
                                    <telerik:RadButton runat="server" Skin="Bootstrap" ID="btnSubmit" Text="Submit"
                                        ToolTip="Submit" OnClick="btnSubmit_Click" OnClientClicking="ConfirmSubmit" OnClientClicked="ShowCustomLoading">
                                        <Icon PrimaryIconUrl="~/Resources/Pixel/images/icons/submit.png" PrimaryIconTop="5px" PrimaryIconLeft="15px"
                                            PrimaryIconHeight="24px" PrimaryIconWidth="24px" />
                                    </telerik:RadButton>
                                </li>
                                <li runat="server" id="libtnApprove" visible="false">
                                    <telerik:RadButton runat="server" Skin="Bootstrap" ID="btnApprove" Text="Approve" AccessKey="a"
                                        ToolTip="Approve" OnClick="btnApprove_Click" OnClientClicking="ConfirmApprove" OnClientClicked="ShowCustomLoading">
                                        <Icon PrimaryIconUrl="~/Resources/Pixel/images/icons/approved.png" PrimaryIconTop="7px" PrimaryIconLeft="15px"
                                            PrimaryIconHeight="24px" PrimaryIconWidth="24px" />
                                    </telerik:RadButton>
                                </li>
                                <li runat="server" id="libtnFinalApprove" visible="false">
                                    <telerik:RadButton runat="server" Skin="Bootstrap" ID="btnFinalApprove" Text="Final Approve"
                                        ToolTip="Final Approve" OnClick="btnFinalApprove_Click" OnClientClicking="ConfirmApprove" OnClientClicked="ShowCustomLoading">
                                        <Icon PrimaryIconUrl="~/Resources/Pixel/images/icons/approved.png" PrimaryIconTop="7px" PrimaryIconLeft="15px"
                                            PrimaryIconHeight="24px" PrimaryIconWidth="24px" />
                                    </telerik:RadButton>
                                </li>
                                <li runat="server" id="libtnUpdate" visible="false">
                                    <telerik:RadButton runat="server" Skin="Bootstrap" ID="btnUpdate" Text="Update"
                                        ToolTip="Update" OnClick="btnUpdate_Click" OnClientClicked="ShowCustomLoading">
                                        <Icon PrimaryIconUrl="~/Resources/Pixel/images/icons/save.png" PrimaryIconTop="5px" PrimaryIconLeft="15px"
                                            PrimaryIconHeight="24px" PrimaryIconWidth="24px" />
                                    </telerik:RadButton>
                                </li>
                                <li runat="server" id="libtnTransferToSAP" visible="false">
                                    <telerik:RadButton runat="server" Skin="Bootstrap" ID="btnTransferToSAP" Text="Transfer To SAP"
                                        ToolTip="Transfer To SAP" OnClick="btnTransferToSAP_Click" OnClientClicking="ConfirmTransfer" OnClientClicked="ShowCustomLoading">
                                        <Icon PrimaryIconUrl="../Resources/Pixel/images/icons/submit.png"
                                            PrimaryIconLeft="15px" PrimaryIconTop="5px" PrimaryIconHeight="24px" PrimaryIconWidth="24px"></Icon>
                                    </telerik:RadButton>
                                </li>
                                <li runat="server" id="libtnReject" visible="false">
                                    <telerik:RadButton runat="server" Skin="Bootstrap" ID="btnReject" Text="Reject"
                                        ToolTip="Reject" OnClick="btnReject_Click" OnClientClicking="ConfirmReject" OnClientClicked="ShowCustomLoading">
                                        <Icon PrimaryIconUrl="../Resources/Pixel/images/icons/cancel.png"
                                            PrimaryIconLeft="15px" PrimaryIconTop="5px" PrimaryIconHeight="24px" PrimaryIconWidth="24px"></Icon>
                                    </telerik:RadButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section class="section tabs-projects clearfix">
            <div class="container-fluid">
                <div class="row " style="padding: 0px 15px;">
                    <div class="panel panel-default col-lg-12 col-md-12 col-sm-12 col-xs-12 nopad" style="margin-bottom: 0px; border-bottom-right-radius: 0px; border-bottom-left-radius: 0px;">
                        <div class="panel-heading">
                            <h4 class="panel-title" style="padding-left: 15px; padding-right: 15px;">Service Entry Sheet</h4>
                        </div>
                        <div class="panel-body entry-form" runat="server" style="font-family: 'lato'; font-weight: normal; font-style: normal; font-size: 15px; color: #545454;">

                            <telerik:RadTabStrip RenderMode="Mobile" runat="server" ID="rtsSESProcess" MultiPageID="rmpSESProcess" SelectedIndex="0" Skin="Metro">
                                <Tabs>
                                    <telerik:RadTab Text="SES Header" Value="0" Width="150px" PageViewID="rpvSESHeader" Height="20px" Selected="true" TabIndex="0"
                                        CssClass="tab DefaultBGSetting" SelectedCssClass="selectedTab" HoveredCssClass="hoveredTab">
                                    </telerik:RadTab>

                                    <telerik:RadTab Text="SES Details" Value="1" Width="150px" PageViewID="rpvSESDetails" Height="20px" TabIndex="1"
                                        CssClass="tab DefaultBGSetting" SelectedCssClass="selectedTab" HoveredCssClass="hoveredTab">
                                    </telerik:RadTab>

                                    <telerik:RadTab Text="" Value="2" Width="150px" PageViewID="rpvAssignWorkflow" Height="20px" TabIndex="2"
                                        CssClass="tab DefaultBGSetting" SelectedCssClass="selectedTab" HoveredCssClass="hoveredTab">
                                    </telerik:RadTab>

                                    <telerik:RadTab Text="Invoice Document" Value="3" Width="150px" PageViewID="rpvAttachments" Height="20px" TabIndex="3"
                                        CssClass="tab DefaultBGSetting" SelectedCssClass="selectedTab" HoveredCssClass="hoveredTab">
                                    </telerik:RadTab>

                                    <telerik:RadTab Text="Workflow Status" Value="4" Width="150px" PageViewID="rpvWorkflowStatus" Height="20px" TabIndex="4"
                                        CssClass="tab DefaultBGSetting" SelectedCssClass="selectedTab" HoveredCssClass="hoveredTab">
                                    </telerik:RadTab>

                                    <telerik:RadTab Text="Comments" Value="5" Width="150px" PageViewID="rpvComments" Height="20px" TabIndex="5"
                                        CssClass="tab DefaultBGSetting" SelectedCssClass="selectedTab" HoveredCssClass="hoveredTab">
                                    </telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="rmpSESProcess" SelectedIndex="0">

                                <telerik:RadPageView runat="server" ID="rpvSESHeader">
                                    <asp:Panel ID="pnlSESHeader" runat="server">
                                        <div class="panel-heading">
                                            <h4 class="panel-title" style="padding-left: 15px; padding-right: 15px;">SES Header</h4>
                                        </div>
                                        <div class="row " style="padding: 0px 15px; padding-right: 15px;">
                                            <div class="panel panel-default col-lg-12 col-md-12 col-sm-12 col-xs-12 nopad" style="margin-bottom: 0px; border-bottom-right-radius: 0px; border-bottom-left-radius: 0px;">
                                                <div class="panel-body entry-form" runat="server" style="font-family: 'lato'; font-weight: normal; font-style: normal; font-size: 15px; color: #545454;">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="form-group">
                                                                <label style="width: 200px;">Service Entry No.</label>
                                                                <asp:Label ID="lblServiceEntryNo" runat="server" Font-Bold="true" Text="" Width="250px" Visible="true"></asp:Label>
                                                                <label style="width: 200px; padding-left: 20px;">Vendor Name</label>
                                                                <asp:Label ID="lblVendorName" runat="server" Font-Bold="true" Text="" Width="400px"></asp:Label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label style="width: 200px;">PO Number</label>
                                                                <asp:Label ID="lblPONumber" runat="server" Font-Bold="true" Text="" Width="250px"></asp:Label>
                                                                <label style="width: 200px; padding-left: 20px;">Item No</label>
                                                                <asp:Label ID="lblItemNo" runat="server" Font-Bold="true" Text="" Width="250px"></asp:Label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label style="width: 200px;">PO Item Short Text</label>
                                                                <asp:Label ID="lblPOItemShortText" runat="server" Font-Bold="true" Text="" Width="250px"></asp:Label>
                                                                <label style="width: 200px; padding-left: 20px;">Short Text</label>
                                                                <asp:Label ID="lblShortText" runat="server" Font-Bold="true" Text="" Width="250px"></asp:Label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label style="width: 200px;">Document Header Text</label>
                                                                <asp:Label ID="lblDocumentHeaderText" runat="server" Font-Bold="true" Text="" Width="250px"></asp:Label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label style="width: 200px;">Created Date</label>
                                                                <asp:Label ID="lblCreatedDate" runat="server" Font-Bold="true" Text="" Width="250px"></asp:Label>
                                                                <label style="width: 200px; padding-left: 20px;">Posting Date</label>
                                                                <asp:Label ID="lblPostingDate" runat="server" Font-Bold="true" Text="" Width="250px"></asp:Label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label style="width: 200px;">Service Location</label>
                                                                <asp:Label ID="lblServiceLocation" runat="server" Font-Bold="true" Text="" Width="250px"></asp:Label>
                                                                <label style="width: 200px; padding-left: 20px;">Service Period</label>
                                                                <asp:Label ID="lblServicePeriod" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label style="width: 200px;">Invoice No</label>
                                                                <asp:Label ID="lblInvoiceNo" runat="server" Font-Bold="true" Text="" Width="250px"></asp:Label>
                                                                <label style="width: 200px; padding-left: 20px;">Invoice Date</label>
                                                                <telerik:RadDatePicker runat="server" ID="rdpInvoiceDate" Skin="Bootstrap" DateInput-DateFormat="dd/MM/yyyy"
                                                                    DateInput-DisplayDateFormat="dd/MM/yyyy" Width="120px" data-step="4" data-position="bottom">
                                                                    <Calendar runat="server" ShowRowHeaders="False">
                                                                        <SpecialDays>
                                                                            <telerik:RadCalendarDay Repeatable="Today">
                                                                            </telerik:RadCalendarDay>
                                                                        </SpecialDays>
                                                                    </Calendar>
                                                                </telerik:RadDatePicker>
                                                                <asp:Label ID="lblInvoiceDate" runat="server" Font-Bold="true" Text="" Width="250px" Visible="false"></asp:Label>
                                                            </div>
                                                            <div class="form-group" id="divSGSTCGST" runat="server" visible="false">
                                                                <label style="width: 200px;">SGST</label>
                                                                <asp:Label ID="lblSGST" runat="server" Font-Bold="true" Text="" Width="250px"></asp:Label>
                                                                <label style="width: 200px; padding-left: 20px;">CGST</label>
                                                                <asp:Label ID="lblCGST" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                            </div>
                                                            <div class="form-group" id="divIGST" runat="server" visible="false">
                                                                <label style="width: 200px;">IGST</label>
                                                                <asp:Label ID="lblIGST" runat="server" Font-Bold="true" Text="" Width="250px"></asp:Label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label style="width: 200px;">Invoice Value</label>
                                                                <telerik:RadNumericTextBox Skin="Bootstrap" runat="server" ID="txtInvoiceValue" Height="30px" Width="150px" Visible="false">
                                                                </telerik:RadNumericTextBox>
                                                                <asp:Label ID="lblInvoiceValue" runat="server" Font-Bold="true" Text="" Width="150px" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCurrency" runat="server" Font-Bold="true" Text="" Style="padding-left: 10px;"></asp:Label>
                                                                <label style="width: 200px; padding-left: 80px;">Invoice Receipt Date</label>
                                                                <telerik:RadDatePicker runat="server" ID="rdpInvoiceReceiptDate" Skin="Bootstrap" DateInput-DateFormat="dd/MM/yyyy"
                                                                    DateInput-DisplayDateFormat="dd/MM/yyyy" Width="120px" data-step="4" data-position="bottom" Enabled="true">
                                                                    <Calendar runat="server" ShowRowHeaders="False">
                                                                        <SpecialDays>
                                                                            <telerik:RadCalendarDay Repeatable="Today">
                                                                            </telerik:RadCalendarDay>
                                                                        </SpecialDays>
                                                                    </Calendar>
                                                                </telerik:RadDatePicker>
                                                                <asp:Label ID="lblInvoiceReceiptDate" runat="server" Font-Bold="true" Text="" Width="250px" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </telerik:RadPageView>

                                <telerik:RadPageView runat="server" ID="rpvSESDetails">
                                    <asp:Panel ID="pnlSESDetails" runat="server" Visible="true">
                                        <div class="panel-heading">
                                            <h4 class="panel-title" style="padding-left: 15px; padding-right: 15px;">SES Details</h4>
                                        </div>
                                        <div class="FormGrid">
                                            <telerik:RadGrid runat="server" ID="grdSESDetails" Skin="Metro" RenderMode="Auto"
                                                AllowMultiRowSelection="false" Width="100%"
                                                AutoGenerateColumns="false" MasterTableView-CommandItemDisplay="None">
                                                <MasterTableView DataKeyNames="EXTROW,KTEXT1" HeaderStyle-BackColor="#00539d" HeaderStyle-ForeColor="White" AlternatingItemStyle-Wrap="true"
                                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="false">
                                                    <NoRecordsTemplate>
                                                        <div>No Records</div>
                                                    </NoRecordsTemplate>
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Service No." DataField="EXTROW" AllowFiltering="true"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Service Description" DataField="KTEXT1" AllowFiltering="true"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Qty" DataField="MENGE" AllowFiltering="true"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="UOM" DataField="MEINS" AllowFiltering="true"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Gross Price" DataField="PEINH" DataFormatString="{0:n}" AllowFiltering="true"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Service Line Total" DataField="NETWR" DataFormatString="{0:n}" AllowFiltering="true"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="false" Scrolling-UseStaticHeaders="true" Scrolling-AllowScroll="true"></ClientSettings>
                                            </telerik:RadGrid>
                                        </div>
                                    </asp:Panel>
                                </telerik:RadPageView>

                                <telerik:RadPageView runat="server" ID="rpvAssignWorkflow">
                                    <asp:Panel ID="pnlAssignWorkflow" runat="server">
                                        <div class="panel-heading">
                                            <h4 class="panel-title" style="padding-left: 15px; padding-right: 15px;"><asp:Label id="lblAssignWorklowHeading" runat="server"></asp:Label></h4>
                                        </div>
                                        <div class="row " style="padding: 0px 15px; padding-right: 15px;">
                                            <div class="panel panel-default col-lg-12 col-md-12 col-sm-12 col-xs-12 nopad" style="margin-bottom: 0px; border-bottom-right-radius: 0px; border-bottom-left-radius: 0px;">
                                                <div class="panel-body entry-form" runat="server" style="font-family: 'lato'; font-weight: normal; font-style: normal; font-size: 15px; color: #545454;">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <asp:Panel ID="pnlSelectApprover" runat="server" Visible="false">
                                                                <div class="form-group">
                                                                    <label style="width: 200px;">Select Approver</label>
                                                                    <telerik:RadComboBox ID="rcbTeamMembers" runat="server" Width="300px" Skin="Bootstrap" AutoPostBack="true"
                                                                        MinFilterLength="3" Filter="Contains" EnableLoadOnDemand="true" OnItemsRequested="rcbTeamMembers_ItemsRequested">
                                                                    </telerik:RadComboBox>

                                                                    <telerik:RadButton runat="server" CssClass="WindowButton" Skin="Bootstrap" ID="btnAddMember" Text="Add" ToolTip="Add" OnClick="btnAddMember_Click">
                                                                        <Icon PrimaryIconUrl="../Resources/pixel/Images/add.png" PrimaryIconTop="8px" PrimaryIconLeft="15px"
                                                                            PrimaryIconHeight="28px" PrimaryIconWidth="28px" />
                                                                    </telerik:RadButton>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Panel ID="pnlTeamMembers" runat="server" Visible="true">
                                            <div class="FormGrid">
                                                <telerik:RadGrid runat="server" ID="grdTeamMembers" AutoGenerateColumns="false" OnItemDataBound="grdTeamMembers_ItemDataBound"
                                                    OnItemCommand="grdTeamMembers_ItemCommand" Skin="Metro" RenderMode="Auto">
                                                    <MasterTableView DataKeyNames="Sequence,EmployeeNo" HeaderStyle-Wrap="true" ItemStyle-Wrap="true"
                                                        HeaderStyle-BackColor="#00539d" HeaderStyle-ForeColor="White">
                                                        <HeaderStyle BorderColor="#007acc" />
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="Sequence" HeaderText="Sequence" HeaderStyle-Width="80px" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"></telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Photos" HeaderStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:Image runat="server" ID="imgCreatedBy" Style="width: 50px; height: 50px; border: solid 1px #e5e6f1; border-radius: 5%;" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" HeaderStyle-Width="80px" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-Width="150px" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Designation" HeaderText="Designation" HeaderStyle-Width="150px" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Department" HeaderText="Department" HeaderStyle-Width="150px" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Unit" HeaderText="Unit" HeaderStyle-Width="150px" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" Visible="false"></telerik:GridBoundColumn>
                                                            <telerik:GridButtonColumn HeaderStyle-Width="70px" ButtonType="ImageButton" ImageUrl="../Resources/Images/cross.png"
                                                                ButtonCssClass="rbEdit" UniqueName="DeleteColumn" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                                                CommandName="Delete" HeaderText="Delete" Visible="false" />
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings
                                                        Scrolling-AllowScroll="true"
                                                        Scrolling-SaveScrollPosition="true"
                                                        Scrolling-UseStaticHeaders="true"
                                                        Scrolling-FrozenColumnsCount="4"
                                                        EnableRowHoverStyle="true"
                                                        Selecting-AllowRowSelect="true"
                                                        EnablePostBackOnRowClick="true">
                                                        <Resizing AllowResizeToFit="true" AllowColumnResize="true" />
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </div>
                                        </asp:Panel>
                                    </asp:Panel>
                                </telerik:RadPageView>

                                <telerik:RadPageView runat="server" ID="rpvAttachments">
                                    <asp:Panel ID="pnlAttachments" runat="server" Visible="true">
                                        <div class="panel-heading">
                                            <h4 class="panel-title" style="padding-left: 15px; padding-right: 15px;">Invoice Document</h4>
                                        </div>
                                        <asp:Panel ID="pnlInvoiceUpload" runat="server" Visible="true">
                                            <div class="form-group">
                                                <label style="width: 200px;">Invoice Upload</label>
                                                <telerik:RadAsyncUpload Font-Bold="true"
                                                    OnClientFileSelected="OnFileSelected"
                                                    OnClientFileUploadFailed="OnFileUploadFailed"
                                                    OnClientFilesUploaded="OnFilesUploaded"
                                                    OnClientProgressUpdating="OnProgressUpdating"
                                                    OnClientFileUploaded="OnFileUploaded"
                                                    OnClientFileUploadRemoved="OnFileUploadRemoved"
                                                    EnableInlineProgress="true"
                                                    HideFileInput="false"
                                                    Width="240px"
                                                    runat="server"
                                                    ID="ruAttachment"
                                                    Skin="Metro"
                                                    UploadedFilesRendering="AboveFileInput"
                                                    OnClientValidationFailed="OnClientValidationFailed"
                                                    MultipleFileSelection="Disabled"
                                                    MaxFileInputsCount ="1"
                                                    AllowedFileExtensions=".pdf" />
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlgrdAttachments" runat="server" Visible="true">
                                            <div class="FormGrid">
                                                <telerik:RadGrid runat="server" ID="grdAttachments" Skin="Metro" RenderMode="Auto"
                                                    AllowMultiRowSelection="false" OnItemCommand="grdAttachments_ItemCommand" Width="100%"
                                                    AutoGenerateColumns="false" MasterTableView-CommandItemDisplay="None">
                                                    <MasterTableView DataKeyNames="LBLNI,FileName,AliasName" HeaderStyle-BackColor="#00539d" HeaderStyle-ForeColor="White" AlternatingItemStyle-Wrap="true"
                                                        ItemStyle-Wrap="true" HeaderStyle-Wrap="false">
                                                        <NoRecordsTemplate>
                                                            <div>No Records</div>
                                                        </NoRecordsTemplate>
                                                        <Columns>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Service Entry No" DataField="LBLNI" AllowFiltering="true"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="250px" HeaderText="File Name" DataField="FileName" AllowFiltering="true"></telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="200px" HeaderText="Alias Name" DataField="AliasName" AllowFiltering="false" ItemStyle-HorizontalAlign="Left" UniqueName="aliasname">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnAliaName" runat="server" ForeColor="Blue" Text='<%# Eval("AliasName").ToString() %>' CommandName="FileDownload"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Uploaded By" DataField="UploadedBy" AllowFiltering="true"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Uploaded On" DataField="UploadedOn" DataFormatString="{0:dd/MM/yyyy}" AllowFiltering="true"></telerik:GridBoundColumn>
                                                            <telerik:GridButtonColumn HeaderStyle-Width="70px" ButtonType="ImageButton" ImageUrl="../Resources/Images/cross.png"
                                                                ButtonCssClass="rbEdit" UniqueName="DeleteColumn" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                                                CommandName="Delete" HeaderText="Delete" Visible="false" />
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="false" Scrolling-UseStaticHeaders="true" Scrolling-AllowScroll="true"></ClientSettings>
                                                </telerik:RadGrid>
                                            </div>
                                        </asp:Panel>
                                    </asp:Panel>
                                </telerik:RadPageView>

                                <telerik:RadPageView runat="server" ID="rpvWorkflowStatus">
                                    <asp:Panel ID="pnlWorkflowStatus" runat="server">
                                        <div class="panel-heading">
                                            <h4 class="panel-title" style="padding-left: 15px; padding-right: 15px;">Workflow Status</h4>
                                        </div>
                                        <asp:Panel ID="pnlWorkflow" runat="server" Visible="true">
                                            <div class="FormGrid">
                                                <telerik:RadGrid runat="server" ID="grdWorkflowStatus" Skin="Metro" RenderMode="Auto"
                                                    AllowMultiRowSelection="false" Width="100%"
                                                    AutoGenerateColumns="false" OnItemDataBound="grdWorkflowStatus_ItemDataBound">
                                                    <MasterTableView DataKeyNames="LBLNI,CreatedBy,Flowstatus" HeaderStyle-BackColor="#00539d" HeaderStyle-ForeColor="White" AlternatingItemStyle-Wrap="true"
                                                        ItemStyle-Wrap="true" HeaderStyle-Wrap="false">
                                                        <NoRecordsTemplate>
                                                            <div>No Records</div>
                                                        </NoRecordsTemplate>
                                                        <Columns>
                                                            <telerik:GridBoundColumn HeaderText="SES No" HeaderStyle-Width="100px" DataField="LBLNI" AllowFiltering="true" Visible="false"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="S.No." HeaderStyle-Width="50px" DataField="Sequence" AllowFiltering="true"></telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Photo" HeaderStyle-Width="50px">
                                                                <ItemTemplate>
                                                                    <asp:Image runat="server" ID="imgCreatedBy" Style="width: 50px; height: 50px; border: solid 1px #e5e6f1; border-radius: 5%;" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn HeaderText="Employee" DataField="UserID" HeaderStyle-Width="150px"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Date" DataField="CreatedDate" HeaderStyle-Width="50px"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Comments" DataField="Comments" HeaderStyle-Width="250px"></telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Workflow Status" HeaderStyle-Width="150px"
                                                                ItemStyle-Width="150px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblStatus" CssClass="DocumentRejected" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="false" Scrolling-UseStaticHeaders="true" Scrolling-AllowScroll="true"></ClientSettings>
                                                </telerik:RadGrid>
                                            </div>
                                        </asp:Panel>
                                    </asp:Panel>
                                </telerik:RadPageView>

                                <telerik:RadPageView runat="server" ID="rpvComments">
                                    <asp:Panel ID="pnlComments" runat="server">
                                        <div class="panel-heading">
                                            <h4 class="panel-title" style="padding-left: 15px; padding-right: 15px;">Comments</h4>
                                        </div>
                                        <div class="row " style="padding: 0px 15px; padding-right: 15px;">
                                            <div class="panel panel-default col-lg-12 col-md-12 col-sm-12 col-xs-12 nopad" style="margin-bottom: 0px; border-bottom-right-radius: 0px; border-bottom-left-radius: 0px;">
                                                <div class="panel-body entry-form" runat="server" style="font-family: 'lato'; font-weight: normal; font-style: normal; font-size: 15px; color: #545454;">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <asp:Panel ID="Panel2" runat="server" Visible="true">
                                                                <div class="form-group">
                                                                    <label style="width: 200px;">Workflow Comments</label>
                                                                    <telerik:RadTextBox Skin="Bootstrap" runat="server" ID="txtComments" Height="70px" MaxLength="500" Width="500px" TextMode="MultiLine">
                                                                    </telerik:RadTextBox>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </telerik:RadPageView>

                            </telerik:RadMultiPage>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </asp:Panel>

    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            var uploadsInProgress = 0;
            var MAX_TOTAL_BYTES = 3145728;
            var filesSize = new Array();
            var OVERSIZE_MESSAGE = "You are only allowed to add up to 3 MB of files total";
            var isDuplicateFile = false;

            function OnFileSelected(sender, args) {

                for (var fileindex in sender._uploadedFiles) {
                    if (sender._uploadedFiles[fileindex].fileInfo.FileName == args.get_fileName()) {
                        isDuplicateFile = true;
                    }
                }
                if (!uploadsInProgress) {
                    $("input[id$=btnSave]").attr("disabled", "disabled");

                }
                uploadsInProgress++;
            }

            function OnFilesUploaded(sender, args) {
                if (sender._uploadedFiles.length == 0) {
                    filesSize = new Array();
                    uploadsInProgress = 0;
                    $("input[id$=btnSave]").removeAttr("disabled");
                }
                if (uploadsInProgress > 0) {
                    DecrementUploadsInProgress();
                }

            }

            function OnProgressUpdating(sender, args) {

                filesSize[args.get_data().fileName] = args.get_data().fileSize;

            }

            function OnFileUploadFailed(sender, args) {
                DecrementUploadsInProgress();
            }

            function OnFileUploaded(sender, args) {
                DecrementUploadsInProgress();
                var totalBytes = 0;
                var numberOfFiles = sender._uploadedFiles.length;

                if (isDuplicateFile) {
                    sender.deleteFileInputAt(numberOfFiles - 1);
                    isDuplicateFile = false;
                    sender.updateClientState();
                    alert("You can't add the same file twice");
                    return;
                }

                for (var index in filesSize) {
                    totalBytes += filesSize[index];
                }
                if (totalBytes > MAX_TOTAL_BYTES) {
                    sender.deleteFileInputAt(numberOfFiles - 1);
                    sender.updateClientState();
                    alert(OVERSIZE_MESSAGE);
                }
            }

            function OnFileUploadRemoved(sender, args) {
                if (args.get_fileName() != null) {
                    if (!isDuplicateFile) {
                        delete filesSize[args.get_fileName()];
                    }
                }
            }
            function OnClientValidationFailed(sender, args) {
                var fileExtention = args.get_fileName().substring(args.get_fileName().lastIndexOf('.') + 1, args.get_fileName().length);
                if (args.get_fileName().lastIndexOf('.') != -1) {//this checks if the extension is correct
                    if (sender.get_allowedFileExtensions().indexOf(fileExtention) == -1) {
                        alert("Wrong Extension!");
                    }
                }
                else {
                    alert("not correct extension!");
                }
            }

            function DecrementUploadsInProgress() {
                uploadsInProgress--;
                if (!uploadsInProgress) {
                    $("input[id$=btnSave]").removeAttr("disabled");

                }
            }

        </script>
    </telerik:RadCodeBlock>

    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            $ = $telerik.$;
            var _CollabPanelHeight = 100;

            function OpenDashboard(ControltoVisible, ControlToInHide, ControlToInHide1) {
                var panel = document.getElementById(ControltoVisible);
                if (panel.style.display = "none") {
                    panel.style.display = "inline-block";
                    document.getElementById(ControlToInHide).style.display = "none";
                    document.getElementById(ControlToInHide1).style.display = "none";
                }
                else {
                    panel.style.display = "none";
                }
            }

            function ScrollDashboard(sender, args) {
                if ($(sender).attr("aria-Expanded") != true) {
                    $telerik.$('html, body').animate({
                        scrollTop: $("#" + args).offset().top
                    }, 2000);
                }
            }

            function ConfirmSubmit(sender, args) {
                args.set_cancel(!window.confirm("Once Submitted You Cannot Change it again! \nDo You Want to Continue?"));
            }

            function ConfirmApprove(sender, args) {
                args.set_cancel(!window.confirm("Are you sure you want to Approve?"));
            }

            function ConfirmReject(sender, args) {
                args.set_cancel(!window.confirm("Are you sure you want to Reject?"));
            }

            function ConfirmDelete(sender, args) {
                args.set_cancel(!window.confirm("Are you sure you want to Delete?"));
            }

            function ConfirmTransfer(sender, args) {
                args.set_cancel(!window.confirm("Are you sure you want to Transfer To SAP?"));
            }

            function openNav() {
                document.getElementById("mySidenav").style.width = "400px";
                document.getElementById("main").style.marginRight = "400px";
                document.getElementById("opcl").className = "opsh";
            }

            function closeNav() {
                document.getElementById("mySidenav").style.width = "0";
                document.getElementById("main").style.marginRight = "0";
                document.getElementById("opcl").className = "clsh";
            }


            $(document).ready(function () {
                try {
                    containerwidth = Math.max(document.documentElement.clientWidth || 0, window.innerWidth || 0)
                    if (containerwidth >= 1024) {
                        $('#opcl').show();
                        $('.flexLabel').removeClass('forceBlock');
                    }
                    else {
                        $('#opcl').hide();
                        $('.flexLabel').addClass('forceBlock');
                    }
                }
                catch (e) { $('.flexLabel').addClass('forceBlock'); }
            });
            window.onresize = function () {
                try {
                    containerwidth = Math.max(document.documentElement.clientWidth || 0, window.innerWidth || 0)
                    if (containerwidth >= 1024) {
                        $('#opcl').show();
                        $('.flexLabel').removeClass('forceBlock');
                    }
                    else {
                        $('#opcl').hide();
                        $('.flexLabel').addClass('forceBlock');
                    }
                }
                catch (e) { $('.flexLabel').addClass('forceBlock'); }
            };

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
