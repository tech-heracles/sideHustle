<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaNdermarjeBij.aspx.cs"
    Inherits="PlatinumWeb.LupaNdermarjeBij" %>

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
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaNdermarjeBij.aspx-IMB.4.0.js&v76""
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"  AsyncPostBackTimeout="360000">
    </asp:ScriptManager>
         
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
        Font-Size="9pt" Modal="True" ImagePosition="Top">
        <LoadingDivStyle Opacity="30">
        </LoadingDivStyle>
    </dx:ASPxLoadingPanel>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){  try { window.parent.window.parent.SessionTimeout.sendKeepAlive(); }
        catch (err) {
   
        }}" />--%>
    </dx:ASPxGlobalEvents>
    <div id='div' style="display: none">
        <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" ClientInstanceName="panel"
            Width="100%" HeaderText="Zgjidh Ndermarjen" ShowHeader="False">
            <PanelCollection>
                <dx:PanelContent>
                    <%-- shtuar--%>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                    OnItemClick="ASPxMenu1_ItemClick">
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
                    <asp:UpdatePanel ID="pnlKryesor" runat="server">
                        <ContentTemplate><asp:HiddenField ID="hfStatusi" runat="server" />
                            <asp:HiddenField ID="hfMesazhi" runat="server" />
                            <div style="visibility: hidden">
                                <dx:ASPxButton ID="ASPxButton1" runat="server" ClientInstanceName="btn" Text="ASPxButton"
                                    Height="0px" OnClick="ASPxButton1_Click">
                                </dx:ASPxButton>
                            </div>
                           
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dx:ASPxGridView ID="gvLupaNdermarje" runat="server" ClientInstanceName="gvLupaNdermarje"
                                            OnDataBound="gvLupaNdermarje_DataBound" OnCustomJSProperties="gvLupaNdermarje_CustomJSProperties"
                                            Width="100%" OnCustomCallback="gvLupaNdermarje_CustomCallback">
                                            <Templates>
                                                <TitlePanel>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton ID="gridaSelectFaqe" runat="server" ToolTip="Zgjidh te gjithe faqen"
                                                                    AutoPostBack="false" Image-Url="images/check2.png" Image-Height="16px" Font-Size="8"
                                                                    UseSubmitBehavior="false">
                                                                    <ClientSideEvents Click="function(s, e) { gvLupaNdermarje.SelectAllRowsOnPage(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe"
                                                                    AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8"
                                                                    UseSubmitBehavior="false">
                                                                    <ClientSideEvents Click="function(s, e) { gvLupaNdermarje.SelectRows(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="gridaUnSelectTeGjitha" runat="server" ToolTip="Fshi Zgjedhjen"
                                                                    AutoPostBack="false" Image-Url="images/uncheck2.png" Image-Height="16px" Font-Size="8"
                                                                    UseSubmitBehavior="false">
                                                                    <ClientSideEvents Click="function(s, e) { gvLupaNdermarje.UnselectRows(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </TitlePanel>
                                            </Templates>
                                            <ClientSideEvents RowDblClick="function(s, e) {OnGridSelectionChanged()  }" BeginCallback="function(s, e) { BeginCallback(s,e); }">
                                            </ClientSideEvents>
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
                            ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top" ClientVisible ="false">
                        </dx:ASPxComboBox>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel ><asp:HiddenField ID="hfId" runat="server" />      <asp:HiddenField ID="hfCmimi2" runat="server" />
            <asp:HiddenField ID="hfDtFillimi" runat="server" />
            <asp:HiddenField ID="hfDtMbarimi" runat="server" />
            <asp:HiddenField ID="hfSasiMin" runat="server" />
            <asp:HiddenField ID="hfSasiMax" runat="server" />
            <asp:HiddenField ID="hfCmimi" runat="server" />
            <asp:HiddenField ID="hfReshtaTeSelektuar" runat="server" />
    </div>
    </form>
</body>
</html>
