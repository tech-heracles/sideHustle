<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportWK.aspx.cs" Inherits="PlatinumWeb.ImportWK" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server"
        id="themeJQuery" />
    <link rel="stylesheet" type="text/css" media="screen" href="js/jqGrid445/css/ui.jqgrid.css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/ImportWK.aspx-IMB.4.0.js&v76""
        type="text/javascript"></script>
</head>
<body onload="Init()" onkeydown="enter()">
    <form id="form1" runat="server">
         
    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
        Font-Size="9pt" Modal="True" ImagePosition="Top">
        <LoadingDivStyle Opacity="30">
        </LoadingDivStyle>
    </dx:ASPxLoadingPanel>
    <div style="width: 100%; height: 100%">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
     </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ try{ window.parent.SessionTimeout.sendKeepAlive(); } catch(e){}}" />--%>
        </dx:ASPxGlobalEvents>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                            <div id="dvMenu" style="display: none">
                                <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                            BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                            <ClientSideEvents Init="Init_MenuInfo" />
                                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <SubMenuStyle GutterWidth="17px" />
                                        </dx:ASPxMenu>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
            </Triggers>
            <ContentTemplate>
                <asp:HiddenField ID="status1" runat="server" Value="false" />
                <asp:HiddenField ID="hfStatusRuajtje" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="670px" ClientInstanceName="splitter"
            PaneMinSize="670px">
            <Panes>
                <%-- Header pane--%>
                <dx:SplitterPane PaneStyle-BackColor="Transparent" Separators-Size="10px" ScrollBars="Vertical">
                    <Separators Size="10px">
                    </Separators>
                    <PaneStyle>
                    </PaneStyle>
                    <ContentCollection>
                        <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                            <asp:Panel ID="ContentPanel" runat="server">
                                <asp:UpdatePanel ID="pnlLidhur" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table id='hl' runat="server">
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <table id="tblFillim">
                                    <tbody>
                                    </tbody>
                                </table>
                                <div id="divgride1" style="display: none; overflow: auto;">
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td valign="top">
                                                <dx:ASPxGridView ID="gvImport" runat="server" ClientInstanceName="gvImport" Settings-ShowGroupPanel="false"
                                                    Width="100%" OnCustomCallback="gvImport_CustomCallback" OnDataBound="gvImport_DataBound" OnCellEditorInitialize="gvImport_CellEditorInitialize"
                                                    OnHtmlRowCreated="gvImport_HtmlRowCreated" OnCustomJSProperties="gvImport_CustomJSProperties"
                                                    OnAfterPerformCallback="gvImport_AfterPerformCallback" OnRowDeleting="gvImport_RowDeleting"
                                                    OnRowInserting="gvImport_RowInserting" OnRowUpdating="gvImport_RowUpdating">
                                                    <ClientSideEvents SelectionChanged="function(s,e){}" EndCallback="function(s,e){}"
                                                        CallbackError="Callback_Error" />
                                                    <Styles>
                                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                        </Header>
                                                    </Styles>
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsEditing EditFormColumnCount="4" />
                                                    <StylesEditors>
                                                        <CalendarHeader Spacing="1px">
                                                        </CalendarHeader>
                                                        <ProgressBar Height="25px">
                                                        </ProgressBar>
                                                    </StylesEditors>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
            </Panes>
        </dx:ASPxSplitter >
        <asp:HiddenField ID="HfKonfAmb" runat="server" />
        <asp:HiddenField ID="hfShtimModifikim" runat="server" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HFStatusiDokumentit" runat="server" />
        <asp:HiddenField ID="hfKolonaGride" runat="server" />
        <asp:HiddenField ID="HfGridCol" runat="server" />
        <asp:HiddenField ID="hfKonffillestar" runat="server" />
        <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
        <asp:HiddenField ID="hfGridaKodi" runat="server" />
        <asp:HiddenField ID="hfGridaDetajimi" runat="server" />
    </div>
    <br />
    <br />
    <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
    </dx:ASPxHiddenField>
    <div>
        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
            CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
            <ClientSideEvents Closing="closing" />
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl >
    </div>
    </form>
</body>
</html>