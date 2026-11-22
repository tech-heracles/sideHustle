<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaNivelCmimi.aspx.cs"
    Inherits="PlatinumWeb.LupaNivelCmimi" %>
<%@ Register TagPrefix="avec" Namespace="PlatinumWeb" Assembly="PlatinumWeb" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
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
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaNivelCmimi.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
           
        <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" runat="server">
        </dx:ASPxHiddenField>
        <div id='div' style="display: none">
         
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
            <asp:UpdatePanel ID="pnlKryesor" runat="server">
                <ContentTemplate>
                    <dx:ASPxGridView ID="gvLupaNivCm" runat="server" ClientInstanceName="gvLupaNivCm"
                        OnDataBound="gvLupaNivCm_DataBound" OnAfterPerformCallback="gvLupaNivCm_AfterPerformCallback"
                        Width="100%" OnCustomCallback="gvLupaNivCm_CustomCallback" SettingsDataSecurity-AllowReadUnlistedFieldsFromClientApi="True">
                        <SettingsPager></SettingsPager>
                        <StylesPager Summary-Width="100%" PageNumber-Width="100%">
                            <PageNumber Width="100%">
                            </PageNumber>
                            <Summary Width="100%">
                            </Summary>
                        </StylesPager>
                        <ClientSideEvents RowDblClick="function(s, e) { OnGridSelectionChanged(); }" BeginCallback="function(s, e) { BeginCallback(s,e); }" />
                        <Styles>
                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                            </Header>
                        </Styles>
                        <StylesEditors>
                            <ProgressBar Height="100%">
                            </ProgressBar>
                        </StylesEditors>
                    </dx:ASPxGridView>
                </ContentTemplate>
            </asp:UpdatePanel>
                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None"
                            ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top" ClientVisible ="false">
                        </dx:ASPxComboBox>
        </div>
    </form>
</body>
</html>
