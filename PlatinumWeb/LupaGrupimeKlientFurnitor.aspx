<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaGrupimeKlientFurnitor.aspx.cs"
    Inherits="PlatinumWeb.LupaGrupimeKlientFurnitor" %>
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
<head runat="server">
    <title>AVEC Accounting</title>
    <style type="text/css">
        .style1
        {
            height: 58px;
        }
    </style>
  <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
  <meta name="viewport" content="width=device-width,initial-scale=1.0" />
        <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaGrupimeKlientFurnitor.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">
          
    <asp:ScriptManager ID="ScriptManager1" runat="server">
      
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
    </dx:ASPxGlobalEvents>
         <dx:ASPxHiddenField ID="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
    <div id='div' style="display: none">
        <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" ClientInstanceName="panel"
            Width="100%" Height="100%" HeaderText="Zgjidh Kodifikimin" Font-Bold="True"  
                
            ShowHeader="False">
         
            <Border BorderColor="#D7D7D7" BorderStyle="Solid" BorderWidth="1px" />
            
           
            <BorderBottom BorderWidth="0px" />
            <ContentPaddings PaddingBottom="3px" PaddingLeft="3px" PaddingRight="3px" PaddingTop="3px" />
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
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <dx:ASPxMenu ID="ASPxMenu2" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                            BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                            <ItemStyle   HorizontalAlign="Left" />
                                            <SubMenuStyle GutterWidth="17px" />
                                        </dx:ASPxMenu>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="pnlKryesor" runat="server">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                        BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                        <ItemStyle   HorizontalAlign="Left" />
                                        <SubMenuStyle GutterWidth="17px" />
                                    </dx:ASPxMenu>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%-- shtuar--%>
                            <br />
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dx:ASPxGridView ID="gvLupaGrupKlientFurnitor" runat="server" ClientInstanceName="gvLupaGrupKlientFurnitor"
                                            Width="100%"    
                                            OnAfterPerformCallback="gvLupaGrupKlientFurnitor_AfterPerformCallback" OnHeaderFilterFillItems="gvLupaGrupKlientFurnitor_HeaderFilterFillItems"
                                            OnDataBound="gvLupaGrupKlientFurnitor_DataBound" OnCustomCallback="gvLupaGrupKlientFurnitor_CustomCallback">
                                            
                                            <StylesPager Summary-Width="100%" PageNumber-Width="100%">
                                                <PageNumber Width="100%">
                                                </PageNumber>
                                                <Summary Width="100%">
                                                </Summary>
                                            </StylesPager>
                                            <ClientSideEvents RowDblClick="function(s, e) { zgjidhElement(); }" 
                                                              BeginCallback="function(s, e) { BeginCallback(s, e); }" />
                                            <Styles>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                            <StylesEditors>
                                                <ProgressBar Height="100%">
                                                </ProgressBar>
                                            </StylesEditors>
                                            <SettingsPager></SettingsPager>
                                        </dx:ASPxGridView>
                                        <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Green" ClientInstanceName="pergjigja">
                                        </dx:ASPxLabel>
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
