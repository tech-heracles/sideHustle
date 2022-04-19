<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GjeneroProjektProdhimi.aspx.cs"
    Inherits="PlatinumWeb.GjeneroProjektProdhimi" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxw" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="js/css/le-frog/jquery-ui.css" media="screen" rel="stylesheet" type="text/css"
        runat="server" id="themeJQuery" />
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <%--<script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
<script src="js/jquery-ui-1.10.2.custom.min.js" type="text/javascript"></script>
<script src="js/myMesazh-IMB.2.1.js?versioni22" type="text/javascript"></script>
<script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
<script src="js/myButtonClickLupa-IMB.2.1.js?versioni22" type="text/javascript"></script>
 
<script src="js/myCookies-IMB.2.1.js?versioni22" type="text/javascript"></script>
<script src="js/Utils-IMB.2.1.js?versioni22" type="text/javascript"></script>
<script src="js/myJQGrid-IMB.2.1.js?versioni22" type="text/javascript"></script>
<script src="JsGlobal.js" type="text/javascript"></script>
<script src="js/json2.js" type="text/javascript"></script>
<script src="js/aspx.js/GjeneroProjektProdhimi.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script>--%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/jquery.ui.datepicker-sq.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/aspx.js/GjeneroProjektProdhimi.aspx-IMB.2.1.js&v76"""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
         
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <div style="width: 100%; height: 100%">
            <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
               
            </asp:ScriptManager>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                <%--<ClientSideEvents EndCallback="function(s,e){ try{ window.parent.SessionTimeout.sendKeepAlive(); } catch(e){}}" />--%>
            </dx:ASPxGlobalEvents>
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
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popKonvertuar" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                        ClientInstanceName="popKonvertuar" CloseAction="CloseButton" EnableAnimation="False"
                        EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" Width="300px">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl433" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel133" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent133" runat="server">
                                            <dx:ASPxLabel Wrap="True" ID="lblMsgboxKonv" runat="server" ClientInstanceName="lblMsgboxKonv" ClientIDMode="AutoID" Text="Jeni i sigurt?">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOk5" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                                OnClick="ButtonOk5_Click" Text="Po">
                                                                <ClientSideEvents Click="Click_ButtonOk5" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancel5" runat="server" ClientIDMode="AutoID" Text="Jo" AutoPostBack="false">
                                                                <ClientSideEvents Click="Click_ButtonCancel5" />
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="hfStatusRuajtje" runat="server" />
                    <asp:HiddenField ID="hfKategoria" runat="server" />
                    <asp:HiddenField ID="hfKodi" runat="server" />
                    <asp:HiddenField ID="hfLupaKodifikim1" runat="server" />
                    <asp:HiddenField ID="hfLupaKodifikim2" runat="server" />
                    <asp:HiddenField ID="hfNjesia" runat="server" />
                    <asp:HiddenField ID="hfSasia" runat="server" />
                    <asp:HiddenField ID="hfCmimi" runat="server" />
                    <asp:HiddenField ID="hfMagazina" runat="server" />
                    <asp:HiddenField ID="hfVlefta" runat="server" />
                    <asp:HiddenField ID="hfMagazina2" runat="server" />
                    <asp:HiddenField ID="hfSkemaKontabel" runat="server" />
                    <asp:HiddenField ID="hfDetajimetSelektuara" runat="server" />
                    <asp:HiddenField ID="hfLupaKlientFurnitor" runat="server" />
                    <asp:HiddenField ID="hfLupaMagazina" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="gridDataObject" runat="server" />
                    <asp:HiddenField ID="gridDataObject2" runat="server" />
                    <asp:HiddenField ID="proveObjekt2" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaGjitheDok" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="730px" ClientInstanceName="splitter"
                PaneMinSize="700px">
                <Panes>
                    <%-- Header pane--%>
                    <dx:SplitterPane PaneStyle-BackColor="Transparent" Separators-Size="10px" ScrollBars="Vertical">
                        <Separators Size="10px">
                        </Separators>
                        <PaneStyle>
                        </PaneStyle>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="spliter" runat="server">
                                 <asp:Panel ID="ContentPanel" runat="server">
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
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%" AnimationType="None">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
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
                                                            ClientInstanceName="lblKonfigurimi" Text="">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <%--<div id="dvlblDtNga">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtNga" ID="lblDtNga" runat="server"
                                            Text="Date nga:" ClientInstanceName="lblDtNga">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvdteDtNga">--%>
                                        <dx:ASPxDateEdit ID="dteDtNga" runat="server" ClientInstanceName="dteDtNga" ShowShadow="False"
                                            Width="100%">
                                            <ClientSideEvents DateChanged="function (s,e){DateChanged(s,e);}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <%--</div>--%>
                                        <%--<div id="dvlblDtDeri">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtDeri" ID="lblDtDeri" runat="server"
                                            Text="Date deri:" ClientInstanceName="lblDtDeri">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvdteDtDeri">--%>
                                        <dx:ASPxDateEdit ID="dteDtDeri" runat="server" ClientInstanceName="dteDtDeri" ShowShadow="False"
                                            Width="100%">
                                            <ClientSideEvents DateChanged="function (s,e){DateChanged(s,e);}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <%--</div>--%>
                                        <%--<div id="dvlblArtikulli">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneArtikulli" ID="lblArtikulli"
                                            runat="server" Text="Produkti" ClientInstanceName="lblArtikulli">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvbtneArtikulli">--%>
                                        <dx:ASPxComboBox ID="btneArtikulli" ClientInstanceName="btneArtikulli" runat="server"
                                            EnableCallbackMode="True" ShowShadow="False" OnItemRequestedByValue="btneArtikulli_ItemRequestedByValue"
                                            Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickArtikulli();}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <%--<RequiredField IsRequired="true" />--%>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%--</div>--%>
                                        <%--<div id="dvlblGrup">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneGrup" ID="lblGrup" runat="server"
                                            Text="Grupi" ClientInstanceName="lblGrup">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvbtneGrup">--%>
                                        <dx:ASPxComboBox ID="btneGrup" ClientInstanceName="btneGrup" runat="server" ShowShadow="False"
                                            Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickGrup();}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <%--<RequiredField IsRequired="true" />--%>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%--</div>--%>
                                        <%--<div id="dvlblNenGrup">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneNenGrup" ID="lblNenGrup" runat="server"
                                            Text="NenGrupi" ClientInstanceName="lblNenGrup">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvbtneNenGrup">--%>
                                        <dx:ASPxComboBox ID="btneNenGrup" ClientInstanceName="btneNenGrup" runat="server"
                                            Width="100%" ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickNenGrup();}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <%-- <RequiredField IsRequired="true" />--%>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%--</div>--%>
                                        <%--<div id="dvlblKlienti">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKlienti" ID="lblKlienti" runat="server"
                                            Text="Klienti" ClientInstanceName="lblKlienti">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvbtneKlienti">--%>
                                        <dx:ASPxComboBox ID="btneKlienti" runat="server" ClientInstanceName="btneKlienti"
                                            ShowShadow="False" ValueType="System.Int32" EnableClientSideAPI="True" IncrementalFilteringMode="Contains"
                                            EnableSynchronization="True" EnableCallbackMode="True" ValidationSettings-CausesValidation="true"
                                            DropDownRows="3" CallbackPageSize="3" OnItemRequestedByValue="btneKlienti_ItemRequestedByValue"
                                            OnItemsRequestedByFilterCondition="btneKlienti_ItemsRequestedByFilterCondition"
                                            SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <ClientSideEvents ButtonClick="function(s,e){Furnitori_Click();}" SelectedIndexChanged="function(s,e){IndexChangedFurnitori(s,e);}"
                                                GotFocus="function(s, e){s.SelectAll();}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <%-- <RequiredField IsRequired="true" />--%>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%--</div>--%>
                                        <%--<div id="dvlblNrUrdherShitje">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneNrUrdherShitje" ID="lblNrUrdherShitje"
                                            runat="server" Text="Nr Urdher Shitje" ClientInstanceName="lblNrUrdherShitje">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvbtneNrUrdherShitje">--%>
                                        <dx:ASPxComboBox ID="btneNrUrdherShitje" ClientInstanceName="btneNrUrdherShitje"
                                            Width="100%" runat="server" ShowShadow="False" EnableSynchronization="True" 
                                            EnableCallbackMode="True" DropDownRows="10" CallbackPageSize="10" OnItemRequestedByValue="btneNrUrdherShitje_ItemRequestedByValue"
                                            OnItemsRequestedByFilterCondition="btneNrUrdherShitje_ItemsRequestedByFilterCondition"
                                            IncrementalFilteringMode="Contains"
                                            Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickKerko();}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <%--<RequiredField IsRequired="true" />--%>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxButton ID="btnKerko" runat="server" Text="Kerko" ClientInstanceName="btnKerko"
                                            AutoPostBack="false" ValidationGroup="entries" Width="100%">
                                            <ClientSideEvents Click="function(s, e) { kerko(); }" />
                                        </dx:ASPxButton>
                                        <%--</div>--%>
                                    </div>
                                    <br />
                                    <div id="dvbtnKerko" style="display: none">
                                        <table width="100%">
                                            <tr align="center">
                                                <td></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <br />
                                    <br />
                                    <div id="dvgvFaturat" class="atributeDiveFshehur">
                                        <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" ClientIDMode="AutoID" Width="100%"
                                            ClientInstanceName="nvFatura">
                                            <Groups>
                                                <dx:NavBarGroup Text="Porositë" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px"
                                                    HeaderStyle-ForeColor="Gray" Expanded="True">
                                                    <HeaderStyle Font-Bold="True" Font-Size="14px" ForeColor="Gray"></HeaderStyle>
                                                    <ContentTemplate>
                                                        <dx:ASPxGridView ID="grid_faturat" runat="server" ClientInstanceName="grid_faturat"
                                                            Settings-ShowGroupPanel="false" Width="100%" OnCustomCallback="grid_faturat_CustomCallback"
                                                            OnDataBound="grid_faturat_DataBound" OnProcessColumnAutoFilter="grid_faturat_ProcessColumnAutoFilter"
                                                            OnHtmlRowCreated="grid_faturat_HtmlRowCreated" OnCustomJSProperties="grid_faturat_CustomJSProperties"
                                                            OnAfterPerformCallback="grid_faturat_AfterPerformCallback">
                                                            <ClientSideEvents SelectionChanged="SelectionChanged_grid_faturat" />
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
                                                    </ContentTemplate>
                                                </dx:NavBarGroup>
                                            </Groups>
                                        </dx:ASPxNavBar>
                                    </div>
                                    <br />
                                    <br />
                                    <div id="divgride1" style="display: none">
                                        <div id="divgride2">
                                            <table id="rowed5">
                                            </table>
                                        </div>
                                    </div>
                                    <%--      <div id="divgride1" style="visibility: hidden">
                                    <dx:ASPxGridView ID="gvGjenerimi" runat="server" ClientInstanceName="gvGjenerimi"
                                        Width="100%" OnHtmlRowCreated="gvGjenerimi_HtmlRowCreated" OnCustomJSProperties="gvGjenerimi_CustomJSProperties"
                                        OnCustomCallback="gvGjenerimi_CustomCallback" OnAfterPerformCallback="gvGjenerimi_AfterPerformCallback"
                                        OnDataBound="gvGjenerimi_DataBound">
                                        <ClientSideEvents BeginCallback="function (s,e){merrTeDhenatCheck();}" EndCallback="function (s,e){vendosTeDhenaCheck(s,e);}" />
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                </div>--%>
                                    <br />
                                    <br />
                                    <br />
                                    <table id="tblFund" class="renditKontrolle" align="right">
                                        <tbody>
                                            <%--<tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td width="55%">
                                            </td>
                                        </tr>--%>
                                        </tbody>
                                    </table>
                                    <div id="dvFundi" class="atributeDiveFshehur">
                                        <%--<div id="dvlblGrup1">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGrup1" ID="lblGrup1" runat="server"
                                            Text="Grupim 1:" ClientInstanceName="lblGrup1">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvcmbGrup1">--%>
                                        <dx:ASPxComboBox ID="cmbGrup1" runat="server" ClientInstanceName="cmbGrup1" ShowShadow="False"
                                            SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                ValidationGroup="entries1" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%--</div>--%>
                                        <%--<div id="dvlblGrup2">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGrup2" ID="lblGrup2" runat="server"
                                            Text="Grupim 2:" ClientInstanceName="lblGrup2">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvcmbGrup2">--%>
                                        <dx:ASPxComboBox ID="cmbGrup2" runat="server" ClientInstanceName="cmbGrup2" ShowShadow="False"
                                            SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                ValidationGroup="entries1" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%--</div>--%>
                                        <%--<div id="dvlblGrup3">--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGrup3" ID="lblGrup3" runat="server"
                                            Text="Grupim 3:" ClientInstanceName="lblGrup3">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvcmbGrup3">--%>
                                        <dx:ASPxComboBox ID="cmbGrup3" runat="server" ClientInstanceName="cmbGrup3" ShowShadow="False"
                                            SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                ValidationGroup="entries1" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%--</div>--%>
                                    </div>
                                 </asp:Panel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
                <Styles>
                </Styles>
                <Images>
                </Images>
            </dx:ASPxSplitter >
            <asp:HiddenField ID="HfKonfAmb" runat="server" />
            <asp:HiddenField ID="HfColNjesiArt" runat="server" />
            <asp:HiddenField ID="HfColNjesAdminis" runat="server" />
            <asp:HiddenField ID="HfColNjesAdminisDest" runat="server" />
            <asp:HiddenField ID="HfColTrupMag" runat="server" />
            <asp:HiddenField ID="HfColDetArt" runat="server" />
            <asp:HiddenField ID="HfColDetArt2" runat="server" />
            <asp:HiddenField ID="HfColArt" runat="server" />
            <asp:HiddenField ID="hfShtimModifikim" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HFStatusiDokumentit" runat="server" />
            <asp:HiddenField ID="hfKolonaGride" runat="server" />
            <asp:HiddenField ID="HfGridCol" runat="server" />
            <asp:HiddenField ID="hfKontabilizimi" runat="server" />
            <asp:HiddenField ID="hfFD" runat="server" />
            <asp:HiddenField ID="hfKonffillestar" runat="server" />
            <asp:HiddenField ID="hfFH" runat="server" />
            <asp:HiddenField ID="hfPlanifikime" runat="server" />
            <asp:HiddenField ID="hfLlogaria" runat="server" />
            <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
            <asp:HiddenField ID="hfGridaKodi" runat="server" />
            <asp:HiddenField ID="hfGridaDetajimi" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            <asp:HiddenField ID="hfKontrolletNrAutom" runat="server" />
            <asp:HiddenField ID="hfAtributeNrAutom" runat="server" />
            <dx:ASPxHiddenField ID="hfNrAutoShitje" runat="server" ClientInstanceName="hfNrAutoShitje">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
            </dx:ASPxHiddenField>
        </div>
        <br />
        <br />
        <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
        </dx:ASPxHiddenField>
        <div>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                <ClientSideEvents Closing="closing" />
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl >
        </div>
    </form>
</body>
</html>
