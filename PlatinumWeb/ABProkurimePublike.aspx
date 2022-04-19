<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ABProkurimePublike.aspx.cs" Inherits="PlatinumWeb.ABProkurimePublike" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
        <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/ABProkurimePublike.aspx-IMB.6.3.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000" EnablePartialRendering="true">
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
                                OnItemClick="ASPxMenu1_ItemClick">
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                <ClientSideEvents ItemClick="gvItemClick" Init="function(s) {s.SetClientVisible(true);}" />
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
                 
                <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                    Font-Size="9pt" Modal="True" ImagePosition="Top">
                    <LoadingDivStyle Opacity="30">
                    </LoadingDivStyle>
                </dx:ASPxLoadingPanel>

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
                                                            OnClick="ButtonOk_Click" Text="Ok">
                                                            <ClientSideEvents Click="ClickOk" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                            <ClientSideEvents Click="ClickCancel" />
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
        <div id="dvDetyra" style="display: none">
            <dx:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server"   TabSpacing="3px"
                ClientInstanceName="PageControl" Width="100%" ActiveTabIndex="0">
                <ClientSideEvents ActiveTabChanged="PageControlTabChanging" />
                <ContentStyle>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                </ContentStyle>
                <TabPages>

                    <dx:TabPage Name="Dokumentat" Text="Lista e Dokumentave">
                        <ContentCollection>
                            <dx:ContentControl>
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
                                                    <ClientSideEvents SelectedIndexChanged="gvSelectedIndexChanged" />
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
                                                    ClientInstanceName="lblKonfigurimi">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33"></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:UpdatePanel ID="pnlGrida" runat="server">
                                    <ContentTemplate>
                                           <dx:ASPxGridView ID="gvListaProkurimet" ClientInstanceName="gvListaProkurimet" ToolTip="Prokurimet" runat="server"
                                    Width="100%" OnDataBound="gvListaProkurimet_DataBound">
                                     <ClientSideEvents RowDblClick="gvListaProkurimetRowDblClick"
                                                FocusedRowChanged="gvListaProkurimetFocusedRowChanged"
                                                BeginCallback="gvListaProkurimetBeginCallback" />
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
                                </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                             
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>

                    <dx:TabPage Name="Dokumenti" Text="Regjistrimi i prokurimeve">
                        <ContentCollection>
                            <dx:ContentControl ID="content">
                                <table id="tblPasqyra" class="renditKontrolle">
                                    <tbody>
                                    </tbody>
                                </table>
                                <div id="divFillim" class="atributeDiveFshehur">
                                    
                                    
                                           
                                         <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrDok" ID="lblNrDok" runat="server"
                                        Text="Kodi" ClientInstanceName="lblNrDok">
                                    </dx:ASPxLabel>
                                    
                                    <dx:ASPxTextBox ID="txtNrDok" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtNrDok">
										
										<ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
											ValidationGroup="entries" SetFocusOnError="True" RegularExpression-ValidationExpression="^[\s\S]{0,20}$"
											RegularExpression-ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere">
											<ErrorFrameStyle ImageSpacing="4px">
												<ErrorTextPaddings PaddingLeft="4px" />
											</ErrorFrameStyle>
											<RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere" ValidationExpression="^[\s\S]{0,20}$"></RegularExpression>
											<RequiredField IsRequired="true" />
										</ValidationSettings>
										<DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
										</DisabledStyle>
									</dx:ASPxTextBox>
                                           
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbPeriudha" ID="lblPeriudha" runat="server"
                                        Text="Periudha" ClientInstanceName="lblPeriudha">
                                    </dx:ASPxLabel>
                                            
                                    <dx:ASPxComboBox ID="cmbPeriudha" runat="server"  EnableSynchronization="True" Width="100%" AutoPostBack="false" ClientInstanceName="cmbPeriudha">
                                        <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true"
                                            Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                  
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>

                                </div>
                                
                              <br />
                                <div id="divGrida" class="atributeDiveFshehur">
                                    
                                        <dx:ASPxRoundPanel EnableHierarchyRecreation="false" runat="server" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" HeaderText="Trupi i dokumentit">
                                    <PanelCollection>
                                        <dx:PanelContent>
                                            

                                             <dx:ASPxGridView ID="gvProkurimet" ClientInstanceName="gvProkurimet" runat="server" OnCustomCallback="gvProkurimet_CustomCallback"
                                    Width="100%" OnDataBound="gvProkurimet_DataBound"  OnBatchUpdate="gvProkurimet_BatchUpdate"  OnCustomErrorText="gvProkurimet_CustomErrorText">
                                                <ClientSideEvents EndCallback="gvEndCallback" BatchEditStartEditing="StartEditing" BatchEditEndEditing="EndEditing" />
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <Templates>
                                        <StatusBar></StatusBar>
                                        <FooterCell></FooterCell>
                                        <GroupRowContent>
                                                 <%# Container.Column.Caption  %> : <%# Container.GroupText %>
                                        </GroupRowContent>

                                     </Templates>
                                                  <SettingsBehavior AutoExpandAllGroups="true" />
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
                                    <ClientSideEvents />
                                    <StylesEditors>
                                        <CalendarHeader Spacing="1px">
                                        </CalendarHeader>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                                 <SettingsLoadingPanel  Mode="Disabled" />
                                </dx:ASPxGridView>



                                        </dx:PanelContent>
                                    </PanelCollection>

                                        </dx:ASPxRoundPanel >
                               
                                    </div>
                                <br />
                               
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl >
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                     <asp:HiddenField ID="HfKonfAmb" runat="server" />
        <asp:HiddenField ID="hfShtimModifikim" runat="server" />
        <asp:HiddenField ID="hfKolonaGride" runat="server" />
        <asp:HiddenField ID="hfKonffillestar" runat="server" />
           <asp:HiddenField ID="hfStatusVeprimi" runat="server" />
        <asp:HiddenField ID="hfStatusDokumenti" runat="server" />
        <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
        <asp:HiddenField ID="hfGridaKodi" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
     
        
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
              <dx:ASPxHiddenField ID="hfNrAutoDet" runat="server" ClientInstanceName="hfNrAutoDet">
                </dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
                </dx:ASPxHiddenField>
    </form>
</body>
</html>
