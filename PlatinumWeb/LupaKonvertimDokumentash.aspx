<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaKonvertimDokumentash.aspx.cs"
    Inherits="PlatinumWeb.LupaKonvertimDokumentash" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

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
    <title>AVEC Accounting</title>
    <style type="text/css">
        .style1
        {
            height: 54px;
        }
    </style>
  <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
  <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/JsGlobal.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaKonvertimDokumentash.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">
           
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
    </dx:ASPxGlobalEvents>
    <div id='div' style="display: none">
    <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled">
    </dx:ASPxHiddenField>
        <asp:HiddenField ID="hfNiveli" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaGjitheDok" runat="server" />
        <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" HeaderText="Zgjidh Llogarine"
            ClientInstanceName="panel" Width="100%" ShowHeader="False">
            <PanelCollection>
                <dx:PanelContent>
                    <%-- shtuar--%>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                   >
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
                    <asp:UpdatePanel ID="pnlKryesor" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lblPeriudha" runat="server" Text="Periudha">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxRadioButton ID="rbAktuale" runat="server" Text="Aktuale" AutoPostBack="false"
                                            ClientInstanceName="rbAktuale">
                                            <ClientSideEvents CheckedChanged="CheckedChanged_rbAktuale" />
                                        </dx:ASPxRadioButton>
                                    </td>
                                    <td>
                                        <dx:ASPxRadioButton ID="rbVitiUshtrimor" runat="server" AutoPostBack="false" Text="Viti Ushtrimor"
                                            ClientInstanceName="rbVitiUshtrimor">
                                            <ClientSideEvents CheckedChanged="CheckedChanged_rbVitiUshtrimor" />
                                        </dx:ASPxRadioButton>
                                    </td>
                                    <td>
                                        <dx:ASPxRadioButton ID="rbNgaDeri" runat="server" Text="Nga" AutoPostBack="false"
                                            ClientInstanceName="rbNga">
                                            <ClientSideEvents CheckedChanged="CheckedChanged_rbNga" />
                                        </dx:ASPxRadioButton>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="dteNga" runat="server" ClientInstanceName="dteNga" ShowShadow="False"
                                            Width="80%" ClientSideEvents-DateChanged=" buttonClick();">
                                            <ValidationSettings>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ClientSideEvents DateChanged="function(s, e) {
	
buttonClick();	

}" />
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
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lblDeri" runat="server" Text="Deri" ClientInstanceName="lblDeri">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="dteDeri" runat="server" ClientInstanceName="dteDeri" ShowShadow="False"
                                            Width="80%" ClientSideEvents-DateChanged=" buttonClick();">
                                            <ValidationSettings>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ClientSideEvents DateChanged="function(s, e) { buttonClick(); } "></ClientSideEvents>
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
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnKerko" runat="server" Text="Kerko" AutoPostBack="false" ClientInstanceName="btnKerko">
                                            <ClientSideEvents Click="function(s, e) {
	buttonClick();
}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <%-- shtuar--%>
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxGridView ID="gvLupaKonvDok" runat="server" ClientInstanceName="gvLupaKonvDok"
                                            OnDataBound="gvLupaKonvDok_DataBound" OnAfterPerformCallback="gvLupaKonvDok_AfterPerformCallback"
                                            Width="100%" OnCustomCallback="gvLupaKonvDok_CustomCallback" OnCustomJSProperties="gvLupaKonvDok_CustomJSProperties">
                                            <ClientSideEvents RowDblClick="function(s, e) {
                                                OnGridSelectionChanged();
                                                }" BeginCallback="function(s, e) { BeginCallback(s,e); }" />
                                            <Styles>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                            <StylesEditors>
                                                <ProgressBar Height="25px">
                                                </ProgressBar>
                                            </StylesEditors>
                                            <SettingsPager></SettingsPager>
                                        </dx:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                            <%--shtuar--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--shtuar--%>
                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None"
                            ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top" ClientVisible ="false">
                        </dx:ASPxComboBox>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel >
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Zgjidh KF "
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <iframe id="Container" name="ContainerKF" frameborder="0" runat="server"></iframe>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
