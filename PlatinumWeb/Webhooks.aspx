<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Webhooks.aspx.cs" Inherits="PlatinumWeb.Webhooks" %>
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
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" rel="Stylesheet" type="text/css" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Webhooks.aspx-IMB.6.5.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                            </dx:ASPxPanel>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvWebhooks">

            <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
                ActiveTabIndex="0" TabSpacing="3px" Width="100%" Height="520px">
                <ContentStyle>
                    <border bordercolor="#AECAF0" borderstyle="Solid" borderwidth="1px" />
                </ContentStyle>
                <TabPages>
                    <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl3" runat="server">
                                <table class="renditKontrolle">
                                    <tr>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label" runat="server" Style="font-size: large" Text="Modeli:">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth33">
                                            <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                                                Style="font-size: medium" Height="24px" Width="100%" AnimationType="None">
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
                                            <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth33"></td>
                                    </tr>
                                </table>
                                <dx:ASPxGridView ID="gvWebhooks" runat="server" ClientInstanceName="gvWebhooks"
                                    OnAfterPerformCallback="gvWebhooks_AfterPerformCallback" Width="100%"
                                    OnCustomCallback="gvWebhooks_CustomCallback"
                                    OnDataBound="gvWebhooks_DataBound"
                                    OnProcessColumnAutoFilter="gvWebhooks_ProcessColumnAutoFilter">

                                    <ClientSideEvents RowDblClick="function(s, e) {
            OnGridDoubleClick(e.visibleIndex); kaloTab=true;    	
}"
                                        SelectionChanged="function(s, e){OnGridSelectionChanged(e);}"
                                        FocusedRowChanged="function(s, e) {
            mbush=true;	
}"
                                        BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
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
                    <dxtc:TabPage Name="Webhooks" Text="Webhooks">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                <table id="tblWebhooks" class="renditKontrolle">
                                    <tbody>
                                    </tbody>
                                </table>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server" Text="Kodi:" ClientInstanceName="lblKodi">
                                </dx:ASPxLabel>
                                <dx:ASPxTextBox ID="txtKodi" runat="server" ClientInstanceName="txtKodi" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                        ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                                <dx:ASPxLabel Wrap="False" ID="lblUrlPrites" runat="server" AssociatedControlID="txtUrlPritese"
                                    ClientInstanceName="lblUrlPrites" Text="UrlPritese">
                                </dx:ASPxLabel>
                                <dx:ASPxTextBox ID="txtUrlPritese" runat="server" ClientInstanceName="txtUrlPritese" Width="100%">
                                    <ValidationSettings CausesValidation="true" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                        SetFocusOnError="true" ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>
                                

                               <dx:ASPxLabel Wrap="False" ID="lblAktive" runat="server" AssociatedControlID="cbAktive"
                                    ClientInstanceName="lblAktive" Text="Enable SSL:">
                                </dx:ASPxLabel>

                                <dx:ASPxCheckBox ID="cbAktive" runat="server" ClientInstanceName="cbAktive"
                                    TextSpacing="2px">
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxCheckBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKategoria" ID="lblKategoria"
                                    runat="server" Text="Kategoria:" ClientInstanceName="lblKategoria">
                                </dx:ASPxLabel>
                                <dx:ASPxComboBox ID="cmbKategoria" runat="server" ClientInstanceName="cmbKategoria" ShowShadow="False" ReadOnly="false" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                        ValidationGroup="entries1" ValidateOnLeave="false">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>

                                 <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbEventi" ID="lblEventi"
                                    runat="server" Text="Eventi:" ClientInstanceName="lblEventi">
                                </dx:ASPxLabel>
                                <dx:ASPxComboBox ID="cmbEventi" runat="server" ClientInstanceName="cmbEventi" ShowShadow="False" ReadOnly="false" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                        ValidationGroup="entries1" ValidateOnLeave="false">
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

                </TabPages>
                <ClientSideEvents ActiveTabChanged="activeTabChanged" />

                <%--<Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />--%>
            </dxtc:ASPxPageControl>
        </div>
        <asp:UpdatePanel ID="pnlKryesor" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfStatusi" runat="server" />
                <asp:HiddenField ID="hfKontrollet" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfKonfillestar" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupHelp" runat="server" AllowDragging="True" ClientInstanceName="popupHelp"
                    CloseAction="CloseButton" EnableAnimation="False" HeaderText="Ndihma" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" AllowResize="True" AppearAfter="10" ClientIDMode="AutoID"
                    Height="400px" SettingsLoadingPanel-ImagePosition="Top" Width="400px">
                    <ContentStyle VerticalAlign="Top">
                    </ContentStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl12" runat="server">
                            <dx:ASPxMemo ID="memoNdihma" runat="server" Height="300px" Width="350px" ClientInstanceName="memoNdihma"
                                ReadOnly="True">
                                <ValidationSettings>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </dx:ASPxMemo>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--</div>--%>
    </form>
</body>
</html>
