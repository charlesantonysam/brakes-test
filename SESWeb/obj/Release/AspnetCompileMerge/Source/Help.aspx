<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="SESWeb.Help" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/tr/xhtml11/dtd/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="Resources/Styles/Style.css" rel="stylesheet" />
    <title></title>
    <style type="text/css" media="print">
        #MainSplitter {
            display: none;
        }

        @page {
            margin: 10px;
        }

        body {
            display: block;
            margin: 0px;
            border: none;
        }

        #fnp, #fcp, #printheader {
            display: block !important;
            visibility: visible;
        }
        #fcp .screenshot img{
                max-width:95% !important;
            }
    </style>
    <style type="text/css" media="all">
        * {
            scrollbar-arrow-color: #E5E6EB;
            scrollbar-base-color: #FFFFFF;
            scrollbar-face-color: #f2f3f7;
            scrollbar-highlight-color: #FFFFFF;
            scrollbar-3dlight-color: #FFFFFF;
            scrollbar-darkshadow-color: #FFFFFF;
            scrollbar-shadow-color: #ecedf1;
        }

        #pnlInfo {
            position: relative !important;
        }

        #printheader span, #printheader h1,
        #printheader h2, #printheader h3, #printheader h4 {
        }

        #printheader table tr td {
            vertical-align: top;
        }

        #fnp, #fcp, #printheader {
            display: none;
        }

        #pnlFileName, .fileName {
            font-size: 22px;
            font-weight: 600;
            color: #3b3636;
            padding: 0px 2px 0px 10px;
        }

        #pnlFileContent, #pnlContent, .fileContent {
             font-size: 16px;
            overflow: visible;
            padding: 10px 20px 10px 20px;
        }

           #fcp p,#fcp h3,#fcp h4, #pnlContent p, #pnlContent h3, #pnlContent h4 {
                font-weight: normal !important;
            }

            #pnlContent ul {
                padding: 0px 0px 0px 0px;
                list-style: none;
            }
                #pnlContent ul li {
                    margin:15px 0px 0px 0px;
                }
                #pnlContent ul li h3{
                    font-weight: 600 !important;
                }

                    #pnlContent ul li h4 {
                        padding: 0px 0px 0px 20px;
                        font-weight: 500 !important;
                    }

            #pnlContent .screenshot {
                box-shadow: 1px 1px 10px rgba(59, 54, 54, 0.50);
                margin: 10px;
                display: inline-block;
                width: auto;
            }
               
    </style>
</head>
<body style="height: 100%; margin: 0px" scroll="no">
    <form id="form1" runat="server" style="height: 100%; margin: 0px">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <telerik:RadSplitter RenderMode="Lightweight" ID="MainSplitter" runat="server" Orientation="Horizontal"
            Height="100%"
            Width="100%">
            <telerik:RadPane runat="server" ClientIDMode="Static" ID="TopPane" BackColor="#3D6CEF" ForeColor="#E5E6EB" Height="55px" Scrolling="None">
                <div style="overflow: visible;">
                    <img src="Resources/Images/Banner.png" style="cursor:pointer; vertical-align: middle; width: 31vw; height: 4vw;" onclick="javascript:window.location='Help.aspx'" /><span style="font-size: 2vw; font-weight: 600; vertical-align: middle;">&nbsp;-&nbsp;User Help Documentation.</span>Version 1.0
                </div>
            </telerik:RadPane>
            <telerik:RadPane runat="server" ClientIDMode="Static" ID="TopMainPane">
                <telerik:RadSplitter runat="server" Orientation="Vertical">
                    <telerik:RadPane ClientIDMode="Static" ID="LeftPane" runat="server"
                        Width="220px" Scrolling="Both">
                        <telerik:RadTreeView RenderMode="Lightweight" runat="server" ID="rtHome"
                            DataFieldID="id" DataFieldParentID="parentID" CheckBoxes="false">
                            <DataBindings>
                                <telerik:RadTreeNodeBinding TextField="Text"></telerik:RadTreeNodeBinding>
                                <telerik:RadTreeNodeBinding Depth="0" Checkable="false" TextField="Text" Expanded="true"
                                    CssClass="rootNode"></telerik:RadTreeNodeBinding>
                            </DataBindings>
                        </telerik:RadTreeView>
                    </telerik:RadPane>
                    <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward" />
                    <telerik:RadPane ClientIDMode="Static" ID="MainPane" runat="server" Scrolling="Both">
                        <!-- Place the content of the pane here -->

                        <div id="pnlFileContent" runat="server" visible="false">
                            <div id="pnlFileName" runat="server"></div>
                            <hr />
                            <div id="pnlContent" runat="server">
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnlInfo" Visible="false" ClientIDMode="Static">
                            <div style="position: absolute; margin: 20%; font-size: 30px; color: silver;">
                                Select any Menu from the list to get help on that !
                            </div>
                        </asp:Panel>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>

        </telerik:RadSplitter>
        <div id="printheader">
            <table border="0" style="border-bottom: 2px solid silver !important; width: 100%; height: 50px; overflow: hidden !important; color: #3b3636">
                <tr>
                    <td>
                        <img src="Resources/Images/Banner.png" />
                    </td>
                    <td>
                        <h2>User Help Documentation.</h2>
                        <h4>V1.0,<%= DateTime.Now.ToString("dd-MMM-yyyy HH:mm") %>,User:<%= objAuthorization.UserID %></h4>
                    </td>
                </tr>
            </table>

        </div>
        <div id="fnp" class="fileName" runat="server"></div>

        <div id="fcp" runat="server" class="fileContent">
        </div>
        <script type="text/javascript">            
        </script>
    </form>
</body>
</html>
