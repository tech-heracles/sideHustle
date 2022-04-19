<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaSeriale.aspx.cs" Inherits="PlatinumWeb.LupaSeriale" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>




<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <style type="text/css">
        .style1
        {
            height: 54px;
        }
    </style>
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaSeriale.aspx-IMB.4.1.js&v76"""
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">         
        <asp:ScriptManager ID="ScriptManager1" runat="server">
      </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <ClientSideEvents EndCallback="function(s,e){ }"
                ControlsInitialized="function(s, e) { UpdateButtonState(); }" />
        </dx:ASPxGlobalEvents>
        <div id='div' style="display: none">
            <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" ClientInstanceName="panel"
                Width="100%" HeaderText="Zgjidh Konfigurimin" ShowHeader="False">
                <PanelCollection>
                    <dx:PanelContent>
                        <asp:UpdatePanel ID="pnlKryesor" runat="server">
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
	    menu_click(e); }" Init="function(s) {s.SetClientVisible(true);}" />
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
                                <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server"></dx:ASPxHiddenField>
                                <dx:ASPxHiddenField ID="hfSeriale" runat="server" ClientInstanceName="hfSeriale">
                                </dx:ASPxHiddenField> <dx:ASPxHiddenField ID="hfSasiSeriale" runat="server" ClientInstanceName="hfSasiSeriale">
                                </dx:ASPxHiddenField>
                                <dx:ASPxHiddenField ID="hfSerialeTePerdorura" runat="server" ClientInstanceName="hfSerialeTePerdorura">
                                </dx:ASPxHiddenField>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%-- shtuar--%>
                        <table width="100%">
                            <tr>
                                <td style="width: 40%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lblFushat" runat="server" Text="Serialet e artikullit" Font-Bold="true">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxGridView ID="gvFushat" runat="server" ClientInstanceName="gvFushat"
                                                    Settings-ShowGroupPanel="false" Width="100%" OnCustomCallback="gvFushat_CustomCallback"
                                                    OnDataBound="gvFushat_DataBound"
                                                    OnCustomJSProperties="gvFushat_CustomJSProperties" OnAfterPerformCallback="gvFushat_AfterPerformCallback"
                                                    Settings-ShowVerticalScrollBar="True" Settings-VerticalScrollableHeight="300">
                                                    <ClientSideEvents SelectionChanged="function(s,e){UpdateButtonState();}" RowDblClick="function (s,e){ gvFushat.PerformCallback('djathtas1'); UpdateButtonState();}"
                                                         FocusedRowChanged="function(s,e){UpdateButtonState();}" EndCallback="function(s,e){gvZgjedhur.PerformCallback();}" />
                                                    <Styles>
                                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                        </Header>
                                                    </Styles>
                                                    <StylesEditors>
                                                        <CalendarHeader Spacing="1px">
                                                        </CalendarHeader>
                                                        <ProgressBar Height="25px">
                                                        </ProgressBar>
                                                    </StylesEditors>
                                                </dx:ASPxGridView>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 5%">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnDjathtas1" runat="server" Text="&gt;"
                                                    AutoPostBack="false"
                                                    ClientInstanceName="btnDjathtas1" Width="50px">
                                                    <ClientSideEvents Click="function (s,e){ gvFushat.PerformCallback('djathtas1'); UpdateButtonState();}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnDjathtasGjitha" runat="server" Text="&gt;&gt;"
                                                    AutoPostBack="false"
                                                    ClientInstanceName="btnDjathtasGjitha" Width="50px">
                                                    <ClientSideEvents Click="function (s,e){gvFushat.PerformCallback('djathtasgjithe'); UpdateButtonState();}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnMajtas1" runat="server" Text="&lt;"
                                                    AutoPostBack="false"
                                                    ClientInstanceName="btnMajtas1" Width="50px">
                                                    <ClientSideEvents Click="function (s,e){gvFushat.PerformCallback('majtas1'); UpdateButtonState();}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnMajtaGjitha" runat="server" Text="&lt;&lt;"
                                                    AutoPostBack="false"
                                                    ClientInstanceName="btnMajtaGjitha" Width="50px">
                                                    <ClientSideEvents Click="function (s,e){gvFushat.PerformCallback('majtasgjithe');  UpdateButtonState();}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 40%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lblZgjedhur" runat="server" Text="Serialet e zgjedhura" Font-Bold="true">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxGridView ID="gvZgjedhur" runat="server" ClientInstanceName="gvZgjedhur"
                                                    Settings-ShowGroupPanel="false" Width="100%" OnCustomCallback="gvZgjedhur_CustomCallback"
                                                    OnDataBound="gvZgjedhur_DataBound"
                                                    OnCustomJSProperties="gvZgjedhur_CustomJSProperties" OnAfterPerformCallback="gvZgjedhur_AfterPerformCallback"
                                                    Settings-ShowVerticalScrollBar="True" Settings-VerticalScrollableHeight="300">
                                                    <ClientSideEvents SelectionChanged="function(s,e){UpdateButtonState();}" RowDblClick="function (s,e){gvFushat.PerformCallback('majtas1'); UpdateButtonState();}" FocusedRowChanged="function(s,e){UpdateButtonState();}" EndCallback="function(s,e){}" />
                                                    <Styles>
                                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                        </Header>
                                                    </Styles>
                                                    <StylesEditors>
                                                        <CalendarHeader Spacing="1px">
                                                        </CalendarHeader>
                                                        <ProgressBar Height="25px">
                                                        </ProgressBar>
                                                    </StylesEditors>
                                                </dx:ASPxGridView>

                                            </td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                        </table>

                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel >

            <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                        CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                        EnableAnimation="False" PopupVerticalAlign="WindowCenter" AllowResize="True"
                        AppearAfter="10" ClientIDMode="AutoID" Height="400px">
                        <ClientSideEvents  Closing="function(s, e) {
	popupUniversal.SetContentUrl('');  gvFushat.PerformCallback('pastro'); UpdateButtonState();
}"   />
                        <ContentStyle>
                            <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                                PaddingTop="1px" />
                        </ContentStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl >
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
