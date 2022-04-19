<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VeprimeBanka.aspx.cs" Inherits="PlatinumWeb.VeprimeBanka" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/aspx.js/VeprimeBanka.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body onload="changeName()">
    <form id="form1" runat="server">
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30"></LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="6000000"></asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server"></dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState" ViewStateMode="Enabled"></dx:ASPxHiddenField>
        <asp:HiddenField ID="hfURL" runat="server" />
         
       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 <asp:HiddenField ID="fshi" runat="server" />
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                <asp:HiddenField ID="hfObjektRuajtur" runat="server" />
                <asp:HiddenField ID="vjennga" runat="server" />
                <table style="width: 100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound" ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True" OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                <ClientSideEvents ItemClick="function(s, e) {menu_click(s,e);}" Init="function(s) {s.SetClientVisible(true);}" />
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
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientInstanceName="popFshi" CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowHeader="true" Width="300px" Enabled="True">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" Width="200px">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxLabel ID="lblMsgbox" ClientInstanceName="lblMsgbox" runat="server" Text="Jeni i sigurt?"></dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <div style="text-align: right;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonOk" runat="server" Text="Ok" CausesValidation="False" OnClick="ButtonOk_Click2">
                                                            <ClientSideEvents Click="function(s, e) {popFshi.Hide();Utils.shfaqLoadingGif();}" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonCancel" runat="server" Text="Anullo">
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
                <dx:ASPxPopupControl ID="popCancel" runat="server" AllowDragging="True" ClientInstanceName="popCancel" CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowHeader="true" Width="300px" Enabled="True" EnableHierarchyRecreation="False">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl114" runat="server">
                            <dx:ASPxPanel ID="ASPxPanel111" runat="server" Width="200px">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxLabel ID="lblMsgbox1" ClientInstanceName="lblMsgbox1" runat="server" Text="Jeni i sigurt?"></dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <div style="text-align: right;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonOk1" runat="server" Text="Ok" CausesValidation="False">
                                                            <ClientSideEvents Click="function(s, e) {myFaqeCelje.kontrolloTeDrejta($('#hfURL').val());popCancel.Hide();}" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonCancel2" runat="server" Text="Anullo">
                                                            <ClientSideEvents Click="function(s, e) {popCancel.Hide();}" />
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
        <table class="renditKontrolle">
            <tbody>
                <tr>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label" runat="server" ClientIDMode="AutoID" Text="Modeli:"></dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth33">
                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None" ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top">
                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
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
                    </td>
                    <td class="renditKontrolleLabelMeWidth33">
                        <dx:ASPxLabel ID="lblKonfigurimi" runat="server" Text="" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi"></dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth33"></td>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="pnlKryesor" runat="server">
            <ContentTemplate>
                <dx:ASPxLabel ID="pergjigja" runat="server" Text="" EncodeHtml="False" ForeColor="Green"></dx:ASPxLabel>
                <br />
                <dx:ASPxGridView ID="grid_veprimeBankaKoka" ClientInstanceName="grid_veprimeBankaKoka" runat="server" Width="100%" OnDataBound="grid_veprimeBankaKoka_DataBound" OnAfterPerformCallback="grid_veprimeBankaKoka_AfterPerformCallback" OnCustomCallback="grid_veprimeBankaKoka_CustomCallback" OnCustomJSProperties="grid_veprimeBankaKoka_CustomJSProperties" OnProcessColumnAutoFilter="grid_veprimeBankaKoka_ProcessColumnAutoFilter" OnHeaderFilterFillItems="grid_veprimeBankaKoka_HeaderFilterFillItems" SettingsBehavior-SortMode="Custom" OnCustomColumnSort="grid_veprimeBankaKoka_CustomColumnSort">
                    <Templates>
                        <TitlePanel></TitlePanel>
                    </Templates>
                    <Styles>
                        <Header ImageSpacing="5px" SortingImageSpacing="5px"></Header>
                    </Styles>
                    <SettingsPager PageSize="15"></SettingsPager>
                    <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e,e.visibleIndex); }"
                        FocusedRowChanged="function(s,e){mbushfusha(e);}" BeginCallback="function(s, e) {BeginCallback(s,e);}" />
                    <StylesEditors>
                        <ProgressBar Height="25px"></ProgressBar>
                    </StylesEditors>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_veprimeBankaKoka" ExportedRowType="Selected" />
                <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva"></dx:ASPxHiddenField>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <ContentTemplate>
                            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal" CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowResize="True" AppearAfter="10" ClientIDMode="AutoID" AutoUpdatePosition="True" Font-Bold="False">
                                <ContentStyle>
                                    <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px" PaddingTop="1px" />
                                </ContentStyle>
                                <ContentCollection>
                                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server"></dx:PopupControlContentControl>
                                </ContentCollection>
                            </dx:ASPxPopupControl>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <iframe id="Container" runat="server" frameborder="0" height="0" name="Container" width="0"></iframe>
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxButton ID="btnPdfExportHidden" ClientVisible="False" ClientInstanceName="btnPdfExportHidden" runat="server" ToolTip="Export to Pdf" Text="Export to Pdf" Font-Size="8pt" UseSubmitBehavior="False" OnClick="btnPdfExport_Click">
            <ClientSideEvents Click="function(s, e) { clickExport(e) }" />
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnXlsxExportHidden" ClientVisible="False" ClientInstanceName="btnXlsxExportHidden" runat="server" ToolTip="Export to Xlsx" Text="Export to Xlsx" Font-Size="8" UseSubmitBehavior="false" OnClick="btnXlsxExport_Click">
            <ClientSideEvents Click="function(s, e) { clickExport(e) }" />
        </dx:ASPxButton>
    </form>
</body>
</html>
