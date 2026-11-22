<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetajimeArtikulli.aspx.cs"
    Inherits="PlatinumWeb.DetajimeArtikulli" %>
<%@ Register TagPrefix="avec" Namespace="PlatinumWeb" Assembly="PlatinumWeb" %>

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
<head id="Head1" runat="server">
    <title>AVEC Accounting</title>
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
</head>

<script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/DetajimeArtikulli.aspx-IMB.2.1.js&v76""
    type="text/javascript"></script>
<body>
    <form id="form1" runat="server">
         
    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
        Font-Size="9pt" Modal="True" ImagePosition="Top">
        <LoadingDivStyle Opacity="30">
        </LoadingDivStyle>
    </dx:ASPxLoadingPanel>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
       
    </asp:ScriptManager>
    <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled">
    </dx:ASPxHiddenField>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
    </dx:ASPxGlobalEvents>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <avec:AvecToolbar ID="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound" OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                    BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                    <ClientSideEvents Init="function(s,e){myMesazh.InicializoTimer();
                                    }" />
                                    <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <SubMenuStyle GutterWidth="17px" />
                                </dx:ASPxMenu>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                ClientInstanceName="popFshi" CloseAction="CloseButton" CssPostfix="Glass" EnableAnimation="False"
                EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" Width="300px">
                <HeaderStyle>
                    <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                </HeaderStyle>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                        <dxp:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                            <PanelCollection>
                                <dxp:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="lblMsgbox" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?">
                                    </dx:ASPxLabel>
                                    <br />
                                    <br />
                                    <div style="text-align: right;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk" Text="Ok" AutoPostBack="False">
                                                        <ClientSideEvents Click="function(s, e){Click_ButtonOk(s,e);}" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                        <ClientSideEvents Click="Click_ButtonCancel" />
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
            <div style="display: none">
                <dx:ASPxButton ID="ASPxButton1" runat="server" ClientInstanceName="btn" Text="ASPxButton"
                    Height="0px">
                </dx:ASPxButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="pnlKryesor" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <dx:ASPxGridView ID="gvDetajimArtikulli" runat="server" Width="100%" 
                OnHeaderFilterFillItems="gvDetajimArtikulli_HeaderFilterFillItems"  OnDataBound="gvDetajimArtikulli_DataBound"
                OnRowUpdating="gvDetajimArtikulli_RowUpdating" ClientInstanceName="gvDetajimArtikulli"
                OnProcessColumnAutoFilter="gvDetajimArtikulli_ProcessColumnAutoFilter" OnCancelRowEditing="gvDetajimArtikulli_StartRowEditing"
                OnRowValidating="gvDetajimArtikulli_RowValidating" 
                OnCellEditorInitialize="gvDetajimArtikulli_CellEditorInitialize" OnRowInserting="gvDetajimArtikulli_RowInserting"
                OnCustomCallback="gvDetajimArtikulli_CustomCallback">
                <SettingsBehavior AllowSort="true" />
                <ClientSideEvents RowDblClick="function(s, e){RowDblClickGrida(s, e);}" BeginCallback="function(s, e) {BeginCallback(s,e);}"
                    EndCallback="function(s, e) { EndCallbackGrida(s, e); }" />
                <Styles>
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                </Styles>
                <StylesEditors>
                    <ProgressBar Height="25px">
                    </ProgressBar>
                </StylesEditors>
            </dx:ASPxGridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hfRuaj" runat="server" />
    <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
    </dx:ASPxHiddenField>
    </div>
    <asp:HiddenField ID="hfAutorizime" runat="server" />
    </form>
</body>
</html>
