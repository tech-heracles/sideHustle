<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KonfigPASH.aspx.cs" Inherits="PlatinumWeb.KonfigPASH" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


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
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server"
        id="themeJQuery" />
        <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="screen" href="js/jqGrid445/css/ui.jqgrid.css" />
<%--    <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.10.2.custom.min.js" type="text/javascript"></script>
    <script src="js/myMesazh-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myButtonClickLupa-IMB.2.1.js?versioni22" type="text/javascript"></script>
     
    <script src="js/Utils-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myJQGrid-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/json2.js" type="text/javascript"></script>
    <script src="js/myBuxhet-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myFushaShtese-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script type="text/javascript" src="js/myNrAuto-IMB.2.1.js?versioni22"></script>
    <script src="js/jqGrid445/grid.locale-en.js" type="text/javascript"></script>
    <script src="js/jqGrid445/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="js/jsLinq/JSLINQ.js" type="text/javascript"></script>
    <script src="js/aspx.js/KonfigPASH.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script>--%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/json2.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/jsLinq/JSLINQ.js;~/js/aspx.js/KonfigPASH.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
      
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.    SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                            ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                            OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
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
                                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
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
         <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true">
        </dx:ASPxHiddenField>
    <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" ClientInstanceName="PageControl"
        ActiveTabIndex="1" Width="100%"   TabSpacing="3px">
        <ContentStyle>
            <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
        </ContentStyle>
        <TabPages>
            <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                <ContentCollection>
                    <dxw:ContentControl ID="ContentControl11" runat="server">
                        <dx:ASPxGridView ID="gvKonfigPASH" ClientInstanceName="gvKonfigPASH" runat="server"
                            Width="100%" OnDataBound="gvKonfigPASH_DataBound" OnAfterPerformCallback="gvKonfigPASH_AfterPerformCallback"
                            OnCustomCallback="gvKonfigPASH_CustomCallback" OnHeaderFilterFillItems="gvKonfigPASH_HeaderFilterFillItems"
                            OnCustomJSProperties="gvKonfigPASH_CustomJSProperties">
                            <Styles>
                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                </Header>
                            </Styles>
                            <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true;  }"
                                SelectionChanged="function(s, e){OnGridSelectionChanged(e);}" FocusedRowChanged="function(s, e) {
            mbush=true;	
}" BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                            <StylesEditors>
                                <ProgressBar Height="25px">
                                </ProgressBar>
                            </StylesEditors>
                        </dx:ASPxGridView>
                    </dxw:ContentControl>
                </ContentCollection>
            </dxtc:TabPage>
            <dxtc:TabPage Name="Kartela" Text="Kartela">
                <ContentCollection>
                    <dxw:ContentControl ID="ContentControl1" runat="server">
                        <table class="renditKontrolleNje">
                            <tr>
                                <td class="renditKontrolleCaption">
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="kodi_ASPxTextBox" ID="kodi_ASPxLabel" runat="server" Text="Kodi:">
                                    </dx:ASPxLabel>
                                </td>
                                <td >
                                    <dx:ASPxTextBox ID="kodi_ASPxTextBox" ClientInstanceName="kodi_ASPxTextBox" runat="server"
                                        Width="100%" ValidationSettings-CausesValidation="True" ValidationSettings-SetFocusOnError="True"
                                        ValidationSettings-ValidationGroup="entries">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" SetFocusOnError="True" ValidationGroup="entries">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                               
                            </tr>
                            <tr>
                                <td class="renditKontrolleCaption">
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="emertimi_ASPxTextBox" ID="emertimi_ASPxLabel" runat="server" Text="Emertimi:">
                                    </dx:ASPxLabel>
                                </td>
                                <td rowspan="2" >
                                    <dx:ASPxMemo ID="emertimi_ASPxTextBox" ClientInstanceName="emertimi_ASPxTextBox" Rows="3"
                                        runat="server" Width="100%" ValidationSettings-CausesValidation="True" ValidationSettings-SetFocusOnError="True"
                                        ValidationSettings-ValidationGroup="entries">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" SetFocusOnError="True" ValidationGroup="entries">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxMemo>
                                </td>
                               
                            </tr>
                            <tr></tr>
                            <tr>
                                <td class="renditKontrolleCaption">
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="metoda_ASPxComboBox" ID="ASPxLabel1" runat="server" Text="Metoda:" ClientInstanceName="lblMetoda">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="metoda_ASPxComboBox" ClientInstanceName="metoda_ASPxComboBox"
                                        runat="server" ValidationSettings-Display="Static" ValidationSettings-CausesValidation="True"
                                        ValidationSettings-SetFocusOnError="True" ValidationSettings-ValidationGroup="entries" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" SetFocusOnError="True" ValidationGroup="entries">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                    </dxw:ContentControl>
                </ContentCollection>
            </dxtc:TabPage>
            <dxtc:TabPage Text="Llogarite cash" Name="Llogarite cash" ActiveTabStyle-Width="100%">
                <ContentCollection>
                    <dxw:ContentControl ID="ContentControl2" runat="server">
                        <div id="divgride2">
                            <table id="rowed5" runat="server" style="width: 100%">
                            </table>
                        </div>
                    </dxw:ContentControl>
                </ContentCollection>
            </dxtc:TabPage>
            <dxtc:TabPage Text="Fluks shfrytezimi" Name="Fluks shfrytezimi" ActiveTabStyle-Width="100%">
                <ContentCollection>
                    <dxw:ContentControl ID="ContentControl4" runat="server">
                        <div id="divgride3">
                            <table id="rowed6" runat="server" style="width: 100%">
                            </table>
                        </div>
                    </dxw:ContentControl>
                </ContentCollection>
            </dxtc:TabPage>
            <dxtc:TabPage Text="Fluks investues" Name="Fluks investues" ActiveTabStyle-Width="100%">
                <ContentCollection>
                    <dxw:ContentControl ID="ContentControl5" runat="server">
                        <div id="divgride4">
                            <table id="rowed7" runat="server" style="width: 100%">
                            </table>
                        </div>
                    </dxw:ContentControl>
                </ContentCollection>
            </dxtc:TabPage>
            <dxtc:TabPage Text="Fluks financiar" Name="Fluks financiar" ActiveTabStyle-Width="100%">
                <ContentCollection>
                    <dxw:ContentControl ID="ContentControl6" runat="server">
                        <div id="divgride5">
                            <table id="rowed8" runat="server" style="width: 100%">
                            </table>
                        </div>
                    </dxw:ContentControl>
                </ContentCollection>
            </dxtc:TabPage>
            <dxtc:TabPage  Name="Buxhetet" Text="Buxhetet">
                <ContentCollection>
                    <dxw:ContentControl ID="ContentControl7" runat="server">
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Names="Calibri" Font-Size="Medium"
                            Text="Plotesoni buxhetin per zerin: ">
                        </dx:ASPxLabel>
                        <dx:ASPxLabel ID="zeri_buxheti_ASPxLabel" runat="server" Font-Names="Calibri" ClientInstanceName="zeri_buxheti_ASPxLabel"
                            Font-Size="Medium">
                        </dx:ASPxLabel>
                        <div style="width: 100%; height: 5px">
                        </div>
                        <dx:ASPxGridView ID="grid_buxhetet" runat="server" ClientInstanceName="gvBuxheti"
                            OnHtmlRowCreated="grid_buxhetet_HtmlRowCreated" OnCustomCallback="grid_buxhetet_CustomCallback">
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
        </TabPages>
        <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
        <ClientSideEvents ActiveTabChanged="Active_TabChanged" />
    </dxtc:ASPxPageControl >
    <br />
    <br />
    <asp:UpdatePanel ID="pnlKryesor" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenFieldZerat" runat="server" />
            <asp:HiddenField ID="hfShtimModifikim" runat="server" />
            <asp:HiddenField ID="gridaZerat" runat="server" />
            <asp:HiddenField ID="gridaLlogarite" runat="server" />
            <asp:HiddenField ID="hfBuxheti1" runat="server" />
            <asp:HiddenField ID="hfBuxheti2" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="hfId" runat="server" />
            <asp:HiddenField ID="hfKolonaGride" runat="server" />
            <asp:HiddenField ID="hfKolonaSubGride" runat="server" />
            <asp:HiddenField ID="hfStatusi" runat="server" />
            <asp:HiddenField ID="hfKthehu" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
      <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Zgjidh komponenten prind"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                <ContentStyle>
                    <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                        PaddingTop="1px" />
                </ContentStyle>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <iframe id="Container" name="ContainerPasqyreFinanciare" frameborder="0" runat="server">
                        </iframe>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl >
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
