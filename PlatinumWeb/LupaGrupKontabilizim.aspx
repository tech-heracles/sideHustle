<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaGrupKontabilizimi.aspx.cs"
    Inherits="PlatinumWeb.LupaGrupKontabilizimi" %>

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
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaGrupKontabilizimi.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">
         
    <div>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
          
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.  window.parent.  SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <asp:UpdatePanel ID="pnl" runat="server">
            <ContentTemplate>
                <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" Width="100%" HeaderText="Zgjidh grupin e kontabilizimit"
                    ShowHeader="False">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                            ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                            OnItemClick="ASPxMenu1_ItemClick">
                                            <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                            <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                            <ClientSideEvents ItemClick="Item_Click" Init="function(s) {s.SetClientVisible(true);}" />
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
                            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientInstanceName="popFshi"
                                CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes"
                                Font-Bold="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                Width="300px" ClientIDMode="AutoID" CssPostfix="Glass">
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
                                                                    <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                                        OnClick="ButtonOk_Click2" Text="Ok">
                                                                        <ClientSideEvents Click="function(s, e) {   
                                                       
	popFshi.Hide();
    Utils.shfaqLoadingGif();;
}" />
                                                                    </dx:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo">
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
                            <div style="visibility: hidden">
                                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton" ClientInstanceName="btn">
                                </dx:ASPxButton>
                            </div>
                            <%--  <dx:ASPxLabel ID="lblKodi" runat="server" Text="Kodi">
                    </dx:ASPxLabel>
                    <dx:ASPxTextBox ID="txtKodi" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                    <dx:ASPxLabel ID="lblPershkrimi" runat="server" Text="Pershkrimi">
                    </dx:ASPxLabel>
                    <dx:ASPxTextBox ID="txtPershkrimi" runat="server" Width="170px">
                    </dx:ASPxTextBox>--%>
                            <br />
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dx:ASPxGridView ID="gvLupaGrKont" runat="server" ClientInstanceName="gvLupaGrKont"
                                            OnDataBound="gvLupaGrKont_DataBound" OnAfterPerformCallback="gvLupaGrKont_AfterPerformCallback"
                                            OnHeaderFilterFillItems="gvLupaGrKont_HeaderFilterFillItems" Width="100%" OnRowInserting="gvLupaGrKont_RowInserting"
                                            OnRowValidating="gvLupaGrKont_RowValidating" OnStartRowEditing="gvLupaGrKont_StartRowEditing"
                                            OnRowUpdating="gvLupaGrKont_RowUpdating">
                                            <ClientSideEvents RowDblClick="function(s, e) {
                                      Utils.shfaqLoadingGif();;
                                      OnGridSelectionChanged();                                      
                                    }" EndCallback="function(s, e) { EndCallbackGrida(s, e); }" />
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
                         Utils.shfaqLoadingGif();;
                                      

}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <%--   <dx:ASPxLabel ID="pergjigja" runat="server" Text="">
                    </dx:ASPxLabel>--%>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel >
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
    </div>
    </form>
</body>
</html>
