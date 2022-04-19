<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaSkemaKontabelRegjistrime.aspx.cs"
    Inherits="PlatinumWeb.LupaSkemaKontabelRegjistrime" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<html>
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
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaSkemaKontabelRegjistrime.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">         
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.  window.parent.    SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
    <div id='div' style="display: none">
        <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" ClientInstanceName="panel" runat="server"
            HeaderText="Zgjidh Llogarine" ShowHeader="False" Width='100%'>
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
                                <dx:ASPxGridView ID="gvLupaSkemaKontRegj" runat="server" ClientInstanceName="gvLupaSkemaKontRegj"
                                    OnDataBound="gvLupaSkemaKontRegj_DataBound" OnAfterPerformCallback="gvLupaSkemaKontRegj_AfterPerformCallback"
                                    Width="100%">
                                    <ClientSideEvents RowDblClick="function(s, e) {
                                        OnGridSelectionChanged();                                      
                                        }" />
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
                            <td align="center" class="style1">
                                <dx:ASPxButton ID="btnOk" runat="server" Text="Ok" AutoPostBack="False" ClientInstanceName="btnOk">
                                    <ClientSideEvents Click="function(s, e) {
                                     OnGridSelectionChanged(); 
                                                      
}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel >
    </div>
    </form>
</body>
</html>
