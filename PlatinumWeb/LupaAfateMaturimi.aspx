<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaAfateMaturimi.aspx.cs"
    Inherits="PlatinumWeb.LupaAfateMaturimi" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ucMenuAndMsgFrame.ascx" TagPrefix="ucMenu" TagName="ucMenuAndMsgFrame" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <style type="text/css">
        .style1 {
            height: 54px;
        }
    </style>
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaAfateMaturimi.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id='div' style="display: none">
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            </dx:ASPxGlobalEvents>
            <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" HeaderText="Zgjidh Llogarine"
                ClientInstanceName="panel" Width="100%" ShowHeader="False">
                <PanelCollection>
                    <dx:PanelContent>
                        <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" runat="server" OnMenuClick="Menu_ItemClick" OnMenuTemplate="PercaktoTemplateMenu" OnFilterSave="Ruaj_ASPxButton_Click" OnFilterDelete="FshiFilter_ASPxButton_Click"/>
                        <asp:UpdatePanel ID="pnlKryesor" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <dx:ASPxGridView ID="gvLupaAfatMaturimi" runat="server" ClientInstanceName="gvLupaAfatMaturimi"
                                                OnDataBound="gvLupaAfatMaturimi_DataBound" OnAfterPerformCallback="gvLupaAfatMaturimi_AfterPerformCallback"
                                                Width="100%"
                                                OnCustomCallback="gvLupaAfatMaturimi_CustomCallback">
                                                <ClientSideEvents RowDblClick="function(s, e) {OnGridSelectionChanged();}"
                                                    BeginCallback="function(s, e) {BeginCallback(s,e);}" />
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None"
                            ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top" ClientVisible="false">
                        </dx:ASPxComboBox>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
        </div>
    </form>
</body>
</html>
