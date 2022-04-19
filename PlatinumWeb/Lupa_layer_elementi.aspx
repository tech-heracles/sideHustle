<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lupa_layer_elementi.aspx.cs"
    Inherits="PlatinumWeb.LupaDegaAdministrative" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

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
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <%--    <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
     <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/aspx.js/LupaDegaAdministrative.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script>--%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Lupa_layer_elementi-IMB.6.4.js""
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
      
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){
         window.parent. window.parent.   SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
    <div id='div' style="display: none">
        <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" ClientInstanceName="panel" runat="server"
            Width="100%" Height="100%" HeaderText="Zgjidh Layer/Elementi" Font-Bold="True"
             ShowHeader="False">
            
            <Border BorderColor="#D7D7D7" BorderStyle="Solid" BorderWidth="1px" />
            
            
            <BorderBottom BorderWidth="0px" />
            <ContentPaddings PaddingBottom="3px" PaddingLeft="3px" PaddingRight="3px" PaddingTop="3px" />
            <PanelCollection>
                <dx:PanelContent>
                    <%-- shtuar--%>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                    >
                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                    <ClientSideEvents ItemClick="function(s, e) {
	                        menu_click(s,e);
                            }" />
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
                    <%-- shtuar--%>
                    <asp:UpdatePanel ID="pnlKryesor" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dx:ASPxGridView ID="gvLayerElementi" runat="server" ClientInstanceName="gvLayerElementi"
                                            OnDataBound="gvLayerElementi_DataBound" OnAfterPerformCallback="gvLayerElementi_AfterPerformCallback"
                                            OnCustomJSProperties="gvLayerElementi_CustomJSProperties" Width="100%"
                                            OnCustomCallback="gvLayerElementi_CustomCallback">
                                            <StylesPager Summary-Width="100%" PageNumber-Width="100%">
                                                <PageNumber Width="100%">
                                                </PageNumber>
                                                <Summary Width="100%">
                                                </Summary>
                                            </StylesPager>
                                            <ClientSideEvents RowDblClick="function(s, e) { OnGridSelectionChanged(); }" BeginCallback="function(s, e) { BeginCallback(s,e); }" />
                                            <Styles>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                            <StylesEditors>
                                                <ProgressBar Height="100%">
                                                </ProgressBar>
                                            </StylesEditors>
                                        </dx:ASPxGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <%-- <td align="center" class="style1">
                                <dx:ASPxButton ID="btnOk" runat="server" Text="Ok" AutoPostBack="False"  
                                     >
                                    <ClientSideEvents Click="function(s, e) {
                         OnGridSelectionChanged();                                      
                       
                        }" />
                                </dx:ASPxButton>
                            </td>--%>
                                </tr>
                            </table>
                            <%--shtuar--%>
                            <%--                            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popZgjidhFiltrin" runat="server" AllowDragging="True"
                                ClientInstanceName="popZgjidhFiltrin" CloseAction="CloseButton" EnableAnimation="False"
                                EnableViewState="False" HeaderText="Zgjidh  Filtrin" Modal="True" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" Width="300px" ClientIDMode="AutoID"  
                                CssPostfix="Glass"  >
                                <HeaderStyle>
                                    <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                                </HeaderStyle>
                                <ContentCollection>
                                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                        <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ZgjidhFiltrin_ASPxRoundPanel" runat="server" Width="410px"
                                              ShowHeader="false" CssPostfix="Glass"
                                            GroupBoxCaptionOffsetY="-24px"  >
                                            <ContentPaddings PaddingLeft="4px" PaddingTop="10px" PaddingBottom="10px" />
                                            <PanelCollection>
                                                <dxp:PanelContent ID="PanelContent1" runat="server">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="Filtri_ASPxLabel" runat="server" Text="Filtri:" Style="font-weight: 700;
                                                                    color: #666666">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td style="width: 50%">
                                                                <dx:ASPxTextBox ID="Filtri_ASPxTextBox" runat="server" ClientInstanceName="Filtri_ASPxTextBox">
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                    <ContentTemplate>
                                                                        <dx:ASPxButton ID="LupaFiltri_ASPxButton" runat="server"  
                                                                            CssPostfix="Glass" CausesValidation="False"  >
                                                                            <Image Url="images/Lupe.ico">
                                                                            </Image>
                                                                            <ClientSideEvents Click="function(s, e) {                                                   
                                                       Filtra_Click(); 
                                                        }" />
                                                                        </dx:ASPxButton>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="Apliko_ASPxButton"  
                                                                    CssPostfix="Glass" runat="server" Text="Apliko" OnClick="Apliko_ASPxButton_Click"
                                                                    CausesValidation="False"  
                                                                    Style="font-weight: 700">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
                                                        }" Click="function(s, e) {
	                                                        popZgjidhFiltrin.Hide();
                                                        }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxRoundPanel >
                                    </dx:PopupControlContentControl>
                                </ContentCollection>
                            </dx:ASPxPopupControl >--%>
                            <%--                            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popRuaj" runat="server" AllowDragging="True" ClientInstanceName="popRuaj"
                                CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Ruaj Filtrin"
                                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                Width="300px" ClientIDMode="AutoID"  
                                CssPostfix="Glass"  >
                                <HeaderStyle>
                                    <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                                </HeaderStyle>
                                <ContentCollection>
                                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                        <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="RuajFiltrin_ASPxRoundPanel" runat="server" Width="300px"
                                            EnableTheming="False"   CssPostfix="Glass"
                                            GroupBoxCaptionOffsetY="-24px" ShowHeader="false"  >
                                            <ContentPaddings PaddingLeft="4px" PaddingTop="10px" PaddingBottom="10px" />
                                            <HeaderStyle Height="23px">
                                                <Paddings PaddingBottom="0px" PaddingLeft="2px" PaddingTop="0px" />
                                            </HeaderStyle>
                                            <PanelCollection>
                                                <dxp:PanelContent ID="PanelContent2" runat="server">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="Kodi_ASPxLabel" runat="server" Text="Kodi:" CssClass="style1">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxTextBox ID="Kodi_ASPxTextBox" runat="server" Width="120px" ClientInstanceName="Kodi_ASPxTextBox">
                                                                    <ValidationSettings CausesValidation="True">
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                                <asp:RequiredFieldValidator ID="Kodi_RequiredFieldValidator" runat="server" ErrorMessage="Jepni Kodin!"
                                                                    ControlToValidate="Kodi_ASPxTextBox" Display="None">
                                *</asp:RequiredFieldValidator>
                                                                <cc1:ValidatorCalloutExtender ID="Kodi_ValidatorCalloutExtender" Enabled="True" TargetControlID="Kodi_RequiredFieldValidator"
                                                                    HighlightCssClass="validorCalloutHighlight" runat="server">
                                                                </cc1:ValidatorCalloutExtender>
                                                                <asp:CustomValidator ID="Kodi_CustomValidator" runat="server" ErrorMessage="Ky kod ekziston"
                                                                    ControlToValidate="Kodi_ASPxTextBox" Display="None" OnServerValidate="Kodi_CustomValidator_ServerValidate">*</asp:CustomValidator>
                                                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" Enabled="True" TargetControlID="Kodi_CustomValidator"
                                                                    HighlightCssClass="validorCalloutHighlight" runat="server">
                                                                </cc1:ValidatorCalloutExtender>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxLabel ID="Shenime_ASPxLabel" runat="server" Text="Shenime:" CssClass="style1">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxTextBox ID="Shenime_ASPxTextBox" runat="server" Width="170px">
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="Ruaj_ASPxButton"  
                                                                    CssPostfix="Glass" runat="server" Text="Ruaj" OnClick="Ruaj_ASPxButton_Click"
                                                                     >
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxRoundPanel >
                                    </dx:PopupControlContentControl>
                                </ContentCollection>
                            </dx:ASPxPopupControl >--%>
                            <%--                            <asp:UpdatePanel ID="pnlfiltra" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                <ContentTemplate>
                                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="ASPxPopupControl1" runat="server" AllowDragging="True"
                                        ClientInstanceName="popFiltra" CloseAction="CloseButton" EnableAnimation="False"
                                        EnableViewState="False" HeaderText="Zgjidh Filtrin" Modal="True" PopupHorizontalAlign="WindowCenter"
                                        PopupVerticalAlign="WindowCenter" ClientIDMode="AutoID"  
                                        CssPostfix="Glass"  >
                                        <HeaderStyle>
                                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                                        </HeaderStyle>
                                        <ContentCollection>
                                            <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
                                                <iframe id="Container" name="Container" frameborder="0" runat="server"></iframe>
                                            </dx:PopupControlContentControl>
                                        </ContentCollection>
                                    </dx:ASPxPopupControl >
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            --%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--  </ContentTemplate>
                     </asp:UpdatePanel>--%>
                    <%--shtuar--%>
                </dx:PanelContent>
            </PanelCollection>
            
            <BackgroundImage Repeat="RepeatX" />
            
        </dx:ASPxRoundPanel >
    </div>
    </form>
</body>
</html>
