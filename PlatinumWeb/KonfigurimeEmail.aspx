<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KonfigurimeEmail.aspx.cs"
    Inherits="PlatinumWeb.KonfigurimeEmail" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" rel="Stylesheet" type="text/css" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/KonfigurimeEmail.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
         
    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
        Font-Size="9pt" Modal="True" ImagePosition="Top">
        <LoadingDivStyle Opacity="30">
        </LoadingDivStyle>
    </dx:ASPxLoadingPanel>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
       
    </asp:ScriptManager>
    
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
                            <ClientSideEvents Init="function(s) {s.SetClientVisible(true);}" />
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
    <asp:UpdatePanel ID="pnlKonfigurimi" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <br />
            <table class="renditKontrolleDy">
                <tr>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" ID="lblOutgoingSmtp" runat="server" AssociatedControlID="txtOutgoingSmtp"
                            ClientInstanceName="lblOutgoingSmtp" Text="Outgoing Mail Server (SMTP):">
                        </dx:ASPxLabel>
                    </td>
                    <td class="CustomRenditKontrolleCellMeWidth50">
                        <dx:ASPxTextBox ID="txtOutgoingSmtp" runat="server" ClientInstanceName="txtOutgoingSmtp"
                            Width="100%">
                            <ValidationSettings CausesValidation="true" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                SetFocusOnError="true" ValidationGroup="entries">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" ID="lblDergoEmailNga" runat="server" AssociatedControlID="txtDergoEmailNga"
                            ClientInstanceName="lblDergoEmailNga" Text="Dërgo email nga:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="CustomRenditKontrolleCellMeWidth50">
                        <dx:ASPxTextBox ID="txtDergoEmailNga" runat="server" ClientInstanceName="txtDergoEmailNga"
                            Width="100%">
                            <ValidationSettings CausesValidation="true" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                SetFocusOnError="true" ValidationGroup="entries">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ErrorText="Format i gabuar email-it!" />
                                <RequiredField IsRequired="false" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" ID="lblPassword" AssociatedControlID="txtPassword" runat="server"
                            ClientInstanceName="lblPassword" Text="Fjalekalimi:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="CustomRenditKontrolleCellMeWidth50">
                        <dx:ASPxTextBox ID="txtPassword" runat="server" ClientInstanceName="txtPassword"
                            Width="100%" Password="true">
                            <ValidationSettings CausesValidation="true" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                SetFocusOnError="true" ValidationGroup="entries" ValidateOnLeave="false">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RegularExpression ErrorText="Password-i duhet te kete te pakten 6 shkronja/shifra"
                                    ValidationExpression=".......*" />
                                <RequiredField IsRequired="false" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" ID="lblVerifikoPassword" AssociatedControlID="txtVerifikoPassword"
                            runat="server" ClientInstanceName="lblVerifikoPassword" Text="Verifiko fjalekalimin:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="CustomRenditKontrolleCellMeWidth50">
                        <dx:ASPxTextBox ID="txtVerifikoPassword" runat="server" ClientInstanceName="txtVerifikoPassword"
                            Width="100%" Password="true">
                            <ValidationSettings CausesValidation="true" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                SetFocusOnError="true" ValidationGroup="entries">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" ID="lblPortaSmtp" runat="server" AssociatedControlID="txtPortaSmtp"
                            ClientInstanceName="lblPortaSmtp" Text="Porta SMTP:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="CustomRenditKontrolleCellMeWidth50">
                        <dx:ASPxTextBox ID="txtPortaSmtp" runat="server" ClientInstanceName="txtPortaSmtp"
                            Width="100%">
                            <ValidationSettings CausesValidation="true" SetFocusOnError="true" ErrorDisplayMode="ImageWithTooltip"
                                Display="Dynamic" ValidationGroup="entries">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" ID="lblEnableSsl" runat="server" AssociatedControlID="cbEnableSsl"
                            ClientInstanceName="lblEnableSsl" Text="Enable SSL:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxCheckBox ID="cbEnableSsl" runat="server" ClientInstanceName="cbEnableSsl"
                            TextSpacing="2px" Width="100%">
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxCheckBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
