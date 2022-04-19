<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KonfigurimFormatImporti.aspx.cs"
    Inherits="PlatinumWeb.KonfigurimFormatImporti" %>

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
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/KonfigurimFormatImporti.aspx-IMB.2.3.js&v76""
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
        <%--<ClientSideEvents EndCallback="function(s,e){try{ window.parent.    SessionTimeout.sendKeepAlive();} catch(e){}}" />--%>
    </dx:ASPxGlobalEvents>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfKonffillestar" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
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
                                        BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                        <ClientSideEvents Init="function(s,e){$('#dvMenu').show();myMesazh.InicializoTimer();}" />
                                        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                        <SubMenuStyle GutterWidth="17px" />
                                    </dx:ASPxMenu>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
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
    Utils.shfaqLoadingGif();;
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <table class="renditKontrolle">
        <tr>
            <td class="renditKontrolleCaption">
                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                    runat="server" ClientIDMode="AutoID" Text="Modeli:">
                </dx:ASPxLabel>
            </td>
            <td class="renditKontrolleCellMeWidth33">
                <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                    ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                    Width="100%" AnimationType="None">
                    <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                    <DropDownButton>
                        <Image>
                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                        </Image>
                    </DropDownButton>
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                        <ErrorFrameStyle ImageSpacing="4px">
                            <ErrorTextPaddings PaddingLeft="4px" />
                        </ErrorFrameStyle>
                    </ValidationSettings>
                </dx:ASPxComboBox>
            </td>
            <td class="renditKontrolleLabelMeWidth33">
                <dx:ASPxLabel ID="lblKonfigurimi" runat="server" Text="" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi">
                </dx:ASPxLabel>
            </td>
            <td class="renditKontrolleCellMeWidth33">
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="pnlKryesor" runat="server">
        <ContentTemplate>
            <dx:ASPxLabel ID="pergjigja" runat="server" Text="" EncodeHtml="False" ForeColor="Green">
            </dx:ASPxLabel>
            <br />
            <dx:ASPxGridView ID="gvKonfigurim" ClientInstanceName="gvKonfigurim" runat="server"
                Width="100%" OnDataBound="gvKonfigurim_DataBound" OnAfterPerformCallback="gvKonfigurim_AfterPerformCallback"
                OnCustomCallback="gvKonfigurim_CustomCallback" OnCustomJSProperties="gvKonfigurim_CustomJSProperties"
                OnProcessColumnAutoFilter="gvKonfigurim_ProcessColumnAutoFilter" OnHeaderFilterFillItems="gvKonfigurim_HeaderFilterFillItems">
                <Templates>
                    <TitlePanel>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Zgjidh kolonat" AutoPostBack="false"
                                        ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                        <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s,e,gvKonfigurim)}"
                                            Init="myFaqeCelje.InitTeDrejtaKonf" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="pnlruaj" runat="server">
                                        <ContentTemplate>
                                            <dx:ASPxButton ID="ASPxButton3" runat="server" Text="Ruaj kolonat" AutoPostBack="true"
                                                ClientVisible="false" Image-Url="images/new/disk_blue (3).png" Font-Size="8"
                                                OnClick="RuajKolona_Click">
                                                <ClientSideEvents Init="myFaqeCelje.InitTeDrejtaKonf" />
                                            </dx:ASPxButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </TitlePanel>
                </Templates>
                <Styles>
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                </Styles>
                <SettingsPager PageSize="15">
                </SettingsPager>
                <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e,e.visibleIndex); }"
                    FocusedRowChanged="function(s,e){mbushfusha(e);}" BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                <StylesEditors>
                    <ProgressBar Height="25px">
                    </ProgressBar>
                </StylesEditors>
            </dx:ASPxGridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
            CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
            <ClientSideEvents Closing="function(s, e) { popupUniversal.SetContentUrl('');}" />
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl >
    </div>
    </form>
</body>
</html>
