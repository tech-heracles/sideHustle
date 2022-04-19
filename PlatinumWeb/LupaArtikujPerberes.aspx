<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaArtikujPerberes.aspx.cs"
    Inherits="PlatinumWeb.LupaArtikujPerberes" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <style type="text/css">
    .AutoCompleteExtender_CompletionList
    {
        background-color: window;
        color: windowtext;
        padding: 1px;
        font-size: small;
        background-color: Gray; /*creates border with
            autocomplete_completionListElement
            background-color*/
    }
    
    /*AutoComplete flyout */
    .AutoCompleteExtender_CompletionListItem
    {
        text-align: left;
        background-color: White;
    }
    
    /* AutoComplete highlighted item */
    .AutoCompleteExtender_HighlightedItem
    {
        background-color: Silver;
        color: blue;
        font-weight: bold;
        font-size: small;
        cursor: pointer;
    }
</style>
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
<%--    <script src="js/myMesazh-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myWebServices-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="js/myButtonClickLupa-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/aspx.js/LupaArtikujPerberes.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script>--%>
      <script src="DX.ashx?jsfileset=~/js/jquery-1.7.2.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myWebServices-IMB.2.1.js;~/js/aspx.js/LupaArtikujPerberes.aspx-IMB.2.1.js"
        type="text/javascript"></script>
</head>


<body onload="Init()">    
 
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="wsfunc.asmx" />
        </Services>
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <ClientSideEvents EndCallback="function(s,e){ window.parent. window.parent.   SessionTimeout.sendKeepAlive();}" />
    </dx:ASPxGlobalEvents>
    <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <dxm:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                <ItemStyle   HorizontalAlign="Left" />
                <SubMenuStyle GutterWidth="17px" />
            </dxm:ASPxMenu>
        </ContentTemplate>
    </asp:UpdatePanel>
    <dxcb:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="ASPxCallback1"
        OnCallback="ASPxCallback1_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) {CallbackComplete(e.result);}" />
    </dxcb:ASPxCallback>
    <div id="divwidth0" style="width: 150px">
    </div>
    <div id="divwidth1" style="width: 150px">
    </div>
    <div id="divwidth2" style="width: 150px">
    </div>
    <div style="width: 100%; border-style: solid; border-color: Gray; border-width: thin;
   visibility: hidden; display: none">     
        <table width="100%">
            <tr>
                <td colspan="3">
                    <dxe:ASPxLabel ID="lblTemplate" runat="server" Text="Zgjidh template-in:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="lblkodTemplateZgjedhur" runat="server" Text="Kodi:" ClientInstanceName="lblkodTemplateZgjedhur">
                    </dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxComboBox ID="kodTemplateZgjedhur_ComboBox" runat="server" ClientInstanceName="kodTemplateZgjedhur_ComboBox"
                            
                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" 
                         >
                        <ClientSideEvents ButtonClick="function(s,e){ButtonClickTemplateEkzistues();}" LostFocus="function(s,e){LostFocusTemplateEkzistues();}" />
                        
                        
                        <LoadingPanelImage  >
                        </LoadingPanelImage>
                        <DropDownButton>
                            <Image>
                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" 
                                    PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                            </Image>
                        </DropDownButton>
                        
                        
                        <ValidationSettings>
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                        </ValidationSettings>
                    </dxe:ASPxComboBox>
                </td>
                <td>
                    <dxe:ASPxLabel ID="lblpershkrimTemplateZgjedhur" runat="server" Text="Pershkrimi:"
                        ClientInstanceName="lblpershkrimTemplateZgjedhur">
                    </dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxTextBox ID="pershkrimTemplateZgjedhur_TextBox" runat="server" ClientInstanceName="pershkrimTemplateZgjedhur_TextBox"
                        Width="170px">
                    </dxe:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div style="width: 100%; border-style: solid; border-color: Gray; border-width: thin;
        visibility: hidden; display: none">
        <table width="100%">
            <tr>
                <td colspan="2">
                    <dxe:ASPxCheckBox ID="checkTemplate" runat="server" ClientInstanceName="checkTemplate"
                        Text="Ruaj si template">
                        <ClientSideEvents CheckedChanged="function(s,e){templateCheckedChanged();}" />
                    </dxe:ASPxCheckBox>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="lblkodTemplate" runat="server" Text="Kodi:" ClientInstanceName="lblkodTemplate">
                    </dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxTextBox ID="kodTemplate_TextBox" runat="server" ClientInstanceName="kodTemplate_TextBox"
                        Width="170px"    >
                        <ValidationSettings>
                            
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                        </ValidationSettings>
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxLabel ID="lblpershkrimiTemplate" runat="server" Text="Pershkrimi:" ClientInstanceName="lblpershkrimiTemplate">
                    </dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxTextBox ID="pershkrimiTemplate_TextBox" runat="server" ClientInstanceName="pershkrimiTemplate_TextBox"
                        Width="170px"    >
                        <ValidationSettings>
                            
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                        </ValidationSettings>
                    </dxe:ASPxTextBox>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <%--  --%>
    <div>
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="pnlgrida" runat="server">
                        <ContentTemplate>
                            <dxwgv:ASPxGridView ID="gvLupaArtPerberes" runat="server" ClientInstanceName="gvLupaArtPerberes"
                                Width="100%" EnableCallBacks="False" OnAfterPerformCallback="gvLupaArtPerberes_AfterPerformCallback"
                                OnHtmlRowCreated="gvLupaArtPerberes_HtmlRowCreated" OnCustomCallback="gvLupaArtPerberes_CustomCallback"
                                OnCustomJSProperties="gvLupaArtPerberes_CustomJSProperties" OnDataBound="gvLupaArtPerberes_DataBound">
                                <Styles    >
                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                    </Header>
                                </Styles>
                                <Images  >
                                    <LoadingPanelOnStatusBar  >
                                    </LoadingPanelOnStatusBar>
                                    <LoadingPanel  >
                                    </LoadingPanel>
                                </Images>
                                <ImagesFilterControl>
                                    <LoadingPanel  >
                                    </LoadingPanel>
                                </ImagesFilterControl>
                                <StylesEditors>
                                    <ProgressBar Height="25px">
                                    </ProgressBar>
                                </StylesEditors>
                            </dxwgv:ASPxGridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table width="60%">
            <tr>
                <td style="width: 70%">
                </td>
                <td align="right">
                    <dxe:ASPxButton ID="ruaj_Button" runat="server" Text="OK" ValidationGroup="entries"
                        AutoPostBack="false"    
                        Width="100%">
                        <ClientSideEvents Click="function(s, e) {RuajClick();}" />
                    </dxe:ASPxButton>
                </td>
                <td align="right">
                    <dxe:ASPxButton ID="anullo_Button" runat="server"  
                        AutoPostBack="false"   Text="Anullo" CausesValidation="False"
                        Width="100%">
                        <ClientSideEvents Click="function(s, e) {AnulloClick();}" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dxpc:ASPxPopupControl ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                    <ClientSideEvents Closing="function(s, e) {
	popupUniversal.SetContentUrl('');
}" />
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="hfArtikujtEkzistues" runat="server" />
    <asp:HiddenField ID="hfTrupiFillimit" runat="server" />
    </form>
</body>
</html>
