<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMListaAnketa.aspx.cs" Inherits="PlatinumWeb.CRMListaAnketa" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>




<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha CRM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/images/CRM/faviconCRM.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/images/CRM/faviconCRM.ico" type="image/ico" />
    <link type="text/css" rel="stylesheet" href="~/js/srcCRM/css/jquery.mmenu.all.css" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link type="text/css" rel="stylesheet" href="AlphaCRM.css" />
    <link href="css/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/Utils-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/srcCRM/js/jquery.mmenu.min.all.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/CRMListaAnketa.aspx-IMB.4.8.js&v49"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('nav#menu').mmenu({
                classes: "mm-light",
            });
        });
    </script>
</head>
<body>
    <div id="page">
        <div class="header">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 1%;">
                        <a href="#menu"></a>
                    </td>
                    <td style="width: 94%; vertical-align: top;">Lista Anketa</td>
                    <td style="width: 5%;">
                        <div id="emriLogout" class="emriLogout">
                            <div id="userInfo">
                                <div id="emri">
                                    <dx:ASPxLabel ID="lblUserEmri" ClientInstanceName="lblUserEmri" runat="server" Text=""
                                        Font-Size="14" ForeColor="White" Font-Names="Calibri">
                                    </dx:ASPxLabel>
                                </div>
                                <div id="logout">
                                    <a style="position: relative; color: white; background-image: none;" class="fa fa-sign-out fa-2x"> <i class="fa fa-sign-out  fa-lg"></i>&nbsp;</a>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="content">
            <form id="form1" runat="server">
                
                <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                    Font-Size="9pt" Modal="True" ImagePosition="Top">
                    <LoadingDivStyle Opacity="30">
                    </LoadingDivStyle>
                </dx:ASPxLoadingPanel>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                   
                </asp:ScriptManager>
                <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                    ViewStateMode="Enabled">
                </dx:ASPxHiddenField>
                <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                </dx:ASPxGlobalEvents>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table width="100%">

                            <tr>
                                <td>
                                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                        ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                        OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                        <ClientSideEvents ItemClick="function(s, e) {
	                        menu_click(s,e);
                            }" Init="function(s) {s.SetClientVisible(true);}" />
                                        <ItemImage Height="32px" Width="32px">
                                        </ItemImage>
                                        <SubMenuItemImage Height="16px" Width="16px">
                                        </SubMenuItemImage>
                                        <ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" VerticalAlign="Middle">
                                            <Paddings PaddingBottom="1px" PaddingTop="9px" />
                                        </ItemStyle>
                                        <SubMenuItemStyle Width="32px">
                                        </SubMenuItemStyle>
                                    </dx:ASPxMenu>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                                BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                                <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <SubMenuStyle GutterWidth="17px" />
                                            </dx:ASPxMenu>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientInstanceName="popFshi"
                            CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes"
                            Font-Bold="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                            Width="300px" ClientIDMode="AutoID" CssPostfix="Glass">
                            <HeaderStyle>
                                <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                            </HeaderStyle>
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                                    <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="lblMsgbox" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?">
                                                </dx:ASPxLabel>
                                                <br />
                                                <br />
                                                <div style="text-align: right;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                                    OnClick="ButtonOk_Click2" Text="Ok">
                                                                    <ClientSideEvents Click="Click_ButtonOk" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                                    <ClientSideEvents Click="function(s, e) {
		popFshi.Hide();
}" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxPanel >
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl >
                        <div style="visibility: hidden">
                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton" ClientInstanceName="btn">
                            </dx:ASPxButton>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="width: 100%">
                    <asp:UpdatePanel ID="pnlGrida" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <dx:ASPxGridView ID="gvCRMListaAnketa" runat="server" Width="100%" OnAfterPerformCallback="gvCRMListaAnketa_AfterPerformCallback"
                                OnHeaderFilterFillItems="gvCRMListaAnketa_HeaderFilterFillItems" OnAutoFilterCellEditorInitialize="gvCRMListaAnketa_AutoFilterCellEditorInitialize"
                                ClientInstanceName="gvCRMListaAnketa" OnProcessColumnAutoFilter="gvCRMListaAnketa_ProcessColumnAutoFilter" OnDataBound="gvCRMListaAnketa_DataBound"
                                OnInitNewRow="gvCRMListaAnketa_InitNewRow" OnCustomCallback="gvCRMListaAnketa_CustomCallback"
                                OnCustomJSProperties="gvCRMListaAnketa_CustomJSProperties" ClientIDMode="AutoID" Settings-ShowTitlePanel="true">
                                <ClientSideEvents RowDblClick="Row_DblClick"
                                    FocusedRowChanged="function(s,e){mbushfusha(e);}" 
                                    BeginCallback="function(s, e) {	BeginCallback(s,e); }" />
                                <Templates>
                                    <TitlePanel>
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="ASPxButton2" runat="server" ToolTip="Zgjidh kolonat" AutoPostBack="false"
                                                        ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                        <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s, e, gvCRMListaAnketa)}"
                                                            Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="pnlruaj" runat="server">
                                                        <ContentTemplate>
                                                            <dx:ASPxButton ID="ASPxButton3" runat="server" ToolTip="Ruaj kolonat" AutoPostBack="true"
                                                                ClientVisible="false" Image-Url="images/new/disk_blue (3).png" Font-Size="8"
                                                                OnClick="RuajKolona_Click">
                                                                <ClientSideEvents Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                            </dx:ASPxButton>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="gridaSelectFaqe" runat="server" ToolTip="Zgjidh te gjithe faqen"
                                                        AutoPostBack="false" Image-Url="images/check2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                        <ClientSideEvents Click="function(s, e) { gvCRMListaAnketa.SelectAllRowsOnPage(); }" />
                                                    </dx:ASPxButton>

                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe"
                                                        AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                        <ClientSideEvents Click="function(s, e) { gvCRMListaAnketa.SelectRows(); }" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="gridaUnSelectTeGjitha" runat="server" ToolTip="Fshi Zgjedhjen"
                                                        AutoPostBack="false" Image-Url="images/uncheck2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                        <ClientSideEvents Click="function(s, e) { gvCRMListaAnketa.UnselectRows(); }" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="btnXlsxExport" runat="server" AutoPostBack="false" ToolTip="Export to Xlsx" Image-Height="16px" Image-Url="images/xlsx24.png"
                                                        Font-Size="8">
                                                        <ClientSideEvents Click="function(s, e) { btnXlsxExportHidden.DoClick(); }" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="btnPdfExport" runat="server" AutoPostBack="false" ToolTip="Export to Pdf" Image-Height="16px" Image-Url="images/pdf_icon.png"
                                                        Font-Size="8">
                                                        <ClientSideEvents Click="function(s, e) { btnPdfExportHidden.DoClick(); }" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </TitlePanel>
                                </Templates>
                                <Styles>
                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                    </Header>
                                </Styles>
                                <SettingsPager PageSize="15">
                                </SettingsPager>
                                <StylesEditors>
                                    <ProgressBar Height="25px">
                                    </ProgressBar>
                                </StylesEditors>
                                <Paddings PaddingLeft="20px" PaddingRight="20px" PaddingTop="1px" />
                            </dx:ASPxGridView>
                            <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                            <asp:HiddenField ID="hfRuaj" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
                    </dx:ASPxHiddenField>
                    <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gvCRMListaAnketa"
                        ExportedRowType="Selected" />
                    <dx:ASPxButton ID="btnPdfExportHidden" ClientVisible="False" ClientInstanceName="btnPdfExportHidden"
                        runat="server" ToolTip="Export to Pdf" Text="Export to Pdf" Font-Size="8pt" UseSubmitBehavior="False"
                        OnClick="btnPdfExport_Click">
                        <ClientSideEvents Click="function(s, e) { clickExport(e) }" />
                    </dx:ASPxButton>

                    <dx:ASPxButton ID="btnXlsxExportHidden" ClientVisible="False" ClientInstanceName="btnXlsxExportHidden"
                        runat="server" ToolTip="Export to Xlsx" Text="Export to Xlsx" Font-Size="8" UseSubmitBehavior="false"
                        OnClick="btnXlsxExport_Click">
                        <ClientSideEvents Click="function(s, e) {
              clickExport(e) 
}" />
                    </dx:ASPxButton>
                </div>
            </form>
        </div>
        <nav id="menu">
            <ul id="ulMenu">
            </ul>
        </nav>
    </div>
</body>
</html>
