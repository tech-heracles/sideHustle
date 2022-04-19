<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaKlientShpejte.aspx.cs"
    Inherits="PlatinumWeb.LupaKlientShpejte" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/LupaKlientShpejte.aspx-IMB.2.2.js&v76" type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server" style="width: 100%">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                OnItemClick="ASPxMenu1_ItemClick">
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                <ClientSideEvents ItemClick="function(s, e) {
	                            menu_click(s,e);
                                }"
                                    Init="function(s) {s.SetClientVisible(true);}" />
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvKlientFurnitor" style="display: none">
            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server">
                <PanelCollection>
                    <dx:PanelContent>
                        <table class="renditKontrolle">
                            <tr>
                                <td class="renditKontrolleCaption">
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                        runat="server" ClientIDMode="AutoID" Text="Modeli:">
                                    </dx:ASPxLabel>
                                </td>
                                <td class="renditKontrolleCellMeWidth33">
                                    <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                        ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                                        Width="100%" AnimationType="None">
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
                        <br />
                        <table id="tblInformacion" class="renditKontrolle">
                            <tbody>
                            </tbody>
                        </table>
                        <%--<div id="dvlblKodi">--%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                            Text="Kodi:" ClientInstanceName="lblKodi">
                        </dx:ASPxLabel>
                        <%--</div>--%>
                        <%--<div id="dvtxtKodi">--%>
                        <dx:ASPxTextBox ID="txtKodi" runat="server" AutoPostBack="false" ClientInstanceName="txtKodi"
                            Width="100%">
                            <ClientSideEvents TextChanged="function(s, e) {
                                                             
                                                            }"
                                Init="function(s, e) { s.Focus(); }" />
                            <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="True"
                                Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 100 karaktere" ValidationExpression="^[\s\S]{0,100}$"></RegularExpression>
                                <RequiredField IsRequired="True"></RequiredField>
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <%--</div>--%>
                        <%--<div id="dvlblEmertimi">--%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi" ID="lblEmertimi" runat="server"
                            Text="Emertimi:" ClientInstanceName="lblEmertimi">
                        </dx:ASPxLabel>
                        <%--</div>--%>
                        <%--<div id="dvtxtEmertimi">--%>
                        <dx:ASPxMemo ID="txtEmertimi" runat="server" AutoPostBack="false" ClientInstanceName="txtEmertimi"
                            Width="100%" Rows="3">
                            <%--  <ClientSideEvents TextChanged="function(s, e) {
	                                             
                                                    }       " />--%>
                            <ClientSideEvents TextChanged="function(s, e) {txtEmertimiTextChanged(s,e);}" />
                            <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="True"
                                Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="True"></RequiredField>
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxMemo>
                        <%--</div>--%>
                        <%--<div id="dvlblNrLlog2">--%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNr2" ID="lblNrLlog2" runat="server"
                            Text="Nr. Llogari:" ClientInstanceName="lblNrLlog2">
                        </dx:ASPxLabel>
                        <%--</div>--%>
                        <%--<div id="dvtxtNr2">--%>
                        <dx:ASPxComboBox ID="txtNr2" runat="server" AutoPostBack="false" ClientInstanceName="txtNr2"
                            CallbackPageSize="10" EnableCallbackMode="True" OnItemRequestedByValue="txtNr2_ItemRequestedByValue" OnItemsRequestedByFilterCondition="txtNr2_ItemsRequestedByFilterCondition"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                            <ClientSideEvents ButtonClick=" function(s,e) {ButtonClickedLlogaria(s); }" LostFocus="function(s, e) {LostFocusNrLlogarieTxtNr2(s);}"
                                TextChanged="function(s, e) {
	nrLlogariChange();
}" />
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries" SetFocusOnError="true" EnableCustomValidation="True">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="True"></RequiredField>
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLlogDytesor" ID="lblLlogDytesore"
                            runat="server" Text="Llogari dytesore:" ClientInstanceName="lblLlogDytesore">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvtxtLlogDytesor"> --%>
                        <dx:ASPxComboBox ID="txtLlogDytesor" runat="server" ClientInstanceName="txtLlogDytesor"
                            OnItemRequestedByValue="txtLlogDytesor_ItemRequestedByValue" OnItemsRequestedByFilterCondition="txtLlogDytesor_ItemsRequestedByFilterCondition" SettingsLoadingPanel-ImagePosition="Top"
                            ShowShadow="False" Width="100%">
                            <ClientSideEvents ButtonClick="function(s, e) {ButtonClickedParaLlogaria(s);}" TextChanged="function(s, e) {
	nrLlogariParaChange();
}"
                                LostFocus="function(s, e) {
	nrLlogariParaChange();
}" />
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
                                <RequiredField IsRequired="True"></RequiredField>
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                        <%--  <asp:CustomValidator ID="nrLLogariseCostumValidator" runat="server" ValidationGroup="entries"
                            ValidateEmptyText="False" ControlToValidate="txtNr2" ClientValidationFunction="validateLLogariKod"
                            ErrorMessage="Nr. i llogarise nuk ekziston!" SetFocusOnError="True"></asp:CustomValidator>--%>
                        <%--</div>--%>
                        <%-- </div>--%>
                        <%--<div id="dvlblTitulli"> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbTitulli" ID="lblTitulli" runat="server"
                            Text="Titulli:" ClientInstanceName="lblTitulli">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvcmbTitulli"> --%>
                        <dx:ASPxComboBox ID="cmbTitulli" runat="server" ClientInstanceName="cmbTitulli"
                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
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
                        <%--</div> --%>

                        <%--<div id="dvlblMonedha">--%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="lblPershkrimMonedha" ID="lblMonedha"
                            runat="server" Text="Monedha:" ClientInstanceName="lblMonedha">
                        </dx:ASPxLabel>
                        <%--</div>--%>
                        <%--<div id="dvlblPershkrimMonedha" 
                        align="center">--%>
                        <dx:ASPxTextBox ID="lblPershkrimMonedha" runat="server" Text="" ClientInstanceName="lblPershkrimMonedha"
                            class="klasePerLblKonfigurimi" Height="25px" Width="100%">
                            <Border BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" />
                        </dx:ASPxTextBox>
                        <%--</div>--%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNipt" ID="lblNipt" runat="server"
                            Text="Nipt:" ClientInstanceName="lblNipt">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvtxtNipt"> --%>
                        <dx:ASPxTextBox ID="txtNipt" runat="server" ClientInstanceName="txtNipt" Width="100%">
                            <ClientSideEvents TextChanged="function(s, e) {	
}" />
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries1" ValidateOnLeave="false">
                                <RequiredField IsRequired="True"></RequiredField>
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <%--<div id="dvlblLlojAdrese"> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojAdrese" ID="lblLlojAdrese"
                            runat="server" Text="Lloj adrese:" ClientInstanceName="lblLlojAdrese">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvcmbLlojAdrese"> --%>
                        <dx:ASPxComboBox ID="cmbLlojAdrese" runat="server" ClientInstanceName="cmbLlojAdrese"
                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	adresat();
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
                        <%--</div> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtAdresa" ID="lblAdresa" runat="server"
                            Text="Adresa:" ClientInstanceName="lblAdresa">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvtxtAdresa"> --%>
                        <dx:ASPxMemo ID="txtAdresa" runat="server" ClientInstanceName="txtAdresa" Width="100%"
                            Rows="3">
                            <ClientSideEvents TextChanged="function(s, e) {
	ndryshoVlereAdreseKodiPostar();
}" />

                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="false">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="True"></RequiredField>
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxMemo>
                        <%--</div> --%>
                        <%--<div id="dvlblKodiPostar"> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodiPostar" ID="lblKodiPostar"
                            runat="server" Text="Kodi Postar:" ClientInstanceName="lblKodiPostar">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvtxtKodiPostar"> --%>
                        <dx:ASPxTextBox ID="txtKodiPostar" runat="server" ClientInstanceName="txtKodiPostar"
                            Width="100%">
                            <ClientSideEvents TextChanged="function(s, e) {
	ndryshoVlereAdreseKodiPostar();
}" />

                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="false">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="True"></RequiredField>
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <%--</div> --%>
                        <%--<div id="dvlblKategoriZbritje"> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKategoriZbritje" ID="lblKategoriZbritje"
                            runat="server" Text="Kategori Zbritje(totale):" ClientInstanceName="lblKategoriZbritje">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvbtneKategoriZbritje"> --%>
                        <dx:ASPxComboBox ID="btneKategoriZbritje" Enabled="true" runat="server" ClientInstanceName="btneKategoriZbritje"
                            EnableCallbackMode="False"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                            <ClientSideEvents ButtonClick="function(s, e) { KategoriZbritje_Click();}" TextChanged="function (s,e) {var s=btneKategoriZbritje.GetText().split(';');
                                       btneKategoriZbritje.SetText(s[0]);
                                       txtKategoriPerqindja.SetText(s[2]);}" />
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries1" ValidateOnLeave="false">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                        <%--</div> --%>
                        <%--<div id="dvlblKategoriPerqindje"> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKategoriPerqindja" ID="lblKategoriPerqindje"
                            runat="server" Text="Perqindja:" ClientInstanceName="lblKategoriPerqindje">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvtxtKategoriPerqindja"> --%>
                        <dx:ASPxTextBox ID="txtKategoriPerqindja" ClientInstanceName="txtKategoriPerqindja"
                            runat="server" Width="100%">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                ValidationGroup="entries" SetFocusOnError="True">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <%--</div> --%>
                        <%--<div id="dvlblZbritjeAnalitike"> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneNivelZbritje" ID="lblZbritjeAnalitike"
                            runat="server" Text="Zbritje Analitike:" ClientInstanceName="lblZbritjeAnalitike">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvbtneNivelZbritje"> --%>
                        <dx:ASPxComboBox ID="btneNivelZbritje" Enabled="true" runat="server" ClientInstanceName="btneNivelZbritje"
                            EnableCallbackMode="True" OnItemRequestedByValue="btneNivelZbritje_ItemRequestedByValue"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                            <ClientSideEvents ButtonClick="function(s, e) { NivelZbritje_Click();}" TextChanged="function(s,e){ var s=btneNivelZbritje.GetText().split(','); btneNivelZbritje.SetText(s[1]);}" />
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries1" ValidateOnLeave="false">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                        <%--</div> --%>
                        <%--<div id="dvlblNivelCmimi"> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneNivelCmimi" ID="lblNivelCmimi"
                            runat="server" Text="Nivel Cmimi:" ClientInstanceName="lblNivelCmimi">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvbtneNivelCmimi"> --%>
                        <dx:ASPxComboBox ID="btneNivelCmimi" Enabled="true" runat="server" ClientInstanceName="btneNivelCmimi"
                            EnableCallbackMode="True" OnItemRequestedByValue="btneNivelCmimi_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneNivelCmimi_ItemsRequestedByFilterCondition"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                            <ClientSideEvents ButtonClick="function(s, e) { NivelCmimi_Click();}" TextChanged="function(s, e) {niveliChange();
                                       }" />
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidateOnLeave="false"
                                ValidationGroup="entries" SetFocusOnError="true">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                        <%--</div> --%>
                        <%--<div id="dvlblGrupimi1"> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneGrupimi1" ID="lblGrupimi1" runat="server"
                            Text="Grupimi 1:" ClientInstanceName="lblGrupimi1">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvbtneGrupimi1"> --%>
                        <dx:ASPxComboBox ID="btneGrupimi1" runat="server" ClientInstanceName="btneGrupimi1"
                            EnableCallbackMode="False"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                            <ClientSideEvents ButtonClick="function(s, e) {
                                            grida='1';
                                            GrupiKF_Click(1);
                                            }"
                                TextChanged="function(s, e) {
                                            }"
                                SelectedIndexChanged="function(s,e){Utils.SelektimiBosh(s,e);}" />
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
                        <%--</div> --%>
                        <%--<div id="dvlblGrupimi2"> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneGrupimi2" ID="lblGrupimi2" runat="server"
                            Text="Grupimi 2:" ClientInstanceName="lblGrupimi2">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvbtneGrupimi2"> --%>
                        <dx:ASPxComboBox ID="btneGrupimi2" runat="server" ClientInstanceName="btneGrupimi2"
                            EnableCallbackMode="False"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                            <ClientSideEvents ButtonClick="function(s, e) {
										grida ='2';
										GrupiKF_Click(2);
										}"
                                TextChanged="function(s, e) {
										}"
                                SelectedIndexChanged="function(s,e){Utils.SelektimiBosh(s,e);}" />
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
                        <%--</div> --%>
                        <%--<div id="dvlblGrupimi3"> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneGrupimi3" ID="lblGrupimi3" runat="server"
                            Text="Grupimi 3:" ClientInstanceName="lblGrupimi3">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvbtneGrupimi3"> --%>
                        <dx:ASPxComboBox ID="btneGrupimi3" runat="server" ClientInstanceName="btneGrupimi3"
                            EnableCallbackMode="False"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                            <ClientSideEvents ButtonClick="function(s, e) {
											grida='3';
											GrupiKF_Click(3);}"
                                TextChanged="function(s, e) { }"
                                SelectedIndexChanged="function(s,e){Utils.SelektimiBosh(s,e);}" />
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
                        <%-- </div>--%>
                        <%-- <div id="dvlblAktiviteti">--%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtAktiviteti" ID="lblAktiviteti"
                            runat="server" Text="Aktiviteti:" ClientInstanceName="lblAktiviteti">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvtxtAktiviteti"> --%>
                        <dx:ASPxTextBox ID="txtAktiviteti" runat="server" ClientInstanceName="txtAktiviteti"
                            Width="100%">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                ValidateOnLeave="false">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="True"></RequiredField>
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <%-- </div>--%>
                        <%--<div id="dvlblAgjentShitje"> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAgjentShitjesh" ID="lblAgjentShitje"
                            runat="server" Text="Agjent Shitjesh:" ClientInstanceName="lblAgjentShitje">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvcmbAgjentShitjesh"> --%>
                        <dx:ASPxComboBox ID="cmbAgjentShitjesh" runat="server" ClientInstanceName="cmbAgjentShitjesh"
                            ShowShadow="False" EnableCallbackMode="false"
                            SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                            <ClientSideEvents ButtonClick="function(s,e){AgjentShitjesh_Click()}" />
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="false">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="True"></RequiredField>
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                        <%--</div> --%>
                        <%--<div id="dvlblEmriBanka"> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmriBanka" ID="lblEmriBanka"
                            runat="server" Text="Banka:" ClientInstanceName="lblEmriBanka">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvtxtEmriBanka"> --%>
                        <dx:ASPxComboBox ID="txtEmriBanka" ClientInstanceName="txtEmriBanka" runat="server"
                            EnableCallbackMode="True" ShowShadow="False" ValueType="System.String" Style="margin-bottom: 0px"
                            TabIndex="3" OnItemRequestedByValue="txtEmriBanka_ItemRequestedByValue" OnItemsRequestedByFilterCondition="txtEmriBanka_ItemsRequestedByFilterCondition" AutoPostBack="false"
                            SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickBanka();}" TextChanged="function(s,e){TextChangedBanka()}" />
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
                        <%--</div> --%>
                        <%--<div id="dvlblPershkrimiEmriBanka" style=" width: 100px;
                                    vertical-align: bottom;" align="center"> --%>
                        <dx:ASPxTextBox ID="lblPershkrimiEmriBanka" runat="server" Text="" ClientInstanceName="lblPershkrimiEmriBanka"
                            class="klasePerLblKonfigurimi" Height="25px" Width="100%">
                        </dx:ASPxTextBox>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTel" ID="lblTel" runat="server"
                            Text="Tel:" ClientInstanceName="lblTel">
                        </dx:ASPxLabel>

                        <dx:ASPxTextBox ID="txtTel" runat="server" ClientInstanceName="txtTel" Width="100%">

                            <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="True" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                <RequiredField IsRequired="True"></RequiredField>
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>

                        </dx:ASPxTextBox>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmail" ID="lblEmail" runat="server"
                            Text="Email:" ClientInstanceName="lblEmail">
                        </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtEmail" runat="server" ClientInstanceName="txtEmail" Width="100%">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries" SetFocusOnError="true">
                                <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ErrorText="Format i gabuar e-mail!" />
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="True"></RequiredField>
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerKerkimi" ID="lblEmerKerkimi"
                            runat="server" Text="Emer Kerkimi:" ClientInstanceName="lblEmerKerkimi">
                        </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtEmerKerkimi" runat="server" ClientInstanceName="txtEmerKerkimi"
                            Width="100%">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                ValidateOnLeave="false">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbBij" ID="lblBij" runat="server"
                            Text="Ndermarrje bij:" ClientInstanceName="lblBij">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="cmbBij" runat="server" Width="100%" ClientInstanceName="cmbBij"
                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" OnItemRequestedByValue="cmbBij_ItemRequestedByValue">
                            <ClientSideEvents ButtonClick="function(s, e) {Ndermarje_Click();}" />
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries2" SetFocusOnError="true" ValidateOnLeave="false">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" ErrorText="*" />
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojPorosie" ID="lblLlojPorosie" runat="server"
                            Text="Lloj Porosie" ClientInstanceName="lblLlojPorosie">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="cmbLlojPorosie" runat="server" Width="100%" ClientInstanceName="cmbLlojPorosie"
                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">

                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries2" SetFocusOnError="true" ValidateOnLeave="false">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" ErrorText="*" />
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLimitiParalajmerues" ID="lblLimitParalajmerues"
                            runat="server" Text="Limiti Paralajmerues:" ClientInstanceName="lblLimitParalajmerues">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvtxtLimitiParalajmerues"> --%>
                        <dx:ASPxTextBox ID="txtLimitiParalajmerues" runat="server" ClientInstanceName="txtLimitiParalajmerues"
                            ValidationSettings-CausesValidation="true" Width="100%">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                ValidationGroup="entries1" SetFocusOnError="true">
                                <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <%-- <RequiredField IsRequired="True"></RequiredField>--%>
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <%--</div> --%>
                        <%--<div id="dvlblLimitiBllokues"> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLimitiBllokues" ID="lblLimitiBllokues"
                            runat="server" Text="Limiti Bllokues:" ClientInstanceName="lblLimitiBllokues">
                        </dx:ASPxLabel>
                        <%-- </div>--%>
                        <%--<div id="dvtxtLimitiBllokues"> --%>
                        <dx:ASPxTextBox ID="txtLimitiBllokues" runat="server" ClientInstanceName="txtLimitiBllokues"
                            Width="100%">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries1" SetFocusOnError="true">
                                <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <%--  <RequiredField IsRequired="True"></RequiredField>--%>
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>

                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtQyteti" ID="lblQyteti" runat="server"
                            Text="Qyteti:" ClientInstanceName="lblQyteti">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvtxtQyteti"> --%>
                        <dx:ASPxComboBox ID="txtQyteti" runat="server" ClientInstanceName="txtQyteti" ShowShadow="False"
                            SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	adresat();
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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMetoda" ID="lblMetoda" runat="server"
                            Text="Metoda e Pageses:" ClientInstanceName="lblMetoda">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvcmbMetoda"> --%>
                        <dx:ASPxComboBox ID="cmbMetoda" runat="server" ClientInstanceName="cmbMetoda" ShowShadow="False"
                            SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries" SetFocusOnError="true" ValidateOnLeave="false">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" ErrorText="*" />
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxComboBox>

                        <%-- </div>--%>
                        <%--<div id="dvlblEmertimFature"> --%>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimFature" ID="lblEmertimFature" runat="server"
                            Text="Emertimi ne fature:" ClientInstanceName="lblEmertimFature">
                        </dx:ASPxLabel>
                        <%--</div> --%>
                        <%--<div id="dvtxtEmertimFature"> --%>
                        <dx:ASPxTextBox ID="txtEmertimFature" runat="server" ClientInstanceName="txtEmertimFature" Width="100%">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                ValidateOnLeave="false">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="True"></RequiredField>
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodiISKSH" ID="lblKodiISKSH" runat="server"
                            Text="Kodi ISKSH:" ClientInstanceName="lblKodiISKSH">
                        </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtKodiISKSH" runat="server" ClientInstanceName="txtKodiISKSH" Width="100%">
                            <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="True" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RegularExpression ErrorText="Kodi ISKSH nuk duhet te jete me shume se 100 karaktere" ValidationExpression="^[\s\S]{0,100}$"></RegularExpression>
                                <RequiredField IsRequired="True"></RequiredField>
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbMeDogane" ID="lblMeDogane" runat="server" Text="Me Dogane" ClientInstanceName="lblMeDogane">
                        </dx:ASPxLabel>
                        <dx:ASPxCheckBox ID="cbMeDogane" runat="server" ClientInstanceName="cbMeDogane" Width="100%">
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxCheckBox>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <%-- per te ruajtur vlerat e konfigurimit te lupave si konfig ambjenti--%>
                    <asp:HiddenField ID="hfLupaLlogaria" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogKons" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogDytesor" runat="server" />
                    <asp:HiddenField ID="hfLupaNivelZbritje" runat="server" />
                    <asp:HiddenField ID="hfLupaKushtePagese" runat="server" />
                    <asp:HiddenField ID="hfLupaKushteDergimi" runat="server" />
                    <asp:HiddenField ID="hfLupaAutorizimi" runat="server" />
                    <asp:HiddenField ID="hfLupaAfateMaturimi" runat="server" />
                    <asp:HiddenField ID="hfLupaKategoriZbritje" runat="server" />
                    <asp:HiddenField ID="hfLupaNivelCmimi" runat="server" />
                    <asp:HiddenField ID="hfLupaMenyraTransp" runat="server" />
                    <asp:HiddenField ID="hfLupaAgjShitje" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="hfAdresa" runat="server" />
                    <asp:HiddenField ID="hfKodiPostar" runat="server" />
                    <asp:HiddenField ID="hfArkivaDokId" runat="server" />
                    <asp:HiddenField ID="hfdateHapje" runat="server" />
                    <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
                    </dx:ASPxHiddenField>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hfMeme" runat="server" />
            <dx:ASPxHiddenField ID="hfNrAutoKF" runat="server" ClientInstanceName="hfNrAutoKF">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
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
                    </dx:ASPxPopupControl>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
