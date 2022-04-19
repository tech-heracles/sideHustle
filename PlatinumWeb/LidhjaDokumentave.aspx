<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LidhjaDokumentave.aspx.cs"
    Inherits="PlatinumWeb.LidhjaDokumentave" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicdKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>




<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <%--   <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/myMesazh-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myButtonClickLupa-IMB.2.1.js?versioni22" type="text/javascript"></script>
     
    <script src="js/Utils-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myJQGrid-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script type="text/javascript" src="js/myCookies-IMB.2.1.js?versioni22"></script>
    <script src="JsGlobal.js" type="text/javascript"></script>
    <script src="js/json2.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/myNrAuto-IMB.2.1.js?versioni22"></script>
    <script src="js/aspx.js/LidhjaDokumentave.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script>--%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/LidhjaDokumentave.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">
         
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
          
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ try{ window.parent.SessionTimeout.sendKeepAlive(); } catch(e){}}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
        </dx:ASPxHiddenField>
        <div style="width: 100%; height: 100%">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" AutoPostBack="false" ClientInstanceName="ASPxMenu1"
                                    ItemImagePosition="Top" OnItemClick="ASPxMenu1_ItemClick" OnDataBound="ASPxMenu1_DataBound"
                                    SeparatorWidth="1px" ShowPopOutImages="True" Width="100%">
                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                    <%-- --%>
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
                                        <dx:ASPxMenu ID="MenuInfo" runat="server" BorderBetweenItemAndSubMenu="HideRootOnly"
                                            ClientIDMode="AutoID" ClientInstanceName="MenuInfo" ShowPopOutImages="True" Width="100%">
                                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <SubMenuStyle GutterWidth="17px" />
                                        </dx:ASPxMenu>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                        ClientInstanceName="popFshi" CloseAction="CloseButton" CssPostfix="Glass" EnableAnimation="False"
                        EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" Width="300px">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                                <dxp:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lblMsgbox" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                                Height="23px" OnClick="ButtonOk_Click2" Text="Ok">
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
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxp:ASPxPanel >
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl >
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popMesazhQK" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                        ClientInstanceName="popMesazhQK" CloseAction="None" EnableAnimation="False"
                        EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" Width="300px" ShowCloseButton="False">
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
                                                            <dx:ASPxButton ID="ButtonOkQK" runat="server" CausesValidation="False" ClientInstanceName="ButtonOkQK" AutoPostBack="false"
                                                                Text="Po">
                                                                <ClientSideEvents Click="function(s, e) {
	popMesazhQK.Hide();
  hapPopUp(s,e);
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancelQK" runat="server" ClientIDMode="AutoID" Text="Jo" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s, e) {
		popMesazhQK.Hide();
        JopopupClick(s,e);
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
                                    <table id="tblFillim" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <div id="dvFillim" class="atributeDiveFshehur">
                                        <%--<div id="dvkonfigurimi_Label">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="konfigurimi_ComboBox" ID="konfigurimi_Label"
                                            runat="server" ClientInstanceName="konfigurimi_Label" Text="Zgjidhni konfigurimin:">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvkonfigurimi_ComboBox" style="visibility: hidden;">--%>
                                        <dx:ASPxComboBox ID="konfigurimi_ComboBox" runat="server" ClientInstanceName="konfigurimi_ComboBox"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidateOnLeave="false" ValidationGroup="entries1">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%-- </div>--%>
                                        <div id="dvlblKonfigurimi">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi"
                                                            ClientInstanceName="lblKonfigurimi"  Text="">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <dx:ASPxHiddenField ID="hfNrAutoShitje" runat="server" ClientInstanceName="hfNrAutoShitje">
                                        </dx:ASPxHiddenField>
                                        <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
                                        </dx:ASPxHiddenField>
                                        <%--<div id="dvlblNr">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="nrLidhje_TextBox" ID="lblNr" runat="server"
                                            Text="Numer lidhje: " ClientInstanceName="lblNr">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvnrLidhje_TextBox">--%>
                                        <dx:ASPxTextBox ID="nrLidhje_TextBox" runat="server" Width="100%" ClientInstanceName="nrLidhje_TextBox">
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
                                        <%--<div id="dvlblData">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="dtDokumenti_DateEdit" ID="lblData"
                                            runat="server" Text="Date dokumenti: " ClientInstanceName="lblData">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvdtDokumenti_DateEdit">--%>
                                        <dx:ASPxDateEdit ID="dtDokumenti_DateEdit" runat="server" ClientInstanceName="dtDokumenti_DateEdit"
                                            Width="100%" ShowShadow="False">
                                            <ClientSideEvents DateChanged="function(s,e){DateChanged(s,e)}" GotFocus="function(s, e){ dateGotFocus( s, e); }" />
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
                                                <HeaderStyle Spacing="1px"></HeaderStyle>
                                                <FooterStyle Spacing="17px"></FooterStyle>
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                        <%--</div>--%>
                                        <%--<div id="dvlblKlientFurnitori">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="klientFurnitor_ButtonEdit" ID="lblKlientFurnitori"
                                            runat="server" Text="Klient/Furnitor: " ClientInstanceName="lblKlientFurnitori">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvklientFurnitor_ButtonEdit">--%>
                                        <dx:ASPxComboBox ID="klientFurnitor_ButtonEdit" runat="server" ClientInstanceName="klientFurnitor_ButtonEdit"
                                            Width="100%" ShowShadow="False" ValueType="System.Int32" EnableClientSideAPI="True"
                                            IncrementalFilteringMode="Contains" EnableSynchronization="True" EnableCallbackMode="True"
                                            DropDownRows="3" CallbackPageSize="3" OnItemRequestedByValue="klientFurnitor_ButtonEdit_ItemRequestedByValue"
                                            OnItemsRequestedByFilterCondition="klientFurnitor_ButtonEdit_ItemsRequestedByFilterCondition"
                                            SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){shfaqKlientetFurnitoret();}" SelectedIndexChanged="function (s,e){KlientFurnitoriChanged()}"
                                                GotFocus="function(s, e){s.SelectAll();}" />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
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
                                    </div>
                                    <dx:ASPxLabel runat="server" ID="pergjigja" Text="">
                                    </dx:ASPxLabel>
                                    <br />

                                    <div id="gridak" class="atributeDiveFshehur">
                                        <dx:ASPxGridView ID="grid_dokKryesor" runat="server" ClientInstanceName="grid_dokKryesor"
                                            Width="100%" OnDataBound="grid_dokKryesor_DataBound" OnHtmlRowCreated="grid_dokKryesor_HtmlRowCreated"
                                            OnCustomJSProperties="grid_dokKryesor_CustomJSProperties" OnAfterPerformCallback="grid_dokKryesor_AfterPerformCallback"
                                            OnProcessColumnAutoFilter="grid_dokKryesor_ProcessColumnAutoFilter" OnCustomCallback="grid_dokKryesor_CustomCallback">
                                            <ClientSideEvents SelectionChanged="function(s,e){OnGridSelectionChanged(s,e);}"
                                                BeginCallback="function(s,e){begincallbackKrye(s,e)}" EndCallback="function (s,e){EndCallback()}" />
                                            <Styles>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                            <StylesEditors>
                                                <ProgressBar Height="25px">
                                                </ProgressBar>
                                            </StylesEditors>
                                        </dx:ASPxGridView>
                                    </div>
                                    <br />
                                    <br />
                                    <div id="gridal" class="atributeDiveFshehur">
                                        <dx:ASPxGridView ID="grid_dokLidhes" runat="server" ClientInstanceName="grid_dokLidhes"
                                            Width="100%" OnDataBound="grid_dokLidhes_DataBound" OnHtmlRowPrepared="grid_dokLidhes_HtmlRowPrepared"
                                            OnHtmlRowCreated="grid_dokLidhes_HtmlRowCreated" OnCustomJSProperties="grid_dokLidhes_CustomJSProperties"
                                            OnAfterPerformCallback="grid_dokLidhes_AfterPerformCallback"
                                            OnProcessColumnAutoFilter="grid_dokLidhes_ProcessColumnAutoFilter">
                                            <ClientSideEvents SelectionChanged="function(s,e){OnGridSelectionChangedLidhes(s,e);}"
                                                BeginCallback="function(s,e){begincallback()}" />
                                            <Styles>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                            <StylesEditors>
                                                <ProgressBar Height="25px">
                                                </ProgressBar>
                                            </StylesEditors>
                                        </dx:ASPxGridView>
                                    </div>
                                    <br />
                                    <br />
                                    <table id="tblTotal" class="renditKontrolle" align="right">
                                        <tbody>
                                            <%--<tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td width="50%">
                                            </td>
                                        </tr>--%>
                                        </tbody>
                                    </table>
                                    <div id="dvTotal" class="atributeDiveFshehur">
                                        <%--<div id="dvlblDateRegj">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="dtRegjistrimi_DateEdit" ID="lblDateRegj"
                                            runat="server" Text="Date regjistrimi: " ClientInstanceName="lblDateRegj">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvdtRegjistrimi_DateEdit">--%>
                                        <dx:ASPxDateEdit ID="dtRegjistrimi_DateEdit" runat="server" ClientInstanceName="dtRegjistrimi_DateEdit"
                                            ShowShadow="False" Width="100%">
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
                                                <HeaderStyle Spacing="1px"></HeaderStyle>
                                                <FooterStyle Spacing="17px"></FooterStyle>
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                        <%--</div>--%>
                                        <%--<div id="dvlblTotali">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTotali" ID="lblTotali" ClientInstanceName="lblTotali"
                                            runat="server" Text="Totali">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvtxtTotali">--%>
                                        <dx:ASPxTextBox ID="txtTotali" ClientInstanceName="txtTotali" runat="server"
                                            Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
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
                                        <%--<div id="dvlblTotali2">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTotali2" ID="lblTotali2" ClientInstanceName="lblTotali2"
                                            runat="server" Text="Totali">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvtxtTotali2">--%>
                                        <dx:ASPxTextBox ID="txtTotali2" ClientInstanceName="txtTotali2" runat="server"
                                            Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
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
                                        <%--<div id="dvlblDiferenca">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtDiferenca" ID="lblDiferenca"
                                            ClientInstanceName="lblDiferenca" runat="server" Text="Diferenca">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvtxtDiferenca">--%>
                                        <dx:ASPxTextBox ID="txtDiferenca" ClientInstanceName="txtDiferenca" runat="server"
                                            Width="100%">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
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
                                    </div>
                                    <br />
                                    <br />
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
            <%--  <dx:ASPxButton ID="ruajLidhje_Button" runat="server" Text="Ruaj Lidhje" CausesValidation="false"
            ClientInstanceName="ruajLidhje_Button" AutoPostBack="false"  
             >
            <ClientSideEvents Click="function(s,e){ 
                                    merrTeDhena();
                                    merrTeDhena1();
                                    if(isValid())
                                    {
                                        popKuadruar.Show();
                                        grid_llogarite.PerformCallback();
                                    } 
                                    }" />
        </dx:ASPxButton>--%>
            <asp:UpdatePanel ID="pnlKryesor" runat="server">
                <ContentTemplate>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popKuadruar" runat="server" AllowDragging="True" ClientInstanceName="popKuadruar"
                        CloseAction="CloseButton" EnableAnimation="False" HeaderText="Kujdes" AllowResize="True"
                        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        ClientIDMode="AutoID" ShowHeader="true" Enabled="True">
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                <dxp:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel2" runat="server" Width="600px">
                                    <PanelCollection>
                                        <dxp:PanelContent>
                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Doni te ruhen diferencat nga kursi?">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: left;">
                                                <table width="100%">
                                                    <tr align="left" style="width: 20%">
                                                        <td>
                                                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="PO" OnClick="ruajLidhje_Button_Click"
                                                                Width="100%">
                                                                <ClientSideEvents Click="function(s, e) {
	        popKuadruar.Hide();
    document.getElementById('hfMeKontabilizim').value = 'true';
    RuajClick(s,e);
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td style="width: 20%">
                                                            <dx:ASPxButton ID="ASPxButton2" runat="server" Text="JO" OnClick="ruajLidhje_Button_Click"
                                                                Width="100%">
                                                                <ClientSideEvents Click="function(s, e) {
		popKuadruar.Hide();
        document.getElementById('hfMeKontabilizim').value = 'false';
        RuajClick(s,e);
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td style="width: 60%">
                                                            <dx:ASPxTextBox ID="txtKuadruar" runat="server" ClientInstanceName="txtKuadruar"
                                                                Width="100%">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxGridView ID="grid_llogarite" runat="server" ClientInstanceName="grid_llogarite" Width="100%">
                                                                <Styles>
                                                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                                    </Header>
                                                                </Styles>
                                                                <StylesEditors>
                                                                    <ProgressBar Height="25px">
                                                                    </ProgressBar>
                                                                </StylesEditors>
                                                            </dx:ASPxGridView>
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
        </div>
        <asp:UpdatePanel ID="pnlpopup" runat="server" ChildrenAsTriggers="True" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="ASPxPopupControl1" runat="server" AllowDragging="True"
                    AllowResize="True" AppearAfter="10" ClientIDMode="AutoID" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                    <ClientSideEvents CloseUp="function(s, e) {
	closePopup(s,e);
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
        <asp:UpdatePanel ID="pnlhf" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <asp:HiddenField ID="hfNiveli" runat="server" />
                <asp:HiddenField ID="hfNrDokumenti" runat="server" />
                <asp:HiddenField ID="hfDtDokumenti" runat="server" />
                <asp:HiddenField ID="hfVleftaPaLikujduar" runat="server" />
                <asp:HiddenField ID="hfVlefta" runat="server" />
                <asp:HiddenField ID="hfMonedha" runat="server" />
                <asp:HiddenField ID="hfKursi" runat="server" />
                <asp:HiddenField ID="hfNiveli1" runat="server" />
                <asp:HiddenField ID="hfNrDokumenti1" runat="server" />
                <asp:HiddenField ID="hfDtDokumenti1" runat="server" />
                <asp:HiddenField ID="hfVleftaPaLikujduar1" runat="server" />
                <asp:HiddenField ID="hfVlefta1" runat="server" />
                <asp:HiddenField ID="hfMonedha1" runat="server" />
                <asp:HiddenField ID="hfKursi1" runat="server" />
                <asp:HiddenField ID="hfKuadruar" runat="server" />
                <asp:HiddenField ID="hfDiferenca" runat="server" />
                <asp:HiddenField ID="hfMeKontabilizim" runat="server" />
                <asp:HiddenField ID="hfKolonaGride" runat="server" />
                <asp:HiddenField ID="hfStatusRuajtje" runat="server" />
                <asp:HiddenField ID="hfIdDokKryesor" runat="server" />
                <asp:HiddenField ID="hfIdDokLidhes" runat="server" />
                <asp:HiddenField ID="hfKontrolletNrAutom" runat="server" />
                <asp:HiddenField ID="hfAtributeNrAutom" runat="server" />
                <asp:HiddenField ID="hfqkmesazhiVDK" runat="server" Value="jo" />
                <asp:HiddenField ID="hfUrlVDK" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
    </form>
</body>
</html>
