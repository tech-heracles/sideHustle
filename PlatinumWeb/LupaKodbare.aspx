<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaKodbare.aspx.cs" Inherits="PlatinumWeb.LupaKodbare" %>

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
    <style type="text/css">
        .style1
        {
            height: 58px;
        }
    </style>
  <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
  <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/aspx.js/LupaKodbare.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
       
    </asp:ScriptManager>
         
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){
        if(window.parent.identifikuesPerPopupKodbare == 'LupaArtShpejt')
            window.parent.window.parent.window.parent.SessionTimeout.sendKeepAlive();
        else
            window.parent.window.parent.SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
    <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel"
        Font-Size="9pt" Modal="True" ImagePosition="Top">
        <loadingdivstyle opacity="30">
        </loadingdivstyle>
    </dx:ASPxLoadingPanel>
    <asp:HiddenField ID="hfKodbare" runat="server" />
    <div>
        <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" Width="100%" Height="100%"
            HeaderText="Shto Kodbare" Font-Bold="True"   ShowHeader="False">
            
            <Border BorderColor="#D7D7D7" BorderStyle="Solid" BorderWidth="1px" />
           
           
            <BorderBottom BorderWidth="0px" />
            <ContentPaddings PaddingBottom="3px" PaddingLeft="3px" PaddingRight="3px" PaddingTop="3px" />
            <PanelCollection>
                <dx:PanelContent>
                    <%-- shtuar--%>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                   >
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
                    <%-- shtuar--%>
                    <asp:UpdatePanel ID="pnlKryesor" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dx:ASPxGridView ID="gvLupaKodbar" runat="server" ClientInstanceName="gvLupaKodbar"
                                            OnAfterPerformCallback="gvLupaKodbar_AfterPerformCallback" Width="100%" OnHtmlRowCreated="gvLupaKodbar_HtmlRowCreated"
                                            OnCustomCallback="gvLupaKodbar_CustomCallback" OnCustomJSProperties="gvLupaKodbar_CustomJSProperties"
                                            OnDataBound="gvLupaKodbar_DataBound">
                                            <StylesPager Summary-Width="100%" PageNumber-Width="100%">
                                                <PageNumber Width="100%">
                                                </PageNumber>
                                                <Summary Width="100%">
                                                </Summary>
                                            </StylesPager>
                                            <ClientSideEvents RowDblClick="function(s, e) {
                                                           

}" BeginCallback="function(s, e) {
	merrTeDhenaNew();
}" EndCallback="function(s, e) {
		merrTeDhenaNew();
        eval('txtPershkrimi' +parseFloat(parseFloat( indeksi)+parseFloat(1))).SetFocus(true);
}" />
                                            <Styles>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                            <StylesEditors>
                                                <ProgressBar Height="100%">
                                                </ProgressBar>
                                            </StylesEditors>
                                            <SettingsPager></SettingsPager>
                                        </dx:ASPxGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="style1">
                                        <%--  <dx:ASPxButton ID="btnOk" runat="server" Text="Ok" AutoPostBack="False"  
                                     >
                                    <ClientSideEvents Click="function(s, e) {
                        
    window.parent.btneKodbari.SetFocus(true);                                     
 window.parent.popupUniversal.Hide();
}" />
                                </dx:ASPxButton>--%>
                                      <%--  <dx:ASPxTextBox ID="txtPersh" runat="server" ClientInstanceName="txtPersh" Width="0"
                                            Height="0" ForeColor="White">
                                            <Border BorderColor="White" />
                                        </dx:ASPxTextBox>--%>
                                        <asp:HiddenField ID="hfPershkrimi" runat="server" />
                                    </td>
                                </tr>
                            </table>
                       
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
			<ContentTemplate>
				<dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
					CloseAction="CloseButton" ShowCollapseButton="true" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
					EnableAnimation="False" PopupVerticalAlign="WindowCenter" AllowResize="True"
					AppearAfter="10" ClientIDMode="AutoID" Height="400px">
					<ClientSideEvents Closing="function(s, e) {
	popupUniversal.SetContentUrl('');  
}" />
					<ContentStyle>
						<Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
							PaddingTop="1px" />
					</ContentStyle>
					<ContentCollection>
						<dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
						</dx:PopupControlContentControl>
					</ContentCollection>
                </dx:ASPxPopupControl>
			
			</ContentTemplate>
		</asp:UpdatePanel>
                </dx:PanelContent>
            </PanelCollection>
            <BackgroundImage Repeat="RepeatX" />
            
        </dx:ASPxRoundPanel >
    </div>
    </form>
</body>
</html>
