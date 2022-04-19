<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KonfigurimDokumentash.aspx.cs"
    Inherits="PlatinumWeb.KonfigurimDokumentash" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <title>Alpha Web</title>
    <style type="text/css">
        .style1 {
            height: 18px;
        }
    </style>
    
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/aspx.js/KonfigurimDokumentash.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">     
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
           
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.    SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" runat="server">
        </dx:ASPxHiddenField>
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                <div id="dvMenu" style="display: none">
                                    <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <dx:ASPxMenu ID="MenuInfo" runat="server" BorderBetweenItemAndSubMenu="HideRootOnly"
                                                ClientIDMode="AutoID" ClientInstanceName="MenuInfo" ShowPopOutImages="True" Width="100%"
                                                AllowSelectItem="True">
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <div style="visibility: hidden">
                        <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Red" ClientInstanceName="pergjigja"
                            ClientVisible="false">
                        </dx:ASPxLabel>
                    </div>
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <table class="renditKontrolle">
                <tr>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="Kategoria_ComboBox" ID="lblKategoria"
                            runat="server" Text="Kategoria:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth50">
                        <dx:ASPxComboBox ID="Kategoria_ComboBox" runat="server" ClientInstanceName="Kategoria_ComboBox"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <ClientSideEvents SelectedIndexChanged="function(s,e){ Kategoria_ComboBoxSelectedIndexChanged(s, e); }" />
                        </dx:ASPxComboBox>
                    </td>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNivelRegj" ID="lblNenkategoria"
                            runat="server" Text="NenKategori:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth50">
                        <dx:ASPxComboBox ID="cmbNivelRegj" runat="server" ClientInstanceName="cmbNivelRegj"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <ClientSideEvents SelectedIndexChanged="function(s,e){indexChange();}" />
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="kodKonfig_TextBox" ID="kod_Label"
                            runat="server" Text="Kodi i Konfigurimit:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth50">
                        <dx:ASPxTextBox ID="kodKonfig_TextBox" runat="server" Width="100%" ClientInstanceName="kodKonfig_TextBox">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries">
                                <ErrorImage Height="14px" />
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="pershkrimKonfig_TextBox" ID="pershkrimi_Label"
                            runat="server" Text="Pershkrimi Shqip i Konfigurimit:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth50">
                        <dx:ASPxTextBox ID="pershkrimKonfig_TextBox" runat="server" Width="100%" ClientInstanceName="pershkrimKonfig_TextBox">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries">
                                <ErrorImage Height="14px" />
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                        <%--   <dx:ASPxMemo ID="pershkrimKonfig_TextBox" runat="server" Width="100%" ClientInstanceName="pershkrimKonfig_TextBox"
                            Rows="3">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries">
                                <ErrorImage Height="14px" />
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxMemo>--%>
                    </td>
                </tr>
                <tr>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAutorizimi" ID="lblAutorizimi"
                            runat="server" Text="Autorizimi:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth50">
                        <%--<dx:ASPxComboBox ID="cmbAutorizimi" ClientInstanceName="cmbAutorizimi" Width="100%"
                            runat="server" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
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
                                <RequiredField IsRequired="false" />
                            </ValidationSettings>
                            <ClientSideEvents ButtonClick="function(s, e) {KPF=0;Autorizime_Click();}" SelectedIndexChanged="function(s,e){Utils.SelektimiBosh(s,e);}" />
                        </dx:ASPxComboBox>--%>
                        <div>
                            <select id="cmbAutorizimi">
                            </select>
                            <asp:HiddenField ID="cmbAutorizimiHf" ClientIDMode="Static" runat="server" />

                        </div>
                    </td>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="pershkrimKonfigEng_TextBox" ID="pershkrimiEng_Label"
                            runat="server" Text="Pershkrimi Anglisht i Konfigurimit:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth50">
                        <dx:ASPxTextBox ID="pershkrimKonfigEng_TextBox" runat="server" Width="100%" ClientInstanceName="pershkrimKonfigEng_TextBox">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries">
                                <ErrorImage Height="14px" />
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>                        
                    </td>
                </tr>
                <tr>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="radhaTextBox" ID="radhaLabel" runat="server"
                            Text="Radha:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth50">
                        <dx:ASPxTextBox ID="radhaTextBox" runat="server" Width="100%">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries">
                                <ErrorImage Height="14px" />
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="pershkrimKonfigFr_TextBox" ID="pershkrimiFr_Label"
                            runat="server" Text="Pershkrimi Frengjisht i Konfigurimit:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth50">
                        <dx:ASPxTextBox ID="pershkrimKonfigFr_TextBox" runat="server" Width="100%" ClientInstanceName="pershkrimKonfigFr_TextBox">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries">
                                <ErrorImage Height="14px" />
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>                        
                    </td>
                </tr>
                <tr>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="Lloji_cmb" ID="lblLloji" runat="server"
                            Text="Lloji">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth50">
                        <dx:ASPxComboBox ID="Lloji_cmb" runat="server" ClientInstanceName="Lloji_cmb" AutoPostBack="false"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%" ValueType="System.Int32">
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <%--   <ClientSideEvents SelectedIndexChanged="function(s,e){ Kategoria_ComboBoxSelectedIndexChanged(s, e); }" />--%>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbFormatNumri" ID="lblFormatNumri" runat="server"
                            Text="Formati i numrave:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth50">
                        <dx:ASPxComboBox ID="cmbFormatNumri" ClientInstanceName="cmbFormatNumri" Width="100%"
                            runat="server" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" EnableCallbackMode="True"
                            OnItemRequestedByValue="cmbFormatNumri_ItemRequestedByValue">
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
                                <RequiredField IsRequired="false" />
                            </ValidationSettings>
                            <ClientSideEvents ButtonClick="function(s, e) { FormatNumri_Click(); }" />
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
            <br />
            <dxcp:ASPxCallbackPanel EnableHierarchyRecreation="false" ID="ASPxCallbackPanel1" runat="server" Width="100%" ClientInstanceName="panel"
                OnCallback="ASPxCallbackPanel1_Callback">
                <ClientSideEvents EndCallback="function (s,e) {index_EndCallback();}" />
                <ClientSideEvents EndCallback="function (s,e) {index_EndCallback();}"></ClientSideEvents>
                <PanelCollection>
                    <dxp:PanelContent>
                        <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
                        <table width="100%" style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Kontrollet">
                                    </dx:ASPxLabel>
                                    <dx:ASPxGridView ID="grid_kontrollet" runat="server" Width="98%" ClientInstanceName="grid_kontrollet"
                                                     OnHtmlRowCreated="grid_kontrollet_HtmlRowCreated" OnAfterPerformCallback="grid_kontrollet_AfterPerformCallback"
                                                     OnCustomJSProperties="grid_kontrollet_CustomJSProperties">
                                        <ClientSideEvents BeginCallback="function(s, e) { merrTeDhena(); }"
                                                          EndCallback="function(s, e) { ShfaqTeDhenat(); }">
                                        </ClientSideEvents>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Grida">
                                    </dx:ASPxLabel>
                                    <dx:ASPxGridView ID="grid_trupi" runat="server" Width="98%" ClientInstanceName="grid_trupi"
                                        OnHtmlRowCreated="grid_trupi_HtmlRowCreated" OnAfterPerformCallback="grid_trupi_AfterPerformCallback"
                                        OnCustomJSProperties="grid_trupi_CustomJSProperties">
                                        <ClientSideEvents BeginCallback="function(s, e) { merrTeDhenaTrupi(); }"
                                            EndCallback="function(s, e) { ShfaqTeDhenatTrupi(); }">
                                        </ClientSideEvents>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Kushtet">
                                    </dx:ASPxLabel>
                                    <dx:ASPxGridView ID="grid_kushte" runat="server" Width="98%" ClientInstanceName="grid_kushte"
                                        OnHtmlRowCreated="grid_kushte_HtmlRowCreated" OnAfterPerformCallback="grid_kushte_AfterPerformCallback"
                                        OnCustomJSProperties="grid_kushte_CustomJSProperties">
                                        <ClientSideEvents BeginCallback="function(s, e) { merrTeDhenaKushte(); }" /> 
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hfVleratFillestareKontrollet" runat="server" />
                        <asp:HiddenField ID="hfVleratFillestareTrupi" runat="server" />
                        <asp:HiddenField ID="hfVleratFillestareKushtet" runat="server" />
                        <asp:HiddenField ID="hfSkemaKontabelRegjistrime" runat="server" />
                        <asp:HiddenField ID="hfRezervim" runat="server" />
                        <asp:HiddenField ID="hfMagazina" runat="server" />
                        <asp:HiddenField ID="hfShitja" runat="server" />
                        <asp:HiddenField ID="hfAmortizimi" runat="server" />
                        <asp:HiddenField ID="hfQK" runat="server" />
                        <asp:HiddenField ID="hfFormula" runat="server" />
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:HiddenField ID="HiddenFieldTrupi" runat="server" />
                        <asp:HiddenField ID="HiddenFieldKusht" runat="server" />
                        <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                        <asp:HiddenField ID="hfCheck" runat="server" />
                        <asp:HiddenField ID="hfEmertimi" runat="server" />
                        <asp:HiddenField ID="hfPrioriteti" runat="server" />
                        <asp:HiddenField ID="hfFiltri" runat="server" />
                        <asp:HiddenField ID="hfVleraMag" runat="server" />
                        <asp:HiddenField ID="hfVleraAmor" runat="server" />
                        <asp:HiddenField ID="hfVleraFleteHyrje" runat="server" />
                        <asp:HiddenField ID="hfVleraShit" runat="server" />
                        <asp:HiddenField ID="hfVleraMag2" runat="server" />
                        <asp:HiddenField ID="hfVleraBarkod" runat="server" />
                        <asp:HiddenField ID="hfVleraKont" runat="server" />
                        <asp:HiddenField ID="hfVleraDokVartes" runat="server" />
                        <asp:HiddenField ID="hfBart" runat="server" />
                        <asp:HiddenField ID="hfLidhjeArketim" runat="server" />   
                        <asp:HiddenField ID="hfFIDK" runat="server" />
                        <asp:HiddenField ID="hfNrAutoDok" runat="server" />
                        <asp:HiddenField ID="hfFletaKont" runat="server" />
                        <asp:HiddenField ID="hfIdKushTemplate" runat="server" />
                        <asp:HiddenField ID="hfLlojRreshtiVlera" runat="server" />
                        <asp:HiddenField ID="hfPezull" runat="server" />
                        <asp:HiddenField ID="hfProces" runat="server" />    
                        <asp:HiddenField ID="hfZevendesim" runat="server" /> 
                        <asp:HiddenField ID="hfZevendesimT" runat="server" />
                        <asp:HiddenField ID="hfZevendesimVlera" runat="server" />
                        <asp:HiddenField ID="hfLlojSubjektiDefaultVlera" runat="server" />
                        <asp:HiddenField ID="hfSubjektiDefaultVlera" runat="server" />
                        <asp:HiddenField ID="hfArketim" runat="server" />
                        <asp:HiddenField ID="hfVleraArk" runat="server" />
                        <asp:HiddenField ID="hfPagese" runat="server" />
                        <asp:HiddenField ID="hfVleraPagese" runat="server" />
                        <asp:HiddenField ID="hfBlere" runat="server" />
                        <asp:HiddenField ID="hfVleraBlere" runat="server" />
                        <asp:HiddenField ID="hfVleraRSKDMD" runat="server" />
                        <asp:HiddenField ID="hfSHDQKPMD" runat="server" />
                        <asp:HiddenField ID="hfTKLL_B" runat="server" />
                        <asp:HiddenField ID="hfZLL_B" runat="server" />
                        <asp:HiddenField ID="hfNKAKNKA" runat="server" />
                        <asp:HiddenField ID="hfSDAF" runat="server" />
                        <asp:HiddenField ID="hfZSP" runat="server" />
                        <%--  </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel >
        </div>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" AllowResize="True"
                    AppearAfter="10" ClientIDMode="AutoID" ClientInstanceName="popupUniversal" CloseAction="CloseButton"
                    EnableAnimation="False" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="Middle">
                    <ClientSideEvents Closing="function(s, e) { popupUniversal.SetContentUrl(''); }" />
                    <ContentStyle>
                        <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                            PaddingTop="1px" />
                    </ContentStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hfKodLupa" runat="server" />
        <asp:HiddenField ID="hfGridaKodLupa" runat="server" />
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
    </form>
</body>
</html>
