<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserThemes.aspx.cs" Inherits="PlatinumWeb.UserThemes" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>






<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <%-- <script src="js/myCookies-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/myMesazh-IMB.2.1.js?versioni22" type="text/javascript"></script>    
    <script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
     
    <script src="js/Utils-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/aspx.js/UserThemes.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script>--%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/aspx.js/UserThemes.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
   </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
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
                                                        Text="Ok" OnClick="ButtonOk_Click2">
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
    <div id="dvUserThemes">
        <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
              TabSpacing="3px" Width="100%" ActiveTabIndex="1">
            <ContentStyle>
                <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
            </ContentStyle>
            <TabPages>
                <dxtc:TabPage Name="Lista" Text="Lista">
                    <ContentCollection>
                        <dxw:ContentControl ID="ContentControl2" runat="server">
                            <dx:ASPxGridView ID="gvThemesAmbjente" ClientInstanceName="gvThemesAmbjente" runat="server"
                                Width="98%" OnDataBound="gvThemesAmbjente_DataBound" OnAfterPerformCallback="gvThemesAmbjente_AfterPerformCallback"
                                OnHeaderFilterFillItems="gvThemesAmbjente_HeaderFilterFillItems" OnCustomCallback="gvThemesAmbjente_CustomCallback"
                                OnAutoFilterCellEditorInitialize="gvThemesAmbjente_AutoFilterCellEditorInitialize">
                                <Styles>
                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                    </Header>
                                </Styles>
                                <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex); kaloTab=true;}"
                                    FocusedRowChanged="function(s, e) {gridFocusRowCanged(s,e);}" BeginCallback="function(s, e) {BeginCallback(s,e);}"
                                    SelectionChanged="function(s, e) {OnGridSelectionChanged(s,e);}" />
                                <StylesEditors>
                                    <CalendarHeader Spacing="1px">
                                    </CalendarHeader>
                                    <ProgressBar Height="25px">
                                    </ProgressBar>
                                </StylesEditors>
                                <SettingsEditing Mode="Inline" />
                            </dx:ASPxGridView>
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
                <dxtc:TabPage Name="Te Pergjithshme" Text="Te Pergjithshme">
                    <ContentCollection>
                        <dxw:ContentControl>
                            <table class="renditKontrolleDy">
                                <tr>
                                    <td class="renditKontrolleCaption">
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="ASPxLabel4" runat="server"
                                            Text="Kodi: ">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="renditKontrolleCellMeWidth50">
                                        <dx:ASPxTextBox ID="txtKodi" ClientInstanceName="txtKodi" runat="server" Width="100%">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries" ValidateOnLeave="true"
                                                ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="true" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere dhe nuk duhet te permbaje hapsira!"
                                                    ValidationExpression="^[\s\S]{0,20}$"></RegularExpression>
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="renditKontrolleCaption">
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbDefault" ID="ASPxLabel6" runat="server"
                                            Text="Default: ">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="renditKontrolleCellMeWidth50">
                                        <dx:ASPxCheckBox ID="cbDefault" runat="server" ClientInstanceName="cbDefault" Enabled="true"
                                            ClientEnabled="false" TextSpacing="2px">
                                        </dx:ASPxCheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="renditKontrolleCaption">
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi" ID="ASPxLabel5" runat="server"
                                            Text="Pershkrimi: ">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td rowspan="2" class="renditKontrolleCellMeWidth50">
                                        <dx:ASPxMemo ID="txtPershkrimi" ClientInstanceName="txtPershkrimi" runat="server"
                                            Width="100%" Rows="3">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </td>
                                    <td class="renditKontrolleCaption">
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbZgjedhur" ID="ASPxLabel7" runat="server"
                                            Text="Zgjedhur: ">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="renditKontrolleCellMeWidth50">
                                        <dx:ASPxCheckBox ID="cbZgjedhur" runat="server" ClientInstanceName="cbZgjedhur" Enabled="true"
                                            ClientEnabled="false" TextSpacing="2px">
                                        </dx:ASPxCheckBox>
                                    </td>
                                </tr>
                            </table>
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
                <dxtc:TabPage Name="Menute" Text="Menute">
                    <ContentCollection>
                        <dxw:ContentControl ID="ContentControlMenuThemes" ClientIDMode="AutoID" runat="server">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Zgjidhni temen qe deshironi per menute:">
                            </dx:ASPxLabel>
                            <br />
                            <dx:ASPxDataView ID="dataViewFrames" runat="server" Width="100%" AllowPaging="false"
                                ColumnCount="4" SettingsLoadingPanel-ImagePosition="Top" ClientInstanceName="dataViewFrames"
                                BackgroundImage-Repeat="Repeat">
                                <ItemTemplate>
                                    <div style="height: 100%; width: 100%">
                                        <div style="text-align: center">
                                            <dx:ASPxLabel ID="emriThemeFramet" Style="font-weight: 700; color: #333333" runat="server"
                                                Text='<%# Eval("EMRITHEME") %>' ToolTip='<%# Eval("EMRITHEME") %>' />
                                        </div>
                                        <br />
                                        <div style="text-align: center">
                                            <dx:ASPxImage ID="ImgThemeFramet" runat="server" ImageUrl='<%# Eval("URLIMGPREVIEW") %>'
                                                ImageAlign="Middle" ToolTip='<%# Eval("EMRITHEME") %>'>
                                                <ClientSideEvents Click="function(s, e){ku = 'framet'; checkUncheckCheckboxin(s, e, ku);}" />
                                            </dx:ASPxImage>
                                        </div>
                                        <br />
                                        <table width="100%">
                                            <tr align="center">
                                                <%--<td style="text-align: left; width: 15%">
                                                    <dx:ASPxButton ID="btnPreviewFramet" runat="server" ClientInstanceName='<%# Eval("IDTHEME")+"framePreview" %>'
                                                        Text="Preview"    
                                                        AutoPostBack="false"  >
                                                        <ClientSideEvents Click="function (s,e){ku='framet';apliko(s,e,false);}" />
                                                        <ClientSideEvents Click="Utils.PreviewThemeFrames"  />
                                                    </dx:ASPxButton>
                                                </td>--%>
                                                <td>
                                                    <dx:ASPxCheckBox ID="cbZgjidhFramet" ClientInstanceName='<%# Eval("IDTHEME")+"framet" %>'
                                                        runat="server" Text='Zgjidh' AutoPostBack="false" ToolTip='<%# Eval("EMRITHEME") %>'>
                                                        <ClientSideEvents CheckedChanged="function (s,e) { ku='framet';
                                                                                                           zgjidhThemes(s, e, ku);
                                                                                                          }" />
                                                    </dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </dx:ASPxDataView>
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
                <dxtc:TabPage Name="Ambjenti Kryesor" Text="Ambjenti Kryesor">
                    <ContentCollection>
                        <dxw:ContentControl ID="ContentControl3" runat="server">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Zgjidhni temen qe deshironi per komponentet e ambjentit kryesor:">
                            </dx:ASPxLabel>
                            <br />
                            <dx:ASPxDataView ID="dataViewAmbjKryesor" runat="server" Width="100%" AllowPaging="false"
                                ColumnCount="4" SettingsLoadingPanel-ImagePosition="Top" ClientInstanceName="dataViewAmbjKryesor"
                                BackgroundImage-Repeat="Repeat">
                                <ItemTemplate>
                                    <div style="height: 100%; width: 100%">
                                        <div style="text-align: center">
                                            <dx:ASPxLabel ID="emriThemeAmbjKr" Style="font-weight: 700; color: #333333" runat="server"
                                                Text='<%# Eval("EMRITHEME") %>' />
                                        </div>
                                        <br />
                                        <div style="text-align: center">
                                            <dx:ASPxImage ID="ImgThemeAmbjKr" runat="server" ImageUrl='<%# Eval("URLIMGPREVIEW") %>'
                                                ImageAlign="Middle" ToolTip='<%# Eval("EMRITHEME") %>'>
                                                <ClientSideEvents Click="function(s, e){ku = 'ambjKr'; checkUncheckCheckboxin(s, e, ku);}" />
                                            </dx:ASPxImage>
                                        </div>
                                        <br />
                                        <table width="100%">
                                            <tr align="center">
                                                <%--<td style="text-align: left; width: 15%">
                                                    <dx:ASPxButton ID="btnPreviewAmbjKr" runat="server" ClientInstanceName='<%# Eval("IDTHEME") + "ambjKrPreview" %>'
                                                        Text="Preview"    
                                                        AutoPostBack="True"   ToolTip='<%# Eval("EMRITHEME") %>'>
                                                        <ClientSideEvents Click="Utils.PreviewThemeAmbjentKryesor"  />
                                                    </dx:ASPxButton>
                                                </td>--%>
                                                <td>
                                                    <dx:ASPxCheckBox ID="cbZgjidhAmbjKr" ClientInstanceName='<%# Eval("IDTHEME") + "ambjKr" %>'
                                                        runat="server" Text='Zgjidh' AutoPostBack="false" ToolTip='<%# Eval("EMRITHEME") %>'>
                                                        <ClientSideEvents CheckedChanged="function (s,e) { ku='ambjKr';
                                                                                                           zgjidhThemes(s, e, ku);                                                                                                           
                                                                                                         }" />
                                                    </dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </dx:ASPxDataView>
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
                <dxtc:TabPage Name="Gridat e Regjistrimeve" Text="Gridat e Regjistrimeve">
                    <ContentCollection>
                        <dxw:ContentControl ID="ContentControl5" runat="server">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Zgjidhni temen qe deshironi per gridat e regjistrimeve:">
                            </dx:ASPxLabel>
                            <br />
                            <dx:ASPxDataView ID="dataViewJQuery" runat="server" Width="100%" AllowPaging="false"
                                ColumnCount="4" SettingsLoadingPanel-ImagePosition="Top" ClientInstanceName="dataViewJQuery"
                                BackgroundImage-Repeat="Repeat">
                                <ItemTemplate>
                                    <div style="height: 100%; width: 100%">
                                        <div style="text-align: center">
                                            <dx:ASPxLabel ID="emriThemeJQuery" Style="font-weight: 700; color: #333333" runat="server"
                                                Text='<%# Eval("EMRITHEME") %>' />
                                        </div>
                                        <br />
                                        <div style="text-align: center">
                                            <dx:ASPxImage ID="ImgThemeJQuery" runat="server" ImageUrl='<%# Eval("URLIMGPREVIEW") %>'
                                                ImageAlign="Middle" ToolTip='<%# Eval("EMRITHEME") %>'>
                                                <ClientSideEvents Click="function(s, e){ku = 'jQuery'; checkUncheckCheckboxin(s, e, ku);}" />
                                            </dx:ASPxImage>
                                        </div>
                                        <br />
                                        <table width="100%">
                                            <tr align="center">
                                                <%--<td style="text-align: left; width: 15%">
                                                    <dx:ASPxButton ID="btnPreviewJQuery" runat="server" ClientInstanceName='<%# Eval("IDTHEME") + "jQueryPrev" %>'
                                                        Text="Preview"    
                                                        AutoPostBack="false"  >
                                                        <ClientSideEvents Click="function (s,e){apliko(s,e,false);}" />
                                                    </dx:ASPxButton>
                                                </td>--%>
                                                <td>
                                                    <dx:ASPxCheckBox ID="cbZgjidhJQuery" ClientInstanceName='<%# Eval("IDTHEME") + "jQuery" %>'
                                                        runat="server" Text='Zgjidh' AutoPostBack="false" ToolTip='<%# Eval("EMRITHEME") %>'>
                                                        <ClientSideEvents CheckedChanged="function (s,e) { ku='jQuery';                                                                                                          
                                                                                                           zgjidhThemes(s, e, ku);                                                                                                           
                                                                                                          }" />
                                                    </dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </dx:ASPxDataView>
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
                <dxtc:TabPage Name="Sfondi" Text="Sfondi">
                    <ContentCollection>
                        <dxw:ContentControl ID="ContentControl4" runat="server">
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Zgjidhni imazhin qe deshironi si sfond:">
                            </dx:ASPxLabel>
                            <br />
                            <%--<asp:UpdatePanel ID="UpdDataThemes" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                            <dx:ASPxDataView runat="server" EnableCallBacks="true" ClientIDMode="AutoID" Width="100%"
                                AllowPaging="false" PagerPanelSpacing="1px" OnPageIndexChanging="DataThemes_PageIndexChanging"
                                ColumnCount="4" SettingsLoadingPanel-ImagePosition="Top"
                                ID="DataThemes" ClientInstanceName="DataThemes" BackgroundImage-Repeat="Repeat"
                                OnCustomCallback="DataThemes_CustomCallback">
                                <PagerSettings Position="Bottom">
                                </PagerSettings>
                                <ClientSideEvents EndCallback="function(s, e){ endCallBackDataViewThemes(s, e);}" />
                                <ItemTemplate>
                                    <div>
                                        <table style="display: inline-block;">
                                            <tr>
                                                <td>
                                                    <div style="text-align: center">
                                                        <dx:ASPxLabel ID="lblYear" Style="font-weight: 700; color: #333333" runat="server"
                                                            Text='<%# Eval("EmriTheme") %>' />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="text-align: center">
                                                        <dx:ASPxImage ID="imgCover" runat="server" Height="170px" Width="200px" ImageUrl='<%# Eval("PathTheme") %>'
                                                            ImageAlign="Middle" ToolTip='<%# Eval("EMRITHEME") %>'>
                                                            <ClientSideEvents Click="function(s, e){ku = 'bgImg'; checkUncheckCheckboxin(s, e, ku);}" />
                                                        </dx:ASPxImage>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <%--<td style="text-align: left; width: 15%">
                                                                <dx:ASPxButton ID="btnApliko" ClientInstanceName='<%# Eval("EmriTheme") %>' runat="server"
                                                                    Text='Apliko'    
                                                                    AutoPostBack="false"   ToolTip='<%# Eval("EmriTheme") %>'>
                                                                    <ClientSideEvents Click="function (s,e){apliko(s,e,false);}" />
                                                                </dx:ASPxButton>
                                                            </td>--%>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="cbZgjidhBgImg" ClientInstanceName='<%# Eval("IDTHEME")+"bgImg" %>'
                                                                    runat="server" Text='Zgjidh' TextAlign="Left" Layout="OrderedList" AutoPostBack="false"
                                                                    ToolTip='<%# Eval("EMRITHEME") %>' AllowGrayedByClick="False">
                                                                    <ClientSideEvents CheckedChanged="function (s,e) { ku='bgImg';                                                                                                                       
                                                                                                                       zgjidhThemes(s, e, ku);                                                                                                           
                                                                                                                     }" />
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </dx:ASPxDataView>
                            <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
            </TabPages>
            <ClientSideEvents ActiveTabChanged="function(s, e) { tabsActiveTabChanged(s,e);}" />
            <%--<ClientSideEvents ActiveTabChanged="function(s, e) {
	           indexModifiko = gvThemesAmbjente.GetFocusedRowIndex();               
                if(mbush)
                  {
                    if(indexModifiko !=-1)
                    {
                        OnGridDoubleClick(indexModifiko);
                    }
                    else 
                    {
                        mbush = false;
                        $('#hfShtimModifikim')[0].value = 'shtim'; 
                        $('#hfId')[0].value = 0;
                        pastrofusha();
                    }
                  }
                  kaloTab=false;
                  myMenu.PercaktoMenuSipasTabit(e.tab.index);
            }" />--%>
        </dxtc:ASPxPageControl >
    </div>
    <asp:UpdatePanel ID="pnlData" runat="server">
        <ContentTemplate>
            <div style="visibility: hidden">
                <dx:ASPxButton ID="btn" runat="server" ClientInstanceName="btn" Text="Ruaj" OnClick="btn_Click">
                </dx:ASPxButton>
            </div>
            <asp:HiddenField ID="hfStatusi" runat="server" />
            <asp:HiddenField ID="hfId" runat="server" />
            <asp:HiddenField ID="hfShtimModifikim" runat="server" />
            <asp:HiddenField ID="hfThemeFramet" runat="server" />
            <asp:HiddenField ID="hfThemeAmbjKr" runat="server" />
            <asp:HiddenField ID="hfThemeJQuery" runat="server" />
            <asp:HiddenField ID="hfBgImage" runat="server" />
            <asp:HiddenField ID="hfIdBgImagePrev" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
    </dx:ASPxHiddenField>
    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="ASPxPopupControl1" runat="server" AllowDragging="True"
        ClientInstanceName="popPreview" CloseAction="CloseButton" EnableAnimation="False"
        EnableViewState="False" HeaderText="Preview" ContentUrl="" Font-Bold="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientIDMode="AutoID"
        Width="1000px" Height="700px" CssPostfix="Glass">
        <HeaderStyle>
            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
        </HeaderStyle>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl >
    </form>
</body>
</html>
