<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaKomponente.aspx.cs"
    Inherits="PlatinumWeb.LupaKomponente" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

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
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/ListPagesaUtils.js;~/js/aspx.js/LupaKomponente.aspx-IMB.2.1.js&v76""" type="text/javascript"></script>
    <link href="AlphaWeb.css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        <asp:HiddenField ID="hfParametra" runat="server" />
        <asp:HiddenField ID="hfVeprimi" runat="server" />
        <div>
            <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1"  runat="server" ContentPaddings-Padding="0"  HeaderText="Zgjidh Llogarine"
                Width="100%" ShowHeader="False">
                <PanelCollection>
                    <dx:PanelContent>
                        <table width="100%" style="margin:0">
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lblPunonjesi" runat="server" Text="Punonjesi" Width="150px" Wrap="True">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtPunonjesi" runat="server" ClientEnabled="False" ClientInstanceName="txtPunonjesi"
                                        Width="170px">
                                        <ValidationSettings>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 20%">
                                    <dx:ASPxButton ID="btnImporti" runat="server" Text="Rimerr vlerat e importuara" ClientInstanceName="btnImporti" AutoPostBack="false">
                                        <ClientSideEvents Click="lupaKomponente.handlers.btnImportiClick" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>

                        <table width="100%"  style="margin:0">

                            <tr>
                                <td>
 
                                    <dx:ASPxLoadingPanel ID="LoadingPanel" ClientInstanceName="LoadingPanel" runat="server"></dx:ASPxLoadingPanel>
                                    <dx:ASPxGridView  OnCustomSummaryCalculate="gvLupaK_CustomSummaryCalculate" Font-Size="11"  ID="gvLupaK" EnableViewState="true" OnHtmlRowCreated="gvLupaK_HtmlRowCreated" ClientInstanceName="gvLupaK" runat="server"  SettingsBehavior-SortMode="Custom" OnCustomColumnSort="gvLupaK_CustomColumnSort"
                                        Width="100%"  OnCustomGroupDisplayText="gvLupaK_CustomGroupDisplayText"
                                      OnBatchUpdate="gvLupaK_BatchUpdate" Settings-VerticalScrollableHeight="350" Settings-VerticalScrollBarMode="Auto">
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                            <BatchEditModifiedCell  BackColor="Transparent"/>
                                            <FocusedRow ForeColor="Black"></FocusedRow>
                                        </Styles>
                                        <ClientSideEvents EndCallback="lupaKomponente.handlers.endCallbackGrida"
                                            BatchEditStartEditing="lupaKomponente.handlers.BatchEditStartEditingGrida"
                                            BatchEditEndEditing="lupaKomponente.handlers.BatchEditEndEditingGrida"
                                            BatchEditRowValidating="lupaKomponente.handlers.BatchEditRowValidatingGrida"
                                            RowCollapsing="lupaKomponente.handlers.RowCollapsingGrida"
                                            RowDblClick="lupaKomponente.handlers.RowDblClickGrida" />

                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Settings  ShowGroupFooter="VisibleIfExpanded" />
                                        <SettingsBehavior AutoExpandAllGroups="true" />
                                        <Templates>

                                             <GroupRowContent>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width:30%">
                                                                 <dx:ASPxLabel Font-Size="11" Font-Bold="true"  ForeColor="Black"   ID="lblGrupi"  Text='<%#   Container.GroupText.Replace(".","")  %>' runat="server" />
                                                        </td>

                                                        <td style="width:70%">
                                                            <dx:ASPxLabel ID="lbl" Font-Size="11" Font-Bold="true" ForeColor="Black"   runat="server" Text='<%# Convert.ToDecimal(gvLupaK.GetGroupSummaryValue(Container.VisibleIndex, gvLupaK.GroupSummary["Vlera"])).ToString(DbCore.clsFunksione.krijoNumer(int.Parse(Request.QueryString["formatnr"]), "0")) %>'
                        Visible='<%# !Container.Expanded %>'></dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </table>
                </GroupRowContent>

                                            <StatusBar>
                                            </StatusBar>
                                        </Templates>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>

                                            <td>
                                                <dx:ASPxLabel ID="lblPaguar" runat="server" Text="Paguar" Width="150px" Wrap="True">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="txtPaguar" runat="server" ClientEnabled="False" ClientInstanceName="txtPaguar" Width="170px">
                                                    <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                                                        LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                                    <ValidationSettings>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dx:ASPxTextBox>
                                            </td>
                                                  <td  style="float:right">
                                    <asp:UpdatePanel ID="pnnl" runat="server">
                                        <ContentTemplate>

                                            <dx:ASPxButton runat="server" ID="btnKtheu" ClientInstanceName="btnKtheu" AutoPostBack="false" Text="Lista e Punonjesve">
                                            <ClientSideEvents Click="lupaKomponente.handlers.btnKthehuClick" Init="lupaKomponente.handlers.btnKthehuInit" />
                                            </dx:ASPxButton>
                                            <dx:ASPxButton ID="btnOk"  runat="server" Width="70px" Text="Ok" AllowFocus="true"
                                                ClientInstanceName="btnOk" OnClick="btnOk_Click">
                                                <ClientSideEvents Click="lupaKomponente.handlers.btnOkClick" />
                                            </dx:ASPxButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
            <asp:HiddenField ID="hfKompFill" runat="server" />
            <dx:ASPxHiddenField ID="hflejomod" runat="server" ClientInstanceName="hflejomod"></dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfdetyrueshme" runat="server" ClientInstanceName="hfdetyrueshme"></dx:ASPxHiddenField>
        </div>
        <div>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine" ShowCloseButton="false"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                <ClientSideEvents CloseUp="lupaKomponente.handlers.popupUniversalCloseUp"  CloseButtonClick="lupaKomponente.handlers.popupUniversalCloseUp" />
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </div>
    </form>
</body>
</html>