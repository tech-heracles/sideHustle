<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegjistrimDokumentash.aspx.cs"
    Inherits="PlatinumWeb.RegjistrimDokumentash" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>


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

<%@ Register Src="~/ucPopUpKlonimi.ascx" TagPrefix="ucPopUpKlonimi" TagName="ucPopUpKlonimi" %>
<%@ Register Src="~/ucPopUpEinvoice.ascx" TagPrefix="ucPopUpEinvoice" TagName="ucPopUpEinvoice" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="DX.ashx?cssfile=~/AlphaWeb.css" rel="stylesheet" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myCookies-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/RegjistrimDokumentash.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <dx:ASPxButton class="btn" onclick="shto_dok()">+
        <ClientSideEvents click="shto_dok(s, e)" />
    </dx:ASPxButton>
<style>
.btn {
  height: 50px;
  width: 50px;
  line-height: 50px;
  font-size: 2em;
  border-radius: 50%;
  background-color: #0072c6;
  color: white;
  text-align: center;
  border: none;
  cursor: pointer;
  position: fixed;
  z-index: 1;
  bottom: 5%;
  right: 2%;
  box-shadow: 0 3px 5px -1px rgba(0, 0, 0, 0.2), 0 6px 10px 0 rgba(0, 0, 0, 0.14), 0 1px 18px 0 rgba(0, 0, 0, 0.12);
}
</style>
    <form id="form1" runat="server">

        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
        </dx:ASPxHiddenField>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaGjitheDok" runat="server" />
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                <ClientSideEvents ItemClick="function(s, e) {
	                        menu_click(s,e);
                            }"
                                    Init="function(s) {s.SetClientVisible(true);}" />
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
                                        <dx:ASPxMenu ID="MenuInfo" BackColor="transparent" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                            BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                            <ClientSideEvents Init="function(s,e){$('#dvMenu').show();myMesazh.InicializoTimer();}" />
                                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                            <SubMenuStyle GutterWidth="17px" />
                                        </dx:ASPxMenu>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                </table>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popKonvertim" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                    ClientInstanceName="popKonvertim" CloseAction="CloseButton" CssPostfix="Glass"
                    EnableAnimation="False" EnableViewState="False" Font-Bold="true" HeaderText="Kujdes"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    Width="400px">
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl14" runat="server">
                            <dxp:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel11" runat="server" ClientIDMode="AutoID" Width="600px">
                                <PanelCollection>
                                    <dxp:PanelContent ID="PanelContent11" runat="server" SupportsDisabledAttribute="True">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lblKonvertoNe" runat="server" ClientIDMode="AutoID" Text="Konverto Ne:"
                                                        Wrap="False">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxComboBox ID="cmbKonverto" runat="server" ClientInstanceName="cmbKonverto">

                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ndryshoNiveli(s,e);
}" />

                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="lblKonfig" runat="server" ClientIDMode="AutoID" Text="Lloji:"
                                                        Wrap="False">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxComboBox ID="cmbKonf" runat="server" ClientInstanceName="cmbKonf">
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="ButtonOk2" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk2"
                                                        Text="Konverto">
                                                        <ClientSideEvents Click="function(s, e) {   konverto();
	popKonvertim.Hide();

}" />
                                                    </dx:ASPxButton>
                                                </td>

                                            </tr>
                                        </table>
                                        </div>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <ucPopUpKlonimi:ucPopUpKlonimi runat="server" ID="ucPopUpKlonimi"/>
                <ucPopUpEinvoice:ucPopUpEinvoice runat="server" ID="ucPopUpEinvoice"/>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table class="renditKontrolle">
            <tbody>
                <tr>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                            runat="server" ClientIDMode="AutoID" Text="Modeli:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth33">
                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None"
                            ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top">
                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                            <LoadingPanelImage>
                            </LoadingPanelImage>
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
                        <dx:ASPxLabel ID="lblKonfigurimi" runat="server" Text="" BackColor="white" ClientInstanceName="lblKonfigurimi">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth33"></td>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="pnlKryesor" runat="server">
            <ContentTemplate>
                <div style="visibility: hidden">
                    <dx:ASPxLabel ID="pergjigja" runat="server" Text="" EncodeHtml="False" ForeColor="Green"
                        ClientVisible="false">
                    </dx:ASPxLabel>
                </div>
                <asp:HiddenField ID="hfVeprimi" runat="server" />
                <dx:ASPxHiddenField ID="hfKushte" runat="server" ClientInstanceName="hfKushte" SyncWithServer="true" ViewStateMode="Enabled">
                </dx:ASPxHiddenField>
                <br />
                <dx:ASPxGridView ID="grid_RegDok" ClientInstanceName="grid_RegDok" runat="server"
                    Width="100%" OnDataBound="grid_RegDok_DataBound" OnAfterPerformCallback="grid_RegDok_AfterPerformCallback"
                    OnCustomCallback="grid_RegDok_CustomCallback" OnCustomJSProperties="grid_RegDok_CustomJSProperties"
                    OnHeaderFilterFillItems="grid_RegDok_HeaderFilterFillItems" OnHtmlDataCellPrepared="grid_RegDok_HtmlDataCellPrepared"
                    OnHtmlRowCreated="grid_RegDok_HtmlRowCreated"  OnProcessColumnAutoFilter="grid_RegDok_ProcessColumnAutoFilter" SettingsPager-PageSize="20">
                    <Templates>
                    </Templates>
                    <Styles>
                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                        </Header>
                    </Styles>
                    <SettingsPager PageSize="15">
                    </SettingsPager>
                    <ClientSideEvents RowDblClick="function(s, e) { OnGridDoubleClick(s,e,e.visibleIndex); }"
                        BeginCallback="function(s, e) {	BeginCallback(s,e); }" />
                    <StylesEditors>
                        <ProgressBar Height="25px">
                        </ProgressBar>
                    </StylesEditors>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_RegDok"
                    ExportedRowType="Selected" />
                <iframe id="Container" runat="server" frameborder="0" name="Container" height="0"
                    width="0"></iframe>
                <iframe id="Container1" runat="server" frameborder="0" name="Container1" height="0"
                    width="0"></iframe>
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxButton ID="btnPdfExportHidden" ClientVisible="False" ClientInstanceName="btnPdfExportHidden"
            runat="server" ToolTip="Export to Pdf" Text="Export to Pdf" Font-Size="8pt" UseSubmitBehavior="False"
            OnClick="btnPdfExport_Click_Hidden">
            <ClientSideEvents Click="function(s, e) {
              clickExport(e) 
}" />
        </dx:ASPxButton>

        <dx:ASPxButton ID="btnXlsxExportHidden" ClientVisible="False" ClientInstanceName="btnXlsxExportHidden"
            runat="server" ToolTip="Export to Xlsx" Text="Export to Xlsx" Font-Size="8" UseSubmitBehavior="false"
            OnClick="btnXlsxExport_Click_Hidden">
            <ClientSideEvents Click="function(s, e) {
              clickExport(e) 
}" />
        </dx:ASPxButton>

        <div>
            <asp:UpdatePanel ID="UpdatePanel9" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                        CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" AllowResize="True" AppearAfter="10" ClientIDMode="AutoID"
                        AutoUpdatePosition="True" Font-Bold="False">
                        <ClientSideEvents CloseUp="function(s, e) {  }" />
                        <ContentStyle>
                            <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                                PaddingTop="1px" />
                        </ContentStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
