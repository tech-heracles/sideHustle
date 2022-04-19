<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Makro.aspx.cs" Inherits="PlatinumWeb.Makro" %>

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
<html>
<head id="Head1" runat="server">
    <title>Alpha Web</title>
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <%--    <script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/aspx.js/Makro.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script>--%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Makro.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
         
    <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.    SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
          <dx:ASPxHiddenField ID="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
    <asp:UpdatePanel ID="pnlKryesor" runat="server">
        <ContentTemplate>
            <div style="width: 100%">
                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" Width="100%" ItemAutoWidth="False" OnItemClick="ASPxMenu1_ItemClick">
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
            </div>
            <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Green">
            </dx:ASPxLabel>
            <div>
                <dx:ASPxGridView ID="gvMakro" ClientInstanceName="gvMakro" runat="server" Width="60%"
                    OnDataBound="gvMakro_DataBound" OnAfterPerformCallback="gvMakro_AfterPerformCallback"
                    OnHeaderFilterFillItems="gvMakro_HeaderFilterFillItems" OnAutoFilterCellEditorInitialize="gvMakro_AutoFilterCellEditorInitialize1"
                    OnProcessColumnAutoFilter="gvMakro_ProcessColumnAutoFilter">
                    <Styles>
                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                        </Header>
                    </Styles>
                    <ClientSideEvents RowDblClick="function(s, e) {
            OnGridDoubleClick(e.visibleIndex);    	
}" />
                    <StylesEditors>
                        <ProgressBar Height="25px">
                        </ProgressBar>
                    </StylesEditors>
                </dx:ASPxGridView>
            </div>
            <dx:ASPxLabel ID="lblPergjigja" runat="server" Text="">
            </dx:ASPxLabel>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
