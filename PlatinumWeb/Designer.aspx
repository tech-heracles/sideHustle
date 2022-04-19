<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Designer.aspx.cs" Inherits="PlatinumWeb.Designer" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.2.Web.WebForms, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraReports.v18.2.Web.WebForms, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web.ClientControls" TagPrefix="cc1" %>
<!DOCTYPE html>

<html>

<head runat="server">
    <title></title>
    <style>
        body {
            margin: 0;
            padding: 0;
        }

        .fullscreen {
            position: relative;
            margin: 0;
            height: 90%;
        }

        .customButton {
            background-image: url(../images/CustomButton.png);
            background-repeat: no-repeat;
        }
    </style>
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Designer.aspx-IMB.6.4.js&v76" type="text/javascript"></script>
</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="ScriptManager1" />
        <div id="dvMenu" style="display: none">
            <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                        BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True"
                        AppearAfter="300">
                        <ClientSideEvents Init="function(s,e){$('#dvMenu').show();myMesazh.InicializoTimer();}" />
                        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                        <ItemStyle HorizontalAlign="Left" />
                        <SubMenuStyle GutterWidth="17px" />
                    </dx:ASPxMenu>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="clear: both">
             
            <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                Font-Size="9pt" Modal="True" ImagePosition="Top">
                <LoadingDivStyle Opacity="30">
                </LoadingDivStyle>
            </dx:ASPxLoadingPanel>

        </div>
        <dx:ASPxCallbackPanel runat="server" ID="CallbackPanel" ClientInstanceName="CallbackPanel" OnCallback="CallbackPanel_Callback">
            <PanelCollection>
                <dx:PanelContent>
                    <dx:ASPxHiddenField runat="server" ID="hfState" ClientInstanceName="hfState" />
                    <dx:ASPxReportDesigner ID="reportDesigner" ClientInstanceName="reportDesigner" runat="server" CssClass="fullscreen bootstrap-iso" OnUnload="reportDesigner_Unload" OnSaveReportLayout="ASPxReportDesigner1_SaveReportLayout" EnableDataSourceWizard="False" ShouldDisposeDataSources="false">
                        <ClientSideEvents CustomizeMenuActions="reportDesigner_CustomizeMenuActions" EndCallback="reportDesigner_endCallback" />
                        <MenuItems>
                            <cc1:ClientControlsMenuItem ImageClassName="dxrd-image-save" Text="Save As" />
                            <cc1:ClientControlsMenuItem ImageClassName="dxrd-image-run-wizard" Text="Load Custom Layout" />
                        </MenuItems>
                    </dx:ASPxReportDesigner>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </form>
</body>
</html>
