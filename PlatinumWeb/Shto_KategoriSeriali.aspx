<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_KategoriSeriali.aspx.cs" Inherits="PlatinumWeb.Shto_KategoriSeriali" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Shto_KategoriSeriali.aspx-IMB.6.8.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" >
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvBurimi" style="display: none">
            <dx:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" TabSpacing="3px"
                ClientInstanceName="PageControl" Width="100%" ActiveTabIndex="0">
                <ContentStyle>
                    <border bordercolor="#AECAF0" borderstyle="Solid" borderwidth="1px" />
                </ContentStyle>
                <TabPages>
                    <dx:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                        <ContentCollection>
                            <dx:ContentControl>
                                <table class="renditKontrolle">
                                    <tbody>
                                        <tr>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label" runat="server" Style="font-size: large" Text="Modeli:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33">
                                                <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" Width="100%"
                                                    Height="23px" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Style="font-size: medium" AnimationType="None">
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
                                <dx:ASPxGridView ID="gvKategorite" ClientInstanceName="gvKategorite" runat="server"
                                    Width="100%" OnDataBound="gvKategorite_DataBound" OnAfterPerformCallback="gvKategorite_AfterPerformCallback"
                                    OnHeaderFilterFillItems="gvKategorite_HeaderFilterFillItems" OnProcessColumnAutoFilter="gvKategorite_ProcessColumnAutoFilter"
                                    OnCustomJSProperties="gvKategorite_CustomJSProperties" OnCustomCallback="gvKategorite_CustomCallback"
                                    OnAutoFilterCellEditorInitialize="gvKategorite_AutoFilterCellEditorInitialize">
                                    <Templates>
                                        <TitlePanel>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ASPxButton2" runat="server" ToolTip="Zgjidh kolonat" AutoPostBack="false"
                                                            ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                            <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s,e,gvKategorite)}"
                                                                Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="pnlruaj" runat="server">
                                                            <ContentTemplate>
                                                                <dx:ASPxButton ID="ASPxButton3" runat="server" ToolTip="Ruaj kolonat" AutoPostBack="true"
                                                                    ClientVisible="false" Image-Url="images/new/disk_blue (3).png" Font-Size="8"
                                                                    OnClick="RuajKolona_Click">
                                                                    <ClientSideEvents Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                                </dx:ASPxButton>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="gridaSelectFaqe" runat="server" ToolTip="Zgjidh te gjithe faqen"
                                                            AutoPostBack="false" Image-Url="images/check2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) { gvKategorite.SelectAllRowsOnPage(); }" />
                                                        </dx:ASPxButton>

                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe"
                                                            AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) { gvKategorite.SelectRows(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="gridaUnSelectTeGjitha" runat="server" ToolTip="Fshi Zgjedhjen"
                                                            AutoPostBack="false" Image-Url="images/uncheck2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) { gvKategorite.UnselectRows(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                    </Templates>
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
                                    <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                        SelectionChanged="function(s, e){OnGridSelectionChanged(e);}" FocusedRowChanged="function(s, e) {
            mbush=true;	
}"
                                        BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                                    <StylesEditors>
                                        <CalendarHeader Spacing="1px">
                                        </CalendarHeader>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="Informacion" Text="Informacion">

                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl3" runat="server">
                                <table id="tblKategorite" class="renditKontrolleDy">
                                    <tbody>
                                    </tbody>
                                </table>
                                <%--<div id="dvlblKodi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server" Text="Kodi:" ClientInstanceName="lblKodi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtxtKodi">--%>
                                <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi">
                                    <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true"
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
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi" ID="lblEmertimi" runat="server" Text="Emertimi:" ClientInstanceName="lblEmertimi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtxtEmertimi">--%>
                                <dx:ASPxMemo ID="txtEmertimi" runat="server" Width="100%" AutoPostBack="false"
                                    ClientInstanceName="txtEmertimi" Rows="3">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxMemo>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimeFtp" ID="lblKonfigurimeFtp"
                                        runat="server" Text="Konfigurime Ftp:" ClientInstanceName="lblKonfigurimeFtp">
                                </dx:ASPxLabel>
                                <div>
                                    <select id="cmbKonfigurimeFtp">
                                    </select>
                                    <asp:HiddenField ID="cmbKonfigurimeFtpHf" ClientIDMode="Static" runat="server" />           
                                </div>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="rbTipi" ID="lblTipi" runat="server" Text="Tipi" ClientInstanceName="lblTipi">
                                            </dx:ASPxLabel>

                                            <dx:ASPxRadioButtonList ID="rbTipi" runat="server" ValueType="System.String"
                                                ClientInstanceName="rbTipi" Width="168px">
                                                <Items>
                                                    <dx:ListEditItem Text="TXT" Value="TXT" Selected="true" />
                                                    <dx:ListEditItem Text="XLS" Value="XLS" Selected="true" />
                                                    <dx:ListEditItem Text="XLSX" Value="XLSX" Selected="false" />
                                                    <dx:ListEditItem Text="CSV" Value="CSV" Selected="false" />
                                                </Items>
                                                <ClientSideEvents SelectedIndexChanged="function (s,e){ enabled()}" />
                                            </dx:ASPxRadioButtonList>

                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtSimboliNdares" ID="lblSimboliNdares" ClientInstanceName="lblSimboliNdares" runat="server" Text="Simboli Ndares">
                                            </dx:ASPxLabel>

                                            <dx:ASPxTextBox ID="txtSimboliNdares" runat="server" Width="170px"
                                                ClientInstanceName="txtSimboliNdares">
                                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                </DisabledStyle>
                                            </dx:ASPxTextBox>

                                            <dx:ASPxCheckBox ID="cbSimboliNdares" runat="server" Text="Tab" ClientInstanceName="cbSimboliNdares">
                                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                </DisabledStyle>
                                            </dx:ASPxCheckBox> 

                                 <dx:ASPxLabel Wrap="False" AssociatedControlID="cbAktiv" ID="lblMeEmertimKolone" runat="server"
                                    ClientInstanceName="lblMeEmertimKolone" Text="Formati ka kolona:">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcbAktiv">--%>
                                <dx:ASPxCheckBox ID="cbMeEmertimKolone" runat="server" ClientInstanceName="cbMeEmertimKolone" Width="100%">
                                </dx:ASPxCheckBox>
                                <br />
                                <br />

                                <dx:ASPxGridView ID="gvImporti" runat="server" ClientInstanceName="gvImporti" OnAfterPerformCallback="gvImporti_AfterPerformCallback"
                                    OnHtmlRowCreated="gvImporti_HtmlRowCreated" OnDataBound="gvImporti_DataBound"
                                    OnCustomJSProperties="gvImporti_CustomJSProperties" OnCustomCallback="gvImporti_CustomCallback">
                                    <ClientSideEvents BeginCallback="function(s, e) { merrTeDhenaFushaImporti(); }" />
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                                
                                <%--<dx:ASPxGridView ID="gvSeriali" runat="server" ClientInstanceName="gvSeriali" OnAfterPerformCallback="gvSeriali_AfterPerformCallback"
                                    OnHtmlRowCreated="gvSeriali_HtmlRowCreated" OnCustomCallback="gvSeriali_CustomCallback"
                                    OnCustomJSProperties="gvSeriali_CustomJSProperties" Width="30%" OnDataBound="gvSeriali_DataBound">
                                    <ClientSideEvents BeginCallback="function(s, e) {
	
             merrTeDhena();
}" EndCallback="function (s,e){aktivizoGride();}" />
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>--%>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="Active_TabChanged" />
            </dx:ASPxPageControl>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfKodi" runat="server" />
                <asp:HiddenField ID="hfFushat" runat="server" />
                <asp:HiddenField ID="hfKontrollet" runat="server" />
                <asp:HiddenField ID="hfStatusi" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" /> 
                <asp:HiddenField ID="hfKonfigurimeFtp" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>

    </form>
</body>
</html>

