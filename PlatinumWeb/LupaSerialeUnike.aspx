<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaSerialeUnike.aspx.cs" Inherits="PlatinumWeb.LupaSerialeUnike" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <style type="text/css">
        .style1 {
            height: 58px;
        }
    </style>
    
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet"/>
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server"
          id="themeJQuery"/>
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" media="screen" rel="stylesheet" type="text/css"/>
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/aspx.js/LupaSerialeUnike.aspx-IMB.6.8.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
        <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:HiddenField ID="hfKodbare" runat="server" />
        <div>
            <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" Width="100%" Height="100%"
                HeaderText="Shto Kodbare" Font-Bold="True" ShowHeader="False">

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
                                        ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True">
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
                                <div id="dvFillim" class="atributeDiveFshehur">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxCheckBox ID="cbAutomatik" runat="server" CheckState="Unchecked" ClientInstanceName="cbAutomatik"
                                                    TextSpacing="2px">
                                                    <ClientSideEvents CheckedChanged="function (s,e){Checked()}" />
                                                </dx:ASPxCheckBox>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblAutomatik" runat="server" ClientInstanceName="lblAutomatik" Text="Seriale Automatike"
                                                    Width="150px" Wrap="True">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblSasia" runat="server" AssociatedControlID="txtSasia" ClientInstanceName="lblSasia" Text="Sasia automatike" Wrap="false">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="txtSasia" runat="server" ClientEnabled="False" ClientInstanceName="txtSasia" Text="1">
                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RegularExpression ErrorText="Sasia duhet te jete numer!" ValidationExpression="[0-9.,]*" />

                                                    </ValidationSettings>
                                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnGjenero" runat="server" Text="Gjenero"
                                                    ClientInstanceName="btnGjenero" AutoPostBack="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) { Gjenero(); 
}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblNrSerialesh" runat="server" ClientInstanceName="lblNrSerialesh" Text="Nr Serialesh:" Wrap="false">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div id="divgride1">
                                    <div id="divgride2">
                                        <table id="rowed5"> </table>
                                        <div id="pager"></div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                            <ContentTemplate>
                                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                                    CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
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

            </dx:ASPxRoundPanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfSerialet" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>

        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"></dx:ASPxHiddenField>
    </form>
</body>
</html>
