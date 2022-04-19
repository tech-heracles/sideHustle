<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Raport_PivotGrid.aspx.cs"
    Inherits="PlatinumWeb.Raport_PivotGrid" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxg" %>
<%@ Register Assembly="DevExpress.XtraCharts.v18.2.Web, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxcharts" %>
<%@ Register Assembly="DevExpress.XtraCharts.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxge" %>
<%@ Register Assembly="DevExpress.PivotGrid.v18.2.Core, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Data.PivotGrid" TagPrefix="dxgc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/FilterPopup.ascx" TagPrefix="uc1" TagName="FilterPopup" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <style type="text/css">
        .style9 {
            width: 66%;
        }

        .style13 {
            font-weight: 700;
            color: #0072c6;
        }

        td.tdStyle6 {
            width: 16%;
        }

        td.tdStyle7 {
            width: 14%;
        }

        td.tdStyle8 {
            width: 30%;
        }

        td.tdStyle9 {
            width: 5%;
        }
    </style>
    <link href="js/css/pepper-grinder/jquery-ui.css" rel="stylesheet" id="themeJQuery"
        type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" rel="stylesheet" media="screen" type="text/css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/MyJQGridFushat-IMB.2.1.js;~/js/myJQGridPivot-IMB.2.1.js;~/js/aspx.js/Raport_PivotGrid.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
          
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
      </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfTeDrejtaRaporti" ClientInstanceName="hfTeDrejtaRaporti" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        <!-- MENUJA -->
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
                                <ClientSideEvents ItemClick="function(s, e) { menu_click(s,e);}" Init="function(s) {s.SetClientVisible(true);}" />
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
                <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel"
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
                                                            OnClick="fshiKonfigurimRaporti" Text="Ok">
                                                            <ClientSideEvents Click="function(s, e) { popFshi.Hide(); Utils.shfaqLoadingGif();;}" />
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
                            </dx:ASPxPanel >
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--  FUNDI MENUSE-->
        <div style="width: 100%" id="contentDiv">
            <!-- TABET -->
            <dx:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
                  TabSpacing="3px" Width="100%" ActiveTabIndex="2">
                <ContentStyle>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                </ContentStyle>
                <TabPages>
                    <dx:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl3" runat="server">
                                <dx:ASPxGridView ID="ASPxGridView_KonfigPivotGrid" runat="server" ClientInstanceName="ASPxGridView_KonfigPivotGrid"
                                    Width="60%" OnAfterPerformCallback="ASPxGridView_KonfigPivotGrid_AfterPerformCallback"
                                    OnHeaderFilterFillItems="ASPxGridView_KonfigPivotGrid_HeaderFilterFillItems"
                                    OnDataBound="ASPxGridView_KonfigPivotGrid_DataBound" OnCustomCallback="ASPxGridView_KonfigPivotGrid_CustomCallback"
                                    OnCustomJSProperties="ASPxGridView_KonfigPivotGrid_CustomJSProperties" SettingsBehavior-ColumnResizeMode="Control">
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                        SelectionChanged="function(s, e){OnGridSelectionChanged(e);}" FocusedRowChanged="function(s, e) {
	  mbush=true;	
}"
                                        BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                                    <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="Konfigurimi" Text="Konfigurimi">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server">
                                <table id="tblInfoKonfigRaporti">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lblKodi" runat="server" Text="Kodi:" ClientInstanceName="lblKodi">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="kodi_TextBox" runat="server" ClientInstanceName="kodi_TextBox"
                                                    Width="170px">
                                                    <ClientSideEvents TextChanged="function(s, e) {
	
		}" />
                                                    <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true"
                                                        RegularExpression-ValidationExpression="^[\s\S]{0,20}$" RegularExpression-ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere" ValidationExpression="^[\s\S]{0,20}$"></RegularExpression>
                                                        <RequiredField IsRequired="true" ErrorText="*" />
                                                    </ValidationSettings>
                                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                <%--<div id="dvlblAutorizimi"> --%>
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAutorizimi" ID="lblAutorizimi"
                                                    runat="server" Text="Nivel Autorizimi:" ClientInstanceName="lblAutorizimi">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxComboBox ID="cmbAutorizimi" runat="server" ClientInstanceName="cmbAutorizimi" OnItemRequestedByValue="cmbAutorizimi_ItemRequestedByValue"
                                                    SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                                    <ClientSideEvents ButtonClick="function(s, e) {Autorizime_Click();}" />
                                                    <DropDownButton>
                                                        <Image>
                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                        </Image>
                                                    </DropDownButton>
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                        ValidationGroup="entries1" ValidateOnLeave="false">
                                                        <RequiredField IsRequired="True"></RequiredField>
                                                    </ValidationSettings>
                                                    <DisabledStyle Font-Bold="False">
                                                    </DisabledStyle>
                                                </dx:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lblPershkrimi" runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="pershkrimi_TextBox" runat="server" ClientInstanceName="pershkrimi_TextBox"
                                                    Width="170px">
                                                    <ClientSideEvents TextChanged="function(s, e) {
	
		}" />
                                                    <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table id="tblFushatOrganika" style="display: none">
                                                        </table>
                                                        <div id="divFushatOrganikaPager" style="display: none">
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <table id="tblFushatKontabiliteti" style="display: none">
                                                        </table>
                                                        <div id="divFushatKontabilitetiPager" style="display: none">
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <table id="tblFushatPunonjesi" style="display: none"></table>
                                                        <div id="divFushatPunonjesiPager" style="display: none"></div>
                                                    </td>
                                                    <td>
                                                        <table id="tblFushatListepagesa" style="display: none"></table>
                                                        <div id="divFushatListepagesaPager" style="display: none"></div>
                                                    </td>
                                                    <td>
                                                        <table id="tblFushatShitje" style="display: none">
                                                        </table>
                                                        <div id="divFushatShitjePager" style="display: none">
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <table id="tblFushatBlerje" style="display: none">
                                                        </table>
                                                        <div id="divFushatBlerjePager" style="display: none">
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <table id="tblFushatMagazina" style="display: none">
                                                        </table>
                                                        <div id="divFushatMagazinaPager" style="display: none">
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <table id="tblFushatKlient" style="display: none">
                                                        </table>
                                                        <div id="divFushatKlientPager" style="display: none">
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <table id="tblFushatFurnitor" style="display: none">
                                                        </table>
                                                        <div id="divFushatFurnitorPager" style="display: none">
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <table id="tblFushatLlogarite" style="display: none">
                                                        </table>
                                                        <div id="divFushatLlogaritePager" style="display: none">
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <table id="tblFushatArtikull" style="display: none">
                                                        </table>
                                                        <div id="divFushatArtikullPager" style="display: none">
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <table id="tblFushatAnketa" style="display: none"></table>
                                                        <div id="divFushatAnketaPager" style="display: none"></div>
                                                    </td>
                                                    <td>
                                                        <table id="tblFushatAgjenti" style="display: none"></table>
                                                        <div id="divFushatAgjentiPager" style="display: none"></div>
                                                    </td>



                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="tblRow">
                                            </table>
                                            <div id="divRowPager">
                                            </div>
                                        </td>
                                        <td>
                                            <table id="tblColumn">
                                            </table>
                                            <div id="divColumnPager">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="tblData">
                                            </table>
                                            <div id="divDataPager">
                                            </div>
                                        </td>
                                        <td>
                                            <table id="tblFilter">
                                            </table>
                                            <div id="divFilterPager">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="Raporti" Text="Raporti">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server">
                                <div style="display: inline; max-height: 100px;">
                                    <dx:ASPxNavBar ID="navBarFiltrat" runat="server" ClientInstanceName="navBarFiltrat"
                                        Width="100%" Font-Bold="True" CssPostfix="DevEx" ClientIDMode="AutoID">
                                        <Groups>
                                            <dx:NavBarGroup Text="Filtrat" Name="filtratRaport" HeaderStyle-Font-Bold="true"
                                                HeaderStyle-Font-Size="14px" HeaderStyle-ForeColor="Gray">
                                                <HeaderStyle Font-Bold="True" Font-Size="14px" ForeColor="Gray"></HeaderStyle>
                                                <ContentTemplate>
                                                    <div id="filtratRaportDiv">
                                                        <div id="dateDokumenti" style="visibility: hidden">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td class="tdStyle6">
                                                                        <dx:ASPxLabel ID="lblDtDok" ClientInstanceName="lblDtDok" runat="server" Text="Datë dokumenti"
                                                                            Style="font-weight: 700; color: #0072c6">
                                                                        </dx:ASPxLabel>

                                                                    </td>
                                                                    <td style="width: 200px">
                                                                        <dx:ASPxComboBox runat="server" SelectedIndex="0" ID="filterDate" ClientInstanceName="filterDate">
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td class="tdStyle8">
                                                                        <dx:ASPxRadioButtonList ID="radDtDok" ClientInstanceName="radDtDok" Font-Size="14px"
                                                                            runat="server" CssPostfix="Glass" RepeatColumns="4" Height="16px" EnableClientSideAPI="true"
                                                                            Border-BorderStyle="None" ForeColor="Black">
                                                                            <ClientSideEvents ValueChanged="function(s,e){Utils.toggleKontrolletPeriudha(s,txtNgaDok,txtDeriDok);}"
                                                                                Init="function(s,e){Utils.toggleKontrolletPeriudha(s,txtNgaDok,txtDeriDok);}" />
                                                                            <Items>
                                                                                <dx:ListEditItem Text="Aktuale" Value="Aktuale" />
                                                                                <dx:ListEditItem Text="Periudha" Value="Periudha" />
                                                                                <dx:ListEditItem Text="Viti Ushtrimor" Value="VitiUshtrimor" />
                                                                                <dx:ListEditItem Text="Gjithe Vitet" Value="GjitheVitet" Selected="true" />
                                                                            </Items>
                                                                        </dx:ASPxRadioButtonList>
                                                                    </td>
                                                                    <td class="tdStyle9">
                                                                        <dx:ASPxLabel ID="lblNgaDok" runat="server" Text="Nga" Style="font-weight: 700; color: #0072c6">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="tdStyle7">
                                                                        <dx:ASPxDateEdit ID="txtNgaDok" ClientEnabled="false" runat="server" TabIndex="10"
                                                                            AutoPostBack="false" AllowNull="false" DateOnError="Today" EditFormat="Custom"
                                                                            EditFormatString="dd/MM/yyyy" ValidationSettings-CausesValidation="True" ClientInstanceName="txtNgaDok"
                                                                            CssPostfix="Glass" Height="16px">
                                                                            <ClientSideEvents DateChanged="function (s,e){ngaDokDateChanged(s,e);}" />
                                                                            <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                                                <HeaderStyle Spacing="1px" />
                                                                                <FooterStyle Spacing="4px" />
                                                                            </CalendarProperties>
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                            <ValidationSettings CausesValidation="True">
                                                                                <ErrorImage Height="14px" Width="14px" />
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </dx:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="tdStyle9">
                                                                        <dx:ASPxLabel ID="lblDeriDok" runat="server" Text="Deri" Style="font-weight: 700; color: #0072c6">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="tdStyle7">
                                                                        <dx:ASPxDateEdit ID="txtDeriDok" ClientEnabled="false" runat="server" TabIndex="11"
                                                                            AutoPostBack="false" AllowNull="false" DateOnError="Today" EditFormat="Custom"
                                                                            EditFormatString="dd/MM/yyyy" ClientInstanceName="txtDeriDok" CssPostfix="Glass"
                                                                            Height="16px">
                                                                            <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                                                <HeaderStyle Spacing="1px" />
                                                                                <FooterStyle Spacing="4px" />
                                                                            </CalendarProperties>
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                            <ValidationSettings CausesValidation="True">
                                                                                <ErrorImage Height="14px" Width="14px" />
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </dx:ASPxDateEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="perioda" style="visibility: hidden">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td class="tdStyle6">
                                                                        <dx:ASPxLabel ID="lblPerioda" Width="80%" runat="server" CssClass="style13" Text="Perioda">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 200px">
                                                                        <dx:ASPxComboBox runat="server" ID="fushatDateTimePG" ClientInstanceName="fushatDateTimePG">
                                                                            <ClientSideEvents SelectedIndexChanged="fushatDateTimeSelectedChanged" />
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td class="tdStyle8">

                                                                        <dx:ASPxComboBox ID="cmbPerioda" runat="server" CssPostfix="Glass" ValueType="System.String"
                                                                            Height="23px" ClientInstanceName="cmbPerioda">
                                                                            <LoadingPanelImage>
                                                                            </LoadingPanelImage>
                                                                            <Items>
                                                                                <dx:ListEditItem Text="Vjetore" Value="0" Selected="true" />
                                                                                <dx:ListEditItem Text="3 mujore" Value="1" />
                                                                                <dx:ListEditItem Text="Mujore" Value="2" />
                                                                                <dx:ListEditItem Text="Vjetore, 3 mujore" Value="3" />
                                                                                <dx:ListEditItem Text="Vjetore, 3 mujore, mujore" Value="4" />
                                                                                <dx:ListEditItem Text="Ditore" Value="5" />
                                                                            </Items>
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                            <ValidationSettings>
                                                                                <ErrorImage Height="14px" Width="14px" />
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                            <ClientSideEvents SelectedIndexChanged="periodatSelectedChanged" />
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td colspan="4"></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="vepLlogaritese" style="visibility: hidden">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td class="tdStyle6">
                                                                        <dx:ASPxLabel ID="ASPxLabel1" Width="80%" runat="server" CssClass="style13" Text="Veprime LLogaritese">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 200px">
                                                                        <dx:ASPxComboBox runat="server" ID="fushatNumerikePG" ClientInstanceName="fushatNumerikePG" CssPostfix="Glass" ValueType="System.String">
                                                                            <ClientSideEvents SelectedIndexChanged="fushatNumerikeSelectedChanged" />
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td class="tdStyle8">
                                                                        <dx:ASPxComboBox ID="cmbVepLlogaritese" runat="server" CssPostfix="Glass" ValueType="System.String"
                                                                            Height="23px" ClientInstanceName="cmbVepLlogaritese">
                                                                            <LoadingPanelImage>
                                                                            </LoadingPanelImage>
                                                                            <Items>
                                                                                <dx:ListEditItem Text="Shuma" Value="0" Selected="true" />
                                                                                <dx:ListEditItem Text="Min" Value="1" />
                                                                                <dx:ListEditItem Text="Max" Value="2" />
                                                                                <dx:ListEditItem Text="Mesatare" Value="3" />
                                                                                <dx:ListEditItem Text="Numri" Value="4" />
                                                                            </Items>
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                            <ValidationSettings>
                                                                                <ErrorImage Height="14px" Width="14px" />
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                            <ClientSideEvents SelectedIndexChanged="cmbveprimeLlogariteseSelectedChanged" />
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td colspan="4"></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="tipiGrafikut" style="visibility: hidden">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td class="tdStyle6">
                                                                        <dx:ASPxLabel ID="tipiGrafikutLabel" Width="80%" runat="server" CssClass="style13"
                                                                            Text="Tipi Grafikut" ClientInstanceName="tipiGrafikutLabel">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="tdStyle8">
                                                                        <dx:ASPxComboBox ID="cmbGrafiku" runat="server" CssPostfix="Glass" ValueType="System.String"
                                                                            Height="23px" ClientInstanceName="cmbGrafiku">
                                                                            <LoadingPanelImage>
                                                                            </LoadingPanelImage>
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                            <ValidationSettings>
                                                                                <ErrorImage Height="14px" Width="14px" />
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td colspan="4"></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </dx:NavBarGroup>
                                        </Groups>
                                    </dx:ASPxNavBar>
                                    <br />
                                    <br />
                                    <table style="padding-bottom: 10px;">
                                        <tr>
                                            <td style="padding-right: 2px;">
                                                <dx:ASPxButton runat="server" ID="ButtonUndo" AutoPostBack="False" ClientEnabled="False"
                                                    ClientInstanceName="btnUndo" Text="Undo">
                                                    <ClientSideEvents Click="function(s, e) { ASPxPivotGridRaporti.PerformCallback('UNDO'); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td style="padding-left: 2px;">
                                                <dx:ASPxButton runat="server" ID="ButtonRedo" AutoPostBack="False" ClientEnabled="False"
                                                    ClientInstanceName="btnRedo" Text="Redo">
                                                    <ClientSideEvents Click="function(s, e) { ASPxPivotGridRaporti.PerformCallback('REDO'); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td style="padding-left: 2px;">
                                                <dx:ASPxButton runat="server" ID="btnGrafik" AutoPostBack="False" ClientEnabled="True"
                                                    ClientInstanceName="btnGrafik" Text="Me Grafik">
                                                    <ClientSideEvents Click="function(s, e) { ButtonGrafikClick(s, e); }" />
                                                </dx:ASPxButton>
                                            </td>

                                            <td style="padding-left: 2px;">
                                                <dx:ASPxButton runat="server" ID="btnPaGrafik" AutoPostBack="False" ClientEnabled="True"
                                                    ClientInstanceName="btnPaGrafik" Text="Pa Grafik">
                                                    <ClientSideEvents Click="function(s, e) { ButtonPaGrafikClick(s, e); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton runat="server" ID="showFilterbtn" AutoPostBack="false" Text="Krijo Filter">
                                                    <ClientSideEvents Click="function(s,e){ASPxPivotGridRaporti.ShowPrefilter()}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>

                                                <dx:ASPxCallbackPanel EnableHierarchyRecreation="false" ID="callbackCheckBox"  ClientInstanceName="callbackCheckBox" OnCallback="callbackCheckBox_Callback" runat="server">
                                                    <PanelCollection>
                                                        <dx:PanelContent>
                                                            <dx:ASPxHiddenField runat="server" ID="CallbackState" ClientInstanceName="CallbackState"></dx:ASPxHiddenField>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxCheckBox ID="RowGrandTotal" runat="server" CssClass="style13" Text="Grand Total Rreshta"
                                                                            ClientInstanceName="RowGrandTotal" AutoPostBack="false"
                                                                            RightToLeft="False">
                                                                        </dx:ASPxCheckBox>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxCheckBox ID="RowTotal" runat="server" CssClass="style13" Text="Total rreshta"
                                                                            ClientInstanceName="RowTotal"
                                                                            RightToLeft="False">
                                                                        </dx:ASPxCheckBox>

                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxCheckBox ID="ColumnGrandTotal" runat="server" CssClass="style13" Text="Grand Total Colona"
                                                                            ClientInstanceName="ColumnGrandTotal"
                                                                            RightToLeft="False">
                                                                        </dx:ASPxCheckBox>

                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxCheckBox ID="ColumnTotal" runat="server" CssClass="style13" Text="Total Colona"
                                                                            ClientInstanceName="ColumnTotal"
                                                                            RightToLeft="False">
                                                                        </dx:ASPxCheckBox>

                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxCheckBox ID="HiqVleraZero" runat="server" CssClass="style13" Text="Hiq vlera zero"
                                                                            ClientInstanceName="HiqVleraZero"
                                                                            RightToLeft="False">
                                                                        </dx:ASPxCheckBox>

                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dx:PanelContent>
                                                    </PanelCollection>

                                                </dx:ASPxCallbackPanel >

                                            </td>

                                        </tr>
                                    </table>
                                </div>
                                <dxg:ASPxPivotGrid runat="server" ID="ASPxPivotGridRaporti" OnGridLayout="ASPxPivotGridRaporti_GridLayout" OptionsView-ShowHorizontalScrollBar="true"
                                    ClientInstanceName="ASPxPivotGridRaporti" OnCustomCallback="ASPxPivotGridRaporti_CustomCallback" Width="100%"
                                    OnCustomChartDataSourceData="ASPxPivotGridRaporti_CustomChartDataSourceData" OnPageIndexChanged="ASPxPivotGridRaporti_PageIndexChanged" OnCustomFieldValueCells="ASPxPivotGridRaporti_CustomFieldValueCells">
                                    <ClientSideEvents AfterCallback="function(s, e) { ASPxPivotGridRaportiAfterCallback(s, e); }" />
                                    <OptionsLayout ResetOptions="OptionsPager" />
                                    <OptionsView DataHeadersDisplayMode="Popup" DataHeadersPopupMinCount="2"
                                        ShowColumnTotals="True" ShowCustomTotalsForSingleValues="False" ShowGrandTotalsForSingleValues="False"
                                        ShowTotalsForSingleValues="False" ShowRowGrandTotals="True" EnableContextMenuScrolling="true" EnableFilterControlPopupMenuScrolling="true"></OptionsView>
                                    <OptionsChartDataSource ProvideEmptyCells="False" ProvideDataByColumns="False" MaxAllowedSeriesCount="0"
                                        MaxAllowedPointCountInSeries="0"></OptionsChartDataSource>
                                    <OptionsData AllowCrossGroupVariation="False" />
                                    <OptionsPager PagerAlign="Left" RowsPerPage="10">
                                    </OptionsPager>
                                    <OptionsLoadingPanel>
                                        <Style ImageSpacing="5px">
												
												</Style>
                                    </OptionsLoadingPanel>
                                    <Styles CssPostfix="DevEx">
                                        <LoadingPanel ImageSpacing="5px">
                                        </LoadingPanel>
                                    </Styles>
                                    <StylesEditors ButtonEditCellSpacing="0">
                                    </StylesEditors>
                                </dxg:ASPxPivotGrid>
                               <%-- <uc1:FilterPopup runat="server"  PivotGridID="ASPxPivotGridRaporti"   ID="FilterPopup" />--%>
                                <br />
                                <table>
                                    <tr>
                                        <td style="padding-left: 2px; vertical-align: middle">
                                            <dx:ASPxCheckBox ID="sipasKolonaveChk" runat="server" CssClass="style13" Text="Shfaq te dhenat sipas kolonave"
                                                ClientInstanceName="sipasKolonaveChk" OnValueChanged="sipasKolonaveChk_ValueChanged"
                                                RightToLeft="True">
                                                <DisabledStyle ForeColor="LightGray">
                                                </DisabledStyle>
                                            </dx:ASPxCheckBox>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="exportButtonGrafik" runat="server" Text="Eksporto Grafik" ClientInstanceName="exportButtonGrafik"
                                                CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="True">
                                                <ClientSideEvents Click="function (s, e){ eksportoGrafik(s, e); }" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox ID="cmbExportGrafik" runat="server" ClientInstanceName="cmbExportGrafik" AutoPostBack="False"
                                                CallbackPageSize="50">
                                                <DropDownButton>
                                                    <Image>
                                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    </Image>
                                                </DropDownButton>
                                                <Items>
                                                    <dx:ListEditItem Text="Excel Xlsx" Value="1" Selected="true" />
                                                    <dx:ListEditItem Text="Excel Xls" Value="2" />
                                                    <dx:ListEditItem Text="Png" Value="3" />
                                                    <dx:ListEditItem Text="Jpeg" Value="4" />
                                                </Items>
                                            </dx:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 4px; vertical-align: middle" colspan="2">
                                            <dx:ASPxLabel ID="saVleraNeGrafikLabel" Width="100%" runat="server" CssClass="style13"
                                                Text="Nr. i Vlerave ne Grafik" ClientInstanceName="saVleraNeGrafikLabel">
                                                <DisabledStyle ForeColor="LightGray">
                                                </DisabledStyle>
                                            </dx:ASPxLabel>
                                        </td>
                                        <td style="padding-left: 2px;">
                                            <dx:ASPxComboBox ID="cmbSaVleraNeGrafik" runat="server" CssPostfix="Glass" ValueType="System.String"
                                                Height="23px" ClientInstanceName="cmbSaVleraNeGrafik" OnSelectedIndexChanged="cmbSaVleraNeGrafik_SelectedIndexChanged">
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Width="14px" />
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 5px; vertical-align: middle" colspan="2">
                                            <dx:ASPxLabel ID="shfaqZeroLabel" runat="server" Width="100%" CssClass="style13"
                                                ClientInstanceName="shfaqZeroLabel" Text="Shfaq si te barabarta me zero vlerat me te vogla se:">
                                                <DisabledStyle ForeColor="LightGray">
                                                </DisabledStyle>
                                            </dx:ASPxLabel>
                                        </td>
                                        <td style="padding-left: 2px;">
                                            <dx:ASPxTextBox ID="CellValueThreshold" ClientInstanceName="CellValueThreshold"
                                                Width="170px" runat="server" CssClass="style13" AutoPostBack="False" />
                                        </td>
                                    </tr>
                                </table>
                                <dxcharts:WebChartControl ID="WebChart" runat="server" ClientInstanceName="WebChart"
                                    Width="1400px" Height="700px" OnCustomCallback="WebChart_CustomCallback" EnableViewState="False"
                                    SaveStateOnCallbacks="False" SeriesDataMember="Series">
                                    <legend maxhorizontalpercentage="100" alignmenthorizontal="Left" alignmentvertical="BottomOutside"
                                        horizontalindent="0" maxverticalpercentage="100" font="Tahoma, 7pt"></legend>
                                    <seriestemplate argumentdatamember="Arguments" valuedatamembersserializable="Values">
									<ViewSerializable>
										<cc1:SideBySideBarSeriesView></cc1:SideBySideBarSeriesView>
									</ViewSerializable>
											<LabelSerializable>
												<cc1:SideBySideBarSeriesLabel LineVisible="false">
													<FillStyle> 
														<OptionsSerializable>
															<cc1:SolidFillOptions />
														</OptionsSerializable>
													</FillStyle>
													<PointOptionsSerializable>
														<cc1:PointOptions />
													</PointOptionsSerializable>
												</cc1:SideBySideBarSeriesLabel>
											</LabelSerializable>
											<LegendPointOptionsSerializable>
												<cc1:PointOptions  />
											</LegendPointOptionsSerializable>
								</seriestemplate>
                                    <diagramserializable>
									<cc1:XYDiagram PaneLayoutDirection="Horizontal">
											<axisx visibleinpanesserializable="-1">
												<label staggered="True" />
												<range sidemarginsenabled="True" />
												<Label Staggered="True"></Label>
												<Range SideMarginsEnabled="True"></Range>
											</axisx>
											<axisy visibleinpanesserializable="-1">
												<range sidemarginsenabled="True" />
												<Range SideMarginsEnabled="True"></Range>
											</axisy>
									</cc1:XYDiagram>                                           
								</diagramserializable>
                                    <fillstyle>
											<OptionsSerializable>
												<cc1:SolidFillOptions />
											</OptionsSerializable>
								</fillstyle>
                                    <borderoptions visible="False"></borderoptions>
                                    <crosshairoptions>
									<CommonLabelPositionSerializable>
										<cc1:CrosshairMousePosition></cc1:CrosshairMousePosition>
									</CommonLabelPositionSerializable>
								</crosshairoptions>
                                    <tooltipoptions>
									<ToolTipPositionSerializable>
										<cc1:ToolTipMousePosition></cc1:ToolTipMousePosition>
									</ToolTipPositionSerializable>
								</tooltipoptions>
                                </dxcharts:WebChartControl>
                                <dxge:ASPxPivotGridExporter ID="ASPxPivotGridExporterRaporti" runat="server" ASPxPivotGridID="ASPxPivotGridRaporti"
                                    Visible="False">
                                    <OptionsPrint PrintDataHeaders="False" PrintFilterHeaders="False" PrintColumnHeaders="False" PrintRowHeaders="True" PrintUnusedFilterFields="False">
                                        <PageSettings Landscape="True" />
                                    </OptionsPrint>
                                </dxge:ASPxPivotGridExporter>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanging="function(s, e){}" ActiveTabChanged="function(s, e) { activeTabChanging(s, e); }" />
                <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
            </dx:ASPxPageControl >
            <!-- FUNDI TABEVE-->
        </div>


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

        <!-- HIDDENFIELDS-->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfKontrollet" runat="server" />
                <asp:HiddenField ID="hfStatusi" runat="server" />
                <asp:HiddenField ID="fushatRaporti" runat="server" />
                <asp:HiddenField ID="rreshtaRaporti" runat="server" />
                <asp:HiddenField ID="kolonaRaporti" runat="server" />
                <asp:HiddenField ID="dataRaporti" runat="server" />
                <asp:HiddenField ID="filterRaporti" runat="server" />
                <asp:HiddenField ID="HfKolonatKonfig" runat="server" />
                <asp:HiddenField ID="HfGrupimKolone" runat="server" />
                <asp:HiddenField ID="hfLupaAutorizimi" runat="server" />

            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="HfMsgKonfig" ClientInstanceName="HfMsgKonfig" runat="server">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField runat="server" ID="HfGrupimi" ClientInstanceName="HfGrupimi"></dx:ASPxHiddenField>
        <dx:ASPxHiddenField runat="server" ID="HfGroupIntervalet" ClientInstanceName="HfGroupIntervalet"></dx:ASPxHiddenField>
        <!-- FUNDI HIDDENFIELDS-->
    </form>
</body>
</html>
