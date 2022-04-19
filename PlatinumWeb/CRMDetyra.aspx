<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMDetyra.aspx.cs" Inherits="PlatinumWeb.CRMDetyra" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha CRM</title>
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link id="Link1" runat="server" rel="shortcut icon" href="~/images/CRM/faviconCRM.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/images/CRM/faviconCRM.ico" type="image/ico" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <%--<script type="text/javascript" src="~/js/srcCRM/js/jquery.mmenu.min.all.js"></script>--%>
    <link type="text/css" rel="stylesheet" href="~/js/srcCRM/css/jquery.mmenu.all.css" />
    <link type="text/css" rel="stylesheet" href="AlphaCRM.css" />
    <link href="css/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/srcCRM/js/jquery.mmenu.min.all.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/CRMDetyra.aspx-IMB.5.1.js&v49"
        type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $('nav#menu').mmenu({
                classes: "mm-light",
            });
        });
    </script>
</head>
<body>
    <div id="page">
        <div class="header headerLupe">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 1%;">
                        <a href="#menu"></a>
                    </td>
                    <td style="width: 94%; vertical-align: top;">Detyra</td>
                    <td style="width: 5%;">
                        <div id="emriLogout" class="emriLogout">
                            <div id="userInfo">
                                <div id="emri">
                                    <dx:ASPxLabel ID="lblUserEmri" ClientInstanceName="lblUserEmri" runat="server" Text=""
                                        Font-Size="14" ForeColor="White" Font-Names="Calibri">
                                    </dx:ASPxLabel>
                                </div>
                                <div id="logout">
                                    <a style="position: relative; color: white; background-image: none;" class="fa fa-sign-out fa-2x"><i class="fa fa-sign-out  fa-lg"></i>&nbsp;</a>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="content contentLupe">
            <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000" EnablePartialRendering="true">
                   
                </asp:ScriptManager>
                <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                    ViewStateMode="Enabled">
                </dx:ASPxHiddenField>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td>
                                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" CssClass="aspxmenuDetyra" runat="server" OnDataBound="ASPxMenu1_DataBound"
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
                                                                    <ClientSideEvents Click="Click_ButtonOk" />
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
                <div id="dvDetyra" style="display: none">
                    <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server"   TabSpacing="3px"
                        ClientInstanceName="PageControl" Width="100%" Height="600px" ActiveTabIndex="0">
                        <ClientSideEvents ActiveTabChanged="PageControlTabChanging" />
                        <ContentStyle>
                            <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                        </ContentStyle>
                        <TabPages>

                            <dxtc:TabPage Name="Detyrat" Text="Detyrat">
                                <ContentCollection>
                                    <dxw:ContentControl>
                                        <table class="renditKontrolle">
                                            <tbody>
                                                <tr>
                                                    <td class="renditKontrolleCaption">
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                            runat="server" Style="font-size: large" Text="Modeli:">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td class="renditKontrolleCellMeWidth33">
                                                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                            Height="24px" Width="100%" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False"
                                                            Style="font-size: medium" AnimationType="None">
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
                                                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi"
                                                            ClientInstanceName="lblKonfigurimi" >
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td class="renditKontrolleCellMeWidth33"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dx:ASPxGridView ID="ASPxGridView_Detyrat" ClientInstanceName="ASPxGridView_Detyrat" runat="server"
                                            Width="100%" OnDataBound="ASPxGridView_Detyrat_DataBound" OnAfterPerformCallback="ASPxGridView_Detyrat_AfterPerformCallback"
                                            OnHeaderFilterFillItems="ASPxGridView_Detyrat_HeaderFilterFillItems" OnProcessColumnAutoFilter="ASPxGridView_Detyrat_ProcessColumnAutoFilter"
                                            OnCustomJSProperties="ASPxGridView_Detyrat_CustomJSProperties" OnCustomCallback="ASPxGridView_Detyrat_CustomCallback"
                                            OnAutoFilterCellEditorInitialize="ASPxGridView_Detyrat_AutoFilterCellEditorInitialize">                                            
                                            <Styles>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                            <SettingsPager PageSize="15">
                                            </SettingsPager>
                                            <ClientSideEvents RowDblClick="Row_DblClick"
                                                FocusedRowChanged="function(s, e) { mbush=true;}"
                                                BeginCallback="function(s, e) {BeginCallback(s,e);}" />
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

                            <dxtc:TabPage Name="Detyra" Text="Detaje detyra">
                                <ContentCollection>
                                    <dxw:ContentControl ID="content">
                                        <table id="tblDetyra" class="renditKontrolle">
                                            <tbody>
                                            </tbody>
                                        </table>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                            Text="Kodi" ClientInstanceName="lblKodi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi">
                                            <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true"
                                                Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere" ValidationExpression="^[\s\S]{0,20}$"></RegularExpression>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi" ID="lblPershkrimi"
                                            runat="server" Text="Pershkrimi" ClientInstanceName="lblPershkrimi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxMemo ID="txtPershkrimi" runat="server" ClientInstanceName="txtPershkrimi" Rows="3"
                                            Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxMemo>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKategoria" ID="lblKategoria" runat="server"
                                            Text="Kategoria" ClientInstanceName="lblKategoria">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbKategoria" Width="100%" runat="server" ValueType="System.Int32" ClientInstanceName="cmbKategoria"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbRendesia" ID="lblRendesia" runat="server"
                                            Text="Rendesia" ClientInstanceName="lblRendesia">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbRendesia" Width="100%" runat="server" ValueType="System.Int32" ClientInstanceName="cmbRendesia"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>

                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAutorizimi" ID="lblAutorizimi"
                                            runat="server" Text="Nivel Autorizimi:" ClientInstanceName="lblAutorizimi">
                                        </dx:ASPxLabel>
                                        <div>
                                        <select id="cmbAutorizimi">
                                        </select>
                                        <asp:HiddenField ID="cmbAutorizimiHf" ClientIDMode="Static" runat="server" />
                                               
                                    </div>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="Anketat" ClientVisible="false" Text="Anketat">
                                <ContentCollection>
                                    <dxw:ContentControl>
                                        <dx:ASPxGridView ID="gvLupaAnketa" runat="server" ClientInstanceName="gvLupaAnketa"
                                            OnDataBound="gvLupaAnketa_DataBound"
                                            Width="100%"
                                            OnCustomCallback="gvLupaAnketa_CustomCallback">
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

                    </dxtc:ASPxPageControl >
                </div>
                <dx:ASPxButton ID="btnPdfExportHidden" ClientVisible="False" ClientInstanceName="btnPdfExportHidden"
                    runat="server" ToolTip="Export to Pdf" Text="Export to Pdf" Font-Size="8pt" UseSubmitBehavior="False"
                    OnClick="btnPdfExport_Click">
                    <ClientSideEvents Click="function(s, e) {
              clickExport(e) 
}" />
                </dx:ASPxButton>

                <dx:ASPxButton ID="btnXlsxExportHidden" ClientVisible="False" ClientInstanceName="btnXlsxExportHidden"
                    runat="server" ToolTip="Export to Xlsx" Text="Export to Xlsx" Font-Size="8" UseSubmitBehavior="false"
                    OnClick="btnXlsxExport_Click">
                    <ClientSideEvents Click="function(s, e) { clickExport(e) }" />
                </dx:ASPxButton>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                    <ContentTemplate>
                        <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView_Detyrat"
                            ExportedRowType="Selected" />
                        <asp:HiddenField ID="hfKonffillestar" runat="server" />
                        <asp:HiddenField ID="hfLidhur" runat="server" />
                        <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:HiddenField ID="hfKontrollet" runat="server" />
                        <asp:HiddenField ID="hfStatusi" runat="server" />
                        <asp:HiddenField ID="hfKodi" runat="server" />
                        <asp:HiddenField ID="hfEmertimi" runat="server" />
                        <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                        <asp:HiddenField ID="hfLupaAutorizimi" runat="server" />
                        <asp:HiddenField ID="hfDataVlefshmerie" runat="server" />
                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ASPxMenu1"  />  
                    </Triggers>
                </asp:UpdatePanel>
                <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
                </dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="hfNrAutoDet" runat="server" ClientInstanceName="hfNrAutoDet">
                </dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
                </dx:ASPxHiddenField>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                    <ClientSideEvents Closing="closing" />
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </form>
        </div>
        <nav id="menu">
            <ul id="ulMenu">
            </ul>
        </nav>
    </div>
</body>
</html>
