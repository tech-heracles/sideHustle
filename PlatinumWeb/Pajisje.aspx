<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pajisje.aspx.cs" Inherits="PlatinumWeb.Pajisje" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>





<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/jquery.ui.datepicker-sq.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/aspx.js/Pajisje.aspx-IMB.5.8.js&v74"
        type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <div>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            </dx:ASPxGlobalEvents>
            <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
             </asp:ScriptManager>
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
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
                      
                    <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel"
                        Font-Size="9pt" Modal="True" ImagePosition="Top">
                        <LoadingDivStyle Opacity="30">
                        </LoadingDivStyle>
                    </dx:ASPxLoadingPanel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="dvPajisje" style="display: none">
                <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" ClientInstanceName="PageControl"
                      TabSpacing="3px" Width="100%" ActiveTabIndex="0" Height="600px">
                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    </ContentStyle>
                    <TabPages>
                        <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl1" runat="server">
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
                                                        ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top" AnimationType="None">
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
                                                    <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" Text="" class="klasePerLblKonfigurimi"
                                                        ClientInstanceName="lblKonfigurimi">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth33"></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dx:ASPxGridView ID="gvPajisje" ClientInstanceName="gvPajisje"
                                        runat="server" Width="100%" OnCustomJSProperties="gvPajisje_CustomJSProperties"
                                        OnDataBound="gvPajisje_DataBound" OnAfterPerformCallback="gvPajisje_AfterPerformCallback"
                                        OnCustomCallback="gvPajisje_CustomCallback" OnHeaderFilterFillItems="gvPajisje_HeaderFilterFillItems"
                                        OnProcessColumnAutoFilter="gvPajisje_ProcessColumnAutoFilter">
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <Templates>
                                            <TitlePanel>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ASPxButton2" runat="server" ToolTip="Zgjidh kolonat" AutoPostBack="false"
                                                                ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                                <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s, e, gvPajisje)}"
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
                                                                AutoPostBack="false" Image-Url="images/check2.png" Image-Height="16px" Font-Size="8"
                                                                UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="function(s, e) { gvPajisje.SelectAllRowsOnPage(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe"
                                                                AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8"
                                                                UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="function(s, e) { gvPajisje.SelectRows(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="gridaUnSelectTeGjitha" runat="server" ToolTip="Fshi Zgjedhjen"
                                                                AutoPostBack="false" Image-Url="images/uncheck2.png" Image-Height="16px" Font-Size="8"
                                                                UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="function(s, e) { gvPajisje.UnselectRows(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnXlsxExport" runat="server" ToolTip="Export to Xlsx" Image-Height="16px"
                                                                Image-Url="images/xlsx24.png" Font-Size="8" UseSubmitBehavior="false" OnClick="btnXlsxExport_Click">
                                                                <ClientSideEvents Click="function(s, e) {
              clickExport(e) 
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnPdfExport" runat="server" ToolTip="Export to Pdf" Image-Height="16px"
                                                                Image-Url="images/pdf_icon.png" Font-Size="8" UseSubmitBehavior="false" OnClick="btnPdfExport_Click">
                                                                <ClientSideEvents Click="function(s, e) {
              clickExport(e) 
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                            FocusedRowChanged="function(s, e) { onNdryshimFokusi(); }" SelectionChanged="function(s, e) { }"
                                            BeginCallback="function(s, e) {	BeginCallback(s,e); }" />
                                        <SettingsPager PageSize="15">
                                        </SettingsPager>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gvPajisje"
                                        ExportedRowType="Selected" />
                                    <br />
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Informacion" Text="Informacion">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl3" runat="server">
                                    <table id="tblInformacion" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server" ClientInstanceName="lblKodi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" ClientInstanceName="txtKodi">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi" ID="lblEmertimi" runat="server" ClientInstanceName="lblEmertimi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtEmertimi" runat="server" Width="100%" ClientInstanceName="txtEmertimi">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtFjalekalimi" ID="lblFjalekalimi" runat="server" ClientInstanceName="lblFjalekalimi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtFjalekalimi" runat="server" Width="100%" ClientInstanceName="txtFjalekalimi" Password="true">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKoeficentKonsumi" ID="lblKoeficentKonsumi" runat="server" ClientInstanceName="lblKoeficentKonsumi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtKoeficentKonsumi" runat="server" Width="100%" ClientInstanceName="txtKoeficentKonsumi">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKoeficentFitimi" ID="lblKoeficentFitimi" runat="server" ClientInstanceName="lblKoeficentFitimi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtKoeficentFitimi" runat="server" Width="100%" ClientInstanceName="txtKoeficentFitimi">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKlienti" ID="lblKlienti" runat="server" ClientInstanceName="lblKlienti">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="btneKlienti" runat="server" ClientInstanceName="btneKlienti"
                                        Width="100%" ShowShadow="False" ValueType="System.Int32" EnableClientSideAPI="True"
                                        IncrementalFilteringMode="Contains" EnableSynchronization="True" EnableCallbackMode="True"
                                        DropDownRows="3" CallbackPageSize="3" OnItemRequestedByValue="btneKlienti_ItemRequestedByValue"
                                        OnItemsRequestedByFilterCondition="btneKlienti_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents ButtonClick="function(s, e) { Klienti_Click(); }" TextChanged="function(s, e) {textChangedKlienti(s,e);}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="false" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojKonvertimesh" ID="lblLlojKonvertimesh" runat="server" ClientInstanceName="lblLlojKonvertimesh">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbLlojKonvertimesh" runat="server" ClientInstanceName="cmbLlojKonvertimesh"
                                        Width="100%" ShowShadow="False" ValueType="System.Int32" EnableClientSideAPI="True"
                                        IncrementalFilteringMode="Contains" EnableSynchronization="True" EnableCallbackMode="True"
                                        DropDownRows="3" CallbackPageSize="3" SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents ButtonClick="function(s, e) { Klienti_Click(); }" TextChanged="function(s, e) {textChangedKlienti(s,e);}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="false" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbAktiv" ID="lblAktiv" runat="server" ClientInstanceName="lblAktiv">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbAktiv" runat="server" ClientInstanceName="cbAktiv" Checked="false"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbAprovuar" ID="lblAprovuar" runat="server" ClientInstanceName="lblAprovuar">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbAprovuar" runat="server" ClientInstanceName="cbAprovuar" Checked="false"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                    <ClientSideEvents ActiveTabChanged="function(s, e) { tabsActiveTabChanged(s,e);}" />
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                </dxtc:ASPxPageControl >
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <asp:HiddenField ID="hfLupaKlienti" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            <dx:ASPxHiddenField ID="hfNrAutoKF" runat="server" ClientInstanceName="hfNrAutoKF">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                        CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                        EnableAnimation="False" PopupVerticalAlign="WindowCenter" AllowResize="True"
                        AppearAfter="10" ClientIDMode="AutoID" Height="400px">
                        <ClientSideEvents Closing="function(s, e) {	popupUniversal.SetContentUrl(''); }" />
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
        </div>
    </form>
</body>
</html>
