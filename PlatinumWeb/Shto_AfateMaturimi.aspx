<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_AfateMaturimi.aspx.cs"
    Inherits="PlatinumWeb.Shto_AfateMaturimi" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/ucMenuAndMsgFrame.ascx" TagPrefix="ucMenu" TagName="ucMenuAndMsgFrame" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Shto_AfateMaturimi.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" runat="server" OnMenuClick="Menu_ItemClick" OnMenuTemplate="PercaktoTemplateMenu" OnFilterSave="Ruaj_ASPxButton_Click" OnFilterDelete="FshiFilter_ASPxButton_Click" />
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
        <div id="dvAfateMaturimi" style="display: none">
            <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
                TabSpacing="3px" Width="100%" ActiveTabIndex="0">
                <ContentStyle>
                    <border bordercolor="#AECAF0" borderstyle="Solid" borderwidth="1px" />
                </ContentStyle>
                <TabPages>
                    <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl2" runat="server">
                                <table class="renditKontrolle">
                                    <tbody>
                                        <tr>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label" runat="server" Style="font-size: large" Text="Modeli:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33">
                                                <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                    Height="23px" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False"
                                                    Style="font-size: medium" Width="100%" AnimationType="None">
                                                    <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                                                    <DropDownButton>
                                                        <Image>
                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                        </Image>
                                                    </DropDownButton>
                                                    <ValidationSettings>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td class="renditKontrolleLabelMeWidth33">
                                                <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33"></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <dx:ASPxGridView ID="gvAfateMaturimi" ClientInstanceName="gvAfateMaturimi" runat="server"
                                    Width="98%" OnDataBound="gvAfateMaturimi_DataBound" OnAfterPerformCallback="gvAfateMaturimi_AfterPerformCallback"
                                    OnHeaderFilterFillItems="gvAfateMaturimi_HeaderFilterFillItems" OnCustomCallback="gvAfateMaturimi_CustomCallback">
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <Templates>
                                        <TitlePanel>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Zgjidh kolonat" AutoPostBack="false"
                                                            ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                            <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s,e,gvAfateMaturimi)}"
                                                                Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="pnlruaj" runat="server">
                                                            <ContentTemplate>
                                                                <dx:ASPxButton ID="ASPxButton3" runat="server" Text="Ruaj kolonat" AutoPostBack="true"
                                                                    ClientVisible="false" Image-Url="images/new/disk_blue (3).png" Font-Size="8"
                                                                    OnClick="RuajKolona_Click">
                                                                    <ClientSideEvents Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                                </dx:ASPxButton>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                    </Templates>
                                    <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                        SelectionChanged="function(s, e){OnGridSelectionChanged(e);}" FocusedRowChanged="function(s, e) {
            mbush=true;	
}"
                                        BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Name="Afate Maturimi" Text="Afate Maturimi">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                <table id="tblAfateMaturimi" class="renditKontrolle">
                                    <tbody>
                                    </tbody>
                                </table>
                                <%--<div id="dvlblKodi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server" Text="Kodi:" ClientInstanceName="lblKodi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtxtKodi">--%>
                                <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere" ValidationExpression="^[\s\S]{0,20}$"></RegularExpression>
                                        <RequiredField ErrorText="*" IsRequired="True" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>
                                <%--</div>--%>
                                <%--<div id="dvlblEmertimi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi" ID="lblEmertimi" runat="server" Text="Emertimi:" ClientInstanceName="lblEmertimi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtxtEmertimi">--%>
                                <dx:ASPxMemo ID="txtEmertimi" runat="server" Width="100%" AutoPostBack="false"
                                    ClientInstanceName="txtEmertimi" Rows="3">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxMemo>
                                <%--</div>--%>
                                <%--<div id="dvlblLloji">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLloji" ID="lblLloji" runat="server" Text="Lloji:" ClientInstanceName="lblLloji">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcmbLloji">--%>
                                <dx:ASPxComboBox ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji"
                                    ShowShadow="False" ValidationSettings-CausesValidation="True"
                                    ValidationSettings-SetFocusOnError="True" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField ErrorText="*" IsRequired="True" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblDtFillimi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbDtFillimi" ID="lblDtFillimi" runat="server" Text="Dt. Fillimi Maturimi:" ClientInstanceName="lblDtFillimi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcmbDtFillimi">--%>
                                <dx:ASPxComboBox ID="cmbDtFillimi" runat="server" ClientInstanceName="cmbDtFillimi"
                                    ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField ErrorText="*" IsRequired="True" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblPeriudha">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbPeriudha" ID="lblPeriudha" runat="server" Text="Periudha:" ClientInstanceName="lblPeriudha">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcmbPeriudha">--%>
                                <dx:ASPxComboBox ID="cmbPeriudha" runat="server" ClientInstanceName="cmbPeriudha"
                                    ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField ErrorText="*" IsRequired="True" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblPercaktimi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPercaktimi" ID="lblPercaktimi" runat="server" Text="Percaktimi:" ClientInstanceName="lblPercaktimi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtxtPercaktimi">--%>
                                <dx:ASPxTextBox ID="txtPercaktimi" runat="server" Width="100%" AutoPostBack="false"
                                    ClientInstanceName="txtPercaktimi">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true"
                                        RegularExpression-ValidationExpression="\d+" RegularExpression-ErrorText="Kjo fushe mund te permbaje vetem numra te plote.">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RegularExpression ErrorText="Kjo fushe mund te permbaje vetem numra te plote." ValidationExpression="\d+"></RegularExpression>
                                        <RequiredField ErrorText="*" IsRequired="True" />
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
            </dxtc:ASPxPageControl>
        </div>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <dx:ASPxLabel ID="pergjigja" runat="server" Text="" EnableViewState="False" ForeColor="Red">
                </dx:ASPxLabel>
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfStatusi" runat="server" />
                <asp:HiddenField ID="hfKontrollet" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                            CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Zgjidh Autorizimin "
                            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl6" runat="server">
                                    <iframe id="Container" name="ContainerAutorizime" frameborder="0" runat="server"></iframe>
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
