<%@ Page Language="C#" Title="" AutoEventWireup="true" CodeBehind="Shto_Autorizimet.aspx.cs"
    Inherits="PlatinumWeb.Shto_Autorizimet" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>


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
<html>
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <%--    <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myButtonClickLupa-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myBuxhet-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myFushaShtese-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myMesazh-IMB.2.1.js?versioni22" type="text/javascript"></script>
     
    <script src="js/aspx.js/Shto_Autorizimet.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script>--%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Shto_Autorizimet.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <asp:ScriptManager ID="ScriptManager2" runat="server">
          </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
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
                            </dx:ASPxPanel >
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvDega">
            <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server"   TabSpacing="3px"
                ClientInstanceName="PageControl" Width="100%" ActiveTabIndex="1">
                <ContentStyle>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                </ContentStyle>
                <TabPages>
                    <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                        <ContentCollection>
                            <dxw:ContentControl>
                                <dx:ASPxGridView ID="ASPxGridView_Autorizimet" runat="server" ClientInstanceName="ASPxGridView_Autorizimet"
                                    OnDataBound="ASPxGridView_Autorizimet_DataBound" OnAfterPerformCallback="ASPxGridView_Autorizimet_AfterPerformCallback"
                                    OnHeaderFilterFillItems="ASPxGridView_Autorizimet_HeaderFilterFillItems" OnProcessColumnAutoFilter="ASPxGridView_Autorizimet_ProcessColumnAutoFilter"
                                    OnCustomCallback="ASPxGridView_Autorizimet_CustomCallback" OnCustomJSProperties="ASPxGridView_Autorizimet_CustomJSProperties"
                                    Width="50%">
                                    <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                        SelectionChanged="function(s, e){OnGridSelectionChanged(e);}" FocusedRowChanged="function(s, e) {
            mbush=true;	
}"
                                        BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                                    <%-- <Styles    >
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>--%>
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                                <br />
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Name="Autorizimet" Text="Autorizimet">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl3" runat="server">
                                <div style="width: 100%">
                                    <table class="renditKontrolle">
                                        <tr>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                                    Text="Kodi:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth25">
                                                <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" ClientInstanceName="txtKodi">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                        ValidationGroup="entries">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RequiredField IsRequired="true" />
                                                    </ValidationSettings>
                                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi" ID="lblPershkrimi"
                                                    runat="server" Text="Pershkrimi:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td rowspan="3" class="renditKontrolleCellMeWidth25">
                                                <dx:ASPxMemo ID="txtPershkrimi" runat="server" Width="100%" ClientInstanceName="txtPershkrimi" Rows="3">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                        ValidationGroup="entries">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RequiredField IsRequired="true" />
                                                    </ValidationSettings>
                                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dx:ASPxMemo>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth25"></td>
                                            <td class="renditKontrolleCellMeWidth25"></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Selekto te gjitha ne kete faqe"
                                                    Width="100%" ValidationGroup="entries" AutoPostBack="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
	grid.SelectAllRowsOnPage();
}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Hiq gjithe selektimet ne kete faqe"
                                                    Width="100%" ValidationGroup="entries" AutoPostBack="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
	 grid.UnselectAllRowsOnPage();
}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 700px">
                                        <tr>
                                            <%--<td width="15%">
                                                <dx:ASPxLabel ID="lblPerdoruesit" runat="server" Text="Perdoruesit">
                                                </dx:ASPxLabel>
                                            </td>--%>
                                            <td>
                                                <dx:ASPxGridView ID="grid_ListPerdoruesit" ClientInstanceName="grid" runat="server"
                                                    Width="100%" OnDataBound="grid_ListPerdoruesit_DataBound" OnAutoFilterCellEditorInitialize="grid_ListPerdoruesit_AutoFilterCellEditorInitialize"
                                                    OnAfterPerformCallback="grid_ListPerdoruesit_AfterPerformCallback" OnHeaderFilterFillItems="grid_ListPerdoruesit_HeaderFilterFillItems"
                                                    OnProcessColumnAutoFilter="grid_ListPerdoruesit_ProcessColumnAutoFilter" OnCustomCallback="grid_ListPerdoruesit_CustomCallback">
                                                    <Styles>
                                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                        </Header>
                                                    </Styles>
                                                    <StylesEditors>
                                                        <ProgressBar Height="25px">
                                                        </ProgressBar>
                                                    </StylesEditors>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Name="LidhjaAutorizimeve" Text="Lidhja e autorizimeve">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                <div style="width: 100%">
                                    <table class="renditKontrolle">
                                        <tr>
                                            <td class="renditKontrolleCaption"></td>
                                            <dx:ASPxGridView ID="gridLidhjeAutorizime" ClientInstanceName="gridLidhjeAutorizime" OnAfterPerformCallback="gridLidhjeAutorizime_AfterPerformCallback" OnCustomCallback="gridLidhjeAutorizime_CustomCallback" runat="server"
                                                Width="100%" OnDataBound="gridLidhjeAutorizime_DataBound" OnHeaderFilterFillItems="gridLidhjeAutorizime_HeaderFilterFillItems">
                                                <Styles>
                                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                    </Header>
                                                </Styles>
                                                <StylesEditors>
                                                    <ProgressBar Height="25px">
                                                    </ProgressBar>
                                                </StylesEditors>
                                            </dx:ASPxGridView>
                                        </tr>
                                    </table>
                                </div>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="Active_TabChanged" />
            </dxtc:ASPxPageControl >
        </div>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server"></dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfKontrollet" runat="server" />
                <asp:HiddenField ID="hfStatusi" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
