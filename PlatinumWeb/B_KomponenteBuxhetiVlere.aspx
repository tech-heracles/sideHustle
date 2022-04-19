<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="B_KomponenteBuxhetiVlere.aspx.cs" Inherits="PlatinumWeb.B_KomponenteBuxhetiVlere"%>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/ucMenuAndMsgFrame.ascx" TagPrefix="ucMenu" TagName="ucMenuAndMsgFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
        <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <!-- DevExtreme themes -->
    <link rel="stylesheet" type="text/css" href="Content/dx.common.css" />
    <link rel="stylesheet" type="text/css" href="Content/dx.generic.alphaweb-compact.css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />

    <!-- A DevExtreme library -->

    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/Scripts/dx.viz-web.js;~/js/localization/DevExtreme.Perkthime.js;~/js/myDxDataGrid.js;~/js/aspx.js/B_KomponenteBuxhetiVlere.aspx-IMB.7.1.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"> <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top" LoadingDivStyle-Opacity="30"/>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server"></dx:ASPxGlobalEvents>
        <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" runat="server" OnMenuTemplate="PercaktoTemplateMenu" />

        <div id="divgride1" style="min-height:200px;">
            <!------Grida e lidhjes se komponenteve duke perdorur DevExtreme---------->
            <div class="dx-viewport demo-container">
                <div id="data-grid-buxheti">
                    <div id="gvKomponenteVlere" class ="noUndoGrida"></div>
                </div>
            </div>
            <!------------------------------------------------------->
        </div>
        <asp:UpdatePanel runat="server" id="hiddenFields">
            <ContentTemplate>
                    <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled"></dx:ASPxHiddenField>
                    <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta"/>
                    <asp:HiddenField ID="hfShtimModifikim" ClientIDMode="Static" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
