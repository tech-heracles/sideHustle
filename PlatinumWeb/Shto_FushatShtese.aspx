<%@ Page Language="C#" AutoEventWireup="true" Title="" CodeBehind="Shto_FushatShtese.aspx.cs"
    Inherits="PlatinumWeb.FushatShtese" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
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



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Shto_FushatShtese.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
         <dx:ASPxHiddenField ID="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled">
</dx:ASPxHiddenField>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.    SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
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
                            </dx:ASPxPanel >
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>

        <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server"   TabSpacing="3px"
            ClientInstanceName="PageControl" Width="100%" ActiveTabIndex="0">
            <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
            <ContentStyle>
                <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
            </ContentStyle>
            <TabPages>
                <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                    <ContentCollection>
                        <dxw:ContentControl ID="ContentControl1" runat="server">
                            <dx:ASPxGridView ID="ASPxGridView_Modelet" runat="server" ClientInstanceName="ASPxGridView_Modelet" OnDataBound="ASPxGridView_Modelet_DataBound"
                                OnHeaderFilterFillItems="ASPxGridView_Modelet_HeaderFilterFillItems" OnAfterPerformCallback="ASPxGridView_Modelet_AfterPerformCallback"
                                OnProcessColumnAutoFilter="ASPxGridView_Modelet_ProcessColumnAutoFilter"
                                OnCustomJSProperties="ASPxGridView_Modelet_CustomJSProperties" OnCustomCallback="ASPxGridView_Modelet_CustomCallback"
                                OnAutoFilterCellEditorInitialize="ASPxGridView_Modelet_AutoFilterCellEditorInitialize"
                                Width="100%">
                                <Templates>
                                    <TitlePanel>
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Zgjidh kolonat" AutoPostBack="false"
                                                        ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                        <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s,e,ASPxGridView_Modelet)}"
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
                                <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                    SelectionChanged="function(s, e){OnGridSelectionChanged(e);}" FocusedRowChanged="function(s, e) {
            mbush=true;
}"
                                    BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                                <Styles>
                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                    </Header>
                                </Styles>
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
                <dxtc:TabPage Name="Fushat Shtese" Text="Fushat Shtese">

                    <ContentCollection>
                        <dxw:ContentControl ID="ContentControl3" runat="server">
                            <div style="width: 100%">
                                <table class="renditKontrolle" width="100%">
                                    <tr>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel ID="lblKodi" runat="server" AssociatedControlID="txtKodi" Text="Kodi" ClientInstanceName="lblKodi">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td width="50%" class="renditKontrolleCell">
                                            <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" ClientInstanceName="txtKodi">
                                                <ClientSideEvents TextChanged="function(s, e) {

}" />
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" SetFocusOnError="True" ValidationGroup="entries">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>

                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                </DisabledStyle>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td width="3%"></td>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel ID="lblPershkrimi" runat="server" AssociatedControlID="txtPershkrimi" Text="Pershkrimi" ClientInstanceName="lblPershkrimi">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td width="50%" class="renditKontrolleCell">
                                            <dx:ASPxTextBox ID="txtPershkrimi" runat="server" Width="100%" ClientInstanceName="txtPershkrimi">
                                                <ClientSideEvents TextChanged="function(s, e) {

}" />
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" SetFocusOnError="True" ValidationGroup="entries">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>

                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                </DisabledStyle>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel ID="lblLloji" runat="server" Wrap="False" AssociatedControlID="cmbLloji" Text="Lloji i Modelit" ClientInstanceName="lblLloji">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCell">
                                            <dx:ASPxComboBox ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji" ShowShadow="False" Width="100%"
                                                SettingsLoadingPanel-ImagePosition="Top">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {}" />
                                                <LoadingPanelImage>
                                                </LoadingPanelImage>
                                                <DropDownButton>
                                                    <Image>
                                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    </Image>
                                                </DropDownButton>
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" SetFocusOnError="True" ValidationGroup="entries">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>

                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                </DisabledStyle>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td width="3%"></td>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel ID="lblAutorizimi" runat="server" AssociatedControlID="cmbAutorizimi" Text="Autorizimi:" ClientInstanceName="lblAutorizimi">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCell">
                                            <dx:ASPxComboBox ID="cmbAutorizimi" runat="server" ClientInstanceName="cmbAutorizimi"
                                                OnItemRequestedByValue="cmbAutorizimi_ItemRequestedByValue" SettingsLoadingPanel-ImagePosition="Top"
                                                ShowShadow="False" Width="100%">
                                                <ClientSideEvents ButtonClick="function(s, e) {
 KPF=0;
	Autorizime_Click();
}"
                                                    LostFocus="function(s, e) {
}"
                                                    TextChanged="function(s, e) {
}" 
                                                     SelectedIndexChanged="function(s,e){Utils.SelektimiBosh(s,e);}"/>
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
                                                <DisabledStyle Font-Bold="False">
                                                </DisabledStyle>
                                            </dx:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxGridView ID="grid_fushatShtese" runat="server" ClientInstanceName="grid_fushatShtese"
                                                OnHtmlRowCreated="grid_fushatShtese_HtmlRowCreated" OnCustomCallback="grid_fushatShtese_CustomCallback"
                                                OnCustomJSProperties="grid_fushatShtese_CustomJSProperties" OnAfterPerformCallback="grid_fushatShtese_AfterPerformCallback"
                                                Width="90%" EnableCallbackCompression="True"   >
                                                <ClientSideEvents EndCallback="function(s, e) {
	enable();
}" />
                                                <Styles>
                                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                    </Header>
                                                </Styles>
                                                <StylesEditors>
                                                    <CalendarHeader Spacing="1px">
                                                    </CalendarHeader>
                                                    <ProgressBar Height="25px">
                                                    </ProgressBar>
                                                </StylesEditors>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
            </TabPages>
            <LoadingPanelImage>
            </LoadingPanelImage>
            <ClientSideEvents ActiveTabChanged="Active_TabChanged" />
        </dxtc:ASPxPageControl >
        <asp:UpdatePanel ID="pnlKryesor" runat="server">
            <ContentTemplate>
                <dx:ASPxLabel ID="pergjigja" runat="server">
                </dx:ASPxLabel>
                <asp:HiddenField ID="gridDataObject" runat="server" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="hfPershkrimi" runat="server" />
                <asp:HiddenField ID="hfTipi" runat="server" />
                <asp:HiddenField ID="hfGjatesia" runat="server" />
                <asp:HiddenField ID="hfAutorizime" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" Value="20" />
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
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
        <asp:UpdatePanel ID="update" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
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
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
