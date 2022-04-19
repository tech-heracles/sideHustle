<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaKonfigurimDokument.aspx.cs"
    Inherits="PlatinumWeb.LupaKonfigurimDokument" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
  <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
  <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaKonfigurimDokument.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">
           
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.window.parent.    SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
    <div id='div' style="display: none">
        <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" HeaderText="Zgjidh Llogarine"
            ClientInstanceName="panel" Width="100%" ShowHeader="False">
            <PanelCollection>
                <dx:PanelContent>
                    <br />
                    <dx:ASPxGridView ID="gvLupaKonfigDok" runat="server" ClientInstanceName="gvLupaKonfigDok"
                        OnDataBound="gvLupaKonfigDok_DataBound" OnAfterPerformCallback="gvLupaKonfigDok_AfterPerformCallback"
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
                    <br />
                    <div style="text-align: center">
                        <dx:ASPxButton ID="btnOk" runat="server" Text="Ok" AutoPostBack="false" ClientInstanceName="btnOk"
                            Style="text-align: center">
                            <ClientSideEvents Click="function(s, e) {
                                    OnGridSelectionChanged();
                                   
                                                          
}" />
                        </dx:ASPxButton>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel >
    </div>
    </form>
</body>
</html>
