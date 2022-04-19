<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_ListPagesa.aspx.cs"
Inherits="PlatinumWeb.Shto_ListPagesa" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
Namespace="DevExpress.Web" TagPrefix="dx" %>




<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet"/>
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server"
          id="themeJQuery"/>
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" media="screen" rel="stylesheet" type="text/css"/>
     <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/async.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.js;~/js/myMesazh-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/JsGlobal.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/ListPagesaUtils.js;~/js/myCookies-IMB.2.1.js;~/js/json2.js;~/js/aspx.js/Shto_ListPagesa.aspx-IMB.2.1.js&v76"""
            ></script>
</head>
<body>
<form id="form1" runat="server">
     
<dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                     Font-Size="9pt" Modal="True" ImagePosition="Top">
    <LoadingDivStyle Opacity="30">
    </LoadingDivStyle>
</dx:ASPxLoadingPanel>

<div style="height: 100%; width: 100%;">
<asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
</asp:ScriptManager>
<dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
    <%--<ClientSideEvents EndCallback="function(s,e){  try{ window.parent.SessionTimeout.sendKeepAlive();}catch(e){}}"/>--%>
</dx:ASPxGlobalEvents>
<dx:ASPxHiddenField ID="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled">
</dx:ASPxHiddenField>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td>
                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                  ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                   SeparatorWidth="1px">
                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1"/>
                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify"/>
                        <ClientSideEvents ItemClick="function(s, e) {
	                        menu_click(s,e);
                            }" Init="function(s) {s.SetClientVisible(true);}" />
                        <ItemImage Height="32px" Width="32px">
                        </ItemImage>
                        <SubMenuItemImage Height="16px" Width="16px">
                        </SubMenuItemImage>
                        <ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" VerticalAlign="Middle">
                            <Paddings PaddingBottom="1px" PaddingTop="9px"/>
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
                                    <ClientSideEvents Init="function(s,e){$('#dvMenu').show();myMesazh.InicializoTimer();}"/>
                                    <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2"/>
                                    <ItemStyle HorizontalAlign="Left"/>
                                    <SubMenuStyle GutterWidth="17px"/>
                                </dx:ASPxMenu>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popMesazhQK" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                             ClientInstanceName="popMesazhQK" CloseAction="None" EnableAnimation="False" EnableViewState="False"
                             Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                             PopupVerticalAlign="WindowCenter" Width="300px" ShowCloseButton="False">
            <HeaderStyle>
                <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px"/>
            </HeaderStyle>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl7" runat="server">
                    <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel17" runat="server" ClientIDMode="AutoID" Width="271px">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent17" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxLabel Wrap="true" ID="lblMsgbox4" runat="server" ClientIDMode="AutoID" Text="Deshironi te beni shperndarjen ne qendrat e kostos?"
                                              ClientInstanceName="lblmesazhqendra">
                                </dx:ASPxLabel>
                                <br/>
                                <br/>
                                <div style="text-align: right;">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="ButtonOkQK" runat="server" CausesValidation="False" ClientInstanceName="ButtonOkQK"
                                                               AutoPostBack="false" Text="Po">
                                                    <ClientSideEvents Click="function(s, e) {
	popMesazhQK.Hide();
  hapPopUp(s,e);
}"/>
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="ButtonCancelQK" runat="server" ClientIDMode="AutoID" Text="Jo"
                                                               AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s, e) {
		popMesazhQK.Hide();
        JopopupClick(s,e);
}"/>
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
        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popAprovo" runat="server" AllowDragging="True" ClientInstanceName="popAprovo"
                             CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes"
                             Font-Bold="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                             Width="300px" ClientIDMode="AutoID" CssPostfix="Glass">
            <HeaderStyle>
                <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px"/>
            </HeaderStyle>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl41" runat="server">
                    <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel11" runat="server" ClientIDMode="AutoID" Width="271px">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent11" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxLabel ID="lblMsgbox1" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?">
                                </dx:ASPxLabel>
                                <br/>
                                <br/>
                                <div style="text-align: right;">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="ButtonOk1" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                                Text="Ok">
                                                    <ClientSideEvents Click="Click_ButtonOk1"/>
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="ButtonCancel1" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                    <ClientSideEvents Click="function(s, e) {
		popAprovo.Hide();   e.processOnServer = false;
}"/>
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
        <asp:AsyncPostBackTrigger ControlID="ASPxMenu1"/>
    </Triggers>
    <ContentTemplate>
        <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Red" ClientInstanceName="pergjigja"
                      ClientVisible="false">
        </dx:ASPxLabel>
        <asp:HiddenField ID="status1" runat="server" Value="false"/>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ASPxMenu1"/>
    </Triggers>
    <ContentTemplate>
        <asp:HiddenField ID="hfqkmesazhi" runat="server" Value="jo"/>
        <asp:HiddenField ID="hfNewId" runat="server" />
        <asp:HiddenField ID="hfEshteNeCikelAprovimi" runat="server" />
        <asp:HiddenField ID="hfUrl" runat="server"/>
        <asp:HiddenField ID="hfStatusRuajtje" runat="server"/>
        <asp:HiddenField ID="hfSkemaKontabel" runat="server"/>
        <asp:HiddenField ID="hfLupaDepartament" runat="server"/>
        <asp:HiddenField ID="hfLupaNenDepartament" runat="server"/>
        <asp:HiddenField ID="hfLidhur" runat="server"/>

        <asp:HiddenField ID="gridDataObject" runat="server"/>
        <asp:HiddenField ID="hfKomp" runat="server"/>
        <asp:HiddenField ID="proveObjekt2" runat="server"/>
        <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server"/>
    </ContentTemplate>
</asp:UpdatePanel>
<dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshiRresht" runat="server" AllowDragging="True" ClientInstanceName="popFshiRresht"
                     CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes"
                     Font-Bold="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                     Width="300px" ClientIDMode="AutoID" CssPostfix="Glass">
    <HeaderStyle>
        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px"/>
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl24" runat="server">
            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel21" runat="server" ClientIDMode="AutoID" Width="271px">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent21" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxLabel ID="lblMsgboxRreshti" ClientInstanceName="lblMsgboxRreshti" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?">
                        </dx:ASPxLabel>
                        <br/>
                        <br/>
                        <div style="text-align: right;">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="ButtonOk22" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk22"
                                                       AutoPostBack="false" Text="Ok">
                                            <ClientSideEvents Click="function(s, e) {e.processOnServer=false; fshiClickedPo();
	popFshiRresht.Hide();
   
                                                                   

}"/>
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="ButtonCancel22" runat="server" ClientIDMode="AutoID" Text="Anullo" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {     e.processOnServer = false;
		popFshiRresht.Hide();
}"/>
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
<dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
<asp:Panel ID="ContentPanel" runat="server">
<asp:UpdatePanel ID="pnlLidhur" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
    <ContentTemplate>
        <table id="hl" runat="server" >
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
                 ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top" AnimationType="None">
    <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}"/>
    <LoadingPanelImage>
    </LoadingPanelImage>
    <DropDownButton>
        <Image>
            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"/>
        </Image>
    </DropDownButton>
    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                        ValidationGroup="entries" SetFocusOnError="true">
        <ErrorFrameStyle ImageSpacing="4px">
            <ErrorTextPaddings PaddingLeft="4px"/>
        </ErrorFrameStyle>
        <RequiredField IsRequired="true"/>
    </ValidationSettings>
    <DisabledStyle Font-Bold="False">
    </DisabledStyle>
</dx:ASPxComboBox>
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
<dx:ASPxLabel Wrap="False" ID="lblStatusApr" runat="server" AssociatedControlID="lblStatusAprovimi"
              ClientInstanceName="lblStatusApr" Text="">
</dx:ASPxLabel>
<dx:ASPxLabel Wrap="False" ID="lblStatusAprovimi" runat="server" class="klasePerLblKonfigurimi"
              ClientInstanceName="lblStatusAprovimi" Text="">
</dx:ASPxLabel>
<dx:ASPxLabel Wrap="False" AssociatedControlID="cmbDepartamenti" ID="lblDepartamenti"
              runat="server" Text="Departamenti:" ClientInstanceName="lblDepartamenti">
</dx:ASPxLabel>
<dx:ASPxComboBox ID="cmbDepartamenti" runat="server" ClientInstanceName="cmbDepartamenti"
                 EnableCallbackMode="false"
                 SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
    <ClientSideEvents ButtonClick=" 
                                function(s,e) {ButtonClickedDepartamenti('Departamenti'); }"
                      SelectedIndexChanged="function(s, e) { departamentiChanged();
	                                              
                            }"/>
    <LoadingPanelImage>
    </LoadingPanelImage>
    <DropDownButton>
        <Image>
            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"/>
        </Image>
    </DropDownButton>
    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                        ValidateOnLeave="false">
        <ErrorFrameStyle ImageSpacing="4px">
            <ErrorTextPaddings PaddingLeft="4px"/>
        </ErrorFrameStyle>
        <RequiredField IsRequired="true"/>
    </ValidationSettings>
    <DisabledStyle Font-Bold="False">
    </DisabledStyle>
</dx:ASPxComboBox>
<dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNenDepartamenti" ID="lblNenDepartamenti"
              runat="server" Text="Nendepartamenti:" ClientInstanceName="lblNenDepartamenti">
</dx:ASPxLabel>
<dx:ASPxComboBox ID="cmbNenDepartamenti" runat="server" ClientInstanceName="cmbNenDepartamenti"
                 EnableCallbackMode="True" OnItemRequestedByValue="cmbNenDepartamenti_ItemRequestedByValue"
    OnItemsRequestedByFilterCondition="cmbNenDepartamenti_ItemsRequestedByFilterCondition"
                 SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
    <ClientSideEvents ButtonClick=" 
                                function(s,e) {ButtonClickedDepartamenti('NenDepartamenti'); }"
                      LostFocus="function(s, e) {
	                                              
                            }"/>
    <LoadingPanelImage>
    </LoadingPanelImage>
    <DropDownButton>
        <Image>
            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"/>
        </Image>
    </DropDownButton>
    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                        ValidateOnLeave="false">
        <ErrorFrameStyle ImageSpacing="4px">
            <ErrorTextPaddings PaddingLeft="4px"/>
        </ErrorFrameStyle>
        <RequiredField IsRequired="true"/>
    </ValidationSettings>
    <DisabledStyle Font-Bold="False">
    </DisabledStyle>
</dx:ASPxComboBox>
<dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMuaji" ID="lblMuaji" runat="server"
              ClientInstanceName="lblMuaji">
</dx:ASPxLabel>
<dx:ASPxComboBox ID="cmbMuaji" ClientInstanceName="cmbMuaji" runat="server" Width="100%"
                 ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top">
    <ClientSideEvents SelectedIndexChanged="function(s,e){NdryshimMuaji();}"/>
    <LoadingPanelImage>
    </LoadingPanelImage>
    <DropDownButton>
        <Image>
            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"/>
        </Image>
    </DropDownButton>
    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                        ValidationGroup="entries1" ValidateOnLeave="false">
        <ErrorFrameStyle ImageSpacing="4px">
            <ErrorTextPaddings PaddingLeft="4px"/>
        </ErrorFrameStyle>
        <RequiredField IsRequired="true"/>
    </ValidationSettings>
    <DisabledStyle Font-Bold="False">
    </DisabledStyle>
</dx:ASPxComboBox>
<dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrDok" ID="lblNrDok" runat="server"
              Text="Nr dokumenti:" ClientInstanceName="lblNrDok">
</dx:ASPxLabel>
<dx:ASPxTextBox ID="txtNrDok" runat="server" ClientInstanceName="txtNrDok" Width="100%">
    <ClientSideEvents Init="function(s, e) {  }"/>
    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                        ValidationGroup="entries">
        <ErrorFrameStyle ImageSpacing="4px">
            <ErrorTextPaddings PaddingLeft="4px"/>
        </ErrorFrameStyle>
        <RequiredField IsRequired="true"/>
    </ValidationSettings>
    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
    </DisabledStyle>
</dx:ASPxTextBox>
<dx:ASPxLabel Wrap="False" AssociatedControlID="txtKursi" ID="lblKursi" runat="server"
              Text="Kursi" ClientInstanceName="lblKursi">
</dx:ASPxLabel>
<dx:ASPxComboBox ID="txtKursi" Width="100%" runat="server" ClientInstanceName="txtKursi"
                 ShowShadow="False" EnableClientSideAPI="True" IncrementalFilteringMode="Contains"
                 EnableCallbackMode="True" CallbackPageSize="10" SettingsLoadingPanel-ImagePosition="Top">
    <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                      LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e); kontrolloKurs(s,e);}"
                      ButtonClick="function(s, e){ ButtonClickKursi(s, e); }"/>
    <DropDownButton>
        <Image>
            <SpriteProperties HottrackedCssClass="dxditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"/>
        </Image>
    </DropDownButton>
    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                        ValidateOnLeave="true" RegularExpression-ValidationExpression="[0-9.,]*" RegularExpression-ErrorText="Kursi duhet te jete numer!">
        <ErrorFrameStyle ImageSpacing="4px">
            <ErrorTextPaddings PaddingLeft="4px"/>
        </ErrorFrameStyle>
        <RequiredField IsRequired="true"/>
    </ValidationSettings>
    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
    </DisabledStyle>
</dx:ASPxComboBox>
<dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtDok" ID="lblDtDok" runat="server"
              Text="Dt Dokumenti:" ClientInstanceName="lblDtDok">
</dx:ASPxLabel>
<dx:ASPxDateEdit ID="dteDtDok" runat="server" ClientInstanceName="dteDtDok" ShowShadow="False"
                 Width="100%">
    <ClientSideEvents DateChanged="function (s,e){DateChanged(s,e);}" GotFocus="function(s, e){ dateGotFocus( s, e); }"/>
    <DropDownButton>
        <Image>
            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"/>
        </Image>
    </DropDownButton>
    <CalendarProperties>
        <HeaderStyle Spacing="1px"/>
        <FooterStyle Spacing="17px"/>
    </CalendarProperties>
    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                        ValidationGroup="entries" SetFocusOnError="true">
        <ErrorFrameStyle ImageSpacing="4px">
            <ErrorTextPaddings PaddingLeft="4px"/>
        </ErrorFrameStyle>
        <RequiredField IsRequired="true"/>
    </ValidationSettings>
    <DisabledStyle Font-Bold="False">
    </DisabledStyle>
</dx:ASPxDateEdit>
<dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMonedha" ID="lblMonedha" runat="server"
              Text="Monedha:" ClientInstanceName="lblMonedha">
</dx:ASPxLabel>
<dx:ASPxComboBox ID="cmbMonedha" runat="server" ClientInstanceName="cmbMonedha"
                 ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top">
    <ClientSideEvents SelectedIndexChanged="function(s,e){SelectedIndexChangedMonedha();}"/>
    <DropDownButton>
        <Image>
            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"/>
        </Image>
    </DropDownButton>
    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                        ValidationGroup="entries" SetFocusOnError="true">
        <ErrorFrameStyle ImageSpacing="4px">
            <ErrorTextPaddings PaddingLeft="4px"/>
        </ErrorFrameStyle>
        <RequiredField ErrorText="*" IsRequired="True"/>
    </ValidationSettings>
    <DisabledStyle Font-Bold="False">
    </DisabledStyle>
</dx:ASPxComboBox>
<dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime" ID="lblShenime" runat="server"
              Text="Shenime" ClientInstanceName="lblShenime">
</dx:ASPxLabel>
<dx:ASPxMemo ID="txtShenime" runat="server" ClientInstanceName="txtShenime" Rows="3"
             Columns="22" Width="100%">
    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                        ValidateOnLeave="false">
        <ErrorFrameStyle ImageSpacing="4px">
            <ErrorTextPaddings PaddingLeft="4px"/>
        </ErrorFrameStyle>
        <RequiredField IsRequired="true"/>
    </ValidationSettings>
    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
    </DisabledStyle>
    <ClientSideEvents LostFocus="function(s,e){myJQGrid.focusGrid({emergride: '#rowed5', isLidhur: lidhur, idKoloneGrideFokus: arrayIdKolonaGrides[0]});}" TextChanged="function (s,e){pershkrimi();}"/>
</dx:ASPxMemo>

<dx:ASPxButton ID="btnImporti" runat="server" Text="Rimerr vlerat e importuara" ClientInstanceName="btnImporti" AutoPostBack="false" CausesValidation="false">
    <ClientSideEvents Click="btnImportiClick"/>
</dx:ASPxButton>
</div>
<br/>
<br/>
<br/>
<div id="divgride1" style="display: none">
    <div id="divgride2">
        <table id="rowed5">
        </table>
    </div>
</div>
<br/>
<br/>
<br/>
<table id="tblFund" class="renditKontrolle3" align="right">
    <tbody>

    </tbody>
</table>
<br/>
<div id="dvFundi" class="atributeDiveFshehur">
    <%--<div id="dvlblDtRegjistrimi">--%>
    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtRegjistrimi" ID="lblDtRegjistrimi"
                  runat="server" Text="Dt Regjistrimi:" ClientInstanceName="lblDtRegjistrimi">
    </dx:ASPxLabel>
    <%--</div>--%>
    <%--<div id="dvdteDtRegjistrimi">--%>
    <dx:ASPxDateEdit ID="dteDtRegjistrimi" runat="server" ClientInstanceName="dteDtRegjistrimi"
                     ShowShadow="False" Width="100%">
        <DropDownButton>
            <Image>
                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"/>
            </Image>
        </DropDownButton>
        <CalendarProperties>
            <HeaderStyle Spacing="1px"/>
            <FooterStyle Spacing="17px"/>
        </CalendarProperties>
        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                            ValidationGroup="entries" SetFocusOnError="true">
            <ErrorFrameStyle ImageSpacing="4px">
                <ErrorTextPaddings PaddingLeft="4px"/>
            </ErrorFrameStyle>
            <RequiredField IsRequired="true"/>
        </ValidationSettings>
        <DisabledStyle Font-Bold="False">
        </DisabledStyle>
    </dx:ASPxDateEdit>
    <%--</div>--%>
    <%--<div id="dvlblTotali">--%>
    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVlefta" ID="lblTotali" runat="server"
                  Text="Totali" ClientInstanceName="lblTotali">
    </dx:ASPxLabel>
    <%--</div>--%>
    <%--<div id="dvtxtVlefta">--%>
    <dx:ASPxTextBox ID="txtVlefta" runat="server" ClientInstanceName="txtVlefta" Width="100%">
        <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                          LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}"/>
        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                            ValidateOnLeave="false">
            <ErrorFrameStyle ImageSpacing="4px">
                <ErrorTextPaddings PaddingLeft="4px"/>
            </ErrorFrameStyle>
            <RequiredField IsRequired="true"/>
        </ValidationSettings>
        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
        </DisabledStyle>
    </dx:ASPxTextBox>
    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVlefta2" ID="lblTotali2" runat="server"
                  Text="Totali" ClientInstanceName="lblTotali2">
    </dx:ASPxLabel>
    <%--</div>--%>
    <%--<div id="dvtxtVlefta">--%>
    <dx:ASPxTextBox ID="txtVlefta2" runat="server" ClientInstanceName="txtVlefta2" Width="100%">
        <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                          LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}"/>
        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                            ValidateOnLeave="false">
            <ErrorFrameStyle ImageSpacing="4px">
                <ErrorTextPaddings PaddingLeft="4px"/>
            </ErrorFrameStyle>
            <RequiredField IsRequired="true"/>
        </ValidationSettings>
        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
        </DisabledStyle>
    </dx:ASPxTextBox>
    <%--</div>--%>
</div>
</asp:Panel>
</dx:SplitterContentControl>
</ContentCollection>
</dx:SplitterPane>
</Panes>
<ClientSideEvents PaneResized="function(s, e) { spliterPaneCollapsed(s,e);}"/>
<Styles>
</Styles>
<Images>
</Images>
</dx:ASPxSplitter >
<asp:HiddenField ID="hfSkema" runat="server"/>
<asp:HiddenField ID="hfPerdoruesi" runat="server"/>
<asp:HiddenField ID="hfTeDrejtaModSkema" runat="server"/>
<asp:HiddenField ID="HfKonfAmb" runat="server"/>
<asp:HiddenField ID="HfTrupiDok" runat="server" />
 <asp:HiddenField ID="HfVleraTeRillogaritura" runat="server" />
<asp:HiddenField ID="hfShtimModifikim" runat="server"/>
<asp:HiddenField ID="HiddenField1" runat="server"/>
<asp:HiddenField ID="HFStatusiDokumentit" runat="server"/>
<asp:HiddenField ID="hfKolonaGride" runat="server"/>
<asp:HiddenField ID="HfGridCol" runat="server"/>
<asp:HiddenField ID="hfKontabilizimi" runat="server"/>
<asp:HiddenField ID="hfKonffillestar" runat="server"/>
<%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
<asp:HiddenField ID="hfGridaKodi" runat="server"/>
<asp:HiddenField ID="hfKontrolletNrAutom" runat="server"/>
<asp:HiddenField ID="hfAtributeNrAutom" runat="server"/>
<dx:ASPxHiddenField ID="hfNrAutoShitje" runat="server" ClientInstanceName="hfNrAutoShitje">
</dx:ASPxHiddenField>
<dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
</dx:ASPxHiddenField>
</div>
<dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
</dx:ASPxHiddenField>
<dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
</dx:ASPxHiddenField>
<br/>
<br/>
<div style="visibility: hidden">
    <dx:ASPxLabel ID="lblPeriudhaAktuale" ClientInstanceName="lblPeriudhaAktuale" runat="server"
                  Text="">
    </dx:ASPxLabel>
</div>
<dx:ASPxHiddenField ID="hfPeriudhaKontabel" ClientInstanceName="hfPeriudhaKontabel" runat="server"></dx:ASPxHiddenField>
<asp:HiddenField ID="hfArkivaDokId" runat="server" />
<asp:HiddenField ID="hfKlient" runat="server" />
<dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
</dx:ASPxHiddenField>
<div>
    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" HeaderStyle-Paddings-Padding="0" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                         CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                         Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                         AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
        <ClientSideEvents CloseUp="function(s, e) { closePopup(s,e);}"/>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl >
</div>
</form>
</body>
</html>