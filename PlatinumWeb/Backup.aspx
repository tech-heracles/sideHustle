<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Backup.aspx.cs" Inherits="PlatinumWeb.Backup" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>










<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
     <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Backup.aspx-IMB.2.1.js&v76"" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="End_Callback" />--%>
    </dx:ASPxGlobalEvents>
    <div>
        <asp:UpdatePanel runat="server" ID="pnl">
            <ContentTemplate>
                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                    OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                    <ClientSideEvents ItemClick=" Item_Click" Init="function(s) {s.SetClientVisible(true);}" />
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
       
                <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel"
                    Font-Size="9pt" Modal="True" ImagePosition="Top">
                    <LoadingDivStyle Opacity="30">
                    </LoadingDivStyle>
                </dx:ASPxLoadingPanel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
              TabSpacing="3px" Width="100%" ActiveTabIndex="0" Height="520px">
            <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
            <TabPages>
                <dx:TabPage Name="Backup" Text="Backup">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl1" runat="server">
                            <asp:UpdatePanel runat="server" ID="pnlback">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnZgjidhGjitha" runat="server" Text="Zgjidh të gjitha në këtë faqe"
                                                    ClientInstanceName="btnZgjidhGjitha" AutoPostBack="False" CausesValidation="False">
                                                    <ClientSideEvents  Click="function(s, e) {  KlikoTeGjitha();
}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnHiqZgjedhjen" runat="server" Text="Hiq zgjedhjen në këtë faqe"
                                                    ClientInstanceName="btnHiqZgjedhjen" AutoPostBack="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {  HiqTeGjitha();
}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnZgjidhKunder" runat="server" Text="Zgjidh të kundërtën në këtë faqe"
                                                    ClientInstanceName="btnZgjidhKunder" AutoPostBack="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
	  kundert();
}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <dx:ASPxGridView ID="gvNdermarjet" runat="server" ClientInstanceName="gvNdermarjet"
                                        OnDataBound="gvNdermarjet_DataBound" OnAfterPerformCallback="gvNdermarjet_AfterPerformCallback"
                                        Width="100%" OnCustomCallback="gvNdermarjet_CustomCallback" OnCustomJSProperties="gvNdermarjet_CustomJSProperties">
                                        <SettingsBehavior AllowSelectByRowClick="True" />
                                        <SettingsLoadingPanel ImagePosition="Top" />
                                        <ClientSideEvents SelectionChanged="function(s, e) {
    SelectionChange(s, e);
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
                                    <br />
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 30%">
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnDjathtas1" runat="server" Text="˅" OnClick="btnDjathtas1_Click"
                                                    Width="50px">
                                                    <ClientSideEvents Click="function(s, e) { pastro(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnDjathtasGjitha" runat="server" Text="˅˅" OnClick="btnDjathtasGjitha_Click"
                                                    Width="50px">
                                                    <ClientSideEvents Click="function(s, e) { pastro(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnMajtas1" runat="server" Text="^" OnClick="btnMajtas1_Click"
                                                    Width="50px">
                                                    <ClientSideEvents Click="function(s, e) { pastro(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnMajtaGjitha" runat="server" Text="^^" OnClick="btnMajtaGjitha_Click"
                                                    Width="50px">
                                                    <ClientSideEvents Click="function(s, e) { pastro(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td style="width: 30%">
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnZgjidhGjitha2" runat="server" Text="Zgjidh të gjitha në këtë faqe"
                                                    ClientInstanceName="btnZgjidhGjitha2" AutoPostBack="False" CausesValidation="False">
                                                    <ClientSideEvents  Click="function(s, e) { KlikoTeGjitha2(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnHiqZgjedhjen2" runat="server" Text="Hiq zgjedhjen në këtë faqe"
                                                    ClientInstanceName="btnHiqZgjedhjen2" AutoPostBack="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function (s, e) { HiqTeGjitha2(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnZgjidhKunde2r" runat="server" Text="Zgjidh të kundërtën në këtë faqe"
                                                    ClientInstanceName="btnZgjidhKunder2" AutoPostBack="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) { kundert2(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <dx:ASPxGridView ID="gvNdermarjet2" runat="server" ClientInstanceName="gvNdermarjet2"
                                        OnDataBound="gvNdermarjet2_DataBound" OnAfterPerformCallback="gvNdermarjet2_AfterPerformCallback"
                                        Width="100%" OnCustomCallback="gvNdermarjet2_CustomCallback" OnCustomJSProperties="gvNdermarjet2_CustomJSProperties">
                                        <SettingsBehavior AllowSelectByRowClick="True" />
                                        <SettingsLoadingPanel ImagePosition="Top" />
                                        <ClientSideEvents SelectionChanged="function(s, e) { SelectionChange2(s, e); }" />
                                        <Images>
                                            <LoadingPanelOnStatusBar>
                                            </LoadingPanelOnStatusBar>
                                            <LoadingPanel>
                                            </LoadingPanel>
                                        </Images>
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
                                    <asp:UpdatePanel runat="server" ID="pnlProgressBar" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <dx:ASPxProgressBar ID="prbNdermarje" runat="server" Height="25px" ClientInstanceName="prbNdermarje"
                                                Width="100%">
                                            </dx:ASPxProgressBar>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <dx:ASPxTimer ID="tmNdermarje" runat="server" ClientInstanceName="tmNdermarje" Interval="1000">
                                        <ClientSideEvents Tick="Tick_Position" Init="Init_tmNdermarje" />
                                    </dx:ASPxTimer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Name="Download" Text="Download" >
                   
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl3" runat="server">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Lista e backupeve tuaja. Klikoni mbi link per ta shkarkuar!"
                                 Font-Bold="True" Font-Size="Medium">
                            </dx:ASPxLabel>
                            <br />
                            <br />
                            <asp:UpdatePanel ID="pnlLidhur" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table id='hl' runat="server">
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
            <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
        </dx:ASPxPageControl >
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
    </div>
    </form>
</body>
</html>
