<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ElementePerIntegrim.aspx.cs"
    Inherits="PlatinumWeb.ElementePerIntegrim" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>












<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
<script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/ElementePerIntegrim.aspx-IMB.5.8.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
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


        <div id="dvDetyra" style="display: none">
            <dx:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server"   TabSpacing="3px"
                ClientInstanceName="PageControl" Width="100%" ActiveTabIndex="0">
                <ClientSideEvents ActiveTabChanged="PageControlTabChanging" />
                <ContentStyle>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                </ContentStyle>
                <TabPages>

                    <dx:TabPage Name="Dokumentat" Text="Te pergjithshme">
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
                                                    <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
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
                                           <dx:ASPxGridView ID="gvElementePerIntegrim" ClientInstanceName="gvElementePerIntegrim" ToolTip="Evidenca" runat="server"
                                    Width="100%" OnDataBound="gvElementePerIntegrim_DataBound" OnAfterPerformCallback="gvElementePerIntegrim_AfterPerformCallback">
                                     <ClientSideEvents RowDblClick="Row_DblClick"
                                                FocusedRowChanged="function(s, e) { mbush=true;}"
                                                BeginCallback="function(s, e) {BeginCallback(s,e);}" />
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

                    <dx:TabPage Name="Dokumenti" Text="Elemente per integrim">
                        <ContentCollection>
                            <dx:ContentControl ID="content">
                                <table id="tblPasqyra" class="renditKontrolle">
                                    <tbody>
                                    </tbody>
                                </table>
                                <div id="divFillim" class="atributeDiveFshehur">
                                    
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                        Text="Kodi" ClientInstanceName="lblKodi">
                                    </dx:ASPxLabel>
                                    
                                    <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi">
										<ClientSideEvents TextChanged="function(s, e) {
													
															}"
											Init="function(s, e) {  }" />
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
                                    
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi" ID="lblEmertimi" runat="server"
                                        Text="Emertimi" ClientInstanceName="lblEmertimi">
                                    </dx:ASPxLabel>
                                    
                                    <dx:ASPxMemo ID="txtEmertimi" Rows="3" runat="server" Width="100%" AutoPostBack="false"
										ClientInstanceName="txtEmertimi" MaxLength="100">
										
										<ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
											ValidationGroup="entries" SetFocusOnError="True">
											<ErrorFrameStyle ImageSpacing="4px">
												<ErrorTextPaddings PaddingLeft="4px" />
											</ErrorFrameStyle>
											<RequiredField IsRequired="true" />
										</ValidationSettings>
										<DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
										</DisabledStyle>
									</dx:ASPxMemo>
                                    
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbAktiv" ID="lblAktiv" runat="server"
                                        Text="Aktiv" ClientInstanceName="lblAktiv">
                                    </dx:ASPxLabel> 
                                    
                                    <dx:ASPxCheckBox ID="cbAktiv" runat="server" ClientInstanceName="cbAktiv" Width="100%" Checked="true">
										<DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
										</DisabledStyle>
										<ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
											ValidationGroup="entries" ValidateOnLeave="false">
											<ErrorFrameStyle ImageSpacing="4px">
												<ErrorTextPaddings PaddingLeft="4px" />
											</ErrorFrameStyle>
											<RequiredField IsRequired="true" />
										</ValidationSettings>
									</dx:ASPxCheckBox>
                                        
                                   <dx:ASPxLabel Wrap="False" AssociatedControlID="deDateRegjistrimi" ID="lblDateRegjistrimi" runat="server"
                                        Text="Date regjistrimi" ClientInstanceName="lblDateRegjistrimi">
                                    </dx:ASPxLabel>  
                                    
                                    <dx:ASPxDateEdit ID="deDateRegjistrimi" runat="server" ClientInstanceName="deDateRegjistrimi"
										ShowShadow="False" Width="100%">
                                        <ClientSideEvents Init="Init_deDateRegjistrimi" />
										<DropDownButton>
											<Image>
												<SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
											</Image>
										</DropDownButton>
										<CalendarProperties>
											<HeaderStyle Spacing="1px" />
											<FooterStyle Spacing="17px" />
										</CalendarProperties>
										<ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries2">
											<ErrorFrameStyle ImageSpacing="4px">
												<ErrorTextPaddings PaddingLeft="4px" />
											</ErrorFrameStyle>
											<RequiredField IsRequired="true" />
										</ValidationSettings>
										<DisabledStyle Font-Bold="False">
										</DisabledStyle>
									</dx:ASPxDateEdit>

                                     <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLloji" ID="lblLloji" runat="server"
                                        Text="Lloji" ClientInstanceName="lblLloji">
                                    </dx:ASPxLabel>  
                                      
                                    <dx:ASPxComboBox ID="cmbLloji" runat="server"  EnableSynchronization="True" Width="100%" AutoPostBack="false" ClientInstanceName="cmbLloji">
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
                                           
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="memoShenime" ID="lblShenime" runat="server"
                                            Text="Shenime" ClientInstanceName="lblShenime">
                                        </dx:ASPxLabel>
                                           
                                       <dx:ASPxMemo ID="memoShenime" Rows="3" runat="server" Width="100%" AutoPostBack="false"
										ClientInstanceName="memoShenime" MaxLength="100">
										
										<ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
											ValidationGroup="entries" SetFocusOnError="True">
											<ErrorFrameStyle ImageSpacing="4px">
												<ErrorTextPaddings PaddingLeft="4px" />
											</ErrorFrameStyle>
											<RequiredField IsRequired="false" />
										</ValidationSettings>
										<DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
										</DisabledStyle>
									</dx:ASPxMemo>
                                           
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
