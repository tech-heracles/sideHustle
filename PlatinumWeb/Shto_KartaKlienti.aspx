<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_KartaKlienti.aspx.cs" Inherits="PlatinumWeb.Shto_KartaKlienti" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>

<%@ Register Src="~/ucMenuAndMsgFrame.ascx" TagPrefix="ucMenu" TagName="ucMenuAndMsgFrame" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <!-- DevExtreme themes -->
    <link rel="stylesheet" type="text/css" href="Content/dx.common.css" />
    <link rel="stylesheet" type="text/css" href="Content/dx.generic.alphaweb-compact.css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />

    <!-- A DevExtreme library -->
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/Scripts/dx.viz-web.js;~/js/localization/DevExtreme.Perkthime.js;~/js/myDxDataGrid.js;~/js/aspx.js/Shto_KartaKlienti.aspx-IMB.5.4.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
            </Services>
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" runat="server" OnMenuClick="Menu_ItemClick" OnMenuTemplate="PercaktoTemplateMenu" OnFilterSave="Ruaj_ASPxButton_Click" OnFilterDelete="FshiFilter_ASPxButton_Click" />
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
                            </dx:ASPxPanel>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvKarta" style="display: none">
            <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" TabSpacing="3px"
                ClientInstanceName="PageControl" Width="100%" ActiveTabIndex="0">
                <ContentStyle>
                    <border bordercolor="#AECAF0" borderstyle="Solid" borderwidth="1px" />
                </ContentStyle>
                <TabPages>
                    <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                        <ContentCollection>
                            <dxw:ContentControl>
                                <table class="renditKontrolle">
                                    <tbody>
                                        <tr>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label" runat="server" Style="font-size: large" Text="Modeli:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33">
                                                <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" Width="100%"
                                                    Height="23px" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Style="font-size: medium" AnimationType="None">
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
                                                <dx:ASPxLabel ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33"></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <dx:ASPxGridView ID="ASPxGridView_Kartat" ClientInstanceName="ASPxGridView_Kartat" runat="server"
                                    Width="100%" OnDataBound="ASPxGridView_Kartat_DataBound" OnAfterPerformCallback="ASPxGridView_Kartat_AfterPerformCallback"
                                    OnHeaderFilterFillItems="ASPxGridView_Kartat_HeaderFilterFillItems"
                                    OnCustomJSProperties="ASPxGridView_Kartat_CustomJSProperties" OnCustomCallback="ASPxGridView_Kartat_CustomCallback"
                                    OnAutoFilterCellEditorInitialize="ASPxGridView_Kartat_AutoFilterCellEditorInitialize" ToolTip="Kartat">
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
                                    <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                        SelectionChanged="function(s, e){OnGridSelectionChanged(e);}" FocusedRowChanged="function(s, e) {   mbush=true;	}"
                                        BeginCallback="function(s, e) {	BeginCallback(s,e);}" />
                                    <StylesEditors>
                                        <CalendarHeader Spacing="1px">
                                        </CalendarHeader>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>

                                <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView_Kartat" ExportedRowType="Selected" />

                                <br />
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Name="Karta" Text="Karta Klienti">

                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl3" runat="server">
                                <table id="tblKarta" class="renditKontrolle">
                                    <tbody>
                                    </tbody>
                                </table>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server" Text="Kodi:" ClientInstanceName="lblKodi">
                                </dx:ASPxLabel>

                                <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi">
                                    <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true"
                                        Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere" ValidationExpression="^[\s\S]{0,20}$"></RegularExpression>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmri" ID="lblEmri" runat="server" Text="Emri:" ClientInstanceName="lblEmri">
                                </dx:ASPxLabel>

                                <dx:ASPxTextBox ID="txtEmri" runat="server" Width="100%" ClientInstanceName="txtEmri">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmail" ID="lblEmail" runat="server" Text="Email:" ClientInstanceName="lblEmail">
                                </dx:ASPxLabel>

                                <dx:ASPxTextBox ID="txtEmail" ClientInstanceName="txtEmail" runat="server" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKontakt" ID="lblKontakt" runat="server" Text="Kontakti:" ClientInstanceName="lblKontakt">
                                </dx:ASPxLabel>

                                <dx:ASPxTextBox ID="txtKontakt" ClientInstanceName="txtKontakt" runat="server" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbAktiv" ID="lblAktiv" runat="server" Text="Aktiv" ClientInstanceName="lblAktiv">
                                </dx:ASPxLabel>

                                <dx:ASPxCheckBox ID="cbAktiv" runat="server" ClientInstanceName="cbAktiv" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxCheckBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTarga" ID="lblTarga" runat="server" Text="Targa:" ClientInstanceName="lblTarga">
                                </dx:ASPxLabel>

                                <dx:ASPxTextBox ID="txtTarga" ClientInstanceName="txtTarga" runat="server" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShoferi" ID="lblShoferi" runat="server" Text="Shoferi:" ClientInstanceName="lblShoferi">
                                </dx:ASPxLabel>

                                <dx:ASPxTextBox ID="txtShoferi" ClientInstanceName="txtShoferi" runat="server" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>


                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbPolitike" ID="lblPolitike" runat="server" Text="Politike karte" ClientInstanceName="lblPolitike">
                                </dx:ASPxLabel>

                                <dx:ASPxComboBox ID="cmbPolitike" ClientInstanceName="cmbPolitike" Width="100%" runat="server"
                                    OnItemRequestedByValue="cmbPolitike_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbPolitike_ItemsRequestedByFilterCondition" EnableCallbackMode="True"
                                    IncrementalFilteringDelay="7" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" DropDownStyle="DropDown">
                                    <ClientSideEvents TextChanged="function(s, e) {textChangedPolitika(s,e);}" />
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                        ValidationGroup="entries" ValidateOnLeave="false">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                    <ClientSideEvents ButtonClick="function(s, e) {Llog_Click();}" />
                                </dx:ASPxComboBox>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKategori" ID="lblKategori" runat="server" Text="Kategori Zbritje:" ClientInstanceName="lblKategori">
                                </dx:ASPxLabel>

                                <dx:ASPxComboBox ID="cmbKategori" ClientInstanceName="cmbKategori" Width="100%" runat="server"
                                    OnItemRequestedByValue="cmbKategori_ItemRequestedByValue" EnableCallbackMode="True"
                                    IncrementalFilteringDelay="7" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" DropDownStyle="DropDown">
                                    <ClientSideEvents ButtonClick="function(s, e) { Kategori_Click(); }" TextChanged="function(s, e) {textChangedKategoria(s,e);}" />
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbQyteti" ID="lblQyteti" runat="server"
                                    Text="Qyteti:" ClientInstanceName="lblQyteti">
                                </dx:ASPxLabel>
                                <dx:ASPxComboBox ID="cmbQyteti" runat="server" ClientInstanceName="cmbQyteti" ShowShadow="False"
                                    SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
}" />
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

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtAdresa" ID="lblAdresa" runat="server"
                                    Text="Adresa:" ClientInstanceName="lblAdresa">
                                </dx:ASPxLabel>
                                <dx:ASPxMemo ID="txtAdresa" runat="server" ClientInstanceName="txtAdresa" Width="100%" Rows="3">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxMemo>

                                <%--<div id="dteDitelindja">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDitelindja" ID="lblDitelindja" runat="server"
                                    Text="Date Aktivizimi:" ClientInstanceName="lblDitelindja">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%-- <div lblDateAkt>--%>
                                <dx:ASPxDateEdit ID="dteDitelindja" runat="server" ClientInstanceName="dteDitelindja"
                                    ShowShadow="False" Width="100%">
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <CalendarProperties>
                                        <HeaderStyle Spacing="1px" />
                                        <FooterStyle Spacing="17px" />
                                    </CalendarProperties>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries2">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxDateEdit>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtGjendjePike" ID="lblGjendjePike" runat="server" Text="Gjendje Pikesh:" ClientInstanceName="lblGjendjePike">
                                </dx:ASPxLabel>
                                <dx:ASPxTextBox ID="txtGjendjePike" ClientInstanceName="txtGjendjePike" runat="server" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtGjendjepikesh" ID="lblGjendjepikesh" runat="server" Text="Gjendje Pikesh:" ClientInstanceName="lblGjendjepikesh">
                                </dx:ASPxLabel>
                                <dx:ASPxTextBox ID="txtGjendjepikesh" ClientInstanceName="txtGjendjepikesh" runat="server" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtStatusi" ID="lblStatusi" runat="server" Text="Statusi:" ClientInstanceName="lblStatusi">
                                </dx:ASPxLabel>
                                <dx:ASPxTextBox ID="txtStatusi" ClientInstanceName="txtStatusi" runat="server" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKlienti" ID="lblKlienti" runat="server" ClientInstanceName="lblKlienti">
                                </dx:ASPxLabel>
                                <dx:ASPxComboBox ID="cmbKlienti" runat="server" ClientInstanceName="cmbKlienti"
                                    Width="100%" ShowShadow="False" ValueType="System.Int32" EnableClientSideAPI="True"
                                    IncrementalFilteringMode="Contains" EnableSynchronization="True" EnableCallbackMode="True"
                                    DropDownRows="3" CallbackPageSize="3"
                                    OnItemRequestedByValue="cmbKlienti_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbKlienti_ItemsRequestedByFilterCondition"
                                    SettingsLoadingPanel-ImagePosition="Top">
                                    <ClientSideEvents ButtonClick="function(s, e) { Klienti_Click(); }" TextChanged="function(s, e) {textChangedKlienti(s,e);}" />
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                        ValidationGroup="entries" ValidateOnLeave="false">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtDepartamenti" ID="lblDepartamenti" runat="server" ClientInstanceName="lblDepartamenti">
                                </dx:ASPxLabel>
                                <dx:ASPxTextBox ID="txtDepartamenti" runat="server" Width="100%" ClientInstanceName="txtDepartamenti">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtModeli" ID="lblModeli" runat="server" ClientInstanceName="lblModeli">
                                </dx:ASPxLabel>
                                <dx:ASPxTextBox ID="txtModeli" ClientInstanceName="txtModeli" runat="server" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtId" ID="lblId" runat="server" ClientInstanceName="lblId">
                                </dx:ASPxLabel>
                                <dx:ASPxTextBox ID="txtId" ClientInstanceName="txtId" runat="server" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>
                                <dx:ASPxLabel Wrap="False" ID="lblMenyrePagese" AssociatedControlID="txtMenyrePagese"
                                    runat="server" ClientVisible="false" Text="Menyre pagese" ClientInstanceName="lblMenyrePagese">
                                </dx:ASPxLabel>
                                <dx:ASPxComboBox ID="txtMenyrePagese" Width="100%" runat="server" ClientVisible="false" ClientInstanceName="txtMenyrePagese"
                                    ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">                                    
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ValidateOnLeave="False"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>

                    <dxtc:TabPage Name="Limiti i Kartes" Text="Limiti i Kartes">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                <table class="renditKontrolle">
                                    <tbody>
                                    </tbody>
                                </table>
                                <%--<dx:ASPxGridView ID="gvLimiti" ClientInstanceName="gvLimiti" Width="100%" ToolTip="Ambjenti i Limitit te Kartes" runat="server" OnCustomCallback="gvLimiti_CustomCallback"
                                    OnBatchUpdate="gvLimiti_BatchUpdate" OnDataBound="gvLimiti_DataBound">
                                    <ClientSideEvents BeginCallback="gvBeginCallBack" 
                                        EndCallback="gvEndCallback" CustomButtonClick="CustomButtonsClick" BatchEditRowValidating="rowValidation" BatchEditStartEditing="StartEditing" BatchEditEndEditing="EndEditing" />

                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
                                    <ClientSideEvents />
                                    <StylesEditors>
                                        <CalendarHeader Spacing="1px">
                                        </CalendarHeader>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>

                                    <Templates>
                                        <StatusBar>
                                        </StatusBar>
                                    </Templates>
                                </dx:ASPxGridView>--%>

                                <div id="divgride1" style="min-height: 200px;">
                                    <div class="dx-viewport demo-container">
                                        <div id="data-grid-buxheti">
                                            <div id="dxDataGrid_Limiti" class="noUndoGrida"></div>
                                        </div>
                                    </div>
                                </div>


                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="function(s,e){activeTabsChanged(s, e);}" />
            </dxtc:ASPxPageControl>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfKontrollet" runat="server" />
                <asp:HiddenField ID="hfStatusi" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                <asp:HiddenField ID="hfLupaKlienti" runat="server" />
                <asp:HiddenField ID="hfKushti" runat="server" />
                <asp:HiddenField ID="hfMerrLimiteNgaSesioni" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>

        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                    EnableAnimation="False" PopupVerticalAlign="WindowCenter" AllowResize="True"
                    AppearAfter="10" ClientIDMode="AutoID" Height="400px">
                    <ClientSideEvents Closing="function(s, e) {	popupUniversal.SetContentUrl(''); }" />
                    <ContentStyle>
                        <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                            PaddingTop="1px" />
                    </ContentStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>



