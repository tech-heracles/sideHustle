<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoogleHarte.aspx.cs" Inherits="PlatinumWeb.GoogleHarte" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>




<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />    
        <link href="styleOL.css" rel="stylesheet" type="text/css" />
    <link href="stileShtoPike.css" rel="stylesheet" type="text/css" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script src="js/jquery-1.11.3.min.js"></script>
    <script src="/js/aspx.js/GoogleHarte.js" type="text/javascript"></script>
    <script src="//maps.googleapis.com/maps/api/js?key=AIzaSyACrrRhtdTbsjW6rvdFyl-7mtFWeJL60R0" async="" defer="defer" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; left: 0; right: 0; margin-left: 0; margin-right: 0;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){
          if (window.parent.identikuesPerPopupFormula == 'LupaArtShpejt')
              window.parent.window.parent.window.parent.SessionTimeout.sendKeepAlive();
          else
              window.parent.window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
        </dx:ASPxHiddenField>
        <div>
            <div style="display: none;">
                <table>
                    <tr>
                        <td>
                          <%--  <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
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
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1"></RootItemSubMenuOffset>
                                <ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" VerticalAlign="Middle">
                                    <Paddings PaddingBottom="1px" PaddingTop="9px" />
                                    <Paddings PaddingTop="9px" PaddingBottom="1px"></Paddings>
                                </ItemStyle>
                                <SubMenuItemStyle Width="32px">
                                </SubMenuItemStyle>
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify"></SubMenuStyle>
                            </dx:ASPxMenu>--%>
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
                <table style="width: 100%; top: 5px;">
                    <tr style="width: 100%">
                        <td style="width: 40%">
                            <dx:ASPxLabel ID="ASPxLabel1" Width="100%" runat="server" Text="Cakto Koordinatat (x, y): "></dx:ASPxLabel>
                        </td>                       
                        <td style="width: 60%">
                            <dx:ASPxTextBox ID="koordinate" ClientInstanceName="koordinate" runat="server" Width="100%">
                                <%--<ClientSideEvents TextChanged="function (s, e){ ndryshoPikeNeHarte(s, e); }" />--%>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="map" class="mapDivCss" style="height:100%;"></div>
              <asp:UpdatePanel ID="UpdatePanel9" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                   <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popmsg" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                    ClientInstanceName="popmsg" CloseAction="CloseButton" EnableAnimation="False"
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
                                        <dx:ASPxLabel Wrap="False" ID="lblMsgbox" runat="server" ClientInstanceName="lblMsgbox" ClientIDMode="AutoID" Text="Jeni i sigurt?">
                                        </dx:ASPxLabel>
                                        <br />
                                        <br />
                                        
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxPanel >
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>

