<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_Punonjes.aspx.cs"
    Inherits="PlatinumWeb.Shto_Punonjes" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/aspx.js/Shto_Punonjes.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000" >
             </asp:ScriptManager>
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
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
                      
                    <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel"
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
            <div id="dvPunonjes" style="display: none">
                <dx:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" ClientInstanceName="PageControl"
                      TabSpacing="3px" Width="100%" ActiveTabIndex="5" Height="400px"
                    ClientIDMode="AutoID">
                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    </ContentStyle>
                    <TabPages>
                        <dx:TabPage Name="Te pergjithshme" Text="Te pergjithshme" NewLine="True">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server">
                                    <table class="renditKontrolle">
                                        <tbody>
                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                        runat="server" Text="Modeli:">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth33">
                                                    <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%" AnimationType="None">
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
                                    <dx:ASPxGridView ID="gvPunonjesit" ToolTip="Punonjesit" ClientInstanceName="gvPunonjesit" runat="server"
                                        Width="100%" OnDataBound="gvPunonjesit_DataBound"   OnCellEditorInitialize="gvPunonjesit_CellEditorInitialize" OnAfterPerformCallback="gvPunonjesit_AfterPerformCallback"
                                        OnAutoFilterCellEditorInitialize="gvPunonjesit_AutoFilterCellEditorInitialize" OnCustomColumnSort="gvPunonjesit_CustomColumnSort"
                                        OnHeaderFilterFillItems="gvPunonjesit_HeaderFilterFillItems" OnCustomCallback="gvPunonjesit_CustomCallback" SettingsBehavior-SortMode="Custom"
                                        OnCustomJSProperties="gvPunonjesit_CustomJSProperties" OnProcessColumnAutoFilter="gvPunonjesit_ProcessColumnAutoFilter" SettingsBehavior-ProcessFocusedRowChangedOnServer="false" 
                                        Style="margin-bottom: 33px">
                                        <Templates>
                                      
                                        </Templates>
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <ClientSideEvents FocusedRowChanged="function(s, e) { if(  PageControl.GetActiveTabIndex()==0) mbush=true;	}"
                                            RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }" SelectionChanged="function(s, e){OnGridSelectionChanged(e);}"
                                            BeginCallback="function(s, e) {	BeginCallback(s,e);}" EndCallback="EndCallbackPunonjesit" />
                                        <SettingsPager PageSize="15">
                                        </SettingsPager>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                        <SettingsBehavior AllowFocusedRow="False" />
                                        <SettingsBehavior AllowSelectByRowClick="false" />
                                    </dx:ASPxGridView>
                                    <br />
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="Punonjes" Text="Punonjes">

                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl3" runat="server">
                                    <table id="tblPunonjes" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrPersonal" ID="lblNrPersonal"
                                        runat="server" Text="NrPersonal:" ClientInstanceName="lblNrPersonal">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtNrPersonal" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtNrPersonal">
                                        <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true"
                                            Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 15 karaktere" ValidationExpression="^[\s\S]{0,15}$"></RegularExpression>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmri" ID="lblEmri" runat="server"
                                        Text="Emri:" ClientInstanceName="lblEmri">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtEmri" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtEmri">
                                        <ClientSideEvents TextChanged="function (s,e){ mbushEmail()}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrRendor" ID="lblNrRendor" runat="server"
                                        Text="Nr. Rendor:" ClientInstanceName="lblNrRendor">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtNrRendor" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtNrRendor">

                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtMbiemri" ID="lblMbiemri" runat="server"
                                        Text="Mbiemri:" ClientInstanceName="lblMbiemri">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtMbiemri" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtMbiemri">
                                        <ClientSideEvents TextChanged="function (s,e){ mbushEmail()}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtAtesia" ID="lblAtesia" runat="server"
                                        Text="Atesia:" ClientInstanceName="lblAtesia">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtAtesia" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtAtesia">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbIdLlogari" ID="lblIdLlogari" runat="server"
                                        Text="Nr. llogari pagese" ClientInstanceName="lblIdLlogari">
                                    </dx:ASPxLabel>                           
                                        <dx:ASPxComboBox ID="cmbIdLlogari" runat="server" ClientInstanceName="cmbIdLlogari" ShowShadow="False"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%"  
                                            OnItemRequestedByValue="cmbIdLlogari_ItemRequestedByValue"
                                        OnItemsRequestedByFilterCondition="cmbIdLlogari_ItemsRequestedByFilterCondition"
                                            >

                                        <ClientSideEvents ButtonClick ="function(s, e) {PunonjesIdLlogari_Click();} " TextChanged   ="function(s,e){OnChange(s,e);}"
                                              />
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
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>


                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDatelindja" ID="lblDatelindja"
                                        runat="server" Text="Datelindja:" ClientInstanceName="lblDatelindja">
                                    </dx:ASPxLabel>
                                    <dx:ASPxDateEdit ID="dteDatelindja" runat="server" ClientInstanceName="dteDatelindja"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries2">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrSig" ID="lblNrSig" runat="server"
                                        Text="Nr. Sig. Shoq:" ClientInstanceName="lblNrSig">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtNrSig" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtNrSig">
                                        <ClientSideEvents TextChanged="function(s, e) {
}" />
                                        <ValidationSettings CausesValidation="true" ValidationGroup="entries1" SetFocusOnError="true"
                                            Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere" ValidationExpression="^[\s\S]{0,20}$"></RegularExpression>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbQyteti" ID="lblQyteti" runat="server"
                                        Text="Qyteti:" ClientInstanceName="lblQyteti">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbQyteti" runat="server" ClientInstanceName="cmbQyteti" ShowShadow="False"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
}" />
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
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtAdresa" ID="lblAdresa" runat="server"
                                        Text="Adresa:" ClientInstanceName="lblAdresa">
                                    </dx:ASPxLabel>
                                    <dx:ASPxMemo ID="txtAdresa" runat="server" ClientInstanceName="txtAdresa" Width="100%" Rows="3">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbAktiv" ID="lblAktiv" runat="server"
                                        Text="Aktiv:" ClientInstanceName="lblAktiv">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbAktiv" runat="server" ClientInstanceName="cbAktiv" Checked="true"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbLlogaritNgaListorare" ID="lblLlogaritNgaListorare" runat="server"
                                        Text="LlogaritNgaListorare:" ClientInstanceName="lblLlogaritNgaListorare">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbLlogaritNgaListorare" runat="server" ClientInstanceName="cbLlogaritNgaListorare" Checked="true"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTel" ID="lblTel" runat="server"
                                        Text="Tel:" ClientInstanceName="lblTel">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtTel" runat="server" ClientInstanceName="txtTel" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            <RequiredField IsRequired="true" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmail" ID="lblEmail" runat="server"
                                        Text="Email:" ClientInstanceName="lblEmail">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtEmail" runat="server" ClientInstanceName="txtEmail" Width="100%">
                                        <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true"
                                            Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <%--  <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorText="Format i gabuar e-mail!" />--%>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbObjektiva" ID="lblObjektiva" runat="server"
                                        Text="Prindi:" ClientInstanceName="lblObjektiva">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbObjektiva" ClientInstanceName="cmbObjektiva" Width="100%" runat="server"
                                         EnableCallbackMode="True"
                                        IncrementalFilteringDelay="7" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                        <ClientSideEvents ButtonClick="function(s, e) {Objektiva_Click();}" />
                                    </dx:ASPxComboBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtSap" ID="lblSap" runat="server"
                                        Text="SAP ID:" ClientInstanceName="lblSap">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtSap" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtSap">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrPashaporte" ID="lblNrPashaporte" runat="server"
                                        Text="Nr. pashaporte:" ClientInstanceName="lblNrPashaporte">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtNrPashaporte" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtNrPashaporte">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGjinia" ID="lblGjinia" runat="server"
                                        Text="Gjinia:" ClientInstanceName="lblGjinia">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbGjinia" runat="server" ClientInstanceName="cmbGjinia" ShowShadow="False"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
}" />
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
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKombesia" ID="lblKombesia" runat="server"
                                        Text="Kombesia:" ClientInstanceName="lblKombesia">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbKombesia" runat="server" ClientInstanceName="cmbKombesia" ShowShadow="False"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {if(cmbKombesia.GetValue()>2) txtLejePune.SetEnabled(true); else {txtLejePune.SetEnabled(false); txtLejePune.SetText('');}}"
                                            Init="function(s, e) { if(cmbKombesia.GetValue()>2) txtLejePune.SetEnabled(true); else {txtLejePune.SetEnabled(false); txtLejePune.SetText('');}}" />
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
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbKryefamiliar" ID="lblKryefamiliar" runat="server"
                                        Text="Kryefamiliar:" ClientInstanceName="lblKryefamiliar">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbKryefamiliar" runat="server" ClientInstanceName="cbKryefamiliar" Checked="true"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbEdukimi" ID="lblEdukimi" runat="server"
                                        Text="Edukimi:" ClientInstanceName="lblEdukimi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbEdukimi" runat="server" ClientInstanceName="cmbEdukimi" ShowShadow="False"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">                                  <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbPunaMeparshme" ID="lblPunaMeparshme" runat="server"
                                        Text=" Puna e Meparshme:" ClientInstanceName="lblPunaMeparshme">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbPunaMeparshme" runat="server" ClientInstanceName="cmbPunaMeparshme" ShowShadow="False"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
}" />
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
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbVendndodhjet" ID="lblVendndodhjet" runat="server"
                                        Text="Vendndodhjet:" ClientInstanceName="lblVendndodhjet">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbVendndodhjet" runat="server" ClientInstanceName="cmbVendndodhjet" ShowShadow="False" OnCallback="cmbVendndodhjet_Callback"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
}" />
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
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrJupiter" ID="lblNrJupiter" runat="server"
                                        Text="Nr. Jupiter:" ClientInstanceName="lblNrJupiter">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtNrJupiter" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtNrJupiter">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLejePune" ID="lblLejePune" runat="server"
                                        Text="Leje pune:" ClientInstanceName="lblLejePune">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtLejePune" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtLejePune">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtUsername" ID="lblUsername" runat="server"
                                        Text="Username:" ClientInstanceName="lblUsername">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtUsername" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtUsername">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime" ID="lblShenime" runat="server"
                                        Text="Shenime:" ClientInstanceName="lblShenime">
                                    </dx:ASPxLabel>
                                    <dx:ASPxMemo ID="txtShenime" runat="server" ClientInstanceName="txtShenime" Width="100%" Rows="3">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="Kontakti emergjences" Text="Kontakti emergjences">

                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl2" runat="server">
                                    <table id="tblKontakti" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblEmriKontakti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmriKontakti" ID="lblEmriKontakti"
                                        runat="server" Text="Emri:" ClientInstanceName="lblEmriKontakti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtEmriKontakti">--%>
                                    <dx:ASPxTextBox ID="txtEmriKontakti" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtEmriKontakti">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblMbiemriKontakti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtMbiemriKontakti" ID="lblMbiemriKontakti"
                                        runat="server" Text="Mbiemri:" ClientInstanceName="lblMbiemriKontakti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtMbiemriKontakti">--%>
                                    <dx:ASPxTextBox ID="txtMbiemriKontakti" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtMbiemriKontakti">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>

                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblAdresaKontakti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtAdresaKontakti" ID="lblAdresaKontakti"
                                        runat="server" Text="Adresa:" ClientInstanceName="lblAdresaKontakti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtAdresaKontakti">--%>
                                    <dx:ASPxMemo ID="txtAdresaKontakti" runat="server" ClientInstanceName="txtAdresaKontakti" Rows="3"
                                        HorizontalAlign="NotSet" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                    <%--<div id="dvlblTelKontakti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTelKontakti" ID="lblTelKontakti"
                                        runat="server" Text="Tel:" ClientInstanceName="lblTelKontakti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtTelKontakti">--%>
                                    <dx:ASPxTextBox ID="txtTelKontakti" runat="server" ClientInstanceName="txtTelKontakti"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblEmailKontakti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmailKontakti" ID="lblEmailKontakti"
                                        runat="server" Text="Email:" ClientInstanceName="lblEmailKontakti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtEmailKontakti">--%>
                                    <dx:ASPxTextBox ID="txtEmailKontakti" runat="server" ClientInstanceName="txtEmailKontakti"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorText="Format i gabuar e-mail!" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblShenimeKontakti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenimeKontakti" ID="lblShenimeKontakti"
                                        runat="server" Text="Shenime:" ClientInstanceName="lblShenimeKontakti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtShenimeKontakti">--%>
                                    <dx:ASPxMemo ID="txtShenimeKontakti" runat="server" ClientInstanceName="txtShenimeKontakti" Rows="3"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmri2" ID="lblEmri2" runat="server"
                                        Text="Punonjesi:" ClientInstanceName="lblEmri2">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtEmri2" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtEmri2">
                                        <ClientSideEvents TextChanged="function (s,e){ mbushEmailPunonjesi()}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="Punesimet" Text="Punesimet">

                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl4" runat="server">
                                    <table id="tblPunesim" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblDepartamenti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmri3" ID="lblEmri3" runat="server"
                                        Text="Punonjesi:" ClientInstanceName="lblEmri3">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtEmri3" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtEmri3">
                                        <ClientSideEvents TextChanged="function (s,e){ mbushEmailPunonjesi(s,e)}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbDepartamenti" ID="lblDepartamenti"
                                        runat="server" Text="Departamenti:" ClientInstanceName="lblDepartamenti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbDepartamenti">--%>
                                    <dx:ASPxComboBox ID="cmbDepartamenti" runat="server" AutoPostBack="false" ClientInstanceName="cmbDepartamenti"
                                        EnableCallbackMode="True" OnItemRequestedByValue="cmbDepartamenti_ItemRequestedByValue"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick=" 
                                                    function(s,e) {ButtonClickedDepartamenti('Departamenti'); }"
                                            SelectedIndexChanged="function(s, e) { departamentiChanged();
	                                              
                                                }" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblNenDepartamenti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNenDepartamenti" ID="lblNenDepartamenti"
                                        runat="server" Text="Nendepartamenti:" ClientInstanceName="lblNenDepartamenti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbNenDepartamenti">--%>
                                    <dx:ASPxComboBox ID="cmbNenDepartamenti" runat="server" ClientInstanceName="cmbNenDepartamenti"
                                        EnableCallbackMode="True" OnItemRequestedByValue="cmbNenDepartamenti_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbNenDepartamenti_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%" >
                                        <ClientSideEvents ButtonClick="buttonClickCmbNenDep"  LostFocus="function(s, e) {
	                                              
                            }"/>
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblDetyra">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtDetyra" ID="lblDetyra" runat="server"
                                        Text="Detyra:" ClientInstanceName="lblDetyra">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtDetyra">--%>
                                    <dx:ASPxTextBox ID="txtDetyra" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtDetyra">
                                        <ClientSideEvents TextChanged="function(s, e) {
}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblGrupi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGrupi" ID="lblGrupi" runat="server"
                                        Text="Grupi:" ClientInstanceName="lblGrupi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbGrupi">--%>
                                    <dx:ASPxComboBox ID="cmbGrupi" runat="server" AutoPostBack="false" ClientInstanceName="cmbGrupi"
                                        EnableCallbackMode="True" OnItemRequestedByValue="cmbGrupi_ItemRequestedByValue"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick=" 
                                                    function(s,e) {ButtonClickedGrupi(s); }"
                                            LostFocus="function(s, e) {
	                                              
                                                }" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblNrKontrate">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrKontrate" ID="lblNrKontrate"
                                        runat="server" Text="Nr. Kontrate:" ClientInstanceName="lblNrKontrate">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtNrKontrate">--%>
                                    <dx:ASPxTextBox ID="txtNrKontrate" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtNrKontrate">
                                        <ClientSideEvents  />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblTipKontrate">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbTipKontrate" ID="lblTipKontrate"
                                        runat="server" Text="Tip Kontrate:" ClientInstanceName="lblTipKontrate">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbTipKontrate">--%>
                                    <dx:ASPxComboBox ID="cmbTipKontrate" runat="server" AutoPostBack="false" ClientInstanceName="cmbTipKontrate"
                                        EnableCallbackMode="True" OnItemRequestedByValue="cmbTipKontrate_ItemRequestedByValue"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick=" 
                                                    function(s,e) {ButtonClickedTipKontrate(); }"
                                            LostFocus="function(s, e) {
	                                              
                                                }" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblDtFillimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtFillimi" ID="lblDtFillimi"
                                        runat="server" Text="Dt. Fillimi:" ClientInstanceName="lblDtFillimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvdteDtFillimi">--%>
                                    <dx:ASPxDateEdit ID="dteDtFillimi" runat="server" ClientInstanceName="dteDtFillimi"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries2">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                    <%--</div>--%>
                                    <%--<div id="dvlblDtPerfundimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtPerfundimi" ID="lblDtPerfundimi"
                                        runat="server" Text="Dt. Perfundimi:" ClientInstanceName="lblDtPerfundimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvdteDtPerfundimi">--%>
                                    <dx:ASPxDateEdit ID="dteDtPerfundimi" runat="server" ClientInstanceName="dteDtPerfundimi"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries2">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                    <%--</div>--%>
                                    <%--<div id="dvlblLlogBankare">--%>

                                    <%--</div>--%>
                                    <%--<div id="dvlblLarguar">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbLarguar" ID="lblLarguar" runat="server"
                                        Text="Larguar:" ClientInstanceName="lblLarguar">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbLarguar">--%>
                                    <dx:ASPxCheckBox ID="cbLarguar" runat="server" ClientInstanceName="cbLarguar" Checked="false"
                                        Width="100%">
                                        <ClientSideEvents CheckedChanged="CheckedChanged_cbLarguar" />
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblDtLargimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtLargimi" ID="lblDtLargimi"
                                        runat="server" Text="Dt. Largimi:" ClientInstanceName="lblDtLargimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvdteDtLargimi">--%>
                                    <dx:ASPxDateEdit ID="dteDtLargimi" runat="server" ClientInstanceName="dteDtLargimi"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries2">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                    <%--</div>--%>
                                    <%--<div id="dvlblArsyeja">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtArsyeja" ID="lblArsyeja" runat="server"
                                        Text="Arsyeja e largimit:" ClientInstanceName="lblArsyeja">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtArsyeja">OnItemRequestedByValue="txtArsyeja_ItemRequestedByValue"OnItemRequestedByValue="txtArsyeja_ItemRequestedByValue"
                                        EnableCallbackMode="True"--%>

                                    <dx:ASPxComboBox ID="txtArsyeja" runat="server" AutoPostBack="false" ClientInstanceName="txtArsyeja"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%" DropDownStyle="DropDown">

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
                                    <%--</div>--%>
                                    <%--<div id="dvlblPeriudhaNjoftimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPeriudhaNjoftimi" ID="lblPeriudhaNjoftimi"
                                        runat="server" Text="Periudha per njoftimin e mbylljes se kontrates:" ClientInstanceName="lblPeriudhaNjoftimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtPeriudhaNjoftimi">--%>
                                    <dx:ASPxTextBox ID="txtPeriudhaNjoftimi" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtPeriudhaNjoftimi">
                                        <ClientSideEvents TextChanged="function(s, e) {
}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblNeProve">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbNeProve" ID="lblNeProve" runat="server"
                                        Text="Ne Prove:" ClientInstanceName="lblNeProve">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbNeProve">--%>
                                    <dx:ASPxCheckBox ID="cbNeProve" runat="server" ClientInstanceName="cbNeProve" Checked="false"
                                        Width="100%">
                                        <ClientSideEvents CheckedChanged="function (s,e){txtPeriudhaProve.SetEnabled(cbNeProve.GetChecked());
                                    txtPeriudhaProve.SetText('');}" />
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblPeriudhaProve">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPeriudhaProve" ID="lblPeriudhaProve"
                                        runat="server" Text="Periudha e proves:" ClientInstanceName="lblPeriudhaProve">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtPeriudhaProve">--%>
                                    <dx:ASPxTextBox ID="txtPeriudhaProve" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtPeriudhaProve">
                                        <ClientSideEvents TextChanged="function(s, e) {
}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>


                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbProfesioni" ID="lblProfesioni"
                                        runat="server" Text="Profesioni:" ClientInstanceName="lblProfesioni">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbTipKontrate">--%>
                                    <dx:ASPxComboBox ID="cmbProfesioni"  runat="server" ClientInstanceName="cmbProfesioni"  ValueType="System.Int64" EnableClientSideAPI="True" IncrementalFilteringMode="Contains"
                                            EnableCallbackMode="False" CallbackPageSize="10" FilterMinLength="0"
                                      
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        
                                        <ClientSideEvents ButtonClick=" 
                                                    function(s,e) {ButtonClickedProfesioni(); }"/>
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbTitull" ID="lblTitull"
                                        runat="server" Text="Titull pune:" ClientInstanceName="lblTitull">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbTipKontrate">--%>
                                    <dx:ASPxComboBox ID="cmbTitull" runat="server" AutoPostBack="false" ClientInstanceName="cmbTitull"
                                        EnableCallbackMode="False"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick=" 
                                                    function(s,e) {ButtonClickedTitulli(); }"
                                             />
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKodeProfesione" ID="lblKodeProfesione"
                                        runat="server" Text="Kode profesione:" ClientInstanceName="lblKodeProfesione">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbTipKontrate">--%>
                                    <dx:ASPxComboBox ID="cmbKodeProfesione" runat="server" AutoPostBack="false" ClientInstanceName="cmbKodeProfesione"
                                        EnableCallbackMode="True" OnItemRequestedByValue="cmbKodeProfesione_ItemRequestedByValue"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick=" 
                                                    function(s,e) {ButtonClickedKodeProfesione(); }"
                                            LostFocus="function(s, e) {
	                                              
                                                }" />
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
                                    <%--</div>--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbShifte" ID="lblShifte" runat="server"
                                        Text="Punonjes me turne:" ClientInstanceName="lblShifte">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbLarguar">--%>
                                    <dx:ASPxCheckBox ID="cbShifte" runat="server" ClientInstanceName="cbShifte" Checked="false"
                                        Width="100%">

                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbKomisione" ID="lblKomisione" runat="server"
                                        Text="Punonjes me komisione:" ClientInstanceName="lblKomisione">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbLarguar">--%>
                                    <dx:ASPxCheckBox ID="cbKomisione" runat="server" ClientInstanceName="cbKomisione" Checked="false"
                                        Width="100%">

                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbStandBy" ID="lblStandBy" runat="server"
                                        Text="Punonjes ne gatishmeri:" ClientInstanceName="lblStandBy">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbLarguar">--%>
                                    <dx:ASPxCheckBox ID="cbStandBy" runat="server" ClientInstanceName="cbStandBy" Checked="false"
                                        Width="100%">

                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbStatusi" ID="lblStatusi"
                                        runat="server" Text="Statusi:" ClientInstanceName="lblStatusi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbTipKontrate">--%>
                                    <dx:ASPxComboBox ID="cmbStatusi" runat="server" AutoPostBack="false" ClientInstanceName="cmbStatusi"
                                        EnableCallbackMode="True"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">

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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNryshimPozicioni" ID="lblNdryshimPozicioni"
                                        runat="server" Text="Statusi:" ClientInstanceName="lblNdryshimPozicioni">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbTipKontrate">--%>
                                    <dx:ASPxComboBox ID="cmbNryshimPozicioni" runat="server" AutoPostBack="false" ClientInstanceName="cmbNryshimPozicioni"
                                        EnableCallbackMode="True"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">

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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtNenshkrimi" ID="lblDtNenshkrimi"
                                        runat="server" Text="Dt. Nenshkrimi:" ClientInstanceName="lblDtNenshkrimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvdteDtPerfundimi">--%>
                                    <dx:ASPxDateEdit ID="dteDtNenshkrimi" runat="server" ClientInstanceName="dteDtNenshkrimi"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries2">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenimePun" ID="lblShenimePun" runat="server"
                                        Text="Shenime:" ClientInstanceName="lblShenimePun">
                                    </dx:ASPxLabel>
                                    <dx:ASPxMemo ID="txtShenimePun" runat="server" ClientInstanceName="txtShenimePun" Width="100%" Rows="3">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDateAktPun" ID="lblDateAktPun" runat="server"
                                        Text="Date Aktivizimi:" ClientInstanceName="lblDateAktPun">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvdteDateAkt">--%>
                                    <dx:ASPxDateEdit ID="dteDateAktPun" runat="server" ClientInstanceName="dteDateAktPun"
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
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries2">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                    <br />

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="Historiku i punesimit" Text="Historiku i punesimit">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl51" runat="server">
                                    <table id="tblHistoriku" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmri4" ID="lblEmri4" runat="server"
                                        Text="Punonjesi:" ClientInstanceName="lblEmri4">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtEmri4" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtEmri4">
                                        <ClientSideEvents TextChanged="function (s,e){ mbushEmailPunonjesi(s,e)}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <table class="renditKontrolle">
                                        <tbody>
                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimiPun" ID="konfigurimi_LabelPun"
                                                        runat="server" Text="Modeli:">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth33">
                                                    <dx:ASPxComboBox ID="cmbKonfigurimiPun" runat="server" ClientInstanceName="cmbKonfigurimiPun"
                                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%" AnimationType="None">
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfiguriminPun()}" />
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
                                                    <dx:ASPxLabel Wrap="False" ID="lblKonfigurimiPun" runat="server" class="klasePerLblKonfigurimi"
                                                        ClientInstanceName="lblKonfigurimiPun">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth33"></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <br />
                                    <dx:ASPxGridView ID="gvPunesim" ClientInstanceName="gvPunesim" runat="server" Settings-ShowTitlePanel="true" SettingsBehavior-EnableCustomizationWindow="true"
                                        Width="100%" OnDataBound="gvPunesim_DataBound" OnAfterPerformCallback="gvPunesim_AfterPerformCallback"
                                        OnCustomCallback="gvPunesim_CustomCallback" OnCustomJSProperties="gvPunesim_CustomJSProperties" OnProcessColumnAutoFilter="gvPunesim_ProcessColumnAutoFilter"
                                        Style="margin-bottom: 33px">
                                        <Templates>
                                       
                                        </Templates>
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {}" BeginCallback="beginCallbackGridaPunesim"
                                            RowDblClick="rowDblClickGridaPunesim"  EndCallback="endCallbackGridaPunesim" />
                                        <SettingsPager PageSize="10">
                                        </SettingsPager>
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="Qendrat e kostos" Text="Qendra e kostos">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl52" runat="server">
                                    <table id="tblQendra" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmri5" ID="lblEmri5" runat="server"
                                        Text="Punonjesi:" ClientInstanceName="lblEmri5">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtEmri5" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtEmri5">
                                        <ClientSideEvents TextChanged="function (s,e){ mbushEmailPunonjesi(s,e)}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--<div id="dvlblDepartamenti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbQK1" ID="lblQK1"
                                        runat="server" Text="Qender Kosto 1:" ClientInstanceName="lblQK1">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbDepartamenti">--%>
                                    <dx:ASPxComboBox ID="cmbQK1" runat="server" AutoPostBack="false" ClientInstanceName="cmbQK1"
                                        EnableCallbackMode="True" OnItemRequestedByValue="cmbQK1_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbQK1_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick=" 
                                                    function(s,e) {ButtonClickedQendra1(); }"
                                            TextChanged="function(s,e) {cmbQK2.SetText(''); }" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblNenDepartamenti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbQK2" ID="lblQK2"
                                        runat="server" Text="Qender Kosto 2:" ClientInstanceName="lblQK2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbNenDepartamenti">--%>
                                    <dx:ASPxComboBox ID="cmbQK2" runat="server" AutoPostBack="false" ClientInstanceName="cmbQK2"
                                        EnableCallbackMode="True" OnItemRequestedByValue="cmbQK2_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbQK2_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick=" 
                                                    function(s,e) {ButtonClickedQendra2(); }"
                                            TextChanged="function(s, e) { merrPrindQK();
	                                              
                                                }" />
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
                               
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDateAktQK" ID="lblDateAktQK" runat="server"
                                        Text="Date Aktivizimi:" ClientInstanceName="lblDateAktQK">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvdteDateAkt">--%>
                                    <dx:ASPxDateEdit ID="dteDateAktQK" runat="server" ClientInstanceName="dteDateAktQK"
                                        ShowShadow="False" Width="100%">
                                        <ClientSideEvents />
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
                                    <br />
                                    <dx:ASPxGridView ID="gvQendra" ClientInstanceName="gvQendra" runat="server" Settings-ShowTitlePanel="true" SettingsBehavior-EnableCustomizationWindow="true"
                                        Width="100%" OnDataBound="gvQendra_DataBound" OnAfterPerformCallback="gvQendra_AfterPerformCallback"
                                        OnCustomCallback="gvQendra_CustomCallback" OnCustomJSProperties="gvQendra_CustomJSProperties" OnProcessColumnAutoFilter="gvQendra_ProcessColumnAutoFilter"
                                        Style="margin-bottom: 33px">
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {

}" BeginCallback ="function (s,e){}"
                                            RowDblClick="function(s, e) { mbushFushaQendra(); }" 
                                            EndCallback="endCallbackQendra" />
                                        <SettingsPager PageSize="10">
                                        </SettingsPager>
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>    
                          <dx:TabPage Name="Banda" Text="Banda">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl53" runat="server">
                                    <table id="tblBanda" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmri15" ID="lblEmri15" runat="server"
                                        Text="Punonjesi:" ClientInstanceName="lblEmri15">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtEmri15" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtEmri15">
                                        <ClientSideEvents TextChanged="function (s,e){ mbushEmailPunonjesi(s,e)}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
   
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGlobal" ID="lblGlobal"
                                        runat="server" Text="Grupimi Global:" ClientInstanceName="lblGlobal">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbDepartamenti">--%>
                                    <dx:ASPxComboBox ID="cmbGlobal" runat="server" AutoPostBack="false" ClientInstanceName="cmbGlobal"
                                        EnableCallbackMode="False" 
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick=" 
                                                    function(s,e) {ButtonClickedGrupim1(); }"
                                            TextChanged="function(s,e) {cmbLocal.SetText(''); }" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblNenDepartamenti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLocal" ID="lblLocal"
                                        runat="server" Text="Grupimi local:" ClientInstanceName="lblLocal">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbNenDepartamenti">--%>
                                    <dx:ASPxComboBox ID="cmbLocal" runat="server" AutoPostBack="false" ClientInstanceName="cmbLocal"
                                        EnableCallbackMode="True" OnItemRequestedByValue="cmbLocal_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbLocal_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick=" 
                                                    function(s,e) {ButtonClickedGrupim2(); }"
                                            TextChanged="function(s, e) { merrPrind();
	                                              
                                                }" />
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDateAktBanda" ID="lblDateAktBanda" runat="server"
                                        Text="Date Aktivizimi:" ClientInstanceName="lblDateAktBanda">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvdteDateAkt">--%>
                                    <dx:ASPxDateEdit ID="dteDateAktBanda" runat="server" ClientInstanceName="dteDateAktBanda"
                                        ShowShadow="False" Width="100%">
                                        <ClientSideEvents />
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
                                    <br />
                                    <dx:ASPxGridView ID="gvBanda" ClientInstanceName="gvBanda" runat="server" Settings-ShowTitlePanel="true" SettingsBehavior-EnableCustomizationWindow="true"
                                        Width="100%" OnDataBound="gvBanda_DataBound" OnAfterPerformCallback="gvBanda_AfterPerformCallback"
                                        OnCustomCallback="gvBanda_CustomCallback" OnCustomJSProperties="gvBanda_CustomJSProperties" OnProcessColumnAutoFilter="gvBanda_ProcessColumnAutoFilter"
                                        Style="margin-bottom: 33px">
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {}" BeginCallback ="beginCallbackQendra"
                                            RowDblClick="function(s, e) { mbushFushaBanda(); }" 
                                            EndCallback="endCallbackBanda" />
                                        <SettingsPager PageSize="10">
                                        </SettingsPager>
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="Pagat dhe shtesat" Text="Pagat dhe shtesat">

                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl5" runat="server">
                                    <table id="tblPagaShtesa" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmri6" ID="lblEmri6" runat="server"
                                        Text="Punonjesi:" ClientInstanceName="lblEmri6">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtEmri6" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtEmri6">
                                        <ClientSideEvents TextChanged="function (s,e){ mbushEmailPunonjesi(s,e)}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLlogBankare" ID="lblLlogBankare"
                                        runat="server" Text="Llogaria Bankare:" ClientInstanceName="lblLlogBankare">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtLlogBankare">--%>
                                    <dx:ASPxTextBox ID="txtLlogBankare" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtLlogBankare">
                                        <ClientSideEvents TextChanged="function(s, e) {
}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblBanka">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbBanka" ID="lblBanka" runat="server"
                                        Text="Banka:" ClientInstanceName="lblBanka">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbBanka">--%>
                                    <dx:ASPxComboBox ID="cmbBanka" runat="server" AutoPostBack="false" ClientInstanceName="cmbBanka"
                                        EnableCallbackMode="True" 
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s,e) {ButtonClickedBanka(s); }"  />
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLimitTel" ID="lblLimitTel"
                                        runat="server" Text="Limiti i telefonit:" ClientInstanceName="lblLimitTel">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <dx:ASPxButton ID="btnshow" runat="Server" ToolTip="" Text="Historik i pagave" Font-Size="8" AutoPostBack="false" ClientInstanceName="btnshow">
                                        <ClientSideEvents Click="function(s, e) {  hapRaportin(s, e); e.processOnServer=false; }" />
                                    </dx:ASPxButton>
                                    <%--<div id="dvtxtPeriudhaProve">--%>
                                    <dx:ASPxTextBox ID="txtLimitTel" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtLimitTel">
                                        <ClientSideEvents TextChanged="function(s, e) {
}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLimitInternet" ID="lblLimitInternet"
                                        runat="server" Text="Limiti i internetit:" ClientInstanceName="lblLimitInternet">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtPeriudhaProve">--%>
                                    <dx:ASPxTextBox ID="txtLimitInternet" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtLimitInternet">
                                        <ClientSideEvents TextChanged="function(s, e) {
}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--<div id="dvlblLlojPagese">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojPagese" ID="lblLlojPagese"
                                        runat="server" Text="Lloj Pagese:" ClientInstanceName="lblLlojPagese">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbLlojPagese">--%>
                                    <dx:ASPxComboBox ID="cmbLlojPagese" runat="server" ClientInstanceName="cmbLlojPagese"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { ndryshoEmerNjesi();
}" />
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
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblMonedha">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMonedha" ID="lblMonedha" runat="server"
                                        Text="Monedha:" ClientInstanceName="lblMonedha">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbMonedha">--%>
                                    <dx:ASPxComboBox ID="cmbMonedha" runat="server" ClientInstanceName="cmbMonedha"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
}" />
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
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblDateAkt">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDateAkt" ID="lblDateAkt" runat="server"
                                        Text="Date Aktivizimi:" ClientInstanceName="lblDateAkt">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvdteDateAkt">--%>
                                    <dx:ASPxDateEdit ID="dteDateAkt" runat="server" ClientInstanceName="dteDateAkt"
                                        ShowShadow="False" Width="100%">
                                        <ClientSideEvents DateChanged="function (s,e){DtAktChange()}" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblNdryshimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNdryshimi" ID="lblNdryshimi"
                                        runat="server" Text="Data e ndryshimit:" ClientInstanceName="lblNdryshimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbNdryshimi">--%>
                                    <asp:UpdatePanel ID="cmbNdryshimi_pnlNdryshimi" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <dx:ASPxComboBox ID="cmbNdryshimi" runat="server" ClientInstanceName="cmbNdryshimi"
                                                Width="100%" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                                <ClientSideEvents SelectedIndexChanged="gvPagaShtesaSelectedIndexChanged" />
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
                                    </asp:UpdatePanel>
                                    <%--</div>--%>
                                    <br />
                                    <dx:ASPxGridView ID="gvPagaShtesa" ClientInstanceName="gvPagaShtesa" runat="server"
                                        Width="100%" OnHtmlRowCreated="gvPagaShtesa_HtmlRowCreated" OnAfterPerformCallback="gvPagaShtesa_AfterPerformCallback"
                                        OnCustomCallback="gvPagaShtesa_CustomCallback" OnCustomJSProperties="gvPagaShtesa_CustomJSProperties"
                                        Style="margin-bottom: 33px">
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <ClientSideEvents BeginCallback="beginCallbackPagaShtesa" EndCallback="endCallbackPagaShtesa" />
                                        <SettingsPager PageSize="10">
                                        </SettingsPager>
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="Komponentet e listpageses" Text="Komponentet e listpageses">

                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl6" runat="server">
                                    <table id="tblKomponente" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmri7" ID="lblEmri7" runat="server"
                                        Text="Punonjesi:" ClientInstanceName="lblEmri7">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtEmri7" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtEmri7">
                                        <ClientSideEvents TextChanged="function (s,e){ mbushEmailPunonjesi(s,e)}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--<div id="dvlblSkemaSigurimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbSkemaSigurimi" ID="lblSkemaSigurimi"
                                        runat="server" Text="Skema Sigurimit:" ClientInstanceName="lblSkemaSigurimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbSkemaSigurimi">--%>
                                    <dx:ASPxComboBox ID="cmbSkemaSigurimi" runat="server" ClientInstanceName="cmbSkemaSigurimi"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
}" />
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
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblDateAk2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDateAk2" ID="lblDateAk2" runat="server"
                                        Text="Date Aktivizimi:" ClientInstanceName="lblDateAk2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvdteDateAk2">--%>
                                    <dx:ASPxDateEdit ID="dteDateAk2" runat="server" ClientInstanceName="dteDateAk2"
                                        ShowShadow="False" Width="100%">
                                        <ClientSideEvents DateChanged="gvKomponenteListPageseDateChanged" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblNdryshim2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNdryshim2" ID="lblNdryshim2"
                                        runat="server" Text="Data e ndryshimit:" ClientInstanceName="lblNdryshim2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbNdryshim2">--%>
                                    <asp:UpdatePanel ID="cmbNdryshim2_pnlNdryshim2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <dx:ASPxComboBox ID="cmbNdryshim2" runat="server" ClientInstanceName="cmbNdryshim2"
                                                Width="100%" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                                <ClientSideEvents SelectedIndexChanged="gvKomponenteListPageseSelectedIndexChanged" />
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
                                    </asp:UpdatePanel>
                                    <%--</div>--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbDukshme" ID="lblDukshme" runat="server"
                                        Text="E dukshme:" ClientInstanceName="lblDukshme">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbDukshme" runat="server" ClientInstanceName="cbDukshme" Checked="false"
                                        Width="100%">
                                        <ClientSideEvents CheckedChanged="function (s,e) {checkDukshme();}" />
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbDetyrueshme" ID="lblDetyrueshme" runat="server"
                                        Text="E detyrueshme:" ClientInstanceName="lblDetyrueshme">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbDetyrueshme" runat="server" ClientInstanceName="cbDetyrueshme" Checked="false"
                                        Width="100%">
                                        <ClientSideEvents CheckedChanged="function (s,e) {checkDetyrueshme();}" />
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>

                                    <table class="renditKontrolle">
                                        <tbody>
                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimiKomp" ID="lblKonfigKomp"
                                                        runat="server" Text="Modeli:" ClientVisible="false">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth33">
                                                    <dx:ASPxComboBox ID="cmbKonfigurimiKomp" runat="server" ClientVisible="false" ClientInstanceName="cmbKonfigurimiKomp"
                                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%" AnimationType="None">
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfiguriminKomp()}" />
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
                                                    <dx:ASPxLabel Wrap="False" ID="lblKonfigurimiKomp" ClientVisible="false" runat="server" class="klasePerLblKonfigurimi"
                                                        ClientInstanceName="lblKonfigurimiKomp">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth33"></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <br />
                                    <dx:ASPxCallbackPanel EnableHierarchyRecreation="false" ID="ASPxCallback1" runat="server" ClientInstanceName="pnlcallback" OnCallback="ASPxCallback1_Callback">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxGridView ID="gvKomponenteListPagese" ClientInstanceName="gvKomponenteListPagese"
                                                    runat="server" Width="100%" OnHtmlRowCreated="gvKomponenteListPagese_HtmlRowCreated"
                                                    OnAfterPerformCallback="gvKomponenteListPagese_AfterPerformCallback" OnCustomCallback="gvKomponenteListPagese_CustomCallback"
                                                    OnCustomJSProperties="gvKomponenteListPagese_CustomJSProperties" Style="margin-bottom: 33px" >
                                                    <ClientSideEvents BeginCallback="beginCallbackGridaKomponenteListPagese" EndCallback="endCallbackGridaKomponenteListPagese" />
                                                    <Styles>
                                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                        </Header>
                                                    </Styles>
                                                    <SettingsPager PageSize="30">
                                                    </SettingsPager>
                                                </dx:ASPxGridView>
                                                <dx:ASPxHiddenField ID="hfKomponente" runat="server" ClientInstanceName="hfKomponente">
                                                </dx:ASPxHiddenField>
                                                <dx:ASPxHiddenField ID="hfKomponente2" runat="server" ClientInstanceName="hfKomponente2">
                                                </dx:ASPxHiddenField>
                                                <dx:ASPxHiddenField ID="hfKomponente3" runat="server" ClientInstanceName="hfKomponente3">
                                                </dx:ASPxHiddenField>
                                                <dx:ASPxHiddenField ID="hfKodeKomp" runat="server" ClientInstanceName="hfKodeKomp">
                                                </dx:ASPxHiddenField>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                        <ClientSideEvents BeginCallback="beginCallbackPanel" EndCallback="endCallbackPanel" />
                                    </dx:ASPxCallbackPanel >

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                    <ClientSideEvents ActiveTabChanged="activeTabChanged" />
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                </dx:ASPxPageControl >
                           
            </div>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hftabe" runat="server" ClientInstanceName="hftabe">
            </dx:ASPxHiddenField>   
                  <dx:ASPxHiddenField ID="hfNr" runat="server" ClientInstanceName="hfNr">
            </dx:ASPxHiddenField>
            <asp:HiddenField ID="hfArkivaDokId" runat="server" />
            <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
            </dx:ASPxHiddenField>
         
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                  
                    <dx:ASPxGridViewExporter ID="gridExport2" runat="server" GridViewID="gvQendra"
                        ExportedRowType="Selected" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikimPunesim" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikimQendra" runat="server" /> 
                    <asp:HiddenField ID="hfShtimModifikimBanda" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfIdPunesim" runat="server" />
                    <asp:HiddenField ID="hfIdQendra" runat="server" /> 
                    <asp:HiddenField ID="hfIdBanda" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="hfStatusiPunesim" runat="server" />
                    <asp:HiddenField ID="hfStatusiQendra" runat="server" />
           
                    <asp:HiddenField ID="hfLupaDep" runat="server" />
                    <asp:HiddenField ID="hfLupaNendep" runat="server" />
                    <asp:HiddenField ID="hfLupaTipKontrate" runat="server" />
                    <asp:HiddenField ID="hfLupaBanka" runat="server" />
                    <asp:HiddenField ID="hfLupaGrupi" runat="server" />

                    <asp:HiddenField ID="hfParam" runat="server" />
                    <asp:HiddenField ID="hfVlera" runat="server" />
                    <asp:HiddenField ID="hfLejoNrLlogBank" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                    <dx:ASPxHiddenField ID="hfNrAutoKF" runat="server" ClientInstanceName="hfNrAutoKF">
                    </dx:ASPxHiddenField>
                </ContentTemplate>
            </asp:UpdatePanel>

            <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="updateraporti" runat="server">
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
