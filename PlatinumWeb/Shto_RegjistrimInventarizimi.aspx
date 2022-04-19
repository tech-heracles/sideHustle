<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_RegjistrimInventarizimi.aspx.cs" Inherits="PlatinumWeb.Shto_RegjistrimInventarizimi" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>







<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>






<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxnb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
     <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server"
        id="themeJQuery" />
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" media="screen" rel="stylesheet" type="text/css" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/myMesazh-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/json2.js;~/JsGlobal.js;~/js/myCookies-IMB.2.1.js;~/js/aspx.js/Shto_RegjistrimInventarizimi.aspx-IMB.5.4.js&v76""
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
                <ClientSideEvents EndCallback="function(s, e) { EndCallbackGlobalEvents(s, e); }" />
            </dx:ASPxGlobalEvents>
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
            <asp:HiddenField ID="hfTmpColMag" runat="server" />
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
                                    <ClientSideEvents ItemClick="function(s, e) { menu_click(s, e); }" Init="function(s) {s.SetClientVisible(true);}" />
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
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="true" ClientInstanceName="popFshi"
                        CloseAction="CloseButton" EnableAnimation="false" EnableViewState="false" HeaderText="Kujdes"
                        Font-Bold="true" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        Width="300px" ClientIDMode="AutoID" CssPostfix="Glass">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lblMsgbox" ClientInstanceName="lblMsgbox" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?">
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
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Red" ClientInstanceName="pergjigja"
                        ClientVisible="false">
                    </dx:ASPxLabel>
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="hfStatusRuajtje" runat="server" />
                    <asp:HiddenField ID="hfqkmesazhi" runat="server" Value="jo" />
                    <asp:HiddenField ID="hfUrl" runat="server" />

                    <asp:HiddenField ID="hfKodi" runat="server" />
                    <asp:HiddenField ID="hfEmertimi" runat="server" />

                    <asp:HiddenField ID="hfNjesia" runat="server" />
                    <asp:HiddenField ID="hfSasia" runat="server" />
                    <asp:HiddenField ID="hfCmimi" runat="server" />
                    <asp:HiddenField ID="hfMagazina" runat="server" />
                    <asp:HiddenField ID="hfVlefta" runat="server" />

                    <asp:HiddenField ID="hfSkemaKontabel" runat="server" />


                    <asp:HiddenField ID="hfLupaMagazina" runat="server" />

                    <asp:HiddenField ID="hfRuajDraft" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfAutorizimi" runat="server" />
                    <asp:HiddenField ID="gridDataObject" runat="server" />
                    <asp:HiddenField ID="proveObjekt2" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hfTeDrejtaInfoArt" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaArtRi" runat="server" />
            <asp:HiddenField ID="HfKonfAmb" runat="server" />
            <asp:HiddenField ID="HfColNjesiArt" runat="server" />
            <asp:HiddenField ID="HfColNjesAdminis" runat="server" />
            <asp:HiddenField ID="HfColTrupMag" runat="server" />
            <asp:HiddenField ID="HfColArt" runat="server" />
            <asp:HiddenField ID="hfShtimModifikim" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HFStatusiDokumentit" runat="server" />
            <asp:HiddenField ID="hfKolonaGride" runat="server" />
            <asp:HiddenField ID="HfGridCol" runat="server" />
            <asp:HiddenField ID="hfKontabilizimi" runat="server" />
            <asp:HiddenField ID="hfKontrollRivleresim" runat="server" />
            <asp:HiddenField ID="hfKonffillestar" runat="server" />
            <asp:HiddenField ID="hfLlogaria" runat="server" />
            <dx:ASPxHiddenField ID="hfArt" runat="server" ClientInstanceName="HfArt">
            </dx:ASPxHiddenField>

            <dx:ASPxHiddenField ID="hfIdGride" runat="server" ClientInstanceName="hfIdGride">
            </dx:ASPxHiddenField>
            <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
            <asp:HiddenField ID="hfGridaKodi" runat="server" />
            <asp:HiddenField ID="hfKontrolletNrAutom" runat="server" />
            <asp:HiddenField ID="hfAtributeNrAutom" runat="server" />
            <asp:HiddenField ID="hfHapurMbyllur" runat="server" />

            <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="730px" ClientInstanceName="splitter"
                PaneMinSize="600px">
                <Panes>
                    <%-- Header pane--%>
                    <dx:SplitterPane PaneStyle-BackColor="Transparent" Separators-Size="10px" ><%--ScrollBars="Auto"--%>
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
                                            </div>
                                        </div>
                                    </div>

                                    <div id="dvFillim" class="atributeDiveFshehur">
                                        <dx:ASPxLabel Wrap="False" ID="lblLloji" Text="boo" AssociatedControlID="cmbLloji"
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
                                        <dx:ASPxLabel Wrap="False" ID="konfigurimi_Label" AssociatedControlID="cmbKonfigurimi"
                                            runat="server" Text="Lloji:" ClientInstanceName="konfigurimi_Label">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" DropDownHeight="100" AnimationType="None">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="true"
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
                                        <dx:ASPxLabel Wrap="False" ID="lblMagazina" AssociatedControlID="btneMagazina" runat="server"
                                            Text="Magazina" ClientInstanceName="lblMagazina">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="btneMagazina" ClientInstanceName="btneMagazina"
                                            runat="server" ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){identifikuesMagazina='Mag1';ButtonClickMagazina();}" />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="true"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                          <%--     <div id="dvlblFormatiPrintimit">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblFormatPrintimi" AssociatedControlID="cmbFormatPrintimi"
                                            runat="server" Text="Formati i printimit" ClientInstanceName="lblFormatPrintimi">
                                        </dx:ASPxLabel>
                                        <%--   </div>
                                    <div id="dvcmbFormatiPrintimit">--%>
                                        <dx:ASPxComboBox ID="cmbFormatPrintimi" Width="100%" runat="server" ClientInstanceName="cmbFormatPrintimi"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblNrDok" AssociatedControlID="txtNrDok" runat="server"
                                            Text="Nr dokumenti:" ClientInstanceName="lblNrDok">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox Width="100%" ID="txtNrDok" runat="server" ClientInstanceName="txtNrDok">
                                            <ClientSideEvents Init="function(s, e) {  }" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="true"
                                                ValidationGroup="entries">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblPershkrimi" AssociatedControlID="txtPershkrimi"
                                            runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxMemo ID="txtPershkrimi" Width="100%" runat="server" ClientInstanceName="txtPershkrimi"
                                            Rows="3">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                            <ClientSideEvents LostFocus="function(s,e){myJQGrid.focusGrid(focusGridParams());}" />
                                        </dx:ASPxMemo>
                                        <dx:ASPxLabel Wrap="False" ID="lblSkano" AssociatedControlID="txtSkano"
                                            runat="server" Text="Skano:" ClientInstanceName="lblSkano">
                                        </dx:ASPxLabel>
                                        <dx:ASPxMemo ID="txtSkano" ClientVisible="false" Width="100%" runat="server" ClientInstanceName="txtSkano"
                                            Rows="3">
                                            <ClientSideEvents KeyDown="function(s,e){ KaloBarkod(s,e)}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                            <ClientSideEvents LostFocus="function(s,e){myJQGrid.focusGrid(focusGridParams());}" />
                                        </dx:ASPxMemo>
                                        <dx:ASPxLabel Wrap="False" ID="lblDtDok" AssociatedControlID="dteDtDok" runat="server"
                                            Text="Dt Dokumenti:" ClientInstanceName="lblDtDok">
                                        </dx:ASPxLabel>
                                        <dx:ASPxDateEdit Width="100%" ID="dteDtDok" runat="server" ClientInstanceName="dteDtDok"
                                            ShowShadow="False">
                                            <ClientSideEvents DateChanged="function (s,e){DateChanged(s,e);}" GotFocus="function(s, e){ dateGotFocus( s, e); }" />
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
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="true"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>
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

                                    </div>
                                    <dx:ASPxHiddenField ID="hfNrAutoShitje" runat="server" ClientInstanceName="hfNrAutoShitje">
                                    </dx:ASPxHiddenField>
                                    <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
                                    </dx:ASPxHiddenField>
                                    <br />
                                    <br />
                                    <div id="divfund1" style="visibility: hidden;">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <table width="100%">

                                                    <tr>
                                                        <td style="width: 50%">
                                                            <table>
                                                                <tr style="visibility: hidden">
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="btnPeriudha" runat="server" ClientInstanceName="btnPeriudha"
                                                                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                                                            <ClientSideEvents ButtonClick="function(s,
    e) {ShfaqPeriudhen();}"
                                                                                LostFocus="function(s, e) {lostFocusPeriudha(s.GetText());
    valueChangedPeriudha();}" />
                                                                            <LoadingPanelImage>
                                                                            </LoadingPanelImage>
                                                                            <DropDownButton>
                                                                                <Image>
                                                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                                                </Image>
                                                                            </DropDownButton>
                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxLabel Wrap="False" ID="lblPeriudhaAktuale" ClientInstanceName="lblPeriudhaAktuale"
                                                                            runat="server" Text="">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="right" style="width: 10%"></td>
                                                        <td align="right" style="width: 10%"></td>
                                                    </tr>
                                                </table>
                                                <iframe id="Container" runat="server" frameborder="0" height="0" name="Container"
                                                    width="0"></iframe>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
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

                                                <dxnb:ASPxNavBar ID="ASPxNavBar1" runat="server" ClientInstanceName="navbar" Width="100%"
                                                    EnableAnimation="True"  SyncSelectionMode="CurrentPath"
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
                                                                        ImageWidth="12px" EnableClientSideAPI="true" ImageHeight="12px" ImageUrl="~/images/new/flash.png" 
                                                                        ClientSideEvents-Click="function (s,e){ ButtonClickNavBar(s);}"
                                                                        ClientSideEvents-Init="function (s,e){ KontrolloTeDrejta(s);}" DisabledStyle-BackColor="#CCCCCC" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </GroupHeaderTemplate>
                                                    <Groups>                                                        
                                                        <dxnb:NavBarGroup Text="Info Artikulli" Expanded="false" Name="Artikulli">
                                                            <ContentTemplate>
                                                                <dx:ASPxListBox ID="lbxZgjedhur" Height="100%" runat="server" Width="100%" ClientInstanceName="lbxZgjedhur"
                                                                    Font-Size="8">
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
                <Styles>
                </Styles>
                <Images>
                </Images>
            </dx:ASPxSplitter >
        </div>
        <div>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
            </dx:ASPxHiddenField>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                <ClientSideEvents Closing="function(s, e) { popupUniversal.SetContentUrl('');}" />
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl >
        </div>
        <div id="dialog-shtoSasi" title="Shto Sasine">
           Sasia: <input id="shtoSasine" type="text" />           
        </div>
    </form>
</body>
</html>
