<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_Tatime.aspx.cs" Inherits="PlatinumWeb.Shto_Tatime" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>


<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <%--    <script src="js/Utils-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myButtonClickLupa-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myBuxhet-IMB.2.1.js?versioni22" type="text/javascript"></script>
     
    <script src="js/myFushaShtese-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myJQGrid-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/aspx.js/Shto_Tatime.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script>--%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/aspx.js/Shto_Tatime.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
           </asp:ScriptManager>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
            </dx:ASPxGlobalEvents>
            <dx:ASPxHiddenField ID="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled">
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
            <div id="dvTatime" style="display: none">
                <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" ClientInstanceName="PageControl"
                      TabSpacing="3px" Width="100%" ActiveTabIndex="0" Height="400px"
                    ClientIDMode="AutoID">
                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    </ContentStyle>
                    <TabPages>
                        <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme" NewLine="True">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl1" runat="server">
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
                                    <br />
                                    <hr />
                                    <br />
                                    <table id="tblData" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <br />
                                    <br />
                                    <%--<div id="dvlblNdryshimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNdryshimi" ID="lblNdryshimi"
                                        runat="server" Text="Data e ndryshimit:" ClientInstanceName="lblNdryshimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbNdryshimi" style="visibility: hidden;">--%>
                                    <asp:UpdatePanel ID="cmbNdryshimi_pnlNdryshimi" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <dx:ASPxComboBox ID="cmbNdryshimi" runat="server" ClientInstanceName="cmbNdryshimi"
                                                Width="100%" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                                <ClientSideEvents SelectedIndexChanged="function (s,e){gvTatimet.PerformCallback('ndryshodate');}" />
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
                                    <dx:ASPxGridView ID="gvTatimet" ClientInstanceName="gvTatimet" runat="server"
                                        Width="100%" OnDataBound="gvTatimet_DataBound" OnAfterPerformCallback="gvTatimet_AfterPerformCallback"
                                        OnHeaderFilterFillItems="gvTatimet_HeaderFilterFillItems" OnCustomCallback="gvTatimet_CustomCallback"
                                        OnCustomJSProperties="gvTatimet_CustomJSProperties" OnProcessColumnAutoFilter="gvTatimet_ProcessColumnAutoFilter"
                                        Style="margin-bottom: 33px">                                        
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {
 try{  if(  PageControl.GetActiveTabIndex()==0)       mbush=true;} catch(r){}	
}"
                                            RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }" SelectionChanged="function(s, e){OnGridSelectionChanged(e);}"
                                            BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
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
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Tatimet" Text="Tatimet" >
                           
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl3" runat="server">
                                    <table id="tblTatime" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblDateAkt">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDateAkt" ID="lblDateAkt" runat="server"
                                        Text="Date Aktivizimi:" ClientInstanceName="lblDateAkt">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvdteDateAkt" style="visibility: hidden; display: 'none';">--%>
                                    <dx:ASPxDateEdit ID="dteDateAkt" runat="server" ClientInstanceName="dteDateAkt"
                                        ShowShadow="False" Width="100%">
                                        <ClientSideEvents DateChanged="function (s,e){ llogaritmin();
                                        }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
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
                                        <ValidationSettings ValidationGroup="entries2">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                    <%--</div>--%>
                                    <%--<div id="dvlblMin">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtMin" ID="lblMin" runat="server"
                                        Text="Minimumi:" ClientInstanceName="lblMin">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtMin">--%>
                                    <dx:ASPxTextBox ID="txtMin" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtMin"
                                        DisplayFormatString="0.00">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Minimumi duhet te jete numer" ValidationExpression="[0-9,.\-]*"></RegularExpression>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblMax">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtMax" ID="lblMax" runat="server"
                                        Text="Maximumi:" ClientInstanceName="lblMax">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtMax">--%>
                                    <dx:ASPxTextBox ID="txtMax" runat="server" Width="100%" AutoPostBack="false" DisplayFormatString="0.00"
                                        ClientInstanceName="txtMax">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Maximumi duhet te jete numer" ValidationExpression="[0-9,.\-]*"></RegularExpression>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblMenyra">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMenyra" ID="lblMenyra" runat="server"
                                        ClientInstanceName="lblMenyra" Text="Menyra:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbMenyra">--%>
                                    <dx:ASPxComboBox ID="cmbMenyra" runat="server" ClientInstanceName="cmbMenyra" Width="100%"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
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
                                    <%--<div id="dvlblNorma">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNorma" ID="lblNorma" runat="server"
                                        Text="Norma:" ClientInstanceName="lblNorma">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtNorma">--%>
                                    <dx:ASPxTextBox ID="txtNorma" runat="server" ClientInstanceName="txtNorma"
                                        Enabled="true" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                    <ClientSideEvents ActiveTabChanged="Active_TabChanged" />
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                </dxtc:ASPxPageControl >
            </div>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfModel" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
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
