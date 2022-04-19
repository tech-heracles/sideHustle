<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ABKonfiguroFusha.aspx.cs" Inherits="PlatinumWeb.ABKonfiguroFusha" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
        <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/ABKonfiguroFusha.aspx-IMB.5.4.js&v76""
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
                                <ClientSideEvents ItemClick="ItemClickMenu" Init="function(s) {s.SetClientVisible(true);}"/>
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
                                            <ClientSideEvents Init="gvInit" />
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
        <div id="dvAktiviteti" style="display: block">

            <table class="renditKontrolle">
                <tbody>
                    <tr>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAmbjenti" ID="Ambjenti_Label"
                                runat="server" Style="font-size: large" Text="Ambjenti:">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth33">
                            <dx:ASPxComboBox ID="cmbAmbjenti" runat="server" ClientInstanceName="cmbAmbjenti"
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
                            <dx:ASPxLabel Wrap="False" ID="lblAmbjenti" runat="server" class="klasePerLblKonfigurimi"
                                ClientInstanceName="lblAmbjenti">
                            </dx:ASPxLabel>
                        </td>
                      <td class="renditKontrolleCellMeWidth33"></td>
                    </tr>
                </tbody>
            </table>
               
               <dx:ASPxPageControl EnableHierarchyRecreation="false" ID="Tabs" runat="server"   TabSpacing="3px"
                ClientInstanceName="Tabs" Width="100%" ActiveTabIndex="0">
                 <ClientSideEvents ActiveTabChanged="PageControlTabChanging" />
                <ContentStyle>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                </ContentStyle>
                <TabPages>

                    <dx:TabPage Name="Zerat" Text="Konfigurimi i zerave" >
                         <ContentCollection>
                            <dx:ContentControl>
                                <asp:UpdatePanel ID="pnlGrida" runat="server">
                                    <ContentTemplate>
                                            <dx:ASPxGridView ID="gvKonfiguroFusha"  OnCustomErrorText="gvKonfiguroFusha_CustomErrorText"
                                                Width="100%" ToolTip="Ambjenti i zerave" runat="server" OnCustomCallback="gvKonfiguroFusha_CustomCallback"
                                                OnBatchUpdate="gvKonfiguroFusha_BatchUpdate" OnDataBound="gvKonfiguroFusha_DataBound" OnRowValidating="gvKonfiguroFusha_RowValidating">
                                                <ClientSideEvents BeginCallback="gvBeginCallback" EndCallback="gvEndCallback"  BatchEditRowValidating="rowValidation"  />

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
                                                </Templates>
                                            </dx:ASPxGridView>
                                          </ContentTemplate>
                                </asp:UpdatePanel>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>

                    <dx:TabPage Name="ParashikimShpenzimesh" Text="Konfigurimi i parashikimit te shpenzimeve">
                       <ContentCollection>
                            <dx:ContentControl>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="gvKonfigParashikimShpenzimesh"
                                                Width="100%" ToolTip="Konfigurimi i parashikimit te shpenzimeve te personelit" runat="server"
                                            OnBatchUpdate="gvKonfigParashikimShpenzimesh_BatchUpdate" OnRowValidating="gvKonfigParashikimShpenzimesh_RowValidating" OnCustomCallback="gvKonfigParashikimShpenzimesh_CustomCallback"  OnDataBound="gvKonfigParashikimShpenzimesh_DataBound"
                                                >
                                             <ClientSideEvents BeginCallback="gvBeginCallback" EndCallback="gvEndCallback" BatchEditStartEditing="StartEditingParashikimShpenzimesh"  />
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
                                                 <SettingsLoadingPanel Mode="Disabled" />
                                        </dx:ASPxGridView>
                                          
                                     </ContentTemplate>
                                </asp:UpdatePanel>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>

                    <dx:TabPage Name="ShpenzimeOperative" Text="Konfigurimi i shpenzimeve operative" >
                       <ContentCollection>
                            <dx:ContentControl>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                           <dx:ASPxGridView ID="gvKonfiguroShpenzimeOperative"
                                                Width="100%" ToolTip="Ambjenti i zerave" runat="server" OnCustomCallback="gvKonfiguroShpenzimeOperative_CustomCallback"
                                                OnBatchUpdate="gvKonfiguroShpenzimeOperative_BatchUpdate" OnDataBound="gvKonfiguroShpenzimeOperative_DataBound" OnRowValidating="gvKonfiguroShpenzimeOperative_RowValidating">
                                                <ClientSideEvents BeginCallback="gvBeginCallback" EndCallback="gvEndCallback"  BatchEditStartEditing="StartEditingShpenzimeOperative" BatchEditEndEditing="EndEditingShpenzimeOperative" BatchEditRowValidating="rowValidationShpenzimeOperative"/>
                                                    
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
                                                </Templates>
                                            </dx:ASPxGridView>
                                     </ContentTemplate>
                                </asp:UpdatePanel>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>

                    <dx:TabPage Name="RealizimProkurorimesh" Text="Konfigurimi i zerave te prokurimeve publike" >
                       <ContentCollection>
                            <dx:ContentControl>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                           <dx:ASPxGridView ID="gvKonfiguroZeraProkurimesh"
                                                Width="100%" ToolTip="Ambjenti i zerave" runat="server" OnCustomCallback="gvKonfiguroZeraProkurimesh_CustomCallback"
                                                OnBatchUpdate="gvKonfiguroZeraProkurimesh_BatchUpdate" OnDataBound="gvKonfiguroZeraProkurimesh_DataBound" OnRowValidating="gvKonfiguroZeraProkurimesh_RowValidating">
                                                <ClientSideEvents BeginCallback="gvBeginCallback" EndCallback="gvEndCallback"  BatchEditStartEditing="StartEditingZeraProkurimesh" BatchEditEndEditing="EndEditingZeraProkurimesh" BatchEditRowValidating="rowValidationZeraProkurimesh"/>
                                                    
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
                                                </Templates>
                                            </dx:ASPxGridView>
                                     </ContentTemplate>
                                </asp:UpdatePanel>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl >


            
        </div>
        <asp:HiddenField ID="HfKonfAmb" runat="server" />
        <asp:HiddenField ID="hfShtimModifikim" runat="server" />
        <asp:HiddenField ID="hfKolonaGride" runat="server" />
        <asp:HiddenField ID="hfKonffillestar" runat="server" />
        <%-- per te ruajtur Ambjentin  lupes per lupat qe ndodhen ne gride--%>
        <asp:HiddenField ID="hfGridaKodi" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
    </form>
</body>
</html>