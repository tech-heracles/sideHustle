<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_Banka.aspx.cs" Inherits="PlatinumWeb.Shto_Banka" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/aspx.js/Shto_Banka.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
            </dx:ASPxGlobalEvents>
            <dx:ASPxHiddenField ID="hfState" runat="server">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True" OnItemClick="ASPxMenu1_ItemClick">
                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                    <ClientSideEvents ItemClick="function(s, e) { menu_click(s,e); }"
                                        Init="function(s) { s.SetClientVisible(true); }" />
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
                                            <SubMenuStyle GutterWidth="1" />
                                        </dx:ASPxMenu>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                     
                    <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel"
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
                                                                <ClientSideEvents Click="function(s, e) { popFshi.Hide(); Utils.shfaqLoadingGif(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                                <ClientSideEvents Click="function(s, e) { popFshi.Hide(); }" />
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
            <div id="dvArkaBanka" style="display: none">
                <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" ClientInstanceName="PageControl"
                    TabSpacing="3px" Width="100%" ActiveTabIndex="3" Height="520px"
                    ClientIDMode="AutoID">
                    <ContentStyle>
                        <border bordercolor="#AECAF0" borderstyle="Solid" borderwidth="1px" />
                    </ContentStyle>
                    <TabPages>
                        <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme" NewLine="True">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl1" runat="server">
                                    <table class="renditKontrolle">
                                        <tr>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                    runat="server" Text="Modeli:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33">
                                                <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                    ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top" AnimationType="None">
                                                    <ClientSideEvents SelectedIndexChanged="function(s,e){ ndryshoKonfigurimin(); }" />
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
                                                <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" Text="" class="klasePerLblKonfigurimi"
                                                    ClientInstanceName="lblKonfigurimi">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33"></td>
                                        </tr>
                                    </table>
                                    <dx:ASPxGridView ID="ASPxGridView_Bankat" ClientInstanceName="ASPxGridView_Bankat"
                                        runat="server" Width="100%" OnDataBound="ASPxGridView_Bankat_DataBound" OnAfterPerformCallback="ASPxGridView_Bankat_AfterPerformCallback"
                                        OnHeaderFilterFillItems="ASPxGridView_Bankat_HeaderFilterFillItems" OnCustomCallback="ASPxGridView_Bankat_CustomCallback"
                                        OnCustomJSProperties="ASPxGridView_Bankat_CustomJSProperties" OnProcessColumnAutoFilter="ASPxGridView_Bankat_ProcessColumnAutoFilter"
                                        Style="margin-bottom: 33px">
                                        <Templates>
                                            <TitlePanel>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ASPxButton2" runat="server" ToolTip="Zgjidh kolonat" AutoPostBack="false"
                                                                ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                                <ClientSideEvents Click="function (s,e){ myFaqeCelje.buttonKonfiguroClick(s, e, ASPxGridView_Bankat); }"
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
                                                                <ClientSideEvents Click="function(s, e) { ASPxGridView_Bankat.SelectAllRowsOnPage(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe"
                                                                AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="function(s, e) { ASPxGridView_Bankat.SelectRows(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="gridaUnSelectTeGjitha" runat="server" ToolTip="Fshi Zgjedhjen"
                                                                AutoPostBack="false" Image-Url="images/uncheck2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="function(s, e) { ASPxGridView_Bankat.UnselectRows(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnXlsxExport" runat="server" ToolTip="Export to Xlsx" OnClick="btnXlsxExport_Click" Image-Height="16px"
                                                                Image-Url="images/xlsx24.png" Font-Size="8">
                                                                <ClientSideEvents Click="function(s, e) { clickExport(e); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnPdfExport" runat="server" OnClick="btnPdfExport_Click" ToolTip="Export to Pdf" Image-Height="16px"
                                                                Image-Url="images/pdf_icon.png" Font-Size="8">
                                                                <ClientSideEvents Click="function(s, e) { clickExport(e); }" />
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
                                        <ClientSideEvents 
                                            FocusedRowChanged="function(s, e) { onNdryshimFokusi(); }"
                                            RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex); kaloTab=true; }" 
                                            SelectionChanged="function(s, e){OnGridSelectionChanged(e);}"
                                            BeginCallback="function(s, e) {	BeginCallback(s,e); }" 
                                            EndCallback="function(s, e) { EndCallbackGrida(s,e); }"/>
                                        <SettingsPager PageSize="15">
                                        </SettingsPager>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                        <SettingsBehavior AllowFocusedRow="False" />
                                        <SettingsBehavior AllowSelectByRowClick="false" />
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView_Bankat" ExportedRowType="Selected" />
                                    <br />
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Banka" Text="Banka">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl3" runat="server">
                                    <table id="tblBanka" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvkodi_Label">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="kodi_Label" runat="server"
                                        Text="Kodi:" ClientInstanceName="kodi_Label">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtKodi">--%>
                                    <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi">
                                        <ClientSideEvents TextChanged="TextChanged_txtKodi" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvemri_Label">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerBanke" ID="emri_Label" runat="server"
                                        Text="Emri:" ClientInstanceName="emri_Label">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtEmerBanke">--%>
                                    <dx:ASPxMemo ID="txtEmerBanke" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtEmerBanke" Rows="3">
                                        <ClientSideEvents TextChanged="TextChanged_txtEmerBanke" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                    <%--<div id="dvtipi_Label">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbTipi" ID="tipi_Label" runat="server"
                                        ClientInstanceName="tipi_Label" Text="Tipi:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbTipi">--%>
                                    <dx:ASPxComboBox ID="cmbTipi" runat="server" ClientInstanceName="cmbTipi" ClientVisible="false"
                                        TextField="1" Width="100%" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
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
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblLloji">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLloji" ID="lblLloji" runat="server"
                                        Text="Lloji:" ClientInstanceName="lblLloji">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbLloji">--%>
                                    <dx:ASPxComboBox ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji" Enabled="true"
                                        Width="100%" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents TextChanged="function(s, e) { ndryshoArkaBanka(cmbLloji.GetText()); }" />
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
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvgrupi_Label">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtGrupi" ID="grupi_Label" runat="server"
                                        Text="Grupi:" ClientInstanceName="grupi_Label">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtGrupi">--%>
                                    <dx:ASPxComboBox ID="txtGrupi" runat="server" Width="100%" ClientInstanceName="txtGrupi"
                                        EnableCallbackMode="True"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
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
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                        <ClientSideEvents ButtonClick="function(s, e) {Grupi_Click();}" />
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvnrLlogBankare_Label">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrLlogariBankare" ID="nrLlogBankare_Label"
                                        runat="server" ClientInstanceName="nrLlogBankare_Label" Text="Nr. Llogari Bankare:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtNrLlogariBankare">--%>
                                    <dx:ASPxTextBox ID="txtNrLlogariBankare" runat="server" Width="100%" ClientInstanceName="txtNrLlogariBankare"
                                        ClientVisible="false">
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
                                    <%--</div>--%>
                                    <%--<div id="dviban_Label">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtIban" ID="iban_Label" runat="server"
                                        ClientInstanceName="iban_Label" Text="IBAN:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtIban">--%>
                                    <dx:ASPxTextBox ID="txtIban" runat="server" Width="100%" ClientInstanceName="txtIban"
                                        ClientVisible="true">
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
                                    <%--</div>--%>
                                    <%--<div id="dvshenime_Label">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime" ID="shenime_Label" runat="server"
                                        Text="Shenime:" ClientInstanceName="shenime_Label">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtShenime">--%>
                                    <dx:ASPxMemo ID="txtShenime" runat="server" Width="100%" ClientInstanceName="txtShenime"
                                        Rows="3">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                    <%--<div id="dvaktive_Label">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbAktive" ID="aktive_Label" runat="server"
                                        Text="Aktive:" ClientInstanceName="aktive_Label">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbAktive">--%>
                                    <dx:ASPxCheckBox ID="cbAktive" runat="server" Checked="true" ClientInstanceName="cbAktive"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
									
									<dx:ASPxLabel Wrap="False" AssociatedControlID="cbShfaqEinvoice" ID="lblShfaqEinvoice" runat="server"
                                         ClientInstanceName="lblShfaqEinvoice" ClientVisible = "false">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <dx:ASPxCheckBox ID="cbShfaqEinvoice" runat="server" ClientInstanceName="cbShfaqEinvoice"
                                        Width="100%" ClientVisible = "false">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <%--</div>--%>
                                    <%--<div id="dvnrllogari_Label">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrLlogari" ID="nrllogari_Label"
                                        runat="server" Text="Nr Llogari:" ClientInstanceName="nrllogari_Label">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtNrLlogari">--%>
                                    <dx:ASPxComboBox ID="txtNrLlogari" ClientInstanceName="txtNrLlogari" Width="100%"
                                        runat="server" OnItemRequestedByValue="txtNrLlogari_ItemRequestedByValue" OnItemsRequestedByFilterCondition="txtNrLlogari_ItemsRequestedByFilterCondition" EnableCallbackMode="True"
                                        IncrementalFilteringDelay="7" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
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
                                        <ClientSideEvents ButtonClick="function(s, e) {Llogari_Click(1);}" LostFocus="function(s,e) {validoNrLlogarie();}" TextChanged="function(s, e) {llogari_TextChanged(s,e);}" />
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvmonedha_Label">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMonedha" ID="monedha_Label" runat="server"
                                        Text=" Monedha:" ClientInstanceName="monedha_Label">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbMonedha">--%>
                                    <dx:ASPxComboBox ID="cmbMonedha" runat="server" Width="100%" ClientInstanceName="cmbMonedha"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
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
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvkomisioni_Label">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="komisioni_ButtonEdit" ID="komisioni_Label"
                                        runat="server" ClientInstanceName="komisioni_Label" Text="Komisioni">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvkomisioni_ButtonEdit">--%>
                                    <dx:ASPxComboBox ID="komisioni_ButtonEdit" runat="server" ClientInstanceName="komisioni_ButtonEdit"
                                        EnableCallbackMode="True" ClientVisible="false" Width="100%" OnItemRequestedByValue="komisioni_ButtonEdit_ItemRequestedByValue"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                        <ClientSideEvents ButtonClick="function(s, e) {Llogari_Click(2);}" TextChanged="function(s, e) { var s = komisioni_ButtonEdit.GetText().split(';');
                                                                                                                                         komisioni_ButtonEdit.SetText(s[0]); }" />
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblDegeAdministrative">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbDegeAdministrative" ID="lblDegeAdministrative"
                                        runat="server" Text="Inventarizimi:" ClientInstanceName="lblDegeAdministrative">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbDegeAdministrative">--%>
                                    <dx:ASPxComboBox ID="cmbDegeAdministrative" runat="server" ClientInstanceName="cmbDegeAdministrative"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
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
                                    <%--</div>--%>

                                    <%-- te reja   Dega, Numri i klientit, Valuta, Kodi i llogarise, Tipi--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtDega" ID="lblDega" runat="server"
                                        ClientInstanceName="lblDega" Text="IBAN:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtIban">--%>
                                    <dx:ASPxTextBox ID="txtDega" runat="server" Width="100%" ClientInstanceName="txtDega"
                                        ClientVisible="true">
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrKlienti" ID="lblNrKlienti" runat="server"
                                        ClientInstanceName="lblNrKlienti" Text="IBAN:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtIban">--%>
                                    <dx:ASPxTextBox ID="txtNrKlienti" runat="server" Width="100%" ClientInstanceName="txtNrKlienti"
                                        ClientVisible="true">
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

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtValuta" ID="lblValuta" runat="server"
                                        ClientInstanceName="lblValuta" Text="IBAN:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtIban">--%>
                                    <dx:ASPxTextBox ID="txtValuta" runat="server" Width="100%" ClientInstanceName="txtValuta"
                                        ClientVisible="true">
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


                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodiLlogarise" ID="lblKodiLlogarise" runat="server"
                                        ClientInstanceName="lblKodiLlogarise" Text="IBAN:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtIban">--%>
                                    <dx:ASPxTextBox ID="txtKodiLlogarise" runat="server" Width="100%" ClientInstanceName="txtKodiLlogarise"
                                        ClientVisible="true">
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


                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTipi" ID="lblTipi" runat="server"
                                        ClientInstanceName="lblTipi" Text="Tipi:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtIban">--%>
                                    <dx:ASPxTextBox ID="txtTipi" runat="server" Width="100%" ClientInstanceName="txtTipi"
                                        ClientVisible="true">
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

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btnTCR" ID="lblbtnTCR" runat="server" ClientVisible="false"
                                        ClientInstanceName="lblbtnTCR" Text="Gjenerim kodi TCR:">
                                    </dx:ASPxLabel>

                                     <dx:ASPxButton ID="btnTCR" runat="server" AutoPostBack="False" Text="Gjenero TCR" ClientVisible="false"
                                         ClientInstanceName="btnTCR" Width="100px">
                                          <ClientSideEvents Click="btnTCR_click" />  
                                     </dx:ASPxButton>

                                     <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTCR" ID="lbltxtTCR" runat="server" ClientVisible="false"
                                        ClientInstanceName="lbltxtTCR" Text="TCR:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtTCR">--%>
                                    <dx:ASPxTextBox ID="txtTCR" runat="server" Width="100%" ClientInstanceName="txtTCR" ClientVisible="false">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="lbltxtNrRendor" ID="lbltxtNrRendor" runat="server" ClientVisible="false"
                                            ClientInstanceName="lbltxtNrRendor" Text="Numri Rendor:">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvtxtNrRendor">--%>
                                        <dx:ASPxTextBox ID="txtNrRendor" runat="server" Width="100%" ClientInstanceName="txtNrRendor" ClientVisible="false">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>

                                  </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>

                        <dxtc:TabPage Name="Adresa" Text="Adresa">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl2" runat="server">
                                    <table id="tblAdresa" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblKodi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodiAdresa" ID="lblKodi" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtKodiAdresa">--%>
                                    <dx:ASPxTextBox ID="txtKodiAdresa" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtKodiAdresa">
                                        <ClientSideEvents TextChanged="TextChanged_txtKodiAdresa" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblEmer">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerBankeAdresa" ID="lblEmer"
                                        runat="server" Text="Emri:" ClientInstanceName="lblEmer">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtEmerBankeAdresa">--%>
                                    <dx:ASPxTextBox ID="txtEmerBankeAdresa" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtEmerBankeAdresa">
                                        <ClientSideEvents TextChanged="TextChanged_txtEmerBankeAdresa" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblRruga">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtRruga" ID="lblRruga" runat="server"
                                        Text="Rruga:" ClientInstanceName="lblRruga">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtRruga">--%>
                                    <dx:ASPxTextBox ID="txtRruga" Enabled="true" runat="server" Width="100%" ClientInstanceName="txtRruga">
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblQyteti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtQyteti" ID="lblQyteti" runat="server"
                                        Text="Qyteti:" ClientInstanceName="lblQyteti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtQyteti">--%>
                                    <dx:ASPxTextBox ID="txtQyteti" Enabled="true" runat="server" Width="100%" ClientInstanceName="txtQyteti">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblShteti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShteti" ID="lblShteti" runat="server"
                                        Text="Shteti:" ClientInstanceName="lblShteti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtShteti">--%>
                                    <dx:ASPxTextBox ID="txtShteti" Enabled="true" runat="server" Width="100%" ClientInstanceName="txtShteti">
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblZipKod">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtZipKod" ID="lblZipKod" runat="server"
                                        Text="Zip Kod:" ClientInstanceName="lblZipKod">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtZipKod">--%>
                                    <dx:ASPxTextBox ID="txtZipKod" Enabled="True" runat="server" Width="100%" ClientInstanceName="txtZipKod">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblTel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTel" ID="lblTel" runat="server"
                                        Text="Tel:" ClientInstanceName="lblTel">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtTel">--%>
                                    <dx:ASPxTextBox ID="txtTel" Enabled="true" runat="server" Width="100%" ClientInstanceName="txtTel">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true" ValidateOnLeave="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9]*" ErrorText="Sheno vetem numra!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Kontakti" Text="Kontakti">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl5" runat="server">
                                    <table id="tblKontakti" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblKodi2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodiKontakti" ID="lblKodi2" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtKodiKontakti">--%>
                                    <dx:ASPxTextBox ID="txtKodiKontakti" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtKodiKontakti">
                                        <ClientSideEvents TextChanged="TextChanged_txtKodiKontakti" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblEmer2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerBankeKontakti" ID="lblEmer2"
                                        runat="server" Text="Emri:" ClientInstanceName="lblEmer2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtEmerBankeKontakti">--%>
                                    <dx:ASPxMemo ID="txtEmerBankeKontakti" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtEmerBankeKontakti" Rows="3">
                                        <ClientSideEvents TextChanged="TextChanged_txtEmerBankeKontakti" />
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
                                    <%--<div id="dvlblEmerKontakti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmer" ID="lblEmerKontakti" runat="server"
                                        Text="Emri:" ClientInstanceName="lblEmerKontakti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtEmer">--%>
                                    <dx:ASPxTextBox ID="txtEmer" runat="server" Width="100%" ClientInstanceName="txtEmer">
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblMbiemri">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtMbiemri" ID="lblMbiemri" runat="server"
                                        Text="Mbiemri:" ClientInstanceName="lblMbiemri">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtMbiemri">--%>
                                    <dx:ASPxTextBox ID="txtMbiemri" runat="server" Width="100%" ClientInstanceName="txtMbiemri">
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblTelK">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTelK" ID="lblTelK" runat="server"
                                        Text="Tel:" ClientInstanceName="lblTelK">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtTelK">--%>
                                    <dx:ASPxTextBox ID="txtTelK" runat="server" Width="100%" ClientInstanceName="txtTelK">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9]*" ErrorText="Sheno vetem numra!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblFax">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtFax" ID="lblFax" runat="server"
                                        Text="Fax:" ClientInstanceName="lblFax">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtFax">--%>
                                    <dx:ASPxTextBox ID="txtFax" runat="server" Width="100%" ClientInstanceName="txtFax">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9]*" ErrorText="Sheno vetem numra!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblCel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtCel" ID="lblCel" runat="server"
                                        Text="Cel:" ClientInstanceName="lblCel">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtCel">--%>
                                    <dx:ASPxTextBox ID="txtCel" runat="server" Width="100%" ClientInstanceName="txtCel">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9]*" ErrorText="Sheno vetem numra!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblEmail">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmail" ID="lblEmail" runat="server"
                                        Text="Email:" ClientInstanceName="lblEmail">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtEmail">--%>
                                    <dx:ASPxTextBox ID="txtEmail" runat="server" Width="100%" ClientInstanceName="txtEmail">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorText="Format i gabuar e-mail!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblAdresa">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtAdresa" ID="lblAdresa" runat="server"
                                        Text="Adresa:" ClientInstanceName="lblAdresa">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtAdresa">--%>
                                    <dx:ASPxMemo ID="txtAdresa" runat="server" Width="100%" ClientInstanceName="txtAdresa"
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
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                    <ClientSideEvents ActiveTabChanged="Active_TabChanged" />
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                </dxtc:ASPxPageControl>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="hfAutorizime" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogari" runat="server" />
                    <asp:HiddenField ID="hfLupaKomisioni" runat="server" />
                    <asp:HiddenField ID="hfLupaGrupBanka" runat="server" />
                    <asp:HiddenField ID="hfLupaAutorizimi" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="updateraporti" runat="server">
                <ContentTemplate>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                        CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                        <ClientSideEvents Closing="function(s, e) {	popupUniversal.SetContentUrl(''); }" />
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
