<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_RegjistrimRiparimi.aspx.cs" Inherits="PlatinumWeb.Shto_RegjistrimRiparimi" %>


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
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server"
        id="themeJQuery" />
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" media="screen" rel="stylesheet" type="text/css" />

    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/json2.js;~/JsGlobal.js;~/js/myCookies-IMB.2.1.js;~/js/aspx.js/Shto_RegjistrimRiparimi.aspx-IMB.4.0.js&v76"
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
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popMesazhQK" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                        ClientInstanceName="popMesazhQK" CloseAction="CloseButton" EnableAnimation="False"
                        ShowCloseButton="false" EnableViewState="False" Font-Bold="true" HeaderText="Kujdes"
                        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        Width="300px">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl7" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel17" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent17" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel Wrap="False" ID="lblMsgbox4" runat="server" ClientIDMode="AutoID"
                                                Text="Deshironi te beni shperndarjen ne qendrat e kostos?">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOkQK" runat="server" CausesValidation="False" ClientInstanceName="ButtonOkQK"
                                                                AutoPostBack="false" Text="Po">
                                                                <ClientSideEvents Click="Click_ButtonOkQK" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancelQK" runat="server" ClientIDMode="AutoID" Text="Jo"
                                                                AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s, e) {
		popMesazhQK.Hide();
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
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxPanel >
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl >
                </ContentTemplate>
            </asp:UpdatePanel>

               <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState"></dx:ASPxHiddenField>
       
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Red" ClientInstanceName="pergjigja"
                        ClientVisible="false">
                    </dx:ASPxLabel>
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="hfStatusRuajtje" runat="server" />
                    <asp:HiddenField ID="hfqkmesazhi" runat="server" Value="jo" />
                    <asp:HiddenField ID="hfUrl" runat="server" />
                    <asp:HiddenField ID="hfIMEI" runat="server" /><asp:HiddenField ID="hfrreshti" runat="server" />
                    <asp:HiddenField ID="hfAksesor" runat="server" />
                    <asp:HiddenField ID="hfEmertimi" runat="server" />
                    <asp:HiddenField ID="hfDetajimet" runat="server" />
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
                    <asp:HiddenField ID="proveObjekt2" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hfTeDrejtaInfoArt" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaArtRi" runat="server" />
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
            <asp:HiddenField ID="hfKontrollRivleresim" runat="server" />
            <asp:HiddenField ID="hfKonffillestar" runat="server" />
            <asp:HiddenField ID="hfLlogaria" runat="server" />
            <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
            <asp:HiddenField ID="hfGridaKodi" runat="server" />
            <asp:HiddenField ID="hfGridaDetajimi" runat="server" />
            <asp:HiddenField ID="hfKontrolletNrAutom" runat="server" />
            <asp:HiddenField ID="hfAtributeNrAutom" runat="server" />
            <asp:HiddenField ID="hfHapurMbyllur" runat="server" />
            <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="730px" ClientInstanceName="splitter"
                PaneMinSize="700px">
                <Panes>
                    <%-- Header pane--%>
                    <dx:SplitterPane PaneStyle-BackColor="Transparent" Separators-Size="10px" ScrollBars="Auto">
                        <Separators Size="10px">
                        </Separators>
                        <PaneStyle>
                        </PaneStyle>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
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
                                        <%--<div id="dvlblLloji">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblLloji" Text="boo" AssociatedControlID="cmbLloji"
                                            runat="server" ClientInstanceName="lblLloji">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvcmbLloji">--%>
                                        <dx:ASPxComboBox Width="100%" ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){TextChangedLloji();}" />
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
                                        <%-- </div>
                                     <div id="dvkonfigurimi_Label">--%>
                                        <dx:ASPxLabel Wrap="False" ID="konfigurimi_Label" AssociatedControlID="cmbKonfigurimi"
                                            runat="server" Text="Lloji:" ClientInstanceName="konfigurimi_Label">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvcmbKonfigurimi">--%>
                                        <dx:ASPxComboBox Width="100%" ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" AnimationType="None">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="true"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%-- </div>--%>
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

                                        <%--</div>
                                    <div id="dvlblMagazina">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblMagazina" AssociatedControlID="btneMagazina" runat="server"
                                            Text="Magazina" ClientInstanceName="lblMagazina">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvbtneMagazina">--%>
                                        <dx:ASPxComboBox Width="100%" ID="btneMagazina" ClientInstanceName="btneMagazina"
                                            runat="server" ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){identifikuesMagazina='Mag1';ButtonClickMagazina();}"
                                                TextChanged="function(s,e){TextChangedMagazina();}" />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="true"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>

                                        <dx:ASPxLabel Wrap="False" ID="lblDtDok" AssociatedControlID="dteDtDok" runat="server"
                                            Text="Dt Dokumenti:" ClientInstanceName="lblDtDok">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvdteDtDok">--%>
                                        <dx:ASPxDateEdit Width="100%" ID="dteDtDok" runat="server" ClientInstanceName="dteDtDok"
                                            ShowShadow="False">
                                            <ClientSideEvents DateChanged="function (s,e){DateChanged(s,e);}" GotFocus="function(s, e){ dateGotFocus( s, e); }"/>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
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
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="true"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>

                                        <dx:ASPxLabel Wrap="False" ID="lblKerkoGaranci" runat="server" Font-Bold="true"
                                            Text="Kerko Garanci:" ClientInstanceName="lblKerkoGaranci">
                                        </dx:ASPxLabel>


                                        <%--  </div>
                                   
                                    <div id="dvlblNrDok">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblIMEI" AssociatedControlID="txtIMEI" runat="server"
                                            Text="IMEI:" ClientInstanceName="lblIMEI">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvtxtNrDok">--%>
                                        <dx:ASPxTextBox Width="100%" ID="txtIMEI" runat="server" ClientInstanceName="txtIMEI">
                                            <ClientSideEvents Init="function(s, e) {  
	
}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="true"
                                                ValidationGroup="entries">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <%-- <RegularExpression ValidationExpression="[a-zA-Z0-9]*" ErrorText="Format i gabuar!" />--%>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>

                                        <dx:ASPxLabel Wrap="False" ID="lblGaranci" AssociatedControlID="txtGaranci"
                                            runat="server" Text="Garanci:" ClientInstanceName="lblGaranci">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvtxtNrDok">--%>
                                        <dx:ASPxTextBox Width="100%" ID="txtGaranci" runat="server" ClientInstanceName="txtGaranci">
                                            <ClientSideEvents Init="function(s, e) {  
	
}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <%-- <RegularExpression ValidationExpression="[a-zA-Z0-9]*" ErrorText="Format i gabuar!" />--%>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxButton ID="btnKerko" runat="server" Text="Kerko" ClientInstanceName="btnKerko" CausesValidation="False" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s,e){ KerkoClick(s,e)}" />
                                        </dx:ASPxButton>
                                    </div>
                                    <br />
                                    <div id="divgride5" style="display: none">

                                        <dx:ASPxGridView ID="grid_aktuale" runat="server" ClientInstanceName="grid_aktuale"
                                            Width="100%" OnDataBound="grid_aktuale_DataBound"
                                            OnCustomJSProperties="grid_aktuale_CustomJSProperties">

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
                                        <br />
                                        <br />
                                        <dx:ASPxLabel Wrap="False" ID="lblHistoriku" Font-Bold="true"
                                            runat="server" Text="Historiku" ClientInstanceName="lblHistoriku">
                                        </dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <dx:ASPxGridView ID="grid_historiku" runat="server" ClientInstanceName="grid_historiku"
                                            Width="100%" OnDataBound="grid_historiku_DataBound"
                                            OnCustomJSProperties="grid_historiku_CustomJSProperties">

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
                                    </div>

                                    <div id="divgride2" style="display: none">
                                        <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" Width="100%" ClientInstanceName="nvFatura">
                                            <Groups>
                                                <dx:NavBarGroup Text="Artikull per garanci" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px"
                                                    HeaderStyle-ForeColor="Gray">
                                                    <HeaderStyle Font-Bold="True" Font-Size="14px" ForeColor="Gray"></HeaderStyle>
                                                    <ContentTemplate>
                                                        <dx:ASPxGridView ID="grid_faturat" runat="server" ClientInstanceName="grid_faturat"
                                                            Width="100%" OnCustomCallback="grid_faturat_CustomCallback" OnDataBound="grid_faturat_DataBound"
                                                            OnProcessColumnAutoFilter="grid_faturat_ProcessColumnAutoFilter" OnHtmlRowCreated="grid_faturat_HtmlRowCreated"
                                                            OnCustomJSProperties="grid_faturat_CustomJSProperties" OnAfterPerformCallback="grid_faturat_AfterPerformCallback">
                                                            <ClientSideEvents SelectionChanged="function(s,e){}" EndCallback="function (s,e){ kontrolloDateGarancie(s,e)}" />
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
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                        </dx:ASPxNavBar>
                                    </div>
                                    <br />
                                    <br />
                                    <div id="divgride6" style="display: none">
                                        <dx:ASPxLabel Wrap="False" ID="lblAparati" Font-Bold="true"
                                            runat="server" Text="Aparati ne dorezim" ClientInstanceName="lblAparati">
                                        </dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <dx:ASPxGridView ID="grid_Loan" runat="server" ClientInstanceName="grid_Loan"
                                            Width="100%" OnDataBound="grid_Loan_DataBound"
                                            OnCustomJSProperties="grid_Loan_CustomJSProperties" OnHtmlRowCreated="grid_Loan_HtmlRowCreated">

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
                                        <br />
                                        <br />
                                        <dx:ASPxLabel Wrap="False" ID="lblKerkesa" Font-Bold="true"
                                            runat="server" Text="Perfundoni kerkesen ne menyre qe te dorezoni pajisjen!" ClientInstanceName="lblKerkesa">
                                        </dx:ASPxLabel>
                                    </div>
                                    <table id="tblFund" class="renditKontrolle">
                                        <tbody>
                                            <%--<tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 40%">
                                            </td>
                                        </tr>--%>
                                        </tbody>
                                    </table>
                                    <br />

                                    <div id="dvFundi" class="atributeDiveFshehur">

                                        <dx:ASPxLabel Wrap="False" ID="lblProduktGaranci"
                                            runat="server" Text="" ClientInstanceName="lblProduktGaranci">
                                        </dx:ASPxLabel>


                                        <dx:ASPxLabel Wrap="False" ID="lblLoan" AssociatedControlID="cbLoan"
                                            runat="server" Text="Loan:" ClientInstanceName="lblLoan">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvcbMeKonfirmim">--%>
                                        <dx:ASPxCheckBox Width="100%" ID="cbLoan" runat="server" ClientInstanceName="cbLoan">
                                        </dx:ASPxCheckBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblAparatiRiparim" Font-Bold="true"
                                            runat="server" Text="Aparati per riparim:" ClientInstanceName="lblAparatiRiparim">
                                        </dx:ASPxLabel>

                                        



                                        <dx:ASPxLabel Wrap="False" ID="lblStatus" AssociatedControlID="cmbStatus"
                                            runat="server" Text="Statusi:" ClientInstanceName="lblStatus">
                                        </dx:ASPxLabel>
                                        <%--</div>
                                    <div id="dvcmbDegeAdministrative">--%>
                                        <dx:ASPxComboBox Width="100%" ID="cmbStatus" runat="server" ClientInstanceName="cmbStatus"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ClientSideEvents SelectedIndexChanged ="function(s,e){ StatusChanged(s,e)}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="True"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>

                                        <dx:ASPxLabel Wrap="False" ID="lblDorezuar" AssociatedControlID="cmbDorezuar"
                                            runat="server" Text="Aparati ne dorezim eshte kthyer:" ClientInstanceName="lblDorezuar">
                                        </dx:ASPxLabel>
                                        <%--</div>
                                    <div id="dvcmbDegeAdministrative">--%>
                                        <dx:ASPxComboBox Width="100%" ID="cmbDorezuar" runat="server" ClientInstanceName="cmbDorezuar"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="True"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblDifekti" AssociatedControlID="cmbDifekti"
                                            runat="server" Text="Lloj Difekti:" ClientInstanceName="lblDifekti">
                                        </dx:ASPxLabel>
                                        <%--</div>
                                    <div id="dvcmbDegeAdministrative">--%>
                                        <dx:ASPxComboBox Width="100%" ID="cmbDifekti" runat="server" ClientInstanceName="cmbDifekti"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="True"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblPershkrimi" AssociatedControlID="txtPershkrimi"
                                            runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi">
                                        </dx:ASPxLabel>
                                        <%-- </div>



                                    <div id="dvtxtNrDok">--%>
                                        <dx:ASPxMemo ID="txtPershkrimi" Width="100%" runat="server" ClientInstanceName="txtPershkrimi"
                                            Rows="3">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                            <ClientSideEvents LostFocus="function(s,e){}" />
                                        </dx:ASPxMemo>
                                        <%-- </div>
                                     <div id="dvlblNrProjekti">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblNrKontakti" AssociatedControlID="txtNrKontakti"
                                            runat="server" Text="Nr Kontakti" ClientInstanceName="lblNrKontakti">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvtxtNrProjekti">--%>
                                        <dx:ASPxTextBox Width="100%" ID="txtNrKontakti" runat="server" ClientInstanceName="txtNrKontakti">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblIMEISwap" AssociatedControlID="txtIMEISwap"
                                            runat="server" Text="IMEI" ClientInstanceName="lblIMEISwap">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvtxtNrProjekti">--%>
                                        <dx:ASPxTextBox Width="100%" ID="txtIMEISwap" runat="server" ClientInstanceName="txtIMEISwap">
                                            <ClientSideEvents  TextChanged="function (s,e){KontrolloIMEISwap(s,e);
                                                }" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblProdukti" AssociatedControlID="txtProdukti"
                                            runat="server" Text="Produkti" ClientInstanceName="lblProdukti">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvtxtNrProjekti">--%>
                                        <dx:ASPxTextBox Width="100%" ID="txtProdukti" runat="server" ClientInstanceName="txtProdukti">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblAksesor" AssociatedControlID="txtAksesor" runat="server"
                                            Text="Aksesor:" ClientInstanceName="lblAksesor">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvtxtNrDok">--%>
                                        <dx:ASPxMemo Width="100%" ID="txtAksesor" runat="server" ClientInstanceName="txtAksesor" Rows="3">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <%-- <RegularExpression ValidationExpression="[a-zA-Z0-9]*" ErrorText="Format i gabuar!" />--%>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxMemo>




                                        <%-- </div>
                                   
                                    <%--   </div>--%>

                                        <%--<div id="dvlblDtRegjistrimi">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblDtRegjistrimi" runat="server" Text="Dt Regjistrimi:"
                                            ClientInstanceName="lblDtRegjistrimi" AssociatedControlID="dteDtRegjistrimi">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvdteDtRegjistrimi">--%>
                                        <dx:ASPxDateEdit Width="100%" ID="dteDtRegjistrimi" runat="server" ClientInstanceName="dteDtRegjistrimi"
                                            ShowShadow="False">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
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
                                    </div>
                                    <dx:ASPxHiddenField ID="hfNrAutoShitje" runat="server" ClientInstanceName="hfNrAutoShitje">
                                    </dx:ASPxHiddenField>
                                    <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
                                    </dx:ASPxHiddenField>
                                    <br />
                                    <br />
                                    <div id="divgride1" style="display: none">
                                        <dx:ASPxLabel Wrap="False" ID="lblZgjidhProduktin" Font-Bold="true"
                                            runat="server" Text="Zgjidh produktin zevendesues" ClientInstanceName="lblZgjidhProduktin">
                                        </dx:ASPxLabel>

                                        <dx:ASPxGridView ID="gvArtLoan" runat="server" Width="100%" OnAfterPerformCallback="gvArtLoan_AfterPerformCallback"
                                            OnHeaderFilterFillItems="gvArtLoan_HeaderFilterFillItems" OnAutoFilterCellEditorInitialize="gvArtLoan_AutoFilterCellEditorInitialize"
                                            ClientInstanceName="gvArtLoan"
                                            OnProcessColumnAutoFilter="gvArtLoan_ProcessColumnAutoFilter"
                                            OnInitNewRow="gvArtLoan_InitNewRow" OnCustomCallback="gvArtLoan_CustomCallback"
                                            OnCustomJSProperties="gvArtLoan_CustomJSProperties" ClientIDMode="AutoID" OnDataBound="gvArtLoan_DataBound" OnHtmlRowCreated="gvArtLoan_HtmlRowCreated">
                                            <ClientSideEvents EndCallback="function (s,e){EndCallback(s,e)}" />
                                            <Styles>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                            <SettingsPager PageSize="15">
                                            </SettingsPager>
                                            <StylesEditors>
                                                <ProgressBar Height="25px">
                                                </ProgressBar>
                                            </StylesEditors>
                                        </dx:ASPxGridView>

                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                </asp:Panel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <%-- Navigation pane --%>
                </Panes>

                <Styles>
                </Styles>
                <Images>
                </Images>
            </dx:ASPxSplitter >
        </div>
        <div id="divfund1" style="visibility: hidden;">
        </div>
         <asp:UpdatePanel ID="UpdatePanel10" runat="server">
            <ContentTemplate>
               
                <iframe id="Container" runat="server" frameborder="0" height="0" name="Container"
                    width="0"></iframe>
               
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
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

