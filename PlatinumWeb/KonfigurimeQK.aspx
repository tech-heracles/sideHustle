<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KonfigurimeQK.aspx.cs"
    Inherits="PlatinumWeb.KonfigurimeQK" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
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
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/aspx.js/KonfigurimeQK.aspx-IMB.3.1.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
       
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.    SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState" ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
         
    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
        Font-Size="9pt" Modal="True" ImagePosition="Top">
        <LoadingDivStyle Opacity="30">
        </LoadingDivStyle>
    </dx:ASPxLoadingPanel>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" AutoPostBack="true" ClientInstanceName="ASPxMenu1"
                                ItemImagePosition="Top" OnDataBound="ASPxMenu1_DataBound" OnItemClick="ASPxMenu1_ItemClick"
                                SeparatorWidth="1px" ShowPopOutImages="True" Width="100%">
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
                                    <dx:ASPxMenu ID="MenuInfo" runat="server" BorderBetweenItemAndSubMenu="HideRootOnly"
                                        ClientIDMode="AutoID" ClientInstanceName="MenuInfo" ShowPopOutImages="True" Width="100%">
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
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="pnlTree" runat="server">
            <ContentTemplate>
                <table width="60%">
                    <tr>
                        <td colspan="2">
                            <dx:ASPxLabel ID="lblMenyra" runat="server" Text="Mënyra e shpërndarjes në Qendra Kostoje"
                                Font-Bold="true">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbPrioriteti1" ID="lblPrioriteti1"
                                runat="server" Text="Prioriteti 1:" ClientInstanceName="lblPrioriteti1">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 40%">
                            <dx:ASPxComboBox ID="cmbPrioriteti1" runat="server" ClientInstanceName="cmbPrioriteti1"
                                ShowShadow="False" Width="100%" ReadOnly="false" SettingsLoadingPanel-ImagePosition="Top">
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbPrioriteti2" ID="lblPrioriteti2"
                                runat="server" Text="Prioriteti 2:" ClientInstanceName="lblPrioriteti2">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 40%">
                            <dx:ASPxComboBox ID="cmbPrioriteti2" runat="server" ClientInstanceName="cmbPrioriteti2"
                                ShowShadow="False" Width="100%" ReadOnly="false" SettingsLoadingPanel-ImagePosition="Top">
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbPrioriteti3" ID="lblPrioriteti3"
                                runat="server" Text="Prioriteti 3:" ClientInstanceName="lblPrioriteti3">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="cmbPrioriteti3" runat="server" ClientInstanceName="cmbPrioriteti3"
                                ShowShadow="False" Width="100%" ReadOnly="false" SettingsLoadingPanel-ImagePosition="Top">
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbPrioriteti4" ID="lblPrioriteti4"
                                runat="server" Text="Prioriteti 4:" ClientInstanceName="lblPrioriteti4">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="cmbPrioriteti4" runat="server" ClientInstanceName="cmbPrioriteti4"
                                ShowShadow="False" Width="100%" ReadOnly="false" SettingsLoadingPanel-ImagePosition="Top">
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                   <tr>
                        <td>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbPrioriteti5" ID="lblPrioriteti5"
                                runat="server" Text="Prioriteti 5:" ClientInstanceName="lblPrioriteti5">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="cmbPrioriteti5" runat="server" ClientInstanceName="cmbPrioriteti5"
                                ShowShadow="False" Width="100%" ReadOnly="false" SettingsLoadingPanel-ImagePosition="Top">
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                        </td>
                   </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <dx:ASPxLabel ID="lblMesazhi" runat="server" Text="Zgjidhni opsionin për shfaqjen e mesazhit te shpërndarjes në qendra kostoje në regjistrime"
                                Font-Bold="true">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <dx:ASPxRadioButtonList ID="rbMesazhi" ClientInstanceName="rbMesazhi" Font-Size="14px"
                                runat="server" RepeatColumns="1" Height="16px" EnableClientSideAPI="true" Border-BorderStyle="None">
                                <ClientSideEvents ValueChanged="function(s,e){changeRadio(s,e)}" Init="function(s,e){changeRadio(s,e)}" />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                    ValidateOnLeave="false">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxRadioButtonList>
                        </td>
                        <td>
                            <dx:ASPxMemo ID="lblMetodePershkrimi" Rows="7" Width="100%" runat="server" Text="" ClientEnabled="false"
                                ClientInstanceName="lblMetodePershkrimi">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
