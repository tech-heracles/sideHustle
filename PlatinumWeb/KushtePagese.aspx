<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KushtePagese.aspx.cs" Inherits="PlatinumWeb.KushtePagese" %>

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
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<!DOCTYPE html>
<script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/KushtePagese.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
         
    <asp:ScriptManager ID="ScriptManager1" runat="server">
       
    </asp:ScriptManager>
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
    </dx:ASPxHiddenField>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.    SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
    <asp:UpdatePanel ID="pnlKryesor" runat="server">
        <ContentTemplate>
            <div>
                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" Width="60%" ItemAutoWidth="False" OnItemClick="ASPxMenu1_ItemClick">
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
                                                            <ClientSideEvents Click="function(s, e) {popFshi.Hide();}" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonCancel" runat="server" Text="Anullo">
                                                            <ClientSideEvents Click="function(s, e) {popFshi.Hide();}" />
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
                <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Green">
                </dx:ASPxLabel>
                <dx:ASPxGridView ID="grid_KushtePagese" runat="server" Width="60%" ClientInstanceName="grid_KushtePagese"
                    OnDataBound="grid_KushtePagese_DataBound" OnAfterPerformCallback="grid_KushtePagese_AfterPerformCallback">
                    <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex); }" />
                    <Styles>
                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                        </Header>
                    </Styles>
                    <StylesEditors>
                        <ProgressBar Height="25px">
                        </ProgressBar>
                    </StylesEditors>
                </dx:ASPxGridView>
                <br />
                <br />
                <dx:ASPxLabel ID="lblMesazh" runat="server">
                </dx:ASPxLabel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
