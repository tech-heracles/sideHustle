<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GjenerimAutomatik.aspx.cs" Inherits="PlatinumWeb.GjenerimAutomatik" %>


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

    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/aspx.js/GjenerimAutomatik.aspx-IMB.4.6.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
         
        <dx:ASPxLoadingPanel ID="LoadingPanel" ContainerElementID="UpdatePanel1" runat="server"
            ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
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
                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                        ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                        OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                        <ClientSideEvents  ItemClick="function(s, e) { menuClick(s,e);}" Init="function(s) {s.SetClientVisible(true);}" />
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
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>


            <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="730px" ClientInstanceName="splitter"
                PaneMinSize="700px">
                <Panes>
                    <%-- Header pane--%>
                    <dx:SplitterPane PaneStyle-BackColor="Transparent" Separators-Size="10px" ScrollBars="Vertical">
                        <Separators Size="10px">
                        </Separators>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                <dx:ASPxRoundPanel EnableHierarchyRecreation="false" runat="server" ID="ASPxRoundPanel1" ClientIDMode="AutoID" GroupBoxCaptionOffsetY="-28px"
                                    HeaderText="" Width="100%">
                                    <ContentPaddings Padding="14px" />
                                    <PanelCollection>
                                        <dx:PanelContent>
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2">
                                                        <dx:ASPxLabel ID="lblKonfigurimi" runat="server" Text="a" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                            runat="server" ClientIDMode="AutoID" Text="Modeli:">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None"
                                                            ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top">
                                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" Init="function(s,e){ndryshoKonfigurimin()}" />
                                                            <LoadingPanelImage>
                                                            </LoadingPanelImage>
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



                                                    <td>
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojDate" ID="lblLlojDate" runat="server"
                                                            Text="Lloj date">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="cmbLlojDate" runat="server" ShowShadow="False" Width="100%"
                                                            ClientInstanceName="cmbLlojDate" SettingsLoadingPanel-ImagePosition="Top">
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
                                                    <td>
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="dtePeriudhaNga" ID="lblPeriudha"
                                                            runat="server" Text="Nga">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxDateEdit ID="dtePeriudhaNga" runat="server" ClientInstanceName="dtePeriudhaNga" ShowShadow="False" Width="100%">
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
                                                    <td>
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="dtePeriudhaDeri" ID="lblDeri" runat="server"
                                                            Text="Deri">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxDateEdit ID="dtePeriudhaDeri" runat="server" ClientInstanceName="dtePeriudhaDeri" ShowShadow="False" Width="100%">
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
                                                    <td width="10%">
                                                        <asp:UpdatePanel runat="server" ID="pnlkerko">
                                                            <ContentTemplate>
                                                                <dx:ASPxButton ID="btnKerko" runat="server" Text="Kerko" ClientInstanceName="btnKerko"
                                                                    AutoPostBack="false" ValidationGroup="entries" Width="100%">
                                                                    <ClientSideEvents Click="function(s, e) { kerko(); }" />
                                                                </dx:ASPxButton>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>

                                                </tr>
                                            </table>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                <ContentTemplate>




                                                    <dx:ASPxGridView ID="gvGjenerimi" runat="server" ClientInstanceName="gvGjenerimi"
                                                        OnDataBound="gvGjenerimi_DataBound" OnAfterPerformCallback="gvGjenerimi_AfterPerformCallback"
                                                        Width="100%" OnCustomCallback="gvGjenerimi_CustomCallback" OnCustomJSProperties="gvGjenerimi_CustomJSProperties">
                                                        <SettingsBehavior AllowSelectByRowClick="True" />
                                                        <SettingsLoadingPanel ImagePosition="Top" />
                                                        <Settings ShowVerticalScrollBar="true"  UseFixedTableLayout="true" />
                                                        <ClientSideEvents SelectionChanged="function(s, e) { SelectionChange(s,e);  
}"
                                                            EndCallback="function (s,e){ gvGjenerimi2.PerformCallback();
                                                            }" />
                                                        <Styles>
                                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                            </Header>
                                                        </Styles>
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

                                                    <br />
                                                    <br />
                                                    <dx:ASPxGridView ID="gvGjenerimi2" runat="server" ClientInstanceName="gvGjenerimi2"
                                                        OnDataBound="gvGjenerimi2_DataBound" OnAfterPerformCallback="gvGjenerimi2_AfterPerformCallback"
                                                        Width="100%" OnCustomCallback="gvGjenerimi2_CustomCallback" OnHtmlRowCreated="gvGjenerimi2_HtmlRowCreated" OnCustomJSProperties="gvGjenerimi2_CustomJSProperties">
                                                        <SettingsBehavior AllowSelectByRowClick="True" />
                                                        <SettingsLoadingPanel ImagePosition="Top" />
                                                        <Settings ShowVerticalScrollBar="true"  UseFixedTableLayout="true"  />
                                                        <ClientSideEvents SelectionChanged="function(s, e) {
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
                                                    <iframe id="Container" runat="server" frameborder="0" name="Container" height="0"
                                                        width="0"></iframe>
                                                    <asp:HiddenField ID="hfTeDrejtaGjitheDok" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxRoundPanel >
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
                <Styles>
                </Styles>
                <Images>
                </Images>
            </dx:ASPxSplitter >
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
