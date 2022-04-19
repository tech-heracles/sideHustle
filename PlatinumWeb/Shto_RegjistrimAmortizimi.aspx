<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Shto_RegjistrimAmortizimi.aspx.cs" Inherits="PlatinumWeb.Shto_RegjistrimAmortizimi" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>












<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="js/css/le-frog/jquery-ui.css" media="screen" rel="stylesheet" type="text/css"
        runat="server" id="themeJQuery" />
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <title>Alpha Web</title>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/jquery.ui.datepicker-sq.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/multiOpenAccordion-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/aspx.js/Shto_RegjistrimAmortizimi.aspx-IMB.4.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
       </asp:ScriptManager>
          
        <dx:ASPxLoadingPanel ID="LoadingPanel" ContainerElementID="UpdatePanel1" runat="server"
            ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <div>
            <asp:UpdatePanel runat="server" ID="pnl">
                <ContentTemplate>
                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                        ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                        OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                        <ClientSideEvents ItemClick="function(s, e) { menu_click(s,e);}" Init="function(s) {s.SetClientVisible(true);}" />
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
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popMesazhQK" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                        ClientInstanceName="popMesazhQK" CloseAction="CloseButton" EnableAnimation="False"
                        ShowCloseButton="false" EnableViewState="False" Font-Bold="true" HeaderText="Kujdes"
                        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        Width="300px">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl7" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel17" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent17" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel Wrap="False" ID="lblMsgbox4" runat="server" ClientIDMode="AutoID"
                                                Text="Deshironi te beni shperndarjen ne qendrat e kostos?">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOkQK" runat="server" CausesValidation="False" ClientInstanceName="ButtonOkQK"
                                                                AutoPostBack="false" Text="Po">
                                                                <ClientSideEvents Click="function(s, e) { showPopUpShperndarjeQK(s,e);}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancelQK" runat="server" ClientIDMode="AutoID" Text="Jo"
                                                                AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s, e) {
		popMesazhQK.Hide();
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
                                </dx:ASPxPanel >
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl >
                </ContentTemplate>
            </asp:UpdatePanel>
            <eo:ProgressBar ID="ProgressBar1" runat="server" Width="100%" OnRunTask="ProgressBar1_RunTask"
                ClientSideOnError="onTaskError" ClientSideOnValueChanged="onTaskRunning" ClientSideOnTaskDone="onTaskDone" BorderColor="Black"
                BorderStyle="Solid" BorderWidth="1px" ControlSkinID="None" IndicatorColor="LightBlue"
                ShowPercentage="True" Height="18px">
            </eo:ProgressBar>
            <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="730px" ClientInstanceName="splitter"
                PaneMinSize="700px">
                <Panes>
                    <%-- Header pane--%>
                    <dx:SplitterPane PaneStyle-BackColor="Transparent" Separators-Size="10px" ScrollBars="Vertical">
                        <Separators Size="10px">
                        </Separators>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                <asp:Panel ID="ContentPanel" runat="server">
                                    <asp:UpdatePanel ID="pnlLidhur" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table id='hl' runat="server">
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br />
                                    <div id="accordition">
                                        <div>
                                            <h3 id="kokeKonfigurimi"><span id="kokeKonfigurimidiv" class="ui-not-accordion-header-text">Koke Dokumenti:</span></h3>
                                            <div>
                                                <table id="tblKonfigurimi" runat="server">
                                                </table>
                                                <table id="tblAmortizimi" class="renditKontrolle">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                                <table class="CustomRenditKontrolleDy">
                                                    <tr align="right">
                                                        <td align="right">
                                                            <dx:ASPxButton ID="btnLlogarit" runat="server" Text="Llogarit Amortizim" Width="21%"
                                                                ClientInstanceName="btnLlogarit" AutoPostBack="False" CausesValidation="False" HorizontalAlign="Center">
                                                                <ClientSideEvents Click="function(s, e) { konrtolloRreshtaTeZgjedhur(s,e);}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>


                                                <br />
                                                <br />
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnZgjidhGjitha" runat="server" ToolTip="Zgjidh të gjitha në këtë faqe"
                                                                ClientInstanceName="btnZgjidhGjitha" Image-Url="images/check2.png" Image-Height="16px" AutoPostBack="False" CausesValidation="False">
                                                                <ClientSideEvents CheckedChanged="function(s, e) {
          }"
                                                                    Click="function(s, e) {  KlikoTeGjitha();
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe"
                                                                AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="function(s, e) { gvAsete.SelectRows(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnHiqZgjedhjen" runat="server" ToolTip="Hiq zgjedhjen në këtë faqe"
                                                                ClientInstanceName="btnHiqZgjedhjen" Image-Url="images/uncheck2.png" Image-Height="16px" AutoPostBack="False" CausesValidation="False">
                                                                <ClientSideEvents Click="function(s, e) {  HiqTeGjitha();
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dx:ASPxGridView ID="gvAsete" runat="server" ClientInstanceName="gvAsete"
                                                    OnDataBound="gvAsete_DataBound" OnAfterPerformCallback="gvAsete_AfterPerformCallback"
                                                    Width="100%" OnCustomJSProperties="gvAsete_CustomJSProperties">
                                                    <SettingsBehavior AllowSelectByRowClick="True" />
                                                    <SettingsLoadingPanel ImagePosition="Top" />
                                                    <ClientSideEvents SelectionChanged="function(s, e) { }" />
                                                    <ImagesEditors>
                                                        <DropDownEditDropDown>
                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                        </DropDownEditDropDown>
                                                        <SpinEditIncrement>
                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditIncrementImageHover_Aqua"
                                                                PressedCssClass="dxEditors_edtSpinEditIncrementImagePressed_Aqua" />
                                                        </SpinEditIncrement>
                                                        <SpinEditDecrement>
                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditDecrementImageHover_Aqua"
                                                                PressedCssClass="dxEditors_edtSpinEditDecrementImagePressed_Aqua" />
                                                        </SpinEditDecrement>
                                                        <SpinEditLargeIncrement>
                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeIncImageHover_Aqua"
                                                                PressedCssClass="dxEditors_edtSpinEditLargeIncImagePressed_Aqua" />
                                                        </SpinEditLargeIncrement>
                                                        <SpinEditLargeDecrement>
                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeDecImageHover_Aqua"
                                                                PressedCssClass="dxEditors_edtSpinEditLargeDecImagePressed_Aqua" />
                                                        </SpinEditLargeDecrement>
                                                    </ImagesEditors>
                                                    <Styles>
                                                        <LoadingPanel ImageSpacing="8px">
                                                        </LoadingPanel>
                                                    </Styles>
                                                    <StylesEditors>
                                                        <CalendarHeader Spacing="1px">
                                                        </CalendarHeader>
                                                        <ProgressBar Height="25px">
                                                        </ProgressBar>
                                                    </StylesEditors>
                                                </dx:ASPxGridView>
                                            </div>
                                        </div>

                                        <div>
                                            <h3 id="trupKonfigurimi"><span id="trupKonfigurimidiv" class="ui-not-accordion-header-text">Trup Dokumenti</span></h3>
                                            <div>
                                                <table id="tblFund" class="renditKontrolle" align="right">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                                <br />
                                                <br />
                                                <br />
                                                <dx:ASPxButton ID="btnXlsxExport" runat="server" ToolTip="Export to Xlsx" Image-Height="16px" Image-Url="images/xlsx24.png"
                                                    Font-Size="8" OnClick="btnXlsxExport_Click">
                                                    <ClientSideEvents Click="function(s, e) { clickExport(e) }" />
                                                </dx:ASPxButton>
                                                <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" Width="100%" ClientInstanceName="nvFatura">
                                                    <Groups>
                                                        <dx:NavBarGroup Text="Analitike" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px" Expanded="false"
                                                            HeaderStyle-ForeColor="Gray">
                                                            <HeaderStyle Font-Bold="True" Font-Size="14px" ForeColor="Gray"></HeaderStyle>
                                                            <ContentTemplate>
                                                                <dx:ASPxGridView ID="grid_faturat" runat="server" ClientInstanceName="grid_faturat"
                                                                    Width="100%" OnCustomCallback="grid_faturat_CustomCallback" OnDataBound="grid_faturat_DataBound"
                                                                    OnProcessColumnAutoFilter="grid_faturat_ProcessColumnAutoFilter" OnHtmlRowCreated="grid_faturat_HtmlRowCreated"
                                                                    OnCustomJSProperties="grid_faturat_CustomJSProperties" OnAfterPerformCallback="grid_faturat_AfterPerformCallback">
                                                                    <ClientSideEvents EndCallback="grid_faturatEndCallback" />
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
                                                                <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_faturat" ExportedRowType="All" />
                                                            </ContentTemplate>
                                                        </dx:NavBarGroup>
                                                    </Groups>
                                                </dx:ASPxNavBar>

                                            </div>
                                        </div>
                                    </div>

                                    <div id="dvFillim" class="atributeDiveFshehur">
                                        <dx:ASPxLabel Wrap="False" ID="lblLloji" Text="Nenkategoria" AssociatedControlID="cmbLloji"
                                            runat="server" ClientInstanceName="lblLloji">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){TextChangedLloji();}" />
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
                                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" BackColor="white"
                                            ClientInstanceName="lblKonfigurimi" Text="">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMagazina" ID="lblMagazina" runat="server" ClientInstanceName="lblMagazina"
                                            Text="Magazina">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbMagazina" runat="server" ShowShadow="False" Width="100%"
                                            ClientInstanceName="cmbMagazina" SettingsLoadingPanel-ImagePosition="Top">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickMagazina();}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbStandarti" ID="lblStandarti" runat="server"
                                            Text="Standarti:" ClientInstanceName="lblStandarti">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbStandarti" runat="server" ClientInstanceName="cmbStandarti" ShowShadow="False"
                                            Width="100%" SettingsLoadingPanel-ImagePosition="Top">
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
                                            Width="100%">
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
                                        <dx:ASPxLabel Wrap="False" ID="lblKrij" runat="server" AssociatedControlID="lblKrijuesi"
                                            ClientInstanceName="lblKrij" Text="Krijuesi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" ID="lblKrijuesi" runat="server" class="klasePerLblKonfigurimi" ClientInstanceName="lblKrijuesi"
                                            Text="">
                                        </dx:ASPxLabel>
                                    </div>
                                    <br />
                                    <br />
                                    <br />

                                    <div id="dvFundi" class="atributeDiveFshehur">
                                        <dx:ASPxLabel Wrap="False" ID="lblDtRegjistrimi" runat="server" Text="Dt Regjistrimi:"
                                            ClientInstanceName="lblDtRegjistrimi" AssociatedControlID="dteDtRegjistrimi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxDateEdit Width="100%" ID="dteDtRegjistrimi" runat="server" ClientInstanceName="dteDtRegjistrimi"
                                            ShowShadow="False">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
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
                                        <dx:ASPxLabel Wrap="False" ID="lblTotali" runat="server" Text="Totali" ClientInstanceName="lblTotali"
                                            AssociatedControlID="txtVlefta">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox Width="100%" ID="txtVlefta" runat="server" ClientInstanceName="txtVlefta">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
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
                                    <dx:ASPxHiddenField ID="hfNrAutoShitje" runat="server" ClientInstanceName="hfNrAutoShitje">
                                    </dx:ASPxHiddenField>
                                    <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
                                    </dx:ASPxHiddenField>
                                </asp:Panel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
                <Styles>
                </Styles>
                <Images>
                </Images>
            </dx:ASPxSplitter >
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" AllowResize="True"
                AppearAfter="10" ClientIDMode="AutoID" ClientInstanceName="popupUniversal" CloseAction="CloseButton"
                EnableAnimation="False" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter">
                <ClientSideEvents Closing="function(s, e) {
	popupUniversal.SetContentUrl('');
}" />
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl >
        </div>
        <asp:UpdatePanel ID="pnlhf" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfGrupKontabilizimi" runat="server" />
                <asp:HiddenField ID="hfNrLlogaria" runat="server" />
                <asp:HiddenField ID="hfEmerLlogaria" runat="server" />
                <asp:HiddenField ID="hfPershkrimi" runat="server" />
                <asp:HiddenField ID="hfMonedha" runat="server" />
                <asp:HiddenField ID="hfKursi" runat="server" />
                <asp:HiddenField ID="hfDK" runat="server" />
                <asp:HiddenField ID="hfDebi" runat="server" />
                <asp:HiddenField ID="hfKredi" runat="server" />
                <asp:HiddenField ID="hfDebiMon" runat="server" />
                <asp:HiddenField ID="hfKrediMon" runat="server" />
                <asp:HiddenField ID="hfSkemaKontabel" runat="server" />
                <asp:HiddenField ID="hfKolonaGride" runat="server" />
                <asp:HiddenField ID="HfGridCol" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <%-- per te ruajtur vlerat e konfigurimit te  formati te numrave--%>
                <asp:HiddenField ID="hfFormatNr" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfKontrollet" runat="server" />
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfMonedhaNder" runat="server" />
                <asp:HiddenField ID="HfColTrup" runat="server" />
                <asp:HiddenField ID="HfColLlog" runat="server" />
                <asp:HiddenField ID="HfColMon" runat="server" />
                <asp:HiddenField ID="hfKontrolletNrAutom" runat="server" />
                <asp:HiddenField ID="hfAtributeNrAutom" runat="server" />
                <asp:HiddenField ID="hfStatus" runat="server" />
                <asp:HiddenField ID="hfqkmesazhi" runat="server" Value="jo" />
                <asp:HiddenField ID="hfUrl" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- per te ruajtur vlerat e konfigurimit te lupave, merret nga konfig i dok me web service --%>
        <asp:HiddenField ID="hfLupaDokumenti" runat="server" />
        <asp:HiddenField ID="hfLupaMagazina" runat="server" />
        <asp:HiddenField ID="hfLupaSkemaFK" runat="server" />
        <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
        <asp:HiddenField ID="hfGridaLlogaria" runat="server" />
        <asp:HiddenField ID="gridDataObject" runat="server" />
        <asp:HiddenField ID="hfAzhornim" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
        <asp:HiddenField ID="hfPerdoruesAktual" runat="server" />
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
        </dx:ASPxHiddenField>
    </form>
</body>
</html>
