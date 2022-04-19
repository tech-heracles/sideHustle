<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_KonfigurimFormatImporti.aspx.cs"
    Inherits="PlatinumWeb.Shto_KonfigurimFormatImporti" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
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
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css"
        runat="server" id="themeJQuery" />
    <link rel="stylesheet" type="text/css" media="screen" href="js/jqGrid445/css/ui.jqgrid.css" />
 
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/JsGlobal.js;~/js/myCookies-IMB.2.1.js;~/js/json2.js;~/js/aspx.js/Shto_KonfigurimFormatImporti.aspx-IMB.2.3.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
         
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <div style="width: 100%; height: 100%">
            <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
             </asp:ScriptManager>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                <%--<ClientSideEvents EndCallback="function(s,e){ try{ window.parent.SessionTimeout.sendKeepAlive(); } catch(e){}}" />--%>
            </dx:ASPxGlobalEvents>
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                <div id="dvMenu" style="display: none">
                                    <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                                BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
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
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="hfStatusRuajtje" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="670px" ClientInstanceName="splitter"
                PaneMinSize="670px">
                <Panes>
                    <%-- Header pane--%>
                    <dx:SplitterPane PaneStyle-BackColor="Transparent" Separators-Size="10px" ScrollBars="Vertical">
                        <Separators Size="10px">
                        </Separators>
                        <PaneStyle>
                        </PaneStyle>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                <asp:Panel ID="ContentPanel" runat="server">
                                    <asp:UpdatePanel ID="pnlLidhur" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table id='hl' runat="server">
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <table id="tblFillim" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <div id="dvFillim" class="atributeDiveFshehur">
                                        <%--<div id="dvkonfigurimi_Label">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                            runat="server" Text="Lloji:" ClientInstanceName="konfigurimi_Label">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvcmbKonfigurimi">--%>
                                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
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
                                        </dx:ASPxComboBox>
                                        <%--</div>--%>
                                        <div id="dvlblKonfigurimi">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi"
                                                            ClientInstanceName="lblKonfigurimi" Text="">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <%--<div id="dvlblKategoria">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKategoria" ID="lblKategoria"
                                            runat="server" Text="Kategoria" ClientInstanceName="lblKategoria">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvcmbKategoria">--%>
                                        <asp:UpdatePanel ID="cmbKategoria_UpdatePanel3ET" runat='server' >
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cmbKategoria" />
                                            </Triggers>
                                            <ContentTemplate>
                                                 <asp:HiddenField ID="hfMag" runat="server" />
                                                <dx:ASPxComboBox ID="cmbKategoria" ClientInstanceName="cmbKategoria" runat="server"
                                                    AutoPostBack="true" ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top"
                                                    OnSelectedIndexChanged="cmbKategoria_SelectedIndexChanged" Width="100%" OnCallback="cmbKategoria_Callback">
                                                    <ClientSideEvents TextChanged="function(s,e){TextChangedKategoria();}" />
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <%--</div>--%>
                                        <%--<div id="dvlblKodi">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                            Text="Kodi:" ClientInstanceName="lblKodi">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvtxtKodi">--%>
                                        <dx:ASPxTextBox ID="txtKodi" runat="server" ClientInstanceName="txtKodi" Width="100%">
                                            <ClientSideEvents Init="function(s, e) {  
	
}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--</div>--%>
                                        <%--<div id="dvlblShenime">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime" ID="lblShenime" runat="server"
                                            Text="Pershkrimi:" ClientInstanceName="lblShenime">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvtxtShenime">--%>
                                        <dx:ASPxMemo ID="txtShenime" runat="server" ClientInstanceName="txtShenime" Width="100%" Rows="3">
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
                                    </div>
                                    <div id="divgride1" style="display: none">
                                        <asp:UpdatePanel ID="pnlbutona" runat='server'>
                                            <ContentTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 20%">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="lblFushat" runat="server" Text="Fushat">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxListBox ID="lbxFushat" runat="server" Width="100%" Height="500px" ClientInstanceName="lbxFushat"
                                                                            SettingsLoadingPanel-ImagePosition="Top" OnCallback="lbxFushat_Callback" OnDataBound="lbxFushat_DataBound">
                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }"
                                                                                EndCallback="function(s, e) {    UpdateButtonState();  }" />
                                                                            <LoadingPanelImage>
                                                                            </LoadingPanelImage>
                                                                            <ValidationSettings>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </dx:ASPxListBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 5%">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnDjathtas1" runat="server" Text="&gt;" OnClick="btnDjathtas1_Click"
                                                                            ClientInstanceName="btnDjathtas1" Width="50px">
                                                                            <ClientSideEvents Click="function (s,e){disablebtn()}" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnDjathtasGjitha" runat="server" Text="&gt;&gt;" ClientInstanceName="btnDjathtasGjitha"
                                                                            Width="50px" OnClick="btnDjathtasGjitha_Click">
                                                                            <ClientSideEvents Click="function (s,e){disablebtn()}" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnMajtas1" runat="server" Text="&lt;" ClientInstanceName="btnMajtas1"
                                                                            Width="50px" OnClick="btnMajtas1_Click">
                                                                            <ClientSideEvents Click="function (s,e){disablebtn()}" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnMajtaGjitha" runat="server" Text="&lt;&lt;" ClientInstanceName="btnMajtaGjitha"
                                                                            Width="50px" OnClick="btnMajtaGjitha_Click">
                                                                            <ClientSideEvents Click="function (s,e){disablebtn()}" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 60%">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="lblZgjedhur" runat="server" Text="Fushat e Zgjedhura">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 500px" valign="top">
                                                                        <dx:ASPxGridView ID="gvZgjedhur" runat="server" ClientInstanceName="gvZgjedhur"
                                                                            Settings-ShowGroupPanel="false" Width="100%" OnCustomCallback="gvZgjedhur_CustomCallback"
                                                                            OnDataBound="gvZgjedhur_DataBound" OnHtmlRowCreated="gvZgjedhur_HtmlRowCreated"
                                                                            OnCustomJSProperties="gvZgjedhur_CustomJSProperties" OnAfterPerformCallback="gvZgjedhur_AfterPerformCallback"
                                                                            Settings-VerticalScrollableHeight="475" Settings-VerticalScrollBarMode="Auto">
                                                                            <ClientSideEvents SelectionChanged="function(s,e){UpdateButtonState();}" EndCallback="function(s,e){}" />
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
                                                        </td>
                                                        <td style="width: 5%">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnSiper" runat="server" Text="Lart" AutoPostBack="false" ClientInstanceName="btnSiper"
                                                                            Width="70px" OnClick="btnSiper_Click">
                                                                            <ClientSideEvents Click="function (s,e){disablebtn()}" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnPoshte" runat="server" Text="Poshte" AutoPostBack="false"
                                                                            ClientInstanceName="btnPoshte" Width="70px" OnClick="btnPoshte_Click">
                                                                            <ClientSideEvents Click="function (s,e){disablebtn()}" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </asp:Panel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
                <Styles>
                </Styles>
                <Images>
                </Images>
            </dx:ASPxSplitter >
            <asp:HiddenField ID="HfKonfAmb" runat="server" />
            <asp:HiddenField ID="hfShtimModifikim" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HFStatusiDokumentit" runat="server" />
            <asp:HiddenField ID="hfKolonaGride" runat="server" />
            <asp:HiddenField ID="HfGridCol" runat="server" /> 
            <asp:HiddenField ID="hfKonffillestar" runat="server" />
            <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
            <asp:HiddenField ID="hfGridaKodi" runat="server" />
            <asp:HiddenField ID="hfGridaDetajimi" runat="server" />
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
        </div>
        <br />
        <br />
        <div>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                <ClientSideEvents Closing="function(s, e) { popupUniversal.SetContentUrl('');}" />
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl >
        </div>
    </form>
</body>
</html>
