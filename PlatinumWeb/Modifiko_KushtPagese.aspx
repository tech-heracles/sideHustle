<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Modifiko_KushtPagese.aspx.cs"
    Inherits="PlatinumWeb.Modifiko_KushtPagese" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Modifiko_KushtPagese.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server" style="width: 100%">
         
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.    SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <asp:UpdatePanel ID="pnl" runat="server">
            <ContentTemplate>
                <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
                    ActiveTabIndex="0"   TabSpacing="3px" Width="100%">
                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    </ContentStyle>
                    <TabPages>
                        <dxtc:TabPage Text="Kusht Pagese" Name="Kusht Pagese">
                            <TabStyle Height="150px">
                            </TabStyle>
                            <ContentCollection>
                                <dxw:ContentControl runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Kodi:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="kodi_TextBox" runat="server" ClientInstanceName="kodi_TextBox"
                                                    Enabled="false" Width="170px">
                                                    <ClientSideEvents TextChanged="function(s, e) {
	kodi1_TextBox.SetText(kodi_TextBox.GetText());
}" />
                                                    <ValidationSettings CausesValidation="True" ValidationGroup="entries" SetFocusOnError="True">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RequiredField IsRequired="True" ErrorText="Jepni Kodin" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Emertimi:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="emertimi_TextBox" runat="server" ClientInstanceName="emertimi_TextBox"
                                                    Width="170px">
                                                    <ClientSideEvents TextChanged="function(s, e) {
	 emertimi1_TextBox.SetText(emertimi_TextBox.GetText());
}" />
                                                    <ValidationSettings CausesValidation="True" ValidationGroup="entries" SetFocusOnError="True">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RequiredField IsRequired="True" ErrorText="Jepni emertimin" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Pagesa:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxComboBox ID="pagesa_ASPxComboBox" runat="server" Width="170px" ClientInstanceName="pagesa_ASPxComboBox"
                                                    ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {enable();}" TextChanged="function(s, e) {TextChangedPagesa()}" />
                                                    <LoadingPanelImage>
                                                    </LoadingPanelImage>
                                                    <DropDownButton>
                                                        <Image>
                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                        </Image>
                                                    </DropDownButton>
                                                    <ValidationSettings CausesValidation="True" ValidationGroup="entries" SetFocusOnError="True">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RequiredField IsRequired="True" ErrorText="Zgjidhni llojin e pageses" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lblAutorizimi" runat="server" Text="Autorizimi:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxComboBox ID="txtAutorizimi" ClientInstanceName="txtAutorizimi" Width="170px"
                                                    runat="server" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                            grida=false;
                                            Autorizimi_Click();}" LostFocus="function(s, e) {
                                            editorAutorizimi= eval('IdNivelAutorizimi');
	    editorAutorizimi.SetText(txtAutorizimi.GetText());

	ProcessTextChanged('IdNivelAutorizimi',txtAutorizimi.GetText() );  
}" />
                                                    <LoadingPanelImage>
                                                    </LoadingPanelImage>
                                                    <DropDownButton>
                                                        <Image>
                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                        </Image>
                                                    </DropDownButton>
                                                    <ValidationSettings CausesValidation="True" ValidationGroup="entries" SetFocusOnError="True">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div>
                                        <table class="butonat">
                                            <tr>
                                                <td style="width: 70%">
                                                    &nbsp;
                                                </td>
                                                <td align="right" style="width: 10%">
                                                    <dx:ASPxButton ID="ruaj_ASPxButton" runat="server" OnClick="ruaj_Button_Click" Text="Ruaj"
                                                        Width="100%" ValidationGroup="entries">
                                                        <ClientSideEvents Click="function(s,e){valido(s,e);RuajClick(s,e);}" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td align="right" style="width: 10%">
                                                    <dx:ASPxButton ID="pastro_ASPxButton" runat="server" Text="Pastro" Width="100%"
                                                        OnClick="pastro_Button_Click" CausesValidation="False">
                                                        <ClientSideEvents Click="function (s,e){pastro()}" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td align="right" style="width: 10%">
                                                    <dx:ASPxButton ID="anullo_ASPxButton" runat="server" PostBackUrl="~/KushtePagese.aspx"
                                                        Text="Anullo" Width="100%" CausesValidation="False">
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Text="Trupi" Name="Trupi">
                            <TabStyle Height="150px">
                            </TabStyle>
                            <ContentCollection>
                                <dxw:ContentControl runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Kodi:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="kodi1_TextBox" runat="server" ClientInstanceName="kodi1_TextBox"
                                                    Enabled="false" Width="170px">
                                                    <ClientSideEvents TextChanged="function(s, e) {
	 kodi_TextBox.SetText(kodi1_TextBox.GetText());
}" />
                                                    <ValidationSettings>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td align="center">
                                                <dx:ASPxLabel ID="lblAfati" runat="server" Text="Afati:" ClientInstanceName="lblAfati">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="afati_TextBox" runat="server" ClientInstanceName="afati_TextBox"
                                                    Width="170px">
                                                    <ValidationSettings RequiredField-IsRequired="True" ValidationGroup="entries" RegularExpression-ValidationExpression="[0-9]*"
                                                        RegularExpression-ErrorText="Afati duhet te jete numer pozitiv">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RegularExpression ErrorText="Afati duhet te jete numer pozitiv" ValidationExpression="[0-9]*" />
                                                        <RequiredField IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Emertimi:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="emertimi1_TextBox" runat="server" ClientInstanceName="emertimi1_TextBox"
                                                    Width="170px">
                                                    <ClientSideEvents TextChanged="function(s, e) {
	 emertimi_TextBox.SetText(emertimi1_TextBox.GetText());
}" />
                                                    <ValidationSettings>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lblNdarja" runat="server" Text="Ndarja:" ClientInstanceName="lblNdarja">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxComboBox ID="ndarja_ASPxComboBox" runat="server" Width="170px" ClientInstanceName="ndarja_ASPxComboBox"
                                                    ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {enable();}" TextChanged="function(s, e) {TextChangedNdarja()}" />
                                                    <LoadingPanelImage>
                                                    </LoadingPanelImage>
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
                                            <td>
                                                <dx:ASPxLabel ID="lblIntervali" runat="server" Text="Intervali midis ndarjeve:"
                                                    ClientInstanceName="lblIntervali">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxComboBox ID="intervali_ASPxComboBox" runat="server" ClientInstanceName="intervali_ASPxComboBox"
                                                    ShowShadow="False" ValueType="System.String" Width="170px" SettingsLoadingPanel-ImagePosition="Top">
                                                    <LoadingPanelImage>
                                                    </LoadingPanelImage>
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
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblNrNdarjeve" runat="server" Text="Numri i ndarjeve:" ClientInstanceName="lblNrNdarjeve">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="nrNdarjeve_TextBox" runat="server" ClientInstanceName="nrNdarjeve_TextBox"
                                                    Width="170px">
                                                    <ValidationSettings>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div>
                                        <dx:ASPxGridView ID="grid_trupi" runat="server" ClientInstanceName="grid_trupi"
                                            Width="60%" OnAfterPerformCallback="grid_trupi_AfterPerformCallback" OnHtmlRowCreated="grid_trupi_HtmlRowCreated"
                                            OnCustomJSProperties="grid_trupi_CustomJSProperties" OnCustomCallback="grid_trupi_CustomCallback"
                                            OnHtmlFooterCellPrepared="grid_trupi_HtmlFooterCellPrepared">
                                            <Styles>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                            <StylesEditors>
                                                <ProgressBar Height="25px">
                                                </ProgressBar>
                                            </StylesEditors>
                                        </dx:ASPxGridView>
                                    </div>
                                    <div>
                                        <table class="butonat">
                                            <tr>
                                                <td style="width: 70%">
                                                    &nbsp;
                                                </td>
                                                <td align="right" style="width: 10%">
                                                    <dx:ASPxButton ID="ruaj_Button" runat="server" OnClick="ruaj_Button_Click" Text="Ruaj"
                                                        Width="100%" ValidationGroup="entries">
                                                        <ClientSideEvents Click="function(s,e){valido(s,e);RuajClick(s,e);}" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td align="right" style="width: 10%">
                                                    <dx:ASPxButton ID="pastro_Button" runat="server" Text="Pastro" Width="100%" OnClick="pastro_Button_Click"
                                                        CausesValidation="False">
                                                        <ClientSideEvents Click="function (s,e){pastro()}" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td align="right" style="width: 10%">
                                                    <dx:ASPxButton ID="anullo_Button" runat="server" PostBackUrl="~/KushtePagese.aspx"
                                                        Text="Anullo" Width="100%" CausesValidation="False" Height="25px">
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                </dxtc:ASPxPageControl >
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="hfTrupiFillimit" runat="server" />
                <asp:HiddenField ID="HiddenFieldTrupi" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Zgjidh autorizimin "
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <iframe id="Container" name="ContainerAutorizimi" frameborder="0" runat="server">
                            </iframe>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxLabel ID="pergjigja" runat="server" Text="">
        </dx:ASPxLabel>
    </div>
    </form>
</body>
</html>
