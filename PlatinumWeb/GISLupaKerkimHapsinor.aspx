<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GISLupaKerkimHapsinor.aspx.cs"
    Inherits="PlatinumWeb.GISLupaKerkimHapsinor" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>





<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<!DOCTYPE html>
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
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <style type="text/css">
        .style1 {
            height: 58px;
        }
    </style>
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/GISLupaKerkimHapsionr.aspx-IMB.5.3.3.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxHiddenField ID="hfState" runat="server">
        </dx:ASPxHiddenField>
        <div id='div' style="display: none">
            <%--     <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <ClientSideEvents EndCallback="function(s,e){ if(typeof(window.parent.window.parent.window.parent.SessionTimeout) != 'undefined')
                window.parent.window.parent.window.parent.SessionTimeout.sendKeepAlive();
                else
                window.parent.window.parent.SessionTimeout.sendKeepAlive();}" />
        </dx:ASPxGlobalEvents>--%>
            <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" ClientInstanceName="panel"
                Width="100%" Height="100%" HeaderText="Lupa e kerkimit" ShowHeader="False">
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

                        <table style="display: none;">
                            <tr>
                                <td>
                                    <dx:ASPxComboBox ID="cmbLayers" Width="250px" ValueField="IDLAYERSTYPE" TextField="Title" ClientInstanceName="cmbLayers" runat="server" ValueType="System.String">
                                        <ClientSideEvents SelectedIndexChanged="cmbLayerChanged" />
                                    </dx:ASPxComboBox>
                                </td>

                                <td>
                                    <dx:ASPxButton ID="btnKerko" runat="server" AutoPostBack="false" Text="Kerko">
                                        <ClientSideEvents Click="btnKerkoClick" />
                                    </dx:ASPxButton>
                                </td>

                                <td>
                                    <asp:UpdatePanel ID="pnlfiltri" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxComboBox ID="cmbFiltri" ClientInstanceName="cmbFiltri" runat="server" CssPostfix="Aqua"
                                                            ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top" Width="100%" DropDownStyle="DropDown">
                                                            <ClientSideEvents SelectedIndexChanged="SelectedIndexChanged_cmbFiltri"
                                                                KeyUp="myMenu.checkText" Init="myMenu.textChanged" />
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
                                                            <DisabledStyle Font-Bold="False">
                                                            </DisabledStyle>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnRuaj" runat="server" Text="" ClientInstanceName="btnRuaj"
                                                            Image-Url="~/images/new/disk_blue (3).png" OnClick="btnRuaj_Click" Width="10px">
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnFshi" runat="server" Text="" ClientInstanceName="btnFshi"
                                                            Image-Url="~/images/new/button_cancel-32.png" OnClick="btnFshi_Click" Width="10px">
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <dx:ASPxFilterControl ID="filterControll" runat="server"></dx:ASPxFilterControl>
                                </td>
                            </tr>
                        </table>

                        <dx:ASPxCallbackPanel EnableHierarchyRecreation="false" ID="callBackPanel" OnCallback="callBackPanel_Callback" ClientInstanceName="callBackPanel" runat="server" Width="100%">

                            <PanelCollection>

                                <dx:PanelContent>
                                    <dx:ASPxHiddenField runat="server" ClientInstanceName="hfGrida" ID="hfGrida"></dx:ASPxHiddenField>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <dx:ASPxGridView ID="gvLupaKerko" runat="server" ClientInstanceName="gvLupaKerko" SettingsBehavior-ColumnResizeMode="NextColumn"
                                                    OnDataBound="gvLupaKerko_DataBound"  ToolTip="Kerkimi hapsinor"
                                                    Width="100%">
                                                    <ClientSideEvents RowDblClick="OnGridSelectionChanged"  FocusedRowChanged="OnGridSelectionChanged" />
                                                    <%--
                                                    <ClientSideEvents EndCallback="function(s,e){callBackPanel.PerformCallback();}" />--%>
                                                    <Styles>
                                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                        </Header>
                                                    </Styles>
                                                    <StylesEditors>
                                                        <ProgressBar Height="25px">
                                                        </ProgressBar>
                                                    </StylesEditors>
                                                    <Settings ShowFilterRow="true"  />
                                                      <SettingsText FilterBarClear="Pastro filter" FilterBarCreateFilter="Krijo filter" EmptyDataRow="Nuk ka te dhena" />
                                                </dx:ASPxGridView>
                                                 
                                                <dx:ASPxLoadingPanel ID="LoadingPanel" ContainerElementID="callBackPanel" ClientInstanceName="LoadingPanel" runat="server"></dx:ASPxLoadingPanel>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel >
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel >
        </div>
    </form>
</body>
</html>