<%@ Page Title="" Language="C#" MasterPageFile="~/Transactions/Menu.Master" AutoEventWireup="true" CodeBehind="ManageSAPDBAlert.aspx.cs" Inherits="MyDetailsWeb.Transactions.SAPDBALERT.ManageSAPDBAlert" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormIcon" runat="server">
    <img src="../../Resources/Images/Editor.png" />
    <style type="text/css">
        .content {
            padding: 23px 0 0 23px;
            border: none;
            box-sizing: border-box;
        }

.RadMenu .rmGroup .rmLink {

padding: 4px 32px !important;

}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFromTitle" runat="server">
    Manager SAP DB Alert Notification
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="FormButtons">
        <div class="FormElement">
            <div class="Element">
                <telerik:RadButton runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" TabIndex="4" AccessKey="s" ToolTip="Save (Alt+s)">
                    <Icon PrimaryIconCssClass="rbSave" PrimaryIconLeft="8" PrimaryIconTop="4"></Icon>
                </telerik:RadButton>
            </div>
            <div class="Element">
                <telerik:RadButton runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" AccessKey="c" TabIndex="5" ToolTip="Cancel (Alt+c)">
                    <Icon PrimaryIconCssClass="rbRemove" PrimaryIconLeft="8" PrimaryIconTop="4"></Icon>
                </telerik:RadButton>
            </div>
        </div>
    </div>
    <div class="FormElement">
        <div class="Title">
            System
        </div>
        <div class="Element">
            <telerik:RadComboBox ID="rcbSystem" runat="server" TabIndex="1" Width="200px">
                <Items>
                    <telerik:RadComboBoxItem Text="BIP" Value="BIP" />
                    <telerik:RadComboBoxItem Text="BWP" Value="BWP" />
                    <telerik:RadComboBoxItem Text="PIP" Value="PIP" />
                </Items>
            </telerik:RadComboBox>
        </div>
    </div>
    <div class="FormElement">
        <div class="Title">
            Alert
        </div>
        <div class="Element">
            <telerik:RadRadioButtonList AutoPostBack="false" TabIndex="2" EnableTheming="true" RenderMode="Lightweight" Direction="Horizontal" ID="rblAlert" Width="200px" runat="server">
                <Items>
                    <telerik:ButtonListItem Text="ON" Value="0" />
                    <telerik:ButtonListItem Text="OFF" Value="1" />
                </Items>
            </telerik:RadRadioButtonList>
        </div>
    </div>
    <div class="FormElement" style="min-height:100px !important">
        <div class="Title">
            Reason
        </div>
        <div class="Element">
            <telerik:RadTextBox ID="rtbReason" runat="server" Width="400px" TabIndex="3" Height="100px" TextMode="MultiLine"></telerik:RadTextBox>
        </div>
    </div>
    <br />
    <div class="FormGrid" style="width: 99%;">
        <div class="FormElement">
            <telerik:RadGrid ID="RGRDSAPDBAlert"
                ItemStyle-Wrap="false"
                AllowPaging="true"
                OnNeedDataSource="RGRDSAPDBAlert_NeedDataSource"
                OnItemCommand="RGRDSAPDBAlert_ItemCommand"
                OnItemDataBound="RGRDSAPDBAlert_ItemDataBound"
                OnItemCreated="RGRDSAPDBAlert_ItemCreated"
                Skin="Simple"
                Width="70%"
                PageSize="10"
                AllowFilteringByColumn="true"
                ClientSettings-Scrolling-UseStaticHeaders="true"
                ClientSettings-Scrolling-AllowScroll="true"
                AutoGenerateColumns="False"
                runat="server">
                <ClientSettings Scrolling-FrozenColumnsCount="3"></ClientSettings>
                <MasterTableView HeaderStyle-BackColor="#00539d" HeaderStyle-ForeColor="White" AlternatingItemStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-Wrap="true">
                    <HeaderStyle BorderColor="#007acc" Wrap="false" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="System" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" UniqueName="System" HeaderText="System"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Alert" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" UniqueName="Alert" HeaderText="Alert"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Remarks" AllowFiltering="false"  ShowFilterIcon="false" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" UniqueName="Remarks" HeaderText="Remarks"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CreatedBy" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" UniqueName="CreatedBy" HeaderText="Created By"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CreatedOn" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" UniqueName="CreatedOn" HeaderText="Created On"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </div>
</asp:Content>
