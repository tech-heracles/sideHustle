<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMLupaDetyra.aspx.cs" Inherits="PlatinumWeb.CRMLupaDetyra" %>

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
    <title>Alpha Web</title>
    <style type="text/css">
        .style1 {
            height: 58px;
        }
    </style>
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
     <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/CRMLupaDetyra.aspx-IMB.5.1.js&v49"
        type="text/javascript">
    </script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">
          
        <asp:ScriptManager ID="ScriptManager1" runat="server">
          
        </asp:ScriptManager>
        <div id='div' style="display: none">
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server"></dx:ASPxHiddenField>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            </dx:ASPxGlobalEvents>
            <asp:HiddenField ID="hfKontrollet" runat="server" />           
            <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" ClientInstanceName="panel"
                Width="100%" HeaderText="Zgjidh Detyren" ShowHeader="False">
                <%-- Width="350px"--%>
                <PanelCollection>
                    <dx:PanelContent>
                        <%-- shtuar--%>
                        <asp:UpdatePanel ID="pnlKryesor" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound" OnItemClick="ASPxMenu1_ItemClick"
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
                                <%-- shtuar--%>

                                <table width="100%">
                                    <tr>
                                        <td>
                                            <dx:ASPxGridView ID="gvLupaDetyrat" runat="server" ClientInstanceName="gvLupaDetyrat"
                                                OnDataBound="gvLupaDetyrat_DataBound" OnAfterPerformCallback="gvLupaDetyrat_AfterPerformCallback"
                                                OnHeaderFilterFillItems="gvLupaDetyrat_HeaderFilterFillItems" Width="100%"
                                                OnCustomCallback="gvLupaDetyrat_CustomCallback">
                                                <ClientSideEvents RowDblClick="function(s, e) { OnGridSelectionChanged(); }" BeginCallback="function(s, e) { BeginCallback(s,e); }" />
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
                                    <tr>
                                    </tr>
                                </table>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--shtuar--%>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel >
        </div>
        
    </form>
</body>
</html>
