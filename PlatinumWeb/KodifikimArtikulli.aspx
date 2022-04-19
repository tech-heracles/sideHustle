<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KodifikimArtikulli.aspx.cs"
    Inherits="PlatinumWeb.KodifikimArtikulli" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/aspx.js/KodifikimArtikulli.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
         
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" runat="server" AutoPostBack="true" ClientInstanceName="ASPxMenu1"
                                ItemImagePosition="Top" OnDataBound="ASPxMenu1_DataBound" OnItemClick="ASPxMenu1_ItemClick"
                                SeparatorWidth="1px" ShowPopOutImages="True" Width="100%">
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                <ClientSideEvents ItemClick="function(s, e) {
	                        menu_click(s,e);
                            }"
                                    Init="function(s) {s.SetClientVisible(true);}" />
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
                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                                <PanelCollection>
                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
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
                            </dx:ASPxPanel>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <div style="visibility: hidden">
                    <dx:ASPxButton ID="ASPxButton1" runat="server" ClientInstanceName="btn" Text="ASPxButton"
                        Height="0px">
                    </dx:ASPxButton>
                </div>
                <asp:HiddenField ID="hfStatusi" runat="server" />
                <asp:HiddenField ID="gridDataObject" runat="server" />
                <asp:HiddenField ID="hfShtuarGrup1" runat="server" />
                <asp:HiddenField ID="hfShtuarGrup2" runat="server" />
                <asp:HiddenField ID="hfShtuarGrup3" runat="server" />
                <%-- Hidden fields per Arkiven--%>
                <asp:HiddenField ID="hfArkivaDokId" runat="server" />
                <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
                </dx:ASPxHiddenField>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hfGrup" runat="server" Value="0" />
        <div style="width: 100%">
            <asp:UpdatePanel runat="server" ID="pnlGrida" UpdateMode="Conditional">
                <ContentTemplate>
                    <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" ClientInstanceName="PageControl"
                        TabSpacing="3px" Width="100%" ActiveTabIndex="0" Height="600px">
                        <ClientSideEvents ActiveTabChanging="function(s, e) {ndryshimTabi(e.tab);}" ActiveTabChanged="function(s, e) { tabsActiveTabChanged(s,e);}" />
                        <ContentStyle>
                            <border bordercolor="#AECAF0" borderstyle="Solid" borderwidth="1px" />
                        </ContentStyle>
                        <TabPages>
                            <dxtc:TabPage Name="Grupimi 1" Text="Grupimi 1" ClientVisible="false">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl1" runat="server">
                                        <dx:ASPxGridView ID="gvKodifikimArtikulli" runat="server" Width="100%" OnAfterPerformCallback="gridat_AfterPerformCallback"
                                            OnHeaderFilterFillItems="gridat_HeaderFilterFillItems" OnRowUpdating="rowUpdating"
                                            ClientInstanceName="gvKodifikimArtikulli" OnDataBound ="gv_DataBound" OnProcessColumnAutoFilter="gridat_ProcessColumnAutoFilter"
                                            OnCancelRowEditing="startRowEditing" OnRowInserting="rowInserting" OnRowValidating="rowValidating"
                                            OnInitNewRow="initNewRow" OnCustomCallback="gridat_CustomCallback" OnCustomJSProperties="gridat_CustomJSProperties">
                                            <ClientSideEvents EndCallback="function(s, e) { EndCallbackGrida(s, e); }" 
                                                RowDblClick="function(s, e) { RowDblClickGrida(e.visibleIndex, 'gvKodifikimArtikulli'); }"
                                                BeginCallback="function(s, e) { BeginCallback(s,e); }"
                                                FocusedRowChanged="function(s, e) { onNdryshimFokusi(); }" />
                                            <Styles>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                            <SettingsPager PageSize="15">
                                            </SettingsPager>
                                            <SettingsEditing Mode="EditFormAndDisplayRow" />
                                            <StylesEditors>
                                                <ProgressBar Height="25px">
                                                </ProgressBar>
                                            </StylesEditors>
                                        </dx:ASPxGridView>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="Grupimi 2" Text="Grupimi 2" ClientVisible="false">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl2" runat="server">
                                        <dx:ASPxGridView ID="gvKodifikimArtikulliGr2" runat="server" Width="100%" OnAfterPerformCallback="gridat_AfterPerformCallback"
                                            OnHeaderFilterFillItems="gridat_HeaderFilterFillItems" OnRowUpdating="rowUpdating"
                                            ClientInstanceName="gvKodifikimArtikulliGr2" OnDataBound ="gv_DataBound" OnProcessColumnAutoFilter="gridat_ProcessColumnAutoFilter"
                                            OnCancelRowEditing="startRowEditing" OnRowInserting="rowInserting" OnRowValidating="rowValidating"
                                            OnInitNewRow="initNewRow" OnCustomCallback="gridat_CustomCallback" OnCustomJSProperties="gridat_CustomJSProperties">
                                            <ClientSideEvents EndCallback="function(s, e) { EndCallbackGrida(s, e);}" 
                                                RowDblClick="function(s, e) { RowDblClickGrida(e.visibleIndex, 'gvKodifikimArtikulliGr2'); }"
                                                BeginCallback="function(s, e) { BeginCallback(s,e); }"
                                                FocusedRowChanged="function(s, e) { onNdryshimFokusi(); }" />
                                            <Styles>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                            <SettingsPager PageSize="15">
                                            </SettingsPager>
                                            <SettingsEditing Mode="EditFormAndDisplayRow" />
                                            <StylesEditors>
                                                <ProgressBar Height="25px">
                                                </ProgressBar>
                                            </StylesEditors>
                                        </dx:ASPxGridView>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="Grupimi 3" Text="Grupimi 3" ClientVisible="false">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl3" runat="server">
                                        <dx:ASPxGridView ID="gvKodifikimArtikulliGr3" runat="server" Width="100%" OnAfterPerformCallback="gridat_AfterPerformCallback"
                                            OnHeaderFilterFillItems="gridat_HeaderFilterFillItems" OnRowUpdating="rowUpdating"
                                            ClientInstanceName="gvKodifikimArtikulliGr3" OnDataBound="gv_DataBound" OnProcessColumnAutoFilter="gridat_ProcessColumnAutoFilter"
                                            OnCancelRowEditing="startRowEditing" OnRowInserting="rowInserting" OnRowValidating="rowValidating"
                                            OnInitNewRow="initNewRow" OnCustomCallback="gridat_CustomCallback" OnCustomJSProperties="gridat_CustomJSProperties">
                                            <ClientSideEvents EndCallback="function(s, e) { EndCallbackGrida(s, e);}" 
                                                RowDblClick="function(s, e) { RowDblClickGrida(e.visibleIndex, 'gvKodifikimArtikulliGr3'); }"
                                                BeginCallback="function(s, e) { BeginCallback(s,e); }"
                                                FocusedRowChanged="function(s, e) { onNdryshimFokusi(); }" />
                                            <Styles>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                            <SettingsPager PageSize="15">
                                            </SettingsPager>
                                            <SettingsEditing Mode="EditFormAndDisplayRow" />
                                            <StylesEditors>
                                                <ProgressBar Height="25px">
                                                </ProgressBar>
                                            </StylesEditors>
                                        </dx:ASPxGridView>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Text="Informacion" Name="Informacion" ClientVisible="false">
                                <ContentCollection>
                                    <dxw:ContentControl>
                                        <table width="100%" class="renditKontrolle">
                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                                        Text="Kodi:" ClientInstanceName="lblKodi">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td width="30%" class="renditKontrolleCell">
                                                    <dx:ASPxTextBox ID="txtKodi" runat="server" AutoPostBack="false" ClientInstanceName="txtKodi"
                                                        Width="100%">
                                                        <ClientSideEvents Init="function(s, e) {  }" />
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" SetFocusOnError="True" ValidationGroup="entries">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 50 karaktere" ValidationExpression="^[\s\S]{0,50}$"></RegularExpression>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td width="3%"></td>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi" ID="lblEmertimi" runat="server"
                                                        Text="Pershkrimi:" ClientInstanceName="lblEmertimi">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td width="30%" class="renditKontrolleCell">
                                                    <dx:ASPxTextBox ID="txtEmertimi" runat="server" AutoPostBack="false" ClientInstanceName="txtEmertimi"
                                                        Width="100%" Rows="3">
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" SetFocusOnError="True" ValidationGroup="entries">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td width="3%"></td>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbFormati" ID="lblFormati" runat="server"
                                                        Text="Formati i serialit:" ClientInstanceName="lblFormati">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td width="30%" class="renditKontrolleCell">
                                                    <dx:ASPxComboBox ID="cmbFormati" runat="server" ClientInstanceName="cmbFormati" ShowShadow="False"
                                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                                        <DropDownButton>
                                                            <Image>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </Image>
                                                        </DropDownButton>
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True"
                                                            ValidationGroup="entries">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                        <DisabledStyle Font-Bold="False">
                                                        </DisabledStyle>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td width="3%"></td>
                                            </tr>

                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneEmertimPrindi" ID="lblPrindi"
                                                        runat="server" Text="Prindi:" ClientInstanceName="lblPrindi">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCell">
                                                    <dx:ASPxComboBox ID="btneEmertimPrindi" runat="server" ClientInstanceName="btneEmertimPrindi"
                                                        EnableCallbackMode="True" ReadOnly="false" ValidationSettings-CausesValidation="True"
                                                        OnItemRequestedByValue="btneEmertimPrindi_ItemRequestedByValue" 
                                                        OnItemsRequestedByFilterCondition="btneEmertimPrindi_ItemsRequestedByFilterCondition"
                                                        SettingsLoadingPanel-ImagePosition="Top"
                                                        ShowShadow="False" Width="100%">
                                                        <ClientSideEvents ButtonClick="function(s, e) { prindClick(); }" 
                                                            SelectedIndexChanged="function(s, e) { ndryshoPrindi(s,e);}" TextChanged ="function(s, e) { OnChangePrindi(s,e);}" />
                                                        <DropDownButton>
                                                            <Image>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" 
                                                                    PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
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
                                                <td width="3%"></td>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel ID="lblNiveli" runat="server" AssociatedControlID="txtNiveli"
                                                        ClientInstanceName="lblNiveli" Text="Niveli:"
                                                        Wrap="False">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCell">
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
                                                <td width="3%"></td>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneSkema" ID="lblSkema" runat="server"
                                                        Text="Skema:" ClientInstanceName="lblSkema">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxComboBox ID="btneSkema" runat="server" ClientInstanceName="btneSkema" EnableCallbackMode="True"
                                                        OnItemRequestedByValue="btneSkema_ItemRequestedByValue" EnableSynchronization="True" OnItemsRequestedByFilterCondition="btneSkema_ItemsRequestedByFilterCondition"
                                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                                        <ClientSideEvents ButtonClick="function(s, e) {
Skema_Click();
}"
                                                            TextChanged="function(s, e) {btneSkemaTextChanged(s,e); 
}"
                                                            LostFocus="function(s, e) {
    kontrolloSkema();                                       	 
}"
                                                            EndCallback="function(s, e) {
	if(btneSkema.GetEnabled()==false)
  { btneSkema .HideDropDown()
  }
}"
                                                            BeginCallback="function(s, e) {
	if(btneSkema.GetEnabled()==false)
  { btneSkema .HideDropDown()
  }
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
                                                </td>
                                                <td width="3%"></td>
                                            </tr>
                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogBle" ID="lblLlogBle" runat="server"
                                                        Text="Llogari Vlere Kontabel:" ClientInstanceName="lblLlogBle">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCell">
                                                    <dx:ASPxComboBox ID="btneLlogBle" runat="server" ClientInstanceName="btneLlogBle"
                                                        EnableCallbackMode="True" OnItemRequestedByValue="btneLlogBle_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogBle_ItemsRequestedByFilterCondition"
                                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                                        <ClientSideEvents ButtonClick="function(s, e) {

 txtLlog.SetText('ble');
 LlogariB_Click();
}"
                                                                  TextChanged="function(s, e){OnChange(s,e);}"           

                                                            Init="function(s, e) {
		
}"
                                                            EndCallback="function(s, e) {
	if(btneLlogBle.GetEnabled()==false)
  { btneLlogBle .HideDropDown()
  }
}" />
                                                        <DropDownButton>
                                                            <Image>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </Image>
                                                        </DropDownButton>
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                        <DisabledStyle Font-Bold="False">
                                                        </DisabledStyle>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td width="3%"></td>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogShit" ID="lblLlogShit" runat="server"
                                                        Text="Llogari Shitje:" ClientInstanceName="lblLlogShit">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCell">
                                                    <dx:ASPxComboBox ID="btneLlogShit" runat="server" ClientInstanceName="btneLlogShit"
                                                        EnableCallbackMode="True" OnItemRequestedByValue="btneLlogShit_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogShit_ItemsRequestedByFilterCondition"
                                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                                        <ClientSideEvents ButtonClick="function(s, e) {

 txtLlog.SetText('shit');
 LlogariS_Click();
}"
                   TextChanged="function(s, e){OnChange(s,e);}"                                           Init="function(s, e) {
		
}"
                                                            EndCallback="function(s, e) {
	if(btneLlogShit.GetEnabled()==false)
  { btneLlogShit.HideDropDown()
  }
}" />
                                                        <DropDownButton>
                                                            <Image>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </Image>
                                                        </DropDownButton>
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                                            ValidateOnLeave="false">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                        <DisabledStyle Font-Bold="False">
                                                        </DisabledStyle>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td width="3%"></td>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogInv" ID="lblLlogInv" runat="server"
                                                        Text="Llogari Inventar:" ClientInstanceName="lblLlogInv">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCell">
                                                    <dx:ASPxComboBox ID="btneLlogInv" runat="server" ClientInstanceName="btneLlogInv"
                                                        EnableCallbackMode="True" OnItemRequestedByValue="btneLlogInv_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogInv_ItemsRequestedByFilterCondition"
                                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                                        <ClientSideEvents ButtonClick="function(s, e) {
 txtLlog.SetText('inv');
 LlogariI_Click();
}"
                                                                                       TextChanged="function(s, e){OnChange(s,e);}"                               

                                                            Init="function(s, e) {

}"
                                                            EndCallback="function(s, e) {
	if(btneLlogInv.GetEnabled()==false)
  { btneLlogInv .HideDropDown()
  }
}" />
                                                        <DropDownButton>
                                                            <Image>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </Image>
                                                        </DropDownButton>
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                                            ValidateOnLeave="false">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                        <DisabledStyle Font-Bold="False">
                                                        </DisabledStyle>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td width="3%"></td>
                                            </tr>
                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btnLlogShpe" ID="lblLlogShpe" runat="server"
                                                        Text="Llogari Shpenzim Amortizimi:" ClientInstanceName="lblLlogShpe">
                                                    </dx:ASPxLabel>
                                                </td>
                                              
                                                <td class="renditKontrolleCell">
                                                       <dx:ASPxComboBox ID="btnLlogShpe" runat="server" ClientInstanceName="btnLlogShpe"
                                                        EnableCallbackMode="True" OnItemRequestedByValue="btnLlogShpe_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btnLlogShpe_ItemsRequestedByFilterCondition"
                                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                                        <ClientSideEvents ButtonClick="function(s, e) {

 txtLlog.SetText('shpe');
LlogariShp_Click();
}"
                         TextChanged="function(s, e){OnChange(s,e);}"                                    Init="function(s, e) {
	
}"
                                                            EndCallback="function(s, e) {
	if(btnLlogShpe.GetEnabled()==false)
  { btnLlogShpe.HideDropDown()
  }
}" />
                                                        <DropDownButton>
                                                            <Image>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </Image>
                                                        </DropDownButton>
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                                            ValidateOnLeave="false">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                        <DisabledStyle Font-Bold="False">
                                                        </DisabledStyle>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td width="3%"></td>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogTretet" ID="lblLlogTretet"
                                                        runat="server" Text="Llogari AA ne proces:" ClientInstanceName="lblLlogTretet">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCell">
                                                    <dx:ASPxComboBox ID="btneLlogTretet" runat="server" ClientInstanceName="btneLlogTretet"
                                                        EnableCallbackMode="True" OnItemRequestedByValue="btneLlogTretet_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogTretet_ItemsRequestedByFilterCondition"
                                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                                        <ClientSideEvents ButtonClick="function(s, e) {

 txtLlog.SetText('tretet');
LlogariT_Click();
}"
                                  TextChanged="function(s, e){OnChange(s,e);}"                           Init="function(s, e) {
	
}"
                                                            EndCallback="function(s, e) {
	if(btneLlogTretet.GetEnabled()==false)
  { btneLlogTretet .HideDropDown()
  }
}" />
                                                        <DropDownButton>
                                                            <Image>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </Image>
                                                        </DropDownButton>
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                                            ValidateOnLeave="false">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                        <DisabledStyle Font-Bold="False">
                                                        </DisabledStyle>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td width="3%"></td>


                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlogAmortizimi" ID="ASPxLabel1"
                                                        runat="server" Text="Llogari Amortizimi:" ClientInstanceName="lblLlogAmortizimi">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCell">
                                                    <dx:ASPxComboBox ID="cmbLlogAmortizimi" runat="server" ClientInstanceName="cmbLlogAmortizimi"
                                                        EnableCallbackMode="True" OnItemRequestedByValue="cmbLlogAmortizimi_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbLlogAmortizimi_ItemsRequestedByFilterCondition"
                                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                                        <ClientSideEvents ButtonClick="function(s, e) {

 txtLlog.SetText('amor');
LlogariAmor_Click();
}"
        TextChanged="function(s, e){OnChange(s,e);}"                                                     Init="function(s, e) {
		
}"
                                                            EndCallback="function(s, e) {
	if(cmbLlogAmortizimi.GetEnabled()==false)
  { cmbLlogAmortizimi .HideDropDown()
  }
}" />
                                                        <DropDownButton>
                                                            <Image>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </Image>
                                                        </DropDownButton>
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                                            ValidateOnLeave="false">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                        <DisabledStyle Font-Bold="False">
                                                        </DisabledStyle>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td width="3%"></td>
                                            </tr>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="btnLlogPakesim" ID="lblLlogPakesim"
                                                    runat="server" Text="Llogari Pakesim Vlere Dalje:" ClientInstanceName="lblLlogPakesim">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCell">
                                                <dx:ASPxComboBox ID="btnLlogPakesim" runat="server" ClientInstanceName="btnLlogPakesim"
                                                    EnableCallbackMode="True" OnItemRequestedByValue="btnLlogPakesim_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btnLlogPakesim_ItemsRequestedByFilterCondition"
                                                    SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                                    <ClientSideEvents ButtonClick="function(s, e) {

 txtLlog.SetText('pakesim');
LlogariPakesim_Click();
}"
                                                        Init="function(s, e) {
	
}"
                                                        EndCallback="function(s, e) {
	if(btnLlogPakesim.GetEnabled()==false)
  { btnLlogPakesim .HideDropDown()
  }
}" />
                                                    <DropDownButton>
                                                        <Image>
                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                        </Image>
                                                    </DropDownButton>
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                                        ValidateOnLeave="false">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RequiredField IsRequired="true" />
                                                    </ValidationSettings>
                                                    <DisabledStyle Font-Bold="False">
                                                    </DisabledStyle>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td width="3%"></td>

                                        </table>
                                        <br />
                                        <br />
                                        <dx:ASPxGridView ID="gvAmortizimi" runat="server" Width="100%"
                                            ClientInstanceName="gvAmortizimi" OnHtmlRowCreated="gvAmortizimi_HtmlRowCreated"
                                            OnCustomCallback="gvAmortizimi_CustomCallback"
                                            OnCustomJSProperties="gvAmortizimi_CustomJSProperties" ClientIDMode="AutoID">
                                            <ClientSideEvents RowDblClick="function(s, e){ 
                          }"
                                                EndCallback="function(s, e) { }" BeginCallback="function(s, e) {

}" />
                                            <Styles>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                            <SettingsPager PageSize="15">
                                            </SettingsPager>
                                            <StylesEditors>
                                                <ProgressBar Height="25px">
                                                </ProgressBar>
                                            </StylesEditors>
                                        </dx:ASPxGridView>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                        </TabPages>

                    </dxtc:ASPxPageControl>
                    <asp:HiddenField ID="hfRuaj" runat="server" />
                    <asp:HiddenField ID="hfPrindi" runat="server" />
                    <asp:HiddenField ID="hfNiveli" runat="server" />
                    <asp:HiddenField ID="hfGrupi" runat="server" />
                    <asp:HiddenField ID="hfSkemaKlasa" runat="server" />
                    <asp:HiddenField ID="hfSkema" runat="server" />

                    <asp:HiddenField ID="hfLupaLlogInv" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogBle" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogShit" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogTretet" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogShpe" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogAmortizimi" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogPakesim" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"></dx:ASPxHiddenField>
                    <dx:ASPxLabel ID="pergjigja" runat="server" ForeColor="Green">
                    </dx:ASPxLabel>
                    <dx:ASPxTextBox ID="txtLlog" runat="server" Text="" Visible="true" ClientInstanceName="txtLlog"
                        Width="0%" EnableTheming="False" BackColor="White" Border-BorderColor="White"
                        ForeColor="White">
                        <Border BorderColor="White" />
                    </dx:ASPxTextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" AllowResize="True"
                    AppearAfter="10" ClientIDMode="AutoID" ClientInstanceName="popupUniversal" CloseAction="CloseButton"
                    EnableAnimation="False" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter">
                    <ClientSideEvents Closing="function(s, e) {
	popupUniversal.SetContentUrl('');
}" />
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
