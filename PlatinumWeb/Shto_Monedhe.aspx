<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Shto_Monedhe.aspx.cs"
    Inherits="PlatinumWeb.Shto_Monedhe" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Shto_Monedhe.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" AutoPostBack="true" ClientInstanceName="ASPxMenu1"
                                ItemImagePosition="Top" OnDataBound="ASPxMenu1_DataBound" OnItemClick="ASPxMenu1_ItemClick"
                                SeparatorWidth="1px" ShowPopOutImages="True" Width="100%">
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
                                    <dx:ASPxMenu ID="MenuInfo" runat="server" BorderBetweenItemAndSubMenu="HideRootOnly"
                                        ClientIDMode="AutoID" ClientInstanceName="MenuInfo" ShowPopOutImages="True" Width="100%">
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
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                    ClientInstanceName="popFshi" CloseAction="CloseButton" CssPostfix="Glass" EnableAnimation="False"
                    EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" Width="300px">
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
        <div id="dvMonedha" style="display: none">
            <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" Width="100%" ClientInstanceName="monedhat_PageControl"
                ActiveTabIndex="3" Height="600px"  TabSpacing="3px">
                <ContentStyle>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                </ContentStyle>
                <TabPages>
                    <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                        <ContentCollection>
                            <dxw:ContentControl>
                                <table class="renditKontrolle">
                                    <tr>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                runat="server" ClientIDMode="AutoID" Style="font-size: large" Text="Modeli:">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth33">
                                            <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                Height="24px" ShowShadow="False" Style="font-size: medium" ValueType="System.String"
                                                SettingsLoadingPanel-ImagePosition="Top" Width="100%" AnimationType="None">
                                                <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                                                <DropDownButton>
                                                    <Image>
                                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    </Image>
                                                </DropDownButton>
                                                <ValidationSettings>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td class="renditKontrolleLabelMeWidth33">
                                            <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi"
                                                ClientIDMode="AutoID" ClientInstanceName="lblKonfigurimi">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth33"></td>
                                    </tr>
                                </table>
                                <dx:ASPxGridView ID="ASPxGridView_Monedhat" runat="server" Width="100%" OnAfterPerformCallback="ASPxGridView_Monedhat_AfterPerformCallback"
                                    ClientInstanceName="ASPxGridView_Monedhat" OnCustomCallback="ASPxGridView_Monedhat_CustomCallback"
                                    OnCustomJSProperties="ASPxGridView_Monedhat_CustomJSProperties" OnDataBound="ASPxGridView_Monedhat_DataBound"
                                    OnHeaderFilterFillItems="ASPxGridView_Monedhat_HeaderFilterFillItems" OnProcessColumnAutoFilter="ASPxGridView_Monedhat_ProcessColumnAutoFilter">
                                    <Templates>
                                        <TitlePanel>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Zgjidh kolonat" AutoPostBack="false"
                                                            ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                            <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s,e,ASPxGridView_Monedhat)}"
                                                                Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="pnlruaj" runat="server">
                                                            <ContentTemplate>
                                                                <dx:ASPxButton ID="ASPxButton3" runat="server" Text="Ruaj kolonat" AutoPostBack="true"
                                                                    ClientVisible="false" Image-Url="images/new/disk_blue (3).png" Font-Size="8"
                                                                    OnClick="RuajKolona_Click">
                                                                    <ClientSideEvents Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                                </dx:ASPxButton>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                    </Templates>
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
                                    <ClientSideEvents FocusedRowChanged="function(s, e) { mbush=true; }"
                                        RowDblClick="function(s, e) { OnGridDoubleClick(e.visibleIndex); kaloTab=true; }"
                                        SelectionChanged="function(s, e){ OnGridSelectionChanged(e); }"
                                        BeginCallback="function(s, e) { BeginCallback(s,e); }" />
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                                <br />
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage  Text="Monedha" Name="Tab0">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                <table id="tblMonedha" class="renditKontrolle">
                                    <tbody>
                                    </tbody>
                                </table>
                                <%--<div id="dvlblKodi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="kodi_TextBox" ID="lblKodi" runat="server"
                                    Text="Kodi: " ClientInstanceName="lblKodi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvkodi_TextBox">--%>
                                <dx:ASPxTextBox ID="kodi_TextBox" runat="server" AutoPostBack="false" Width="100%"
                                    ClientInstanceName="kodi_TextBox">
                                    <ValidationSettings CausesValidation="true" ValidationGroup="entries" Display="Dynamic"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere" ValidationExpression="^[\s\S]{0,20}$"></RegularExpression>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>
                                <%--</div>--%>
                                <%--<div id="dvlblPershkrimi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="pershkrimiTextBox" ID="lblPershkrimi"
                                    runat="server" Text="Pershkrimi: " ClientInstanceName="lblPershkrimi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvpershkrimiTextBox">--%>
                                <dx:ASPxMemo ID="pershkrimiTextBox" runat="server" Width="100%" ClientInstanceName="pershkrimiTextBox"
                                    Rows="3">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                        ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxMemo>
                                <%--</div>--%>
                                <%--<div id="dvlblAktive">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="active_CheckBox" ID="lblAktive"
                                    runat="server" Text="Aktive: " ClientInstanceName="lblAktive">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvactive_CheckBox">--%>
                                <dx:ASPxCheckBox ID="active_CheckBox" runat="server" ClientInstanceName="active_CheckBox"
                                    TextSpacing="2px" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxCheckBox>
                                <%--</div>--%>
                                <%--<div id="dvlblllogFitimi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogFitimi" ID="lblllogFitimi"
                                    runat="server" Text="Llogari Fitim nga Kursi:" ClientInstanceName="lblllogFitimi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvbtneLlogFitimi">--%>
                                <dx:ASPxComboBox ID="btneLlogFitimi" 
                                    runat="server" 
                                    ClientInstanceName="btneLlogFitimi"
                                    ShowShadow="False" 
                                    ValueType="System.String" 
                                    SettingsLoadingPanel-ImagePosition="Top"
                                    Width="100%"
                                    OnItemRequestedByValue="btneLlogFitimi_ItemRequestedByValue"
                                    OnItemsRequestedByFilterCondition="btneLlogFitimi_ItemsRequestedByFilterCondition">
                                    <ClientSideEvents ButtonClick="function(s,e){ fitim='fitim';
                                                    Llogari_Click();}"
                                        SelectedIndexChanged="function(s,e){ var s = btneLlogFitimi.GetText().split(';');
            btneLlogFitimi.SetText(s[0]);}" />
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                        ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblLlogHumbje">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogHumbje" ID="lblLlogHumbje"
                                    runat="server" Text="Llogari Humbje nga Kursi:" ClientInstanceName="lblLlogHumbje">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvbtneLlogHumbje">--%>
                                <dx:ASPxComboBox ID="btneLlogHumbje" 
                                    runat="server" 
                                    ClientInstanceName="btneLlogHumbje"
                                    ShowShadow="False" 
                                    ValueType="System.String" 
                                    SettingsLoadingPanel-ImagePosition="Top"
                                    Width="100%"
                                    OnItemRequestedByValue="btneLlogHumbje_ItemRequestedByValue"
                                    OnItemsRequestedByFilterCondition="btneLlogHumbje_ItemsRequestedByFilterCondition">
                                    <ClientSideEvents ButtonClick="function(s,e){ fitim='humbje';
                                                    Llogari_Click();}"
                                        SelectedIndexChanged="function(s,e){ var s = btneLlogHumbje.GetText().split(';');
            btneLlogHumbje.SetText(s[0]);}" />
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                        ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblAutorizimi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAutorizimi" ID="lblAutorizimi"
                                    runat="server" Text="Autorizimi:" ClientInstanceName="lblAutorizimi">
                                </dx:ASPxLabel>
                               <div>
                                        <select id="cmbAutorizimi">
                                        </select>
                                        <asp:HiddenField ID="cmbAutorizimiHf" ClientIDMode="Static" runat="server" />
                                               
                                    </div>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="btneFormatNumri" ID="lblFormatNumri"
                                    runat="server" Text="Formati i numrave te kursit:" ClientInstanceName="lblFormatNumri">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvbtneLlogHumbje">--%>
                                <dx:ASPxComboBox ID="btneFormatNumri" runat="server" ClientInstanceName="btneFormatNumri"
                                    ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                                    Width="100%">
                                    <ClientSideEvents ButtonClick="function(s,e){ formatNrButtonClick();}" TextChanged="function(s,e){ textChangedFormatNr();}" />
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                        ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Name="Kurset" Text="Kurset">
                        <ContentCollection>
                            <dxw:ContentControl runat="server" ID="ContentControl42">
                                <table id="tblKursi" class="renditKontrolle">
                                    <tbody>
                                    </tbody>
                                </table>
                                <%--<div id="dvlblMonedha">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="monedha_label" ID="lblMonedha" runat="server"
                                    Text="Monedha:" ClientInstanceName="lblMonedha">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvmonedha_label">--%>
                                <dx:ASPxLabel ID="monedha_label" runat="server" Text="" ClientInstanceName="monedha_label">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvlblNjesia">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="njesia_TextBox" ID="lblNjesia" runat="server"
                                    Text="Njesia:" ClientInstanceName="lblNjesia">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvnjesia_TextBox">--%>
                                <dx:ASPxTextBox ID="njesia_TextBox" runat="server" Width="100%" ClientInstanceName="njesia_TextBox"
                                    Text="1">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                                <%--</div>--%>
                                <br />
                                <dx:ASPxGridView ID="gvKurset" runat="server" ClientInstanceName="gvKurset" Width="80%"
                                    OnHtmlRowCreated="gvKurset_HtmlRowCreated" OnAfterPerformCallback="gvKurset_AfterPerformCallback" OnCustomJSProperties="gvKurset_CustomJSProperties"
                                    OnCustomCallback="gvKurset_CustomCallback" OnAutoFilterCellEditorInitialize="ASPxGridView_Monedhat_AutoFilterCellEditorInitialize">
                                    <ClientSideEvents EndCallback="function(s, e) {formatoFushaDevi(); ShfaqTeDhenat(); }" />
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Name="Historiku" Text="Historiku">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl2" runat="server">
                                <dx:ASPxGridView ID="gvHistoriku" runat="server" ClientInstanceName="gvHistoriku"
                                    OnAfterPerformCallback="gvHistoriku_AfterPerformCallback" Width="94%" OnCustomCallback="gvHistoriku_CustomCallback">
                                    <Templates>
                                        <TitlePanel>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="btnXlsxExport" runat="server" ToolTip="Export to Xlsx" Image-Height="16px"
                                                            Image-Url="images/xlsx24.png" Font-Size="8" UseSubmitBehavior="false" OnClick="btnXlsxExport_Click">
                                                         
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnPdfExport" runat="server" ToolTip="Export to Pdf" Image-Height="16px"
                                                            Image-Url="images/pdf_icon.png" Font-Size="8" UseSubmitBehavior="false" OnClick="btnPdfExport_Click">
                                                          
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
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gvHistoriku"
                                        ExportSelectedRowsOnly ="false" />
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="function(s, e) { ActiveTabChanged(s, e); }" />
                <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
            </dxtc:ASPxPageControl >
        </div>
        <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
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
                <asp:HiddenField ID="hfTrupiFillimit" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfKonffillestar" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hfMonedha" runat="server" />
                <asp:HiddenField ID="hfKurset" runat="server" />
                <asp:HiddenField ID="hfAutorizime" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hfShtimModifikim" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hfId" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hfKontrollet" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hfStatusi" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
