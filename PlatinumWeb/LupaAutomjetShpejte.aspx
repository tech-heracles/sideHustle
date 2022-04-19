<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaAutomjetShpejte.aspx.cs" Inherits="PlatinumWeb.LupaAutomjetShpejte" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/LupaAutomjetShpejte.aspx-IMB.4.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
           
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" runat="server">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server">
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
        <div id="dvAutomjet" style="display: none">
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
                        <br />
                        <table id="tblInformacion" class="renditKontrolle">
                            <tbody>
                            </tbody>
                        </table>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTarga" ID="lblTarga" runat="server" ClientInstanceName="lblTarga">
                        </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtTarga" runat="server" Width="100%" ClientInstanceName="txtTarga">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries" SetFocusOnError="True">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbModelAuto" ID="lblModelAuto" runat="server" ClientInstanceName="lblModelAuto">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="cmbModelAuto" runat="server" ClientInstanceName="cmbModelAuto"
                            Width="100%" ShowShadow="False" ValueType="System.Int32" EnableClientSideAPI="True"
                            IncrementalFilteringMode="Contains" EnableSynchronization="True" EnableCallbackMode="True"
                            DropDownRows="3" CallbackPageSize="3" OnItemRequestedByValue="cmbModelAuto_ItemRequestedByValue"
                            OnItemsRequestedByFilterCondition="cmbModelAuto_ItemsRequestedByFilterCondition"
                            SettingsLoadingPanel-ImagePosition="Top">
                            <ClientSideEvents ButtonClick="function(s, e) { ModelAuto_Click(); }" TextChanged="function(s, e) {textChangedModelAuto(s,e);}" />
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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVitProdhimi" ID="lblVitProdhimi" runat="server" ClientInstanceName="lblVitProdhimi">
                        </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtVitProdhimi" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtVitProdhimi">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries" SetFocusOnError="True">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKilometra" ID="lblKilometra" runat="server" ClientInstanceName="lblKilometra">
                        </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtKilometra" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKilometra">
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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodMotorri" ID="lblKodMotorri" runat="server" ClientInstanceName="lblKodMotorri">
                        </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtKodMotorri" runat="server" Width="100%" ClientInstanceName="txtKodMotorri">
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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrShasie" ID="lblNrShasie" runat="server" ClientInstanceName="lblNrShasie">
                        </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtNrShasie" runat="server" Width="100%" ClientInstanceName="txtNrShasie">
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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKlienti" ID="lblKlienti" runat="server" ClientInstanceName="lblKlienti">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="btneKlienti" runat="server" ClientInstanceName="btneKlienti"
                            Width="100%" ShowShadow="False" ValueType="System.Int32" EnableClientSideAPI="True"
                            IncrementalFilteringMode="Contains" EnableSynchronization="True" EnableCallbackMode="True"
                            DropDownRows="3" CallbackPageSize="3" OnItemRequestedByValue="btneKlienti_ItemRequestedByValue"
                            OnItemsRequestedByFilterCondition="btneKlienti_ItemsRequestedByFilterCondition"
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
                          <dx:ASPxLabel Wrap="False" AssociatedControlID="txtMarka" ID="lblMarka" runat="server" ClientInstanceName="lblMarka">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtMarka" runat="server" Width="100%" ClientInstanceName="txtMarka">
                                     <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel >
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <%-- per te ruajtur vlerat e konfigurimit te lupave si konfig ambjenti--%>
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="hfRuaj" runat="server" />
                    <asp:HiddenField ID="hfLupaKlienti" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxHiddenField ID="hfNrAutoKF" runat="server" ClientInstanceName="hfNrAutoKF">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                        CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                        EnableAnimation="False" PopupVerticalAlign="WindowCenter" AllowResize="True"
                        AppearAfter="10" ClientIDMode="AutoID" Height="400px">
                        <ClientSideEvents Closing="function(s, e) {
	popupUniversal.SetContentUrl('');  
}" />
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
        </div>
    </form>
</body>
</html>
