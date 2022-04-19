<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_Ekzekutim.aspx.cs"
    Inherits="PlatinumWeb.Shto_Ekzekutim" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxnb" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>

<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server"
        id="themeJQuery" />
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
        <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/bootstrap-3.3.6-dist/js/bootstrap.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/jquery.ui.datepicker-sq.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/memoryObject.js;~/js/async.min.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/arkiva.js;~/js/myNrAuto-IMB.2.1.js;~/JsGlobal.js;~/js/myCookies-IMB.2.1.js;~/fine-uploader/jquery.fine-uploader.js;~/js/multiOpenAccordion-IMB.2.1.js;~/js/toolbar.js;~/js/aspx.js/Shto_Ekzekutim.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
</head>
<body>
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
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
            </dx:ASPxHiddenField>
            <asp:HiddenField ID="hfTmpColMag" runat="server" />
            <%-- Hidden fields per Arkiven--%>
            <asp:HiddenField ID="hfArkivaDokId" runat="server" />
            <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
            </dx:ASPxHiddenField>
            <asp:HiddenField ID="hfTeDrejtaArtImazhe" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                    OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                    <ClientSideEvents ItemClick="function(s, e) { menu_click(s,e); }"
                                        Init="function(s) {s.SetClientVisible(true);}" />
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
                                                <ClientSideEvents Init="function(s,e){$('#dvMenu').show();myMesazh.InicializoTimer();}" />
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
                                                                <ClientSideEvents Click="function(s, e) {
	popFshi.Hide();
    Utils.shfaqLoadingGif();;
}" />
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
                                </dx:ASPxPanel>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popMesazhQK" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                        ClientInstanceName="popMesazhQK" CloseAction="None" EnableAnimation="False" EnableViewState="False"
                        Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" Width="300px" ShowCloseButton="False">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl7" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel17" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent17" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel Wrap="true" ID="lblMsgbox4" runat="server" ClientIDMode="AutoID" Text="Deshironi te beni shperndarjen ne qendrat e kostos?"
                                                ClientInstanceName="lblmesazhqendra">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOkQK" runat="server" CausesValidation="False" ClientInstanceName="ButtonOkQK"
                                                                AutoPostBack="false" Text="Po">
                                                                <ClientSideEvents Click="function(s, e) {
	popMesazhQK.Hide();
  hapPopUp(s,e);
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancelQK" runat="server" ClientIDMode="AutoID" Text="Jo"
                                                                AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s, e) {
		popMesazhQK.Hide();
        JopopupClick(s,e);
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxPanel>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popKonvertuar" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                        ClientInstanceName="popKonvertuar" CloseAction="CloseButton" EnableAnimation="False"
                        EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" Width="300px">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl433" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel133" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent133" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel Wrap="True" ID="lblMsgboxKonv" runat="server" ClientInstanceName="lblMsgboxKonv" ClientIDMode="AutoID" Text="Jeni i sigurt?">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOk5" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                                OnClick="ButtonOk5_Click" Text="Po">
                                                                <ClientSideEvents Click="function(s, e) {
	popKonvertuar.Hide();
    Utils.shfaqLoadingGif();;
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancel5" runat="server" ClientIDMode="AutoID" Text="Jo" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s, e) {
		popKonvertuar.Hide();
                                                                    click = false;
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxPanel>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="hfRuajDraft" runat="server" />
                    <asp:HiddenField ID="hfqkmesazhi" runat="server" Value="jo" />
                    <asp:HiddenField ID="hfUrl" runat="server" />
                    <asp:HiddenField ID="hfStatusRuajtje" runat="server" />
                    <asp:HiddenField ID="hfKategoria" runat="server" />
                    <asp:HiddenField ID="hfKodi" runat="server" />
                    <asp:HiddenField ID="hfEmertimi" runat="server" />
                    <asp:HiddenField ID="hfDetajimet" runat="server" />
                    <asp:HiddenField ID="hfNjesia" runat="server" />
                    <asp:HiddenField ID="hfSasia" runat="server" />
                    <asp:HiddenField ID="hfCmimi" runat="server" />
                    <asp:HiddenField ID="hfMagazina" runat="server" />
                    <asp:HiddenField ID="hfVlefta" runat="server" />
                    <asp:HiddenField ID="hfMagazina2" runat="server" />
                    <asp:HiddenField ID="hfSkemaKontabel" runat="server" />
                    <asp:HiddenField ID="hfDetajimetSelektuara" runat="server" />
                    <asp:HiddenField ID="hfLupaKlientFurnitor" runat="server" />
                    <asp:HiddenField ID="hfLupaMagazina" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfAutorizimi" runat="server" />
                    <asp:HiddenField ID="gridDataObject" runat="server" />
                    <asp:HiddenField ID="gridDataObject2" runat="server" />
                    <asp:HiddenField ID="proveObjekt2" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaInfoArt" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="730px" ClientInstanceName="splitter"
                PaneMinSize="700px">
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
                                    <div id="bootPopUp" class="bootstrap-iso">
                                    </div>
                                    <div id="accordition">
                                        <div>
                                            <h3 id="kokeKonfigurimi"><span id="kokeKonfigurimidiv" class="ui-not-accordion-header-text">Koke Dokumenti:</span></h3>
                                            <div>
                                                <table id="tblFillim" class="renditKontrolle">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                                <br />
                                                <br />
                                                <div id="dvgvFaturat" class="atributeDiveFshehur">
                                                    <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" ClientIDMode="AutoID" Width="100%" AllowResize="True"
                                                        ClientInstanceName="nvFatura">
                                                        <Groups>
                                                            <dx:NavBarGroup Text="Planifikime Prodhimi" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px"
                                                                HeaderStyle-ForeColor="Gray" Expanded="True">
                                                                <HeaderStyle Font-Bold="True" Font-Size="14px" ForeColor="Gray"></HeaderStyle>
                                                                <ContentTemplate>
                                                                    <dx:ASPxGridView ID="grid_faturat" runat="server" ClientInstanceName="grid_faturat"
                                                                        Settings-ShowGroupPanel="false" Width="100%" OnCustomCallback="grid_faturat_CustomCallback"
                                                                        OnDataBound="grid_faturat_DataBound" OnProcessColumnAutoFilter="grid_faturat_ProcessColumnAutoFilter"
                                                                        OnCustomJSProperties="grid_faturat_CustomJSProperties"
                                                                        OnAfterPerformCallback="grid_faturat_AfterPerformCallback" OnHtmlDataCellPrepared="grid_faturat_HtmlDataCellPrepared"                                                                        
                                                                        BeginCallback="function(s, e) {	BeginCallback(s,e); }">
                                                                        <ClientSideEvents SelectionChanged="function(s,e){SelectionChangedGridFaturat(s, e);}" />
                                                                        <Styles>
                                                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                                            </Header>
                                                                        </Styles>
                                                                        <StylesEditors>
                                                                            <CalendarHeader Spacing="1px">
                                                                            </CalendarHeader>
                                                                            <ProgressBar Height="25px">
                                                                            </ProgressBar>
                                                                        </StylesEditors>
                                                                    </dx:ASPxGridView>
                                                                </ContentTemplate>
                                                            </dx:NavBarGroup>
                                                        </Groups>
                                                    </dx:ASPxNavBar>
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                            <h3 id="trupKonfigurimi"><span id="trupKonfigurimidiv" class="ui-not-accordion-header-text">Trup Dokumenti</span></h3>
                                            <div>
                                                <div id="divgride3" style="display: none">
                                                    <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" ClientInstanceName="PageControl"
                                                        TabSpacing="3px" Width="100%" ActiveTabIndex="0">
                                                        <ContentStyle>
                                                            <border bordercolor="#AECAF0" borderstyle="Solid" borderwidth="1px" />
                                                        </ContentStyle>
                                                        <TabPages>
                                                            <dxtc:TabPage Name="Produktet" Text="Produktet">
                                                                <ContentCollection>
                                                                    <dxw:ContentControl ID="ContentControl1" runat="server">
                                                                        <div id="divgride1" style="display: none">
                                                                            <div id="divgride2">
                                                                                <table id="rowed5">
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </dxw:ContentControl>
                                                                </ContentCollection>
                                                            </dxtc:TabPage>
                                                            <dxtc:TabPage Name="Recepturat" Text="Recepturat">
                                                                <ContentCollection>
                                                                    <dxw:ContentControl ID="ContentControl2" runat="server">
                                                                        <div id="divgride4" style="display: none; width: 100%">
                                                                            <div id="divgride5">
                                                                                <table id="rowed6">
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </dxw:ContentControl>
                                                                </ContentCollection>
                                                            </dxtc:TabPage>
                                                        </TabPages>
                                                        <ClientSideEvents ActiveTabChanged="function(s,e){activeTabsChanged(s,e);}" />
                                                    </dxtc:ASPxPageControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                            <h3 id="fundKonfigurimi"><span id="fundKonfigurimidiv" class="ui-not-accordion-header-text">Fund Dokumenti</span></h3>
                                            <div>
                                                <table id="tblFund" class="renditKontrolle" align="right">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="dvFillim" class="atributeDiveFshehur">
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                            runat="server" Text="Lloji:" ClientInstanceName="konfigurimi_Label">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                            ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top" AnimationType="None">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi"
                                            ClientInstanceName="lblKonfigurimi" Text="">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneMagazina" ID="lblMagazina" runat="server"
                                            Text="Magazina" ClientInstanceName="lblMagazina">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="btneMagazina" ClientInstanceName="btneMagazina" runat="server"
                                            ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top"
                                            Width="100%">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickMagazina('mag1');}" TextChanged="function(s,e){TextChangedMagazina('mag1');}" />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneMagazin2a" ID="lblMagazin2a"
                                            runat="server" Text="Magazina" ClientInstanceName="lblMagazin2a">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="btneMagazin2a" ClientInstanceName="btneMagazin2a" runat="server"
                                            ShowShadow="False" Width="100%" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickMagazina('mag2');}" TextChanged="function(s,e){TextChangedMagazina('mag2');}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrDok" ID="lblNrDok" runat="server"
                                            Text="Nr dokumenti:" ClientInstanceName="lblNrDok">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtNrDok" runat="server" ClientInstanceName="txtNrDok" Width="100%">
                                            <ClientSideEvents Init="function(s, e) { }" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtDok" ID="lblDtDok" runat="server"
                                            Text="Dt Dokumenti:" ClientInstanceName="lblDtDok">
                                        </dx:ASPxLabel>
                                        <dx:ASPxDateEdit ID="dteDtDok" runat="server" ClientInstanceName="dteDtDok" ShowShadow="False"
                                            Width="100%" AllowMouseWheel="false">
                                            <ClientSideEvents DateChanged="function (s,e){DateChanged(s,e);}" GotFocus="function(s, e){ dateGotFocus( s, e); }" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime" ID="lblShenime" runat="server"
                                            Text="Shenime" ClientInstanceName="lblShenime">
                                        </dx:ASPxLabel>
                                        <dx:ASPxMemo ID="txtShenime" runat="server" ClientInstanceName="txtShenime" Width="100%"
                                            Rows="3">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                            <ClientSideEvents LostFocus="function(s,e){}" />
                                        </dx:ASPxMemo>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNjesiProdhimi" ID="lblNjesiProdhimi" runat="server"
                                            Text="Magazina" ClientInstanceName="lblNjesiProdhimi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbNjesiProdhimi" ClientInstanceName="cmbNjesiProdhimi" runat="server"
                                            ShowShadow="False" Width="100%" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickNjesiProdhimi();}" TextChanged="function(s,e){TextChangedNjesiProdhimi();}" />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                    </div>

                                    <br />

                                    <br />
                                    <div id="dvFundi" class="atributeDiveFshehur">
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtRegjistrimi" ID="lblDtRegjistrimi"
                                            runat="server" Text="Dt Regjistrimi:" ClientInstanceName="lblDtRegjistrimi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxDateEdit ID="dteDtRegjistrimi" runat="server" ClientInstanceName="dteDtRegjistrimi"
                                            ShowShadow="False" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTotali" ID="lblTotali" runat="server"
                                            Text="Totali" ClientInstanceName="lblTotali">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtTotali" runat="server" ClientInstanceName="txtTotali"
                                            Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGrup1" ID="lblGrup1" runat="server"
                                            Text="Grupim 1:" ClientInstanceName="lblGrup1">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbGrup1" runat="server" ClientInstanceName="cmbGrup1" ShowShadow="False"
                                            SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGrup2" ID="lblGrup2" runat="server"
                                            Text="Grupim 2:" ClientInstanceName="lblGrup2">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbGrup2" runat="server" ClientInstanceName="cmbGrup2" ShowShadow="False"
                                            SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGrup3" ID="lblGrup3" runat="server"
                                            Text="Grupim 3:" ClientInstanceName="lblGrup3">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbGrup3" runat="server" ClientInstanceName="cmbGrup3" ShowShadow="False"
                                            SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <iframe id="Container" runat="server" frameborder="0" name="Container" height="0"
                                                width="0"></iframe>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <%-- Navigation pane --%>
                    <dx:SplitterPane MaxSize="300px" ShowCollapseBackwardButton="True" Separators-Size="10px"
                        PaneStyle-BackColor="Transparent" Collapsed="True" ShowCollapseForwardButton="True"
                        ScrollBars="Auto" AllowResize="True" MinSize="80px" AutoWidth="false" AutoHeight="false">
                        <Separators Size="10px">
                        </Separators>

                        <PaneStyle BackColor="Transparent"></PaneStyle>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 100%; vertical-align: top;">
                                    <tr>
                                        <td align="center" valign="top">
                                            <asp:UpdatePanel ID="pnl2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>

                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="center" style="width: 50%">
                                                                <dx:ASPxButton ID="btnMbyllur" runat="server" Text="-" Width="100%" AutoPostBack="false"
                                                                    Height="25px" Font-Size="9" Font-Bold="true" ToolTip="Mos shfaq info">
                                                                    <ClientSideEvents Click="function (s,e){RuajHapurMbyllurminus(false)}" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td align="center" style="width: 50%">
                                                                <dx:ASPxButton ID="btnHapur" runat="server" Text="+" Width="100%" AutoPostBack="false"
                                                                    Height="25px" Font-Size="9" ToolTip="Shfaq info">
                                                                    <ClientSideEvents Click="function (s,e){RuajHapurMbyllurplus(true)}" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>

                                                    </table>

                                                    <dxnb:ASPxNavBar ID="ASPxNavBar2" runat="server" ClientInstanceName="navbar" Width="100%"
                                                        EnableAnimation="True" SyncSelectionMode="CurrentPath"
                                                        EnableClientSideAPI="True" AllowSelectItem="True" Font-Size="8pt">
                                                        <ClientSideEvents HeaderClick="function (s,e) { HeaderClick (s,e); }" ItemClick="function(s, e) {}"
                                                            ExpandedChanging="function (s,e) {Expanded();  }" />
                                                        <GroupHeaderTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="width: 50%; font-weight: bold; height: 12px;">
                                                                        <dx:ASPxLabel Wrap="False" ID="Label1" runat="server" Font-Size="8" Text='<%# Eval("Text") %>' />
                                                                    </td>
                                                                    <td style="width: 10%;">
                                                                        <dx:ASPxHyperLink ID="HyperLink2" runat="server" Text='<%# Eval("Name") %>' NavigateUrl="javascript:void(0)"
                                                                            ImageWidth="12px" EnableClientSideAPI="true" ImageHeight="12px" ImageUrl="~/images/new/flash.png" ClientSideEvents-Click="function (s,e){ ButtonClickNavBar(s);}"
                                                                            ClientSideEvents-Init="function (s,e){ KontrolloTeDrejta(s);}" DisabledStyle-BackColor="#CCCCCC" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </GroupHeaderTemplate>
                                                        <Groups>
                                                            <dxnb:NavBarGroup Text="Info Artikulli" Expanded="true" Name="Artikulli">
                                                                <ContentTemplate>
                                                                    <dx:ASPxListBox ID="lbxZgjedhur" runat="server" Width="100%" ClientInstanceName="lbxZgjedhur"
                                                                        Font-Size="8" SettingsLoadingPanel-ImagePosition="Top">
                                                                        <Columns>
                                                                            <dx:ListBoxColumn FieldName="Emri" Name="Emri" />
                                                                            <dx:ListBoxColumn FieldName="Vlera" Name="Vlera" />
                                                                        </Columns>
                                                                        <LoadingPanelImage>
                                                                        </LoadingPanelImage>
                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxListBox>
                                                                </ContentTemplate>
                                                            </dxnb:NavBarGroup>
                                                        </Groups>
                                                    </dxnb:ASPxNavBar>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
                <ClientSideEvents PaneCollapsed="function(s, e) { spliterPaneCollapsed(s,e);}" PaneExpanded="function(s, e) { spliterPaneCollapsed(s,e);}"
                    PaneCollapsing="function(s, e) { spliterPaneCollapsing(s,e);}" PaneExpanding="function(s, e) { spliterPaneExpanding(s,e);}" PaneResized="function(s, e) { spliterPaneCollapsed(s,e);}" />
            </dx:ASPxSplitter>
            <asp:HiddenField ID="HfKonfAmb" runat="server" />
            <asp:HiddenField ID="HfColNjesiArt" runat="server" />
            <asp:HiddenField ID="HfColNjesAdminis" runat="server" />
            <asp:HiddenField ID="HfColNjesAdminisDest" runat="server" />
            <asp:HiddenField ID="HfColTrupMag" runat="server" />
            <asp:HiddenField ID="HfColDetArt" runat="server" />
            <asp:HiddenField ID="HfColDetArt2" runat="server" />
            <asp:HiddenField ID="HfColArt" runat="server" />
            <asp:HiddenField ID="hfShtimModifikim" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HFStatusiDokumentit" runat="server" />
            <asp:HiddenField ID="hfKolonaGride" runat="server" />
            <asp:HiddenField ID="HfGridCol" runat="server" />
            <asp:HiddenField ID="hfKontabilizimi" runat="server" />
            <asp:HiddenField ID="hfKontrollRivleresim" runat="server" />
            <asp:HiddenField ID="hfFD" runat="server" />
            <asp:HiddenField ID="hfKonffillestar" runat="server" />
            <asp:HiddenField ID="hfFH" runat="server" />
            <asp:HiddenField ID="hfPlanifikime" runat="server" />
            <asp:HiddenField ID="hfLlogaria" runat="server" />
            <asp:HiddenField ID="hfLupaNjesiProdhimi" runat="server" />
            <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
            <asp:HiddenField ID="hfGridaKodi" runat="server" />
            <asp:HiddenField ID="hfGridaDetajimi" runat="server" />
            <asp:HiddenField ID="hfKontrolletNrAutom" runat="server" />
            <asp:HiddenField ID="hfAtributeNrAutom" runat="server" />
            <asp:HiddenField ID="hfKolonaSubGride" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            <asp:HiddenField ID="hfHapurMbyllur" runat="server" />
            <dx:ASPxHiddenField ID="hfNrAutoShitje" runat="server" ClientInstanceName="hfNrAutoShitje">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>

        </div>
        <br />
        <br />

        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                        CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                        <ClientSideEvents CloseUp="function(s, e) { closePopup(s,e);}" />
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popNdryshoNjesiProdhimi" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                        ClientInstanceName="popNdryshoNjesiProdhimi" CloseAction="CloseButton" EnableAnimation="False"
                        EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" Width="300px">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel2" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel Wrap="True" ID="ASPxLabel2" runat="server" ClientIDMode="AutoID"
                                                Text="Keni zgjedhur planifikime prodhimi me njesi prodhimi te ndryshme. Doni te vazhdoni?">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ASPxButton1" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                                Text="Po" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s, e) { ndryshoNjesiProdhimiPoClick(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ASPxButton2" runat="server" ClientIDMode="AutoID" Text="Jo">
                                                                <ClientSideEvents Click="function(s, e) {ndryshoNjesiProdhimiJoClick(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxPanel>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
