<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMLidhAnkete.aspx.cs" Inherits="PlatinumWeb.CRMLidhAnkete" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha CRM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/images/CRM/faviconCRM.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/images/CRM/faviconCRM.ico" type="image/ico" />
    <link type="text/css" rel="stylesheet" href="~/js/srcCRM/css/jquery.mmenu.all.css" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link type="text/css" rel="stylesheet" href="AlphaCRM.css" />
    <link href="css/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <style>
        div#ASPxMenu1_DXM1_{
            width: 540px !important;
        }
        div#ASPxMenu1_DXME1_{
            width: 100% !important;
        }
        ul.dx.dxm-gutter{
            width: 100% !important;
        }
    </style>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/srcCRM/js/jquery.mmenu.min.all.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/CRMLidhAnkete.aspx-IMB.4.7.js&v49"
        type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $('nav#menu').mmenu({
                classes: "mm-light",
            });
        });
    </script>
</head>
<body>
    <div id="page">
        <div class="header">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 1%;">
                        <a href="#menu"></a>
                    </td>
                    <td style="width: 94%; vertical-align: top;">Percakto Veprimtarine</td>
                    <td style="width: 5%;">
                        <div id="emriLogout" class="emriLogout">
                            <div id="userInfo">
                                <div id="emri">
                                    <dx:ASPxLabel ID="lblUserEmri" ClientInstanceName="lblUserEmri" runat="server" Text=""
                                        Font-Size="14" ForeColor="White" Font-Names="Calibri">
                                    </dx:ASPxLabel>
                                </div>
                                <div id="logout">
                                    <a style="position: relative; color: white; background-image: none;" class="fa fa-sign-out fa-2x" ><i class="fa fa-sign-out  fa-lg"></i>&nbsp;</a>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="content">
            <form id="form1" runat="server">
                 
                <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                    Font-Size="9pt" Modal="True" ImagePosition="Top">
                    <LoadingDivStyle Opacity="30">
                    </LoadingDivStyle>
                </dx:ASPxLoadingPanel>
                <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
                
                </asp:ScriptManager>
                <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                    ViewStateMode="Enabled">
                </dx:ASPxHiddenField>
                <asp:UpdatePanel ID="UpdatePanelMenu" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td>
                                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" 
                                        ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                        OnItemClick="ASPxMenu1_ItemClick">
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
                                                                    Text="Ok">
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
                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton" ClientInstanceName="btn">
                            </dx:ASPxButton>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!--  FUNDI MENUSE-->
                <!-- TABET -->
                <div id="dvInfoArtikulli">
                   <%-- <asp:UpdatePanel ID="pnlGrida" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                            <dx:ASPxGridView ID="gvKlienti"  ToolTip="Veprimtaria" runat="server" ClientInstanceName="gvKlienti"
                                Width="100%" OnProcessColumnAutoFilter="gvKlienti_ProcessColumnAutoFilter"
                                OnHtmlRowCreated="gvKlienti_HtmlRowCreated" OnAfterPerformCallback="gvKlienti_AfterPerformCallback"
                                OnHeaderFilterFillItems="gvKlienti_HeaderFilterFillItems" OnDataBound="gvKlienti_DataBound"
                                OnCustomCallback="gvKlienti_CustomCallback" Settings-ShowTitlePanel="True" 
                                OnCustomJSProperties="gvKlienti_CustomJSProperties" SettingsBehavior-ColumnResizeMode="NextColumn">
                                <Templates>

                                    <DetailRow >
                                        <%-- Kontrollet brenda nje DetailRow nuk duhet te kene client instance name, pasi vendosen kontrollet disa here me te njejten ID (per cdo rresht), cka shkakton probleme me eventet --%>
                                                                                <dx:ASPxCheckBox runat="server" ClientSideEvents-Init="InitVlefshmeriaChkb" ID="chkbVlefshmeria" EnableViewState="true" ClientSideEvents-CheckedChanged="checkedChangedVetemTeVlefshmet" Text="Shfaq vetem te vlefshmet"></dx:ASPxCheckBox>
                                        <dx:ASPxGridView runat="server" OnDataBound="gvLidhjet_DataBound" SettingsBehavior-ColumnResizeMode="NextColumn" ID="gvLidhjet" OnBeforePerformDataSelect="gvLidhjet_BeforePerformDataSelect" OnCustomCallback="gvLidhjet_CustomCallback"  Width="100%">
                                                                                    </dx:ASPxGridView>


                                    </DetailRow>
                                </Templates>
                                <SettingsDetail ExportMode="Expanded" ShowDetailRow="true"  />

                                <Styles>
                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                    </Header>
                                </Styles>
                                <ClientSideEvents RowDblClick="function(s, e){}" EndCallback="function(s, e) { }" BeginCallback="function(s, e) { BeginCallback(s,e); }" />
                                <SettingsBehavior ColumnResizeMode="NextColumn"></SettingsBehavior>
                                <Styles>
                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                    </Header>
                                </Styles>
                                <StylesEditors>
                                    <ProgressBar Height="25px">
                                    </ProgressBar>
                                </StylesEditors>
                            </dx:ASPxGridView>
                            <asp:HiddenField ID="hfRuaj" runat="server" />
                       <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hfKonffillestar" runat="server" />
                        <asp:HiddenField ID="hfLidhur" runat="server" />
                        <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:HiddenField ID="hfKontrollet" runat="server" />
                        <asp:HiddenField ID="hfStatusi" runat="server" />
                        <asp:HiddenField runat="server" ID="hfVetemTeVlefshmet" />
                        <%--Perdoret per te ruajtur filtrin e periudhes se detyrave dhe anketave te qe do shfaqen per klientin--%>
                        <asp:HiddenField ID="hfArkivaDokId" runat="server" />
                        <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
                        </dx:ASPxHiddenField>
                         <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
                </dx:ASPxHiddenField>
                <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" AllowResize="True"
                            AppearAfter="10" ClientIDMode="AutoID"  ClientInstanceName="popupUniversal" CloseAction="CloseButton"
                            EnableAnimation="False" HeaderText="Zgjidh Llogarine"  Width="900" Modal="True" PopupHorizontalAlign="WindowCenter"
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
        </div>
        <nav id="menu">
            <ul id="ulMenu">
            </ul>
        </nav>
    </div>
</body>
</html>
