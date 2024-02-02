<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Transactions/BIApplication.master" CodeBehind="UserImpersonation.aspx.cs" Inherits="SESWeb.Transactions.UserImpersonation" %>

<%@ MasterType VirtualPath="~/Transactions/BIApplication.master" %>

<asp:Content ID="pgBreadCrumb" ContentPlaceHolderID="cphBreadCrumb" runat="server">
    <li class="breadcrumbbutton active">
        <img src="../Resources/PIXEL/images/icons/profile-user.png">
        <span>
            <h5>User Impersonation</h5>
        </span>
    </li>
</asp:Content>

<asp:Content ContentPlaceHolderID="cphPagecontent" runat="server" ID="pgContent">
    <asp:Panel runat="server" ID="pnlcontent">
        <section>
            <div class="container-fluid">
                <div class="row " style="padding: 0px 15px;">
                    <div class="panel panel-default col-lg-12 col-md-12 col-sm-12 col-xs-12 nopad" style="margin-bottom: 0px; margin-top: 120px; border-bottom-right-radius: 0px; border-bottom-left-radius: 0px;">
                        <div class="panel-heading">
                            <h4 class="panel-title" style="padding-left: 15px; padding-right: 15px;">User Impersonate</h4>
                        </div>
                        <div class="panel-body entry-form" runat="server" style="font-family: 'lato'; font-weight: normal; font-style: normal; font-size: 15px; color: #545454;">
                            <telerik:RadGrid ID="grdUserLoginImpersonate" runat="server" AutoGenerateColumns="false" Skin="Metro" Width="100%"
                                PagerStyle-AlwaysVisible="true" AllowPaging="true" PagerStyle-Position="TopAndBottom" PageSize="15"
                                OnNeedDataSource="grdUserLoginImpersonate_NeedDataSource" OnItemCommand="grdUserLoginImpersonate_ItemCommand"
                                GroupingSettings-CaseSensitive="false">
                                <ClientSettings
                                    Scrolling-AllowScroll="true"
                                    Scrolling-SaveScrollPosition="true"
                                    Scrolling-UseStaticHeaders="true"
                                    EnableRowHoverStyle="false"
                                    Selecting-AllowRowSelect="false"
                                    EnablePostBackOnRowClick="false">
                                    <Resizing AllowResizeToFit="true" AllowColumnResize="true" />
                                </ClientSettings>
                                <MasterTableView CommandItemDisplay="None" EnableNoRecordsTemplate="false" HeaderStyle-BackColor="#00539d" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true"
                                    DataKeyNames="UserID" AllowFilteringByColumn="true">
                                    <HeaderStyle BorderColor="#007acc" />
                                    <Columns>
                                        <telerik:GridTemplateColumn DataField="UserID" HeaderText="Employee No" UniqueName="UserID" CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnUserID" runat="server" Text='<%# Eval("UserID") %>' CommandName="SetUser"></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Name" UniqueName="EmployeeName" CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true" />
                                        <telerik:GridBoundColumn DataField="dept" HeaderText="Department" UniqueName="Department" CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true" />
                                        <telerik:GridBoundColumn DataField="desig" HeaderText="Designation" UniqueName="Designation" CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true" />
                                        <telerik:GridBoundColumn DataField="Roles" HeaderText="Roles" UniqueName="Roles" CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true" />
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </asp:Panel>

    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            window.onresize = function () { ResizeGrid(); };
            $(document).ready(function () { ResizeGrid(); });
            $(window).on('load', function () { ResizeGrid(); });
            function ResizeGrid() {
                try {
                    var grid = $find("<%= grdUserLoginImpersonate.ClientID %>");
                    var scrollArea = null;
                    try {
                        scrollArea = grid.GridDataDiv;
                    } catch (e) {

                    }
                    if (scrollArea != null) {
                        ControlHeight = ($(window).height() - ($('#<%=grdUserLoginImpersonate.ClientID%>').offset().top + 170));
                        if (ControlHeight > 100) {
                            $(scrollArea).height(ControlHeight);
                        }
                        else {
                            $(scrollArea).height(200);
                        }
                    }
                }
                catch (e) {
                    try { $find("<%= grdUserLoginImpersonate.ClientID %>").GridDataDiv.style.height = '300px'; } catch (e) { }
                }
            }
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
