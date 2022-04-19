<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KonfigDokumentash.aspx.cs"
    Inherits="PlatinumWeb.KonfigDokumentash" %>

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
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
  <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/KonfigDokumentash.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
     
    <form id="form1" runat="server" style="width: 100%">
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
        <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                            ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                            OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
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
                        <div id="dvMenu" style="display: none">
                            <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                        BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True"
                                        AppearAfter="300">
                                        <ClientSideEvents Init="function(s,e){$('#dvMenu').show();myMesazh.InicializoTimer();}" />
                                        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <SubMenuStyle GutterWidth="17px" />
                                    </dx:ASPxMenu>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
            <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk" OnClick="ButtonOk_Click2" Text="Ok" ClientVisible ="false"></dx:ASPxButton>
            <dx:ASPxButton ID="ButtonOk2" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk2" OnClick="ButtonOk2_Click2" Text="Ok" ClientVisible ="false"></dx:ASPxButton>
            <dx:ASPxButton ID="ASPxButton1" runat="server" CausesValidation="False" ClientInstanceName="ASPxButton1" OnClick="ButtonOk_Click" Text="Ok" ClientVisible ="false"></dx:ASPxButton>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="pnlKryesor" runat="server">
        <ContentTemplate>
            <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Green" ClientInstanceName="pergjigja">
            </dx:ASPxLabel>
            <dx:ASPxGridView ID="gvKonfigAmbjentesh" ClientInstanceName="gvKonfigAmbjentesh"
                runat="server" Width="100%" OnDataBound="gvKonfigAmbjentesh_DataBound" OnAfterPerformCallback="gvKonfigAmbjentesh_AfterPerformCallback"
                OnHeaderFilterFillItems="gvKonfigAmbjentesh_HeaderFilterFillItems" OnProcessColumnAutoFilter="gvKonfigAmbjentesh_ProcessColumnAutoFilter"
                OnCustomJSProperties="gvKonfigAmbjentesh_CustomJSProperties" OnCustomCallback="gvKonfigAmbjentesh_CustomCallback">
                <Styles>
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                </Styles>
                <ClientSideEvents RowDblClick="function(s, e) {
                OnGridDoubleClick(e,e.visibleIndex); }" FocusedRowChanged="function(s,e){mbushfusha(e);}"
                    BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                <StylesEditors>
                    <ProgressBar Height="25px">
                    </ProgressBar>
                </StylesEditors>
                <SettingsBehavior AllowFocusedRow="False" />
                <SettingsBehavior AllowSelectByRowClick="false" />
            </dx:ASPxGridView>
            <br />
            <br />
            <dx:ASPxLabel ID="lblMesazh" runat="server">
            </dx:ASPxLabel>
            <asp:HiddenField runat="server" ID="hfshfaq"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="pyetjeFshiKonfig"></asp:HiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
