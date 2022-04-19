<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenyraTransporti.aspx.cs"
    Inherits="PlatinumWeb.MenyraTransporti" %>

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
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/MenyraTransporti.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.    SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
    <div>
        <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" Width="40%" ItemAutoWidth="False">
            <ClientSideEvents ItemClick="Item_Click" Init="function(s) {s.SetClientVisible(true);}" />
        </dx:ASPxMenu>
        <div style="height: 5px">
        </div>
        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientInstanceName="popFshi"
            CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes"
            Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowHeader="true" Width="300px" Enabled="True" >
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                    <dxp:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" Width="200px">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <dx:ASPxLabel ID="lblMsgbox" runat="server" Text="Jeni i sigurt?">
                                </dx:ASPxLabel>
                                <br />
                                <br />
                                <div style="text-align: right;">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="ButtonOk" runat="server" Text="Ok" CausesValidation="False" OnClick="ButtonOk_Click2">
                                                    <ClientSideEvents Click="function(s, e) {
	popFshi.Hide();
}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="ButtonCancel" runat="server" Text="Anullo">
                                                    <ClientSideEvents Click="function(s, e) {
		popFshi.Hide();
}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxp:ASPxPanel >
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl >
        <asp:UpdatePanel ID="pnlKryesor" runat="server">
            <ContentTemplate>
                <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Green">
                </dx:ASPxLabel>
                <dx:ASPxGridView ID="grid_MenyraTransporti" runat="server" OnAfterPerformCallback="grid_MenyraTransporti_AfterPerformCallback"
                    OnDataBound="grid_MenyraTransporti_DataBound" OnHeaderFilterFillItems="grid_MenyraTransporti_HeaderFilterFillItems"
                    OnHtmlRowCreated="grid_MenyraTransporti_HtmlRowCreated" ClientInstanceName="grid_MenyraTransporti"
                    OnRowUpdating="grid_MenyraTransporti_RowUpdating" Width="40%" OnProcessColumnAutoFilter="grid_MenyraTransporti_ProcessColumnAutoFilter">
                    <ClientSideEvents RowDblClick="function(s, e) { indexModifiko=e.visibleIndex;  callWebservice(); }" />
                    <Styles>
                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                        </Header>
                    </Styles>
                    <StylesEditors>
                        <ProgressBar Height="25px">
                        </ProgressBar>
                    </StylesEditors>
                </dx:ASPxGridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
