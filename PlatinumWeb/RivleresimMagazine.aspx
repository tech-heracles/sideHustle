<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RivleresimMagazine.aspx.cs"
    Inherits="PlatinumWeb.RivleresimMagazine" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <title>Alpha Web</title>
    <%--    <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/myMesazh-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myButtonClickLupa-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/aspx.js/RivleresimMagazine.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script>--%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/aspx.js/RivleresimMagazine.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true">
        </dx:ASPxHiddenField>
        <div>
            <asp:UpdatePanel runat="server" ID="pnl">
                <ContentTemplate>
                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                        ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                        OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                        <ClientSideEvents Init="function(s, e) { s.SetClientVisible(true); menuInit(s,e);}" ItemClick="function(s, e) { menuClick(s,e);}" />
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <eo:ProgressBar ID="ProgressBar1" runat="server" Width="100%" OnRunTask="ProgressBar1_RunTask"
                ClientSideOnError="onTaskError" ClientSideOnEvalVar="onTaskRunning" ClientSideOnMessage="onTaskRunning" ClientSideOnUnload="onTaskRunning" ClientSideOnLoad="onTaskRunning" ClientSideOnTaskDone="onTaskDone" BorderColor="Black" ClientSideOnValueChanged="onTaskRunning"
                BorderStyle="Solid" BorderWidth="1px" ControlSkinID="None" IndicatorColor="LightBlue"
                ShowPercentage="True" Height="18px">
            </eo:ProgressBar>
            <asp:UpdatePanel runat="server" ID="pnlProgressBar" UpdateMode="Conditional">
                <ContentTemplate>
                    <%--<dx:ASPxProgressBar ID="prbRivleresim" runat="server" Height="25px" ClientInstanceName="prbRivleresim"
                    Width="100%"    
                    EnableClientSideAPI="True">
                </dx:ASPxProgressBar>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="730px" ClientInstanceName="splitter"
                PaneMinSize="700px">
                <Panes>
                    <%-- Header pane--%>
                    <dx:SplitterPane PaneStyle-BackColor="Transparent" Separators-Size="10px" ScrollBars="Auto">
                        <Separators Size="10px">
                        </Separators>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                <dx:ASPxRoundPanel EnableHierarchyRecreation="false" runat="server" ID="ASPxRoundPanel1" ClientIDMode="AutoID" GroupBoxCaptionOffsetY="-28px"
                                    HeaderText="Rivleresim magazine" Width="100%">
                                    <ContentPaddings Padding="14px" />
                                    <PanelCollection>
                                        <dx:PanelContent>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                <ContentTemplate>

                                                    <dx:ASPxLoadingPanel ID="LoadingPanel" ContainerElementID="UpdatePanel1" runat="server"
                                                        ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top">
                                                        <LoadingDivStyle Opacity="30">
                                                        </LoadingDivStyle>
                                                    </dx:ASPxLoadingPanel>
                                                    <table class="renditKontrolle">
                                                        <tr>
                                                            <td class="renditKontrolleCaption">
                                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMagazina" ID="lblMagazina" runat="server"
                                                                    Text="Magazina">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td class="renditKontrolleCellMeWidth33">
                                                                <dx:ASPxComboBox ID="cmbMagazina" runat="server" ShowShadow="False" Width="100%"
                                                                    ClientInstanceName="cmbMagazina" SettingsLoadingPanel-ImagePosition="Top">
                                                                    <DropDownButton>
                                                                        <Image>
                                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                                        </Image>
                                                                    </DropDownButton>
                                                                    <ClientSideEvents ButtonClick="function(s,e){ButtonClickMagazina();}" LostFocus="function(s,e){LostFocus();}" />
                                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </dx:ASPxComboBox>
                                                            </td>
                                                            <td class="renditKontrolleCaption">
                                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="dtePeriudhaNga" ID="lblPeriudha"
                                                                    runat="server" Text="Periudha">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td class="renditKontrolleCellMeWidth33">
                                                                <dx:ASPxDateEdit ID="dtePeriudhaNga" ClientInstanceName="dtePeriudhaNga" 
                                                                    runat="server" ShowShadow="False" Width="100%">
                                                                    <ClientSideEvents DateChanged="function (s,e){dtePeriudhaChanged(s,e);}" />
                                                                    <CalendarProperties>
                                                                        <HeaderStyle Spacing="1px" />
                                                                        <FooterStyle Spacing="17px" />
                                                                    </CalendarProperties>
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
                                                                </dx:ASPxDateEdit>
                                                            </td>
                                                            <td style="padding: 2px 7px 2px 7px;">
                                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="dtePeriudhaDeri" ID="lblDeri" runat="server"
                                                                    Text=" - ">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td class="renditKontrolleCellMeWidth33">
                                                                <dx:ASPxDateEdit ID="dtePeriudhaDeri" ClientInstanceName="dtePeriudhaDeri"
                                                                    runat="server" ShowShadow="False" Width="100%">
                                                                     <ClientSideEvents DateChanged="function (s,e){dtePeriudhaChanged(s,e);}" />
                                                                    <CalendarProperties>
                                                                        <HeaderStyle Spacing="1px" />
                                                                        <FooterStyle Spacing="17px" />
                                                                    </CalendarProperties>
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
                                                                </dx:ASPxDateEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton ID="btnZgjidhGjitha" runat="server" ToolTip="Zgjidh të gjitha në këtë faqe"
                                                                    ClientInstanceName="btnZgjidhGjitha" Image-Url="images/check2.png" Image-Height="16px" AutoPostBack="False" CausesValidation="False">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
              }"
                                                                        Click="function(s, e) {  KlikoTeGjitha();
    }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe"
                                                                    AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                                    <ClientSideEvents Click="function(s, e) { gvRivleresim.SelectRows(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="gidaSelectMbrapa" runat="server" ToolTip="Zgjidh mbrapa"
                                                                    AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                                    <ClientSideEvents Click="function(s, e) { gvRivleresim.PerformCallback('ZGJIDHMBRAPA'); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="btnHiqZgjedhjen" runat="server" ToolTip="Hiq zgjedhjen në këtë faqe"
                                                                    ClientInstanceName="btnHiqZgjedhjen" Image-Url="images/uncheck2.png" Image-Height="16px" AutoPostBack="False" CausesValidation="False">
                                                                    <ClientSideEvents Click="function(s, e) {  HiqTeGjitha();
    }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <dx:ASPxGridView ID="gvRivleresim" runat="server" ClientInstanceName="gvRivleresim"
                                                        OnDataBound="gvRivleresim_DataBound" OnAfterPerformCallback="gvRivleresim_AfterPerformCallback"
                                                        Width="100%" OnCustomCallback="gvRivleresim_CustomCallback" OnCustomJSProperties="gvRivleresim_CustomJSProperties">
                                                        <SettingsBehavior AllowSelectByRowClick="True" />
                                                        <SettingsLoadingPanel ImagePosition="Top" />
                                                        <ClientSideEvents RowDblClick="function(s, e) { gvRivleresimRowDblClick(s,e); }" />
                                                        <ImagesEditors>
                                                            <DropDownEditDropDown>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </DropDownEditDropDown>
                                                            <SpinEditIncrement>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditIncrementImageHover_Aqua"
                                                                    PressedCssClass="dxEditors_edtSpinEditIncrementImagePressed_Aqua" />
                                                            </SpinEditIncrement>
                                                            <SpinEditDecrement>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditDecrementImageHover_Aqua"
                                                                    PressedCssClass="dxEditors_edtSpinEditDecrementImagePressed_Aqua" />
                                                            </SpinEditDecrement>
                                                            <SpinEditLargeIncrement>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeIncImageHover_Aqua"
                                                                    PressedCssClass="dxEditors_edtSpinEditLargeIncImagePressed_Aqua" />
                                                            </SpinEditLargeIncrement>
                                                            <SpinEditLargeDecrement>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeDecImageHover_Aqua"
                                                                    PressedCssClass="dxEditors_edtSpinEditLargeDecImagePressed_Aqua" />
                                                            </SpinEditLargeDecrement>
                                                        </ImagesEditors>
                                                        <Styles>
                                                            <LoadingPanel ImageSpacing="8px">
                                                            </LoadingPanel>
                                                        </Styles>
                                                        <StylesEditors>
                                                            <CalendarHeader Spacing="1px">
                                                            </CalendarHeader>
                                                            <ProgressBar Height="25px">
                                                            </ProgressBar>
                                                        </StylesEditors>
                                                    </dx:ASPxGridView>

                                                    <br />
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 30%"></td>
                                                            <td>
                                                                <dx:ASPxButton ID="btnDjathtas1" runat="server" Text="v" OnClick="btnDjathtas1_Click"
                                                                    Width="50px">
                                                                    <ClientSideEvents Click="function(s, e) {   pastro();
                                                              
	       
    }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="btnDjathtasGjitha" runat="server" Text="vv" OnClick="btnDjathtasGjitha_Click"
                                                                    Width="50px">
                                                                    <ClientSideEvents Click="function(s, e) {   pastro();
                                                              
	       
    }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="btnMajtas1" runat="server" Text="^" OnClick="btnMajtas1_Click"
                                                                    Width="50px">
                                                                    <ClientSideEvents Click="function(s, e) {   pastro();
                                                              
	       
    }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="btnMajtaGjitha" runat="server" Text="^^" OnClick="btnMajtaGjitha_Click"
                                                                    Width="50px">
                                                                    <ClientSideEvents Click="function(s, e) {   pastro();
                                                              
	       
    }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td style="width: 30%"></td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton ID="btnZgjidhGjitha2" runat="server" ToolTip="Zgjidh të gjitha në këtë faqe"
                                                                    ClientInstanceName="btnZgjidhGjitha2" AutoPostBack="False" CausesValidation="False" Image-Url="images/check2.png" Image-Height="16px">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
              }"
                                                                        Click="function(s, e) {  KlikoTeGjitha2();
    }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="ASPxButton1" runat="server" ToolTip="Zgjidh te gjithe"
                                                                    AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                                    <ClientSideEvents Click="function(s, e) { gvRivleresim2.SelectRows(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="gidaSelectMbrapa2" runat="server" ToolTip="Zgjidh mbrapa"
                                                                    AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                                    <ClientSideEvents Click="function(s, e) { gvRivleresim2.PerformCallback('ZGJIDHMBRAPA'); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="btnHiqZgjedhjen2" runat="server" ToolTip="Hiq zgjedhjen në këtë faqe"
                                                                    ClientInstanceName="btnHiqZgjedhjen2" AutoPostBack="False" CausesValidation="False" Image-Url="images/uncheck2.png" Image-Height="16px">
                                                                    <ClientSideEvents Click="function(s, e) {  HiqTeGjitha2();
    }" />
                                                                </dx:ASPxButton>
                                                            </td>

                                                            <td>
                                                                <dx:ASPxLabel Wrap="False" ID="lblArtikujtPerRivleresim" runat="server"
                                                                    Text="Artikujt per rivlerësim" ClientInstanceName="lblArtikujtPerRivleresim">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <dx:ASPxGridView ID="gvRivleresim2" runat="server" ClientInstanceName="gvRivleresim2"
                                                        OnDataBound="gvRivleresim2_DataBound" OnAfterPerformCallback="gvRivleresim2_AfterPerformCallback"
                                                        Width="100%" OnCustomCallback="gvRivleresim2_CustomCallback" OnCustomJSProperties="gvRivleresim2_CustomJSProperties">
                                                        <SettingsBehavior AllowSelectByRowClick="True" />
                                                        <SettingsLoadingPanel ImagePosition="Top" />
                                                        <ClientSideEvents SelectionChanged="function(s, e) { SelectionChange2(s,e);
    }" />
                                                        <ImagesEditors>
                                                            <DropDownEditDropDown>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </DropDownEditDropDown>
                                                            <SpinEditIncrement>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditIncrementImageHover_Aqua"
                                                                    PressedCssClass="dxEditors_edtSpinEditIncrementImagePressed_Aqua" />
                                                            </SpinEditIncrement>
                                                            <SpinEditDecrement>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditDecrementImageHover_Aqua"
                                                                    PressedCssClass="dxEditors_edtSpinEditDecrementImagePressed_Aqua" />
                                                            </SpinEditDecrement>
                                                            <SpinEditLargeIncrement>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeIncImageHover_Aqua"
                                                                    PressedCssClass="dxEditors_edtSpinEditLargeIncImagePressed_Aqua" />
                                                            </SpinEditLargeIncrement>
                                                            <SpinEditLargeDecrement>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeDecImageHover_Aqua"
                                                                    PressedCssClass="dxEditors_edtSpinEditLargeDecImagePressed_Aqua" />
                                                            </SpinEditLargeDecrement>
                                                        </ImagesEditors>
                                                        <Styles>
                                                            <LoadingPanel ImageSpacing="8px">
                                                            </LoadingPanel>
                                                        </Styles>
                                                        <StylesEditors>
                                                            <CalendarHeader Spacing="1px">
                                                            </CalendarHeader>
                                                            <ProgressBar Height="25px">
                                                            </ProgressBar>
                                                        </StylesEditors>
                                                    </dx:ASPxGridView>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxRoundPanel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
                <Styles>
                </Styles>
                <Images>
                </Images>
            </dx:ASPxSplitter>

            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" AllowResize="True"
                AppearAfter="10" ClientIDMode="AutoID" ClientInstanceName="popupUniversal" CloseAction="CloseButton"
                EnableAnimation="False" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter">
                <ClientSideEvents Closing="function(s, e) { popupUniversal.SetContentUrl(''); }" />
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
