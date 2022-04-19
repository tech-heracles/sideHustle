<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaLloji.aspx.cs" Inherits="PlatinumWeb.LupaLloji" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <style type="text/css">
        .style1
        {
            height: 58px;
        }
    </style>
  <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
  <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaLloji.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">
           
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <div>
            <asp:UpdatePanel ID="pnl" runat="server">
                <ContentTemplate>
                    <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" Width="100%" Height="100%"
                        HeaderText="Zgjidh llojin default" Font-Bold="True"  
                        ShowHeader="False">
                        
                        <Border BorderColor="#D7D7D7" BorderStyle="Solid" BorderWidth="1px" />
                   
                        <BorderBottom BorderWidth="0px" />
                        <ContentPaddings PaddingBottom="3px" PaddingLeft="3px" PaddingRight="3px" PaddingTop="3px" />
                        <PanelCollection>
                            <dx:PanelContent>
                                <dx:ASPxLabel ID="lblKodi" runat="server" Text="Kodi">
                                </dx:ASPxLabel>
                                <dx:ASPxTextBox ID="txtKodi" runat="server" Width="170px">
                                </dx:ASPxTextBox>
                                <dx:ASPxLabel ID="lblPershkrimi" runat="server" Text="Pershkrimi">
                                </dx:ASPxLabel>
                                <dx:ASPxTextBox ID="txtPershkrimi" runat="server" Width="170px">
                                </dx:ASPxTextBox>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <dx:ASPxGridView ID="gvLupaLloji" runat="server" ClientInstanceName="gvLupaLloji"
                                                OnAfterPerformCallback="gvLupaLloji_AfterPerformCallback" OnHtmlRowCreated="gvLupaLloji_HtmlRowCreated"
                                                OnCustomCallback="gvLupaLloji_CustomCallback" OnCustomJSProperties="gvLupaLloji_CustomJSProperties"
                                                OnDataBound="gvLupaLloji_DataBound">
                                                <StylesPager Summary-Width="100%" PageNumber-Width="100%">
                                                    <PageNumber Width="100%">
                                                    </PageNumber>
                                                    <Summary Width="100%">
                                                    </Summary>
                                                </StylesPager>
                                                <ClientSideEvents RowDblClick="function(s, e) {                                                          

                                            }"
                                                    BeginCallback="function(s, e) {
	                                            merrTeDhena();
                                            }"
                                                    EndCallback="function(s, e) {
		                                            merrTeDhena();
                                            }" />
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="style1">
                                            <dx:ASPxButton ID="btnOk" runat="server" Text="Ok" AutoPostBack="False" ClientInstanceName="btnOk">
                                                <ClientSideEvents Click="function(s, e) {                        
                                                  merrTeDhena();                            
                                                 window.parent.popupUniversal.Hide();
                                                }" />
                                            </dx:ASPxButton>
                                            <dx:ASPxTextBox ID="hfCheck" runat="server" ClientInstanceName="hfCheck" Width="0"
                                                Height="0" ForeColor="White">
                                                <Border BorderColor="White" />
                                            </dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="hfEmertimi" runat="server" ClientInstanceName="hfEmertimi" Width="0"
                                                Height="0" ForeColor="White">
                                                <Border BorderColor="White" />
                                            </dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="hfPrioriteti" runat="server" ClientInstanceName="hfPrioriteti"
                                                Width="0" Height="0" ForeColor="White">
                                                <Border BorderColor="White" />
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                     
                        <BackgroundImage Repeat="RepeatX" />
                        
                    </dx:ASPxRoundPanel >
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
