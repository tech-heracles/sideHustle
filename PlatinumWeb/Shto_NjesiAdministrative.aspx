<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_NjesiAdministrative.aspx.cs"
    Inherits="PlatinumWeb.Shto_NjesiAdministrative" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>




<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ucFushatShtese.ascx" TagPrefix="uc1" TagName="ucFushatShtese" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/Shto_NjesiAdministrative.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){if(Utils.getUrlVar('vjenNga') !== 'GIS') window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                OnItemClick="ASPxMenu1_ItemClick">
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
                 
                <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                    Font-Size="9pt" Modal="True" ImagePosition="Top">
                    <LoadingDivStyle Opacity="30">
                    </LoadingDivStyle>
                </dx:ASPxLoadingPanel>
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

                 <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshiStatus" runat="server" AllowDragging="True" ClientInstanceName="popFshiStatus"
                    CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes"
                    Font-Bold="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    Width="300px" ClientIDMode="AutoID" CssPostfix="Glass">
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl14" runat="server">
                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel11" runat="server" ClientIDMode="AutoID" Width="271px">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent11" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblMsgbox1" runat="server" ClientIDMode="AutoID" Text="Statusi do te fshihet, jeni te sigurte?">
                                        </dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <div style="text-align: right;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonOk1" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk1"
                                                            Text="Ok" AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s, e) {    
	popFshiStatus.Hide();

                                                                gvStatus.PerformCallback(indexStatus);
}" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonCancel1" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                            <ClientSideEvents Click="function(s, e) {
		popFshiStatus.Hide();
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
        <div id="dvMagazina" style="display: none">
            <dx:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server"   TabSpacing="3px"
                ClientInstanceName="PageControl" Width="100%" Height="600px" ActiveTabIndex="1">
                <ContentStyle>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                </ContentStyle>
                <TabPages>
                    <dx:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                        <ContentCollection>
                            <dx:ContentControl>
                                <table class="renditKontrolle">
                                    <tr>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                runat="server" Text="Modeli:" Style="font-size: large">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth33">
                                            <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                ShowShadow="False" ValueType="System.String" Height="24px" Style="font-size: medium"
                                                SettingsLoadingPanel-ImagePosition="Top" Width="100%" AnimationType="None">
                                                <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
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
                                        </td>
                                        <td class="renditKontrolleLabelMeWidth33">
                                            <dx:ASPxLabel ID="lblKonfigurimi" runat="server" Text="" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth33"></td>
                                    </tr>
                                </table>
                                <dx:ASPxGridView ID="gvNjesiAdm" ClientInstanceName="gvNjesiAdm" runat="server"
                                    Width="100%" OnDataBound="gvNjesiAdm_DataBound" OnAfterPerformCallback="gvNjesiAdm_AfterPerformCallback"
                                    OnHeaderFilterFillItems="gvNjesiAdm_HeaderFilterFillItems" OnProcessColumnAutoFilter="gvNjesiAdm_ProcessColumnAutoFilter"
                                    OnCustomJSProperties="gvNjesiAdm_CustomJSProperties" OnCustomCallback="gvNjesiAdm_CustomCallback"
                                    OnAutoFilterCellEditorInitialize="gvNjesiAdm_AutoFilterCellEditorInitialize" OnPreRender="gvNjesiAdm_PreRender">
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <Templates>
                                        <TitlePanel>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ASPxButton2" runat="server" ToolTip="Zgjidh kolonat" AutoPostBack="false"
                                                            ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                            <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s,e,gvNjesiAdm)}"
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
                                                            <ClientSideEvents Click="function(s, e) { gvNjesiAdm.SelectAllRowsOnPage(); }" />
                                                        </dx:ASPxButton>

                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe"
                                                            AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) { gvNjesiAdm.SelectRows(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="gridaUnSelectTeGjitha" runat="server" ToolTip="Fshi Zgjedhjen"
                                                            AutoPostBack="false" Image-Url="images/uncheck2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) { gvNjesiAdm.UnselectRows(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnXlsxExport" runat="server" ToolTip="Export to Xlsx" OnClick="btnXlsxExport_Click" Image-Height="16px" Image-Url="images/xlsx24.png"
                                                            Font-Size="8">
                                                            <ClientSideEvents Click="function(s, e) { clickExport(e) }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnPdfExport" runat="server" OnClick="btnPdfExport_Click" ToolTip="Export to Pdf" Image-Height="16px" Image-Url="images/pdf_icon.png"
                                                            Font-Size="8">
                                                            <ClientSideEvents Click="function(s, e) { clickExport(e) }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                    </Templates>
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
                                    <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                        SelectionChanged="function(s, e){OnGridSelectionChanged(e);}" FocusedRowChanged="function(s, e) {
            mbush=true;	
}"
                                        BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gvNjesiAdm"
                                    ExportedRowType="Selected" />
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="Magazina" Text="Magazina">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl3" runat="server">
                                <table id="tblMagazina" class="renditKontrolle">
                                    <tbody>
                                    </tbody>
                                </table>
                                <%--<div id="dvlblKodi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                    Text="Kodi:" ClientInstanceName="lblKodi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtxtKodi"> --%>
                                <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi">
                                    <ClientSideEvents TextChanged="function(s, e) {	kontrolloKodMagazine(); }" />
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                        ValidationGroup="entries" SetFocusOnError="true" RegularExpression-ValidationExpression="^[\s\S]{0,20}$"
                                        RegularExpression-ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere">
                                        
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere" ValidationExpression="^[\s\S]{0,20}$"></RegularExpression>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>
                                <%--</div> --%>
                                <%-- <div id="dvlblPershkrimi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi" ID="lblPershkrimi"
                                    runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtxtPershkrimi"> --%>
                                <dx:ASPxMemo ID="txtPershkrimi" runat="server" Width="100%" AutoPostBack="false"
                                    ClientInstanceName="txtPershkrimi">
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
                                  <dx:ASPxLabel Wrap="False" ID="lblPershkrimDege" AssociatedControlID="lblPershkrimDege" runat="server"
                                            Text="Pershkrim dege" ClientInstanceName="lblPershkrimDege">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtPershkrimDege" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtPershkrimDege">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                <%--</div> --%>
                                <%-- <div id="dvlblAdresa">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtAdresa" ID="lblAdresa" runat="server"
                                    Text="Adresa:" ClientInstanceName="lblAdresa">
                                </dx:ASPxLabel>
                                <%--</div> --%>
                                <%--<div id="dvtxtAdresa"> --%>
                                <dx:ASPxMemo ID="txtAdresa" runat="server" ClientInstanceName="txtAdresa" Width="100%">
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
                                <%--</div> --%>
                                <%--<div id="dvlblInventarizimi"> --%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbInventarizimi" ID="lblInventarizimi"
                                    runat="server" Text="Inventarizimi:" ClientInstanceName="lblInventarizimi">
                                </dx:ASPxLabel>
                                <%--</div> --%>
                                <%--<div id="dvcmbInventarizimi"> --%>
                                <dx:ASPxComboBox ID="cmbInventarizimi" runat="server" ClientInstanceName="cmbInventarizimi"
                                    ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
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
                                <%--</div> --%>
                                <%-- <div id="dvlblAktiv">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbAktiv" ID="lblAktiv" runat="server"
                                    Text="Aktiv:" ClientInstanceName="lblAktiv">
                                </dx:ASPxLabel>
                                <%--</div> --%>
                                <%-- <div id="dvcbAktiv">--%>
                                <dx:ASPxCheckBox ID="cbAktiv" runat="server" ClientInstanceName="cbAktiv">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                        ValidateOnLeave="false">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxCheckBox>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTelefon" ID="lblTelefon" runat="server" ClientInstanceName="lblTelefon">
                                </dx:ASPxLabel>
                                <dx:ASPxTextBox ID="txtTelefon" runat="server" Width="100%" ClientInstanceName="txtTelefon">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                        ValidationGroup="entries1" ValidateOnLeave="false">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmail" ID="lblEmail" runat="server" ClientInstanceName="lblEmail">
                                </dx:ASPxLabel>
                                <dx:ASPxTextBox ID="txtEmail" runat="server" Width="100%" ClientInstanceName="txtEmail">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                        ValidationGroup="entries1" ValidateOnLeave="false">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorText="Format i gabuar e-mail!" />
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbPerdorues" ID="lblPerdorues" runat="server"
                                    Text="Cel perdorues tek tollonat" ClientInstanceName="lblPerdorues">
                                </dx:ASPxLabel>
                                <%--</div> --%>
                                <%-- <div id="dvcbAktiv">--%>
                                <dx:ASPxCheckBox ID="cbPerdorues" runat="server" ClientInstanceName="cbPerdorues">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                        ValidateOnLeave="false">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxCheckBox>
                                <%--</div> --%>
                                <%--<div id="dvlblNdjekjeGjendje">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbNdjekjeGjendje" ID="lblNdjekjeGjendje"
                                    runat="server" Text="Ndjekje Gjendje:" ClientInstanceName="lblNdjekjeGjendje">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcbNdjekjeGjendje"> --%>
                                <dx:ASPxCheckBox ID="cbNdjekjeGjendje" runat="server" ClientInstanceName="cbNdjekjeGjendje"
                                    Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                        ValidateOnLeave="false">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxCheckBox> 
                                
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbDet1" ID="lblDet1"
                                    runat="server" Text="Kontroll gjendje detajim 1:" ClientInstanceName="lblDet1">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcbNdjekjeGjendje"> --%>
                                <dx:ASPxCheckBox ID="cbDet1" runat="server" ClientInstanceName="cbDet1"
                                    Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                        ValidateOnLeave="false">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxCheckBox>
                                 <dx:ASPxLabel Wrap="False" AssociatedControlID="cbDet2" ID="lblDet2"
                                    runat="server" Text="Kontroll gjendje detajim 2:" ClientInstanceName="lblDet2">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcbNdjekjeGjendje"> --%>
                                <dx:ASPxCheckBox ID="cbDet2" runat="server" ClientInstanceName="cbDet2"
                                    Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                        ValidateOnLeave="false">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxCheckBox>
                                <%--</div> --%>
                                <%--<div id="dvlblAutorizimi"> --%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAutorizimi" ID="lblAutorizimi"
                                    runat="server" Text="Autorizimi:" ClientInstanceName="lblAutorizimi">
                                </dx:ASPxLabel>
                               <div>
                                        <select id="cmbAutorizimi">
                                        </select>
                                        <asp:HiddenField ID="cmbAutorizimiHf" ClientIDMode="Static" runat="server" />
                                               
                                    </div>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtRegjistrimi" ID="lblDtRegjistrimi"
                                    runat="server" Text="Date Regjistrimi:" ClientInstanceName="lblDtRegjistrimi">
                                </dx:ASPxLabel>
                                <%--</div> --%>
                                <%-- <div id="dvdteDtRegjistrimi">--%>
                                <dx:ASPxDateEdit Width="100%" ID="dteDtRegjistrimi" runat="server" ClientInstanceName="dteDtRegjistrimi"
                                    ShowShadow="False">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
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
                                <%--</div> --%>
                                <%-- <div id="dvlblDegeAdministrative">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbDegeAdministrative" ID="lblDegeAdministrative"
                                    runat="server" Text="Inventarizimi:" ClientInstanceName="lblDegeAdministrative">
                                </dx:ASPxLabel>
                                <%--</div> --%>
                                <%--<div id="dvcmbDegeAdministrative"> --%>
                                <dx:ASPxComboBox Width="100%" ID="cmbDegeAdministrative" runat="server" ClientInstanceName="cmbDegeAdministrative"
                                    ShowShadow="False" Height="19px" SettingsLoadingPanel-ImagePosition="Top" >
                                    <LoadingPanelImage>
                                    </LoadingPanelImage>
                                         <ClientSideEvents LostFocus ="function(s,e) {TextChangedDega();}"  TextChanged ="function(s,e) {TextChangedDega();}" />
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
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLloji" ID="lblLloji"
                                    runat="server" Text="Lloji:" ClientInstanceName="lblLloji">
                                </dx:ASPxLabel>
                                <%--</div> --%>
                                <%--<div id="dvcmbDegeAdministrative"> --%>
                                <dx:ASPxComboBox Width="100%" ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji"
                                    ShowShadow="False" Height="19px" SettingsLoadingPanel-ImagePosition="Top">
                                    <ClientSideEvents SelectedIndexChanged="function (s,e){LlojiChanged(s);}" />
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
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtFillimStatusi" ID="lblDtFillimStatusi"
                                    runat="server" Text="Data e fillimit te statusit:" ClientInstanceName="lblDtFillimStatusi">
                                </dx:ASPxLabel>
                                <%--</div> --%>
                                <%-- <div id="dvdteDtRegjistrimi">--%>
                                <dx:ASPxDateEdit Width="100%" ID="dteDtFillimStatusi" runat="server" ClientInstanceName="dteDtFillimStatusi"
                                    ShowShadow="False">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
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
                                <%--</div> --%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKohezgjatja" ID="lblKohezgjatja" runat="server"
                                    ClientInstanceName="lblKohezgjatja" Text="Jetegjatesia ne vite:">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtxtKodi"> --%>
                                <dx:ASPxTextBox ID="txtKohezgjatja" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKohezgjatja">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                        ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbStatusi" ID="lblStatusi"
                                    runat="server" Text="Statusi:" ClientInstanceName="lblStatusi">
                                </dx:ASPxLabel>
                                <%--</div> --%>
                                <%--<div id="dvcmbDegeAdministrative"> --%>
                                <dx:ASPxComboBox Width="100%" ID="cmbStatusi" runat="server" ClientInstanceName="cmbStatusi"
                                    ShowShadow="False" Height="19px" SettingsLoadingPanel-ImagePosition="Top">
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
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="btneCaktoNeHarte" ID="lblCaktoNeHarte"
                                    runat="server" Text="Cakto ne harte:" ClientInstanceName="lblCaktoNeHarte">
                                </dx:ASPxLabel>
                                <dx:ASPxComboBox ID="btneCaktoNeHarte" runat="server" ClientInstanceName="btneCaktoNeHarte"
                                    ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                    <ClientSideEvents ButtonClick="function(s, e){ hapLupeHarte(s, e); }" TextChanged="function(s, e){ vendosGeomNeHfState(s, e); }" />
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                        ValidationGroup="entries" SetFocusOnError="true" ValidateOnLeave="false">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField ErrorText="*" IsRequired="True" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime" ID="lblShenime" runat="server"
                                    Text="Shenime:" ClientInstanceName="lblShenime">
                                </dx:ASPxLabel>

                                <dx:ASPxMemo ID="txtShenime" runat="server" Width="100%" AutoPostBack="false"
                                    ClientInstanceName="txtShenime">
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
                                <%--</div>--%>
                                <%--<div id="dvtxtKodi"> --%>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojLayer" ID="lblLlojLayer"
                                    runat="server" Text="Lloj Layeri:" ClientInstanceName="lblLlojLayer">
                                </dx:ASPxLabel>
                                <%--</div> --%>
                                <%--<div id="dvcmbDegeAdministrative"> --%>
                                <dx:ASPxComboBox Width="100%" ID="cmbLlojLayer" runat="server" ClientInstanceName="cmbLlojLayer"
                                    ShowShadow="False" Height="19px" SettingsLoadingPanel-ImagePosition="Top">
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
                                 <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKodiPerIntegrim" ID="lblKodiPerIntegrim"
                                    runat="server" Text="Kodi per integrim:" ClientInstanceName="lblKodiPerIntegrim">
                                </dx:ASPxLabel>
                              
                                <dx:ASPxComboBox ID="btneKodiPerIntegrim" runat="server" ClientInstanceName="btneKodiPerIntegrim"
                                    Width="100%" OnItemRequestedByValue="btneKodiPerIntegrim_ItemRequestedByValue" SettingsLoadingPanel-ImagePosition="Top"
                                    ShowShadow="False">
                                    <ClientSideEvents ButtonClick="function(s, e) {KodiPerIntegrim_Click(); }" />
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
                                      <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojiQ" ID="lblLlojiQ" runat="server"
                                        Text="Lloj qendre:" ClientInstanceName="lblLlojiQ">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbMonedha">--%>
                                    <dx:ASPxComboBox ID="cmbLlojiQ" runat="server" ClientInstanceName="cmbLlojiQ" ShowShadow="False"
                                        Width="100%" SettingsLoadingPanel-ImagePosition="Top">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {qendraKostos_TextBox.SetText('');}" />
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="qendraKostos_TextBox" ID="lblQenderKosto"
                                        runat="server" Text="Qendra e Kostos:" ClientInstanceName="lblQenderKosto">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvqendraKostos_TextBox">--%>
                                    <dx:ASPxComboBox ID="qendraKostos_TextBox" Enabled="true" runat="server" ClientInstanceName="qendraKostos_TextBox"
                                         EnableCallbackMode="True" OnItemRequestedByValue="qendraKostos_TextBox_ItemRequestedByValue" OnItemsRequestedByFilterCondition="qendraKostos_TextBox_ItemsRequestedByFilterCondition"
                                       SettingsLoadingPanel-ImagePosition="Top"
                                        ShowShadow="False" Width="100%">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ClientSideEvents ButtonClick="function(s, e) {Qendra_Click();}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave='false'>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMagPrind" ID="lblMagPrind"
                                    runat="server" Text="Magazina Vartese" ClientInstanceName="lblMagPrind">
                                </dx:ASPxLabel>
                                <dx:ASPxComboBox ID="cmbMagPrind" runat="server" ClientInstanceName="cmbMagPrind"
                                    Width="100%" SettingsLoadingPanel-ImagePosition="Top"
                                    ShowShadow="False"   >
                                    <ClientSideEvents ButtonClick="function(s, e) { MagPrindClick(s, e); }" />
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                  <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave='false'>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbOwnShop" ID="lblOwnShop"
                                    runat="server" Text="Own Shop" ClientInstanceName="lblOwnShop">
                                </dx:ASPxLabel>                               
                                <dx:ASPxCheckBox ID="cbOwnShop" runat="server" ClientInstanceName="cbOwnShop"
                                    Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                        ValidateOnLeave="false">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxCheckBox>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="btnQyteti" ID="lblQyteti" runat="server"
                                        Text="Qyteti:" ClientInstanceName="lblQyteti" ClientVisible="false">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtQyteti"> --%>
                                    <dx:ASPxComboBox ID="btnQyteti" runat="server" ClientInstanceName="btnQyteti" ShowShadow="False"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%" ClientVisible="false">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                 <dx:ASPxLabel Wrap="False" AssociatedControlID="btnTipiMag" ID="lblTipiMag" runat="server"
                                         ClientInstanceName="lblTipiMag" ClientVisible="false">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbKMSH">--%>
                                    <dx:ASPxComboBox ID="btnTipiMag" runat="server" ClientInstanceName="btnTipiMag" ShowShadow="False"
                                        ReadOnly="false" SettingsLoadingPanel-ImagePosition="Top" ClientVisible="false">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                             <RequiredField IsRequired="true" />
                                        <RegularExpression ErrorText="Plotesoni tipin e magazines"  />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="Ndryshim" Text="Ndryshimi i statusit">
                        <ContentCollection>
                            <dx:ContentControl ID="content" runat="server">
                                <dx:ASPxGridView ID="gvStatus" runat="server" Width="50%"
                                    ClientInstanceName="gvStatus" OnHtmlRowCreated="gvStatus_HtmlRowCreated"
                                    OnCustomCallback="gvStatus_CustomCallback"
                                    OnCustomJSProperties="gvStatus_CustomJSProperties" ClientIDMode="AutoID">
                                    <ClientSideEvents RowDblClick="function(s, e){ }"
                                        EndCallback="function(s, e) { EndCallbackGrida(s, e);}" BeginCallback="function(s, e) { }" />
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
                                </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <%-- FUSHAT SHTESE--%>
                        <dx:TabPage Name="Fushat Shtese" Visible="true" Text="Fushat Shtese">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl7" runat="server">
                                    <uc1:ucFushatShtese runat="server" ID="ucFushatShtese" />
                                    <dx:ASPxLabel ID="lblNrLlog6" ClientInstanceName="lblNrLlog6" runat="server"></dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtNr6" ClientVisible="false" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtNr6"></dx:ASPxTextBox>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="function(s, e) { activeTabsChanged(s, e); }" />
            </dx:ASPxPageControl>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfLupaAutorizimi" runat="server" />
                <asp:HiddenField ID="hfLupaElementePerIntegrim" runat="server" />
                <asp:HiddenField ID="hfLupaMagPrind" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfKontrollet" runat="server" />
                <asp:HiddenField ID="hfStatusi" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                <asp:HiddenField ID="hfStatusMagazine" runat="server" />
                <asp:HiddenField ID="hfData" runat="server" />
                <asp:HiddenField ID="hfFushatShtese" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxHiddenField ID="hfNrAutoKF" runat="server" ClientInstanceName="hfNrAutoKF">
			</dx:ASPxHiddenField>
			<dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
			</dx:ASPxHiddenField>
        <asp:HiddenField ID="hfArkivaDokId" runat="server" />
        <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="update" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                    <ClientSideEvents Closing="function(s, e) {
	popupUniversal.SetContentUrl('');
}" />
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
