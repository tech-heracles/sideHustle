<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMenuAndMsgFrame.ascx.cs" Inherits="PlatinumWeb.UcMenuAndMsgFrame" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
<table width="100%">
    <tr>
        <td>
            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                OnItemClick="ASPxMenu1_ItemClick">
                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" /><SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                <ClientSideEvents ItemClick="function(s, e) {menu_click(s,e);}" Init="function(s) {s.SetClientVisible(true);}" />
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
            <div id="dvMenu" style="display: none">
                <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                            BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                <ClientSideEvents Init="function(s,e){$('#dvMenu').show();myMesazh.InicializoTimer();}" />
                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                            <ItemStyle HorizontalAlign="Left" />
                            <SubMenuStyle GutterWidth="17px" />
                        </dx:ASPxMenu>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </td>
    </tr>
</table>
                </ContentTemplate>
                </asp:UpdatePanel>
