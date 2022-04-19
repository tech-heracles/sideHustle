<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ABPlanifikimRealizim.aspx.cs" Inherits="PlatinumWeb.ABPlanifikimRealizim" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>















<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
        <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/ABPlanifikimRealizim.aspx-IMB.6.3.js&v76""
        type="text/javascript"></script>
</head>
<body>
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
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server"
                                ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                SeparatorWidth="1px">
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                <ClientSideEvents ItemClick="ItemClickMenu" Init="function(s) {s.SetClientVisible(true);}" />
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
                                            <ClientSideEvents Init="MenuInfoInit" />
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
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="dvAktiviteti" style="display: block;">
             <table class="renditKontrolle">
                <tbody>
                    <tr>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                runat="server" Style="font-size: large" Text="Modeli:">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth33">
                            <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                Height="24px" Width="100%" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False"
                                Style="font-size: medium" AnimationType="None">
                                <ClientSideEvents SelectedIndexChanged="cmbKonfigurimiSelectedIndexChanged" />
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
                            <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi"
                                ClientInstanceName="lblKonfigurimi" >
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth33"></td>
                    </tr>
                </tbody>
            </table>
             <dx:ASPxGridView ID="gvPlanifikim"
                Width="100%" ToolTip="Planifikim dhe realizim" runat="server"
                OnBatchUpdate="gvPlanifikim_BatchUpdate"
                OnAfterPerformCallback="gvPlanifikim_AfterPerformCallback" OnHtmlFooterCellPrepared="gvPlanifikim_HtmlFooterCellPrepared"
                OnCustomCallback="gvPlanifikim_CustomCallback"  OnDataBound="gvPlanifikim_DataBound" >
                <ClientSideEvents BeginCallback="gvBeginCallback" EndCallback="gvEndCallback" BatchEditStartEditing="StartEditing" BatchEditEndEditing="EndEditing" />

                <Styles>
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                </Styles>
                <SettingsPager PageSize="15">
                </SettingsPager>
                <ClientSideEvents />
                <StylesEditors>
                    <CalendarHeader Spacing="1px">
                    </CalendarHeader>
                    <ProgressBar Height="25px">
                    </ProgressBar>
                </StylesEditors>

                <Templates>
                    <StatusBar>
                    </StatusBar>
                    <FooterCell></FooterCell>
                    
                </Templates>
            </dx:ASPxGridView>
        </div>

        <asp:HiddenField ID="HfKonfAmb" runat="server" />
        <asp:HiddenField ID="hfShtimModifikim" runat="server" />
        <asp:HiddenField ID="hfKolonaGride" runat="server" />
        <asp:HiddenField ID="hfKonffillestar" runat="server" />
        <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
        <asp:HiddenField ID="hfGridaKodi" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta"></dx:ASPxHiddenField>
    </form>
</body>
</html>
