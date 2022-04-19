<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaKonfigurimInfo.aspx.cs"
    Inherits="PlatinumWeb.LupaKonfigurimInfo" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <style type="text/css">
        .style1
        {
            height: 54px;
        }
    </style>
  <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
  <meta name="viewport" content="width=device-width,initial-scale=1.0" />
        <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaKonfigurimInfo.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">           
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <ClientSideEvents EndCallback="function(s,e){ }"
            ControlsInitialized="function(s, e) { UpdateButtonState(); }" />
    </dx:ASPxGlobalEvents>
    <div id='div' style="display: none">
        <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" ClientInstanceName="panel"
            Width="100%" HeaderText="Zgjidh Konfigurimin" ShowHeader="False">
            <PanelCollection>
                <dx:PanelContent>
                    <asp:UpdatePanel ID="pnlKryesor" runat="server">
                        <ContentTemplate>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" Width="100%" AutoPostBack="false" ItemAutoWidth="False"
                                OnItemClick="ASPxMenu1_ItemClick">
                                <ClientSideEvents ItemClick="function(s, e) { 
                                                 menu_click(e);
	                  }" Init="function(s) {s.SetClientVisible(true);}" />
                            </dx:ASPxMenu>
                            <%-- shtuar--%>
                            <table width="100%">
                                <tr>
                                    <td style="width: 40%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lblFushat" runat="server" Text="Fushat">
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxListBox ID="lbxFushat" runat="server" Width="100%" Height="300px" EnableSynchronization ="True" ClientInstanceName="lbxFushat"
                                                            SettingsLoadingPanel-ImagePosition="Top"
                                                         >
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }" />
                                                        <LoadingPanelImage  >
                                                        </LoadingPanelImage>
                                                        <ValidationSettings>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </dx:ASPxListBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 5%">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnDjathtas1" runat="server" Text="&gt;"  
                                                        AutoPostBack="false"    
                                                        ClientInstanceName="btnDjathtas1" Width="50px">
                                                        <ClientSideEvents Click="function (s,e){ Kalo1(lbxFushat,lbxZgjedhur);}" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnDjathtasGjitha" runat="server" Text="&gt;&gt;"  
                                                            AutoPostBack="false"
                                                        ClientInstanceName="btnDjathtasGjitha" Width="50px">
                                                        <ClientSideEvents Click="function (s,e){ TeGjithe(lbxFushat,lbxZgjedhur);}" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnMajtas1" runat="server" Text="&lt;"  
                                                            AutoPostBack="false"
                                                        ClientInstanceName="btnMajtas1" Width="50px">
                                                        <ClientSideEvents Click="function (s,e){ Kalo1(lbxZgjedhur,lbxFushat);}" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnMajtaGjitha" runat="server" Text="&lt;&lt;"  
                                                            AutoPostBack="false"
                                                        ClientInstanceName="btnMajtaGjitha" Width="50px">
                                                        <ClientSideEvents Click="function (s,e){ TeGjithe(lbxZgjedhur,lbxFushat);}" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 40%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lblZgjedhur" runat="server" Text="Fushat e Zgjedhura">
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxListBox ID="lbxZgjedhur" runat="server" Width="100%" Height="300px" EnableSynchronization ="True" ClientInstanceName="lbxZgjedhur"
                                                            SettingsLoadingPanel-ImagePosition="Top"
                                                         >
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }" />
                                                        <LoadingPanelImage  >
                                                        </LoadingPanelImage>
                                                        <ValidationSettings>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </dx:ASPxListBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 5%">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnSiper" runat="server" Text="Lart"  
                                                        AutoPostBack="false" ClientInstanceName="btnSiper"    
                                                        Width="70px">
                                                        <ClientSideEvents Click="function (s,e){ LevizSiper(lbxZgjedhur);}" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnPoshte" runat="server" Text="Poshte"  
                                                        AutoPostBack="false" ClientInstanceName="btnPoshte"    
                                                        Width="70px">
                                                        <ClientSideEvents Click="function (s,e){ LevizPoshte(lbxZgjedhur);}" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel >
    </div>
    </form>
</body>
</html>
