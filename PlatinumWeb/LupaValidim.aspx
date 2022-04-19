<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaValidim.aspx.cs" Inherits="PlatinumWeb.LupaValidim" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaValidim.aspx-IMB.4.0.js&v25"
        type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 743px;
        }

        .auto-style2 {
            width: 755px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <script  type="text/javascript">       
        </script>
         
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
         
        </asp:ScriptManager>
        <dx:ASPxHiddenField ID="hfState" runat="server">
        </dx:ASPxHiddenField>
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
                            <Paddings PaddingTop="9px" PaddingBottom="1px"></Paddings>
                        </ItemStyle>
                        <SubMenuItemStyle Width="32px">
                        </SubMenuItemStyle>
                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify"></SubMenuStyle>
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
                            </dx:ASPxMenu>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>

        <asp:UpdatePanel ID="pnlKryesor" runat="server">
            <ContentTemplate>
                <dx:ASPxPopupControl ID="popMesazh" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                    ClientInstanceName="popMesazh" CloseAction="CloseButton" EnableAnimation="False"
                    EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" Width="400px">
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                            <dx:ASPxPanel ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="371px">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel Wrap="False" ID="lblMsgbox" runat="server" ForeColor="Red" ClientInstanceName="lblMsgbox" ClientIDMode="AutoID" Text="Jeni te sigurte qe doni te kryeni blerjen me pike+leke?">
                                        </dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <div style="text-align: right;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                            Text="Ok" AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s, e) {
	popMesazh.Hide();
                                                                if(isPorosi)
                                                                porositOk();
                                                                else 
kaloArtikullNeGride();
}" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo" AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s, e) {
		popMesazh.Hide();
}" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxPanel>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <table width="100%">
                    <tr id="kontakti">
                        <td>

                            <dx:ASPxLabel ID="lblNrKontakti" runat="server" AssociatedControlID="txtNrKontakti" ClientInstanceName="lblNrKontakti"
                                Wrap="False" Text="Nr Kontaktit">
                            </dx:ASPxLabel>
                        </td>
                        <td align="right">

                            <dx:ASPxLabel ID="lbl355" runat="server" AssociatedControlID="txtNrKontakti" ClientInstanceName="lbl355"
                                Wrap="False" Text="3556">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtNrKontakti" Width="98%" runat="server" ClientInstanceName="txtNrKontakti">
                                <ValidationSettings Display="Dynamic" ValidationGroup="entries" ErrorDisplayMode="ImageWithTooltip"
                                    ValidateOnLeave="true">
                                    <RequiredField IsRequired="True" />
                                    <RegularExpression ValidationExpression="[0-9.,]*" ErrorText="Lejohen vetem numra"></RegularExpression>
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                        <td height="30px">
                            <dx:ASPxButton ID="btnGjeneroPin" runat="server" Text="Gjenero Pin" ClientInstanceName="btnGjeneroPin" ValidationGroup="entries" OnClick="btnGjeneroPin_Click">
                                <ClientSideEvents  Click="btnGjeneroPinClick" />
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxButton>

                        </td>
                    </tr>
                    <tr id="pin">
                        <td>

                            <dx:ASPxLabel ID="lblPIN" runat="server" AssociatedControlID="txtPIN" ClientInstanceName="lblPIN"
                                Wrap="False" Text="PIN">
                            </dx:ASPxLabel>
                        </td>
                        <td></td>
                        <td>
                            <dx:ASPxTextBox ID="txtPIN" runat="server" ClientInstanceName="txtPIN">
                                <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ErrorDisplayMode="ImageWithTooltip"
                                    ValidateOnLeave="False">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                        <td height="30px">
                            <dx:ASPxButton ID="btnVerifikoPIN" runat="server" Text="Verifiko Pin" ClientInstanceName="btnVerifikoPIN" ValidationGroup="entries1" OnClick="btnVerifikoPIN_Click">
                                <ClientSideEvents  Click="btnVerifikoPINClick" />
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxButton>

                        </td>
                    </tr>

                    <tr id="bundle">
                        <td>

                            <dx:ASPxLabel ID="lblKodiBundle" runat="server" AssociatedControlID="txtKodiBundle" ClientInstanceName="lblKodiBundle"
                                Wrap="False" Text="MSISDN qe fiton oferten">
                            </dx:ASPxLabel>
                        </td>
                        <td></td>
                        <td>
                            <dx:ASPxTextBox ID="txtKodiBundle" Width="100%" runat="server" ClientInstanceName="txtKodiBundle">
                                <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ErrorDisplayMode="ImageWithTooltip"
                                    ValidateOnLeave="False">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                        <td height="30px">
                            <dx:ASPxButton ID="btnAktivizo" runat="server" Text="Aktivizo" ClientInstanceName="btnAktivizo" ValidationGroup="entries1" OnClick="btnAktivizo_Click">

                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxButton>

                        </td>
                    </tr>
                    <tr id="kontaktiBazaar">
                        <td>

                            <dx:ASPxLabel ID="lblKontaktiBazaar" runat="server" AssociatedControlID="txtKontaktiBazaar" ClientInstanceName="lblKontaktiBazaar"
                                Wrap="False" Text="Nr Kontaktit">
                            </dx:ASPxLabel>
                        </td>
                        <td align="right">

                            <dx:ASPxLabel ID="lbl355B" runat="server" AssociatedControlID="txtKontaktiBazaar" ClientInstanceName="lbl355B"
                                Wrap="False" Text="3556">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtKontaktiBazaar" Width="100%" runat="server" ClientInstanceName="txtKontaktiBazaar">
                                <ValidationSettings Display="Dynamic" ValidationGroup="entries" ErrorDisplayMode="ImageWithTooltip"
                                    ValidateOnLeave="true">
                                    <RequiredField IsRequired="True" />
                                    <RegularExpression ValidationExpression="[0-9.,]*" ErrorText="Lejohen vetem numra"></RegularExpression>
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                        <td height="30px">
                            <dx:ASPxButton ID="btnGjeneroPinBazaar" runat="server" Text="Gjenero Pin" ClientInstanceName="btnGjeneroPinBazaar" ValidationGroup="entries" OnClick="btnGjeneroPinBazaar_Click">

                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxButton>

                        </td>
                    </tr>

                    <tr id="pinBazaar">
                        <td>

                            <dx:ASPxLabel ID="lblPINBazaar" runat="server" AssociatedControlID="txtPINBazaar" ClientInstanceName="lblPINBazaar"
                                Wrap="False" Text="PIN">
                            </dx:ASPxLabel>
                        </td>
                        <td></td>
                        <td>
                            <dx:ASPxTextBox ID="txtPINBazaar" Width="100%" runat="server" ClientInstanceName="txtPINBazaar">
                                <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ErrorDisplayMode="ImageWithTooltip"
                                    ValidateOnLeave="False">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                        <td height="30px">
                            <dx:ASPxButton ID="btnVerifikoPINBazaar" runat="server" Text="Verifiko Pin" ClientInstanceName="btnVerifikoPINBazaar" ValidationGroup="entries1" OnClick="btnVerifikoPINBazaar_Click">

                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxButton>

                        </td>
                    </tr>
                    <tr id="discount">
                        <td>

                            <dx:ASPxLabel ID="lblKodi" runat="server" AssociatedControlID="txtKodi" ClientInstanceName="lblKodi"
                                Wrap="False" Text="Kodi">
                            </dx:ASPxLabel>
                        </td>
                        <td></td>
                        <td>
                            <dx:ASPxTextBox ID="txtKodi" Width="100%" runat="server" ClientInstanceName="txtKodi">
                                <ValidationSettings Display="Dynamic" ValidationGroup="entries" ErrorDisplayMode="ImageWithTooltip"
                                    ValidateOnLeave="False">
                                    <%--  <RequiredField IsRequired="True" />--%>
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>

                            </dx:ASPxTextBox>
                        </td>
                        <td height="30px">
                            <dx:ASPxButton ID="btnValido" runat="server" Text="Valido Kod" ClientInstanceName="btnValido" ValidationGroup="entries1" OnClick="btnValido_Click">

                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxButton>

                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <div id="dvArtikulli">

                    <table id="tblInformacion" width="100%">
                        <tr id="piket">
                            <td>
                                <dx:ASPxLabel ID="lblPiketGrumbulluara" runat="server" AssociatedControlID="lblPiket" ClientInstanceName="lblPiketGrumbulluara"
                                    Wrap="False" Text="Piket e grumbulluara">
                                </dx:ASPxLabel>
                            </td>
                            <td width="50%">
                                <dx:ASPxLabel ID="lblPiket" runat="server" ClientInstanceName="lblPiket" Font-Bold="true" Font-Size="Large"
                                    Wrap="False" Text="0">
                                </dx:ASPxLabel>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <br style="width: 100px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="lblListaDhuratave" runat="server" ClientInstanceName="lblListaDhuratave"
                                    Wrap="False" Text="Lista e dhuratave">
                                </dx:ASPxLabel>
                            </td>
                        </tr>

                    </table>


                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="gvLupaArtikull" runat="server" ClientInstanceName="gvLupaArtikull"
                                    OnDataBound="gvLupaArtikull_DataBound"
                                    OnAfterPerformCallback="gvLupaArtikull_AfterPerformCallback" EnableCallbackCompression="True" EnableCallBacks="true"
                                    Width="100%" OnCustomCallback="gvLupaArtikull_CustomCallback" OnHtmlRowCreated="gvLupaArtikull_HtmlRowCreated">
                                    <SettingsPager></SettingsPager>
                                    <StylesPager Summary-Width="100%" PageNumber-Width="100%">
                                    </StylesPager>
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <StylesEditors>
                                        <ProgressBar Height="100%">
                                        </ProgressBar>
                                    </StylesEditors>
                                    <Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto" VerticalScrollBarStyle="Standard" />
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hfStatus" runat="server" Value="false" />
                    <asp:HiddenField ID="hfStatusDD" runat="server" />
                    <asp:HiddenField ID="hfStatusBundle" runat="server" Value="false" />
                    <asp:HiddenField ID="hfKodKuponiDD" runat="server" Value="" />
                    <asp:HiddenField runat="server" ID="hfTenure" />

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hfIdMag" runat="server" />
        <asp:HiddenField ID="hfData" runat="server" />
        <asp:HiddenField ID="hfLloji" runat="server" />
    </form>
</body>
</html>
