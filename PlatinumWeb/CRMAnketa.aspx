<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMAnketa.aspx.cs" Inherits="PlatinumWeb.CRMAnketa" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>




<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha CRM</title>
     <link id="Link1" runat="server" rel="shortcut icon" href="~/images/CRM/faviconCRM.ico" type="image/x-icon"/>
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
        <link id="Link2" runat="server" rel="icon" href="~/images/CRM/faviconCRM.ico" type="image/ico"/>
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="~/js/srcCRM/css/jquery.mmenu.all.css" />
    <link type="text/css" rel="stylesheet" href="AlphaCRM.css" />
    <link href="css/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/srcCRM/js/jquery.mmenu.min.all.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/CRMAnketa.aspx-IMB.4.7.js&v76""
        type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $('nav#menu').mmenu({
                classes: "mm-light"
            });
        });
    </script>
</head>
<body>
    <div id="page">
                <div class="header">
            <table style="width:100%;">
                <tr>
                    <td style="width:1%;">
                        <a href="#menu"></a>
                    </td>
                    <td style="width:94%;vertical-align: top;">Konfiguro Ankete</td>
                    <td style="width:5%;">
                        <div id="emriLogout" class="emriLogout">
                            <div id="userInfo">
                                <div id="emri" >
                                    <dx:ASPxLabel  ID="lblUserEmri" ClientInstanceName="lblUserEmri" runat="server" Text=""
                                        Font-Size="14" ForeColor="White" Font-Names="Calibri">
                                    </dx:ASPxLabel>
                                </div>
                                <div id="logout">
                                    <a style="position:relative; color:white;background-image: none;" class="fa fa-sign-out fa-2x"><i class="fa fa-sign-out  fa-lg"></i>&nbsp;</a>
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
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td>
                                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                        ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                        OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                        <ClientSideEvents ItemClick="function (s, e) { menu_click(s, e);}" Init="function(s) {s.SetClientVisible(true);}" />
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
                                                    <ClientSideEvents Init="Init_MenuInfo" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:HiddenField ID="status1" runat="server" Value="false" />
                        <asp:HiddenField ID="hfStatusRuajtje" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="pnlLidhur" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id='hl' runat="server">
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table id="tblFillim" class="renditKontrolle">
                    <tbody>
                    </tbody>
                </table>
                <div id="dvFillim" class="atributeDiveFshehur">
                    <%--<div id="dvkonfigurimi_Label">--%>
                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                        runat="server" Text="Lloji:" ClientInstanceName="konfigurimi_Label">
                    </dx:ASPxLabel>
                    <%--</div>--%>
                    <%--<div id="dvcmbKonfigurimi">--%>
                    <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { ndryshoKonfigurimin() }" />
                        <DropDownButton>
                            <Image>
                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                            </Image>
                        </DropDownButton>
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                            ValidationGroup="entries" SetFocusOnError="true">
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                            <RequiredField IsRequired="true" />
                        </ValidationSettings>
                        <DisabledStyle Font-Bold="False">
                        </DisabledStyle>
                    </dx:ASPxComboBox>
                    <%--</div>--%>
                    <div id="dvlblKonfigurimi">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi"
                                        ClientInstanceName="lblKonfigurimi"  Text="">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <%--<div id="dvlblKodi">--%>
                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                        Text="Kodi:" ClientInstanceName="lblKodi">
                    </dx:ASPxLabel>
                    <%--</div>--%>
                    <%--<div id="dvtxtKodi">--%>
                    <dx:ASPxTextBox ID="txtKodi" runat="server" ClientInstanceName="txtKodi" Width="100%">
                        <%--<ClientSideEvents Init="function (s, e) { }" />--%>
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                            ValidationGroup="entries">
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                            <RequiredField IsRequired="true" />
                        </ValidationSettings>
                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                        </DisabledStyle>
                    </dx:ASPxTextBox>
                    <%--</div>--%>
                    <%--<div id="dvlblShenime">--%>
                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime" ID="lblShenime" runat="server"
                        Text="Emertimi:" ClientInstanceName="lblShenime">
                    </dx:ASPxLabel>
                    <%--</div>--%>
                    <%--<div id="dvtxtShenime">--%>
                    <dx:ASPxMemo ID="txtShenime" runat="server" ClientInstanceName="txtShenime" Width="100%" Rows="3">
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                            ValidateOnLeave="false">
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                            <RequiredField IsRequired="true" />
                        </ValidationSettings>
                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                        </DisabledStyle>
                    </dx:ASPxMemo>
                    <%--</div>--%>

                    <dx:ASPxLabel ID="lblDtFillimi" Wrap="False" AssociatedControlID="dteDtFillimi" runat="server" Text="Date Fillimi" ClientInstanceName="lblDtFillimi"></dx:ASPxLabel>

                    <dx:ASPxDateEdit ID="dteDtFillimi" runat="server" Width="100%" ClientInstanceName="dteDtFillimi" >
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                            ValidationGroup="entries">
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                            <RequiredField IsRequired="true" />
                        </ValidationSettings>
                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                        </DisabledStyle>
                    </dx:ASPxDateEdit>
                    <dx:ASPxLabel ID="lblDtMbarimi" Wrap="False" AssociatedControlID="dteDtMbarimi" runat="server" Text="Date Mbarimi" ClientInstanceName="lblDtMbarimi"></dx:ASPxLabel>

                    <dx:ASPxDateEdit ID="dteDtMbarimi" runat="server" Width="100%" ClientInstanceName="dteDtMbarimi"  >
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                            ValidationGroup="entries">
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                            <RequiredField IsRequired="true" />
                        </ValidationSettings>
                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                        </DisabledStyle>
                    </dx:ASPxDateEdit>

                </div>
                <div id="divgride1" style="display: none">
                    <asp:UpdatePanel ID="pnlbutona" runat='server'>
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td style="width: 20%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lblFushat" runat="server" Text="Fushat">
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxListBox ID="lbxFushat" runat="server" Width="100%" Height="500px" ClientInstanceName="lbxFushat"
                                                        SettingsLoadingPanel-ImagePosition="Top" OnCallback="lbxFushat_Callback" OnDataBound="lbxFushat_DataBound">
                                                        <ClientSideEvents SelectedIndexChanged="function (s, e) { UpdateButtonState(); }"
                                                            EndCallback="function (s, e) { UpdateButtonState(); }" />
                                                        <LoadingPanelImage>
                                                        </LoadingPanelImage>
                                                        <ValidationSettings>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </dx:ASPxListBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 5%">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnDjathtas1" runat="server" Text="&gt;" OnClick="btnDjathtas1_Click"
                                                        ClientInstanceName="btnDjathtas1" Width="100%">
                                                        <ClientSideEvents Click="function (s, e) { disablebtn() }" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnDjathtasGjitha" runat="server" Text="&gt;&gt;" ClientInstanceName="btnDjathtasGjitha"
                                                        Width="100%" OnClick="btnDjathtasGjitha_Click">
                                                        <ClientSideEvents Click="function (s, e) { disablebtn() }" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnMajtas1" runat="server" Text="&lt;" ClientInstanceName="btnMajtas1"
                                                        Width="100%" OnClick="btnMajtas1_Click">
                                                        <ClientSideEvents Click="function (s, e) { disablebtn() }" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnMajtaGjitha" runat="server" Text="&lt;&lt;" ClientInstanceName="btnMajtaGjitha"
                                                        Width="100%" OnClick="btnMajtaGjitha_Click">
                                                        <ClientSideEvents Click="function (s, e) { disablebtn() }" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 60%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lblZgjedhur" runat="server" Text="Fushat e Zgjedhura">
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 500px" valign="top">
                                                    <dx:ASPxGridView ID="gvZgjedhur" runat="server" ClientInstanceName="gvZgjedhur"
                                                        Settings-ShowGroupPanel="false" Width="100%" OnCustomCallback="gvZgjedhur_CustomCallback"
                                                        OnDataBound="gvZgjedhur_DataBound" OnHtmlRowCreated="gvZgjedhur_HtmlRowCreated"
                                                        OnCustomJSProperties="gvZgjedhur_CustomJSProperties" OnAfterPerformCallback="gvZgjedhur_AfterPerformCallback"
                                                        Settings-VerticalScrollableHeight="475" Settings-VerticalScrollBarMode="Auto">
                                                        <ClientSideEvents SelectionChanged="function(s,e){UpdateButtonState();}" EndCallback="function(s,e){}" />
                                                        <Styles>
                                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                            </Header>
                                                        </Styles>
                                                        <StylesEditors>
                                                            <CalendarHeader Spacing="1px">
                                                            </CalendarHeader>
                                                            <ProgressBar Height="25px">
                                                            </ProgressBar>
                                                        </StylesEditors>
                                                    </dx:ASPxGridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <%--<td style="width: 5%">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnSiper" runat="server" Text="Lart" AutoPostBack="false" ClientInstanceName="btnSiper"
                                                        Width="70px" OnClick="btnSiper_Click">
                                                        <ClientSideEvents Click="function (s,e){disablebtn()}" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnPoshte" runat="server" Text="Poshte" AutoPostBack="false"
                                                        ClientInstanceName="btnPoshte" Width="70px" OnClick="btnPoshte_Click">
                                                        <ClientSideEvents Click="function (s,e){disablebtn()}" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>--%>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:HiddenField ID="HfKonfAmb" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HFStatusiDokumentit" runat="server" />
                <asp:HiddenField ID="hfKolonaGride" runat="server" />
                <asp:HiddenField ID="HfGridCol" runat="server" />
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
                <asp:HiddenField ID="hfGridaKodi" runat="server" />
                <asp:HiddenField ID="hfGridaDetajimi" runat="server" />
                <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
                </dx:ASPxHiddenField>
                <%--<table style="width: 100%">
                    <tr style="width: 100%">
                        <td style="width: 20%">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Kodi"></dx:ASPxLabel>
                        </td>
                        <td style="width: 27%">
                            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="80%"></dx:ASPxTextBox>
                        </td>
                        <td style="width: 6%"></td>
                        <td style="width: 20%">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Emertimi"></dx:ASPxLabel>
                        </td>
                        <td style="width: 27%">
                            <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="80%"></dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td style="width: 20%">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Date Fillimi"></dx:ASPxLabel>
                        </td>
                        <td style="width: 27%">
                            <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="80%"></dx:ASPxDateEdit>
                        </td>
                        <td style="width: 6%"></td>
                        <td style="width: 20%">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Date Mbarimi"></dx:ASPxLabel>
                        </td>
                        <td style="width: 27%">
                            <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="80%"></dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <%--  <tr>
                        <td colspan="2" style="width: 47%">
                            <dx:ASPxListBox ID="lbxFushat" runat="server" Width="100%" Height="500px" ClientInstanceName="lbxFushat"
                                SettingsLoadingPanel-ImagePosition="Top" OnCallback="lbxFushat_Callback" OnDataBound="lbxFushat_DataBound">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }"
                                    EndCallback="function(s, e) { UpdateButtonState(); }" />
                                <ValidationSettings>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </dx:ASPxListBox>
                        </td>
                        <td style="width: 6%">
                            <table style="width: 100%">
                                <tr style="width: 100%">
                                    <td style="width: 100%">
                                        <dx:ASPxButton ID="btnDjathtas1" runat="server" Text="&gt;" OnClick="btnDjathtas1_Click"
                                            ClientInstanceName="btnDjathtas1" Width="100%" ForeColor="White" BackColor="#00A74F">
                                            <ClientSideEvents Click="function (s,e){disablebtn()}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%">
                                        <dx:ASPxButton ID="btnDjathtasGjitha" runat="server" Text="&gt;&gt;" ClientInstanceName="btnDjathtasGjitha"
                                            Width="100%" OnClick="btnDjathtasGjitha_Click">
                                            <ClientSideEvents Click="function (s,e){disablebtn()}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%">
                                        <dx:ASPxButton ID="btnMajtas1" runat="server" Text="&lt;" ClientInstanceName="btnMajtas1"
                                            Width="100%" OnClick="btnMajtas1_Click">
                                            <ClientSideEvents Click="function (s,e){disablebtn()}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%">
                                        <dx:ASPxButton ID="btnMajtaGjitha" runat="server" Text="&lt;&lt;" ClientInstanceName="btnMajtaGjitha"
                                            Width="100%" OnClick="btnMajtaGjitha_Click">
                                            <ClientSideEvents Click="function (s,e){disablebtn()}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2" style="width: 47%">
                            <dx:ASPxListBox ID="lbxFushatZgjedhur" runat="server" Width="100%" Height="500px" ClientInstanceName="lbxFushatZgjedhur"
                                SettingsLoadingPanel-ImagePosition="Top" OnCallback="lbxFushat_Callback" OnDataBound="lbxFushat_DataBound">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }"
                                    EndCallback="function(s, e) { UpdateButtonState(); }" />
                                <ValidationSettings>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </dx:ASPxListBox>

                        </td>

                    </tr>
                </table>--%>
            </form>
        </div>
        <nav id="menu">
            <ul id="ulMenu">
            </ul>
        </nav>
    </div>
</body>
</html>
