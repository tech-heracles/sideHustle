<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_KategoriShpenzimi.aspx.cs"
    Inherits="PlatinumWeb.Shto_KategoriShpenzimi" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/aspx.js/Shto_KategoriShpenzimi.aspx-IMB.3.3.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
       
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
       
        <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                    Width="300px" ClientIDMode="AutoID">
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
        <div id="dvLlogaria">
            <%-- style="visibility: hidden"--%>
            <dx:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
                  TabSpacing="3px" Width="100%" Height="520px" ClientIDMode="AutoID">
                <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                <TabPages>
                    <dx:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server">
                                <table class="renditKontrolle">
                                    <tbody>
                                        <tr>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="lblModel" ClientInstanceName="lblModel" runat="server" Style="font-size: large" ClientIDMode="AutoID" Text="Modeli:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33">
                                                    <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                        ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                                                        Width="100%" AnimationType="None">
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
                                                <dx:ASPxLabel ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33"></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <dx:ASPxGridView ID="gvKategoria" ClientInstanceName="gvKategoria" runat="server"
                                    Width="100%" OnDataBound="gvKategoria_DataBound" OnAfterPerformCallback="gvKategoria_AfterPerformCallback"
                                    OnHeaderFilterFillItems="gvKategoria_HeaderFilterFillItems" OnProcessColumnAutoFilter="gvKategoria_ProcessColumnAutoFilter"
                                    OnCustomCallback="gvKategoria_CustomCallback" OnCustomJSProperties="gridllog_CustomJSProperties">
                                    <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                        SelectionChanged="function(s, e){OnGridSelectionChanged(e);}" FocusedRowChanged="function(s, e) {
            mbush=true;	
}" BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
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
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="Kategori Shpenzimi" Text="Kategori Shpenzimi" >
                       
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl3" runat="server">
                                <table id="tblKategoria" class="renditKontrolle">
                                    <tr>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                                Text="Kodi:" ClientInstanceName="lblKodi">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth25">
                                            <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi">
                                                <ClientSideEvents TextChanged="function(s, e) {}" />
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                    ValidationGroup="entries" SetFocusOnError="true" RegularExpression-ValidationExpression="^[\s\S]{0,50}$"
                                                    RegularExpression-ErrorText="Kodi nuk duhet te jete me shume se 50 karaktere">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 50 karaktere" ValidationExpression="^[\s\S]{0,50}$">
                                                    </RegularExpression>
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                </DisabledStyle>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi" ID="lblEmertimi" runat="server"
                                                Text="Emertimi:" ClientInstanceName="lblEmertimi">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth25">
                                            <dx:ASPxMemo ID="txtEmertimi" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtEmertimi"
                                                Rows="3">
                                                <ClientSideEvents TextChanged="function(s, e) {
	                                            
                                                         
  }       " />
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                    ValidationGroup="entries" SetFocusOnError="true">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                </DisabledStyle>
                                            </dx:ASPxMemo>
                                        </td>
                                        <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneEmertimPrindi" ID="lblPrindi"
                                                        runat="server" Text="Prindi:" ClientInstanceName="lblPrindi">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth25">
                                                    <dx:ASPxComboBox ID="btneEmertimPrindi" runat="server" ClientInstanceName="btneEmertimPrindi"
                                                        EnableCallbackMode="True" ReadOnly="false" ValidationSettings-CausesValidation="True"
                                                        SettingsLoadingPanel-ImagePosition="Top" OnItemRequestedByValue="btneEmertimPrindi_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneEmertimPrindi_ItemsRequestedByFilterCondition"
                                                        
                                                        ShowShadow="False" Width="100%">
                                                        
                                                        <ClientSideEvents ButtonClick="function(s, e) {    prindClick();	}" SelectedIndexChanged="function(s, e) { ndryshoPrindi(s,e)}" />

                                                        <DropDownButton>
                                                            <Image>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </Image>
                                                        </DropDownButton>
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True"
                                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                        <DisabledStyle Font-Bold="False">
                                                        </DisabledStyle>
                                                    </dx:ASPxComboBox>


                                                </td>
                                               <%-- <td width="3%"></td>--%>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel ID="lblNiveli" runat="server" AssociatedControlID="txtNiveli"
                                                        ClientInstanceName="lblNiveli" Text="Niveli:"
                                                        Wrap="False">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth25">
                                                    <dx:ASPxTextBox ID="txtNiveli" runat="server" AutoPostBack="false" ClientInstanceName="txtNiveli" ClientEnabled="false"
                                                        Width="100%">

                                                        <ValidationSettings CausesValidation="true" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="entries">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>

                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </td>
                                    </tr>
                                    <tr>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojBuxheti" ID="lblLlojBuxheti"

                                                runat="server" Text="Lloj i buxhetit:" ClientInstanceName="lblLlojBuxheti">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth25">
                                            <dx:ASPxComboBox ID="cmbLlojBuxheti" runat="server" ClientInstanceName="cmbLlojBuxheti"
                                                ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                                <DropDownButton>
                                                    <Image>
                                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    </Image>
                                                </DropDownButton>
                                                <Items>
                                                    <dx:ListEditItem Text="Buxhet" Value="0" Selected="true"/>
                                                    <dx:ListEditItem Text="Projekt Buxhet" Value="1" />
                                                </Items>
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { onChangedLlojBuxheti(s,e)}" />
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True"
                                                    ValidationGroup="entries">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="false" />
                                                </ValidationSettings>
                                                <DisabledStyle Font-Bold="False">
                                                </DisabledStyle>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbViti" ID="lblViti"
                                                runat="server" ClientVisible ="false" Text="Viti:" ClientInstanceName="lblViti">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth25">
                                            <dx:ASPxComboBox ID="cmbViti" runat="server" ClientInstanceName="cmbViti" ClientVisible ="false" 
                                                ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                                <DropDownButton>
                                                    <Image>
                                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    </Image>
                                                </DropDownButton>
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { onChangedViti(s,e)}" />
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True"
                                                    ValidationGroup="entries">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="false" />
                                                </ValidationSettings>
                                                <DisabledStyle Font-Bold="False">
                                                </DisabledStyle>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td class="renditKontrolleCaption"> 
                                             <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDateAkt" ID="lblDateAkt" runat="server"
                                        Text="Date Aktivizimi:" ClientInstanceName="lblDateAkt">
                                    </dx:ASPxLabel>

                                    </td>
                                        <td class="renditKontrolleCellMeWidth25">
                                            <dx:ASPxDateEdit ID="dteDateAkt" runat="server" ClientInstanceName="dteDateAkt"
                                        ShowShadow="False" Width="100%">
                                        <ClientSideEvents DateChanged="function (s,e){}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit></td>
                                        <td class="renditKontrolleCaption"> <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNdryshimi" ID="lblNdryshimi"
                                        runat="server" Text="Historiku:" ClientInstanceName="lblNdryshimi">
                                    </dx:ASPxLabel></td>
                                        <td class="renditKontrolleCellMeWidth25">
                                            <asp:UpdatePanel ID="cmbNdryshimi_pnlNdryshimi" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <dx:ASPxComboBox ID="cmbNdryshimi" runat="server" ClientInstanceName="cmbNdryshimi"
                                                Width="100%" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                                <ClientSideEvents SelectedIndexChanged="function (s,e){ ndryshoDate();  }" />
                                                <DropDownButton>
                                                    <Image>
                                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    </Image>
                                                </DropDownButton>
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                                    ValidateOnLeave="false">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                                <DisabledStyle Font-Bold="False">
                                                </DisabledStyle>
                                            </dx:ASPxComboBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel></td>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cbKatAktive" ID="lblKatAktive" runat="server" Text="Aktiv:" ClientInstanceName="lblKatAktive">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth25">
                                        <dx:ASPxCheckBox ID="cbKatAktive" runat="server" ClientInstanceName="cbKatAktive" Width="100%">
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxCheckBox>
                                        </td>
                                        <td class="renditKontrolleCaption">
                                           <dx:ASPxLabel Wrap="False" ID="lblKapitulli" AssociatedControlID="btneKapitulli" runat="server" 
                                            Text="Kapitulli" ClientInstanceName="lblKapitulli">
                                        </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth25">
                                             <dx:ASPxComboBox ID="btneKapitulli" Width="100%" runat="server" ClientInstanceName="btneKapitulli"
                                            ShowShadow="False" ValueType="System.Int64" EnableClientSideAPI="True" IncrementalFilteringMode="Contains"
                                            EnableCallbackMode="True" CallbackPageSize="10"
                                            SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickKapitulliNgaKoka();}" LostFocus="function(s,e){}"
                                                TextChanged="function(s,e) {}" ValueChanged="function(s, e) {

}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip"
                                                Display="Dynamic" ValidateOnLeave="false" ValidationGroup="entries1">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <dx:ASPxGridView ID="gvBuxheti" runat="server" ClientInstanceName="gvBuxheti"
                                    OnAfterPerformCallback="gvBuxheti_AfterPerformCallback" Width="100%" OnHtmlRowCreated="gvBuxheti_HtmlRowCreated"
                                    OnCustomCallback="gvBuxheti_CustomCallback">
                                    <ClientSideEvents EndCallback="EndCallBackGridaBuxheti" />
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                                <br />
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="function(s, e) { activeTabChanged(s, e); }" />
                <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
            </dx:ASPxPageControl>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
                <asp:HiddenField ID="hfBuxheti1" runat="server" />
                <asp:HiddenField ID="hfBuxheti2" runat="server" />
                <asp:HiddenField ID="hfBuxhetiShenime" runat="server" />
                <asp:HiddenField ID="hfFushatShtese" runat="server" />
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfKontrollet" runat="server" />
                <asp:HiddenField ID="hfStatusi" runat="server" />
                <asp:HiddenField ID="hfMonedhaNder" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                    <ClientSideEvents Closing="function(s, e) {
	popupUniversal.SetContentUrl('');
}" />
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
