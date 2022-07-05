<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShtoVeprimBanka.aspx.cs" Inherits="PlatinumWeb.ShtoVeprimBanka" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxnb" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Alpha Web</title>
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server" id="themeJQuery" />
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" media="screen" rel="stylesheet" type="text/css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/myMesazh-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/JsGlobal.js;~/toword.js;~/js/mySessionStorage.js;~/js/aspx.js/ShtoVeprimBanka.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
   <style>
    .dergoArke{
        display: none;
        width: 22.5%;
        margin-left: .5%;
    }
    .dergoArke p{
        padding-right: 25%;
    }
   </style>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30"></LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server"></dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled"></dx:ASPxHiddenField>
        <dxcb:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="ASPxCallback1" OnCallback="ASPxCallback1_Callback">
            <ClientSideEvents CallbackComplete="function(s, e) {shuma_TextBox.SetText(e.result);}" />
        </dxcb:ASPxCallback>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound" ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True" OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                <ClientSideEvents ItemClick="function(s, e) { menu_click(s,e); }" Init="function(s) {s.SetClientVisible(true);}" />
                                <ItemImage Height="32px" Width="32px"></ItemImage>
                                <SubMenuItemImage Height="16px" Width="16px"></SubMenuItemImage>
                                <ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" VerticalAlign="Middle">
                                    <Paddings PaddingBottom="1px" PaddingTop="9px" />
                                </ItemStyle>
                                <SubMenuItemStyle Width="32px"></SubMenuItemStyle>
                            </dx:ASPxMenu>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%" BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <SubMenuStyle GutterWidth="17px" />
                                    </dx:ASPxMenu>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popMesazhQK" runat="server" AllowDragging="True" ClientIDMode="AutoID" ClientInstanceName="popMesazhQK" CloseAction="CloseButton" EnableAnimation="False" ShowCloseButton="false" EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="300px">
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl7" runat="server">
                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel17" runat="server" ClientIDMode="AutoID" Width="271px">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent17" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel Wrap="true" ID="lblMsgbox4" runat="server" ClientIDMode="AutoID" Text="Deshironi te beni shperndarjen ne qendrat e kostos?" ClientInstanceName="lblmesazhqendra">
                                        </dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <div style="text-align: right;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonOkQK" runat="server" CausesValidation="False" ClientInstanceName="ButtonOkQK" AutoPostBack="false" Text="Po">
                                                            <ClientSideEvents Click="function(s, e) {popMesazhQK.Hide(); hapPopUp(s, e); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonCancelQK" runat="server" ClientIDMode="AutoID" Text="Jo" AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s, e) {popMesazhQK.Hide(); JopopupClick(s, e); }" />
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
                <dx:ASPxPopupControl ID="popKupon" runat="server" AllowDragging="True" ClientIDMode="AutoID" ClientInstanceName="popKupon" CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="300px">
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl43" runat="server">
                            <dx:ASPxPanel ID="ASPxPanel13" runat="server" ClientIDMode="AutoID" Width="271px">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent13" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel Wrap="true" ID="lblMsgbox3" runat="server" ClientIDMode="AutoID" Text="Fatura ekzistuese eshte printuar ne kase. Deshironi te printoni kupon te ri?">
                                        </dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <div style="text-align: right;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonOk3" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk" AutoPostBack="false" Text="Po">
                                                            <ClientSideEvents Click="function(s, e) {popKupon.Hide(); Utils.shfaqLoadingGif(); btn.DoClick();}" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonJo" runat="server" ClientIDMode="AutoID" Text="Jo">
                                                            <ClientSideEvents Click="function(s, e) {popKupon.Hide();}" />
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
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientInstanceName="popFshi" CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes" Font-Bold="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="300px" ClientIDMode="AutoID" CssPostfix="Glass">
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                                <PanelCollection>
                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblMsgbox" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?"></dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <div style="text-align: right;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk" OnClick="ButtonOk_Click2" Text="Ok">
                                                            <ClientSideEvents Click="function(s, e) {popFshi.Hide(); Utils.shfaqLoadingGif();}" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                            <ClientSideEvents Click="function(s, e) {popFshi.Hide();}" />
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
        <br />
        <div id="divgride2" style="width: 100%; height: 100%">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ClientInstanceName="pergjigja"></dx:ASPxLabel>
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                    <asp:HiddenField ID="hfObjektRuajtur" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hfLimit" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="hfqkmesazhi" runat="server" Value="jo" />
                    <asp:HiddenField ID="hfqkmesazhiVDK" runat="server" Value="jo" />
                    <asp:HiddenField ID="hfUrl" runat="server" />
                    <asp:HiddenField ID="hfUrlVDK" runat="server" />
                    <asp:HiddenField ID="hfNrRef" runat="server" />
                    <asp:HiddenField ID="hfUrl1" runat="server" />
                    <asp:HiddenField ID="hfRuajDraft" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hfTeDrejtaModSkema" runat="server" />
            <asp:HiddenField ID="hfSkema" runat="server" />
            <asp:HiddenField ID="HfGridCol" runat="server" />
            <asp:HiddenField ID="hfShtimModifikim" runat="server" />
            <asp:HiddenField ID="hfKthehu" runat="server" />
            <asp:HiddenField ID="hfLloji" runat="server" />
            <asp:HiddenField ID="hfOpsione" runat="server" />
            <asp:HiddenField ID="hfSubjekti" runat="server" />
            <asp:HiddenField ID="hfEmertimi" runat="server" />
            <asp:HiddenField ID="hfDebiKredi" runat="server" />
            <asp:HiddenField ID="hfPershkrimi" runat="server" />
            <asp:HiddenField ID="hfZbritja" runat="server" />
            <asp:HiddenField ID="hfKreditet" runat="server" />
            <asp:HiddenField ID="hfVlera" runat="server" />
            <asp:HiddenField ID="hfVleraMonBaze" runat="server" />
            <asp:HiddenField ID="hfVleraArketuar" runat="server" />
            <asp:HiddenField ID="hfFatura" runat="server" />
            <asp:HiddenField ID="hfKolonaGride" runat="server" />
            <asp:HiddenField ID="hfMonedhaNder" runat="server" />
            <asp:HiddenField ID="hfIdMonedhaNder" runat="server" />
            <asp:HiddenField ID="hfSkemaKontabel" runat="server" />
            <asp:HiddenField ID="hfKMK" runat="server" />
            <asp:HiddenField ID="hfKursiEkzistues" runat="server" />
            <asp:HiddenField ID="hfNivele" runat="server" />
            <asp:HiddenField ID="hfMonedha" runat="server" />
            <asp:HiddenField ID="hfId" runat="server" />
            <asp:HiddenField ID="gridDataObject" runat="server" />
            <asp:HiddenField ID="hfLupaBanka" runat="server" />
            <asp:HiddenField ID="hfLupaKlientFurnitor" runat="server" />
            <asp:HiddenField ID="hfLupaPunonjes" runat="server" />
            <asp:HiddenField ID="hfLupaLlogarite" runat="server" />
            <asp:HiddenField ID="hfKonffillestar" runat="server" />
            <asp:HiddenField ID="hfLidhur" runat="server" />
            <asp:HiddenField ID="hfAutorizimi" runat="server" />
            <asp:HiddenField ID="hfMonedhatKurse" runat="server" />
            <asp:HiddenField ID="HfColTrupBanka" runat="server" />
            <asp:HiddenField ID="HfColKF" runat="server" />
            <asp:HiddenField ID="hfLupaAutomjet" runat="server" />
            <asp:HiddenField ID="HfColLlog" runat="server" />
            <asp:HiddenField ID="HfColFatShitje" runat="server" />
            <asp:HiddenField ID="HfColFatVeprime" runat="server" />
            <asp:HiddenField ID="hfKontrolletNrAutom" runat="server" />
            <asp:HiddenField ID="hfAtributeNrAutom" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaKFRi" runat="server" />
            <asp:HiddenField runat="server" ID="hfDetyrimiMbetur" />
            <asp:HiddenField ID="hfHapurMbyllur" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaInfoKF" runat="server" />
            <asp:HiddenField ID="hfColPunonjes" runat="server" />
            <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
            <asp:HiddenField ID="hfGridaKodi" runat="server" />
            <dx:ASPxHiddenField ID="hfNrAutoBanka" runat="server" ClientInstanceName="hfNrAutoBanka"></dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto"></dx:ASPxHiddenField>
            <asp:HiddenField ID="hfKontabilizimi" runat="server" />
            <%-- Hidden fields per Arkiven--%>
            <asp:HiddenField ID="hfArkivaDokId" runat="server" />
            <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva"></dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfTeDrejtaGjitheDokPerTuLikujduar" runat="server" ClientInstanceName="hfTeDrejtaGjitheDokPerTuLikujduar"></dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfPeriudhaKontabelBanka" ClientInstanceName="hfPeriudhaKontabelBanka" runat="server"></dx:ASPxHiddenField>
            <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="100%" ClientInstanceName="splitter" PaneMinSize="700px">
                <Panes>
                    <%-- Header pane--%>
                    <dx:SplitterPane PaneStyle-BackColor="Transparent" Separators-Size="10px" ScrollBars="auto">
                        <Separators Size="10px"></Separators>
                        <PaneStyle></PaneStyle>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl1" Height="100%" runat="server">
                                <asp:Panel ID="ContentPanel" runat="server">
                                    <asp:UpdatePanel ID="pnlLidhur" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table id='hl' runat="server"></table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div id="accordition">
                                        
                                        <div>
                                            <h3 id="kokeKonfigurimi"><span id="kokeKonfigurimidiv" class="ui-not-accordion-header-text">Koke Dokumenti:</span></h3>
                                            <div>
                                                <table id="tblKonfigurimi" runat="server"></table>
                                                <table id="tblFillim" class="renditKontrolle">
                                                    <tbody></tbody>

                                                </table>
                                                <div id="DergoArke" class="dergoArke">
                                                    <p id="lblDergoArke">
                                                        Dergo: 
                                                    </p>
                                                <input type="checkbox" ID="Dergo" runat="server"/>
                                                </div>
                                                
                                            </div>
                                        </div>
                                          
                                        <div>
                                           
                                            <h3 id="trupKonfigurimi"><span id="trupKonfigurimidiv" class="ui-not-accordion-header-text">Trup Dokumenti</span></h3>
                                            <div>
                                                <div id="divgride1" style="margin-left: 0px; display: none">
                                                    <div id="divgride3">
                                                        <table id="rowed5"></table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                            <h3 id="fundKonfigurimi"><span id="fundKonfigurimidiv" class="ui-not-accordion-header-text">Fund Dokumenti</span></h3>
                                            <div>
                                                <table id="tblFund" class="renditKontrolle" align="right">
                                                    <tbody></tbody>
                                                </table>
                                                <br />
                                                <br />
                                                <br />
                                               
                                                <div id="dvgrid_faturat">
                                                    <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" Width="100%" ClientSideEvents-HeaderClick="nvFaturaClick" ClientInstanceName="nvFatura">
                                                        <Groups>
                                                            <dx:NavBarGroup Text="Faturat" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px" HeaderStyle-ForeColor="Gray">
                                                                <HeaderStyle Font-Bold="True" Font-Size="14px" ForeColor="Gray"></HeaderStyle>
                                                                <ContentTemplate>
                                                                    <dx:ASPxGridView ID="grid_faturat" runat="server" ClientInstanceName="grid_faturat" OnHtmlDataCellPrepared="grid_faturat_HtmlDataCellPrepared" Width="100%" OnCustomCallback="grid_faturat_CustomCallback" OnDataBound="grid_faturat_DataBound" OnCustomJSProperties="grid_faturat_CustomJSProperties">
                                                                        <ClientSideEvents SelectionChanged="SelectionChanged" RowDblClick="SelectionChanged" EndCallback="EndCallbackGrid_Faturat" />
                                                                        <Styles>
                                                                            <Header ImageSpacing="5px" SortingImageSpacing="5px"></Header>
                                                                        </Styles>
                                                                        <StylesEditors>
                                                                            <CalendarHeader Spacing="1px"></CalendarHeader>
                                                                            <ProgressBar Height="25px"></ProgressBar>
                                                                        </StylesEditors>
                                                                        <Templates>
                                                                            <TitlePanel>
                                                                                <table>
                                                                                    <td>
                                                                                        <dx:ASPxButton ID="gridaSelectFaqe" runat="server" ToolTip="Zgjidh te gjithe faqen" AutoPostBack="false" Image-Url="images/check2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                                                            <ClientSideEvents Click="function(s, e) { grid_faturat.SelectAllRowsOnPage(); }" />
                                                                                        </dx:ASPxButton>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe" AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                                                            <ClientSideEvents Click="function(s, e) { grid_faturat.SelectRows(); }" />
                                                                                        </dx:ASPxButton>
                                                                                    </td>
                                                                                </table>
                                                                            </TitlePanel>
                                                                        </Templates>
                                                                    </dx:ASPxGridView>
                                                                </ContentTemplate>
                                                            </dx:NavBarGroup>
                                                        </Groups>
                                                    </dx:ASPxNavBar>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="dvFillim" class="atributeDiveFshehur">
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="veprimi_ComboBox" ID="veprimi_Label" runat="server" ClientInstanceName="veprimi_Label" Text="Zgjidh llojin e veprimit">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="veprimi_ComboBox" runat="server" ClientInstanceName="veprimi_ComboBox" ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents TextChanged="function (s,e){TextChangedVeprimi()}" />
                                            <LoadingPanelImage></LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="konfigurimi_Label" runat="server" Text="Zgjidhni konfigurimin:" ClientInstanceName="konfigurimi_Label" AssociatedControlID="konfigurimi_ComboBox">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="konfigurimi_ComboBox" runat="server" ClientInstanceName="konfigurimi_ComboBox" ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top" DropDownHeight="100px" AnimationType="None">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin(false)}" />
                                            <LoadingPanelImage></LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi" Text="">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" ID="banka_Label" runat="server" Text="Banka: " ClientInstanceName="banka_Label" AssociatedControlID="banka_ComboBox"></dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="banka_ComboBox" ClientInstanceName="banka_ComboBox" runat="server" EnableClientSideAPI="True" IncrementalFilteringMode="Contains" EnableCallbackMode="True" CallbackPageSize="10" ShowShadow="False" Width="100%" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top" OnItemRequestedByValue="banka_ComboBox_ItemRequestedByValue" OnItemsRequestedByFilterCondition="banka_ComboBox_ItemsRequestedByFilterCondition">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickBanka();}"
                                                TextChanged="function(s,e){ndryshokurs=true;  ndryshodege=true; TextChangedBanka();}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblStatusApr" runat="server" AssociatedControlID="lblStatusAprovimi" ClientInstanceName="lblStatusApr" Text=""></dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" ID="lblStatusAprovimi" runat="server" BackColor="#E2F0FF" ClientInstanceName="lblStatusAprovimi" Font-Bold="true" Text=""></dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" ID="monedha_Label" ClientInstanceName="monedha_Label" runat="server" Text="Monedha" BackColor="AliceBlue" AssociatedControlID="gjendja_Label"></dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" ID="gjendja_Label" runat="server" Text="Gjendja" ClientInstanceName="gjendja_Label" BackColor="AliceBlue">
                                            <ClientSideEvents Init="function(s,e){gjendja_labelFormat();}" />
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" ID="nrDokumenti_Label" runat="server" Text="Nr dokumenti:" ClientInstanceName="nrDokumenti_Label" AssociatedControlID="nrDokumenti_TextBox"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="nrDokumenti_TextBox" runat="server" ClientInstanceName="nrDokumenti_TextBox" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblNrLlogari" runat="server" Text="Nr llogari:" ClientInstanceName="lblNrLlogari" AssociatedControlID="txtNrLlogari"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtNrLlogari" runat="server" ClientInstanceName="txtNrLlogari" TabIndex="4" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblFormatiPrintimit" AssociatedControlID="cmbFormatiPrintimit" runat="server" Text="Formati i printimit" ClientInstanceName="lblFormatiPrintimit">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbFormatiPrintimit" Width="100%" runat="server" ClientInstanceName="cmbFormatiPrintimit" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="nrSerial_Label" runat="server" Text="Nr serial:" ClientInstanceName="nrSerial_Label" AssociatedControlID="nrSerial_TextBox"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="nrSerial_TextBox" runat="server" ClientInstanceName="nrSerial_TextBox" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblShoqeria" runat="server" Text="Emer Mbiemer/Shoqeri:" ClientInstanceName="lblShoqeria" AssociatedControlID="txtShoqeria"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtShoqeria" runat="server" ClientInstanceName="txtShoqeria" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbKasa" ID="lblKasa" runat="server" Text="Printo ne kase:" ClientInstanceName="lblKasa"></dx:ASPxLabel>
                                        <dx:ASPxCheckBox ID="cbKasa" runat="server" ClientInstanceName="cbKasa" Width="100%">
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxCheckBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblCustomerNr" runat="server" Text="Customer Number:" ClientInstanceName="lblCustomerNr" AssociatedControlID="txtCustomerNr"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtCustomerNr" runat="server" ClientInstanceName="txtCustomerNr" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblDegeAdministrative" runat="server" Text="Inventarizimi:" ClientInstanceName="lblDegeAdministrative" AssociatedControlID="cmbDegeAdministrative">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbDegeAdministrative" runat="server" ClientInstanceName="cmbDegeAdministrative" ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top">
                                            <LoadingPanelImage></LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries1" SetFocusOnError="true">
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="referenca_Label" runat="server" Text="Referenca" ClientInstanceName="referenca_Label" AssociatedControlID="referenca_TextBox"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="referenca_TextBox" runat="server" ClientInstanceName="referenca_TextBox" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="data_label" runat="server" Text="Dt dokumenti:" ClientInstanceName="data_label" AssociatedControlID="data_DateEdit"></dx:ASPxLabel>
                                        <dx:ASPxDateEdit ID="data_DateEdit" runat="server" ClientInstanceName="data_DateEdit" ShowShadow="False" Width="100%">
                                            <ClientSideEvents DateChanged="function(s,e){ DateChanged(true, false);  }" LostFocus="function(s,e){ lostFocusData(s, e) }" GotFocus="function(s, e){ dateGotFocus( s, e); }" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
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
                                        <dx:ASPxLabel Wrap="False" ID="pershkrimi_Label" runat="server" Text="Pershkrimi:" ClientInstanceName="pershkrimi_Label" AssociatedControlID="pershkrimi_Memo"></dx:ASPxLabel>
                                        <dx:ASPxMemo ID="pershkrimi_Memo" runat="server" ClientInstanceName="pershkrimi_Memo" Rows="5" Columns="22" Width="100%">
                                            <ClientSideEvents TextChanged="TextChangedPershkrimi" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxMemo>
                                        <dx:ASPxLabel Wrap="False" ID="lblArsye" runat="server" Text="Arsye anullimi:" ClientInstanceName="lblArsye" AssociatedControlID="txtArsye"></dx:ASPxLabel>
                                        <dx:ASPxMemo ID="txtArsye" runat="server" ClientInstanceName="txtArsye" Rows="5" Columns="22" Width="100%" TabIndex="11">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxMemo>
                                        <dx:ASPxLabel Wrap="False" ID="vlera_Label" runat="server" Text="Vlera:" ClientInstanceName="vlera_Label" AssociatedControlID="vlera_TextBox"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="vlera_TextBox" runat="server" ClientInstanceName="vlera_TextBox" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ValidationExpression="[0-9,.-]*" ErrorText="Lejohen vetem numra!" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                            <ClientSideEvents Init="function(s, e){ Utils.initTxtNumber(s,e) }" GotFocus="function(s, e){ gotFocusVlera(s, e); }" LostFocus="function(s, e){ lostFocusVlera(s, e);}" TextChanged="function(s,e){setTimeout(TextChangedVlera,10)}" ValueChanged="TextChangedVlera" />
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="shuma_Label" runat="server" Text="Shuma ne fjale:" ClientInstanceName="shuma_Label" AssociatedControlID="shuma_TextBox"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="shuma_TextBox" runat="server" ClientInstanceName="shuma_TextBox" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="menyrePagese_Label" runat="server" Text="Menyre pagese:" ClientInstanceName="menyrePagese_Label" AssociatedControlID="menyrePagese_ComboBox"></dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="menyrePagese_ComboBox" runat="server" ClientInstanceName="menyrePagese_ComboBox" ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="kursi_Label" runat="server" Text="Kursi:" ClientInstanceName="kursi_Label" AssociatedControlID="kursi_TextBox"></dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="kursi_TextBox" Width="100%" runat="server" ClientInstanceName="kursi_TextBox" ShowShadow="False" EnableClientSideAPI="True" IncrementalFilteringMode="Contains" EnableCallbackMode="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents Init="function(s,e){ Utils.initTxtNumber(s,e); }" GotFocus="function(s,e){ Utils.gotFocusTxtNumer(s,e); }" LostFocus="function(s,e){ndryshoFormatimKursi(s,e); keyUpKursi(); lostFocusKursi(); }" ButtonClick="function(s, e){ ButtonClickKursi(s, e); }" ValueChanged="function(s,e){ValuedChangedKursi(s,e);}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="vleraMonedhaBaze_Label" runat="server" Text="Vlera ne monedhen baze" ClientInstanceName="vleraMonedhaBaze_Label" AssociatedControlID="vleraMonedhaBaze_TextBox">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="vleraMonedhaBaze_TextBox" runat="server" ReadOnly="true" ClientInstanceName="vleraMonedhaBaze_TextBox" Width="100%">
                                            <ClientSideEvents Init="function(s,e){ Utils.initTxtNumber(s,e); }" GotFocus="function(s,e){ gotFocusVleraMonBaze(s, e); }" LostFocus="function(s,e){ lostFocusVleraMonBaze(s, e); }" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="komision_Label" runat="server" Text="Komision bankar:" ClientInstanceName="komision_Label" AssociatedControlID="komision_TextBox"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="komision_TextBox" runat="server" ClientInstanceName="komision_TextBox" ClientSideEvents-KeyUp="function(s,e){Totalet();}" Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" KeyUp="function(s,e){Totalet();}"></ClientSideEvents>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries" CausesValidation="true" ValidateOnLeave="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="furnitori_Label" runat="server" Text="Klienti/Furnitori:" ClientInstanceName="furnitori_Label" AssociatedControlID="furnitori_ComboBox"></dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="furnitori_ComboBox" ClientInstanceName="furnitori_ComboBox" runat="server" Width="100%" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" EnableCallbackMode="True" CallbackPageSize="10" EnableSynchronization="True" OnItemRequestedByValue="furnitori_ComboBox_ItemRequestedByValue" OnItemsRequestedByFilterCondition="furnitori_ComboBox_ItemsRequestedByFilterCondition">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickFurnitori();}" TextChanged="function(s,e){TextChangedFurnitori();}" LostFocus="function(s,e){LostFocusFurnitori();}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>

                                        <dx:ASPxLabel Wrap="False" ID="ASPxLabel2" runat="server" Text="Llogari kredite" ClientInstanceName="ASPxLabel2" AssociatedControlID="kredite_ButtonEdit"></dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="kredite_ButtonEdit" ClientInstanceName="kredite_ButtonEdit" AutoPostBack='false' runat="server" Width="100%" ShowShadow="False" EnableCallbackMode="True" OnItemRequestedByValue="btneLlogInv_ItemRequestedByValue" OnItemsRequestedByFilterCondition="kredite_ButtonEdit_ItemsRequestedByFilterCondition" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickLlogariKredite();}" SelectedIndexChanged="function (s,e){ nrLlogariChange(s,e)}" />
                                            <LoadingPanelImage></LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneAutomjet" ID="lblAutomjet" runat="server" ClientInstanceName="lblAutomjet"></dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="btneAutomjet" runat="server" ClientInstanceName="btneAutomjet" Width="100%" ShowShadow="False" ValueType="System.Int32" EnableClientSideAPI="True" IncrementalFilteringMode="Contains" EnableSynchronization="True" EnableCallbackMode="True" DropDownRows="3" CallbackPageSize="3" OnItemRequestedByValue="btneAutomjet_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneAutomjet_ItemsRequestedByFilterCondition" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s, e) { Auto_Click(); }" TextChanged="function(s, e) {textChangedAuto(s,e);}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="false" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTarga" ID="lblTarga" Text="Targa" runat="server" ClientInstanceName="lblTarga"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtTarga" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtTarga">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbDergoMeEmail" ID="lblDergoMeEmail" runat="server" Text="Dergo me email: " ClientInstanceName="lblDergoMeEmail"></dx:ASPxLabel>
                                        <dx:ASPxCheckBox ID="cbDergoMeEmail" runat="server" ClientInstanceName="cbDergoMeEmail" Checked="false" Width="100%">
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                            <ClientSideEvents CheckedChanged="function(s, e) {}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxCheckBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbPrinto" ID="lblPrinto" runat="server" Text="Printo: " ClientInstanceName="lblPrinto"></dx:ASPxLabel>
                                        <dx:ASPxCheckBox ID="cbPrinto" runat="server" ClientInstanceName="cbPrinto" Width="100%">
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                            <ClientSideEvents CheckedChanged="function(s, e) {}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxCheckBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimKase" AssociatedControlID="cmbKonfigurimKase" runat="server" Text="Konfigurimi kase:" ClientInstanceName="lblKonfigurimKase">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbKonfigurimKase" Width="100%" runat="server" ClientInstanceName="cmbKonfigurimKase" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents Init="cmbKonfigurimKaseInit" SelectedIndexChanged="cmbKonfigurimKaseSelectedChanged" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1" ValidateOnLeave="false">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        
                                          
                                    </div>
                                    <br />
                                    <br />
                                    <div id="dvFund" class="atributeDiveFshehur">
                                        <dx:ASPxLabel AssociatedControlID="data_regj_DateEdit" Wrap="False" ID="data_regj_label" runat="server" Text="Dt regjistrimi:" ClientInstanceName="data_regj_label"></dx:ASPxLabel>
                                        <dx:ASPxDateEdit ID="data_regj_DateEdit" runat="server" ClientInstanceName="data_regj_DateEdit" ShowShadow="False" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <dx:ASPxLabel AssociatedControlID="txtDebi" Wrap="False" ID="lblDebi" runat="server" Text="Vlera:" ClientInstanceName="lblDebi"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtDebi" runat="server" ClientInstanceName="txtDebi" Width="100%" ClientSideEvents-KeyUp="function(s,e){Totalet();}">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" KeyUp="function(s,e){Totalet();}"></ClientSideEvents>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel AssociatedControlID="txtKredi" Wrap="False" ID="lblKredi" runat="server" Text="Vlera:" ClientInstanceName="lblKredi"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtKredi" runat="server" ClientInstanceName="txtKredi" Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel AssociatedControlID="txtDiferenca" Wrap="False" ID="lblDiferenca" runat="server" Text="Vlera:" ClientInstanceName="lblDiferenca"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtDiferenca" runat="server" ClientInstanceName="txtDiferenca" Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ValidationExpression="[-0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel AssociatedControlID="txtMbiPagesa" Wrap="False" ID="lblMbiPagesa" runat="server" Text="Mbi Pagesa:" ClientInstanceName="lblMbiPagesa"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtMbiPagesa" runat="server" ClientInstanceName="txtMbiPagesa" TabIndex="3" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ValidationExpression="[-0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel AssociatedControlID="txtDetyrimi" Wrap="False" ID="lblDetyrimi" runat="server" Text="Detyrimi i mbetur:" ClientInstanceName="lblDetyrimi"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtDetyrimi" runat="server" ClientInstanceName="txtDetyrimi" TabIndex="3" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ValidationExpression="[-0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel AssociatedControlID="txtDetyrimiTerminated" Wrap="False" ID="lblDetyrimiTerminated" runat="server" Text="Detyrimi i mbetur Terminated:" ClientInstanceName="lblDetyrimiTerminated">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtDetyrimiTerminated" runat="server" ClientInstanceName="txtDetyrimiTerminated" TabIndex="3" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ValidationExpression="[-0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel AssociatedControlID="txtTotaliZgjedhur" Wrap="False" ID="lblTotaliZgjedhur" runat="server" Text="Totali i perzgjedhur:" ClientInstanceName="lblTotaliZgjedhur">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtTotaliZgjedhur" runat="server" ClientInstanceName="txtTotaliZgjedhur" TabIndex="3" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ValidationExpression="[-0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel AssociatedControlID="txtTotaliPaguar" Wrap="False" ID="lblTotaliPaguar" runat="server" Text="Totali i paguar:" ClientInstanceName="lblTotaliPaguar"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtTotaliPaguar" runat="server" ClientInstanceName="txtTotaliPaguar" TabIndex="3" Width="100%">
                                            <ClientSideEvents TextChanged="textChangedTotaliPaguar" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ValidationExpression="[-0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtFinancieri" ID="lblFinancieri" runat="server" Text="Financieri:" ClientInstanceName="lblFinancieri"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtFinancieri" runat="server" ClientInstanceName="txtFinancieri" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries1">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtDhenesiMarresi" ID="lblDhenesiMarresi" runat="server" Text="Dhenesi/Marresi:" ClientInstanceName="lblDhenesiMarresi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtDhenesiMarresi" runat="server" ClientInstanceName="txtDhenesiMarresi" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries1">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtArketari" ID="lblArketari" runat="server" Text="Arketari:" ClientInstanceName="lblArketari"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtArketari" runat="server" ClientInstanceName="txtArketari" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries1">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneAutomjeti" ID="lblAutomjeti" runat="server" ClientInstanceName="lblAutomjeti"></dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" ID="lblGrup1" runat="server" Text="Grupim 1:" ClientInstanceName="lblGrup1" AssociatedControlID="cmbGrup1"></dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbGrup1" runat="server" ClientInstanceName="cmbGrup1" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <ClientSideEvents TextChanged="function(s, e) {TextChangedGrupe(s, e)}" />
                                            <LoadingPanelImage></LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblGrup2" runat="server" Text="Grupim 2:" ClientInstanceName="lblGrup2" AssociatedControlID="cmbGrup2"></dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbGrup2" runat="server" ClientInstanceName="cmbGrup2" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <LoadingPanelImage></LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblGrup3" runat="server" Text="Grupim 3:" ClientInstanceName="lblGrup3" AssociatedControlID="cmbGrup3"></dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbGrup3" runat="server" ClientInstanceName="cmbGrup3" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <LoadingPanelImage></LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <br />
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <table width="100%" frame="void" style="visibility: hidden">
                                                    <tr>
                                                        <td style="width: 60%"></td>
                                                        <td align="right" style="width: 10%">
                                                            <dx:ASPxButton ID="ruaj_Button" runat="server" Text="Ruaj" Width="100%" OnClick="ruaj_Button_Click" ValidationGroup="entries">
                                                                <ClientSideEvents Click="function(s, e) {     myFaqeCelje.validim(s, e);	RuajClick(s,e); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td align="right" style="width: 10%">
                                                            <dx:ASPxButton ID="ruaj_draft" runat="server" Text="Ruaj si Draft" Width="100%" ValidationGroup="entries" OnClick="ruaj_draft_Click">
                                                                <ClientSideEvents Click="function(s, e) {     myFaqeCelje.validim(s, e);	RuajClick(s,e);}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td align="right" style="width: 10%">
                                                            <dx:ASPxButton ID="pastro_Button" runat="server" Text="Pastro" Width="100%" ClientInstanceName="pastro_Button">
                                                                <ClientSideEvents Click="function (s,e){PastroClick(); e.processOnServer = false;}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td align="right" style="width: 10%">
                                                            <dx:ASPxButton ID="anullo_Button" runat="server" Text="Lista" Width="100%" OnClick="anullo_Button_Click">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="visibility: hidden">
                                                    <dx:ASPxLabel ID="AspxLabel1" runat="server" Text="Periudha kontabel"></dx:ASPxLabel>
                                                    <dx:ASPxComboBox ID="btnPeriudha" runat="server" ClientInstanceName="btnPeriudha" OnValueChanged="btnPeriudha_TextChanged" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                                        <ClientSideEvents ButtonClick="function(s, e) {ShfaqPeriudhen();}" LostFocus="function(s, e) { lostFocusPeriudha(s.GetText()); valueChangedPeriudha();}" />
                                                        <LoadingPanelImage></LoadingPanelImage>
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
                                                    <dx:ASPxLabel ID="lblPeriudhaAktuale" ClientInstanceName="lblPeriudhaAktuale" runat="server" Text=""></dx:ASPxLabel>
                                                </div>
                                                </div>
                                                <iframe id="Container55" runat="server" frameborder="0" height="0" name="Container55" width="0"></iframe>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </asp:Panel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <%-- Navigation pane --%>
                    <dx:SplitterPane MaxSize="700px" ShowCollapseBackwardButton="True" Separators-Size="10px" PaneStyle-BackColor="Transparent" Collapsed="True" ShowCollapseForwardButton="True" ScrollBars="Auto" AllowResize="True" MinSize="80px" AutoWidth="false" AutoHeight="false" Name="pnlInfo">
                        <Separators Size="10px"></Separators>
                        <PaneStyle BackColor="Transparent"></PaneStyle>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 100%; vertical-align: top;">
                                    <tr>
                                        <td align="center" valign="top">
                                            <asp:UpdatePanel ID="pnl2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="center" style="width: 50%">
                                                                <dx:ASPxButton ID="btnMbyllur" runat="server" Text="-" Width="100%" AutoPostBack="false" Height="25px" Font-Size="9" Font-Bold="true" ToolTip="Mos shfaq info">
                                                                    <ClientSideEvents Click="function (s,e){RuajHapurMbyllurminus(false)}" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td align="center" style="width: 50%">
                                                                <dx:ASPxButton ID="btnHapur" runat="server" Text="+" Width="100%" AutoPostBack="false" Height="25px" Font-Size="9" ToolTip="Shfaq info">
                                                                    <ClientSideEvents Click="function (s,e){RuajHapurMbyllurplus(true)}" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <dxnb:ASPxNavBar ID="ASPxNavBar2" runat="server" ClientInstanceName="navbar" Width="100%" EnableAnimation="True" SyncSelectionMode="CurrentPath" EnableClientSideAPI="True" AllowSelectItem="True" Font-Size="8pt">
                                                        <ClientSideEvents HeaderClick="function (s,e) { HeaderClick (s,e); }" ItemClick="function(s, e) {}" ExpandedChanging="function (s,e) {Expanded();  }" />
                                                        <GroupHeaderTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="width: 50%; font-weight: bold; height: 12px;">
                                                                        <dx:ASPxLabel Wrap="False" ID="Label1" runat="server" Font-Size="8" Text='<%# Eval("Text") %>' />
                                                                    </td>
                                                                    <td style="width: 10%;">
                                                                        <dx:ASPxHyperLink ID="HyperLink2" runat="server" Text='<%# Eval("Name") %>' NavigateUrl="javascript:void(0)" ImageWidth="12px" EnableClientSideAPI="true" ImageHeight="12px" ImageUrl="~/images/new/flash.png" ClientSideEvents-Click="function (s,e){ ButtonClickNavBar(s);}"
                                                                            ClientSideEvents-Init="function (s,e){ KontrolloTeDrejta(s);}" DisabledStyle-BackColor="#CCCCCC" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </GroupHeaderTemplate>
                                                        <Groups>
                                                            <dxnb:NavBarGroup Text="Info Klient/Furnitori" Expanded="false" Name="KF">
                                                                <ContentTemplate>
                                                                    <dx:ASPxListBox ID="lbxKF" Height="100%" runat="server" Width="100%" ClientInstanceName="lbxKF" Font-Size="8">
                                                                        <Columns>
                                                                            <dx:ListBoxColumn FieldName="Emri" Name="Emri" />
                                                                            <dx:ListBoxColumn FieldName="Vlera" Name="Vlera" />
                                                                        </Columns>
                                                                    </dx:ASPxListBox>
                                                                </ContentTemplate>
                                                            </dxnb:NavBarGroup>
                                                        </Groups>
                                                    </dxnb:ASPxNavBar>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
                <ClientSideEvents PaneCollapsed="function(s, e) { spliterPaneCollapsed(s,e);}" PaneExpanded="function(s, e) { spliterPaneCollapsed(s,e);}" PaneCollapsing="function(s, e) { spliterPaneCollapsing(s,e);}" PaneExpanding="function(s, e) { spliterPaneExpanding(s,e);}" PaneResized="function(s, e) { spliterPaneResized(s,e);}" />
                <Styles></Styles>
                <Images></Images>
            </dx:ASPxSplitter>
        </div>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta"></dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri"></dx:ASPxHiddenField>
        <div>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" AllowResize="True" AppearAfter="10" ClientIDMode="AutoID" ClientInstanceName="popupUniversal" CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                <ClientSideEvents CloseUp="function(s, e) {  closePopup(s,e); }" />
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server"></dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </div>
        <div>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popUpNrAutomatikDrejtFundit" runat="server" AllowDragging="True" AllowResize="True" AppearAfter="10" ClientIDMode="AutoID" ClientInstanceName="popUpNrAutomatikDrejtFundit" CloseAction="CloseButton" EnableAnimation="False" HeaderText="Kujdes! Numrat automatik drejt fundit..." Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <dx:ASPxLabel runat="server" ClientInstanceName="lblNrAutoDrejtFundit" Text="Kujdes, po perfundojne numrat seriale/dokumentit!" ForeColor="#595959" ClientIDMode="AutoID" ID="lblNrAutoDrejtFundit">
                        </dx:ASPxLabel>
                        <br />
                        <br />
                        <dx:ASPxButton ID="bntOKNrAuto" runat="server" CausesValidation="False" ClientInstanceName="bntOKNrAuto" Text="Ok">
                            <ClientSideEvents Click="function(s, e) {		popUpNrAutomatikDrejtFundit.Hide(); }" />
                        </dx:ASPxButton>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
