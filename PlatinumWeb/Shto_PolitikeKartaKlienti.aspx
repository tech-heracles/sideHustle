<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_PolitikeKartaKlienti.aspx.cs" Inherits="PlatinumWeb.Shto_PolitikeKartaKlienti" %>

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
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>

<%@ Register Src="~/ucMenuAndMsgFrame.ascx" TagPrefix="ucMenu" TagName="ucMenuAndMsgFrame" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Shto_PolitikeKartaKlienti.aspx-IMB.5.4.js&v76"
        type="text/javascript"></script>
    <style type="text/css">
        .auto-style3 {
            width: 146px;
        }

        .auto-style4 {
            width: 324px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" runat="server" OnMenuClick="Menu_ItemClick" OnMenuTemplate="PercaktoTemplateMenu" OnFilterSave="Ruaj_ASPxButton_Click" OnFilterDelete="FshiFilter_ASPxButton_Click" />
                <div id="loadingGifDiv" class="bootstrap-iso" style="display: none">
                    <img id="loadingIMBLogo" src="images/GIFWEB.svg" alt="Loading logo" class="img-responsive center-block">
                </div>
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
        <div id="dvPolitika">
            <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" TabSpacing="3px"
                ClientInstanceName="PageControl" Width="100%" ActiveTabIndex="1">
                <ContentStyle>
                    <border bordercolor="#AECAF0" borderstyle="Solid" borderwidth="1px" />
                </ContentStyle>
                <TabPages>
                    <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                        <ContentCollection>
                            <dxw:ContentControl>
                                <div id="konfigurimi" class="atributeDiveFshehur">
                                    <table class="renditKontrolle">
                                        <tbody>
                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label" runat="server" Style="font-size: large" Text="Modeli:">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth33">
                                                    <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" Width="100%">
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
                                </div>
                                <dx:ASPxGridView ID="ASPxGridView_Politikat" ClientInstanceName="ASPxGridView_Politikat" runat="server"
                                    Width="100%" OnDataBound="ASPxGridView_Politikat_DataBound" OnAfterPerformCallback="ASPxGridView_Politikat_AfterPerformCallback"
                                    OnHeaderFilterFillItems="ASPxGridView_Politikat_HeaderFilterFillItems"
                                    OnCustomJSProperties="ASPxGridView_Politikat_CustomJSProperties" OnCustomCallback="ASPxGridView_Politikat_CustomCallback">
                                    <Templates>
                                        <TitlePanel>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ASPxButton2" runat="server" ToolTip="Zgjidh kolonat" AutoPostBack="false"
                                                            ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                            <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s,e,ASPxGridView_Kartat)}"
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
                                                            <ClientSideEvents Click="function(s, e) { ASPxGridView_Politikat.SelectAllRowsOnPage(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe"
                                                            AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) { ASPxGridView_Politikat.SelectRows(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="gridaUnSelectTeGjitha" runat="server" ToolTip="Fshi Zgjedhjen"
                                                            AutoPostBack="false" Image-Url="images/uncheck2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) { ASPxGridView_Politikat.UnselectRows(); }" />
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
                                    <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                        SelectionChanged="function(s, e){OnGridSelectionChanged(e);}" FocusedRowChanged="function(s, e) {
            mbush=true;	
}"
                                        BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                                    <StylesEditors>
                                        <CalendarHeader Spacing="1px">
                                        </CalendarHeader>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Name="Politike" Text="Politike Karta Klienti">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl3" runat="server">
                                <table id="tblPolitika" class="renditKontrolle" style="width: 40%; min-width: 620px;">
                                    <tbody>
                                        <tr>
                                            <td class="auto-style3">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server" Text="Kodi:" ClientInstanceName="lblKodi">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="auto-style4">
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
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td class="auto-style4">
                                                <asp:UpdatePanel ID="pnlGrida" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
                                                    <ContentTemplate>
                                                        <dx:ASPxGridView ID="ASPxGridView_KategoriPike" runat="server" Width="100%"
                                                            OnRowUpdating="ASPxGridView_KategoriPike_RowUpdating" ClientInstanceName="ASPxGridView_KategoriPike"
                                                            OnRowInserting="ASPxGridView_KategoriPike_RowInserting" OnRowValidating="ASPxGridView_KategoriPike_RowValidating"
                                                            OnRowDeleting="ASPxGridView_KategoriPike_RowDeleting" OnCellEditorInitialize="ASPxGridView_KategoriPike_CellEditorInitialize"
                                                            OnCustomCallback="ASPxGridView_KategoriPike_CustomCallback"
                                                            OnCustomJSProperties="ASPxGridView_KategoriPike_CustomJSProperties">

                                                            <ClientSideEvents RowDblClick="function(s, e){callWebservice(); indexModifiko=e.visibleIndex;      }"
                                                                EndCallback="function(s, e) { EndCallbackGrida(s, e);}" BeginCallback="function(s, e) {BeginCallback(s,e);}" />
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
                                                        <dx:ASPxButton ID="cmdShto" AutoPostBack="false" runat="server" Text="Shto" ClientInstanceName="cmdShto">
                                                            <ClientSideEvents Click="OnShtoClick" />
                                                        </dx:ASPxButton>
                                                        <dx:ASPxButton ID="cmdEdit" AutoPostBack="false" runat="server" Text="Modifiko" ClientInstanceName="cmdEdit">
                                                            <ClientSideEvents Click="OnEditClick" />
                                                        </dx:ASPxButton>
                                                        <dx:ASPxButton ID="cmdFshi" AutoPostBack="false" runat="server" Text="Fshi" ClientInstanceName="cmdFshi">
                                                            <ClientSideEvents Click="OnDeleteClick" />

                                                        </dx:ASPxButton>

                                                    </ContentTemplate>

                                                </asp:UpdatePanel>

                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3">
                                                <dx:ASPxLabel Wrap="False" ID="lblLloji" runat="server" Text="Lloji politike:" ClientInstanceName="lblLloji">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="auto-style4">
                                                <dx:ASPxCheckBox ID="chkZbritje" EnableClientSideAPI="True" ClientInstanceName="rdPerqindje" Width="100%" runat="server" EnableCallbackMode="True" Text="me zbritje" GroupName="lloji" Style="margin-right: 0px" Checked="True">
                                                    <DisabledStyle Font-Bold="False">
                                                    </DisabledStyle>
                                                </dx:ASPxCheckBox>
                                            </td>
                                            <td>
                                                <dx:ASPxCheckBox ID="chkPike" runat="server" EnableClientSideAPI="True" ClientInstanceName="rdPike" EnableCallbackMode="True" Text="me pike" Width="100%" GroupName="lloji" Style="margin-top: 0px">
                                                    <DisabledStyle Font-Bold="False">
                                                    </DisabledStyle>
                                                    <ClientSideEvents CheckedChanged="chkbPikeChanged" />
                                                </dx:ASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVleraPike" ID="lblVleraPike" runat="server" Text="Apliko 1 pike per cdo   " ClientInstanceName="lblVleraPike">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="auto-style4">
                                                <dx:ASPxTextBox ID="txtVleraPike" ClientInstanceName="txtVleraPike" runat="server" Width="100%" Style="margin-left: 0px">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries" SetFocusOnError="True">
                                                        <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RequiredField IsRequired="true" />
                                                    </ValidationSettings>
                                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVleraPike" ID="lblVleraPike2" runat="server" Text="  LEK te vleres se fatures se shitjes." ClientInstanceName="lblVleraPike2">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="Active_TabChanged" />
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
                <asp:HiddenField ID="hfRuaj" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
    </form>
</body>
</html>
