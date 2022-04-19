<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GrupimeKlientFurnitor.aspx.cs"
    Inherits="PlatinumWeb.GrupimeKlientFurnitor" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
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
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/GrupimeKlientFurnitor.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    
    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
        Font-Size="9pt" Modal="True" ImagePosition="Top">
        <LoadingDivStyle Opacity="30">
        </LoadingDivStyle>
    </dx:ASPxLoadingPanel>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
     
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" AutoPostBack="true" ClientInstanceName="ASPxMenu1"
                            ItemImagePosition="Top" OnDataBound="ASPxMenu1_DataBound" SeparatorWidth="1px"
                            ShowPopOutImages="True" Width="100%">
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
                        <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <dx:ASPxMenu ID="MenuInfo" runat="server" BorderBetweenItemAndSubMenu="HideRootOnly"
                                    ClientIDMode="AutoID" ClientInstanceName="MenuInfo" ShowPopOutImages="True" Width="100%">
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
                        <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
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
                                                        <ClientSideEvents Click="Click_ButtonOk" />
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
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel >
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl >
            <div style="visibility: hidden">
                <dx:ASPxButton ID="ASPxButton1" runat="server" ClientInstanceName="btn" Text="ASPxButton"
                    Height="0px">
                </dx:ASPxButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="width: 100%">
        <asp:UpdatePanel runat="server" ID="pnlGrida" UpdateMode="Conditional">
            <ContentTemplate>
                <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" ClientInstanceName="PageControl"
                      TabSpacing="3px" Width="100%" ActiveTabIndex="0" Height="600px">
                    <ClientSideEvents ActiveTabChanging="function(s, e) {ndryshimTabi(e.tab);}" />
                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    </ContentStyle>
                    <TabPages>
                        <dxtc:TabPage Name="Grupimi 1" Text="Grupimi 1">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl1" runat="server">
                                    <dx:ASPxGridView ID="gvKodifikimKlientFurnitor" runat="server" Width="100%" OnAfterPerformCallback="gridat_AfterPerformCallback"
                                        OnHeaderFilterFillItems="gridat_HeaderFilterFillItems" OnRowUpdating="rowUpdating"
                                        ClientInstanceName="gvKodifikimKlientFurnitor" OnProcessColumnAutoFilter="gridat_ProcessColumnAutoFilter"
                                        OnCancelRowEditing="startRowEditing" OnRowInserting="rowInserting" OnRowValidating="rowValidating"
                                        OnInitNewRow="initNewRow" OnCustomCallback="gridat_CustomCallback" OnCustomJSProperties="gridat_CustomJSProperties">
                                        <ClientSideEvents EndCallback="function(s, e) { EndCallbackGrida(s, e);}" RowDblClick="function(s, e) {
                                        RowDblClickGrida1(s,e);}
                                        " BeginCallback="function(s, e) {BeginCallback(s,e);}" />
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <SettingsPager PageSize="15">
                                        </SettingsPager>
                                        <SettingsEditing Mode="EditFormAndDisplayRow" />
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Grupimi 2" Text="Grupimi 2">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl2" runat="server">
                                    <dx:ASPxGridView ID="gvKodifikimKlientFurnitorGrupim2" runat="server" Width="100%"
                                        OnAfterPerformCallback="gridat_AfterPerformCallback" OnHeaderFilterFillItems="gridat_HeaderFilterFillItems"
                                        OnRowUpdating="rowUpdating" ClientInstanceName="gvKodifikimKlientFurnitorGrupim2"
                                        OnProcessColumnAutoFilter="gridat_ProcessColumnAutoFilter" OnCancelRowEditing="startRowEditing"
                                        OnRowInserting="rowInserting" OnRowValidating="rowValidating" OnInitNewRow="initNewRow"
                                        OnCustomCallback="gridat_CustomCallback" OnCustomJSProperties="gridat_CustomJSProperties">
                                        <ClientSideEvents EndCallback="function(s, e) { EndCallbackGrida(s, e);}" RowDblClick="function(s, e) { RowDblClickGrida2(s,e);}"
                                            BeginCallback="function(s, e) {
                                        BeginCallback(s,e);
                                      }" />
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <SettingsPager PageSize="15">
                                        </SettingsPager>
                                        <SettingsEditing Mode="EditFormAndDisplayRow" />
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Grupimi 3" Text="Grupimi 3">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl3" runat="server">
                                    <dx:ASPxGridView ID="gvKodifikimKlientFurnitorGrupim3" runat="server" Width="100%"
                                        OnAfterPerformCallback="gridat_AfterPerformCallback" OnHeaderFilterFillItems="gridat_HeaderFilterFillItems"
                                        OnRowUpdating="rowUpdating" ClientInstanceName="gvKodifikimKlientFurnitorGrupim3"
                                        OnProcessColumnAutoFilter="gridat_ProcessColumnAutoFilter" OnCancelRowEditing="startRowEditing"
                                        OnRowInserting="rowInserting" OnRowValidating="rowValidating" OnInitNewRow="initNewRow"
                                        OnCustomCallback="gridat_CustomCallback" OnCustomJSProperties="gridat_CustomJSProperties">
                                        <ClientSideEvents EndCallback="function(s, e) { EndCallbackGrida(s, e);}" RowDblClick="function(s, e) {
                                        RowDblClickGrida3(s,e);}
                                        " BeginCallback="function(s, e) {
                                        BeginCallback(s,e);
                                      }" />
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <SettingsPager PageSize="15">
                                        </SettingsPager>
                                        <SettingsEditing Mode="EditFormAndDisplayRow" />
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                </dxtc:ASPxPageControl >
                <asp:HiddenField ID="hfRuaj" runat="server" />
                <asp:HiddenField ID="hfPrindi" runat="server" />
                <asp:HiddenField ID="hfNiveli" runat="server" />
                <asp:HiddenField ID="hfGrupi" runat="server" />
                <dx:ASPxLabel ID="pergjigja" runat="server" ForeColor="Green">
                </dx:ASPxLabel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
    </div>
    <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" AllowResize="True"
                AppearAfter="10" ClientIDMode="AutoID" ClientInstanceName="popupUniversal" CloseAction="CloseButton"
                EnableAnimation="False" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter">
                <ClientSideEvents Closing="closing" />
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl >
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
