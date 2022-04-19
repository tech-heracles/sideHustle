<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NdryshimCmimi.aspx.cs" Inherits="PlatinumWeb.NdryshimCmimi" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="Stylesheet1.css" rel="Stylesheet" type="text/css" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/json2.js;~/JsGlobal.js;~/js/myJQGrid-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/aspx.js/NdryshimCmimi.aspx-IMB.4.0.30.js&v76"
      <meta name="viewport" content="width=device-width,initial-scale=1.0" />
        type="text/javascript"></script>
</head>
<body onload="changeName()">
    <form id="form1" runat="server" style="width: 100%">
          
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server"  AsyncPostBackTimeout="360000">
            <%--<Services>
                <asp:ServiceReference Path="wsfunc.asmx" />
            </Services>--%>
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
           <%-- <ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfPassPerkohshem" runat="server" ClientInstanceName="hfPassPerkohshem">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfGjatesiMinPassword" runat="server" ClientInstanceName="hfGjatesiMinPassword">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfSkaduarPassIPerdoruesit" runat="server" ClientInstanceName="hfSkaduarPass"></dx:ASPxHiddenField>
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
                                <ClientSideEvents ItemClick="function(s, e) { menu_click(s,e);
	                      
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="pnlNC" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <br />

                <table>
                    <tr>
                        <td>
                            <div id="dvlblData">
                                <dx:ASPxLabel ID="lblData" runat="server" ClientInstanceName="lblData" Text="Date dokumenti:">
                                </dx:ASPxLabel>
                            </div>
                        </td>
                        <td>
                            <div id="dvdtdata">

                                <dx:ASPxDateEdit ID="dteData" Width="100%" runat="server" ClientInstanceName="dteData"
                                    ShowShadow="False">

                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries">
                                        <RequiredField IsRequired="True" />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>

                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <CalendarProperties>
                                        <HeaderStyle Spacing="1px" />
                                        <FooterStyle Spacing="17px" />
                                    </CalendarProperties>
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxDateEdit>

                            </div>
                        </td>
                        <tr>
                     <td>
                            <br /></td>
                        </tr>
                        <tr>
                            <td>
                                <div id="dvlblartikulli">
                                    <dx:ASPxLabel ID="lblArtikulli" runat="server" ClientInstanceName="lblArtikulli"
                                        Text="Artikulli">
                                    </dx:ASPxLabel>
                                </div>
                            </td>
                            <td>
                                <div id="dvcmbArtikulli">
                                    <dx:ASPxComboBox ID="cmbArtikulli" Width="100%" runat="server" ClientInstanceName="cmbArtikulli" OnItemRequestedByValue="btneArtikulli_ItemRequestedByValue"
                               OnItemsRequestedByFilterCondition="cmbArtikulli_ItemsRequestedByFilterCondition"          ShowShadow="False" ValueType="System.Int64" EnableClientSideAPI="True" IncrementalFilteringMode="Contains"
                                        EnableCallbackMode="True" CallbackPageSize="10"
                                      >
                                        <ClientSideEvents ButtonClick="function(s,e){ButtonClickArtikulli();}" />
                                        <ValidationSettings ErrorText="Ploteso Artikullin" ErrorDisplayMode="ImageWithTooltip"
                                            Display="Dynamic" ValidateOnLeave="false" ValidationGroup="entries">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </div>
                            </td>
                        </tr>  <tr>
                     <td>
                            <br /></td>
                        </tr>
                        <tr>
                            <td>
                                <div id="dvlblcmimi">
                                  <dx:ASPxLabel ID="lblCmimi" runat="server" ClientInstanceName="lblCmimi" Text="Cmimi i ri Dealer pa tvsh:">
                                    </dx:ASPxLabel>
                                    </dx:ASPxLabel>
                                </div>
                            </td>
                            <td>
                                <div id="dvtxtcmimi">
                                    <dx:ASPxTextBox ID="txtCmimi" runat="server" ClientInstanceName="txtCmimi"
                                        Width="100%">
                                        <ValidationSettings ValidationGroup="entries" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip"
                                            RegularExpression-ValidationExpression="[0-9,.]*" ErrorText="Cmimi duhet te jete nr">

                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" ForeColor="Black">
                                        </DisabledStyle>

                                    </dx:ASPxTextBox>
                                </div>
                            </td>
                        </tr>


                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxPopupControl ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
            CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
            <ClientSideEvents Closing="function(s, e) { popupUniversal.SetContentUrl('');}" />
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </form>
</body>
</html>

