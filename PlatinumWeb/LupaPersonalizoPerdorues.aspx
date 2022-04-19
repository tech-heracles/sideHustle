<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaPersonalizoPerdorues.aspx.cs" Inherits="PlatinumWeb.LupaPersonalizoPerdorues" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaPersonalizoPerdorues.aspx-IMB5.7.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
          </asp:ScriptManager>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
            </dx:ASPxGlobalEvents>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
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
                     
                    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                        Font-Size="9pt" Modal="True" ImagePosition="Top">
                        <LoadingDivStyle Opacity="30">
                        </LoadingDivStyle>
                    </dx:ASPxLoadingPanel>
                     <asp:HiddenField ID="hfStatusi" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
                ActiveTabIndex="0"   TabSpacing="3px">
                <ContentStyle>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                </ContentStyle>
                <TabPages>
                    <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl3" runat="server">
                                <table id="tblPersonalizo" class="renditKontrolleDy">
                                    <tr>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="emri_TextBox" ID="lblEmri" runat="server"
                                                Text="Emri:" ClientInstanceName="lblEmri">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth50">
                                            <dx:ASPxTextBox ID="emri_TextBox" runat="server" TabIndex="1" ClientInstanceName="emri_TextBox"
                                                Width="100%">
                                                <ClientSideEvents TextChanged="function(s, e) {	txtEmri2.SetText(emri_TextBox.GetText());
	                                                 }" />
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                    ValidationGroup="entries" SetFocusOnError="True">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ValidationExpression="[a-z,A-Z,0-9]*" ErrorText="Emri duhet te permbaje vetem shkronja dhe numra!" />
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                </DisabledStyle>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="mbiemri_TextBox" ID="lblMbiemri"
                                                runat="server" Text="Mbiemri:" ClientInstanceName="lblMbiemri">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth50">
                                            <dx:ASPxTextBox ID="mbiemri_TextBox" runat="server" TabIndex="2" ClientInstanceName="mbiemri_TextBox"
                                                Width="100%">
                                                <ClientSideEvents TextChanged="function(s, e) {	txtMbiemri2.SetText(mbiemri_TextBox.GetText());
	                                                  }" />
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                    ValidationGroup="entries" SetFocusOnError="True">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ValidationExpression="[a-z,A-Z,0-9]*" ErrorText="Mbiemri duhet te permbaje vetem shkronja dhe numra!" />
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                </DisabledStyle>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="email_TextBox" ID="lblEmail" runat="server"
                                                Text="E-mail:" ClientInstanceName="lblEmail">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth50">
                                            <dx:ASPxTextBox ID="email_TextBox" TabIndex="14" ClientInstanceName="email_TextBox" runat="server"
                                                Width="100%">
                                                <ClientSideEvents TextChanged="function(s, e) {}" />
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                    ValidationGroup="entries" SetFocusOnError="True">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorText="Format i gabuar e-mail!" />
                                                </ValidationSettings>
                                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                </DisabledStyle>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="tel_TextBox" ID="ASPxLabel1" runat="server"
                                                Text="Tel:" ClientInstanceName="lblTel">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth50">
                                            <dx:ASPxTextBox ID="tel_TextBox" runat="server" Width="100%" TabIndex="16" ClientInstanceName="tel_TextBox">
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                    ValidationGroup="entries" SetFocusOnError="True" ValidateOnLeave="false">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ValidationExpression="^\s*\+?\s*([0-9 \(\)][\s-]*){9,}$" ErrorText="Formati nuk eshte i sakte!" />
                                                </ValidationSettings>
                                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                </DisabledStyle>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="fax_TextBox" ID="lblFax" runat="server"
                                                Text="Fax:" ClientInstanceName="lblFax">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth50">
                                            <dx:ASPxTextBox ID="fax_TextBox" runat="server" TabIndex="15" Width="100%" ClientInstanceName="fax_TextBox">
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                    ValidationGroup="entries" SetFocusOnError="True">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ValidationExpression="[0-9]*" ErrorText="Sheno vetem numra!" />
                                                </ValidationSettings>
                                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                </DisabledStyle>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Name="Foto" Text="Foto">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div id="dvZgjidhImazh">
                                            <table border="0" id="mainContainer">
                                                <tr>
                                                    <td class="permbajtja">
                                                        <table>
                                                            <tr>
                                                                <td style="padding-right: 20px; vertical-align: top;">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxLabel ID="lblZgjidhImaxh" runat="server" Text="Zgjidh Imazh:" AssociatedControlID="ngarkoImazh">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxUploadControl ID="ngarkoImazh" runat="server" ClientInstanceName="ngarkuesi"
                                                                                    ShowProgressPanel="True" Size="35" OnFileUploadComplete="ngarkoImazh_FileUploadComplete">
                                                                                    <ClientSideEvents FileUploadComplete="function(s, e) { Ngarkuesi_NeFileNgarkimPlotesuar(e); btnShfaqImazh.DoClick();}"
                                                                                        FilesUploadComplete="function(s, e) { Ngarkuesi_NeFiletNgarkimPlotesuar(e); }"
                                                                                        FileUploadStart="function(s, e) { Ngarkuesi_NeNgarkimFillim(); }"
                                                                                        TextChanged="function(s, e) { UpdateButoniNgarkim(); }"></ClientSideEvents>
                                                                                    <ValidationSettings MaxFileSizeErrorText="Imazhi qe keni zgjedhur eshte shume i madh!"
                                                                                        MaxFileSize="1048576" AllowedFileExtensions=".jpg,.jpeg,.jpe,.gif,.bmp,.png">
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxUploadControl>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="shenim">
                                                                                <dx:ASPxLabel ID="lblTipLejuar" runat="server" Text="Tipe Logosh te Lejuar: jpg, jpeg, gif, jpe, bmp, png"
                                                                                    Font-Size="8pt">
                                                                                </dx:ASPxLabel>
                                                                                <br />
                                                                                <dx:ASPxLabel ID="lblMaksimumiLejuar" runat="server" Text="Maksimumi i madhesise se file-it: 1Mb"
                                                                                    Font-Size="8pt">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2" class="qelizeButoni">
                                                                                <dx:ASPxButton ID="btnNgarko" runat="server" AutoPostBack="False" Text="Ngarko"
                                                                                    ClientInstanceName="btnNgarko" Width="100px">
                                                                                    <ClientSideEvents Click="function(s, e) { ngarkuesi.Upload(); }" />
                                                                                </dx:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="ShfaqjeProve">
                                                                    <asp:UpdatePanel ID="pnlImazh" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <dx:ASPxBinaryImage ID="ASPxBinaryImage1" runat="server" EmptyImage-Url="images/Korniza.bmp"
                                                                                ClientInstanceName="ppp" ImageAlign="Middle" Width="50" Height="50">
                                                                            </dx:ASPxBinaryImage>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
                                        </dx:ASPxHiddenField>
                                        <div style="visibility: hidden">
                                            <dx:ASPxButton ID="btnShfaqImazh" runat="server" CausesValidation="False" ClientInstanceName="btnShfaqImazh"
                                                OnClick="btnShfaqImazh_Click" Text="Ok">
                                            </dx:ASPxButton>
                                        </div>
                                       
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                </TabPages>
                <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
            </dxtc:ASPxPageControl >            
        </div>
    </form>
</body>
</html>
