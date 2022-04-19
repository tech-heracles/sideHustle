<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZbritjeAnalitike.aspx.cs"
    Inherits="PlatinumWeb.ZbritjeAnalitike" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
     Namespace="DevExpress.Web" TagPrefix="dx" %>



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    
   <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/ZbritjeAnalitike.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
          
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
        </asp:ScriptManager>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>

                        <td>
                             <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" AutoPostBack="true" ClientInstanceName="ASPxMenu1"                           ItemImagePosition="Top" Width="100%" ShowPopOutImages="True"  >
                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                    <SubMenuItemImage Height="16px" Width="16px">
                                    </SubMenuItemImage>
                                    <SubMenuItemStyle Width="32px">
                                    </SubMenuItemStyle>
                                    <ItemImage Height="32px" Width="32px">
                                    </ItemImage>
                                    <ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" VerticalAlign="Middle">
                                        <Paddings PaddingBottom="1px" PaddingTop="9px" />
                                    </ItemStyle>
                               
                                 <ClientSideEvents ItemClick="zbritjet.handlers.menu_click" Init="function(s) {s.SetClientVisible(true);}" />
                            </dx:ASPxMenu>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <dx:ASPxMenu ID="MenuInfo" runat="server" BorderBetweenItemAndSubMenu="HideRootOnly"
                                        ClientIDMode="AutoID" ClientInstanceName="MenuInfo" ShowPopOutImages="True" Width="100%">
                                        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <SubMenuStyle GutterWidth="17px" />
                                    </dx:ASPxMenu>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                    ClientInstanceName="popFshi" CloseAction="CloseButton" CssPostfix="Glass" EnableAnimation="False"
                    EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" Width="300px">
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                            <table width="100%">
                                <tr>
                                    <td colspan="3">
                                        <dx:ASPxLabel ID="lblMsgbox" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?"
                                            ClientInstanceName="lblMsgbox">
                                        </dx:ASPxLabel>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%"></td>
                                    <td style="width: 40%" align="right">
                                        <dx:ASPxButton ID="ButtonOk" runat="server"  AutoPostBack="false" CausesValidation="False" ClientInstanceName="ButtonOk"
                                         Text="Ok">
                                            <ClientSideEvents Click="zbritjet.handlers.okClick" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width: 40%" align="right">
                                        <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo"
                                            AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) { popFshi.Hide(); Utils.hiqLoadingGif(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>

                            <%--</dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>--%>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="divgride1" style="display: none">
            <table>
                <tr>
                    <td class="renditKontrolleCaption" style="width: 3%">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="lblKonfigurimi"
                            runat="server" Text="Modeli:" ClientInstanceName='lblKonfigurimi' class="klasePerLblKonfigurimi">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 20%; padding: 2px 2px 2px 3px; border-collapse: collapse">
                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                            ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top" AnimationType="None">
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ClientSideEvents SelectedIndexChanged="zbritjet.handlers.cmbKonfigurimiChanged" />
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="renditKontrolleLabelMeWidth25">
                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimiP" runat="server" Text="" class="klasePerLblKonfigurimi"
                            ClientInstanceName="lblKonfigurimiP" >
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth50"></td>
                </tr>
            </table>
            <div id="dvVlera">
                <table id="tblVlera" class="renditKontrolle">
                    <tbody>
                    </tbody>
                </table>
                
                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbVlerePerqindje" ID="lblVlerePerqindje"
                    runat="server" Text="Vlere/Perqindje" ClientInstanceName="lblVlerePerqindje">
                </dx:ASPxLabel>
                <dx:ASPxComboBox ID="cmbVlerePerqindje" runat="server" ClientInstanceName="cmbVlerePerqindje"
                    ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top">
                    <ClientSideEvents SelectedIndexChanged="zbritjet.handlers.cmbVlerePerqindjeIndexChanged" />
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
                
                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbCmimBazeZbAnalitike" ID="lblCmimBazeZbAnalitike" runat="server"
                    Text="Shfaq cmim baze" ClientInstanceName="lblCmimBazeZbAnalitike">
                </dx:ASPxLabel>
                <dx:ASPxCheckBox ID="cbCmimBazeZbAnalitike" runat="server" ClientInstanceName="cbCmimBazeZbAnalitike" Width="100%">
                    <ClientSideEvents CheckedChanged="cbCmimBazeZbAnalitikeChecked" />
                </dx:ASPxCheckBox>

                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVlera" ID="lblVlera" runat="server"
                    Text="Vlera:" ClientInstanceName="lblVlera">
                </dx:ASPxLabel>
                <dx:ASPxTextBox ID="txtVlera" runat="server" ClientInstanceName="txtVlera" Text="0"
                    ClientVisible="False" Width="100%">
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                        ValidationGroup="entries" SetFocusOnError="true">
                        <ErrorFrameStyle ImageSpacing="4px">
                            <ErrorTextPaddings PaddingLeft="4px" />
                        </ErrorFrameStyle>
                        <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
                <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtFillimi2" ID="lblDtFillimi2"
                    runat="server" Text="Dt.Fillimit:" ClientInstanceName="lblDtFillimi2">
                </dx:ASPxLabel>
                <dx:ASPxDateEdit ID="dteDtFillimi2" runat="server" ClientInstanceName="dteDtFillimi2"
                    ValidationSettings-CausesValidation="True" ShowShadow="False" Width="100%">
                    <CalendarProperties>
                        <HeaderStyle Spacing="1px" />
                        <FooterStyle Spacing="17px" />
                    </CalendarProperties>
                    <DropDownButton>
                        <Image>
                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                        </Image>
                    </DropDownButton>
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                        ValidationGroup="entries">
                        <ErrorFrameStyle ImageSpacing="4px">
                            <ErrorTextPaddings PaddingLeft="4px" />
                        </ErrorFrameStyle>
                    </ValidationSettings>
                </dx:ASPxDateEdit>
                <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtMbarimi2" ID="lblDtMbarimi2"
                    runat="server" Text="Dt.Mbarimit:" ClientInstanceName="lblDtMbarimi2">
                </dx:ASPxLabel>
                <dx:ASPxDateEdit ID="dteDtMbarimi2" runat="server" ClientInstanceName="dteDtMbarimi2"
                    ValidationSettings-CausesValidation="True" ShowShadow="False" Width="100%">
                    <CalendarProperties>
                        <HeaderStyle Spacing="1px" />
                        <FooterStyle Spacing="17px" />
                    </CalendarProperties>
                    <DropDownButton>
                        <Image>
                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                        </Image>
                    </DropDownButton>
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                        ValidationGroup="entries">
                        <ErrorFrameStyle ImageSpacing="4px">
                            <ErrorTextPaddings PaddingLeft="4px" />
                        </ErrorFrameStyle>
                    </ValidationSettings>
                </dx:ASPxDateEdit> 
                 
                
                <dx:ASPxButton ID="btnNdrysho" runat="server" Text="Ndrysho" ClientInstanceName="btnNdrysho"
                    AutoPostBack="False" CausesValidation="False" Width="100%">
                    <ClientSideEvents Click="zbritjet.handlers.NdryshoClicked" />
                </dx:ASPxButton>
               
            </div>
            <hr />
            <table id="tblButonat">
                <tr>

                    <td>
                        <dx:ASPxLabel Wrap="False" ID="lblSelektuar" runat="server" Text="0" class="klasePerLblKonfigurimi"
                            ClientInstanceName="lblSelektuar">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel Wrap="False" ID="lblSelektuar2" runat="server" Text="rreshta te selektuar"
                            ClientInstanceName="lblSelektuar2">
                        </dx:ASPxLabel>
                    </td>
                </tr>
            </table>


            <dx:ASPxGridView ID="gvZbritjeAnalitike" runat="server"  ClientInstanceName="gvZbritjeAnalitike"
                Width="100%" OnCustomCallback="gvZbritjeAnalitike_CustomCallback" OnBatchUpdate="gvZbritjeAnalitike_BatchUpdate"
                OnCustomJSProperties="gvZbritjeAnalitike_CustomJSProperties" OnDataBound="gvZbritjeAnalitike_DataBound"
                ClientIDMode="AutoID" OnHtmlRowPrepared="gvZbritjeAnalitike_OnHtmlRowPrepared" OnAfterPerformCallback="gvZbritjeAnalitike_AfterPerformCallback" >
                <SettingsBehavior  ProcessFocusedRowChangedOnServer="false"  />

                <ClientSideEvents BeginCallback="zbritjet.handlers.beginCallbackGrida" EndCallback="zbritjet.handlers.endCallbackGrida" BatchEditStartEditing="zbritjet.handlers.BatchEditStartEditingGrida" BatchEditEndEditing="zbritjet.handlers.BatchEditEndEditingGrida" BatchEditRowValidating="zbritjet.handlers.BatchEditRowValidatingGrida"  BatchEditConfirmShowing="zbritjet.handlers.BatchEditConfirmUpdating" SelectionChanged="zbritjet.handlers.SelectionChangedGrida" />
                <Templates>
                    <StatusBar>
                    </StatusBar>
                    <FooterCell></FooterCell>
                </Templates>
            </dx:ASPxGridView>
            <hr />

        </div>
        <dx:ASPxPopupControl ID="popupUniversal" runat="server" AllowDragging="True" AllowResize="True"
            AppearAfter="10" ClientIDMode="AutoID" ClientInstanceName="popupUniversal" CloseAction="CloseButton"
            EnableAnimation="False" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter">
            <ClientSideEvents Closing="" />
            <ContentStyle>
                <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                    PaddingTop="1px" />
            </ContentStyle>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
                <asp:HiddenField ID="hfKontrollet" runat="server" />
 
    </form>
</body>
</html>
