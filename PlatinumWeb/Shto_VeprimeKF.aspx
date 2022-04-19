<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_VeprimeKF.aspx.cs"
    Inherits="PlatinumWeb.Shto_VeprimeKF" %>

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
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxnb" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server"
        id="themeJQuery" />
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" media="screen" rel="stylesheet" type="text/css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/myMesazh-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/json2.js;~/JsGlobal.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.ui.datepicker-sq.js;~/js/aspx.js/Shto_VeprimeKF.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; height: 100%">

            <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                Font-Size="9pt" Modal="True" ImagePosition="Top">
                <LoadingDivStyle Opacity="30">
                </LoadingDivStyle>
            </dx:ASPxLoadingPanel>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                <%--<ClientSideEvents EndCallback="function(s,e){ }" 
                    ControlsInitialized="function (s,e) {if ($('#hfqkmesazhi').val() == 'shfaqmesazh' || $('#hfqkmesazhiVDK').val() == 'shfaqmesazh') {  popMesazhQK.Show();}}" />--%>
            </dx:ASPxGlobalEvents>
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
                                    <ClientSideEvents ItemClick="function(s, e) {
	                        menu_click(s,e);
                            }"
                                        Init="function(s) {s.SetClientVisible(true);}" />
                                    <ItemImage Height="32px" Width="32px">
                                    </ItemImage>
                                    <SubMenuItemImage Height="16px" Width="16px">
                                    </SubMenuItemImage>
                                    <ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" VerticalAlign="Middle">
                                        <Paddings PaddingBottom="1px" PaddingTop="9px" />
                                        <Paddings PaddingBottom="1px" PaddingTop="9px" />
                                        <Paddings PaddingBottom="1px" PaddingTop="9px" />
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
                                            <ClientSideEvents Init="function(s, e) {
	
}" />
                                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
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
                                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel Wrap="False" ID="lblMsgbox" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?">
                                            </dx:ASPxLabel>
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                                OnClick="ButtonOk_Click2" Text="Ok">
                                                                <ClientSideEvents Click="function(s, e) {
	popFshi.Hide();
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

                </ContentTemplate>
            </asp:UpdatePanel>
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
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <dx:ASPxLabel Wrap="False" ID="pergjigja" runat="server" Text="" ForeColor="Red"
                        ClientInstanceName="pergjigja">
                    </dx:ASPxLabel>
                    <%--<dx:ASPxLabel ID="status" runat="server" Text="false" ClientInstanceName ="statusNew">
        </dx:ASPxLabel>--%>
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="hfqkmesazhiVDK" runat="server" Value="jo" />
                    <asp:HiddenField ID="hfUrlVDK" runat="server" />
                    <asp:HiddenField ID="hfUrl" runat="server" />
                    <asp:HiddenField ID="hfqkmesazhi" runat="server" Value="jo" />
                    <asp:HiddenField ID="hfStatusRuajtje" runat="server" />
                    <asp:HiddenField ID="hfKodi" runat="server" />
                    <asp:HiddenField ID="hfEmertimi" runat="server" />
                    <asp:HiddenField ID="hfPershkrimi" runat="server" />
                    <asp:HiddenField ID="hfData" runat="server" />
                    <asp:HiddenField ID="hfDebiKredi" runat="server" />
                    <asp:HiddenField ID="hfVlefta" runat="server" />
                    <asp:HiddenField ID="hfMonedha" runat="server" />
                    <asp:HiddenField ID="hfKursi" runat="server" />
                    <asp:HiddenField ID="hfVleftaMon" runat="server" />
                    <asp:HiddenField ID="hfNrFatura" runat="server" />
                    <asp:HiddenField ID="hfDataFatura" runat="server" />
                    <asp:HiddenField ID="hfVleraFatura" runat="server" />
                    <asp:HiddenField ID="hfVleramonFatura" runat="server" />
                    <asp:HiddenField ID="hfFatura" runat="server" />
                    <asp:HiddenField ID="hfNivele" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="gridDataObject" runat="server" />
                    <asp:HiddenField ID="hfLlogKunderparti" runat="server" />
                    <asp:HiddenField ID="hfSkemaKontabel" runat="server" />
                    <asp:HiddenField ID="hfDetajimetSelektuara" runat="server" />
                    <asp:HiddenField ID="hfMonedhaNder" runat="server" />
                    <asp:HiddenField ID="hfLupaKlientFurnitor" runat="server" />
                    <asp:HiddenField ID="hfLlogaria" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfAutorizimi" runat="server" />
                    <asp:HiddenField ID="HfColTrupBanka" runat="server" />
                    <asp:HiddenField ID="HfColKF" runat="server" />
                    <asp:HiddenField ID="HfColLlog" runat="server" />
                    <asp:HiddenField ID="HfColKfKundra" runat="server" />
                    <asp:HiddenField ID="HfColFatShitje" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKFRi" runat="server" />
                    <asp:HiddenField ID="HfGridCol" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaInfoKF" runat="server" />
                    <asp:HiddenField ID="hfHapurMbyllur" runat="server" />
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

                                    <div id="accordition">
                                        <div>
                                            <h3 id="kokeKonfigurimi"><span id="kokeKonfigurimidiv" class="ui-not-accordion-header-text">Koke Dokumenti:</span></h3>
                                            <div>
                                                <table id="tblKonfigurimi" runat="server">
                                                </table>
                                                <table id="tblFillim" class="renditKontrolle">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div>
                                            <h3 id="trupKonfigurimi"><span id="trupKonfigurimidiv" class="ui-not-accordion-header-text">Trup Dokumenti</span></h3>
                                            <div>
                                                <div id="divgride1" style="display: none">
                                                    <div id="divgride2">
                                                        <table id="rowed5">
                                                        </table>
                                                    </div>
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
                                                <br />
                                                <br />
                                                <div id="dvgrid_faturat" style="display: none">
                                                    <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" ClientIDMode="AutoID" Width="100%"
                                                        ClientSideEvents-HeaderClick="function(s, e){nvFaturaClick(s,e);}" 
                                                        ClientInstanceName="nvFatura">
                                                        <Groups>
                                                            <dx:NavBarGroup Text="Faturat" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px"
                                                                HeaderStyle-ForeColor="Gray" Expanded="True">
                                                                <HeaderStyle Font-Bold="True" Font-Size="14px" ForeColor="Gray"></HeaderStyle>
                                                                <ContentTemplate>
                                                                    <dx:ASPxGridView ID="grid_faturat" runat="server" ClientInstanceName="grid_faturat"
                                                                        Settings-ShowGroupPanel="false" Width="100%" OnDataBound="grid_faturat_DataBound"
                                                                        OnCustomCallback="grid_faturat_CustomCallback" OnHtmlDataCellPrepared="grid_faturat_HtmlDataCellPrepared"
                                                                        OnProcessColumnAutoFilter="grid_faturat_ProcessColumnAutoFilter" OnCustomJSProperties="grid_faturat_CustomJSProperties"
                                                                        OnAfterPerformCallback="grid_faturat_AfterPerformCallback">
                                                                        <ClientSideEvents SelectionChanged="function(s,e){SelectionChangedGridFaturat(s,e);}" />
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
                                    </div>

                                    <div id="dvFillim" class="atributeDiveFshehur">
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLloji" ID="lblLloji" runat="server"
                                            ClientInstanceName="lblLloji">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji" ShowShadow="False"
                                            SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){TextChangedLloji();}" />
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
                                        <dx:ASPxLabel ID="konfigurimi_Label" AssociatedControlID="cmbKonfigurimi" runat="server"
                                            Text="Lloji:" ClientInstanceName="konfigurimi_Label">
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

                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbPrinto" ID="lblPrinto" runat="server"
                                            Text="Printo:" ClientInstanceName="lblPrinto">
                                        </dx:ASPxLabel>
                                        <dx:ASPxCheckBox ID="cbPrinto" runat="server" ClientInstanceName="cbPrinto" Width="100%">
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                            <ClientSideEvents LostFocus="function(s,e){myJQGrid.focusGrid({emergride: '#rowed5', isLidhur: lidhur, idKoloneGrideFokus: arrayIdKolonaGrides[0]});}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxCheckBox>

                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrDok" ID="lblNrDok" runat="server"
                                            Text="Nr dokumenti:" ClientInstanceName="lblNrDok">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtNrDok" runat="server" ClientInstanceName="txtNrDok" Width="100%">
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
                                            Text="Date:" ClientInstanceName="lblDtDok">
                                        </dx:ASPxLabel>
                                        <dx:ASPxDateEdit ID="dteDtDok" runat="server" ClientInstanceName="dteDtDok" ShowShadow="False"
                                            Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                            <ClientSideEvents DateChanged="function(s,e){ DateChange();}" GotFocus="function(s, e){ dateGotFocus( s, e); }" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlogariKunderParti" ID="lblLlogariKunderParti"
                                            runat="server" Text="Llogari kunderparti" ClientInstanceName="lblLlogariKunderParti">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbLlogariKunderParti" ClientInstanceName="cmbLlogariKunderParti"
                                            runat="server" Width="100%" ShowShadow="False" Style="margin-bottom: 0px"
                                            EnableCallbackMode="True" SettingsLoadingPanel-ImagePosition="Top" OnItemRequestedByValue="cmbLlogariKunderParti_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbLlogariKunderParti_ItemsRequestedByFilterCondition">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickLlogaria();}" LostFocus="function(s, e) {TextChanged();}"
                                                TextChanged="function(s,e){ TextChanged(s,e); }" />
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKFKunderParti" ID="lblKFKunderParti"
                                            runat="server" Text="Llogari kunderparti" ClientInstanceName="lblKFKunderParti">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbKFKunderParti" ClientInstanceName="cmbKFKunderParti"
                                            runat="server" Width="100%" ShowShadow="False" Style="margin-bottom: 0px" OnItemRequestedByValue="cmbKFKunderParti_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbKFKunderParti_ItemsRequestedByFilterCondition"
                                            EnableCallbackMode="True" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickFurnitori();}" LostFocus="function(s, e) {TextChangedKF();}"
                                                TextChanged="function(s,e){ TextChangedKF(s,e); }" />
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi" ID="lblPershkrimi"
                                            runat="server" Text="Pershkrimi" ClientInstanceName="lblPershkrimi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxMemo ID="txtPershkrimi" runat="server" ClientInstanceName="txtPershkrimi"
                                            Rows="3" Columns="35" Width="100%">
                                            <ClientSideEvents LostFocus="function(s,e){myJQGrid.focusGrid({emergride: '#rowed5', isLidhur: lidhur, idKoloneGrideFokus: arrayIdKolonaGrides[0]});}" TextChanged="function(s,e){ValueChangedPershkrimi();}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxMemo>
                                        <dx:ASPxLabel Wrap="False" ID="lblDegeAdministrative" AssociatedControlID="cmbDegeAdministrative"
                                            runat="server" Text="Inventarizimi:" ClientInstanceName="lblDegeAdministrative">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="cmbDegeAdministrative" runat="server" ClientInstanceName="cmbDegeAdministrative"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents TextChanged="function(s,e) {TextChangedDega();}" />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="True"
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
                                    <br />
                                    <br />

                                    <div id="dvFundi" class="atributeDiveFshehur">
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtRegjistrimi" ID="lblDtRegjistrimi"
                                            runat="server" Text="Dt Regjistrimi:" ClientInstanceName="lblDtRegjistrimi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxDateEdit ID="dteDtRegjistrimi" runat="server" ClientInstanceName="dteDtRegjistrimi"
                                            ShowShadow="False" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                                SetFocusOnError="true">
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVlefta" ID="lblTotali" runat="server"
                                            Text="Totali" ClientInstanceName="lblTotali">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtVlefta" runat="server" ClientInstanceName="txtVlefta" Width="100%">
                                            <ClientSideEvents Init="function(s,e){ Utils.initTxtNumber(s,e); }" GotFocus="function(s,e){ Utils.gotFocusTxtNumer(s,e); }"
                                                LostFocus="function(s,e){ Utils.lostFocusTxtNumer(s,e); }" />
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
                                    </div>

                                    <br />
                                    <div id="divfund1" style="visibility: hidden;">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <table width="90%">
                                                    <tr>
                                                        <td style="width: 50%"></td>
                                                        <td align="right" style="width: 10%">
                                                            <dx:ASPxButton ID="ruaj_Button" runat="server" Text="Ruaj" Width="100%" OnClick="ruaj_Button_Click"
                                                                ValidationGroup="entries">
                                                                <ClientSideEvents Click="function(s, e) {     myFaqeCelje.validim(s, e);
	RuajClick(s,e);
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td align="right" style="width: 10%">
                                                            <dx:ASPxButton ID="ruaj_draft" runat="server" Text="Ruaj si Draft" Width="100%"
                                                                ValidationGroup="entries" OnClick="ruaj_draft_Click">
                                                                <ClientSideEvents Click="function(s, e) { myFaqeCelje.validim(s, e);	RuajClick(s,e); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td align="right" style="width: 10%">
                                                            <dx:ASPxButton ID="pastro_Button" runat="server" Text="Pastro" Width="100%" ClientInstanceName="pastro_Button">
                                                                <ClientSideEvents Click="function (s,e){ PastroClick(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td align="right" style="width: 10%">
                                                            <dx:ASPxButton ID="anullo_Button" runat="server" Text="Lista" Width="100%" OnClick="anullo_Button_Click">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            <iframe id="Container1" runat="server" frameborder="0" height="0" name="Container1"
                                                width="0"></iframe>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <dx:ASPxLabel Wrap="False" ID="lblPeriudha" AssociatedControlID="btnPeriudha" runat="server"
                                        Text="Periudha kontabel" ClientInstanceName="lblPeriudha" ClientVisible="false">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="btnPeriudha" runat="server" ClientInstanceName="btnPeriudha"
                                        ClientVisible="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                        <ClientSideEvents LostFocus="function(s, e) { valueChangedPeriudha();}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" ID="lblPeriudhaAktuale" runat="server" Text="Periudha aktuale:"
                                        ClientInstanceName="lblPeriudhaAktuale" ClientVisible="false">
                                    </dx:ASPxLabel>
                                </asp:Panel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
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
                                                            <dxnb:NavBarGroup Text="Info Klient/Furnitori" Expanded="false" Name="KF">
                                                                <ContentTemplate>
                                                                    <dx:ASPxListBox ID="lbxKF" Height="100%" runat="server" 
                                                                        Width="100%" ClientInstanceName="lbxKF" Font-Size="8">
                                                                        <Columns>
                                                                            <dx:ListBoxColumn FieldName="Emri" Name="Emri" />
                                                                            <dx:ListBoxColumn FieldName="Vlera" Name="Vlera" />
                                                                        </Columns>
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
                 <ClientSideEvents PaneCollapsed="function(s, e) { spliterPaneResized(s,e);}" PaneExpanded="function(s, e) { spliterPaneResized(s,e);}" PaneCollapsing="function(s, e) { spliterPaneCollapsing(s,e);}" PaneExpanding="function(s, e) { spliterPaneExpanding(s,e);}" PaneResized="function(s, e) { spliterPaneResized(s,e);}" />
            </dx:ASPxSplitter>
        </div>
        <asp:HiddenField ID="hfShtimModifikim" runat="server" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HFStatusiDokumentit" runat="server" />
        <asp:HiddenField ID="hfKolonaGride" runat="server" />
        <asp:HiddenField ID="hfKontabilizimi" runat="server" />
        <asp:HiddenField ID="hfMeFatura" runat="server" />
        <asp:HiddenField ID="hfMeKF" runat="server" />
        <asp:HiddenField ID="hfKonffillestar" runat="server" />
        <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
        <asp:HiddenField ID="hfGridaKodi" runat="server" />
        <asp:HiddenField ID="hfKontrolletNrAutom" runat="server" />
        <asp:HiddenField ID="hfAtributeNrAutom" runat="server" />
        <asp:HiddenField ID="hfGridaDetajimi" runat="server" />
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfNrAutoShitje" runat="server" ClientInstanceName="hfNrAutoShitje">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
        </dx:ASPxHiddenField>
        <div>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                <ClientSideEvents CloseUp="Close_Up" />
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
