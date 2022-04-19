<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StrukturaAdministrative.aspx.cs"
    Inherits="PlatinumWeb.StrukturaAdministrative" %>

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
<head runat="server">
    <title>Alpha Web</title>
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
  <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/StrukturaAdministrative.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.    SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
         
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" AutoPostBack="true" ClientInstanceName="ASPxMenu1"
                                    ItemImagePosition="Top" OnDataBound="ASPxMenu1_DataBound" OnItemClick="ASPxMenu1_ItemClick"
                                    SeparatorWidth="1px" ShowPopOutImages="True" Width="100%">
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
                                        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lblMsgbox" runat="server"  ClientInstanceName="lblMsgbox" ClientIDMode="AutoID" Text="Duke fshire prindin, fshihen edhe te gjithe femijet e tij. Jeni i sigurt?">
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
                    <div style="visibility: hidden">
                        <dx:ASPxButton ID="ASPxButton1" runat="server" ClientInstanceName="btn" Text="ASPxButton">
                        </dx:ASPxButton>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
            </dx:ASPxHiddenField>
            <asp:HiddenField ID="hfArkivaDokId" runat="server" />
            <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfNrAutoKF" runat="server" ClientInstanceName="hfNrAutoKF">
            </dx:ASPxHiddenField>
            <table>
                <tbody>
                    <tr>
                        <td>
				            <dx:ASPxButton ID="btnXlsxExport" runat="server" ToolTip="Export to Xlsx" Image-Height="16px"
					            Image-Url="images/xlsx24.png" Font-Size="8" UseSubmitBehavior="false" OnClick="btnXlsxExport_Click">
					        </dx:ASPxButton>
				        </td>
				        <td>
					        <dx:ASPxButton ID="btnPdfExport" runat="server" ToolTip="Export to Pdf" Image-Height="16px"
						        Image-Url="images/pdf_icon.png" Font-Size="8" UseSubmitBehavior="false" OnClick="btnPdfExport_Click">
					        </dx:ASPxButton>
				        </td>
			        </tr>
                </tbody>
            </table>
            <asp:UpdatePanel ID="pnlTree" runat="server">
                <ContentTemplate>
                    <table class="CustomRenditKontrolleDy2">
                        <tbody>
                            <tr>
                                <td class="renditKontrolleCaption">
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                        runat="server" ClientIDMode="AutoID" Text="Modeli:">
                                    </dx:ASPxLabel>
                                </td>
                                <td class="renditKontrolleCellMeWidth75">
                                    <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                        ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top" AnimationType="None">
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
                                <td class="renditKontrolleCellMeWidth25">
                                    <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" Text="" class="klasePerLblKonfigurimi"
                                        ClientInstanceName="lblKonfigurimi">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <dx:ASPxTreeList ID="trlStruktura" runat="server"  SettingsSelection-Recursive="false"  ClientInstanceName="trlStruktura"
                        Width="100%" OnNodeInserting="trlStruktura_NodeInserting" OnNodeUpdating="trlStruktura_NodeUpdating"
                        OnNodeValidating="trlStruktura_NodeValidating" OnProcessDragNode="trlStruktura_ProcessDragNode"
                        OnHtmlRowPrepared="trlStruktura_HtmlRowPrepared" OnStartNodeEditing="trlStruktura_StartNodeEditing">
                        <ClientSideEvents EndCallback="function(s, e) { endcallback(s,e); }"
                            NodeDblClick="function(s, e){callWebservice(); }"  />
                        <Styles>
                            <CustomizationWindowContent VerticalAlign="Top">
                            </CustomizationWindowContent>
                        </Styles>
                    </dx:ASPxTreeList>
                    <dx:ASPxTreeListExporter ID="ASPxTreeListExporter1" runat="server" TreeListID="trlStruktura" Settings-PageSettings-Landscape ="True" Settings-AutoWidth="True">
                    </dx:ASPxTreeListExporter>
                    <asp:HiddenField ID="hfRuaj" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hfLloji" runat="server" />
            <asp:HiddenField ID="hfQendra" runat="server" />       
            <asp:HiddenField ID="hfPersoni" runat="server" />
            <asp:HiddenField ID="hfAktiv" runat="server" value="true"/>
          
           

        </div>
        <asp:UpdatePanel ID="update" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
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
    </form>
</body>
</html>
