<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RaportiShpejte.aspx.cs"
    Inherits="PlatinumWeb.RaportiShpejte" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.2.Web.WebForms, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Src="~/ImbReportToolbar.ascx" TagPrefix="uc1" TagName="ImbReportToolbar" %>

<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Text" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" rel="stylesheet" />    
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />    
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script type="text/html" id="custom-designerSelector-template">
        <div class="dxrd-toolbar-item-zoom custom-design-selector-item" data-bind="visible: visible">
            <div class="dxrd-toolbar-item-zoom-editor custom-design-selector-item-editor" data-bind="dxSelectBox: { hint: $data.text, items: $data.items, value: $data.currentValue, displayExpr: $data.displayExpr, valueExpr: $data.valueExpr, onValueChanged: $data.onValueChanged, searchEnabled: true }"></div>
        </div>
    </script>

    <script type="text/html" id="custom-toolbarItem-template">
        <div class="dxrd-toolbar-item-zoom" data-bind="visible: visible">
            <div class="dxrd-toolbar-item-zoom-editor" data-bind="dxSelectBox: { hint: $data.text, items: $data.items, value: $data.currentValue, displayExpr: $data.displayExpr, valueExpr: $data.valueExpr, onValueChanged: $data.onValueChanged }"></div>
        </div>
    </script>
    
    <script type="text/html" id="custom-toolbarItem-template-button">
        <div class="custom-toolbar-item" data-bind="visible: visible">
            <div data-bind="dxButton: { text: $data.text, onClick: $data.onClick }"></div>
        </div>
    </script>

    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/RaporteUtils.js;~/js/aspx.js/RaportiShpejte.aspx-IMB.2.1.js&v76"
    type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
         
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"/>
        <dxcp:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="100%" OnCallback="ASPxCallbackPanel1_Callback"
            ClientIDMode="AutoID" ClientSideEvents-BeginCallback="ASPxCallbackPanel1_BeginCallback">
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled"/>
                    <dx:ASPxWebDocumentViewer ID="rvRaporti" runat="server" AllowURLsWithJSContent="true" ClientInstanceName="rvRaporti" >
                        <ClientSideEvents CustomizeMenuActions="CustomizeMenuActions"  Init="InitReportViewer"/>
                    </dx:ASPxWebDocumentViewer>
                </dxp:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel >
    </div>
    </form>
</body>
</html>