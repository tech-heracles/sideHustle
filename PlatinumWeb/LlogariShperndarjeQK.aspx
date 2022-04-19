<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LlogariShperndarjeQK.aspx.cs" Inherits="PlatinumWeb.LlogariShperndarjeQK" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxw" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LlogariShperndarjeQK.aspx-IMB.3.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server"></dx:ASPxGlobalEvents>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30"></LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" runat="server" AutoPostBack="true" ClientInstanceName="ASPxMenu1" ItemImagePosition="Top" OnDataBound="ASPxMenu1_DataBound" OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px" ShowPopOutImages="True" Width="100%">
                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                    <ClientSideEvents Init="function(s) {s.SetClientVisible(true);}" />
                                    <ItemImage Height="32px" Width="32px"> </ItemImage>
                                    <SubMenuItemImage Height="16px" Width="16px"> </SubMenuItemImage>
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
                                        <dx:ASPxMenu ID="MenuInfo" runat="server" BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ClientInstanceName="MenuInfo" ShowPopOutImages="True" Width="100%">
                                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" /><ItemStyle HorizontalAlign="Left" />
                                            <SubMenuStyle GutterWidth="17px" />
                                        </dx:ASPxMenu>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="div">
                <asp:UpdatePanel ID="pnlTree" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lblLlog" runat="server" Text="Llogari që shpërndahen në Qendra Kostoje" Font-Bold="true"></dx:ASPxLabel>
                                </td>
                                <td></td>
                                <td>
                                    <dx:ASPxLabel ID="lblLlogMund" runat="server" Text="Llogari të mundshme" Font-Bold="true"></dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 40%">
                                    <dxp:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel2" runat="server" Width="100%" ScrollBars="Vertical" Height="500px">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxTreeList ID="trlStruktura" runat="server" ClientInstanceName="trlStruktura" Width="100%">
                                                    <Styles>
                                                        <CustomizationWindowContent VerticalAlign="Top"></CustomizationWindowContent>
                                                    </Styles>
                                                </dx:ASPxTreeList>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                </td>
                                <td style="width: 5%">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnMajtas1" runat="server" Text="&lt;" ClientInstanceName="btnMajtas1" Width="50px" OnClick="btnMajtas1_Click"></dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnDjathtas1" runat="server" Text="&gt;" OnClick="btnDjathtas1_Click" ClientInstanceName="btnDjathtas1" Width="50px"></dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 40%">
                                    <dxp:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" Width="100%" ScrollBars="Vertical" Height="500px">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxTreeList ID="trlStruktura2" runat="server" ClientInstanceName="trlStruktura2" Width="100%">
                                                    <Styles>
                                                        <CustomizationWindowContent VerticalAlign="Top"></CustomizationWindowContent>
                                                    </Styles>
                                                </dx:ASPxTreeList>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>
