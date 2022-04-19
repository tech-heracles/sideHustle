<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NdryshimFjalekalimi.aspx.cs"
    Inherits="PlatinumWeb.NdryshimFjalekalimi" %>

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



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="Stylesheet1.css" rel="Stylesheet" type="text/css" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <style type="text/css">
        body * {
            margin: 0;
            padding: 0;
            outline: 0;
            border: 0;
        }

        body {
            margin: 8px !important;
        }

            body.kesh {
                overflow: auto;
                margin: 0 !important;
            }

        .main {
            text-align: center;
            position: relative;
            padding: 0;
            margin: 0;
        }

        .kesh .main {
            text-align: center;
            padding: 0;
            margin: 0;
            position: inherit;
        }

        .center {
            display: inline-block;
            /*padding-top:20px;*/
        }

        .content {
            width: 700px;
            padding-top: 100px;
        }

        .kesh .content {
            width: 700px;
            padding-top: 100px;
            padding-left: 32%;
            display: block;
        }

        .grid {
            /*vendos tekstin brenda grides majtas*/
            text-align: left;
        }

        .right {
            width: auto;
            height: 16px;
            background-color: #0072c6;
            border-radius: 4px;
            padding: 9px 11px 9px 11px;
            float: right;
            display: inline-flex;
        }

            .right:hover {
                background-color: #208bda;
            }

        .kesh .right {
            width: 100%;
            height: 40px;
            background-color: #1478B4;
            display: inline-flex;
            border-radius: 0px;
            padding: 0;
            float: none;
        }

            .kesh .right:hover {
                background-color: none;
            }

        .klient .right {
            width: auto;
            height: 16px;
            background-color: #5D9AD3;
            border-radius: 4px;
            padding: 9px 11px 9px 11px;
            float: right;
            display: inline-flex;
        }

        .vodafone .right {
            width: auto;
            height: 16px;
            background-color: #ed1b24;
            border-radius: 4px;
            padding: 9px 11px 9px 11px;
            float: right;
            display: inline-flex;
        }

        .klient .right:hover {
            background-color: #75afe5;
        }

        .vodafone .right:hover {
            background-color: #f13941;
        }

        .emri {
            padding: 0;
            margin: 0;
            line-height: 16px;
        }

        .kesh .emri2 {
            padding: 0;
            margin: 0;
            line-height: 16px;
            float: right;
            border: 1px solid white;
            height: 20px;
            border-radius: 5px;
        }

        .kesh .emriKesh {
            border: 1px solid white;
            height: 30px;
            border-radius: 5px;
            margin-top: 5px;
            display: inline-flex;
            background-color: #3375a8;
            margin-right: 10px;
        }

        .dalje {
            margin-left: 3px;
        }

        .zgjidh {
            margin-top: 20px;
        }

        .kesh .footer {
            position: absolute;
            right: 0;
            bottom: 0;
            left: 0;
            padding: 0.6rem;
            background-color: #1478B4;
            text-align: center;
        }

        @media all and (max-width:700px) {
            .content {
                padding-top: 20px;
            }
        }
    </style>
    <link href="FaqeKryesore.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/aspx.js/NdryshimFjalekalimi.aspx-IMB.2.1.js&v76" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <div id="linkuDalje" visible="false" runat="server">
            <section class="main">
                <header class="right header">
                    <div class="emri">
                        <dx:ASPxLabel ID="lblUserEmri" ClientInstanceName="lblUserEmri" runat="server" Text=""
                            Font-Size="11" ForeColor="white" Font-Names="Calibri">
                        </dx:ASPxLabel>
                    </div>
                    <div class="dalje">
                        <dx:ASPxHyperLink CssClass="dalje" ID="hyperLinkDalje" runat="server" EnableViewState="false" ViewStateMode="Disabled" ImageUrl="images/dalje.png" ForeColor="Black" Font-Underline="false" Text="Dalje" Font-Size="11"
                            Font-Names="Calibri">
                        </dx:ASPxHyperLink>
                    </div>
                </header>
            </section>
        </div>
          
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfPassPerkohshem" runat="server" ClientInstanceName="hfPassPerkohshem">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfResetimPass" runat="server" ClientInstanceName="hfResetimPass">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfGjatesiMinPassword" runat="server" ClientInstanceName="hfGjatesiMinPassword">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfSkaduarPassIPerdoruesit" runat="server" ClientInstanceName="hfSkaduarPass"></dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState"></dx:ASPxHiddenField>
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
                                <ClientSideEvents ItemClick="function(s, e) {
	                      
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
        <asp:UpdatePanel ID="pnlPaswordi" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <br />
                <table>
                    <tr>
                        <td>
                            <div id="dvlblUsername">
                                <dx:ASPxLabel ID="lblUsername" runat="server" ClientInstanceName="lblUsername" Text="Perdoruesi:">
                                </dx:ASPxLabel>
                            </div>
                        </td>
                        <td>
                            <div id="dvusername_TextBox" style="width: 167px">
                                <dx:ASPxTextBox ID="btnPerdorues" HorizontalAlign="Center" runat="server" ClientEnabled="True" ClientInstanceName="btnPerdorues"
                                    Cursor="default" Font-Bold="True" Font-Italic="False" Font-Names="Times New Roman"
                                    Font-Size="12pt" VerticalAlign="Top" Width="170px" Enabled="False">
                                    <DisabledStyle BackColor="#EEEEEE" BackgroundImage-HorizontalPosition="center">
                                        <BackgroundImage HorizontalPosition="center" />
                                    </DisabledStyle>
                                </dx:ASPxTextBox>

                                <dx:ASPxButton ID="btnRuaj" runat="server" ClientVisible="false" OnClick="btnRuaj_Click" ClientEnabled="True" ClientInstanceName="btnRuaj"
                                    Cursor="default" Font-Bold="True" Font-Italic="False" Font-Names="Times New Roman"
                                    Font-Size="12pt" Height="15px" VerticalAlign="Top" Width="170px">
                                </dx:ASPxButton>
                            </div>
                        </td>
                        <tr style="height: 30px; padding-left: 10px;">
                            <td>
                                <div id="dvlblPasswordieksistues">
                                    <dx:ASPxLabel ID="lblPasswordieksistues" runat="server" ClientInstanceName="lblPasswordieksistues"
                                        Text="Fjalekalimi ekzistues:">
                                    </dx:ASPxLabel>
                                </div>
                            </td>
                            <td>
                                <div id="dvtxtPasswordieksistues">
                                    <dx:ASPxTextBox ID="txtPasswordieksistues" runat="server" ClientInstanceName="txtPasswordieksistues"
                                        Password="True" Width="170px">
                                        <ValidationSettings CausesValidation="true" SetFocusOnError="true" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <%--  <RegularExpression ErrorText="Password-i duhet te kete te pakten 6 shkronja/shifra"
                                            ValidationExpression=".......*" />--%>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </div>
                            </td>
                        </tr>
                        <tr style="height: 30px; padding-left: 10px;">
                            <td>
                                <div id="dvlblPassword">
                                    <dx:ASPxLabel ID="lblPassword" runat="server" ClientInstanceName="lblPassword" Text="Fjalekalimi:">
                                    </dx:ASPxLabel>
                                </div>
                            </td>
                            <td>
                                <div id="dvpassword_TextBox">
                                    <dx:ASPxTextBox ID="password_TextBox" runat="server" ClientInstanceName="password_TextBox"
                                        Password="True" Width="170px">
                                        <ClientSideEvents TextChanged="function(s, e) { kontrolloPassword(s, e); }" />
                                        <ValidationSettings CausesValidation="True" SetFocusOnError="True" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <%--<RegularExpression ErrorText="Password-i duhet te kete te pakten 6 shkronja/shifra"
                                            ValidationExpression=".......*" />--%>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </div>
                            </td>
                        </tr>
                        <tr style="height: 30px; padding-left: 15px;">
                            <td>
                                <div id="dvlblKonfirmoPassword">
                                    <dx:ASPxLabel ID="lblKonfirmoPassword" runat="server" ClientInstanceName="lblKonfirmoPassword"
                                        Text="Konfirmo fjalekalimin:">
                                    </dx:ASPxLabel>
                                </div>
                            </td>
                            <td>
                                <div id="dvkonfirmo_Textbox">
                                    <dx:ASPxTextBox ID="konfirmo_Textbox" runat="server" ClientInstanceName="konfirmo_Textbox"
                                        Password="True" Width="170px">
                                        <ValidationSettings CausesValidation="True" SetFocusOnError="True" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </div>
                            </td>
                        </tr>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popNdryshoPass" runat="server" AllowDragging="True" ClientIDMode="AutoID"
            ClientInstanceName="popNdryshoPass" CloseAction="CloseButton" CssPostfix="Glass" EnableAnimation="False"
            EnableViewState="False" Font-Bold="true" HeaderText="Skadim Fjalëkalimi" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" Width="300px">
            <HeaderStyle>
                <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
            </HeaderStyle>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                    <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxLabel ID="lblSkaduarPass" runat="server" ClientIDMode="AutoID" Text="Fjalëkalimi juaj ka skaduar , ju duhet t'a ndryshoni atë!">
                                </dx:ASPxLabel>
                                <br />
                                <br />
                                <div style="text-align: right;">
                                    <table>
                                        <tr>
                                            <td style="width: 57%"></td>
                                            <td style="align-content: center">
                                                <dx:ASPxButton ID="ButtonOk" Width="70px" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                    Text="Ok" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {popNdryshoPass.Hide();}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="visibility: hidden">
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </form>
</body>
</html>
