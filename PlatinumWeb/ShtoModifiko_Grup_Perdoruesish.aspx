<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShtoModifiko_Grup_Perdoruesish.aspx.cs"Inherits="PlatinumWeb.ShtoModifiko_Grup_Perdoruesish" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Xpo.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Xpo" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />

    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <link href="js/css/le-frog/jquery-ui.css" media="screen" rel="stylesheet" type="text/css" runat="server" id="themeJQuery" />
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" />    
    <link href="js/css/ui.multiselect.css" rel="stylesheet" />
    <link href="css/animate.min.css" rel="stylesheet" />
    <link href="css/bootstrap-notify.css" rel="stylesheet" />
    <link href="DataTables-1.10.12/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
     <!-- DevExtreme themes -->
    <link rel="stylesheet" type="text/css" href="Content/dx.common.css" />
    <link rel="stylesheet" type="text/css" href="Content/dx.generic.alphaweb-compact.css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />

    <!-- A DevExtreme library -->
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/ui.multiselect.js;~/bootstrap-3.3.6-dist/js/bootstrap.min.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/jquery.ui.datepicker-sq.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/JsGlobal.js;~/js/myCookies-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/Scripts/dx.viz-web.js;~/js/myDxTreeList.js;~/js/aspx.js/ShtoModifiko_Grup_Perdoruesish.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
    <style>
        html {
            min-height: 100%; /* make sure it is at least as tall as the viewport */
            position: relative;
        }

        .multiselect {
            height: 300px !important;
        }

        .ui-multiselect {
            margin: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="9999999" runat="server">
        </asp:ScriptManager>
        <div id="backDiv1" runat="server">
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <asp:UpdatePanel ID="pnlMenu" runat="server">
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
                                        <ItemStyle HorizontalAlign="Left" BackColor="#CCCCCC" />
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
        <asp:UpdatePanel runat="server" ID="pnl">
            <ContentTemplate>
                <div id="dvRolet" style="display: none">
                    <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="tabetASPxPageControl" runat="server" ClientInstanceName="PageControl"
                        TabSpacing="3px" Width="100%" ActiveTabIndex="0" Height="100%" EnableClientSideAPI="True">
                        <ClientSideEvents ActiveTabChanging="function (s, e) { PageControl_ActiveTabChanging(s, e); } "
                            ActiveTabChanged="function (s, e) { tabsActiveTabChanged(s, e); } " />
                        <Paddings PaddingLeft="5px" PaddingRight="5px" Padding="2px" />
                        <TabPages>
                            <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                        <table class="renditKontrolle">
                                            <tbody>
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
                                                            <ValidationSettings>
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
                                            </tbody>
                                        </table>
                                        <table width="100%" style="overflow: scroll; overflow-x: scroll; overflow-y: scroll">
                                            <tr>
                                                <td colspan="2">
                                                    <dx:ASPxGridView ID="roletASPxGridView" runat="server" ClientInstanceName="gridaRoli"
                                                        OnAfterPerformCallback="roletASPxGridView_AfterPerformCallback"
                                                        OnAutoFilterCellEditorInitialize="roletASPxGridView_AutoFilterCellEditorInitialize"
                                                        Width="100%" OnCustomCallback="roletASPxGridView_CustomCallback" OnDataBound="roletASPxGridView_DataBound"
                                                        OnProcessColumnAutoFilter="roletASPxGridView_ProcessColumnAutoFilter">
                                                        <Templates>
                                                            <TitlePanel>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxButton ID="ASPxButton2" runat="server" ToolTip="Zgjidh kolonat" AutoPostBack="false"
                                                                                ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                                                <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s,e,gridaRoli)}"
                                                                                    Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                                            </dx:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:UpdatePanel ID="pnlruaj" runat="server">
                                                                                <ContentTemplate>
                                                                                    <dx:ASPxButton ID="ASPxButton3" runat="server" ToolTip="Ruaj kolonat" AutoPostBack="true"
                                                                                        ClientVisible="false" Image-Url="images/new/disk_blue (3).png" Font-Size="8"
                                                                                        OnClick="RuajKolona_Click">
                                                                                        <ClientSideEvents Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                                                    </dx:ASPxButton>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxButton ID="gridaSelectFaqe" runat="server" ToolTip="Zgjidh te gjithe faqen"
                                                                                AutoPostBack="false" Image-Url="images/check2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                                                <ClientSideEvents Click="function(s, e) { gridaRoli.SelectAllRowsOnPage(); }" />
                                                                            </dx:ASPxButton>

                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe"
                                                                                AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                                                <ClientSideEvents Click="function(s, e) { gridaRoli.SelectRows(); }" />
                                                                            </dx:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxButton ID="gridaUnSelectTeGjitha" runat="server" ToolTip="Fshi Zgjedhjen"
                                                                                AutoPostBack="false" Image-Url="images/uncheck2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                                                <ClientSideEvents Click="function(s, e) { gridaRoli.UnselectRows(); }" />
                                                                            </dx:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </TitlePanel>
                                                        </Templates>
                                                        <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                                            FocusedRowChanged="function (s,e) { onNdryshimFokusi(); }" BeginCallback="function (s, e) { BeginCallback(s,e); }"
                                                            EndCallback="function (s,e){PageControl.AdjustSize();}" />
                                                        <Styles>
                                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                            </Header>
                                                        </Styles>
                                                    </dx:ASPxGridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="Roli" Text="Roli">
                                <ContentCollection>
                                    <dxw:ContentControl>
                                        <table class="renditKontrolleDy">
                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="kodiASPxTextBox" ID="kodiASPxLabel"
                                                        runat="server" ClientIDMode="AutoID" Text="Kodi: ">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50">
                                                    <dx:ASPxTextBox ID="kodiASPxTextBox" runat="server" ClientInstanceName="kodiASPxTextBox"
                                                        Width="100%">
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                            SetFocusOnError="True" ValidationGroup="entries">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="True" />
                                                        </ValidationSettings>
                                                        <ReadOnlyStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </ReadOnlyStyle>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="pershkrimiASPxTextBox" ID="pershkrimiASPxLabel"
                                                        runat="server" ClientIDMode="AutoID" Text="Pershkrimi: ">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td rowspan="2" class="renditKontrolleCellMeWidth50">
                                                    <dx:ASPxMemo ID="pershkrimiASPxTextBox" runat="server" ClientInstanceName="pershkrimiASPxTextBox"
                                                        Width="100%" Rows="3">
                                                        <ClientSideEvents TextChanged="function(s, e){ndryshuarTeDhenaRoli();}" />
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                            SetFocusOnError="True" ValidationGroup="entries">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="True" />
                                                        </ValidationSettings>
                                                        <ReadOnlyStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </ReadOnlyStyle>
                                                    </dx:ASPxMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dateKrijimiASPxDateEdit" ID="dateKrijimiASPxLabel"
                                                        runat="server" ClientIDMode="AutoID" Text="Date Krijimi: ">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50">
                                                    <dx:ASPxDateEdit ID="dateKrijimiASPxDateEdit" runat="server" ClientInstanceName="dateKrijimiASPxDateEdit"
                                                        ReadOnly="True" ShowShadow="False" Width="100%">
                                                        <CalendarProperties>
                                                            <HeaderStyle Spacing="1px" />
                                                            <FooterStyle Spacing="17px" />
                                                        </CalendarProperties>
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
                                                        <ReadOnlyStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </ReadOnlyStyle>
                                                    </dx:ASPxDateEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dateModifikimiASPxDateEdit" ID="dateModifikimiASPxLabel"
                                                        runat="server" ClientIDMode="AutoID" Text="Date Modifikimi:">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50">
                                                    <dx:ASPxDateEdit ID="dateModifikimiASPxDateEdit" runat="server" ClientInstanceName="dateModifikimiASPxDateEdit"
                                                        ReadOnly="True" ShowShadow="False" Style="margin-bottom: 0px" Width="100%">
                                                        <CalendarProperties>
                                                            <HeaderStyle Spacing="1px" />
                                                            <FooterStyle Spacing="17px" />
                                                        </CalendarProperties>
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
                                                        <ReadOnlyStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </ReadOnlyStyle>
                                                    </dx:ASPxDateEdit>
                                                </td>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="idKrijuesiASPxTextBox" ID="idKrijuesiASPxLabel"
                                                        runat="server" ClientIDMode="AutoID" Text="Krijuesi:">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50">
                                                    <dx:ASPxTextBox ID="idKrijuesiASPxTextBox" runat="server" ClientInstanceName="idKrijuesiASPxTextBox"
                                                        ReadOnly="True" Width="100%">
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ReadOnlyStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </ReadOnlyStyle>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="aktivASPxCheckBox" ID="aktivASPxLabel"
                                                        runat="server" ClientIDMode="AutoID" Text="Aktiv: ">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50">
                                                    <dx:ASPxCheckBox ID="aktivASPxCheckBox" runat="server" TextSpacing="2px" ClientInstanceName="aktivASPxCheckBox">
                                                        <ClientSideEvents ValueChanged="function(s, e){ndryshuarTeDhenaRoli();}" />
                                                    </dx:ASPxCheckBox>
                                                </td>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLicenca" ID="lblLicenca"
                                                        runat="server" ClientIDMode="AutoID" Text="Licenca: ">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50">
                                                    <dx:ASPxComboBox ID="cmbLicenca" runat="server" ClientInstanceName="cmbLicenca"
                                                        IncrementalFilteringMode="Contains" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False"
                                                        ValueType="System.Int32" Width="100%" AutoPostBack="false">

                                                        <DropDownButton>
                                                            <Image>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua"
                                                                    PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </Image>
                                                        </DropDownButton>
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table class="butonat">
                                            <tr>
                                                <td align="right" style="width: 80%"></td>
                                                <td align="right" style="width: 10%">&nbsp;
                                                </td>
                                                <td align="right" style="width: 10%">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="Ndermarjet" Text="Ndermarjet">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl3" runat="server">
                                        <table style="height: 300px; width: 100%; overflow: scroll;">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">                                                
                                                    <td width="100%" style="height: 300px">
                                                        <asp:UpdatePanel runat="server" ID="gvNderRolUpdPnl">
                                                            <ContentTemplate>
                                                                
                                                                <dx:ASPxGridView ID="lboxNdermarjetRol" runat="server" 
                                                                    ClientInstanceName="gvNderRol"
                                                                    OnCustomCallback="lboxNdermarjetRol_CustomCallback" 
                                                                    OnHtmlRowCreated="lboxNdermarjetRol_OnHtmlRowCreated"
                                                                    Width="100%">
                                                                    <ClientSideEvents 
                                                                        FocusedRowChanged="function(s, e) { $('#hfNdermSel').val(s.GetRowKey(s.GetFocusedRowIndex())); }"
                                                                        SelectionChanged="function(s, e) {gvNderRol_SelectionChanged(s,e);}" 
                                                                        BeginCallback = "function(s, e) { gvNderRol_BeginCallback(s, e); }"
                                                                        EndCallback=" function (s, e) { gvNderRolEndCallbcak(s, e); } " />
                                                                    <SettingsBehavior 
                                                                        AllowDragDrop="False" 
                                                                        AllowFocusedRow="True" 
                                                                        AllowGroup="False"
                                                                        AllowSort="False" />
                                                                    <SettingsPager PageSize="100">
                                                                    </SettingsPager>
                                                                    <Settings 
                                                                        ShowVerticalScrollBar="True" 
                                                                        VerticalScrollableHeight="249" 
                                                                        ShowFilterRowMenu="true" 
                                                                        ShowFilterRow="true" />
                                                                    <SettingsLoadingPanel ImagePosition="Top"  Mode="Disabled" />
                                                                    <ImagesEditors>
                                                                        <DropDownEditDropDown>
                                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                                        </DropDownEditDropDown>
                                                                        <SpinEditIncrement>
                                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditIncrementImageHover_Aqua"
                                                                                PressedCssClass="dxEditors_edtSpinEditIncrementImagePressed_Aqua" />
                                                                        </SpinEditIncrement>
                                                                        <SpinEditDecrement>
                                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditDecrementImageHover_Aqua"
                                                                                PressedCssClass="dxEditors_edtSpinEditDecrementImagePressed_Aqua" />
                                                                        </SpinEditDecrement>
                                                                        <SpinEditLargeIncrement>
                                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeIncImageHover_Aqua"
                                                                                PressedCssClass="dxEditors_edtSpinEditLargeIncImagePressed_Aqua" />
                                                                        </SpinEditLargeIncrement>
                                                                        <SpinEditLargeDecrement>
                                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeDecImageHover_Aqua"
                                                                                PressedCssClass="dxEditors_edtSpinEditLargeDecImagePressed_Aqua" />
                                                                        </SpinEditLargeDecrement>
                                                                    </ImagesEditors>
                                                                    <Styles>
                                                                        <LoadingPanel ImageSpacing="8px">
                                                                        </LoadingPanel>
                                                                    </Styles>
                                                                    <StylesEditors>
                                                                        <CalendarHeader Spacing="1px">
                                                                        </CalendarHeader>
                                                                        <ProgressBar Height="25px">
                                                                        </ProgressBar>
                                                                    </StylesEditors>
                                                                </dx:ASPxGridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="Te Drejta" Text="Te Drejta" TabStyle-Height="100%">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl2" runat="server">
                                        <div style="height: 100%; width: 100%">
                                            <table class="renditKontrolleTre">
                                                <tbody>
                                                    <tr>
                                                        <td class="renditKontrolleCaption">
                                                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" AssociatedControlID="ASPxComboBoxNdermarrje"
                                                                ClientIDMode="AutoID" Text="Ndermarrja" Wrap="False">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="renditKontrolleCell">
                                                            <dx:ASPxComboBox ID="ASPxComboBoxNdermarrje" runat="server" ClientInstanceName="ASPxComboBoxNdermarrje"
                                                                IncrementalFilteringMode="Contains" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False"
                                                                ValueType="System.Int32" Width="100%" AutoPostBack="false">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e){ ASPxComboBoxNdermarrje_SelectedIndexChanged(s, e); }" />
                                                                <DropDownButton>
                                                                    <Image>
                                                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua"
                                                                            PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                                    </Image>
                                                                </DropDownButton>
                                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                                <td>
                                                                    <dx:ASPxCheckBox ID="cbNdryshoCmimeShitje" runat="server" ClientInstanceName="cbNdryshoCmimeShitje"
                                                                        Width="100%">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxLabel ID="lblNdryshoCmimeShitje" runat="server" Text="Ndrysho cmimet e shitjes"
                                                                        ClientInstanceName="lblNdryshoCmimeShitje" AssociatedControlID="cbNdryshoCmimeShitje"
                                                                        Wrap="False">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxCheckBox ID="cbNdryshoCmimeBlerje" runat="server" ClientInstanceName="cbNdryshoCmimeBlerje"
                                                                        Width="100%">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxLabel ID="lblNdryshoCmimeBlerje" ClientInstanceName="lblNdryshoCmimeBlerje"
                                                                        Wrap="False" AssociatedControlID="cbNdryshoCmimeBlerje" runat="server" Text="Ndrysho cmimet e blerjes">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxCheckBox ID="cbKonvertimSipasUrdherShitje" ClientInstanceName="cbKonvertimSipasUrdherShitje"
                                                                        Width="100%" runat="server">
                                                                        <ClientSideEvents Init="function(s, e){ InitKonvSipasURdherShitje(s, e); }" />
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxLabel ID="lblKonvertimSipasUrdherShitje" ClientInstanceName="lblKonvertimSipasUrdherShitje"
                                                                        AssociatedControlID="cbKonvertimSipasUrdherShitje" runat="server" Text="Konvertim Sipas Urdhër Shitje"
                                                                        Wrap="False">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="renditKontrolleCaption">
                                                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" AssociatedControlID="ASPxComboBoxViti"
                                                                ClientIDMode="AutoID" Text="Viti" Wrap="False">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="renditKontrolleCell">
                                                            <dx:ASPxComboBox ID="ASPxComboBoxViti" runat="server" ClientInstanceName="ASPxComboBoxViti"
                                                                IncrementalFilteringMode="Contains" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False"
                                                                ValueType="System.Int32" Width="100%" AutoPostBack="false">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e){ ASPxComboBoxViti_SelectedIndexChanged(s, e); }" />
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
                                                                <td>
                                                                    <dx:ASPxCheckBox ID="cbNdryshoZbritjeAnalitike" ClientInstanceName="cbNdryshoZbritjeAnalitike"
                                                                        Width="100%" runat="server">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxLabel ID="lblNdryshoZbritjeAnalitike" runat="server" Text="Ndrysho zbritje analitike"
                                                                        Wrap="False" ClientInstanceName="lblNdryshoZbritjeAnalitike" AssociatedControlID="cbNdryshoZbritjeAnalitike">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxCheckBox ID="cbNdryshoZbritjeTotale" ClientInstanceName="cbNdryshoZbritjeTotale"
                                                                        Width="100%" runat="server">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxLabel ID="lblNdryshoZbritjeTotale" ClientInstanceName="lblNdryshoZbritjeTotale"
                                                                        AssociatedControlID="cbNdryshoZbritjeTotale" runat="server" Text="Ndrysho zbritje totale"
                                                                        Wrap="False">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxCheckBox ID="cbVetemKonvertim" ClientInstanceName="cbVetemKonvertim"
                                                                        Width="100%" runat="server">
                                                                        <ClientSideEvents CheckedChanged="function(s, e){ cbVetemKonvertimClick(s, e); }" />
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxLabel ID="lblVetemKonvertim" ClientInstanceName="lblVetemKonvertim"
                                                                        AssociatedControlID="cbVetemKonvertim" runat="server" Text="Vetëm Fatura Shitje të konvertuara"
                                                                        Wrap="False">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <table class="renditKontrolle">
                                                <tr>
                                                    <td class="renditKontrolleCaption">
                                                        <div id="divgride1">
                                                            <div id="divgride2">
                                                                <div id="rowed5">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <br />
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="LidhjaRoleve" Text="Lidhja e roleve">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl4" runat="server">
                                        <div style="width: 100%">
                                            <table class="renditKontrolle">
                                                <tr>
                                                    <td class="renditKontrolleCaption"></td>
                                                    <dx:ASPxGridView ID="gridLidhjeRole" 
                                                        OnAfterPerformCallback="gridLidhjeRole_AfterPerformCallback" 
                                                        OnCustomCallback="gridLidhjeRole_CustomCallback" 
                                                        ClientInstanceName="gridLidhjeRole" runat="server"
                                                        Width="100%" 
                                                        OnDataBound="gridLidhjeRole_DataBound" 
                                                        OnHeaderFilterFillItems="gridLidhjeRole_HeaderFilterFillItems" AutoGenerateColumns="true">
                                                        <Styles>
                                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                            </Header>
                                                        </Styles>
                                                        <StylesEditors>
                                                            <ProgressBar Height="25px">
                                                            </ProgressBar>
                                                        </StylesEditors>
                                                    </dx:ASPxGridView>
                                                </tr>
                                            </table>
                                        </div>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                        </TabPages>
                        <ContentStyle>
                            <border bordercolor="#AECAF0" borderstyle="Solid" borderwidth="1px" />
                        </ContentStyle>
                    </dxtc:ASPxPageControl>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfStatusi" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                <asp:HiddenField ID="newHiddenField" runat="server" ViewStateMode="Enabled" />
                <asp:HiddenField ID="HiddenFieldViti" runat="server" ViewStateMode="Enabled" />
                <asp:HiddenField ID="HiddenFieldNdermarrje" runat="server" ViewStateMode="Enabled" />
                <asp:HiddenField ID="hfVitetSel" runat="server" />
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfNderm" runat="server" />
                <asp:HiddenField ID="hfNdermSel" runat="server" />
                <asp:HiddenField ID="hfKomponente" runat="server" />                
                <asp:HiddenField ID="hfNdermarjeChanged" runat="server" />
                <asp:HiddenField ID="hfNdryshuarTeDhenaRoli" runat="server" />
                <asp:HiddenField ID="HiddenFieldNdermarrjeDestinacion" runat="server" ViewStateMode="Enabled" />
                <asp:HiddenField ID="HiddenFieldVitiDestinacion" runat="server" ViewStateMode="Enabled" />
                <asp:HiddenField ID="HiddenFieldGjuha" runat="server" ViewStateMode="Enabled" />
                <asp:HiddenField ID="HfColTeDrejtat" runat="server" />
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popKlono" runat="server" AllowDragging="True" ClientInstanceName="popKlono"
                    CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Zgjidhni ndermarrjen"
                    Font-Bold="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    Width="492px" ClientIDMode="AutoID" CssPostfix="Glass">
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel2" runat="server" ClientIDMode="AutoID" Width="450px">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                        <div style="text-align: center;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="lblNdermarja" runat="server" Text="Ndermarrja:">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="cmbNdermarja" runat="server" ClientInstanceName="cmbNdermarja"
                                                            IncrementalFilteringMode="Contains" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False"
                                                            ValueType="System.Int32" Width="100%" AutoPostBack="false">
                                                            <ClientSideEvents SelectedIndexChanged="function (s,e){ cmbNdermarjaKlonim_SelectedIndexChanged(s, e); }" />
                                                            <DropDownButton>
                                                                <Image>
                                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua"
                                                                        PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                                </Image>
                                                            </DropDownButton>
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxLabel ID="lblViti" runat="server" Text="Viti:">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="cmbViti" runat="server" ClientInstanceName="cmbViti">
                                                            <ClientSideEvents SelectedIndexChanged="function (s,e){ cmbViti_SelectedIndexChanged(s, e); }" />
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="btnOk" runat="server" CausesValidation="False" ClientInstanceName="btnOk"
                                                            OnClick="btnOk_Click2" Text="Ruaj">
                                                            <ClientSideEvents Click="function(s, e) { popKlono.Hide(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnAnullo" runat="server" ClientIDMode="AutoID" Text="Anullo"
                                                            AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s, e) {
		popKlono.Hide();
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
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        </div>
    </form>
</body>
</html>
