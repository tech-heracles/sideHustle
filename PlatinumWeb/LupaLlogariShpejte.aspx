
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaLlogariShpejte.aspx.cs" Inherits="PlatinumWeb.LupaLlogariShpejte" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

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
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/LupaLlogariShpejte.aspx-IMB.4.0.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
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
                                <td class="renditKontrolleLabelMeWidth33"></td>
                    </tr>
                </table>
                 
                <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                    Font-Size="9pt" Modal="True" ImagePosition="Top">
                    <LoadingDivStyle Opacity="30">
                    </LoadingDivStyle>
                </dx:ASPxLoadingPanel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvLlogari" style="display: none">
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
                                        Width="100%" AnimationType="None" >
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
                                <td class="renditKontrolleLabelMeWidth33"></td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <table id="tblInformacion" class="renditKontrolle">
                            <tbody>
                            </tbody>
                        </table>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNr" ID="lblNr" runat="server"
                            Text="Nr:" ClientInstanceName="lblNr">
                        </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtNr" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtNr">
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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerLlogarie1" ID="lblEmer1"
                            runat="server" Text="Emer Llogarie 1:" ClientInstanceName="lblEmer1">
                        </dx:ASPxLabel>
                        <dx:ASPxMemo ID="txtEmerLlogarie1" runat="server" Width="100%" AutoPostBack="false"
                            ClientInstanceName="txtEmerLlogarie1" Rows="3">
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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMonedha" ID="lblMonedha" runat="server"
                            Text="Monedha:" ClientInstanceName="lblMonedha">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="cmbMonedha" runat="server" ClientInstanceName="cmbMonedha"
                            ShowShadow="False" Width="100%" OnItemRequestedByValue="cmbMonedha_ItemRequestedByValue"
                            SettingsLoadingPanel-ImagePosition="Top">
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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGrupi" ID="lblGrupi" runat="server"
                            Text="Grupi:" ClientInstanceName="lblGrupi">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="cmbGrupi" runat="server" AutoPostBack="false" ClientInstanceName="cmbGrupi"
                            EnableCallbackMode="True" Width="100%" ShowShadow="False" ValueType="System.String"
                            OnItemRequestedByValue="cmbGrupi_ItemRequestedByValue" SettingsLoadingPanel-ImagePosition="Top">
                            <ClientSideEvents ButtonClick=" function(s,e) { grida=false; Grupi_Click(); } " />
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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNengrupi" ID="lblNengrupi" runat="server"
                            Text="Nengrupi:" ClientInstanceName="lblNengrupi">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="cmbNengrupi" runat="server" ClientInstanceName="cmbNengrupi"
                            EnableCallbackMode="True" Width="100%" ShowShadow="False" ValueType="System.String"
                            OnItemRequestedByValue="cmbNengrupi_ItemRequestedByValue" SettingsLoadingPanel-ImagePosition="Top">
                            <ClientSideEvents ButtonClick=" function(s,e) { ButtonClickNengrupi(s, e); } " />
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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKpf1" ID="lblKpf1"
                            runat="server" Text="Grupi 1:" ClientInstanceName="lblKpf1">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="cmbKpf1" Enabled="true" runat="server" ClientInstanceName="cmbKpf1"
                            EnableCallbackMode="True" Width="100%" OnItemRequestedByValue="cmbKpf1_ItemRequestedByValue"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                            <DisabledStyle Font-Bold="False" BackColor="#EEEEEE" ForeColor="Black">
                            </DisabledStyle>
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                ValidationGroup="entries" SetFocusOnError="true">
                                <RequiredField IsRequired="true" />
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                            </ValidationSettings>
                            <ClientSideEvents ButtonClick="function(s, e) {grida=false;KPF1_Click(); }" />
                        </dx:ASPxComboBox>



                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime1" ID="lblShenime1" runat="server"
                                        Text="Shenime 1:" ClientInstanceName="lblShenime1">
                         </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtShenime1" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtShenime1">

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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime2" ID="lblShenime2" runat="server"
                                        Text="Shenime 2:" ClientInstanceName="lblShenime2">
                                    </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtShenime2" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtShenime2">
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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime3" ID="lblShenime3" runat="server"
                                        Text="Shenime 3:" ClientInstanceName="lblShenime3">
                                    </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtShenime3" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtShenime3">
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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime4" ID="lblShenime4" runat="server"
                                        Text="Shenime 4:" ClientInstanceName="lblShenime4">
                                    </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtShenime4" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtShenime4">
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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime5" ID="lblShenime5" runat="server"
                                        Text="Shenime 5:" ClientInstanceName="lblShenime5">
                                    </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtShenime5" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtShenime5">
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


						<dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLloji" ID="lblLloji" runat="server"
                                        Text="Lloj qendre:" ClientInstanceName="lblLloji">
                                    </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji" ShowShadow="False"
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
                    <asp:HiddenField ID="hfLupaGrupi" runat="server" />
                    <asp:HiddenField ID="hfLupaNengrupi" runat="server" />
                    <asp:HiddenField ID="hfLupaKpf1" runat="server" />
                    <asp:HiddenField ID="hfRuaj" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
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
