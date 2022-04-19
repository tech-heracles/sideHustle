<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogeSistemi.aspx.cs" Inherits="PlatinumWeb.LogeSistemi" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/ucMenuAndMsgFrame.ascx" TagPrefix="ucMenu" TagName="ucMenuAndMsgFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>

    <!-- DevExtreme themes -->
    <link rel="stylesheet" type="text/css" href="Content/dx.common.css" />
    <link rel="stylesheet" type="text/css" href="Content/dx.generic.alphaweb-compact.css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <!-- A DevExtreme library -->
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/Scripts/jszip.js;~/Scripts/dx.viz-web.js;~/js/myDxDataGrid.js;~/js/aspx.js/LogeSistemi.aspx-IMB.8.2.js&v76"
        type="text/javascript">
    </script>
    <style>
        tr {
            padding: 15px;
            margin: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top" LoadingDivStyle-Opacity="30" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" runat="server" OnMenuTemplate="PercaktoTemplateMenu" />
        <div>
            <dxtc:ASPxPageControl ID="PageControl" runat="server" ActiveTabIndex="0" ClientInstanceName="PageControl">
                <TabPages>
                    <dxtc:TabPage Name="Loge Sistemi" Text="Loge Sistemi">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl1" runat="server" SupportDisabledAttribute="true">
                                <table class="renditKontrolle" style="width:20%">
                                    <tbody>
                                        <tr>
                                            <td class="renditKontrolleCaption" style="width:25%">
                                                <dx:ASPxLabel ID="lblData" runat="server" Text="Periudha : "></dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxDateEdit ID="dataNga" runat="server" ClientInstanceName="dataNga" ClientEnabled="true" EditFormat="Date">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxDateEdit ID="dataDeri" runat="server" ClientInstanceName="dataDeri" ClientEnabled="true" EditFormat="Date">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="renditKontrolleCaption" style="width:25%">
                                                <dx:ASPxLabel ID="lblModuli" runat="server" Text="Lloji : " AssociatedControlID="cbModuli"></dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxComboBox ID="cbModuli" runat="server" ClientInstanceName="cbModuli" ClientEnabled="true" ClientSideEvents-SelectedIndexChanged="moduliSelectedIndexChanged">
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td class="renditKontrolleCaption"></td>
                                        </tr>
                                        <tr>
                                            <td class="renditKontrolleCaption" style="width:25%">
                                                <dx:ASPxLabel ID="lblVerbosity" runat="server" Text="Verbosity : " AssociatedControlID="cbVerbosity"></dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxComboBox ID="cbVerbosity" runat="server" ClientEnabled="true" ClientInstanceName="cbVerbosity" ClientSideEvents-SelectedIndexChanged="verbositySelectedIndexChanged">
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxButton ID="btnRuajVerbosity" ClientInstanceName ="btnRuajVerbosity" runat="server" Text="Ruaj nivel Verbosity" ToolTip="Per llojin e zgjedhur.">
                                                    <ClientSideEvents Click="RuajNivelVerbosity" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <!------Grida e logeve duke perdorur DevExtreme---------->
                                <div class="demo-container" style="padding-top:20px">
                                    <div id="data-grid-importi">
                                        <div id="grida" class="noUndoGrida"></div>
                                    </div>
                                </div>
                                <!------------------------------------------------------->
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                </TabPages>
            </dxtc:ASPxPageControl>
        </div>
    </form>
</body>
</html>
