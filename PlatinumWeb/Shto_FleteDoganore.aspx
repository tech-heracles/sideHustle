<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_FleteDoganore.aspx.cs"
    Inherits="PlatinumWeb.Shto_FleteDoganore" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>



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
    <link href="js/jqGrid445/css/ui.jqgrid.css" media="screen" rel="stylesheet" type="text/css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/myMesazh-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/json2.js;~/JsGlobal.js;~/js/myCookies-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/Shto_FleteDoganore.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; height: 100%">
              
            <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                Font-Size="9pt" Modal="True" ImagePosition="Top">
                <LoadingDivStyle Opacity="30">
                </LoadingDivStyle>
            </dx:ASPxLoadingPanel>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
               </asp:ScriptManager>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                <%--<ClientSideEvents EndCallback="function(s,e){ try{ window.parent.SessionTimeout.sendKeepAlive(); } catch(e){  }}" />--%>
            </dx:ASPxGlobalEvents>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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
                                                        <ClientSideEvents Init="function(s,e){   myMesazh.InicializoTimer();
                                                    }" />
                                                        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <SubMenuStyle GutterWidth="17px" />
                                                    </dx:ASPxMenu>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
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
                                            <dxp:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                                                <PanelCollection>
                                                    <dxp:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxLabel ID="lblMsgbox" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?">
                                                        </dx:ASPxLabel>
                                                        <br />
                                                        <div style="text-align: right;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                                            OnClick="ButtonOk_Click2" Text="Ok">
                                                                            <ClientSideEvents Click="function(s, e) {
	Utils.shfaqLoadingGif();;
    popFshi.Hide();
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
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxp:ASPxPanel >
                                        </dx:PopupControlContentControl>
                                    </ContentCollection>
                                </dx:ASPxPopupControl >
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popMesazhQK" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                            ClientInstanceName="popMesazhQK" CloseAction="None" EnableAnimation="False" EnableViewState="False"
                            Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                            PopupVerticalAlign="WindowCenter" Width="300px" ShowCloseButton="False">
                            <HeaderStyle>
                                <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                            </HeaderStyle>
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl7" runat="server">
                                    <dxp:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel17" runat="server" ClientIDMode="AutoID" Width="271px">
                                        <PanelCollection>
                                            <dxp:PanelContent ID="PanelContent17" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel Wrap="true" ID="lblMsgbox4" runat="server" ClientIDMode="AutoID" Text="Deshironi te beni shperndarjen ne qendrat e kostos?"
                                                    ClientInstanceName="lblmesazhqendra">
                                                </dx:ASPxLabel>
                                                <br />
                                                <br />
                                                <div style="text-align: right;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton ID="ButtonOkQK" runat="server" CausesValidation="False" ClientInstanceName="ButtonOkQK"
                                                                    AutoPostBack="false" Text="Po">
                                                                    <ClientSideEvents Click="function(s, e) {
	popMesazhQK.Hide();
  hapPopUp(s,e);
}" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="ButtonCancelQK" runat="server" ClientIDMode="AutoID" Text="Jo"
                                                                    AutoPostBack="false">
                                                                    <ClientSideEvents Click="function(s, e) {
		popMesazhQK.Hide();
        JopopupClick(s,e);
}" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel >
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl >
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Red" ClientInstanceName="pergjigja">
                    </dx:ASPxLabel>
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                    <asp:HiddenField ID="HfGridCol" runat="server" />
                    <asp:HiddenField ID="hfqkmesazhi" runat="server" Value="jo" />
                    <asp:HiddenField ID="hfUrl" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfArtikulli" runat="server" />
                    <asp:HiddenField ID="hfLlog" runat="server" />
                    <asp:HiddenField ID="hfMagazina" runat="server" />
                    <asp:HiddenField ID="hfFurnitori" runat="server" />
                    <asp:HiddenField ID="hfNrFature" runat="server" />
                    <asp:HiddenField ID="hfIdFaturave" runat="server" />
                    <asp:HiddenField ID="hfDtFature" runat="server" />
                    <asp:HiddenField ID="hfNjesia" runat="server" />
                    <asp:HiddenField ID="hfSasia" runat="server" />
                    <asp:HiddenField ID="hfCmimi" runat="server" />
                    <%-- <asp:HiddenField ID="hfMonedha" runat="server" />
        <asp:HiddenField ID="hfKursi" runat="server" /> --%>
                    <asp:HiddenField ID="hfVlMb" runat="server" />
                    <asp:HiddenField ID="hfTransport" runat="server" />
                    <asp:HiddenField ID="hfSiguracion" runat="server" />
                    <asp:HiddenField ID="hfTjera" runat="server" />
                    <asp:HiddenField ID="hfVlDogane" runat="server" />
                    <asp:HiddenField ID="hfTaksa" runat="server" />
                    <asp:HiddenField ID="hfVleraSub" runat="server" />
                    <asp:HiddenField ID="hfKolonaGride" runat="server" />
                    <asp:HiddenField ID="hfKolonaSubGride" runat="server" />
                    <asp:HiddenField ID="hfStatusDok" runat="server" />
                    <asp:HiddenField ID="hfFaturaBlerje" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfAutorizimi" runat="server" />
                    <asp:HiddenField ID="hfKontabilizimi" runat="server" />
                    <asp:HiddenField ID="hfKontrolletNrAutom" runat="server" />
                    <asp:HiddenField ID="hfAtributeNrAutom" runat="server" />
                    <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
                    <asp:HiddenField ID="hfGridaKodiKonfig" runat="server" />
                    <%-- per te ruajtur id e rreshtave te selektuar te grides se faturave--%>
                    <asp:HiddenField ID="hfReshtaTeSelektuar" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                    <dx:ASPxHiddenField ID="hfNrAutoShitje" runat="server" ClientInstanceName="hfNrAutoShitje">
                    </dx:ASPxHiddenField>
                    <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
                    </dx:ASPxHiddenField>
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="730px" ClientInstanceName="splitter"
                PaneMinSize="700px">
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

                                    <div id="accordition">
                                        <div>
                                            <h3 id="kokeKonfigurimi"><span id="kokeKonfigurimidiv" class="ui-not-accordion-header-text">Koke Dokumenti:</span></h3>
                                            <div>
                                                <table id="tblKonfigurimi" runat="server">
                                                </table>
                                                <table id="tblFillim" class="renditKontrolle">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                                <br />
                                                <div id="dvgvFaturat" class="atributeDiveFshehur">
                                                    <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" ClientIDMode="AutoID" Width="100%"
                                                        ClientInstanceName="nvFatura">
                                                        <Groups>
                                                            <dx:NavBarGroup Text="Faturat" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px"
                                                                HeaderStyle-ForeColor="Gray" Expanded="False">
                                                                <HeaderStyle Font-Bold="True" Font-Size="14px" ForeColor="Gray"></HeaderStyle>
                                                                <ContentTemplate>
                                                                    <dx:ASPxGridView ID="grid_faturat" runat="server" ClientInstanceName="grid_faturat"
                                                                        Settings-ShowGroupPanel="false" Width="100%" OnCustomCallback="grid_faturat_CustomCallback"
                                                                        OnDataBound="grid_faturat_DataBound" OnProcessColumnAutoFilter="grid_faturat_ProcessColumnAutoFilter"
                                                                        OnHtmlRowCreated="grid_faturat_HtmlRowCreated" OnCustomJSProperties="grid_faturat_CustomJSProperties"
                                                                        OnAfterPerformCallback="grid_faturat_AfterPerformCallback">
                                                                        <ClientSideEvents SelectionChanged="function(s,e){SelectionChangedGridFaturat(e.visibleIndex);}" />
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
                                                                </ContentTemplate>
                                                            </dx:NavBarGroup>
                                                        </Groups>
                                                    </dx:ASPxNavBar>
                                                </div>
                                                <br />
                                                <table id="tblMes1" width="100%">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                                <br />
                                                <div id="dvgvTaksat" class="atributeDiveFshehur">
                                                    <table width="100%">
                                                        <tr>
                                                            <th>
                                                                <dx:ASPxLabel Wrap="False" ID="lblTaksat" runat="server" Text="Taksat" ClientInstanceName="lblTaksat"
                                                                    Font-Bold="True">
                                                                </dx:ASPxLabel>
                                                            </th>
                                                            <th>
                                                                <dx:ASPxLabel Wrap="False" ID="lblTVSH" runat="server" Text="TVSH" ClientInstanceName="lblTVSH"
                                                                    Font-Bold="True">
                                                                </dx:ASPxLabel>
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="50%" class="renditKontrolleCell" align="center">
                                                                <asp:UpdatePanel ID="pnlGridTaksat" runat="server">
                                                                    <ContentTemplate>
                                                                        <dx:ASPxGridView ID="gvTaksat" runat="server" ClientInstanceName="gvTaksat" OnHtmlRowCreated="gvTaksa_HtmlRowCreated"
                                                                            OnCustomCallback="gvTaksat_CustomCallback" OnCustomJSProperties="gvTaksat_CustomJSProperties"
                                                                            Width="100%" OnDataBound="gvTaksat_DataBound">
                                                                            <ClientSideEvents BeginCallback="function(s, e) {	
                                                                        }"
                                                                                EndCallback="function (s,e){      llogaritVlera();}" />
                                                                            <Styles>
                                                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                                                </Header>
                                                                            </Styles>
                                                                            <StylesEditors>
                                                                                <ProgressBar Height="25px">
                                                                                </ProgressBar>
                                                                            </StylesEditors>
                                                                        </dx:ASPxGridView>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td width="50%" class="renditKontrolleCell" align="center">
                                                                <asp:UpdatePanel ID="pnlGridTVSH" runat="server">
                                                                    <ContentTemplate>
                                                                        <dx:ASPxGridView ID="gvTVSH" runat="server" ClientInstanceName="gvTVSH" OnHtmlRowCreated="gvTVSH_HtmlRowCreated"
                                                                            OnCustomCallback="gvTVSH_CustomCallback" OnCustomJSProperties="gvTVSH_CustomJSProperties"
                                                                            Width="100%" OnDataBound="gvTVSH_DataBound">
                                                                            <ClientSideEvents BeginCallback="function(s, e) {	
                                                                        }"
                                                                                EndCallback="function (s,e){ llogaritVlera() ; }" />
                                                                            <Styles>
                                                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                                                </Header>
                                                                            </Styles>
                                                                            <StylesEditors>
                                                                                <ProgressBar Height="25px">
                                                                                </ProgressBar>
                                                                            </StylesEditors>
                                                                        </dx:ASPxGridView>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <br />
                                                <table id="tblMes2" align="left" class="CustomRenditKontrolleDy">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                                <br />
                                                <table width="100%">
                                                    <tr align="left">
                                                        <td>
                                                            <div id="dvbtnFleteDoganore" style="display: none; padding-left: 7px;">
                                                                <dx:ASPxButton ID="btnFleteDoganore" ClientInstanceName="btnFleteDoganore" runat="server"
                                                                    Text="Flete Doganore" ClientIDMode="AutoID" AutoPostBack="false" Width="180px"
                                                                    CausesValidation="False">
                                                                    <ClientSideEvents Click="function(s,e){ButtonClickFleteDoganore(); }" />
                                                                </dx:ASPxButton>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div>
                                            <h3 id="trupKonfigurimi"><span id="trupKonfigurimidiv" class="ui-not-accordion-header-text">Trup Dokumenti</span></h3>
                                            <div>
                                                <div id="divgride1" style="display: none">
                                                    <div id="divgride2">
                                                        <table id="rowed5">
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                            <h3 id="fundKonfigurimi"><span id="fundKonfigurimidiv" class="ui-not-accordion-header-text">Fund Dokumenti</span></h3>
                                            <div>
                                                <table id="tblFund" class="renditKontrolle" align="right">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="dvFillim" class="atributeDiveFshehur">
                                        <%-- Rreshti 1 i kontrolleve per tablen Fillim --%>
                                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi"
                                            ClientInstanceName="lblKonfigurimi" Text="Fleta Doganore">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimiLabel"
                                            runat="server" Text="Lloj" ClientInstanceName="konfigurimiLabel">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrDok" ID="lblNrDokumenti" runat="server"
                                            Text="Nr.Dokumenti" ClientInstanceName="lblNrDokumenti">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtDtDok" ID="lblDtDokumenti" runat="server"
                                            Text="Dt.Dokumenti" ClientInstanceName="lblDtDokumenti">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                            ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top" AnimationType="None">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxTextBox ID="txtNrDok" runat="server" ClientInstanceName="txtNrDok" Width="100%">
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
                                        <dx:ASPxDateEdit ID="txtDtDok" runat="server" ClientInstanceName="txtDtDok" ShowShadow="False"
                                            Width="100%">
                                            <ClientSideEvents DateChanged="function (s,e) {dateChanged()}" GotFocus="function(s, e){ dateGotFocus( s, e); }" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                    </div>

                                    <div id="dvMes1" class="atributeDiveFshehur">
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMonedha" ID="lblMonedha" runat="server"
                                            Text="Monedha" ClientInstanceName="lblMonedha">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKursi" ID="lblKursi" runat="server"
                                            Text="Kursi" ClientInstanceName="lblKursi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVlFatura" ID="lblVleraFaturuar"
                                            runat="server" Text="Vlera e faturuar" ClientInstanceName="lblVleraFaturuar">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVlMb" ID="lblVleraMB" runat="server"
                                            Text="Vlera ne MB" ClientInstanceName="lblVleraMB">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbMonedha" ClientInstanceName="cmbMonedha" runat="server"
                                            SettingsLoadingPanel-ImagePosition="Top" Width="100%" ShowShadow="False">
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
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
                                        <dx:ASPxComboBox ID="txtKursi" Width="100%" runat="server" ClientInstanceName="txtKursi"
                                            ShowShadow="False" EnableClientSideAPI="True" IncrementalFilteringMode="Contains"
                                            EnableCallbackMode="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                                                LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e); LostFocusKursiKokaFleteDoganore(s,e); }"
                                                ButtonClick="function(s, e){ ButtonClickKursi(s, e); }" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
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
                                        <dx:ASPxTextBox ID="txtVlFatura" ClientInstanceName="txtVlFatura" runat="server"
                                            Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                                                LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);LostFocusKursiKokaFleteDoganore(s,e);}" />
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
                                        <dx:ASPxTextBox ID="txtVlMb" runat="server" ClientInstanceName="txtVlMb" Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e); LostFocusPerLlogaritjeTeVlDoganore(s,e);}" />

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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTransporti" ID="lblTransport"
                                            runat="server" Text="Transport" ClientInstanceName="lblTransport">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtSiguracion" ID="lblSiguracion"
                                            runat="server" Text="Siguracion" ClientInstanceName="lblSiguracion">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTjera" ID="lblTeTjera" runat="server"
                                            Text="Te tjera" ClientInstanceName="lblTeTjera">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVlDoganore" ID="lblVleraDoganimi"
                                            runat="server" Text="Vlera doganim" ClientInstanceName="lblVleraDoganimi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtTransporti" ClientInstanceName="txtTransporti" runat="server"
                                            Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);LostFocusPerLlogaritjeTeVlDoganore(s,e);} " />
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
                                        <dx:ASPxTextBox ID="txtSiguracion" ClientInstanceName="txtSiguracion" runat="server"
                                            Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e); LostFocusPerLlogaritjeTeVlDoganore(s,e);} " />
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
                                        <dx:ASPxTextBox ID="txtTjera" ClientInstanceName="txtTjera" runat="server" Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);LostFocusPerLlogaritjeTeVlDoganore(s,e);  }" />

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
                                        <dx:ASPxTextBox ID="txtVlDoganore" ClientInstanceName="txtVlDoganore" runat="server"
                                            Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
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
                                    </div>

                                    <div id="dvMes2" class="atributeDiveFshehur">
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTotalTaksa" ID="lblTotaliTaksa"
                                            runat="server" Text="Totali i taksave" ClientInstanceName="lblTotaliTaksa">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtTotalTaksa" ClientInstanceName="txtTotalTaksa" runat="server"
                                            Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e); LostFocusPerLlogaritjeTeVlDoganore(s,e); }" />

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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTotalTVSH" ID="lblTotalTVSH"
                                            runat="server" Text="Totali i TVSH" ClientInstanceName="lblTotalTVSH">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtTotalTVSH" ClientInstanceName="txtTotalTVSH" runat="server"
                                            Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);LostFocusPerLlogaritjeTeVlDoganore(s,e);}" />
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
                                    </div>

                                    <div id="dvFund" class="atributeDiveFshehur">
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtDtRegj" ID="lblDtRegjistrimi"
                                            runat="server" Text="Dt.Regjistrimi" ClientInstanceName="lblDtRegjistrimi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxDateEdit ID="txtDtRegj" runat="server" ClientInstanceName="txtDtRegj" ShowShadow="False"
                                            Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTransportTotal" ID="lblTotalTransporti"
                                            runat="server" Text="Totali transport" ClientInstanceName="lblTotalTransporti">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtTransportTotal" ClientInstanceName="txtTransportTotal" runat="server"
                                            Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtSiguracionTotal" ID="lblTotalSiguracioni"
                                            runat="server" Text="Totali Siguracion" ClientInstanceName="lblTotalSiguracioni">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtSiguracionTotal" ClientInstanceName="txtSiguracionTotal"
                                            runat="server" Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTjeraTotal" ID="lblTotalTjera"
                                            runat="server" Text="Totali Te tjera" ClientInstanceName="lblTotalTjera">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtTjeraTotal" ClientInstanceName="txtTjeraTotal" runat="server"
                                            Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtDoganTotal" ID="lblTotalVleraDoganore"
                                            runat="server" Text="Totali Vlera doganore" ClientInstanceName="lblTotalVleraDoganore">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtDoganTotal" ClientInstanceName="txtDoganTotal" runat="server"
                                            Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTaksaTotal" ID="lblTotalTaksa"
                                            runat="server" Text="Totali Taksa" ClientInstanceName="lblTotalTaksa">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtTaksaTotal" ClientInstanceName="txtTaksaTotal" runat="server"
                                            Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
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
            <asp:HiddenField ID="hfVleraFaturuar" runat="server" />
            <asp:HiddenField ID="hfKursi" runat="server" />
            <asp:HiddenField ID="hfIdMonedha" runat="server" />
            <asp:HiddenField ID="hfKodMonedha" runat="server" />
            <asp:HiddenField ID="hfLupaFatura" runat="server" />
            <asp:HiddenField ID="hfNiveli" runat="server" />
            <asp:HiddenField ID="hfKodi" runat="server" />
            <asp:HiddenField ID="hfPershkrimi" runat="server" />
            <asp:HiddenField ID="hfVlera" runat="server" />
            <asp:HiddenField ID="hfTvsh" runat="server" />
            <asp:HiddenField ID="hfNorma" runat="server" />
            <asp:HiddenField ID="hfKodiTVSH" runat="server" />
            <asp:HiddenField ID="hfPershkrimiTVSH" runat="server" />
            <asp:HiddenField ID="hfVleraFaturuarTVSH" runat="server" />
            <asp:HiddenField ID="hfVleftaTvsh" runat="server" />
            <asp:HiddenField ID="hfNormaTVSH" runat="server" />
            <asp:HiddenField ID="hfAQT" runat="server" />
            <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
            <div>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                    <ClientSideEvents CloseUp="function(s, e) {
                   
	closePopup(s,e);
}" />
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </div>
            <table style="visibility: hidden">
                <tr>
                    <td>
                        <dx:ASPxLabel ID="AspxLabel1" runat="server" Text="Periudha
    kontabel">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="btnPeriudha" runat="server" ClientInstanceName="btnPeriudha"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                            <ClientSideEvents ButtonClick="function(s,
    e) {ShfaqPeriudhen();}"
                                LostFocus="function(s, e) {
    valueChangedPeriudha();}" />
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
                        <dx:ASPxLabel ID="lblPeriudhaAktuale" ClientInstanceName="lblPeriudhaAktuale" runat="server"
                            Text="">
                        </dx:ASPxLabel>
                    </td>
                </tr>
            </table>
        </div>

        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>

    </form>
</body>
</html>
