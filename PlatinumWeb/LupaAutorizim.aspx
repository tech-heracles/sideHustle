<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaAutorizim.aspx.cs"
    Inherits="PlatinumWeb.LupaAutorizim" %>
<%@ Register TagPrefix="avec" Namespace="PlatinumWeb" Assembly="PlatinumWeb" %>

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
            height: 58px;
        }
    </style>
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaAutorizim.aspx-IMB.2.1.js&v76""" type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">
           
    <asp:ScriptManager ID="ScriptManager1" runat="server">
      
    </asp:ScriptManager>
    <div id='div' style="display: none">
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server"></dx:ASPxHiddenField>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
        <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" ClientInstanceName="panel"
            Width="100%" HeaderText="Zgjidh Nivelin e autorizimeve" ShowHeader="False">
            <%-- Width="350px"--%>
            <PanelCollection>
                <dx:PanelContent>
                    <%-- shtuar--%>
                    <table width="100%">
                        <tr>
                            <td>
                                <avec:AvecToolbar ID="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound" />
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
                    <asp:UpdatePanel ID="pnlKryesor" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dx:ASPxGridView ID="gvLupaAutorizim" runat="server" ClientInstanceName="gvLupaAutorizim"
                                            OnDataBound="gvLupaAutorizim_DataBound" OnAfterPerformCallback="gvLupaAutorizim_AfterPerformCallback"
                                            OnHeaderFilterFillItems="gvLupaAutorizim_HeaderFilterFillItems" Width="100%"
                                            OnCustomCallback="gvLupaAutorizim_CustomCallback">
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
    </div>
    </form>
</body>
</html>
